Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Namespace Operations
    Module General
        Public Sub Delete(paths As String(), useShell As Boolean, deletePermanently As Boolean)
            Try
                For Each path As String In paths
                    Try
                        Dim pathInfo = WalkmanLib.IsFileOrDirectory(path)
                        If Helpers.PathContainsADS(path) Then
                            Trinet.Core.IO.Ntfs.DeleteAlternateDataStream(Helpers.GetADSPathFile(path), Helpers.GetADSPathStream(path))
                        ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) AndAlso File.GetAttributes(path).HasFlag(FileAttributes.ReparsePoint) Then
                            File.Delete(path)
                        ElseIf useShell OrElse Not deletePermanently Then
                            If pathInfo.HasFlag(PathEnum.IsFile) Then
                                My.Computer.FileSystem.DeleteFile(path, FileIO.UIOption.AllDialogs,
                                    If(deletePermanently, FileIO.RecycleOption.DeletePermanently, FileIO.RecycleOption.SendToRecycleBin))
                            ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) Then
                                My.Computer.FileSystem.DeleteDirectory(path, FileIO.UIOption.AllDialogs,
                                    If(deletePermanently, FileIO.RecycleOption.DeletePermanently, FileIO.RecycleOption.SendToRecycleBin))
                            End If
                        Else
                            If pathInfo.HasFlag(PathEnum.IsFile) Then
                                File.Delete(path)
                            ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) Then
                                'BackgroundProgress.bwFolderOperations.RunWorkerAsync({"delete", path})
                                'BackgroundProgress.ShowDialog()
                            End If
                        End If
                    Catch ex As OperationCanceledException ' ignore user cancellation
                    Catch ex As IOException When Other.Win32FromHResult(ex.HResult) = Other.shareViolation
                        If MessageBox.Show("File """ & path & """ is in use! Open Handle Manager?", "Item in use",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                            'HandleManager.Show(FileBrowser)
                            'HandleManager.Activate()
                        End If
                    End Try
                Next
            Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
                Select Case WalkmanLib.CustomMsgBox(ex.Message, Other.cMBTitle, Other.cMBbRelaunch, Other.cMBbRunSysTool, Other.cMBbCancel,
                                                    MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                    Case Other.cMBbRelaunch
                        FileBrowser.RestartAsAdmin()
                    Case Other.cMBbRunSysTool
                        WalkmanLib.RunAsAdmin("cmd", "/c del " & paths.PathsConcat() & " & pause")
                        Threading.Thread.Sleep(500)
                End Select
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
            End Try
        End Sub
    End Module
End Namespace
