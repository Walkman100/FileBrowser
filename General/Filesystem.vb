Imports System
Imports System.Collections.Generic
Imports System.IO
Imports Trinet.Core.IO.Ntfs

Public Class Filesystem
    <Flags>
    Public Enum EntryType
        None
        File
        Directory
        Hardlink
        AlternateDataStream
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

    Private Shared Iterator Function Filter(paths As IEnumerable(Of String)) As IEnumerable(Of EntryInfo)
        For Each path As String In paths
            Dim info As New FileInfo(path)
            If (Settings.ShowHidden OrElse Not info.Attributes.HasFlag(FileAttributes.Hidden)) AndAlso
               (Settings.ShowSystem OrElse Not info.Attributes.HasFlag(FileAttributes.System)) AndAlso
               (Settings.ShowDot OrElse Not info.Name.Chars(0) = "."c) Then
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
                    Try : entryInfo.SymlinkTarget = WalkmanLib.GetSymlinkTarget(path)
                    Catch : End Try
                End If
                If entryInfo.Extension.ToLowerInvariant() = ".lnk" Then entryInfo.LinkTarget = WalkmanLib.GetShortcutInfo(path).TargetPath
                If entryInfo.Extension.ToLowerInvariant() = ".url" Then entryInfo.UrlTarget = Helpers.GetUrlTarget(path)
                entryInfo.AllTarget = If(entryInfo.SymlinkTarget, If(entryInfo.LinkTarget, entryInfo.UrlTarget))

                Try : entryInfo.ADSCount = info.ListAlternateDataStreams.Count
                Catch : End Try

                entryInfo.OpensWith = WalkmanLib.GetOpenWith(path)
                If entryInfo.OpensWith = "Filetype not associated!" Then entryInfo.OpensWith = Nothing
                entryInfo.DownloadURL = Helpers.GetDownloadURL(path)
                entryInfo.DownloadReferrer = Helpers.GetDownloadReferrer(path)

                If File.Exists(path) Then
                    entryInfo.Type = EntryType.File

                    If Not Settings.ShowExtensions Then
                        entryInfo.DisplayName = info.NameNoExt()
                    End If

                    entryInfo.Size = info.Length
                    entryInfo.SizeOnDisk = WalkmanLib.GetCompressedSize(path)
                    Try
                        entryInfo.HardlinkCount = WalkmanLib.GetHardlinkCount(path)
                        If entryInfo.HardlinkCount > 1 Then
                            entryInfo.Type = entryInfo.Type Or EntryType.Hardlink
                        End If
                    Catch : End Try

                ElseIf Directory.Exists(path) Then
                    entryInfo.Type = EntryType.Directory
                End If

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
        Next
    End Function

    Public Shared Function GetFolders(path As String) As IEnumerable(Of EntryInfo)
        If Directory.Exists(path) Then
            Return Filter(Directory.EnumerateDirectories(path))
        Else
            Throw New DirectoryNotFoundException($"Directory ""{path}"" not found!")
        End If
    End Function

    Public Shared Function GetItems(path As String) As IEnumerable(Of EntryInfo)
        If Directory.Exists(path) Then
            Return Filter(Directory.EnumerateFileSystemEntries(path))
        Else
            Throw New DirectoryNotFoundException($"Directory ""{path}"" not found!")
        End If
    End Function
End Class
