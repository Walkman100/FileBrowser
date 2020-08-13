Imports System
Imports System.Diagnostics
Imports System.IO
Imports Trinet.Core.IO.Ntfs

Public Class Launch
    Private Shared Function FormatEntry(path As String, format As String) As String
        If format Is Nothing Then Return path
        If Helpers.PathContainsADS(path) Then
            If Not AlternateDataStreamExists(Helpers.GetADSPathFile(path), Helpers.GetADSPathStream(path)) Then Return Nothing

            Dim adsInfo As AlternateDataStreamInfo = GetAlternateDataStream(Helpers.GetADSPathFile(path), Helpers.GetADSPathStream(path))

            If format.Contains("{path}") Then
                format = format.Replace("{path}", Helpers.GetADSPath(adsInfo))
            End If
            If format.Contains("{directory}") Then
                format = format.Replace("{directory}", Helpers.GetADSDirectory(adsInfo))
            End If
            If format.Contains("{name}") Then
                format = format.Replace("{name}", Helpers.GetADSFile(adsInfo) & ":" & adsInfo.Name)
            End If
            If format.Contains("{namenoext}") Then
                format = format.Replace("{namenoext}", IO.Path.GetFileNameWithoutExtension(adsInfo.FilePath) & ":" & adsInfo.Name)
            End If
            If format.Contains("{fileext}") Then
                format = format.Replace("{fileext}", IO.Path.GetExtension(Helpers.GetADSPath(adsInfo)))
            End If
            If format.Contains("{openwith}") Then
                format = format.Replace("{openwith}", WalkmanLib.GetOpenWith(Helpers.GetADSPath(adsInfo)))
            End If
            If format.Contains("{target}") Then
                If adsInfo.Attributes.HasFlag(FileAttributes.ReparsePoint) Then
                    format = format.Replace("{target}", WalkmanLib.GetSymlinkTarget(Helpers.GetADSPath(adsInfo)))
                Else
                    format = format.Replace("{target}", WalkmanLib.GetShortcutInfo(Helpers.GetADSPath(adsInfo)).TargetPath)
                End If
            End If
        Else
            If WalkmanLib.IsFileOrDirectory(path) = PathEnum.NotFound Then Return Nothing

            Dim fileInfo As New FileInfo(path)

            If format.Contains("{path}") Then
                format = format.Replace("{path}", fileInfo.FullName)
            End If
            If format.Contains("{directory}") Then
                format = format.Replace("{directory}", fileInfo.DirectoryName)
            End If
            If format.Contains("{name}") Then
                format = format.Replace("{name}", fileInfo.Name)
            End If
            If format.Contains("{namenoext}") Then
                format = format.Replace("{namenoext}", fileInfo.NameNoExt)
            End If
            If format.Contains("{fileext}") Then
                format = format.Replace("{fileext}", fileInfo.Extension)
            End If
            If format.Contains("{openwith}") Then
                format = format.Replace("{openwith}", WalkmanLib.GetOpenWith(path))
            End If
            If format.Contains("{target}") Then
                If fileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint) Then
                    format = format.Replace("{target}", WalkmanLib.GetSymlinkTarget(path))
                ElseIf fileInfo.Extension.ToLower() = ".lnk" Then
                    format = format.Replace("{target}", WalkmanLib.GetShortcutInfo(path).TargetPath)
                Else
                    Throw New InvalidOperationException(String.Format("""{0}"" is not a SymLink or Shortcut!", path))
                End If
            End If
        End If

        format = Environment.ExpandEnvironmentVariables(format)
        Return format
    End Function

    Public Shared Sub LaunchItem(path As String, fileFormat As String, argumentsFormat As String)
        fileFormat = FormatEntry(path, fileFormat)
        argumentsFormat = FormatEntry(path, argumentsFormat)

        Process.Start(fileFormat, argumentsFormat)
    End Sub

    Public Shared Sub RunAsAdmin(path As String, fileFormat As String, argumentsFormat As String)
        fileFormat = FormatEntry(path, fileFormat)
        argumentsFormat = FormatEntry(path, argumentsFormat)

        WalkmanLib.RunAsAdmin(fileFormat, argumentsFormat)
    End Sub

    Public Shared Sub ExecuteItem(path As String, fileFormat As String, argumentsFormat As String)
        fileFormat = FormatEntry(path, fileFormat)
        argumentsFormat = FormatEntry(path, argumentsFormat)

        Process.Start(New ProcessStartInfo(fileFormat, argumentsFormat) With {.UseShellExecute = False})
    End Sub

    Public Shared Sub ShowPath(path As String, format As String)
        path = FormatEntry(path, format)

        'FileBrowser.ShowPath(path)
    End Sub

    Public Shared Sub WinProperties(path As String, format As String, Optional tab As String = Nothing)
        path = FormatEntry(path, format)

        WalkmanLib.ShowProperties(path, tab)
    End Sub

    Public Shared Sub OpenWith(path As String, format As String)
        path = FormatEntry(path, format)

        WalkmanLib.OpenWith(path)
    End Sub
End Class
