Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Public Enum ItemType
    Cut
    Copy
End Enum

Public Enum PasteType
    Normal
    Hardlink
    Symlink
    Shortcut
    Junction
End Enum

Public Class ItemClipboard
    Public ReadOnly Property ItemStore As New Dictionary(Of String, ItemType)

    Private Sub ItemsUpdated()
        FileBrowser.clipboardList.Items = ItemStore.Select(Function(kv As KeyValuePair(Of String, ItemType)) As ListViewItem
                                                               Return New ListViewItem({kv.Key, kv.Value.ToString()})
                                                           End Function).ToList()
    End Sub

    Public Sub AddItems(paths() As String, type As ItemType, replace As Boolean)
        If replace Then ItemStore.Clear()

        For Each path As String In paths
            ItemStore.Add(path, type)
        Next

        ItemsUpdated()
        FileBrowser.handle_SelectedItemChanged()
    End Sub

    Public Sub PasteItems(target As String, type As PasteType)
        For Each item As KeyValuePair(Of String, ItemType) In ItemStore.ToList ' so that we can modify the original
            Dim target2 As String
            If WalkmanLib.IsFileOrDirectory(target).HasFlag(PathEnum.IsDirectory) Then
                target2 = Path.Combine(target, Path.GetFileName(item.Key))
            Else
                target2 = target
            End If

            Select Case type
                Case PasteType.Normal
                    If item.Value = ItemType.Copy Then
                        Operations.Copy(item.Key, target2, FileBrowser.UseShell)
                    Else
                        Operations.Move(item.Key, target2, FileBrowser.UseShell)
                    End If
                Case PasteType.Hardlink
                    Operations.CreateHardlink(item.Key, target2)
                Case PasteType.Symlink
                    ' allow overwriting directory symlinks if pasting as a symlink and source is a folder
                    If Directory.Exists(target) AndAlso File.GetAttributes(target).HasFlag(FileAttributes.ReparsePoint) AndAlso Directory.Exists(item.Key) Then
                        target2 = target
                    End If
                    Operations.CreateSymlink(item.Key, target2)
                Case PasteType.Shortcut
                    Operations.CreateShortcut(item.Key, target2 & ".lnk")
                Case PasteType.Junction
                    ' allow overwriting directory junctions if pasting as a junction and source is a folder
                    If Directory.Exists(target) AndAlso File.GetAttributes(target).HasFlag(FileAttributes.ReparsePoint) AndAlso Directory.Exists(item.Key) Then
                        target2 = target
                    End If
                    'Operations.CreateJunction(item.Key, target2)
            End Select

            If item.Value = ItemType.Cut Then
                ItemStore.Remove(item.Key)
            End If
        Next

        ItemsUpdated()
        FileBrowser.handle_SelectedItemChanged()
    End Sub
End Class
