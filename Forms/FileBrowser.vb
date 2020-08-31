Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Public Class FileBrowser
#Region "Properties"
    ' class instances. all others are static classes, or Forms which are automatically instances
    Public ctxMenu As New CtxMenu
    Public itemClipboard As New ItemClipboard
    Private WithEvents winCtxMenu As New WalkmanLib.ContextMenu

    Private _currentDir As String
    Public Property CurrentDir() As String
        Get
            Return _currentDir
        End Get
        Set(ByVal value As String)
            If Directory.Exists(value) Then
                _currentDir = value
                cbxURI.Text = value
                LoadFolder()
                ShowNode(value)
            End If
        End Set
    End Property

    Public Property UseShell As Boolean
        Get
            Return menuToolsUseShell.Checked
        End Get
        Set(value As Boolean)
            menuToolsUseShell.Checked = value
        End Set
    End Property
#End Region

    Private Sub FileBrowser_Load() Handles Me.Shown
        Settings.Init()
        ContextMenuConfig.Init()

        If Not Settings.DisableUpdateCheck Then
            WalkmanLib.CheckIfUpdateAvailableInBackground("FileBrowser", My.Application.Info.Version, New RunWorkerCompletedEventHandler(AddressOf UpdateCheckComplete))
        End If
        UseShell = Settings.WindowsShellDefaultValue
        Me.Size = New Size(Settings.WindowDefaultWidth.GetValueOrDefault(Me.Width), Settings.WindowDefaultHeight.GetValueOrDefault(Me.Height))
        Me.CenterToParent()
        If Settings.WindowMaximised Then
            Me.WindowState = FormWindowState.Maximized
        End If

        lstCurrent.DoubleBuffered(True)
        treeViewDirs.PathSeparator = Path.DirectorySeparatorChar

        treeViewDirs.Nodes.Clear()
        If Helpers.GetOS() = OS.Windows Then
            For Each drive In Environment.GetLogicalDrives()
                AddNode(treeViewDirs, drive)
            Next
        Else
            Dim subNode As TreeNode = treeViewDirs.Nodes.Add(Path.DirectorySeparatorChar)
            subNode.Nodes.Add("")
        End If

        handle_SelectedItemChanged()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        winCtxMenu.HandleWindowMessage(m)
        MyBase.WndProc(m)
    End Sub

#Region "Helpers"
    Private Function UpdateItem(item As ListViewItem, itemInfo As Filesystem.EntryInfo) As ListViewItem
        item.Tag = itemInfo
        item.Text = itemInfo.DisplayName
        item.SubItems.Item(1).Text = itemInfo.Extension
        item.SubItems.Item(2).Text = itemInfo.LastWriteTime.ToString()
        item.SubItems.Item(3).Text = itemInfo.LastAccessTime.ToString()
        item.SubItems.Item(4).Text = itemInfo.CreationTime.ToString()
        item.SubItems.Item(5).Text = itemInfo.Size.ToString()
        item.SubItems.Item(6).Text = itemInfo.SizeOnDisk.ToString()
        item.SubItems.Item(7).Text = itemInfo.Attributes.ToString()
        item.SubItems.Item(8).Text = itemInfo.AllTarget
        item.SubItems.Item(9).Text = itemInfo.SymlinkTarget
        item.SubItems.Item(10).Text = itemInfo.LinkTarget
        item.SubItems.Item(11).Text = itemInfo.UrlTarget
        item.SubItems.Item(12).Text = itemInfo.HardlinkCount.ToString()
        item.SubItems.Item(13).Text = itemInfo.ADSCount.ToString()
        item.SubItems.Item(14).Text = itemInfo.OpensWith
        item.SubItems.Item(15).Text = itemInfo.DownloadURL
        item.SubItems.Item(16).Text = itemInfo.DownloadReferrer
        Return item
    End Function

    Private Function CreateItem(itemInfo As Filesystem.EntryInfo) As ListViewItem
        Return UpdateItem(New ListViewItem(Enumerable.Repeat(String.Empty, 17).ToArray()), itemInfo)
    End Function

    Private Function GetItemInfo(item As ListViewItem) As Filesystem.EntryInfo
        Return DirectCast(item.Tag, Filesystem.EntryInfo)
    End Function

    Private Function AddNode(root As TreeView, text As String) As TreeNode
        Dim subNode As TreeNode = root.Nodes.Add(text, text)
        Try : If Directory.EnumerateDirectories(text).Any Then
                subNode.Nodes.Add("")
            End If
        Catch : End Try
        Return subNode
    End Function
    Private Function AddNode(parentNode As TreeNode, text As String) As TreeNode
        Dim subNode As TreeNode = parentNode.Nodes.Add(text, text)
        Try : If Directory.EnumerateDirectories(Path.Combine(parentNode.FullPath, text)).Any() Then
                subNode.Nodes.Add("")
            End If
        Catch : End Try
        Return subNode
    End Function

    Private Sub LoadFolder()
        lstCurrent.Items.Clear()
        For Each itemInfo In Filesystem.GetItems(_currentDir)
            lstCurrent.Items.Add(CreateItem(itemInfo))
        Next
    End Sub

    Private Sub SelectItem(name As String)
        lstCurrent.SelectedItems.Clear()

        For Each item As ListViewItem In lstCurrent.Items
            Dim itemInfo As Filesystem.EntryInfo = GetItemInfo(item)
            If Path.GetFileName(itemInfo.FullName) = name Then
                item.Selected = True
                item.Focused = True
                item.EnsureVisible()
            End If
        Next
        lstCurrent.Select()
    End Sub

    Public Sub ShowFile(filePath As String)
        CurrentDir = Path.GetDirectoryName(filePath)
        If CurrentDir = Path.GetDirectoryName(filePath) Then ' have to check, as the path could be not loaded
            SelectItem(Path.GetFileName(filePath))
        End If
    End Sub

    Private Sub LoadNode(node As TreeNode)
        node.Nodes.Clear()
        For Each item As Filesystem.EntryInfo In Filesystem.GetFolders(node.FullPath)
            AddNode(node, item.DisplayName)
        Next
    End Sub

    Public Sub ShowNode(nodePath As String)
        g_showingNodes = True
        Try
            Dim parent As TreeNode = Nothing
            For Each folder As String In nodePath.Split({Path.DirectorySeparatorChar}, StringSplitOptions.RemoveEmptyEntries)
                Dim foundNodes As TreeNode()

                If folder.EndsWith(Path.VolumeSeparatorChar) Then
                    If Helpers.GetOS() = OS.Windows Then folder &= Path.DirectorySeparatorChar
                    foundNodes = treeViewDirs.Nodes.Find(folder, False)
                Else
                    foundNodes = parent?.Nodes.Find(folder, False)
                End If

                If foundNodes?.Length = 0 Then
                    Exit For
                End If

                parent = foundNodes(0)
                parent.Expand()
            Next
            treeViewDirs.SelectedNode = parent
        Finally
            g_showingNodes = False
        End Try
    End Sub

    Public Function GetSelectedPaths(Optional forceTree As Boolean = False) As String()
        If Not forceTree AndAlso lstCurrent.SelectedItems.Count > 0 Then
            Return lstCurrent.SelectedItems.Cast(Of ListViewItem).Select(Function(t) GetItemInfo(t).FullName).ToArray()
        ElseIf treeViewDirs.SelectedNode IsNot Nothing Then
            Return {treeViewDirs.SelectedNode.FullPath}
        Else
            Return {}
        End If
    End Function

    Public Sub RenameSelected() Handles winCtxMenu.ItemRenamed
        If lstCurrent.SelectedItems.Count = 1 Then
            lstCurrent.SelectedItems(0).BeginEdit()
        ElseIf lstCurrent.SelectedItems.Count > 1 Then
            Dim newName As String = Path.GetFileNameWithoutExtension(lstCurrent.SelectedItems(0).Text) & "_{0}" & GetItemInfo(lstCurrent.SelectedItems(0)).Extension
            If Operations.GetInput(newName, "Rename Items", "Enter New Name:", "{0} will be replaced with an incrementing number.") = DialogResult.OK Then
                For i = 1 To lstCurrent.SelectedItems.Count
                    Dim itemInfo As Filesystem.EntryInfo = GetItemInfo(lstCurrent.SelectedItems(i - 1))
                    Operations.Rename(itemInfo.FullName, String.Format(newName, i))
                Next
            End If
        ElseIf treeViewDirs.SelectedNode IsNot Nothing Then
            treeViewDirs.SelectedNode.BeginEdit()
        End If
    End Sub

    Private Sub UpdateCheckComplete(sender As Object, e As RunWorkerCompletedEventArgs)
        If Not Settings.DisableUpdateCheck Then
            If e.Error Is Nothing Then
                If DirectCast(e.Result, Boolean) Then
                    Select Case WalkmanLib.CustomMsgBox("An update is available!", "Go to Download page", "Disable Update Check", "Ignore",
                                                        Microsoft.VisualBasic.MsgBoxStyle.Information, "Update Check", ownerForm:=Me)
                        Case "Go to Download page"
                            Launch.LaunchItem("https://github.com/Walkman100/FileBrowser/releases/latest", Nothing, Nothing)
                        Case "Disable Update Check"
                            Settings.chkDisableUpdateCheck.Checked = True
                    End Select
                End If
            Else
                Microsoft.VisualBasic.MsgBox("Update check failed!" & Environment.NewLine & e.Error.Message,
                                             Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Update Check")
            End If
        End If
    End Sub
#End Region

#Region "TreeView"
    Dim g_showingNodes As Boolean = False ' make sure we don't infinitely load nodes
    Private Sub treeViewDirs_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treeViewDirs.AfterSelect
        If Not g_showingNodes Then CurrentDir = e.Node.FullPath
    End Sub
    Private Sub treeViewDirs_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles treeViewDirs.BeforeExpand
        LoadNode(e.Node)
        If e.Node.Nodes.Count = 0 Then e.Cancel = True
    End Sub
    Private Sub treeViewDirs_AfterLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles treeViewDirs.AfterLabelEdit
        If Not String.IsNullOrEmpty(e.Label) Then Operations.Rename(e.Node.FullPath, e.Label)
    End Sub
#End Region

#Region "ListView"
    Private Sub lstCurrent_ItemActivate() Handles lstCurrent.ItemActivate
        If lstCurrent.SelectedItems.Count = 1 AndAlso Directory.Exists(GetItemInfo(lstCurrent.SelectedItems.Item(0)).FullName) Then
            CurrentDir = GetItemInfo(lstCurrent.SelectedItems.Item(0)).FullName
        ElseIf lstCurrent.SelectedItems.Count > 0 Then
            For Each itemInfo As Filesystem.EntryInfo In lstCurrent.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetItemInfo)
                Launch.LaunchItem(itemInfo.FullName, Nothing, Nothing)
            Next
        End If
    End Sub
    Private Sub treeViewDirs_BeforeLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles treeViewDirs.BeforeLabelEdit
        If e.Node.Parent Is Nothing Then
            e.CancelEdit = True
        End If
    End Sub
    Private Sub lstCurrent_AfterLabelEdit(sender As Object, e As LabelEditEventArgs) Handles lstCurrent.AfterLabelEdit
        If Not String.IsNullOrEmpty(e.Label) Then Operations.Rename(GetItemInfo(lstCurrent.Items.Item(e.Item)).FullName, e.Label)
    End Sub
    Private Sub lstCurrent_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstCurrent.ColumnClick

    End Sub
#End Region

#Region "ToolStrips"
    Private Sub menuFileCreate_Click() Handles menuFileCreate.Click

    End Sub
    Private Sub menuFileCopyPath_Click() Handles menuFileCopyPath.Click
        Launch.Copy(GetSelectedPaths(), If(My.Computer.Keyboard.ShiftKeyDown, """{path}""", "{path}"))
    End Sub
    Private Sub menuFileRename_Click() Handles menuFileRename.Click
        RenameSelected()
    End Sub
    Private Sub menuFileRecycle_Click() Handles menuFileRecycle.Click
        Operations.Delete(GetSelectedPaths(), UseShell, My.Computer.Keyboard.ShiftKeyDown)
    End Sub
    Private Sub menuFileDelete_Click() Handles menuFileDelete.Click
        Operations.Delete(GetSelectedPaths(), UseShell, Not My.Computer.Keyboard.ShiftKeyDown)
    End Sub
    Private Sub menuFileCopyTo_Click() Handles menuFileCopyTo.Click
        Helpers.CopyTo(GetSelectedPaths(), My.Computer.Keyboard.ShiftKeyDown)
    End Sub
    Private Sub menuFileMoveTo_Click() Handles menuFileMoveTo.Click
        Helpers.MoveTo(GetSelectedPaths(), My.Computer.Keyboard.ShiftKeyDown)
    End Sub
    Private Sub menuFileProperties_Click() Handles menuFileProperties.Click
        For Each path As String In GetSelectedPaths()
            Launch.WinProperties(path, Nothing)
        Next
    End Sub
    Private Sub menuFileLaunch_Click() Handles menuFileLaunch.Click
        For Each path As String In GetSelectedPaths()
            Launch.LaunchItem(path, Nothing, Nothing)
        Next
    End Sub
    Private Sub menuFileRunAs_Click() Handles menuFileRunAs.Click
        For Each path As String In GetSelectedPaths()
            '   taken from default %PATHEXT% on windows
            If {".com", ".exe", ".bat", ".cmd", ".vbs", ".vbe", ".msc"}.Any(AddressOf path.EndsWith) Then
                Launch.RunAsAdmin(path, Nothing, Nothing)
            Else
                Launch.RunAsAdmin(path, "{openwith}", """{path}""")
            End If
        Next
    End Sub
    Private Sub menuFileOpenWith_Click() Handles menuFileOpenWith.Click
        For Each path As String In GetSelectedPaths()
            Launch.OpenWith(path, Nothing)
        Next
    End Sub
    Private Sub menuFileShowTarget_Click() Handles menuFileShowTarget.Click
        If lstCurrent.SelectedItems.Count > 0 Then
            For Each itemInfo As Filesystem.EntryInfo In lstCurrent.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetItemInfo)
                If Not String.IsNullOrEmpty(itemInfo.AllTarget) Then
                    ShowFile(itemInfo.AllTarget)
                    Exit For
                End If
            Next
        ElseIf treeViewDirs.SelectedNode IsNot Nothing Then
            Dim info As New DirectoryInfo(treeViewDirs.SelectedNode.FullPath)
            If info.Attributes.HasFlag(FileAttributes.ReparsePoint) Then
                Try : ShowFile(WalkmanLib.GetSymlinkTarget(info.FullName))
                Catch : End Try
            End If
        End If
    End Sub
    Private Sub menuFileExit_Click() Handles menuFileExit.Click
        Application.Exit()
    End Sub

    Private Sub menuEditCut_Click() Handles menuEditCut.Click
        itemClipboard.AddItems(GetSelectedPaths(), ItemType.Cut, Not My.Computer.Keyboard.ShiftKeyDown)
    End Sub
    Private Sub menuEditCopy_Click() Handles menuEditCopy.Click
        itemClipboard.AddItems(GetSelectedPaths(), ItemType.Copy, Not My.Computer.Keyboard.ShiftKeyDown)
    End Sub
    Private Sub menuEditPaste_Click() Handles menuEditPaste.Click
        itemClipboard.PasteItems(CurrentDir, PasteType.Normal)
    End Sub
    Private Sub menuEditPasteAsHardlink_Click() Handles menuEditPasteAsHardlink.Click
        itemClipboard.PasteItems(CurrentDir, PasteType.Hardlink)
    End Sub
    Private Sub menuEditPasteAsSymlink_Click() Handles menuEditPasteAsSymlink.Click
        itemClipboard.PasteItems(CurrentDir, PasteType.Symlink)
    End Sub
    Private Sub menuEditPasteAsShortcut_Click() Handles menuEditPasteAsShortcut.Click
        itemClipboard.PasteItems(CurrentDir, PasteType.Shortcut)
    End Sub
    Private Sub menuEditPasteAsJunction_Click() Handles menuEditPasteAsJunction.Click
        itemClipboard.PasteItems(CurrentDir, PasteType.Junction)
    End Sub
    Private Sub menuEditSelectAll_Click() Handles menuEditSelectAll.Click
        lstCurrent.BeginUpdate()
        For Each item As ListViewItem In lstCurrent.Items
            item.Selected = True
        Next
        lstCurrent.EndUpdate()
    End Sub
    Private Sub menuEditDeselectAll_Click() Handles menuEditDeselectAll.Click
        lstCurrent.BeginUpdate()
        lstCurrent.SelectedItems.Clear()
        lstCurrent.EndUpdate()
    End Sub
    Private Sub menuEditInvert_Click() Handles menuEditInvert.Click
        lstCurrent.BeginUpdate()
        For Each item As ListViewItem In lstCurrent.Items
            item.Selected = Not item.Selected
        Next
        lstCurrent.EndUpdate()
    End Sub

    Private Sub menuGoBack_Click() Handles menuGoBack.Click

    End Sub
    Private Sub menuGoForward_Click() Handles menuGoForward.Click

    End Sub
    Private Sub menuGoUp_Click() Handles menuGoUp.Click
        CurrentDir = Path.GetDirectoryName(CurrentDir)
    End Sub
    Private Sub menuGoRoot_Click() Handles menuGoRoot.Click
        CurrentDir = Path.GetPathRoot(CurrentDir)
    End Sub
    Private Sub menuGoHome_Click() Handles menuGoHome.Click
        If Helpers.GetOS() = OS.Windows Then
            CurrentDir = Environment.GetEnvironmentVariable("UserProfile")
        Else
            CurrentDir = Environment.GetEnvironmentVariable("HOME")
        End If
    End Sub
    Private Sub menuGoRefresh_Click() Handles menuGoRefresh.Click
        LoadFolder()
    End Sub
    Private Sub menuGoStop_Click() Handles menuGoStop.Click

    End Sub

    Private Sub menuToolsSettings_Click() Handles menuToolsSettings.Click
        Settings.Show(Me)
    End Sub
    Private Sub menuToolsContextMenu_Click() Handles menuToolsContextMenu.Click
        ContextMenuConfig.Init() ' the window is Closed, so needs to be re-inited
        ContextMenuConfig.Show(Me)
    End Sub
    Private Sub menuToolsColumns_Click() Handles menuToolsColumns.Click
        Dim frm As New ColumnConfig
        frm.Show(Me)
    End Sub
    Private Sub menuToolsResizeColumns_Click() Handles menuToolsResizeColumns.Click
        For Each column As ColumnHeader In lstCurrent.Columns.Cast(Of ColumnHeader).Where(Function(c) c.Width > 0)
            lstCurrent.AutoResizeColumn(column.Index, ColumnHeaderAutoResizeStyle.HeaderSize)
        Next
    End Sub

    Private Sub btnGo_Click() Handles btnGo.Click
        Dim newPath As String = Environment.ExpandEnvironmentVariables(cbxURI.Text)
        If Directory.Exists(newPath) Then
            CurrentDir = newPath
            cbxURI.Items.Add(newPath)
        End If
    End Sub

    Private Sub cbxURI_KeyUp(sender As Object, e As KeyEventArgs) Handles cbxURI.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            btnGo_Click()
        End If
    End Sub
#End Region

#Region "Other UI Methods"
    Private Sub FileBrowser_Resize() Handles MyBase.Resize
        cbxURI.Size = New Size(Me.Width - 52, cbxURI.Size.Height)

        If Settings.Loaded AndAlso Me.WindowState <> FormWindowState.Maximized Then
            Settings.txtWindowDefaultWidth.Text = Me.Width.ToString()
            Settings.txtWindowDefaultHeight.Text = Me.Height.ToString()
        End If
    End Sub
    Public Sub handle_SelectedItemChanged() Handles lstCurrent.SelectedIndexChanged, treeViewDirs.AfterSelect
        Dim itemSelected As Boolean = (lstCurrent.SelectedItems.Count > 0) OrElse treeViewDirs.SelectedNode IsNot Nothing
        menuFileCopyPath.Enabled = itemSelected
        menuFileRename.Enabled = itemSelected
        menuFileRecycle.Enabled = itemSelected
        menuFileDelete.Enabled = itemSelected
        menuFileCopyTo.Enabled = itemSelected
        menuFileMoveTo.Enabled = itemSelected
        menuFileProperties.Enabled = itemSelected
        menuFileLaunch.Enabled = itemSelected
        menuFileRunAs.Enabled = itemSelected
        menuFileOpenWith.Enabled = itemSelected
        menuFileShowTarget.Enabled = itemSelected
        menuEditCut.Enabled = itemSelected
        menuEditCopy.Enabled = itemSelected
        menuEditSelectAll.Enabled = itemSelected
        menuEditDeselectAll.Enabled = itemSelected
        menuEditInvert.Enabled = itemSelected

        If itemSelected Then itemSelected = (itemClipboard.ItemStore.Count > 0)

        menuEditPaste.Enabled = itemSelected
        menuEditPasteAsHardlink.Enabled = itemSelected
        menuEditPasteAsSymlink.Enabled = itemSelected
        menuEditPasteAsShortcut.Enabled = itemSelected
        menuEditPasteAsJunction.Enabled = itemSelected
    End Sub

    Private g_forceTree As Boolean = False
    Private Sub ShowContext(sender As Object, pos As Point)
        Dim paths As String() = GetSelectedPaths(sender Is treeViewDirs)

        If My.Computer.Keyboard.CtrlKeyDown Then
            winCtxMenu.BuildMenu(Handle, paths, flags:=WalkmanLib.ContextMenu.QueryContextMenuFlags.CanRename Or
                If(My.Computer.Keyboard.ShiftKeyDown, WalkmanLib.ContextMenu.QueryContextMenuFlags.ExtendedVerbs, WalkmanLib.ContextMenu.QueryContextMenuFlags.Normal))
            winCtxMenu.ShowMenu(Handle, DirectCast(sender, Control).PointToScreen(pos))
            winCtxMenu.DestroyMenu()
        Else
            g_forceTree = sender Is treeViewDirs
            ctxMenu.UpdateMenu(ctxMenuL, paths)
            If sender Is lstCurrent Then
                ctxMenuL.Show(lstCurrent, pos)
            ElseIf sender Is treeViewDirs Then
                ctxMenuL.Show(treeViewDirs, pos)
            Else
                ctxMenuL.Show(PointToScreen(pos))
            End If
        End If
    End Sub
    Private Sub handleMouseUp(sender As Object, e As MouseEventArgs) Handles lstCurrent.MouseUp, treeViewDirs.MouseUp
        If e.Button = MouseButtons.Right Then
            ShowContext(sender, e.Location)
        End If
    End Sub
    Private Sub handleKeyUp(sender As Object, e As KeyEventArgs) Handles lstCurrent.KeyUp, treeViewDirs.KeyUp
        If e.KeyCode = Keys.Delete AndAlso e.Modifiers = Keys.Shift Then
            menuFileRecycle_Click()
        ElseIf e.KeyCode = Keys.Apps OrElse (e.KeyCode = Keys.F10 AndAlso e.Modifiers = Keys.Shift) Then
            ShowContext(sender, New Point(0, 0))
        End If
    End Sub
    Public Sub ctxMenuL_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ctxMenuL.ItemClicked
        ctxMenu.RunItem(e.ClickedItem, GetSelectedPaths(g_forceTree))
    End Sub
    Private Sub handleToolTipChanged(text As String, ex As Exception) Handles winCtxMenu.HelpTextChanged
        If ex Is Nothing AndAlso Not String.IsNullOrEmpty(text) Then
            statusLabel.Text = text
        Else
            statusLabel.Text = Nothing
        End If
    End Sub

    Public Sub RestartAsAdmin()
        WalkmanLib.RunAsAdmin(Path.Combine(Application.StartupPath, Process.GetCurrentProcess.ProcessName & ".exe"), """" & CurrentDir & """")
        Application.Exit()
    End Sub

    Public Sub ErrorParser(ex As Exception)
        If TypeOf ex Is UnauthorizedAccessException AndAlso Not WalkmanLib.IsAdmin() Then
            If Microsoft.VisualBasic.MsgBox(ex.Message & Environment.NewLine & Environment.NewLine & "Restart as Admin?",
                                            Microsoft.VisualBasic.MsgBoxStyle.YesNo Or Microsoft.VisualBasic.MsgBoxStyle.Exclamation,
                                            "Access Denied") = Microsoft.VisualBasic.MsgBoxResult.Yes Then
                RestartAsAdmin()
            End If
        Else
            WalkmanLib.ErrorDialog(ex, messagePumpForm:=Me)
        End If
    End Sub
#End Region
End Class
