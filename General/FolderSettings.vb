Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Public Class FolderSettings
    Class FileAccess
        Private Const settingsFileName As String = ".directory"
        Private Const settingsSectionName As String = "[FileBrowser]"

        Private Shared Function getFolderSettingsFile(folderPath As String) As StreamReader
            Dim settingsPath As String = Path.Combine(folderPath, settingsFileName)
            If File.Exists(settingsPath) Then
                Return New StreamReader(settingsPath)
            Else
                Return Nothing
            End If
        End Function
        Private Shared Function lineIsSectionName(line As String) As Boolean
            Return line.Trim() = settingsSectionName
        End Function
        Private Shared Function getLineSettingName(line As String) As String
            If Not line.Contains("=") Then Return line.Trim()

            Return line.Remove(line.IndexOf("="c)).Trim()
        End Function
        Private Shared Function getLineSettingValue(line As String) As String
            If Not line.Contains("=") Then Return Nothing

            Return line.Substring(line.IndexOf("="c) + 1).Trim()
        End Function

        Public Shared Function FolderHasSettings(folderPath As String) As Boolean
            Using sr As StreamReader = getFolderSettingsFile(folderPath)
                If sr Is Nothing Then Return False

                While Not sr.EndOfStream
                    If lineIsSectionName(sr.ReadLine()) Then Return True
                End While
            End Using

            Return False
        End Function

        Public Shared Function GetSetting(folderPath As String, settingName As String) As String
            Using sr As StreamReader = getFolderSettingsFile(folderPath)
                If sr Is Nothing Then Return Nothing

                Dim inSection As Boolean = False
                While Not sr.EndOfStream
                    Dim line As String = sr.ReadLine()

                    If inSection = True Then
                        '               reached end of section
                        If line.Trim() = String.Empty Then Return Nothing
                        '               reached next section
                        If line.Trim().StartsWith("[") AndAlso line.Trim().EndsWith("]") Then Return Nothing

                        If getLineSettingName(line) = settingName Then
                            Return getLineSettingValue(line)
                        End If
                    ElseIf lineIsSectionName(line) Then
                        inSection = True
                    End If
                End While
            End Using

            Return Nothing
        End Function

        Public Shared Sub SaveSetting(folderPath As String, settingName As String, settingValue As String)
            Dim settingLine As String = $"{settingName}={settingValue}"
            Dim settingsPath As String = Path.Combine(folderPath, settingsFileName)
            Dim fileContents As String() = If(File.Exists(settingsPath), File.ReadAllLines(settingsPath), Nothing)

            If Not File.Exists(settingsPath) Then
                ' file doesn't exist. create with just setting
                File.Create(settingsPath).Close()
                fileContents = {settingsSectionName, settingLine}
            ElseIf fileContents.All(Function(s) s.Trim() = String.Empty) Then
                ' file exists, but all lines are empty. replace with just setting
                fileContents = {settingsSectionName, settingLine}
            ElseIf Not fileContents.Any(Function(s As String) lineIsSectionName(s)) Then
                ' file exists, has content, but no SectionName found. append to the end
                Array.Resize(fileContents, fileContents.Length + 3)
                fileContents(fileContents.Length - 2) = settingsSectionName
                fileContents(fileContents.Length - 1) = settingLine
            Else
                ' file exits, has content, and SectionName found.
                Dim inSection As Boolean = False
                Dim sectionIndex As Integer = 0
                Dim setSetting As Boolean = False

                For i = 0 To fileContents.Length - 1
                    Dim line As String = fileContents(i)

                    If inSection = True Then
                        If getLineSettingName(line) = settingName Then
                            ' change existing setting value
                            fileContents(i) = settingLine
                            setSetting = True

                            Exit For
                        End If
                    ElseIf lineIsSectionName(line) Then
                        inSection = True
                        sectionIndex = i
                    End If
                Next

                If Not setSetting Then
                    ' insert new setting immediately after SectionName
                    Dim fileContentsList As List(Of String) = fileContents.ToList()
                    fileContentsList.Insert(sectionIndex + 1, settingLine)
                    fileContents = fileContentsList.ToArray()
                End If
            End If

            WalkmanLib.RemoveAttribute(settingsPath, FileAttributes.Hidden, AddressOf FileBrowser.ErrorParser)
            File.WriteAllLines(settingsPath, fileContents)
            WalkmanLib.AddAttribute(settingsPath, FileAttributes.Hidden, AddressOf FileBrowser.ErrorParser)
        End Sub
    End Class

    ''' <summary>Loads columns from folder config to columns in <see cref="FileBrowser.lstCurrent"/></summary>
    Public Shared Sub GetColumns(folderPath As String)
        Dim colLst As New List(Of Settings.Column)

        Dim colConfig As String = FileAccess.GetSetting(folderPath, "Columns")
        If String.IsNullOrWhiteSpace(colConfig) Then Return

        For Each colStr As String In colConfig.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)
            Dim colSplit1 As String() = colStr.Split({":"c}, StringSplitOptions.RemoveEmptyEntries)
            If colSplit1.Length <> 2 Then Continue For

            Dim col As New Settings.Column With {
                .SaveName = colSplit1(0).Trim()
            }

            Dim colSplit2 As String() = colSplit1(1).Split(","c)
            If colSplit2.Length <> 2 Then Continue For

            Integer.TryParse(colSplit2(0), col.DisplayIndex)
            Integer.TryParse(colSplit2(1), col.Width)

            colLst.Add(col)
        Next

        For Each col As Settings.Column In colLst
            With FileBrowser.lstCurrent.Columns.Cast(Of ColumnHeader).
                        First(Function(c) DirectCast(c.Tag, String).ToLowerInvariant() = col.SaveName.ToLowerInvariant())
                .DisplayIndex = col.DisplayIndex
                .Width = col.Width
            End With
        Next
    End Sub

    ''' <summary>Saves current columns from <see cref="FileBrowser.lstCurrent"/> to folder config</summary>
    Public Shared Sub SaveColumns(folderPath As String)
        Dim colLst As New List(Of Settings.Column)

        colLst.AddRange(FileBrowser.lstCurrent.Columns.Cast(Of ColumnHeader).
                                    Select(Function(ch) New Settings.Column With {
                                        .SaveName = DirectCast(ch.Tag, String),
                                        .DisplayIndex = ch.DisplayIndex,
                                        .Width = ch.Width
                                    }))

        Dim colConfig As String = Nothing
        For Each col As Settings.Column In colLst
            Dim colData As String = $"{col.SaveName}:{col.DisplayIndex},{col.Width}"

            If colConfig Is Nothing Then
                colConfig = colData
            Else
                colConfig &= $";{colData}"
            End If
        Next

        FileAccess.SaveSetting(folderPath, "Columns", colConfig)
    End Sub

    Public Shared Sub DeleteColumnConfig(folderPath As String)
        FileAccess.SaveSetting(folderPath, "Columns", Nothing)
    End Sub
End Class
