Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Public Class CtxMenu
    Public Enum ActionType
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be an array with two strings</summary>
        Launch
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be an array with two strings</summary>
        Admin
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be an array with two strings</summary>
        Execute
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        Show
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be an array with two strings</summary>
        Properties
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        OpenWith

        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        Cut
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        Copy
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        Paste

        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        PasteAsHardlink
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        PasteAsSymlink
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        PasteAsShortcut
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        PasteAsJunction

        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be Nothing</summary>
        CopyTo
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be Nothing</summary>
        MoveTo
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be Nothing</summary>
        DeleteToRecycleBin
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be Nothing</summary>
        DeletePermanently
    End Enum

    Structure EntryInfo
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
        ''' See https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator#pattern-options for filter info
        ''' </summary>
        Public Filter As String

        Public ActionType As ActionType
        Public ActionArgs As Object
    End Structure

    Private entryDict As New Dictionary(Of Integer, EntryInfo)

    Public Sub BuildMenu(contextMenu As ContextMenuStrip, items As List(Of EntryInfo))
        contextMenu.Items.Clear()

        Dim currentIndex As Integer = 0
        For Each item As EntryInfo In items
            entryDict.Add(currentIndex, item)
            Dim menuItem As New ToolStripMenuItem(item.Text) With {.Tag = currentIndex}

            Dim icon As Icon = ImageHandling.GetIcon(item.IconPath)
            If icon IsNot Nothing Then
                menuItem.Image = icon.ToBitmap()
            End If
            contextMenu.Items.Add(menuItem)

            currentIndex += 1
        Next
    End Sub

    Private Function FilterName(name As String, filter As String) As Boolean
        Dim filterArr As String() = filter.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)

        Return filterArr.Any(Function(f As String) name Like f)
    End Function

    Public Sub UpdateMenu(contextMenu As ContextMenuStrip, paths As String())
        For Each item As ToolStripItem In contextMenu.Items
            Dim index As Integer = DirectCast(item.Tag, Integer)
            If Not entryDict.ContainsKey(index) Then Throw New InvalidOperationException("Context menu contains item not in dictionary!")
            Dim itemInfo As EntryInfo = entryDict.Item(index)
            Dim itemShouldBeVisible As Boolean = True

            For Each path As String In paths
                Dim existsInfo As PathEnum = WalkmanLib.IsFileOrDirectory(path)
                If itemInfo.DirectoryOnly Then
                    itemShouldBeVisible = existsInfo.HasFlag(PathEnum.IsDirectory)
                ElseIf itemInfo.DriveOnly Then
                    itemShouldBeVisible = existsInfo.HasFlag(PathEnum.IsDrive)
                ElseIf itemInfo.FileOnly Then
                    itemShouldBeVisible = existsInfo.HasFlag(PathEnum.IsFile)
                End If

                If itemShouldBeVisible AndAlso itemInfo.Extended Then
                    itemShouldBeVisible = My.Computer.Keyboard.ShiftKeyDown
                End If
                If itemShouldBeVisible AndAlso existsInfo.HasFlag(PathEnum.IsFile) AndAlso itemInfo.Filter IsNot Nothing Then
                    itemShouldBeVisible = FilterName(New FileInfo(path).Name, itemInfo.Filter)
                End If
            Next
            item.Visible = itemShouldBeVisible
        Next
    End Sub

    Public Sub RunItem(item As ToolStripItem, paths As String())
        Dim index As Integer = DirectCast(item.Tag, Integer)
        If Not entryDict.ContainsKey(index) Then Throw New InvalidOperationException("Selected item not in dictionary!")
        Dim itemInfo As EntryInfo = entryDict.Item(index)

        Select Case itemInfo.ActionType
            Case ActionType.Launch
                Dim args As String() = CType(itemInfo.ActionArgs, String())
                For Each path As String In paths
                    Launch.LaunchItem(path, args(0), args(1))
                Next
            Case ActionType.Admin
                Dim args As String() = CType(itemInfo.ActionArgs, String())
                For Each path As String In paths
                    Launch.RunAsAdmin(path, args(0), args(1))
                Next
            Case ActionType.Execute
                Dim args As String() = CType(itemInfo.ActionArgs, String())
                For Each path As String In paths
                    Launch.ExecuteItem(path, args(0), args(1))
                Next

            Case ActionType.Show
                Dim args As String = CType(itemInfo.ActionArgs, String)
                For Each path As String In paths
                    Launch.ShowPath(path, args)
                Next
            Case ActionType.Properties
                Dim args As String() = CType(itemInfo.ActionArgs, String())
                For Each path As String In paths
                    Launch.WinProperties(path, args(0), args(1))
                Next
            Case ActionType.OpenWith
                Dim args As String = CType(itemInfo.ActionArgs, String)
                For Each path As String In paths
                    Launch.ShowPath(path, args)
                Next

            Case ActionType.Cut
                Dim args As String = CType(itemInfo.ActionArgs, String)
                'FileBrowser.itemClipboard.ReplaceItems(paths, ItemType.Cut)
            Case ActionType.Copy
                Dim args As String = CType(itemInfo.ActionArgs, String)
                'FileBrowser.itemClipboard.ReplaceItems(paths, ItemType.Copy)
            Case ActionType.Paste
                Dim args As String = CType(itemInfo.ActionArgs, String)
                'FileBrowser.itemClipboard.PasteItems(paths(0), PasteType.Normal)
            Case ActionType.PasteAsHardlink
                Dim args As String = CType(itemInfo.ActionArgs, String)
                'FileBrowser.itemClipboard.PasteItems(paths(0), PasteType.Hardlink)
            Case ActionType.PasteAsSymlink
                Dim args As String = CType(itemInfo.ActionArgs, String)
                'FileBrowser.itemClipboard.PasteItems(paths(0), PasteType.Symlink)
            Case ActionType.PasteAsShortcut
                Dim args As String = CType(itemInfo.ActionArgs, String)
                'FileBrowser.itemClipboard.PasteItems(paths(0), PasteType.Shortcut)
            Case ActionType.PasteAsJunction
                Dim args As String = CType(itemInfo.ActionArgs, String)
                'FileBrowser.itemClipboard.PasteItems(paths(0), PasteType.Junction)

            Case ActionType.CopyTo
                'Operations.CopyTo(paths)
            Case ActionType.MoveTo
                'Operations.MoveTo(paths)
            Case ActionType.DeleteToRecycleBin
                'Operations.DeleteToRecycleBin(paths)
            Case ActionType.DeletePermanently
                'Operations.DeletePermanently(paths)
        End Select
    End Sub
End Class
