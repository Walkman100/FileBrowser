Imports System
Imports System.Linq
Imports System.Windows.Forms

Public Class Sorting
    ''' <summary>equivalent to <see cref="FileBrowser.lstCurrent"/> Columns index field</summary>
    Enum SortBy
        Name
        Extension
        LastModified
        LastAccessed
        Created
        Size
        DiskSize
        Attributes
        LinkTarget
        SymlinkTarget
        ShortcutTarget
        UrlTarget
        HardlinkCount
        StreamCount
        OpensWith
        DownloadURL
        DownloadReferrer
    End Enum

    Public Shared Sub Sort(baseControl As Control, hostControl As System.ComponentModel.Component,
                           items As ListView.ListViewItemCollection, sortBy As SortBy, sortOrder As SortOrder)
        Dim itemInfos As IOrderedEnumerable(Of ListViewItem) = items.Cast(Of ListViewItem).OrderBy(Function(x) x.Text)

        If Helpers.AutoInvoke(baseControl, Function() Settings.ShowFoldersFirst) Then
            itemInfos = itemInfos.OrderByDescending(Function(x) FileBrowser.GetItemInfo(x).Attributes.HasFlag(IO.FileAttributes.Directory))

            itemInfos = itemInfos.ThenByAorD(sortOrder, Function(x) keySelector(x, sortBy))
        Else
            itemInfos = itemInfos.OrderByAorD(sortOrder, Function(x) keySelector(x, sortBy))
        End If

        itemInfos = itemInfos.ThenByAorD(sortOrder, Function(x) keySelector(x, SortBy.Name))

        Dim itemArr As ListViewItem() = itemInfos.ToArray()

        Helpers.AutoInvoke(baseControl, Sub()
                                            Helpers.BeginUpdate(hostControl)
                                            items.Clear()
                                            items.AddRange(itemArr)
                                            Helpers.EndUpdate(hostControl)
                                        End Sub)
    End Sub

    Private Shared Function keySelector(item As ListViewItem, sortBy As SortBy) As Object
        Dim info As Filesystem.EntryInfo = FileBrowser.GetItemInfo(item)
        Select Case sortBy
            Case SortBy.Name
                Return info.DisplayName
            Case SortBy.Extension
                Return info.Extension
            Case SortBy.LastModified
                Return info.LastWriteTime
            Case SortBy.LastAccessed
                Return info.LastAccessTime
            Case SortBy.Created
                Return info.CreationTime
            Case SortBy.Size
                Return info.Size
            Case SortBy.DiskSize
                Return info.SizeOnDisk
            Case SortBy.Attributes
                Return info.Attributes
            Case SortBy.LinkTarget
                Return info.AllTarget
            Case SortBy.SymlinkTarget
                Return info.SymlinkTarget
            Case SortBy.ShortcutTarget
                Return info.LinkTarget
            Case SortBy.UrlTarget
                Return info.UrlTarget
            Case SortBy.HardlinkCount
                Return info.HardlinkCount
            Case SortBy.StreamCount
                Return info.ADSCount
            Case SortBy.OpensWith
                Return info.OpensWith
            Case SortBy.DownloadURL
                Return info.DownloadURL
            Case SortBy.DownloadReferrer
                Return info.DownloadReferrer
            Case Else
                Throw New NotSupportedException("Invalid SortBy: " & sortBy.ToString())
        End Select
    End Function
End Class
