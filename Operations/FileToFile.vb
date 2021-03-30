Imports System
Imports System.IO
Imports System.Windows.Forms

Namespace Operations
    Public Class FileToFile
        Public Shared Sub Rename(sourcePath As String, targetName As String)
            Dim fileProperties As New FileInfo(sourcePath)
            Dim fullTargetName = fileProperties.DirectoryName & Path.DirectorySeparatorChar & targetName

            Try
                If WalkmanLib.IsFileOrDirectory(fullTargetName).HasFlag(PathEnum.Exists) AndAlso sourcePath <> fullTargetName Then
                    Select Case MessageBox.Show($"Target ""{fullTargetName}"" already exists! Remove first?", "Target exists",
                                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
                        Case DialogResult.Yes
                            Delete({fullTargetName}, skipDialog:=True)
                        Case DialogResult.Cancel
                            Exit Sub
                    End Select
                End If

                fileProperties.MoveTo(fullTargetName)
            Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
                Select Case WalkmanLib.CustomMsgBox(ex.Message, Other.cMBTitle, Other.cMBbRelaunch, Other.cMBbRunSysTool, Other.cMBbCancel,
                                                    MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                    Case Other.cMBbRelaunch
                        FileBrowser.RestartAsAdmin()
                    Case Other.cMBbRunSysTool
                        WalkmanLib.RunAsAdmin("cmd", $"/c ren ""{sourcePath}"" ""{targetName}"" & pause")
                End Select
            Catch ex As IOException When Other.Win32FromHResult(ex.HResult) = Other.shareViolation
                If MessageBox.Show($"File ""{sourcePath}"" is in use! Open Handle Manager?", "Item in use",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                    Launch.HandleManager(sourcePath)
                End If
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
            End Try
        End Sub

        Public Shared Sub Move(sourcePath As String, targetPath As String, useShell As Boolean)
            Try
                If useShell Then
                    Dim pathInfo = WalkmanLib.IsFileOrDirectory(sourcePath)
                    If pathInfo.HasFlag(PathEnum.IsFile) Then
                        My.Computer.FileSystem.MoveFile(sourcePath, targetPath, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs)
                    ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) Then
                        My.Computer.FileSystem.MoveDirectory(sourcePath, targetPath, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs)
                    End If
                Else
                    If WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) AndAlso sourcePath <> targetPath Then
                        Select Case MessageBox.Show($"Target ""{targetPath}"" already exists! Remove first?", "Target exists",
                                                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
                            Case DialogResult.Yes
                                Delete({targetPath}, skipDialog:=True)
                            Case DialogResult.Cancel
                                Exit Sub
                        End Select
                    End If

                    File.Move(sourcePath, targetPath)
                End If
            Catch ex As OperationCanceledException ' ignore user cancellation
            Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
                Select Case WalkmanLib.CustomMsgBox(ex.Message, Other.cMBTitle, Other.cMBbRelaunch, Other.cMBbRunSysTool, Other.cMBbCancel,
                                                    MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                    Case Other.cMBbRelaunch
                        FileBrowser.RestartAsAdmin()
                    Case Other.cMBbRunSysTool
                        WalkmanLib.RunAsAdmin("cmd", $"/c move ""{sourcePath}"" ""{targetPath}"" & pause")
                End Select
            Catch ex As IOException When Other.Win32FromHResult(ex.HResult) = Other.shareViolation
                If MessageBox.Show($"File ""{sourcePath}"" is in use! Open Handle Manager?", "Item in use",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                    Launch.HandleManager(sourcePath)
                End If
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
            End Try
        End Sub

        Public Shared Sub Copy(sourcePath As String, targetPath As String, useShell As Boolean)
            Try
                Dim pathInfo = WalkmanLib.IsFileOrDirectory(sourcePath)
                If useShell Then
                    If pathInfo.HasFlag(PathEnum.IsFile) Then
                        My.Computer.FileSystem.CopyFile(sourcePath, targetPath, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs)
                    ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) Then
                        My.Computer.FileSystem.CopyDirectory(sourcePath, targetPath, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs)
                    End If
                Else
                    If pathInfo.HasFlag(PathEnum.IsFile) Then
                        If WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) AndAlso sourcePath <> targetPath AndAlso
                                MessageBox.Show($"Target ""{targetPath}"" already exists! Are you sure you want to overwrite it?", "Target exists",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                            Exit Sub
                        End If

                        Dim sourceStream As FileStream = Nothing
                        Dim targetStream As FileStream = Nothing

                        Try
                            sourceStream = File.OpenRead(sourcePath)
                            targetStream = File.OpenWrite(targetPath)
                            WalkmanLib.StreamCopy(sourceStream, targetStream, $"Copying {sourcePath} to {targetPath}...",
                                                  "File to File copy", Sub(s, e)
                                                                           If e.Error IsNot Nothing Then
                                                                               FileBrowser.ErrorParser(e.Error)
                                                                           End If
                                                                       End Sub)
                        Catch
                            sourceStream?.Dispose()
                            targetStream?.Dispose()
                            Throw
                        End Try
                    ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) Then
                        'BackgroundProgress.bwFolderOperations.RunWorkerAsync({"copy", sourcePath, targetPath})
                        'BackgroundProgress.ShowDialog()
                    End If
                End If
            Catch ex As OperationCanceledException ' ignore user cancellation
            Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
                Select Case WalkmanLib.CustomMsgBox(ex.Message, Other.cMBTitle, Other.cMBbRelaunch, Other.cMBbRunSysTool, Other.cMBbCancel,
                                                    MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                    Case Other.cMBbRelaunch
                        FileBrowser.RestartAsAdmin()
                    Case Other.cMBbRunSysTool
                        WalkmanLib.RunAsAdmin("xcopy", $"/F /H /K ""{sourcePath}"" ""{targetPath}*""")
                End Select
            Catch ex As IOException When Other.Win32FromHResult(ex.HResult) = Other.shareViolation
                If MessageBox.Show($"A file is in use! Open Handle Manager on ""{sourcePath}""?", "Item in use",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                    Launch.HandleManager(sourcePath)
                End If
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
            End Try
        End Sub
    End Class
End Namespace
