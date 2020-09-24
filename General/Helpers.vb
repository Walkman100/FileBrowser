Imports System
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Trinet.Core.IO.Ntfs

Enum OS
    Windows
    Other
End Enum

Public Class Helpers
    Friend Shared Function GetOS() As OS
        Return If(Environment.GetEnvironmentVariable("OS") = "Windows_NT", OS.Windows, OS.Other)
    End Function

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
            Operations.Copy(path, target, FileBrowser.UseShell)
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
            Operations.Move(path, target, FileBrowser.UseShell)
        Next
    End Sub

    Public Shared Function CreateFile(rootPath As String, useInputBox As Boolean) As String
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
        File.CreateText(target).Close()
        Return target
    End Function

    Public Shared Function CreateFolder(rootPath As String, useInputBox As Boolean) As String
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


    Public Shared Function GetUrlTarget(path As String) As String
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

    Public Shared Function GetDownloadURL(path As String) As String
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

    Public Shared Function GetDownloadReferrer(path As String) As String
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


    Public Shared Function GetADSFile(adsInfo As AlternateDataStreamInfo) As String
        Return Path.GetFileName(adsInfo.FilePath)
    End Function

    Public Shared Function GetADSDirectory(adsInfo As AlternateDataStreamInfo) As String
        Return Path.GetDirectoryName(adsInfo.FilePath)
    End Function

    Public Shared Function GetADSPath(adsInfo As AlternateDataStreamInfo) As String
        Return adsInfo.FilePath & ":" & adsInfo.Name
    End Function

    Public Shared Function PathContainsADS(path As String) As Boolean
        Return path.Count(Function(c As Char) c = ":"c) > 1
    End Function

    Public Shared Function GetADSPathStream(adsPath As String) As String
        Return adsPath.Substring(adsPath.IndexOf(":"c, 2) + 1) ' start at 2 to ignore DriveSeparator
    End Function

    Public Shared Function GetADSPathFile(adsPath As String) As String
        Return adsPath.Remove(adsPath.IndexOf(":"c, 2)) ' start at 2 to ignore DriveSeparator
    End Function
End Class
