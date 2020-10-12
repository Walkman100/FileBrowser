Imports System
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Trinet.Core.IO.Ntfs

Enum OS
    Windows
    Other
End Enum

Namespace Helpers
    Module Helpers
        Friend Function GetOS() As OS
            Return If(Environment.GetEnvironmentVariable("OS") = "Windows_NT", OS.Windows, OS.Other)
        End Function

        Private sfd As New SaveFileDialog 'With {.CheckFileExists = False}
        Private sdd As New Ookii.Dialogs.VistaFolderBrowserDialog With {.UseDescriptionForTitle = True}

        Public Sub CopyTo(paths As String(), manualEdit As Boolean)
            Dim target As String

            ' if multiple files will be copied, get directory to copy all files to
            If paths.Length > 1 Then
                target = Path.GetDirectoryName(paths(0))
                If manualEdit Then
                    If Operations.GetInput(target, "Copy Files", "Enter folder to copy files to:") = DialogResult.Cancel Then Exit Sub
                Else
                    sdd.SelectedPath = target
                    sdd.Description = "Select folder to copy files to:"
                    If sdd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Exit Sub
                    target = sdd.SelectedPath
                End If

            Else ' if one file will be copied, allow getting full file path to copy to
                target = paths(0)
                If manualEdit Then
                    If Operations.GetInput(target, "Copy File", "Enter path to copy file to:") = DialogResult.Cancel Then Exit Sub
                Else
                    sfd.FileName = Path.GetFileName(target)
                    sfd.InitialDirectory = Path.GetDirectoryName(target)
                    sfd.Title = "Select path to copy file to:"
                    If sfd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Exit Sub
                    target = sfd.FileName
                End If
            End If

            For Each path As String In paths
                Operations.Copy(path, target, FileBrowser.UseShell)
            Next
        End Sub

        Public Sub MoveTo(paths As String(), manualEdit As Boolean)
            Dim target As String
            If paths.Length > 1 Then
                target = Path.GetDirectoryName(paths(0))
                If manualEdit Then
                    If Operations.GetInput(target, "Move Files", "Enter folder to move files to:") = DialogResult.Cancel Then Exit Sub
                Else
                    sdd.SelectedPath = target
                    sdd.Description = "Select folder to move files to:"
                    If sdd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Exit Sub
                    target = sdd.SelectedPath
                End If
            Else
                target = paths(0)
                If manualEdit Then
                    If Operations.GetInput(target, "Move File", "Enter path to move file to:") = DialogResult.Cancel Then Exit Sub
                Else
                    sfd.FileName = Path.GetFileName(target)
                    sfd.InitialDirectory = Path.GetDirectoryName(target)
                    sfd.Title = "Select path to move file to:"
                    If sfd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Exit Sub
                    target = sfd.FileName
                End If
            End If

            For Each path As String In paths
                Operations.Move(path, target, FileBrowser.UseShell)
            Next
        End Sub

        Public Function CreateFile(rootPath As String, useInputBox As Boolean) As String
            Dim target As String = "New File"
            If useInputBox Then
                If Operations.GetInput(target, "Create File", "Enter name of file to create:") = DialogResult.Cancel Then Return Nothing
            Else
                sfd.FileName = target
                sfd.InitialDirectory = rootPath
                sfd.Title = "Create File"
                If sfd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Return Nothing
                target = sfd.FileName
            End If

            target = Path.Combine(If(rootPath, ""), target)
            If Clipboard.ContainsImage AndAlso Microsoft.VisualBasic.MsgBox("Image detected in clipboard! Use it as file contents?",
                                                                            Microsoft.VisualBasic.MsgBoxStyle.YesNo) = Microsoft.VisualBasic.MsgBoxResult.Yes Then
                Clipboard.GetImage.Save(target)
                Return target
            End If

            Using f As StreamWriter = File.CreateText(target)
                Dim text As String = Clipboard.GetText()
                If Operations.GetInput(text, "File Contents", "Enter file contents:") = DialogResult.OK Then
                    f.Write(text)
                End If
            End Using
            Return target
        End Function

        Public Function CreateFolder(rootPath As String, useInputBox As Boolean) As String
            Dim target As String = "New Folder"
            If useInputBox Then
                If Operations.GetInput(target, "Create Folder", "Enter name of folder to create:") = DialogResult.Cancel Then Return Nothing
            Else
                sfd.FileName = target
                sfd.InitialDirectory = rootPath
                sfd.Title = "Create Folder"
                If sfd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Return Nothing
                target = sfd.FileName
            End If

            target = Path.Combine(If(rootPath, ""), target)
            target = Directory.CreateDirectory(target).FullName
            Return target
        End Function

        Public Sub ShowFileExternal(filePath As String)
            If GetOS() = OS.Windows Then
                Launch.LaunchItem(filePath, "explorer.exe", "/select, ""{path}""")
            Else
                Launch.LaunchItem(filePath, "xdg-open", "{directory}")
            End If
        End Sub
    End Module

    Module GetFileInfo
        Public Function GetUrlTarget(path As String) As String
            Using fs As StreamReader = File.OpenText(path)
                While Not fs.EndOfStream
                    Dim line As String = fs.ReadLine()
                    If line?.StartsWith("URL=", True, Globalization.CultureInfo.InvariantCulture) Then
                        Return line.Substring(4)
                    End If
                End While

                Return Nothing
            End Using
        End Function

        Private Const DownloadADS As String = "Zone.Identifier"

        Public Function GetDownloadURL(path As String) As String
            Try
                If AlternateDataStreamExists(path, DownloadADS) Then
                    Using fs As StreamReader = GetAlternateDataStream(path, DownloadADS).OpenText
                        While Not fs.EndOfStream
                            Dim line As String = fs.ReadLine()
                            If line?.StartsWith("HostUrl=", True, Globalization.CultureInfo.InvariantCulture) Then
                                Return line.Substring(8)
                            End If
                        End While
                    End Using
                End If
            Catch : End Try
            Return Nothing
        End Function

        Public Function GetDownloadReferrer(path As String) As String
            Try
                If AlternateDataStreamExists(path, DownloadADS) Then
                    Using fs As StreamReader = GetAlternateDataStream(path, DownloadADS).OpenText
                        While Not fs.EndOfStream
                            Dim line As String = fs.ReadLine()
                            If line?.StartsWith("ReferrerUrl=", True, Globalization.CultureInfo.InvariantCulture) Then
                                Return line.Substring(12)
                            End If
                        End While
                    End Using
                End If
            Catch : End Try
            Return Nothing
        End Function

        Public Function ConvSize(size As Double) As String
            Select Case Settings.SizeUnits
                Case 0 ' Auto (Decimal - 1000)
                    Return ConvSize(size, 10, False)
                Case 1 ' Auto (Binary  - 1024)
                    Return ConvSize(size, 10, True)
                Case 2 ' bytes (8 bits)
                    Return ConvSize(size, 0, False)
                Case 3 ' kB  (Decimal - 1000)
                    Return ConvSize(size, 1, False)
                Case 4 ' KiB (Binary  - 1024)
                    Return ConvSize(size, 1, True)
                Case 5 ' MB  (Decimal - 1000)
                    Return ConvSize(size, 2, False)
                Case 6 ' MiB (Binary  - 1024)
                    Return ConvSize(size, 2, True)
                Case 7 ' GB  (Decimal - 1000)
                    Return ConvSize(size, 3, False)
                Case 8 ' GiB (Binary  - 1024)
                    Return ConvSize(size, 3, True)
                Case 9 ' TB  (Decimal - 1000)
                    Return ConvSize(size, 4, False)
                Case 10 'TiB (Binary  - 1024)
                    Return ConvSize(size, 4, True)
                Case 11 'PB  (Decimal - 1000)
                    Return ConvSize(size, 5, False)
                Case 12 'PiB (Binary  - 1024)
                    Return ConvSize(size, 5, True)
            End Select
            Return Nothing
        End Function

        ''' <param name="magnitude">
        ''' Order of magnitude, e.g. 0 for bytes, 1 for KB, 2 for MB
        ''' <br/>Use 10 for auto-selection
        ''' </param>
        Private Function ConvSize(size As Double, magnitude As Integer, binary As Boolean) As String
            Dim mult As Integer = If(binary, 1024, 1000)

            If magnitude = 10 Then
                Select Case size
                    Case Is > mult ^ 5
                        magnitude = 5
                    Case Is > mult ^ 4
                        magnitude = 4
                    Case Is > mult ^ 3
                        magnitude = 3
                    Case Is > mult ^ 2
                        magnitude = 2
                    Case Is > mult
                        magnitude = 1
                    Case Else
                        magnitude = 0
                End Select
            End If

            size /= mult ^ magnitude
            If binary Then size = Math.Truncate(size * 1000 ^ magnitude) / 1000 ^ magnitude

            Dim postString As String = ""
            Select Case magnitude
                Case 0 : postString = " bytes"
                Case 1 : postString = If(binary, " KiB", " kB")
                Case 2 : postString = If(binary, " MiB", " MB")
                Case 3 : postString = If(binary, " GiB", " GB")
                Case 4 : postString = If(binary, " TiB", " TB")
                Case 5 : postString = If(binary, " PiB", " PB")
            End Select

            Return size.ToString("#,##0.### ### ### ### ### ### ### ###").Trim & postString
        End Function
    End Module

    Module ADSHelpers
        Public Function GetADSFile(adsInfo As AlternateDataStreamInfo) As String
            Return Path.GetFileName(adsInfo.FilePath)
        End Function

        Public Function GetADSDirectory(adsInfo As AlternateDataStreamInfo) As String
            Return Path.GetDirectoryName(adsInfo.FilePath)
        End Function

        Public Function GetADSPath(adsInfo As AlternateDataStreamInfo) As String
            Return adsInfo.FilePath & ":" & adsInfo.Name
        End Function

        Public Function PathContainsADS(path As String) As Boolean
            Return path.Count(Function(c As Char) c = ":"c) > 1
        End Function

        Public Function GetADSPathStream(adsPath As String) As String
            Return adsPath.Substring(adsPath.IndexOf(":"c, 2) + 1) ' start at 2 to ignore DriveSeparator
        End Function

        Public Function GetADSPathFile(adsPath As String) As String
            Return adsPath.Remove(adsPath.IndexOf(":"c, 2)) ' start at 2 to ignore DriveSeparator
        End Function
    End Module
End Namespace
