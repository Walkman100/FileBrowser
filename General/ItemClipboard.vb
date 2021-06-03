Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Public Class ItemClipboard
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

    Public ReadOnly Property ItemStore As New Dictionary(Of String, ItemType)

    Private Sub ItemsUpdated()
        FileBrowser.clipboardList.Items = ItemStore.Select(Function(kv As KeyValuePair(Of String, ItemType)) As ListViewItem
                                                               Return New ListViewItem({kv.Key, kv.Value.ToString()})
                                                           End Function).ToList()
    End Sub

    Private Const DropEffectFormat As String = "Preferred DropEffect"
    Private Function GetSystemClipboardFiles(Optional ByRef type As ItemType = Nothing) As String()
        type = ItemType.Copy
        If Clipboard.ContainsData(DropEffectFormat) Then
            Dim ms As MemoryStream = DirectCast(Clipboard.GetData(DropEffectFormat), MemoryStream)
            If ms?.ReadByte() = DragDropEffects.Move Then
                type = ItemType.Cut
            End If
        End If

        Return Clipboard.GetFileDropList().Cast(Of String).ToArray()
    End Function
    Private Sub SetSystemClipboardFiles(items As String(), type As ItemType)
        Dim dataObject As New DataObject
        dataObject.SetData(DataFormats.FileDrop, True, items)

        If type = ItemType.Cut Then ' copy is default - thanks to https://stackoverflow.com/q/2077981/2999220
            dataObject.SetData(DropEffectFormat, New MemoryStream(System.BitConverter.GetBytes(DragDropEffects.Move)))
        End If

        Clipboard.SetDataObject(dataObject, True)
    End Sub
    Private Sub AddSystemClipboardFiles(items As String(), type As ItemType)
        Dim bothItems As String() = GetSystemClipboardFiles().Union(items).ToArray()

        SetSystemClipboardFiles(bothItems, type)
    End Sub

    Public Sub AddItems(paths As String(), type As ItemType, replace As Boolean)
        If replace Then ItemStore.Clear()

        For Each path As String In paths
            If Not ItemStore.ContainsKey(path) Then
                ItemStore.Add(path, type)
            Else
                ' if path already exists, update type
                ItemStore(path) = type
            End If
        Next

        If Settings.CopySystem Then
            If replace Then
                Clipboard.Clear()
                SetSystemClipboardFiles(paths, type)
            Else
                AddSystemClipboardFiles(paths, type)
            End If
        End If

        ItemsUpdated()
        FileBrowser.handle_SelectedItemChanged()
    End Sub

    Public Sub PasteItems(target As String, type As PasteType)
        For Each item As KeyValuePair(Of String, ItemType) In ItemStore.ToList() ' so that we can modify the original
            Dim target2 As String = target
            If WalkmanLib.IsFileOrDirectory(target).HasFlag(PathEnum.IsDirectory) Then
                target2 = Path.Combine(target, Helpers.GetFileName(item.Key))
            End If

            If type = PasteType.Shortcut AndAlso Not target2.ToLower().EndsWith(".lnk") Then target2 &= ".lnk"
            Dim targetExists As Boolean = False
            If Helpers.PathContainsADS(target2) AndAlso
                    Trinet.Core.IO.Ntfs.AlternateDataStreamExists(Helpers.GetADSPathFile(target2), Helpers.GetADSPathStream(target2)) Then
                targetExists = True
            ElseIf WalkmanLib.IsFileOrDirectory(target2).HasFlag(PathEnum.Exists) Then
                targetExists = True
            End If
            If targetExists Then
                Dim newName As String = Helpers.GetFileName(target2)
                If Input.GetInput(newName, "Target Exists!", "Enter name to paste as:") = DialogResult.OK Then
                    target2 = Path.Combine(Path.GetDirectoryName(target2), newName)
                Else
                    Exit Sub
                End If
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
                    Operations.CreateShortcut(item.Key, target2)
                Case PasteType.Junction
                    ' allow overwriting directory junctions if pasting as a junction and source is a folder
                    If Directory.Exists(target) AndAlso File.GetAttributes(target).HasFlag(FileAttributes.ReparsePoint) AndAlso Directory.Exists(item.Key) Then
                        target2 = target
                    End If
                    Operations.CreateJunction(item.Key, target2)
            End Select

            If item.Value = ItemType.Cut Then
                ItemStore.Remove(item.Key)
            End If
        Next

        ItemsUpdated()
        FileBrowser.handle_SelectedItemChanged()
    End Sub
End Class
