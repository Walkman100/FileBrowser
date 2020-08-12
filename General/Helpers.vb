Imports System.IO
Imports System.Windows.Forms

Public Class Helpers
    Private Shared sfd As New SaveFileDialog 'With {.CheckFileExists = False}
    Private Shared sdd As New Ookii.Dialogs.VistaFolderBrowserDialog With {.UseDescriptionForTitle = True}

    Public Shared Sub CopyTo(paths As String(), manualEdit As Boolean)
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
            Operations.Copy(path, target, True) 'FileBrowser.chkUseShell.Checked)
        Next
    End Sub

    Public Shared Sub MoveTo(paths As String(), manualEdit As Boolean)
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
            Operations.Move(path, target, True) 'FileBrowser.chkUseShell.Checked)
        Next
    End Sub
End Class
