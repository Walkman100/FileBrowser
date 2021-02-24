Imports System
Imports System.Collections.Generic
Imports System.IO
Imports Trinet.Core.IO.Ntfs

Public Class Filesystem
    <Flags>
    Public Enum EntryType
        None = 0
        File = 1
        Directory = 2
        Hardlink = 4
        AlternateDataStream = 8
        ' Symlink, Compressed, Hidden e.t.c. are Attributes
    End Enum

    Public Structure EntryInfo
        Public FullName As String
        Public DisplayName As String
        Public Extension As String
        Public LastWriteTime As Date
        Public LastAccessTime As Date
        Public CreationTime As Date
        Public Size As Long
        Public SizeOnDisk As Double
        Public Type As EntryType
        Public Attributes As FileAttributes
        Public SymlinkTarget As String
        Public LinkTarget As String
        Public UrlTarget As String
        Public AllTarget As String
        Public HardlinkCount As UInteger
        Public ADSCount As Integer
        Public OpensWith As String
        Public DownloadURL As String
        Public DownloadReferrer As String
    End Structure

    Public Shared Function GetItemEntryInfo(info As FileInfo, showExtensions As Boolean) As EntryInfo
        Dim entryInfo As New EntryInfo With {
            .FullName = info.FullName,
            .DisplayName = info.Name,
            .Extension = info.Extension,
            .LastWriteTime = info.LastWriteTime,
            .LastAccessTime = info.LastAccessTime,
            .CreationTime = info.CreationTime,
            .Attributes = info.Attributes
        }

        If entryInfo.Attributes.HasFlag(FileAttributes.ReparsePoint) Then
            Try : entryInfo.SymlinkTarget = WalkmanLib.GetSymlinkTarget(info.FullName)
            Catch : End Try
        End If
        If entryInfo.Extension.ToLowerInvariant() = ".lnk" Then entryInfo.LinkTarget = WalkmanLib.GetShortcutInfo(info.FullName).TargetPath
        If entryInfo.Extension.ToLowerInvariant() = ".url" Then entryInfo.UrlTarget = Helpers.GetUrlTarget(info.FullName)
        entryInfo.AllTarget = If(entryInfo.SymlinkTarget, If(entryInfo.LinkTarget, entryInfo.UrlTarget))

        Try : entryInfo.ADSCount = info.ListAlternateDataStreams.Count
        Catch : End Try

        entryInfo.OpensWith = WalkmanLib.GetOpenWith(info.FullName)
        If entryInfo.OpensWith = "Filetype not associated!" Then entryInfo.OpensWith = Nothing
        entryInfo.DownloadURL = Helpers.GetDownloadURL(info.FullName)
        entryInfo.DownloadReferrer = Helpers.GetDownloadReferrer(info.FullName)

        If File.Exists(info.FullName) Then
            entryInfo.Type = EntryType.File

            If Not showExtensions Then
                entryInfo.DisplayName = info.NameNoExt()
            End If

            entryInfo.Size = info.Length
            entryInfo.SizeOnDisk = WalkmanLib.GetCompressedSize(info.FullName)
            Try
                entryInfo.HardlinkCount = WalkmanLib.GetHardlinkCount(info.FullName)
                If entryInfo.HardlinkCount > 1 Then
                    entryInfo.Type = entryInfo.Type Or EntryType.Hardlink
                End If
            Catch : End Try

        ElseIf Directory.Exists(info.FullName) Then
            entryInfo.Type = EntryType.Directory
        End If

        Return entryInfo
    End Function

    Private Shared Iterator Function Filter(baseControl As Windows.Forms.Control, paths As IEnumerable(Of String)) As IEnumerable(Of EntryInfo)
        Dim _showHidden As Boolean = Helpers.AutoInvoke(baseControl, Function() Settings.ShowHidden)
        Dim _showSystem As Boolean = Helpers.AutoInvoke(baseControl, Function() Settings.ShowSystem)
        Dim _showDot As Boolean = Helpers.AutoInvoke(baseControl, Function() Settings.ShowDot)
        Dim _showExtensions As Boolean = Helpers.AutoInvoke(baseControl, Function() Settings.ShowExtensions)

        For Each path As String In paths
            Dim errored As Boolean = False
            Try
                Dim info As New FileInfo(path)
                If (_showHidden OrElse Not info.Attributes.HasFlag(FileAttributes.Hidden)) AndAlso
                   (_showSystem OrElse Not info.Attributes.HasFlag(FileAttributes.System)) AndAlso
                   (_showDot OrElse Not info.Name.Chars(0) = "."c) Then

                    Dim entryInfo As EntryInfo = GetItemEntryInfo(info, _showExtensions)
                    Yield entryInfo

                    If Settings.ShowADSSeparate Then
                        If entryInfo.ADSCount > 0 Then
                            For Each adsInfo As AlternateDataStreamInfo In info.ListAlternateDataStreams()
                                entryInfo = New EntryInfo With {
                                    .FullName = adsInfo.FullPath.Remove(adsInfo.FullPath.LastIndexOf(":"c)),
                                    .Size = adsInfo.Size,
                                    .Type = EntryType.AlternateDataStream,
                                    .DisplayName = IO.Path.GetFileName(adsInfo.FilePath) & ":" & adsInfo.Name
                                }

                                Yield entryInfo
                            Next
                        End If
                    End If
                End If

            Catch ex As NotSupportedException ' invalid path created on linux
                errored = True
            End Try

            If errored Then ' can't put yield in a Catch or Finally...
                Yield New EntryInfo With {
                    .FullName = path,
                    .DisplayName = path.Substring(path.LastIndexOf(IO.Path.DirectorySeparatorChar) + 1) & " (Error)"
                }
            End If
        Next
    End Function

    Public Shared Function GetFolders(baseControl As Windows.Forms.Control, path As String) As IEnumerable(Of EntryInfo)
        If Directory.Exists(path) Then
            Return Filter(baseControl, Directory.EnumerateDirectories(path))
        Else
            Throw New DirectoryNotFoundException($"Directory ""{path}"" not found!")
        End If
    End Function

    Public Shared Function GetItems(baseControl As Windows.Forms.Control, path As String) As IEnumerable(Of EntryInfo)
        If Directory.Exists(path) Then
            Return Filter(baseControl, Directory.EnumerateFileSystemEntries(path))
        Else
            Throw New DirectoryNotFoundException($"Directory ""{path}"" not found!")
        End If
    End Function
End Class
