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
                            If deletePermanently OrElse MessageBox.Show("Cannot recycle AlternateDataStreams! Delete permanently?", "Deleting ADS",
                                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                                Trinet.Core.IO.Ntfs.DeleteAlternateDataStream(Helpers.GetADSPathFile(path), Helpers.GetADSPathStream(path))
                            End If
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

        Private sfd As New SaveFileDialog 'With {.CheckFileExists = False}
        Private sdd As New Ookii.Dialogs.VistaFolderBrowserDialog With {.UseDescriptionForTitle = True}

        Public Sub MoveTo(paths As String(), manualEdit As Boolean)
            Dim target As String
            If paths.Length > 1 Then
                target = Path.GetDirectoryName(paths(0))
                If manualEdit Then
                    If Input.GetInput(target, "Move Files", "Enter folder to move files to:") = DialogResult.Cancel Then Exit Sub
                Else
                    sdd.SelectedPath = target
                    sdd.Description = "Select folder to move files to:"
                    If sdd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Exit Sub
                    target = sdd.SelectedPath
                End If
            Else
                target = paths(0)
                If manualEdit Then
                    If Input.GetInput(target, "Move File", "Enter path to move file to:") = DialogResult.Cancel Then Exit Sub
                Else
                    sfd.FileName = Path.GetFileName(target)
                    sfd.InitialDirectory = Path.GetDirectoryName(target)
                    sfd.Title = "Select path to move file to:"
                    If sfd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Exit Sub
                    target = sfd.FileName
                End If
            End If

            For Each path As String In paths
                Delegator.Move(path, target, FileBrowser.UseShell)
            Next
        End Sub
        Public Sub CopyTo(paths As String(), manualEdit As Boolean)
            Dim target As String

            ' if multiple files will be copied, get directory to copy all files to
            If paths.Length > 1 Then
                target = Path.GetDirectoryName(paths(0))
                If manualEdit Then
                    If Input.GetInput(target, "Copy Files", "Enter folder to copy files to:") = DialogResult.Cancel Then Exit Sub
                Else
                    sdd.SelectedPath = target
                    sdd.Description = "Select folder to copy files to:"
                    If sdd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Exit Sub
                    target = sdd.SelectedPath
                End If

            Else ' if one file will be copied, allow getting full file path to copy to
                target = paths(0)
                If manualEdit Then
                    If Input.GetInput(target, "Copy File", "Enter path to copy file to:") = DialogResult.Cancel Then Exit Sub
                Else
                    sfd.FileName = Path.GetFileName(target)
                    sfd.InitialDirectory = Path.GetDirectoryName(target)
                    sfd.Title = "Select path to copy file to:"
                    If sfd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Exit Sub
                    target = sfd.FileName
                End If
            End If

            For Each path As String In paths
                Delegator.Copy(path, target, FileBrowser.UseShell)
            Next
        End Sub

        Public Function CreateFile(rootPath As String, useInputBox As Boolean) As String
            Dim target As String = "New File"
            If useInputBox Then
                If Input.GetInput(target, "Create File", "Enter name of file to create:") = DialogResult.Cancel Then Return Nothing
            Else
                sfd.FileName = target
                sfd.InitialDirectory = rootPath
                sfd.Title = "Create File"
                If sfd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Return Nothing
                target = sfd.FileName
            End If

            target = Path.Combine(If(rootPath, ""), target)
            If Clipboard.ContainsImage AndAlso MessageBox.Show("Image detected in clipboard! Use it as file contents?",
                                                               Application.ProductName, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Clipboard.GetImage.Save(target)
                Return target
            End If

            Try
                Using f As StreamWriter = File.CreateText(target)
                    Dim text As String = Clipboard.GetText()
                    If Input.GetInput(text, "File Contents", "Enter file contents:") = DialogResult.OK Then
                        f.Write(text)
                    End If
                End Using
            Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
                Select Case WalkmanLib.CustomMsgBox(ex.Message, Other.cMBTitle, Other.cMBbRelaunch, Other.cMBbRunSysTool, Other.cMBbCancel,
                                                    MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                    Case Other.cMBbRelaunch
                        FileBrowser.RestartAsAdmin()
                    Case Other.cMBbRunSysTool
                        WalkmanLib.RunAsAdmin("fsutil", "file createnew """ & target & """ 0")
                        Threading.Thread.Sleep(1000)
                    Case Other.cMBbCancel
                        Return Nothing
                End Select
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
            End Try

            Return target
        End Function
        Public Function CreateFolder(rootPath As String, useInputBox As Boolean) As String
            Dim target As String = "New Folder"
            If useInputBox Then
                If Input.GetInput(target, "Create Folder", "Enter name of folder to create:") = DialogResult.Cancel Then Return Nothing
            Else
                sfd.FileName = target
                sfd.InitialDirectory = rootPath
                sfd.Title = "Create Folder"
                If sfd.ShowDialog(FileBrowser) = DialogResult.Cancel Then Return Nothing
                target = sfd.FileName
            End If

            target = Path.Combine(If(rootPath, ""), target)

            Try
                target = Directory.CreateDirectory(target).FullName
            Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
                Select Case WalkmanLib.CustomMsgBox(ex.Message, Other.cMBTitle, Other.cMBbRelaunch, Other.cMBbRunSysTool, Other.cMBbCancel,
                                                    MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                    Case Other.cMBbRelaunch
                        FileBrowser.RestartAsAdmin()
                    Case Other.cMBbRunSysTool
                        WalkmanLib.RunAsAdmin("cmd", "/c mkdir """ & target & """ & pause")
                        Threading.Thread.Sleep(1000)
                    Case Other.cMBbCancel
                        Return Nothing
                End Select
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
            End Try

            Return target
        End Function
    End Module
End Namespace
