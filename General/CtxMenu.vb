Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Public Class CtxMenu
    Public Enum EntryType
        MenuItem
        Separator
    End Enum
    Public Enum ActionType
        None = -1
        ''' <summary><see cref="EntryInfo.ActionArgs1"/> is the file to launch, <see cref="EntryInfo.ActionArgs2"/> is the arguments</summary>
        Launch
        ''' <summary><see cref="EntryInfo.ActionArgs1"/> is the file to run as admin, <see cref="EntryInfo.ActionArgs2"/> is the arguments</summary>
        Admin
        ''' <summary><see cref="EntryInfo.ActionArgs1"/> is the file to execute, <see cref="EntryInfo.ActionArgs2"/> is the arguments</summary>
        Execute
        ''' <summary><see cref="EntryInfo.ActionArgs1"/> is the file to show</summary>
        Show
        ''' <summary><see cref="EntryInfo.ActionArgs1"/> is the file to show properties for, <see cref="EntryInfo.ActionArgs2"/> is the tab to show, or Nothing</summary>
        Properties
        ''' <summary><see cref="EntryInfo.ActionArgs1"/> is the file to OpenWith</summary>
        OpenWith
        ''' <summary><see cref="EntryInfo.ActionArgs1"/> is the text to copy</summary>
        CopyText

        ''' <summary>ActionArgs are not used.</summary>
        Cut
        ''' <summary>ActionArgs are not used.</summary>
        CutAdd
        ''' <summary>ActionArgs are not used.</summary>
        Copy
        ''' <summary>ActionArgs are not used.</summary>
        CopyAdd
        ''' <summary>ActionArgs are not used.</summary>
        Paste

        ''' <summary>ActionArgs are not used.</summary>
        PasteAsHardlink
        ''' <summary>ActionArgs are not used.</summary>
        PasteAsSymlink
        ''' <summary>ActionArgs are not used.</summary>
        PasteAsShortcut
        ''' <summary>ActionArgs are not used.</summary>
        PasteAsJunction

        ''' <summary>ActionArgs are not used.</summary>
        Rename
        ''' <summary>ActionArgs are not used.</summary>
        CopyTo
        ''' <summary>ActionArgs are not used.</summary>
        MoveTo
        ''' <summary>ActionArgs are not used.</summary>
        DeleteToRecycleBin
        ''' <summary>ActionArgs are not used.</summary>
        DeletePermanently
    End Enum

    Structure EntryInfo
        Public EntryType As EntryType
        Public IconPath As String
        Public Text As String
        Public AdminIcon As Boolean

        'when to show
        Public Extended As Boolean
        Public DirectoryOnly As Boolean
        Public DriveOnly As Boolean
        Public FileOnly As Boolean
        ''' <summary>
        ''' Item filters separated by ";".
        ''' See <a href="https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator#pattern-options">Like-Operator</a> for filter info
        ''' </summary>
        Public Filter As String

        Public ActionType As ActionType
        Public ActionArgs1 As String
        Public ActionArgs2 As String

        Public IsSubItem As Boolean
        Public SubItems As List(Of EntryInfo)
    End Structure

    Private entryDict As New Dictionary(Of Integer, EntryInfo)

    Private Sub AddItem(collection As ToolStripItemCollection, itemInfo As EntryInfo, ByRef index As Integer)
        entryDict.Add(index, itemInfo)
        If itemInfo.EntryType = EntryType.Separator Then
            collection.Add(New ToolStripSeparator() With {.Tag = index})
        Else
            Dim item As New ToolStripMenuItem(itemInfo.Text) With {.Tag = index}

            Dim iconPath As String = itemInfo.IconPath

            If ImageHandling.PathIsRESXItem(iconPath) Then
                item.Image = ImageHandling.GetRESXResource(iconPath)
            Else
                Try
                    item.Image = ImageHandling.GetIcon(iconPath)?.ToBitmap()
                Catch
                    iconPath = ImageHandling.ExpandImagePath(iconPath)
                    Try : item.Image = Image.FromFile(iconPath)
                    Catch : item.Image = New PictureBox().ErrorImage
                    End Try
                End Try
            End If

            If itemInfo.AdminIcon Then item.Image = ImageHandling.AddAdminOverlay(item.Image)

            collection.Add(item)
        End If
        index += 1
    End Sub

    Public Sub BuildMenu(contextMenu As ContextMenuStrip, items As List(Of EntryInfo))
        entryDict.Clear()
        contextMenu.Items.Clear()

        Dim currentIndex As Integer = 0
        For Each itemInfo As EntryInfo In items
            AddItem(contextMenu.Items, itemInfo, currentIndex)
        Next

        For i = 0 To items.Count - 1
            Dim itemInfo As EntryInfo = items(i)

            If itemInfo.SubItems IsNot Nothing AndAlso TypeOf contextMenu.Items(i) Is ToolStripMenuItem Then
                Dim item As ToolStripMenuItem = DirectCast(contextMenu.Items(i), ToolStripMenuItem)
                ' without this, FileBrowser.ctxMenuL_ItemClicked only handles root items
                AddHandler item.DropDownItemClicked, AddressOf FileBrowser.ctxMenuL_ItemClicked

                For Each subItem As EntryInfo In itemInfo.SubItems
                    AddItem(item.DropDownItems, subItem, currentIndex)
                Next
            End If
        Next
        WalkmanLib.ApplyTheme(Settings.Theme, contextMenu.Items)
    End Sub

    Private Function FilterName(name As String, filter As String) As Boolean
        Dim filterArr As String() = filter.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)

        Return filterArr.Any(Function(f As String) name Like f)
    End Function

    Private Sub UpdateMenu(collection As ToolStripItemCollection, paths As String())
        If collection.Count = 0 Then Return

        ' build list of PathEnum (exists info) before looping ToolStripItems
        Dim pathInfos As New List(Of KeyValuePair(Of String, PathEnum))
        For Each path As String In paths
            Dim existsInfo As PathEnum = WalkmanLib.IsFileOrDirectory(path)

            If existsInfo = PathEnum.NotFound AndAlso Helpers.PathContainsADS(path) AndAlso
                    Trinet.Core.IO.Ntfs.AlternateDataStreamExists(Helpers.GetADSPathFile(path), Helpers.GetADSPathStream(path)) Then
                ' we're dealing with an ADS
                existsInfo = PathEnum.Exists Or PathEnum.IsFile
            End If

            pathInfos.Add(New KeyValuePair(Of String, PathEnum)(path, existsInfo))
        Next

        Dim enableClipboardItems As Boolean = False
        If FileBrowser.itemClipboard.ItemStore.Count > 0 Then enableClipboardItems = True
        If Clipboard.ContainsFileDropList Then enableClipboardItems = True

        For Each item As ToolStripItem In collection
            Dim index As Integer = DirectCast(item.Tag, Integer)
            If Not entryDict.ContainsKey(index) Then Throw New InvalidOperationException("Context menu contains item not in dictionary!")
            Dim itemInfo As EntryInfo = entryDict.Item(index)

            If TypeOf item Is ToolStripSeparator Then
                If itemInfo.Extended Then
                    item.Visible = My.Computer.Keyboard.ShiftKeyDown
                End If
                Continue For
            End If

            Dim itemShouldBeVisible As Boolean = paths.Length > 0

            If itemInfo.ActionType = ActionType.Paste OrElse itemInfo.ActionType = ActionType.PasteAsHardlink OrElse itemInfo.ActionType = ActionType.PasteAsJunction OrElse
                    itemInfo.ActionType = ActionType.PasteAsShortcut OrElse itemInfo.ActionType = ActionType.PasteAsSymlink Then
                item.Enabled = enableClipboardItems
            End If

            For Each info As KeyValuePair(Of String, PathEnum) In pathInfos
                If itemShouldBeVisible Then
                    If itemInfo.DirectoryOnly Then
                        itemShouldBeVisible = info.Value.HasFlag(PathEnum.IsDirectory)
                    ElseIf itemInfo.DriveOnly Then
                        itemShouldBeVisible = info.Value.HasFlag(PathEnum.IsDrive)
                    ElseIf itemInfo.FileOnly Then
                        itemShouldBeVisible = info.Value.HasFlag(PathEnum.IsFile)
                    End If
                End If

                If itemShouldBeVisible AndAlso itemInfo.Extended Then
                    itemShouldBeVisible = My.Computer.Keyboard.ShiftKeyDown
                End If
                If itemShouldBeVisible AndAlso info.Value.HasFlag(PathEnum.IsFile) AndAlso Not String.IsNullOrEmpty(itemInfo.Filter) Then
                    If Helpers.PathContainsADS(info.Key) Then
                        itemShouldBeVisible = FilterName(New FileInfo(Helpers.GetADSPathFile(info.Key)).Name, itemInfo.Filter)
                    Else
                        itemShouldBeVisible = FilterName(New FileInfo(info.Key).Name, itemInfo.Filter)
                    End If
                End If
            Next
            item.Visible = itemShouldBeVisible

            ' if item is ToolStripSeparator it is skipped, so this shouldn't be a problem
            Dim itemAsMenu As ToolStripMenuItem = DirectCast(item, ToolStripMenuItem)
            If itemAsMenu.HasDropDownItems Then
                UpdateMenu(itemAsMenu.DropDownItems, paths)
            End If
        Next
    End Sub
    Public Sub UpdateMenu(contextMenu As ContextMenuStrip, paths As String())
        UpdateMenu(contextMenu.Items, paths)
    End Sub

    Public Sub RunItem(item As ToolStripItem, paths As String())
        If TypeOf item Is ToolStripSeparator Then Return
        Dim index As Integer = DirectCast(item.Tag, Integer)
        If Not entryDict.ContainsKey(index) Then Throw New InvalidOperationException("Selected item not in dictionary!")
        Dim itemInfo As EntryInfo = entryDict.Item(index)

        If paths.Length < 1 Then Throw New InvalidOperationException("No paths selected!")

        Select Case itemInfo.ActionType
            Case ActionType.Launch
                For Each path As String In paths
                    Launch.LaunchItem(path, itemInfo.ActionArgs1, itemInfo.ActionArgs2)
                Next
            Case ActionType.Admin
                For Each path As String In paths
                    Launch.RunAsAdmin(path, itemInfo.ActionArgs1, itemInfo.ActionArgs2)
                Next
            Case ActionType.Execute
                For Each path As String In paths
                    Launch.ExecuteItem(path, itemInfo.ActionArgs1, itemInfo.ActionArgs2)
                Next

            Case ActionType.Show
                For Each path As String In paths
                    Launch.ShowPath(path, itemInfo.ActionArgs1)
                Next
            Case ActionType.Properties
                For Each path As String In paths
                    Launch.WinProperties(path, itemInfo.ActionArgs1, itemInfo.ActionArgs2)
                Next
            Case ActionType.OpenWith
                For Each path As String In paths
                    Launch.OpenWith(path, itemInfo.ActionArgs1)
                Next
            Case ActionType.CopyText
                Launch.Copy(paths, itemInfo.ActionArgs1)

            Case ActionType.Cut
                FileBrowser.itemClipboard.AddItems(paths, ItemClipboard.ItemType.Cut, replace:=Not My.Computer.Keyboard.ShiftKeyDown, addSystem:=Settings.CopySystem)
            Case ActionType.CutAdd
                FileBrowser.itemClipboard.AddItems(paths, ItemClipboard.ItemType.Cut, replace:=My.Computer.Keyboard.ShiftKeyDown, addSystem:=Settings.CopySystem)
            Case ActionType.Copy
                FileBrowser.itemClipboard.AddItems(paths, ItemClipboard.ItemType.Copy, replace:=Not My.Computer.Keyboard.ShiftKeyDown, addSystem:=Settings.CopySystem)
            Case ActionType.CopyAdd
                FileBrowser.itemClipboard.AddItems(paths, ItemClipboard.ItemType.Copy, replace:=My.Computer.Keyboard.ShiftKeyDown, addSystem:=Settings.CopySystem)
            Case ActionType.Paste
                FileBrowser.itemClipboard.PasteItems(paths(0), ItemClipboard.PasteType.Normal, useSystem:=My.Computer.Keyboard.ShiftKeyDown)
            Case ActionType.PasteAsHardlink
                FileBrowser.itemClipboard.PasteItems(paths(0), ItemClipboard.PasteType.Hardlink, useSystem:=My.Computer.Keyboard.ShiftKeyDown)
            Case ActionType.PasteAsSymlink
                FileBrowser.itemClipboard.PasteItems(paths(0), ItemClipboard.PasteType.Symlink, useSystem:=My.Computer.Keyboard.ShiftKeyDown)
            Case ActionType.PasteAsShortcut
                FileBrowser.itemClipboard.PasteItems(paths(0), ItemClipboard.PasteType.Shortcut, useSystem:=My.Computer.Keyboard.ShiftKeyDown)
            Case ActionType.PasteAsJunction
                FileBrowser.itemClipboard.PasteItems(paths(0), ItemClipboard.PasteType.Junction, useSystem:=My.Computer.Keyboard.ShiftKeyDown)

            Case ActionType.Rename
                FileBrowser.RenameSelected()
            Case ActionType.CopyTo
                Operations.CopyTo(paths, textInput:=My.Computer.Keyboard.ShiftKeyDown)
            Case ActionType.MoveTo
                Operations.MoveTo(paths, textInput:=My.Computer.Keyboard.ShiftKeyDown)
            Case ActionType.DeleteToRecycleBin
                Operations.Delete(paths, useShell:=FileBrowser.UseShell, deletePermanently:=My.Computer.Keyboard.ShiftKeyDown)
            Case ActionType.DeletePermanently
                Operations.Delete(paths, useShell:=FileBrowser.UseShell, deletePermanently:=Not My.Computer.Keyboard.ShiftKeyDown)
        End Select
    End Sub
End Class
