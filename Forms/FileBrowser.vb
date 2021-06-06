Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
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
                Task.Run(Sub() ShowNode(Me, value))
                If Settings.RememberDir Then
                    Settings.txtDefaultDir.Text = value
                End If
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
        AddHandler lstCurrent.DrawColumnHeader, AddressOf WalkmanLib.CustomPaint.ListView_DrawCustomColumnHeader
        AddHandler lstCurrent.DrawItem, AddressOf WalkmanLib.CustomPaint.ListView_DrawDefaultItem
        AddHandler lstCurrent.DrawSubItem, AddressOf WalkmanLib.CustomPaint.ListView_DrawDefaultSubItem
        Settings.Init()
        ContextMenuConfig.Init()

        If Not Settings.DisableUpdateCheck Then
            WalkmanLib.CheckIfUpdateAvailableInBackground("FileBrowser", My.Application.Info.Version, New RunWorkerCompletedEventHandler(AddressOf UpdateCheckComplete))
        End If
        UseShell = Settings.WindowsShellDefaultValue
        Me.Size = New Size(Settings.WindowDefaultWidth.GetValueOrDefault(Me.Width), Settings.WindowDefaultHeight.GetValueOrDefault(Me.Height))
        scMain.SplitterDistance = Settings.SplitterSize.GetValueOrDefault(scMain.SplitterDistance)
        Me.CenterToParent()
        If Settings.WindowMaximised Then
            Me.WindowState = FormWindowState.Maximized
        End If

        lstCurrent.DoubleBuffered(True)
        treeViewDirs.DoubleBuffered(True)
        treeViewDirs.PathSeparator = Path.DirectorySeparatorChar

        treeViewDirs.ImageList = ImageHandling.CreateImageList(16)

        treeViewDirs.Nodes.Clear()
        If WalkmanLib.GetOS() = WalkmanLib.OS.Windows Then
            For Each drive In Environment.GetLogicalDrives()
                AddRootNode(treeViewDirs, drive)
            Next

            If WalkmanLib.IsAdmin Then
                Me.Text = "[Admin] FileBrowser"
                menuFileRelaunch.Visible = False
            Else
                Me.Text = "FileBrowser"
            End If
        Else
            menuFileProperties.Visible = False
            menuFileRunAs.Visible = False
            menuFileOpenWith.Visible = False
            menuFileRelaunch.Visible = False

            Dim subNode As TreeNode = treeViewDirs.Nodes.Add(Path.DirectorySeparatorChar)
            subNode.Nodes.Add("")
        End If

        handle_SelectedItemChanged()

        Dim resultInfo As WalkmanLib.ResultInfo = WalkmanLib.ProcessArgs(Environment.GetCommandLineArgs.Skip(1).ToArray(), flagDict, True)
        If resultInfo.gotError Then
            MessageBox.Show(resultInfo.errorInfo, "Error processing args", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If resultInfo.extraParams.Count > 0 Then
            CurrentDir = resultInfo.extraParams.Item(0)
        ElseIf CurrentDir = "" Then ' check if ShowFile has been called
            CurrentDir = Settings.DefaultDir
        End If
    End Sub

    Public Sub ApplyTheme(theme As WalkmanLib.Theme)
        WalkmanLib.ApplyTheme(theme, Me, True)
        WalkmanLib.ApplyTheme(theme, Me.components.Components, True)
        WalkmanLib.ApplyTheme(theme, Settings)
        WalkmanLib.ApplyTheme(theme, clipboardList.Controls, True)

        ' ToolStrip custom paint
        If theme = WalkmanLib.Theme.Default Then
            ToolStripManager.RenderMode = ToolStripManagerRenderMode.Professional
        ElseIf theme = WalkmanLib.Theme.SystemDark Then
            ToolStripManager.RenderMode = ToolStripManagerRenderMode.System
        Else
            ToolStripManager.Renderer = New WalkmanLib.CustomPaint.ToolStripSystemRendererWithDisabled(theme.ToolStripItemDisabledText)
        End If

        ' ListView custom paint
        lstCurrent.Tag = theme.ListViewColumnColors
        clipboardList.lstMain.Tag = theme.ListViewColumnColors
        clipboardList.BackColor = theme.MenuStripBG
        clipboardList.lblItemCount.BackColor = theme.MenuStripBG

        ' update items
        Dim _settings As Settings = Settings
        Task.Run(Sub()
                     For Each node As TreeNode In treeViewDirs.Nodes
                         SetNodeColor(node, _settings, True)
                     Next
                 End Sub)
        Dim colors As Tuple(Of Color, Color, Color) = GetItemColors(_settings)
        Task.Run(Sub()
                     For Each item As ListViewItem In lstCurrent.Items
                         Dim itemInfo As Filesystem.EntryInfo = GetItemInfo(item)
                         If _settings.HighlightCompressed AndAlso itemInfo.Attributes.HasFlag(FileAttributes.Compressed) Then
                             item.ForeColor = colors.Item2
                         ElseIf _settings.HighlightEncrypted AndAlso itemInfo.Attributes.HasFlag(FileAttributes.Encrypted) Then
                             item.ForeColor = colors.Item3
                         Else
                             item.ForeColor = colors.Item1
                         End If
                     Next
                 End Sub)
    End Sub

    ''' <summary>Get item colors for current theme</summary>
    ''' <param name="_settings">Instance of <see cref="Settings"/> to get theme from</param>
    ''' <returns><see cref="Tuple"/> with default color as <see langword="Item1"/>, compressed color as <see langword="Item2"/>, and encrypted color as <see langword="Item3"/></returns>
    Private Shared Function GetItemColors(_settings As Settings) As Tuple(Of Color, Color, Color)
        If _settings.Theme = WalkmanLib.Theme.Default Then
            Return New Tuple(Of Color, Color, Color)(_settings.Theme.TreeViewFG, Color.MediumBlue, Color.Green)
        ElseIf _settings.Theme = WalkmanLib.Theme.SystemDark Then
            Return New Tuple(Of Color, Color, Color)(_settings.Theme.TreeViewFG, Color.LightSkyBlue, Color.LimeGreen)
        ElseIf _settings.Theme = WalkmanLib.Theme.Dark Then
            Return New Tuple(Of Color, Color, Color)(_settings.Theme.TreeViewFG, Color.FromArgb(&HFF3A99E8), Color.LimeGreen)
        ElseIf _settings.Theme = WalkmanLib.Theme.Inverted Then
            Return New Tuple(Of Color, Color, Color)(_settings.Theme.TreeViewFG, Color.DeepSkyBlue, Color.LimeGreen)
        Else
            Return New Tuple(Of Color, Color, Color)(_settings.Theme.TreeViewFG, Color.RoyalBlue, Color.Green)
        End If
    End Function

    Private flagDict As New Dictionary(Of String, WalkmanLib.FlagInfo) From {
        {"select", New WalkmanLib.FlagInfo With {
            .shortFlag = "s"c,
            .hasArgs = True,
            .action = Function(file)
                          ShowFile(file)
                          Return True
                      End Function
        }},
        {"show", New WalkmanLib.FlagInfo With {
            .hasArgs = True,
            .action = Function(file)
                          ShowFile(file)
                          Return True
                      End Function
        }}
    }

    Protected Overrides Sub WndProc(ByRef m As Message)
        winCtxMenu.HandleWindowMessage(m)
        MyBase.WndProc(m)
    End Sub

#Region "Helpers"
    Private Shared Function UpdateItem(item As ListViewItem, itemInfo As Filesystem.EntryInfo) As ListViewItem
        item.Tag = itemInfo
        item.Text = itemInfo.DisplayName
        item.SubItems.Item(1).Text = itemInfo.Extension
        item.SubItems.Item(2).Text = itemInfo.LastWriteTime.ToString()
        item.SubItems.Item(3).Text = itemInfo.LastAccessTime.ToString()
        item.SubItems.Item(4).Text = itemInfo.CreationTime.ToString()
        item.SubItems.Item(5).Text = Helpers.ConvSize(itemInfo.Size)
        item.SubItems.Item(6).Text = Helpers.ConvSize(itemInfo.SizeOnDisk)
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

        Dim colors As Tuple(Of Color, Color, Color) = GetItemColors(Settings)
        If Settings.HighlightCompressed AndAlso itemInfo.Attributes.HasFlag(FileAttributes.Compressed) Then
            item.ForeColor = colors.Item2
        ElseIf Settings.HighlightEncrypted AndAlso itemInfo.Attributes.HasFlag(FileAttributes.Encrypted) Then
            item.ForeColor = colors.Item3
        Else
            item.ForeColor = colors.Item1
        End If

        Return item
    End Function
    Private Shared Function CreateItem(itemInfo As Filesystem.EntryInfo) As ListViewItem
        Return UpdateItem(New ListViewItem(Enumerable.Repeat(String.Empty, 17).ToArray()), itemInfo)
    End Function
    Public Shared Function GetItemInfo(item As ListViewItem) As Filesystem.EntryInfo
        Return DirectCast(item.Tag, Filesystem.EntryInfo)
    End Function

    Private Function AddRootNode(root As TreeView, text As String) As TreeNode
        Dim node As TreeNode = root.Nodes.Add(text, text)
        SetNodeExpandable(Me, node)
        SetNodeColor(node, Settings)
        If Settings.EnableIcons Then SetNodeImage(Settings, node)
        Return node
    End Function
    Private Sub SetNodeExpandable(baseControl As Control, node As TreeNode)
        Try
            If Directory.EnumerateDirectories(node.FullPath).Any() Then
                Helpers.AutoInvoke(baseControl, Sub() node.Nodes.Add(""))
            End If
        Catch : End Try
    End Sub
    Private Function GetForeColor(path As String, _settings As Settings) As Color
        Dim colors As Tuple(Of Color, Color, Color) = GetItemColors(_settings)
        If _settings.HighlightCompressed AndAlso File.GetAttributes(path).HasFlag(FileAttributes.Compressed) Then
            Return colors.Item2
        ElseIf _settings.HighlightEncrypted AndAlso File.GetAttributes(path).HasFlag(FileAttributes.Encrypted) Then
            Return colors.Item3
        Else
            Return colors.Item1
        End If
    End Function
    Private Sub SetNodeColor(node As TreeNode, _settings As Settings, Optional recurse As Boolean = False)
        Try : node.ForeColor = GetForeColor(node.FullPath, _settings)
        Catch : End Try

        If recurse Then
            For Each subNode As TreeNode In node.Nodes
                SetNodeColor(subNode, _settings, recurse)
            Next
        End If
    End Sub
    Private Sub SetNodeImage(settings As Settings, node As TreeNode)
        ImageHandling.SetImage(settings, node, treeViewDirs.ImageList, treeViewDirs.ImageList.ImageSize.Width)
    End Sub

    Public Function GetSelectedPaths(Optional forceTree As Boolean = False, Optional useGlobalNode As Boolean = False) As String()
        If Not forceTree AndAlso lstCurrent.SelectedItems.Count > 0 Then
            Return lstCurrent.SelectedItems.Cast(Of ListViewItem).Select(Function(t) GetItemInfo(t).FullName).ToArray()
        ElseIf useGlobalNode AndAlso g_selectedNode IsNot Nothing Then
            Return {g_selectedNode.FixedFullPath}
        ElseIf Not useGlobalNode AndAlso treeViewDirs.SelectedNode IsNot Nothing Then
            Return {treeViewDirs.SelectedNode.FixedFullPath}
        Else
            Return {}
        End If
    End Function

    Public Sub RenameSelected() Handles winCtxMenu.ItemRenamed
        If lstCurrent.SelectedItems.Count = 1 Then
            lstCurrent.SelectedItems(0).BeginEdit()
        ElseIf lstCurrent.SelectedItems.Count > 1 Then
            Dim newName As String = Path.GetFileNameWithoutExtension(lstCurrent.SelectedItems(0).Text) & "_{0}" & GetItemInfo(lstCurrent.SelectedItems(0)).Extension
            If Input.GetInput(newName, "Rename Items", "Enter New Name:", "{0} will be replaced with an incrementing number.") = DialogResult.OK Then
                For i = 1 To lstCurrent.SelectedItems.Count
                    Dim itemInfo As Filesystem.EntryInfo = GetItemInfo(lstCurrent.SelectedItems(i - 1))
                    Operations.Rename(itemInfo.FullName, String.Format(newName, i))
                Next
            End If
        ElseIf g_selectedNode IsNot Nothing Then
            g_selectedNode.BeginEdit()
        End If
    End Sub

    Private Sub UpdateCheckComplete(sender As Object, e As RunWorkerCompletedEventArgs)
        If Not Settings.DisableUpdateCheck Then
            If e.Error Is Nothing Then
                If DirectCast(e.Result, Boolean) Then
                    Select Case WalkmanLib.CustomMsgBox("An update is available!", "Update Check", "Go to Download page", "Disable Update Check",
                                                        "Ignore", MessageBoxIcon.Information, ownerForm:=Me)
                        Case "Go to Download page"
                            Launch.LaunchItem("https://github.com/Walkman100/FileBrowser/releases/latest", Nothing, Nothing)
                        Case "Disable Update Check"
                            Settings.chkDisableUpdateCheck.Checked = True
                    End Select
                End If
            Else
                MessageBox.Show("Update check failed!" & Environment.NewLine & e.Error.Message, "Update Check", 0, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub
#End Region

#Region "Loading Data"
    Private Async Sub LoadFolder()
        fswCurrent.EnableRaisingEvents = False

        If bwLoadFolder.IsBusy Then
            ' thanks to https://web.archive.org/web/20210315183540/https://social.msdn.microsoft.com/Forums/windowsapps/en-US/a9330b2a-9552-4722-a238-3a6d24f0c3a0/quotawaitquot-for-backgroundworker#54a41b87-8fbd-4416-b356-c64b0f79d935-isAnswer
            Dim tcs As New TaskCompletionSource(Of Object)
            Dim handler As RunWorkerCompletedEventHandler = Sub() tcs.TrySetResult(Nothing)

            AddHandler bwLoadFolder.RunWorkerCompleted, handler
            bwLoadFolder.CancelAsync()

            Await tcs.Task
            RemoveHandler bwLoadFolder.RunWorkerCompleted, handler
        End If

        lstCurrent.Items.Clear()
        lstCurrent.SmallImageList = Nothing
        lstCurrent.LargeImageList = Nothing
        g_disableSaveColumns = True

        Try
            bwLoadFolder.RunWorkerAsync()
        Catch ex As Exception
            statusLabel.Text = "Error starting BackgroundWorker: " & ex.Message
        End Try
    End Sub

    Private Sub bwLoadFolder_DoWork(sender As Object, e As DoWorkEventArgs) Handles bwLoadFolder.DoWork
        Dim bw As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        Dim cancelCheck As Func(Of Boolean) = Function() bw.CancellationPending

        If cancelCheck() Then e.Cancel = True : Return

        Dim colLst As List(Of Settings.Column) = Helpers.Invoke(Me, Function() Settings.DefaultColumns)
        If cancelCheck() Then e.Cancel = True : Return
        Helpers.ApplyColumns(Me, colLst)
        If cancelCheck() Then e.Cancel = True : Return

        If Helpers.Invoke(Me, Function() Settings.SaveColumns) Then
            FolderSettings.GetColumns(Me, CurrentDir)
        End If
        If cancelCheck() Then e.Cancel = True : Return
        Invoke(Sub() g_disableSaveColumns = False)

        Parallel.ForEach(Filesystem.GetItems(Me, _currentDir),
                         New ParallelOptions With {.MaxDegreeOfParallelism = Environment.ProcessorCount},
                         Sub(itemInfo, loopState)
                             If cancelCheck() Then loopState.Stop() : Return
                             Invoke(Sub() lstCurrent.Items.Add(CreateItem(itemInfo)))
                             If cancelCheck() Then loopState.Stop() : Return
                         End Sub)

        If cancelCheck() Then e.Cancel = True : Return

        Invoke(Sub() lastSort = New KeyValuePair(Of Sorting.SortBy, SortOrder)(Sorting.SortBy.Name, SortOrder.Ascending))
        If cancelCheck() Then e.Cancel = True : Return
        Sorting.Sort(Me, lstCurrent, lstCurrent.Items, lastSort.Key, lastSort.Value, cancelCheck)

        If cancelCheck() Then e.Cancel = True : Return

        If Helpers.Invoke(Me, Function() Settings.EnableIcons) Then
            If cancelCheck() Then e.Cancel = True : Return
            lstCurrent.SmallImageList = ImageHandling.GetImageList(16)
            If cancelCheck() Then e.Cancel = True : Return
            ImageHandling.SetImageListImages(Me, lstCurrent.Items, lstCurrent.SmallImageList, 16, True, cancelCheck)
        End If
    End Sub

    Private Sub bwLoadFolder_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bwLoadFolder.RunWorkerCompleted
        g_disableSaveColumns = False

        If e.Error IsNot Nothing Then
            ErrorParser(e.Error)
        ElseIf Not Settings.DisableViewAutoUpdate Then
            fswCurrent.Path = CurrentDir
            fswCurrent.EnableRaisingEvents = True
        End If
    End Sub

    Private Sub fswCurrent_ItemChanged() Handles fswCurrent.Changed, fswCurrent.Created, fswCurrent.Deleted, fswCurrent.Renamed
        fswCurrent.EnableRaisingEvents = False
        If Not Settings.DisableViewAutoUpdate Then LoadFolder()
    End Sub

    Private Sub SelectItem(name As String)
        lstCurrent.SelectedItems.Clear()

        For Each item As ListViewItem In lstCurrent.Items
            Dim itemInfo As Filesystem.EntryInfo = GetItemInfo(item)
            If Helpers.GetFileName(itemInfo.FullName).ToLowerInvariant() = name.ToLowerInvariant() Then
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
            SelectItem(Helpers.GetFileName(filePath))
        End If
    End Sub

    Private Sub LoadNode(baseControl As Control, node As TreeNode)
        Helpers.AutoInvoke(baseControl, Sub() node.Nodes.Clear())

        Dim _settings As Settings = Helpers.AutoInvoke(baseControl, Function() Settings)

        treeViewDirs.BeginUpdate()
        For Each item As Filesystem.EntryInfo In Filesystem.GetFolders(Me, node.FullPath)
            Helpers.AutoInvoke(baseControl, Function() node.Nodes.Add(item.DisplayName, item.DisplayName))
        Next
        treeViewDirs.EndUpdate()

        ' set node Color and Image in background, use Task.Run so we can continue loading nodes while these are running
        Task.Run(Sub()
                     For Each subNode As TreeNode In node.Nodes
                         SetNodeColor(subNode, _settings)
                     Next
                 End Sub)
        Task.Run(Sub()
                     If _settings.EnableIcons Then
                         For Each subNode As TreeNode In node.Nodes
                             SetNodeImage(_settings, subNode)
                         Next
                     End If
                 End Sub)

        For Each subNode As TreeNode In node.Nodes
            SetNodeExpandable(baseControl, subNode)
        Next
    End Sub

    Public Sub ShowNode(baseControl As Control, nodePath As String)
        Dim parent As TreeNode = Nothing
        For Each folder As String In nodePath.Split({Path.DirectorySeparatorChar}, StringSplitOptions.RemoveEmptyEntries)
            Dim foundNodes As TreeNode()

            If folder.EndsWith(Path.VolumeSeparatorChar) Then
                If WalkmanLib.GetOS() = WalkmanLib.OS.Windows Then folder &= Path.DirectorySeparatorChar
                foundNodes = treeViewDirs.Nodes.Find(folder, False)
            Else
                foundNodes = parent?.Nodes.Find(folder, False)
            End If

            If foundNodes Is Nothing OrElse foundNodes?.Length = 0 Then
                Exit For
            End If

            parent = foundNodes(0)

            If Not parent.IsExpanded Then
                g_disableNodeLoad = True ' expand event is ran Async, so run it manually instead
                parent.Expand()
                g_disableNodeLoad = False

                LoadNode(baseControl, parent)
            End If
        Next

        parent.EnsureVisible()
        g_disableNavigate = True ' suppress treeViewDirs_AfterSelect navigating to the selected node
        treeViewDirs.SelectedNode = parent
        g_disableNavigate = False
    End Sub
#End Region

#Region "TreeView"
    Private g_disableNavigate As Boolean = False
    Private Sub treeViewDirs_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treeViewDirs.AfterSelect
        If Not g_disableNavigate Then
            CurrentDir = e.Node.FixedFullPath()
        End If
    End Sub
    Private g_disableNodeLoad As Boolean = False
    Private Async Sub treeViewDirs_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles treeViewDirs.BeforeExpand
        Try
            If Not g_disableNodeLoad Then
                Await Task.Run(Sub() LoadNode(Me, e.Node))
                ' note the below actually does nothing, as the event finishes firing when Await is reached
                If e.Node.Nodes.Count = 0 Then e.Cancel = True
            End If
        Catch ex As Exception
            ErrorParser(ex)
        End Try
    End Sub
    Private Sub treeViewDirs_AfterCollapse(sender As Object, e As TreeViewEventArgs) Handles treeViewDirs.AfterCollapse
        For Each item As TreeNode In e.Node.Nodes
            ImageHandling.ReleaseImage(item, treeViewDirs.ImageList)
        Next
    End Sub
    Private Sub treeViewDirs_BeforeLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles treeViewDirs.BeforeLabelEdit
        If e.Node.Parent Is Nothing Then
            e.CancelEdit = True
        End If
    End Sub
    Private Sub treeViewDirs_AfterLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles treeViewDirs.AfterLabelEdit
        If Not String.IsNullOrEmpty(e.Label) Then Operations.Rename(e.Node.FixedFullPath, e.Label)
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
    Private Sub lstCurrent_AfterLabelEdit(sender As Object, e As LabelEditEventArgs) Handles lstCurrent.AfterLabelEdit
        If Not String.IsNullOrEmpty(e.Label) Then
            Dim itemInfo As Filesystem.EntryInfo = GetItemInfo(lstCurrent.Items.Item(e.Item))
            Dim targetName As String = e.Label

            If Settings.ShowExtensions = False AndAlso itemInfo.Type <> Filesystem.EntryType.AlternateDataStream Then
                targetName &= itemInfo.Extension
            End If

            Operations.Rename(itemInfo.FullName, targetName)
        End If
    End Sub
    Private lastSort As KeyValuePair(Of Sorting.SortBy, SortOrder)
    Private Sub lstCurrent_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstCurrent.ColumnClick
        Dim sortBy As Sorting.SortBy = DirectCast(e.Column, Sorting.SortBy)

        If sortBy = lastSort.Key Then
            lastSort = New KeyValuePair(Of Sorting.SortBy, SortOrder)(sortBy,
                If(lastSort.Value = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending))
        Else
            lastSort = New KeyValuePair(Of Sorting.SortBy, SortOrder)(sortBy, SortOrder.Ascending)
        End If
        Sorting.Sort(Me, lstCurrent, lstCurrent.Items, lastSort.Key, lastSort.Value)
    End Sub

    Private g_disableSaveColumns As Boolean = True
    Private Sub lstCurrent_ColumnEdit() Handles lstCurrent.ColumnReordered, lstCurrent.ColumnWidthChanged
        If Not g_disableSaveColumns AndAlso Settings.SaveColumns Then
            FolderSettings.SaveColumns(CurrentDir)
        End If
    End Sub
#End Region

#Region "ToolStrips"
    Private Sub menuFileCreateFile_Click() Handles menuFileCreateFile.Click
        ShowFile(Operations.CreateFile(CurrentDir, Not My.Computer.Keyboard.ShiftKeyDown))
    End Sub
    Private Sub menuFileCreateFolder_Click() Handles menuFileCreateFolder.Click
        ShowFile(Operations.CreateFolder(CurrentDir, Not My.Computer.Keyboard.ShiftKeyDown))
    End Sub
    Private Sub menuFileCopyPath_Click() Handles menuFileCopyPath.Click
        Launch.Copy(GetSelectedPaths(), If(My.Computer.Keyboard.ShiftKeyDown, """{path}""", "{path}"))
    End Sub
    Private Sub menuFileRename_Click() Handles menuFileRename.Click
        g_selectedNode = treeViewDirs.SelectedNode
        RenameSelected()
    End Sub
    Private Sub menuFileRecycle_Click() Handles menuFileRecycle.Click
        Operations.Delete(GetSelectedPaths(), useShell:=UseShell, deletePermanently:=My.Computer.Keyboard.ShiftKeyDown)
    End Sub
    Private Sub menuFileDelete_Click() Handles menuFileDelete.Click
        Operations.Delete(GetSelectedPaths(), useShell:=UseShell, deletePermanently:=Not My.Computer.Keyboard.ShiftKeyDown)
    End Sub
    Private Sub menuFileCopyTo_Click() Handles menuFileCopyTo.Click
        Operations.CopyTo(GetSelectedPaths(), My.Computer.Keyboard.ShiftKeyDown)
    End Sub
    Private Sub menuFileMoveTo_Click() Handles menuFileMoveTo.Click
        Operations.MoveTo(GetSelectedPaths(), My.Computer.Keyboard.ShiftKeyDown)
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
    Private Sub menuFileExecute_Click() Handles menuFileExecute.Click
        For Each path As String In GetSelectedPaths()
            Launch.ExecuteItem(path, Nothing, Nothing)
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
        itemClipboard.AddItems(GetSelectedPaths(), ItemClipboard.ItemType.Cut, replace:=Not My.Computer.Keyboard.ShiftKeyDown, addSystem:=Settings.CopySystem)
    End Sub
    Private Sub menuEditCopy_Click() Handles menuEditCopy.Click
        itemClipboard.AddItems(GetSelectedPaths(), ItemClipboard.ItemType.Copy, replace:=Not My.Computer.Keyboard.ShiftKeyDown, addSystem:=Settings.CopySystem)
    End Sub
    Private Sub menuEditPasteFBNormal_Click() Handles menuEditPasteFBNormal.Click
        itemClipboard.PasteItems(CurrentDir, ItemClipboard.PasteType.Normal, useSystem:=False)
    End Sub
    Private Sub menuEditPasteFBHardlink_Click() Handles menuEditPasteFBHardlink.Click
        itemClipboard.PasteItems(CurrentDir, ItemClipboard.PasteType.Hardlink, useSystem:=False)
    End Sub
    Private Sub menuEditPasteFBSymlink_Click() Handles menuEditPasteFBSymlink.Click
        itemClipboard.PasteItems(CurrentDir, ItemClipboard.PasteType.Symlink, useSystem:=False)
    End Sub
    Private Sub menuEditPasteFBShortcut_Click() Handles menuEditPasteFBShortcut.Click
        itemClipboard.PasteItems(CurrentDir, ItemClipboard.PasteType.Shortcut, useSystem:=False)
    End Sub
    Private Sub menuEditPasteFBJunction_Click() Handles menuEditPasteFBJunction.Click
        itemClipboard.PasteItems(CurrentDir, ItemClipboard.PasteType.Junction, useSystem:=False)
    End Sub
    Private Sub menuEditPasteSysNormal_Click() Handles menuEditPasteSysNormal.Click
        itemClipboard.PasteItems(CurrentDir, ItemClipboard.PasteType.Normal, useSystem:=True)
    End Sub
    Private Sub menuEditPasteSysHardlink_Click() Handles menuEditPasteSysHardlink.Click
        itemClipboard.PasteItems(CurrentDir, ItemClipboard.PasteType.Hardlink, useSystem:=True)
    End Sub
    Private Sub menuEditPasteSysSymlink_Click() Handles menuEditPasteSysSymlink.Click
        itemClipboard.PasteItems(CurrentDir, ItemClipboard.PasteType.Symlink, useSystem:=True)
    End Sub
    Private Sub menuEditPasteSysShortcut_Click() Handles menuEditPasteSysShortcut.Click
        itemClipboard.PasteItems(CurrentDir, ItemClipboard.PasteType.Shortcut, useSystem:=True)
    End Sub
    Private Sub menuEditPasteSysJunction_Click() Handles menuEditPasteSysJunction.Click
        itemClipboard.PasteItems(CurrentDir, ItemClipboard.PasteType.Junction, useSystem:=True)
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
        Select Case WalkmanLib.GetOS()
            Case WalkmanLib.OS.Windows
                CurrentDir = Environment.GetEnvironmentVariable("UserProfile")
            Case WalkmanLib.OS.Linux
                CurrentDir = Environment.GetEnvironmentVariable("HOME")
            Case WalkmanLib.OS.MacOS
                CurrentDir = Environment.GetEnvironmentVariable("HOME")
        End Select
    End Sub
    Private Sub menuGoRefresh_Click() Handles menuGoRefresh.Click
        LoadFolder()
    End Sub
    Private Sub menuGoStop_Click() Handles menuGoStop.Click
        bwLoadFolder.CancelAsync()
    End Sub

    Private Sub menuToolsSettings_Click() Handles menuToolsSettings.Click
        If Settings.Visible Then
            Settings.BringToFront()
        Else
            Settings.Show(Me)
        End If
    End Sub
    Private Sub menuToolsContextMenu_Click() Handles menuToolsContextMenu.Click
        If ContextMenuConfig.Visible Then
            ContextMenuConfig.BringToFront()
        Else
            ContextMenuConfig.Init() ' the window is Closed, so needs to be re-inited
            ContextMenuConfig.Show(Me)
        End If
    End Sub
    Private Sub menuToolsColumns_Click() Handles menuToolsColumns.Click
        If ColumnConfig.Visible Then
            ColumnConfig.BringToFront()
        Else
            ColumnConfig.Show(Me)
        End If
    End Sub
    Private Sub menuToolsResizeColumns_Click() Handles menuToolsResizeColumns.Click
        For Each column As ColumnHeader In lstCurrent.Columns.Cast(Of ColumnHeader).Where(Function(c) c.Width > 0)
            lstCurrent.AutoResizeColumn(column.Index, ColumnHeaderAutoResizeStyle.HeaderSize)
        Next
    End Sub
    Private Sub menuToolsSaveColumns_Click() Handles menuToolsSaveColumns.Click
        Settings.SaveDefaultColumns()
    End Sub
    Private Sub menuToolsRestoreDefault_Click() Handles menuToolsRestoreDefault.Click
        FolderSettings.DeleteColumnConfig(CurrentDir)
        Helpers.ApplyColumns(Me, Settings.DefaultColumns)
    End Sub

    Private Sub btnGo_Click() Handles btnGo.Click
        Dim newPath As String = Environment.ExpandEnvironmentVariables(cbxURI.Text)
        CurrentDir = newPath

        If CurrentDir = newPath Then
            If cbxURI.Items.Contains(newPath) Then
                cbxURI.Items.Remove(newPath)
            End If
            cbxURI.Items.Insert(0, newPath)
            Settings.SaveSettings()

            cbxURI.Text = newPath
            lstCurrent.Select()
        End If
    End Sub
    Private Sub cbxURI_KeyUp(sender As Object, e As KeyEventArgs) Handles cbxURI.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            btnGo_Click()
        End If
    End Sub
    Private Sub cbxURI_DropDownClosed() Handles cbxURI.DropDownClosed
        cbxURI.Text = DirectCast(cbxURI.SelectedItem, String)
        btnGo_Click()
    End Sub
#End Region

#Region "Other UI Methods"
    Public Sub FileBrowser_Resize() Handles MyBase.Resize
        cbxURI.Size = New Size(Me.Width - 52, cbxURI.Size.Height)

        If Settings.Loaded AndAlso Me.WindowState <> FormWindowState.Maximized AndAlso Settings.WindowRemember Then
            Settings.txtWindowDefaultWidth.Text = Me.Width.ToString()
            Settings.txtWindowDefaultHeight.Text = Me.Height.ToString()
        End If
    End Sub
    Public Sub scMain_SplitterDistanceChanged() Handles scMain.SplitterMoved
        If Settings.Loaded AndAlso Settings.SplitterRemember Then
            Settings.txtSplitterDefaultSize.Text = scMain.SplitterDistance.ToString()
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
        menuFileExecute.Enabled = itemSelected
        menuFileRunAs.Enabled = itemSelected
        menuFileOpenWith.Enabled = itemSelected
        menuFileShowTarget.Enabled = itemSelected
        menuEditCut.Enabled = itemSelected
        menuEditCopy.Enabled = itemSelected
        menuEditSelectAll.Enabled = itemSelected
        menuEditDeselectAll.Enabled = itemSelected
        menuEditInvert.Enabled = itemSelected

        If itemSelected Then itemSelected = (itemClipboard.ItemStore.Count > 0)

        menuEditPasteFBNormal.Enabled = itemSelected
        menuEditPasteFBHardlink.Enabled = itemSelected
        menuEditPasteFBSymlink.Enabled = itemSelected
        menuEditPasteFBShortcut.Enabled = itemSelected
        menuEditPasteFBJunction.Enabled = itemSelected
    End Sub
    Private Sub menuEditPasteSystem_DropDownOpening(sender As Object, e As EventArgs) Handles menuEditPasteSystem.DropDownOpening
        Dim sysClipboardHasItems As Boolean = ((lstCurrent.SelectedItems.Count > 0) OrElse treeViewDirs.SelectedNode IsNot Nothing) _
            AndAlso Clipboard.ContainsFileDropList()

        menuEditPasteSysNormal.Enabled = sysClipboardHasItems
        menuEditPasteSysHardlink.Enabled = sysClipboardHasItems
        menuEditPasteSysSymlink.Enabled = sysClipboardHasItems
        menuEditPasteSysShortcut.Enabled = sysClipboardHasItems
        menuEditPasteSysJunction.Enabled = sysClipboardHasItems
    End Sub

    Private g_forceTree As Boolean = False
    Private g_selectedNode As TreeNode = Nothing
    Private Sub ShowContext(sender As Object, pos As Point)
        Dim paths As String() = GetSelectedPaths(forceTree:=sender Is treeViewDirs, useGlobalNode:=sender Is treeViewDirs)

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
    Private Sub treeViewDirs_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles treeViewDirs.NodeMouseClick
        If e.Button = MouseButtons.Right Then
            g_selectedNode = e.Node
            ShowContext(sender, e.Location)
        End If
    End Sub
    Private Sub lstCurrent_MouseUp(sender As Object, e As MouseEventArgs) Handles lstCurrent.MouseUp
        If e.Button = MouseButtons.Right Then
            ShowContext(sender, e.Location)
        End If
    End Sub

    Private Sub handleKeyUp(sender As Object, e As KeyEventArgs) Handles lstCurrent.KeyUp, treeViewDirs.KeyUp, menuStrip.KeyUp, toolStripURL.KeyUp, cbxURI.KeyUp
        e.Handled = True
        If e.KeyCode = Keys.F4 OrElse e.KeyCode = Keys.F6 Then
            cbxURI.ComboBox.Select()
        ElseIf e.KeyCode = Keys.N AndAlso e.Modifiers = (Keys.Control Or Keys.Shift) Then
            ' have to handle this separately as shift is still being held down
            ShowFile(Operations.CreateFolder(CurrentDir, True))
        ElseIf e.KeyCode = Keys.Delete AndAlso e.Modifiers = Keys.Shift Then
            menuFileRecycle_Click()
        ElseIf e.KeyCode = Keys.Apps OrElse (e.KeyCode = Keys.F10 AndAlso e.Modifiers = Keys.Shift) Then
            g_selectedNode = treeViewDirs.SelectedNode
            ShowContext(sender, New Point(0, 0))
        Else
            e.Handled = False
        End If
    End Sub
    Public Sub ctxMenuL_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ctxMenuL.ItemClicked
        ctxMenuL.Close()
        ctxMenu.RunItem(e.ClickedItem, GetSelectedPaths(forceTree:=g_forceTree, useGlobalNode:=g_forceTree))
    End Sub
    Private Sub handleToolTipChanged(text As String, ex As Exception) Handles winCtxMenu.HelpTextChanged
        If ex Is Nothing AndAlso Not String.IsNullOrEmpty(text) Then
            statusLabel.Text = text
        Else
            statusLabel.Text = Nothing
        End If
    End Sub

    Public Sub RestartAsAdmin() Handles menuFileRelaunch.Click
        WalkmanLib.RunAsAdmin(Path.Combine(Application.StartupPath, Process.GetCurrentProcess.ProcessName & ".exe"), """" & CurrentDir & """")
        Application.Exit()
    End Sub

    Public Sub ErrorParser(ex As Exception)
        If TypeOf ex Is UnauthorizedAccessException AndAlso Not WalkmanLib.IsAdmin() Then
            If MessageBox.Show(ex.Message & Environment.NewLine & Environment.NewLine & "Restart as Admin?",
                               "Access Denied", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                RestartAsAdmin()
            End If
        Else
            WalkmanLib.ErrorDialog(ex)
        End If
    End Sub
#End Region
End Class
