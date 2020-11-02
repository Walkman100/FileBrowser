Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class Operations
    ' cMBb = CustomMsgBoxBtn
    Public Const cMBbRelaunch As String = "Relaunch as Admin"
    Public Const cMBbRunSysTool As String = "Run System Tool as Admin"
    Public Const cMBbCancel As String = "Cancel"
    Public Const cMBTitle As String = "Access denied!"

    Private Shared Function Win32FromHResult(HResult As Integer) As Integer
        'getting Win32 error from HResult:
        ' https://docs.microsoft.com/en-us/dotnet/standard/io/handling-io-errors#handling-ioexception
        ' https://devblogs.microsoft.com/oldnewthing/20061103-07/?p=29133
        ' https://stackoverflow.com/a/426467/2999220
        Return (HResult And &HFFFF)
    End Function

    '32 (0x20) = ERROR_SHARING_VIOLATION: The process cannot access the file because it is being used by another process.
    Private Const shareViolation As Integer = &H20

#Region "File/Folder general"
    Public Shared Sub Rename(sourcePath As String, targetName As String)
        Dim fileProperties As New FileInfo(sourcePath)
        Dim fullTargetName = fileProperties.DirectoryName & Path.DirectorySeparatorChar & targetName

        Try
            If WalkmanLib.IsFileOrDirectory(fullTargetName).HasFlag(PathEnum.Exists) AndAlso sourcePath <> fullTargetName Then
                Select Case MsgBox("Target """ & fullTargetName & """ already exists! Remove first?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNoCancel)
                    Case MsgBoxResult.Yes
                        Delete({fullTargetName}, False, Nothing)
                    Case MsgBoxResult.Cancel
                        Exit Sub
                End Select
            End If

            fileProperties.MoveTo(fullTargetName)
        Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
            Select Case WalkmanLib.CustomMsgBox(ex.Message, cMBTitle, cMBbRelaunch, cMBbRunSysTool, cMBbCancel, MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                Case cMBbRelaunch
                    FileBrowser.RestartAsAdmin()
                Case cMBbRunSysTool
                    WalkmanLib.RunAsAdmin("cmd", "/c ren """ & sourcePath & """ """ & targetName & """ && pause")
            End Select
        Catch ex As IOException When Win32FromHResult(ex.HResult) = shareViolation
            If MsgBox("File """ & sourcePath & """ is in use! Open Handle Manager?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'HandleManager.Show(FileBrowser)
                'HandleManager.Activate()
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
                    My.Computer.FileSystem.MoveFile(sourcePath, targetPath, FileIO.UIOption.AllDialogs)
                ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) Then
                    My.Computer.FileSystem.MoveDirectory(sourcePath, targetPath, FileIO.UIOption.AllDialogs)
                End If
            Else
                If WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) AndAlso sourcePath <> targetPath Then
                    Select Case MsgBox("Target """ & targetPath & """ already exists! Remove first?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNoCancel)
                        Case MsgBoxResult.Yes
                            Delete({targetPath}, useShell, True)
                        Case MsgBoxResult.Cancel
                            Exit Sub
                    End Select
                End If

                File.Move(sourcePath, targetPath)
            End If
        Catch ex As OperationCanceledException ' ignore user cancellation
        Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
            Select Case WalkmanLib.CustomMsgBox(ex.Message, cMBTitle, cMBbRelaunch, cMBbRunSysTool, cMBbCancel, MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                Case cMBbRelaunch
                    FileBrowser.RestartAsAdmin()
                Case cMBbRunSysTool
                    WalkmanLib.RunAsAdmin("cmd", "/c move """ & sourcePath & """ """ & targetPath & """ & pause")
            End Select
        Catch ex As IOException When Win32FromHResult(ex.HResult) = shareViolation
            If MsgBox("File """ & sourcePath & """ is in use! Open Handle Manager?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'HandleManager.Show(FileBrowser)
                'HandleManager.Activate()
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
                    My.Computer.FileSystem.CopyFile(sourcePath, targetPath, FileIO.UIOption.AllDialogs)
                ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) Then
                    My.Computer.FileSystem.CopyDirectory(sourcePath, targetPath, FileIO.UIOption.AllDialogs)
                End If
            Else
                If pathInfo.HasFlag(PathEnum.IsFile) Then
                    If WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) AndAlso sourcePath <> targetPath AndAlso
                            MsgBox("Target """ & targetPath & """ already exists! Are you sure you want to overwrite it?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    File.Copy(sourcePath, targetPath, overwrite:=True)
                ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) Then
                    'BackgroundProgress.bwFolderOperations.RunWorkerAsync({"copy", sourcePath, targetPath})
                    'BackgroundProgress.ShowDialog()
                End If
            End If
        Catch ex As OperationCanceledException ' ignore user cancellation
        Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
            Select Case WalkmanLib.CustomMsgBox(ex.Message, cMBTitle, cMBbRelaunch, cMBbRunSysTool, cMBbCancel, MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                Case cMBbRelaunch
                    FileBrowser.RestartAsAdmin()
                Case cMBbRunSysTool
                    WalkmanLib.RunAsAdmin("xcopy", "/F /H /K """ & sourcePath & """ """ & targetPath & "*""")
            End Select
        Catch ex As IOException When Win32FromHResult(ex.HResult) = shareViolation
            If MsgBox("A file is in use! Open Handle Manager on """ & sourcePath & """?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'HandleManager.Show(FileBrowser)
                'HandleManager.Activate()
            End If
        Catch ex As Exception
            FileBrowser.ErrorParser(ex)
        End Try
    End Sub

    Public Shared Sub Delete(paths As String(), useShell As Boolean, deletePermanently As Boolean)
        Try
            For Each path As String In paths
                Try
                    Dim pathInfo = WalkmanLib.IsFileOrDirectory(path)
                    If useShell OrElse Not deletePermanently Then
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
                Catch ex As IOException When Win32FromHResult(ex.HResult) = shareViolation
                    If MsgBox("File """ & path & """ is in use! Open Handle Manager?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        'HandleManager.Show(FileBrowser)
                        'HandleManager.Activate()
                    End If
                End Try
            Next
        Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
            Select Case WalkmanLib.CustomMsgBox(ex.Message, cMBTitle, cMBbRelaunch, cMBbRunSysTool, cMBbCancel, MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                Case cMBbRelaunch
                    FileBrowser.RestartAsAdmin()
                Case cMBbRunSysTool
                    WalkmanLib.RunAsAdmin("cmd", "/c del " & paths.PathsConcat() & " & pause")
                    Threading.Thread.Sleep(500)
            End Select
        Catch ex As Exception
            FileBrowser.ErrorParser(ex)
        End Try
    End Sub
#End Region

#Region "Create Win32"
    Public Shared Sub CreateShortcut(sourcePath As String, targetPath As String)
        Try
            If WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) AndAlso sourcePath <> targetPath AndAlso
                    MsgBox("Target """ & targetPath & """ already exists! Are you sure you want to overwrite the shortcut's Target Path?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If

            WalkmanLib.CreateShortcut(targetPath, sourcePath)
        Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
            Select Case WalkmanLib.CustomMsgBox(ex.Message, cMBTitle, cMBbRelaunch, cMBbRunSysTool, cMBbCancel, MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                Case cMBbRelaunch
                    FileBrowser.RestartAsAdmin()
                Case cMBbRunSysTool
                    Dim scriptPath As String = Environment.GetEnvironmentVariable("temp") & Path.DirectorySeparatorChar & "createShortcut.vbs"
                    Using writer As New StreamWriter(File.Open(scriptPath, FileMode.Create))
                        writer.WriteLine("Set lnk = WScript.CreateObject(""WScript.Shell"").CreateShortcut(""" & targetPath & """)")
                        writer.WriteLine("lnk.TargetPath = """ & sourcePath & """")
                        writer.WriteLine("lnk.Save")
                    End Using

                    WalkmanLib.RunAsAdmin("wscript", scriptPath)
            End Select
        Catch ex As Exception
            FileBrowser.ErrorParser(ex)
        End Try
    End Sub

    Public Shared Sub CreateSymlink(sourcePath As String, targetPath As String)
        Try
            If WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) AndAlso sourcePath <> targetPath Then
                Select Case MsgBox("Target """ & targetPath & """ already exists! Remove first?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNoCancel)
                    Case MsgBoxResult.Yes
                        Delete({targetPath}, False, Nothing)
                    Case MsgBoxResult.Cancel
                        Exit Sub
                End Select
            End If

            Dim pathInfo = WalkmanLib.IsFileOrDirectory(sourcePath)
            WalkmanLib.CreateSymLink(targetPath, sourcePath, pathInfo.HasFlag(PathEnum.IsDirectory))
        Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
            Select Case WalkmanLib.CustomMsgBox(ex.Message, cMBTitle, cMBbRelaunch, cMBbRunSysTool, cMBbCancel, MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                Case cMBbRelaunch
                    FileBrowser.RestartAsAdmin()
                Case cMBbRunSysTool
                    Dim pathInfo = WalkmanLib.IsFileOrDirectory(sourcePath)
                    If pathInfo.HasFlag(PathEnum.IsFile) Then
                        WalkmanLib.RunAsAdmin("cmd", "/c mklink """ & targetPath & """ """ & sourcePath & """ & pause")
                    ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) Then
                        WalkmanLib.RunAsAdmin("cmd", "/c mklink /d """ & targetPath & """ """ & sourcePath & """ & pause")
                    End If
            End Select
        Catch ex As Exception
            FileBrowser.ErrorParser(ex)
        End Try
    End Sub

    Public Shared Sub CreateHardlink(sourcePath As String, targetPath As String)
        Try
            If WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) AndAlso sourcePath <> targetPath Then
                Select Case MsgBox("Target """ & targetPath & """ already exists! Remove first?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNoCancel)
                    Case MsgBoxResult.Yes
                        Delete({targetPath}, False, Nothing)
                    Case MsgBoxResult.Cancel
                        Exit Sub
                End Select
            End If

            WalkmanLib.CreateHardLink(targetPath, sourcePath)
        Catch ex As UnauthorizedAccessException When Not WalkmanLib.IsAdmin()
            Select Case WalkmanLib.CustomMsgBox(ex.Message, cMBTitle, cMBbRelaunch, cMBbRunSysTool, cMBbCancel, MessageBoxIcon.Exclamation, ownerForm:=FileBrowser)
                Case cMBbRelaunch
                    FileBrowser.RestartAsAdmin()
                Case cMBbRunSysTool
                    WalkmanLib.RunAsAdmin("cmd", "/c mklink /h """ & targetPath & """ """ & sourcePath & """ & pause")
            End Select
        Catch ex As Exception
            FileBrowser.ErrorParser(ex)
        End Try
    End Sub
#End Region

#Region "Input"
    Public Shared Function GetInput(ByRef input As String, Optional windowTitle As String = Nothing, Optional header As String = Nothing, Optional content As String = Nothing) As DialogResult
        If OokiiDialogsLoaded() Then
            Return OokiiInputBox(input, windowTitle, header, content)
        Else
            Dim inputBoxPrompt As String = header
            If content IsNot Nothing Then
                inputBoxPrompt &= Environment.NewLine & content
            End If

            input = InputBox(inputBoxPrompt, windowTitle, input)
            If String.IsNullOrEmpty(input) Then
                Return DialogResult.Cancel
            Else
                Return DialogResult.OK
            End If
        End If
    End Function

    Private Shared Function OokiiInputBox(ByRef input As String, Optional windowTitle As String = Nothing, Optional header As String = Nothing, Optional content As String = Nothing) As DialogResult
        Dim ooInput As New Ookii.Dialogs.InputDialog With {
            .Input = input,
            .WindowTitle = windowTitle,
            .MainInstruction = header,
            .Content = content
        }

        Dim returnResult = ooInput.ShowDialog(FileBrowser)
        input = ooInput.Input
        Return returnResult
    End Function

    Private Shared Function OokiiDialogsLoaded() As Boolean
        Try
            OokiiDialogsLoadedDelegate()
            Return True
        Catch ex As FileNotFoundException When ex.FileName.StartsWith("Ookii.Dialogs")
            Return False
        Catch ex As Exception
            MsgBox("Unexpected error loading Ookii.Dialogs.dll!" & Environment.NewLine & Environment.NewLine & ex.Message, MsgBoxStyle.Exclamation)
            Return False
        End Try
    End Function
    Private Shared Sub OokiiDialogsLoadedDelegate() ' because calling a not found class will fail the caller of the method not directly in the method
        Dim test = Ookii.Dialogs.TaskDialogIcon.Information
    End Sub
#End Region
End Class
