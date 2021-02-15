Imports System
Imports System.Diagnostics
Imports System.IO
Imports Trinet.Core.IO.Ntfs

Public Class Launch
    Private Shared Function FormatEntry(path As String, format As String, Optional returnPathOnFormatEmpty As Boolean = True) As String
        If String.IsNullOrEmpty(format) Then Return If(returnPathOnFormatEmpty, path, String.Empty)

        If Helpers.PathContainsADS(path) AndAlso AlternateDataStreamExists(Helpers.GetADSPathFile(path), Helpers.GetADSPathStream(path)) Then
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
        ElseIf WalkmanLib.IsFileOrDirectory(path) <> PathEnum.NotFound Then
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
        Else ' path is not found. try our best to replace values
            If format.Contains("{path}") Then
                format = format.Replace("{path}", path)
            End If
            If format.Contains("{directory}") Then
                format = format.Replace("{directory}", path.Remove(path.LastIndexOf(IO.Path.DirectorySeparatorChar)))
            End If
            If format.Contains("{name}") Then
                format = format.Replace("{name}", path.Substring(path.LastIndexOf(IO.Path.DirectorySeparatorChar) + 1))
            End If
        End If

        If format.Contains("{walkmanutils}") Then
            format = format.Replace("{walkmanutils}", WalkmanLib.GetWalkmanUtilsPath())
        End If

        format = Environment.ExpandEnvironmentVariables(format)
        Return format
    End Function

    Public Shared Sub LaunchItem(path As String, fileFormat As String, argumentsFormat As String)
        fileFormat = FormatEntry(path, fileFormat)
        argumentsFormat = FormatEntry(path, argumentsFormat, False)

        Process.Start(fileFormat, argumentsFormat)
    End Sub

    Public Shared Sub RunAsAdmin(path As String, fileFormat As String, argumentsFormat As String)
        fileFormat = FormatEntry(path, fileFormat)
        argumentsFormat = FormatEntry(path, argumentsFormat, False)

        Dim ieframePath As String = IO.Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "System32", "ieframe.dll")
        If fileFormat.ToLowerInvariant() = ieframePath.ToLowerInvariant() Then
            ' rundll32.exe "%WinDir%\system32\ieframe.dll",OpenURL FilePath
            argumentsFormat = argumentsFormat.Trim(""""c)
            WalkmanLib.RunAsAdmin("rundll32", String.Format("""{0}"",OpenURL {1}", ieframePath, argumentsFormat))
            Exit Sub
        End If

        For Each envVar As String In {"ProgramFiles", "ProgramFiles(x86)", "ProgramW6432"}
            Dim PhotoViewerPath As String = IO.Path.Combine(Environment.GetEnvironmentVariable(envVar), "Windows Photo Viewer", "PhotoViewer.dll")
            If fileFormat.ToLowerInvariant() = PhotoViewerPath.ToLowerInvariant() Then
                ' rundll32 "%ProgramFiles%\Windows Photo Viewer\PhotoViewer.dll", ImageView_Fullscreen FilePath
                argumentsFormat = argumentsFormat.Trim(""""c)
                WalkmanLib.RunAsAdmin("rundll32", String.Format("""{0}"", ImageView_Fullscreen {1}", PhotoViewerPath, argumentsFormat))
                Exit Sub
            End If
        Next

        WalkmanLib.RunAsAdmin(fileFormat, argumentsFormat)
    End Sub

    Public Shared Sub ExecuteItem(path As String, fileFormat As String, argumentsFormat As String)
        fileFormat = FormatEntry(path, fileFormat)
        argumentsFormat = FormatEntry(path, argumentsFormat, False)

        Process.Start(New ProcessStartInfo(fileFormat, argumentsFormat) With {.UseShellExecute = False})
    End Sub

    Public Shared Sub ShowPath(path As String, format As String)
        path = FormatEntry(path, format)

        FileBrowser.ShowFile(path)
    End Sub

    Public Shared Sub WinProperties(path As String, format As String, Optional tab As String = Nothing)
        path = FormatEntry(path, format)

        WalkmanLib.ShowProperties(path, tab)
    End Sub

    Public Shared Sub OpenWith(path As String, format As String)
        path = FormatEntry(path, format)

        WalkmanLib.OpenWith(path)
    End Sub

    Public Shared Sub Copy(paths As String(), format As String)
        Dim text As String = String.Empty
        For Each path As String In paths
            If Not text = String.Empty Then
                text &= Environment.NewLine
            End If
            text &= FormatEntry(path, format)
        Next

        WalkmanLib.SafeSetText(text)
    End Sub

    Public Shared Sub HandleManager(item As String, Optional args As String = Nothing)
        Dim walkmanUtilsPath As String = WalkmanLib.GetWalkmanUtilsPath()
        Dim handleManagerPath As String = Path.Combine(walkmanUtilsPath, "HandleManager.exe")

        If Not File.Exists(handleManagerPath) Then
            Windows.Forms.MessageBox.Show("Could not find HandleManager in WalkmanUtils install!" & Environment.NewLine & Environment.NewLine &
                                          "Looking for: " & handleManagerPath, "Launching HandleManager",
                                          Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Process.Start(handleManagerPath, If(args Is Nothing, "", args & " ") & """" & item & """")
    End Sub
End Class
