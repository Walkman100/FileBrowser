Imports System
Imports System.IO
Imports System.Windows.Forms

Namespace Operations
    Module Win32
        Public Sub CreateShortcut(sourcePath As String, targetPath As String)
            Try
                If WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) AndAlso sourcePath <> targetPath AndAlso
                        MessageBox.Show($"Target ""{targetPath}"" already exists! Are you sure you want to overwrite the shortcut's Target Path?",
                                        "Target exists", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                    Exit Sub
                End If

                WalkmanLib.CreateShortcut(targetPath, sourcePath)
            Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
                Select Case WalkmanLib.CustomMsgBox(ex.Message, Other.cMBTitle, Other.cMBbRelaunch, Other.cMBbRunSysTool, Other.cMBbCancel,
                                                    MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                    Case Other.cMBbRelaunch
                        FileBrowser.RestartAsAdmin()
                    Case Other.cMBbRunSysTool
                        Dim scriptPath As String = Environment.GetEnvironmentVariable("TEMP") & Path.DirectorySeparatorChar & "createShortcut.vbs"
                        Using writer As New StreamWriter(File.Open(scriptPath, FileMode.Create))
                            writer.WriteLine($"Set lnk = WScript.CreateObject(""WScript.Shell"").CreateShortcut(""{targetPath}"")")
                            writer.WriteLine($"lnk.TargetPath = ""{sourcePath}""")
                            writer.WriteLine("lnk.Save")
                        End Using

                        WalkmanLib.RunAsAdmin("wscript", scriptPath)
                End Select
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
            End Try
        End Sub

        Public Sub CreateSymlink(sourcePath As String, targetPath As String)
            Try
                If WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) AndAlso sourcePath <> targetPath Then
                    Select Case MessageBox.Show($"Target ""{targetPath}"" already exists! Remove first?", "Target exists",
                                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
                        Case DialogResult.Yes
                            Delete({targetPath}, skipDialog:=True)
                        Case DialogResult.Cancel
                            Exit Sub
                    End Select
                End If

                Dim pathInfo = WalkmanLib.IsFileOrDirectory(sourcePath)
                WalkmanLib.CreateSymLink(targetPath, sourcePath, pathInfo.HasFlag(PathEnum.IsDirectory))
            Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
                Select Case WalkmanLib.CustomMsgBox(ex.Message, Other.cMBTitle, Other.cMBbRelaunch, Other.cMBbRunSysTool, Other.cMBbCancel,
                                                    MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                    Case Other.cMBbRelaunch
                        FileBrowser.RestartAsAdmin()
                    Case Other.cMBbRunSysTool
                        Dim pathInfo = WalkmanLib.IsFileOrDirectory(sourcePath)
                        If pathInfo.HasFlag(PathEnum.IsFile) Then
                            WalkmanLib.RunAsAdmin("cmd", $"/c mklink ""{targetPath}"" ""{sourcePath}"" & pause")
                        ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) Then
                            WalkmanLib.RunAsAdmin("cmd", $"/c mklink /d ""{targetPath}"" ""{sourcePath}"" & pause")
                        End If
                End Select
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
            End Try
        End Sub

        Public Sub CreateHardlink(sourcePath As String, targetPath As String)
            Try
                If WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) AndAlso sourcePath <> targetPath Then
                    Select Case MessageBox.Show($"Target ""{targetPath}"" already exists! Remove first?", "Target exists",
                                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
                        Case DialogResult.Yes
                            Delete({targetPath}, skipDialog:=True)
                        Case DialogResult.Cancel
                            Exit Sub
                    End Select
                End If

                WalkmanLib.CreateHardLink(targetPath, sourcePath)
            Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
                Select Case WalkmanLib.CustomMsgBox(ex.Message, Other.cMBTitle, Other.cMBbRelaunch, Other.cMBbRunSysTool, Other.cMBbCancel,
                                                    MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                    Case Other.cMBbRelaunch
                        FileBrowser.RestartAsAdmin()
                    Case Other.cMBbRunSysTool
                        WalkmanLib.RunAsAdmin("cmd", $"/c mklink /h ""{targetPath}"" ""{sourcePath}"" & pause")
                End Select
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
            End Try
        End Sub

        Public Sub CreateJunction(sourcePath As String, targetPath As String)
            Try
                If WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) AndAlso sourcePath <> targetPath Then
                    Select Case MessageBox.Show($"Target ""{targetPath}"" already exists! Remove first?", "Target exists",
                                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
                        Case DialogResult.Yes
                            Delete({targetPath}, skipDialog:=True)
                        Case DialogResult.Cancel
                            Exit Sub
                    End Select
                End If

                WalkmanLib.CreateJunction(targetPath, sourcePath, True)
            Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin
                Select Case WalkmanLib.CustomMsgBox(ex.Message, Other.cMBTitle, Other.cMBbRelaunch, Other.cMBbRunSysTool, Other.cMBbCancel,
                                                    MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                    Case Other.cMBbRelaunch
                        FileBrowser.RestartAsAdmin()
                    Case Other.cMBbRunSysTool
                        WalkmanLib.RunAsAdmin("cmd", $"/c mklink /j ""{targetPath}"" ""{sourcePath}"" & pause")
                End Select
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
            End Try
        End Sub
    End Module
End Namespace
