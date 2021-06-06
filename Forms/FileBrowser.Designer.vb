<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FileBrowser
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.treeViewDirs = New System.Windows.Forms.TreeView()
        Me.ctxMenuL = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.lstCurrent = New System.Windows.Forms.ListView()
        Me.colHeadName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadExtension = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadLastModified = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadLastAccessed = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadCreated = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadDiskSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadAttributes = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadLinkTarget = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadSymlinkTarget = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadShortcutTarget = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadUrlTarget = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadHardlinkCount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadStreamCount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadOpensWith = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadDownloadURL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadDownloadReferrer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.colHeadItemPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadItemType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.menuStrip = New System.Windows.Forms.MenuStrip()
        Me.menuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileCreateFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileCreateFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuFileCopyPath = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileRename = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileRecycle = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileCopyTo = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileMoveTo = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuFileProperties = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileLaunch = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileExecute = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileRunAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileOpenWith = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileShowTarget = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuFileRelaunch = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCutFBReplace = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCutFBAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCutSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuEditCutSysReplace = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCutSysAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCutSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuEditCutBothReplace = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCutBothAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCopyFBReplace = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCopyFBAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCopySeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuEditCopySysReplace = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCopySysAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCopySeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuEditCopyBothReplace = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCopyBothAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuEditPasteFileBrowser = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteFBNormal = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteFBHardlink = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteFBSymlink = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteFBShortcut = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteFBJunction = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteSystem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteSysNormal = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteSysHardlink = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteSysSymlink = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteSysShortcut = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteSysJunction = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuEditSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditDeselectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditInvert = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGo = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoBack = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoForward = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoUp = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoRoot = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoHome = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuGoRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoStop = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsContextMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuToolsColumns = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsResizeColumns = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsSaveColumns = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsRestoreDefault = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuToolsUseShell = New System.Windows.Forms.ToolStripMenuItem()
        Me.status = New System.Windows.Forms.StatusStrip()
        Me.statusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.toolStripURL = New System.Windows.Forms.ToolStrip()
        Me.cbxURI = New System.Windows.Forms.ToolStripComboBox()
        Me.btnGo = New System.Windows.Forms.ToolStripButton()
        Me.bwLoadFolder = New System.ComponentModel.BackgroundWorker()
        Me.fswCurrent = New System.IO.FileSystemWatcher()
        Me.clipboardList = New ExpandableList()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.menuStrip.SuspendLayout()
        Me.status.SuspendLayout()
        Me.toolStripURL.SuspendLayout()
        CType(Me.fswCurrent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'treeViewDirs
        '
        Me.treeViewDirs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeViewDirs.FullRowSelect = True
        Me.treeViewDirs.HideSelection = False
        Me.treeViewDirs.LabelEdit = True
        Me.treeViewDirs.Location = New System.Drawing.Point(0, 0)
        Me.treeViewDirs.Name = "treeViewDirs"
        Me.treeViewDirs.ShowNodeToolTips = True
        Me.treeViewDirs.Size = New System.Drawing.Size(220, 425)
        Me.treeViewDirs.TabIndex = 0
        '
        'ctxMenuL
        '
        Me.ctxMenuL.Name = "ctxMenu"
        Me.ctxMenuL.Size = New System.Drawing.Size(61, 4)
        '
        'lstCurrent
        '
        Me.lstCurrent.AllowColumnReorder = True
        Me.lstCurrent.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colHeadName, Me.colHeadExtension, Me.colHeadLastModified, Me.colHeadLastAccessed, Me.colHeadCreated, Me.colHeadSize, Me.colHeadDiskSize, Me.colHeadAttributes, Me.colHeadLinkTarget, Me.colHeadSymlinkTarget, Me.colHeadShortcutTarget, Me.colHeadUrlTarget, Me.colHeadHardlinkCount, Me.colHeadStreamCount, Me.colHeadOpensWith, Me.colHeadDownloadURL, Me.colHeadDownloadReferrer})
        Me.lstCurrent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstCurrent.FullRowSelect = True
        Me.lstCurrent.GridLines = True
        Me.lstCurrent.HideSelection = False
        Me.lstCurrent.LabelEdit = True
        Me.lstCurrent.Location = New System.Drawing.Point(0, 0)
        Me.lstCurrent.Name = "lstCurrent"
        Me.lstCurrent.Size = New System.Drawing.Size(614, 425)
        Me.lstCurrent.TabIndex = 1
        Me.lstCurrent.UseCompatibleStateImageBehavior = False
        Me.lstCurrent.View = System.Windows.Forms.View.Details
        '
        'colHeadName
        '
        Me.colHeadName.Tag = "Name"
        Me.colHeadName.Text = "Name"
        Me.colHeadName.Width = 100
        '
        'colHeadExtension
        '
        Me.colHeadExtension.Tag = "Extension"
        Me.colHeadExtension.Text = "Extension"
        Me.colHeadExtension.Width = 59
        '
        'colHeadLastModified
        '
        Me.colHeadLastModified.Tag = "LastModified"
        Me.colHeadLastModified.Text = "Last Modified"
        Me.colHeadLastModified.Width = 100
        '
        'colHeadLastAccessed
        '
        Me.colHeadLastAccessed.Tag = "LastAccessed"
        Me.colHeadLastAccessed.Text = "Last Accessed"
        Me.colHeadLastAccessed.Width = 100
        '
        'colHeadCreated
        '
        Me.colHeadCreated.Tag = "Created"
        Me.colHeadCreated.Text = "Created"
        Me.colHeadCreated.Width = 100
        '
        'colHeadSize
        '
        Me.colHeadSize.Tag = "Size"
        Me.colHeadSize.Text = "Size"
        Me.colHeadSize.Width = 100
        '
        'colHeadDiskSize
        '
        Me.colHeadDiskSize.Tag = "DiskSize"
        Me.colHeadDiskSize.Text = "Size on Disk"
        Me.colHeadDiskSize.Width = 100
        '
        'colHeadAttributes
        '
        Me.colHeadAttributes.Tag = "Attributes"
        Me.colHeadAttributes.Text = "Attributes"
        Me.colHeadAttributes.Width = 100
        '
        'colHeadLinkTarget
        '
        Me.colHeadLinkTarget.Tag = "LinkTarget"
        Me.colHeadLinkTarget.Text = "Link Target"
        Me.colHeadLinkTarget.Width = 100
        '
        'colHeadSymlinkTarget
        '
        Me.colHeadSymlinkTarget.Tag = "SymlinkTarget"
        Me.colHeadSymlinkTarget.Text = "Symlink Target"
        Me.colHeadSymlinkTarget.Width = 100
        '
        'colHeadShortcutTarget
        '
        Me.colHeadShortcutTarget.Tag = "ShortcutTarget"
        Me.colHeadShortcutTarget.Text = "Shortcut Target"
        Me.colHeadShortcutTarget.Width = 100
        '
        'colHeadUrlTarget
        '
        Me.colHeadUrlTarget.Tag = "UrlTarget"
        Me.colHeadUrlTarget.Text = "URL Target"
        Me.colHeadUrlTarget.Width = 100
        '
        'colHeadHardlinkCount
        '
        Me.colHeadHardlinkCount.Tag = "HardlinkCount"
        Me.colHeadHardlinkCount.Text = "Hardlink Count"
        Me.colHeadHardlinkCount.Width = 100
        '
        'colHeadStreamCount
        '
        Me.colHeadStreamCount.Tag = "StreamCount"
        Me.colHeadStreamCount.Text = "Stream Count"
        Me.colHeadStreamCount.Width = 100
        '
        'colHeadOpensWith
        '
        Me.colHeadOpensWith.Tag = "OpensWith"
        Me.colHeadOpensWith.Text = "Opens With"
        Me.colHeadOpensWith.Width = 100
        '
        'colHeadDownloadURL
        '
        Me.colHeadDownloadURL.Tag = "DownloadURL"
        Me.colHeadDownloadURL.Text = "Download URL"
        Me.colHeadDownloadURL.Width = 100
        '
        'colHeadDownloadReferrer
        '
        Me.colHeadDownloadReferrer.Tag = "DownloadReferrer"
        Me.colHeadDownloadReferrer.Text = "Download Referrer"
        Me.colHeadDownloadReferrer.Width = 120
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.scMain.Location = New System.Drawing.Point(0, 50)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.treeViewDirs)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.lstCurrent)
        Me.scMain.Size = New System.Drawing.Size(838, 425)
        Me.scMain.SplitterDistance = 220
        Me.scMain.TabIndex = 2
        '
        'colHeadItemPath
        '
        Me.colHeadItemPath.Text = "Item Path"
        Me.colHeadItemPath.Width = 293
        '
        'colHeadItemType
        '
        Me.colHeadItemType.Text = "Item Type"
        Me.colHeadItemType.Width = 59
        '
        'menuStrip
        '
        Me.menuStrip.AllowItemReorder = True
        Me.menuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.menuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuFile, Me.menuEdit, Me.menuGo, Me.menuTools})
        Me.menuStrip.Location = New System.Drawing.Point(0, 0)
        Me.menuStrip.Name = "menuStrip"
        Me.menuStrip.ShowItemToolTips = True
        Me.menuStrip.Size = New System.Drawing.Size(838, 24)
        Me.menuStrip.TabIndex = 0
        Me.menuStrip.Text = "MenuStrip1"
        '
        'menuFile
        '
        Me.menuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuFileCreateFile, Me.menuFileCreateFolder, Me.menuFileSeparator1, Me.menuFileCopyPath, Me.menuFileRename, Me.menuFileRecycle, Me.menuFileDelete, Me.menuFileCopyTo, Me.menuFileMoveTo, Me.menuFileSeparator2, Me.menuFileProperties, Me.menuFileLaunch, Me.menuFileExecute, Me.menuFileRunAs, Me.menuFileOpenWith, Me.menuFileShowTarget, Me.menuFileSeparator3, Me.menuFileRelaunch, Me.menuFileExit})
        Me.menuFile.Name = "menuFile"
        Me.menuFile.Size = New System.Drawing.Size(37, 20)
        Me.menuFile.Text = "&File"
        '
        'menuFileCreateFile
        '
        Me.menuFileCreateFile.Image = Global.My.Resources.Resources.NewFile
        Me.menuFileCreateFile.Name = "menuFileCreateFile"
        Me.menuFileCreateFile.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.menuFileCreateFile.Size = New System.Drawing.Size(237, 22)
        Me.menuFileCreateFile.Text = "Create File..."
        Me.menuFileCreateFile.ToolTipText = "Hold Shift to use a Select Dialog. Can create ADSs"
        '
        'menuFileCreateFolder
        '
        Me.menuFileCreateFolder.Image = Global.My.Resources.Resources.NewFolder
        Me.menuFileCreateFolder.Name = "menuFileCreateFolder"
        Me.menuFileCreateFolder.Size = New System.Drawing.Size(237, 22)
        Me.menuFileCreateFolder.Text = "Create Folder... (Ctrl+Shift+N)"
        Me.menuFileCreateFolder.ToolTipText = "Hold Shift to use a Select Dialog"
        '
        'menuFileSeparator1
        '
        Me.menuFileSeparator1.Name = "menuFileSeparator1"
        Me.menuFileSeparator1.Size = New System.Drawing.Size(234, 6)
        '
        'menuFileCopyPath
        '
        Me.menuFileCopyPath.Image = Global.My.Resources.Resources.CopyPath
        Me.menuFileCopyPath.Name = "menuFileCopyPath"
        Me.menuFileCopyPath.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.menuFileCopyPath.Size = New System.Drawing.Size(237, 22)
        Me.menuFileCopyPath.Text = "Copy Path"
        Me.menuFileCopyPath.ToolTipText = "Hold Shift to surround path with quotes"
        '
        'menuFileRename
        '
        Me.menuFileRename.Image = Global.My.Resources.Resources.Rename
        Me.menuFileRename.Name = "menuFileRename"
        Me.menuFileRename.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.menuFileRename.Size = New System.Drawing.Size(237, 22)
        Me.menuFileRename.Text = "Rename"
        '
        'menuFileRecycle
        '
        Me.menuFileRecycle.Image = Global.My.Resources.Resources.Recycle
        Me.menuFileRecycle.Name = "menuFileRecycle"
        Me.menuFileRecycle.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.menuFileRecycle.Size = New System.Drawing.Size(237, 22)
        Me.menuFileRecycle.Text = "Delete to Recycle Bin"
        Me.menuFileRecycle.ToolTipText = "Hold Shift to delete Permanently"
        '
        'menuFileDelete
        '
        Me.menuFileDelete.Image = Global.My.Resources.Resources.Delete
        Me.menuFileDelete.Name = "menuFileDelete"
        Me.menuFileDelete.Size = New System.Drawing.Size(237, 22)
        Me.menuFileDelete.Text = "Delete Permanently (Shift+Del)"
        Me.menuFileDelete.ToolTipText = "Hold Shift to send to Recycle Bin"
        '
        'menuFileCopyTo
        '
        Me.menuFileCopyTo.Image = Global.My.Resources.Resources.CopyTo
        Me.menuFileCopyTo.Name = "menuFileCopyTo"
        Me.menuFileCopyTo.Size = New System.Drawing.Size(237, 22)
        Me.menuFileCopyTo.Text = "Copy To..."
        Me.menuFileCopyTo.ToolTipText = "Hold Shift to enter path in a textbox instead of a Select Dialog"
        '
        'menuFileMoveTo
        '
        Me.menuFileMoveTo.Image = Global.My.Resources.Resources.MoveTo
        Me.menuFileMoveTo.Name = "menuFileMoveTo"
        Me.menuFileMoveTo.Size = New System.Drawing.Size(237, 22)
        Me.menuFileMoveTo.Text = "Move To..."
        Me.menuFileMoveTo.ToolTipText = "Hold Shift to enter path in a textbox instead of a Select Dialog"
        '
        'menuFileSeparator2
        '
        Me.menuFileSeparator2.Name = "menuFileSeparator2"
        Me.menuFileSeparator2.Size = New System.Drawing.Size(234, 6)
        '
        'menuFileProperties
        '
        Me.menuFileProperties.Image = Global.My.Resources.Resources.Properties
        Me.menuFileProperties.Name = "menuFileProperties"
        Me.menuFileProperties.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Enter), System.Windows.Forms.Keys)
        Me.menuFileProperties.Size = New System.Drawing.Size(237, 22)
        Me.menuFileProperties.Text = "Properties"
        Me.menuFileProperties.ToolTipText = "Open the Windows Properties window on the currently selected items"
        '
        'menuFileLaunch
        '
        Me.menuFileLaunch.Image = Global.My.Resources.Resources.Run
        Me.menuFileLaunch.Name = "menuFileLaunch"
        Me.menuFileLaunch.Size = New System.Drawing.Size(237, 22)
        Me.menuFileLaunch.Text = "Launch"
        Me.menuFileLaunch.ToolTipText = "Launch the currently selected items with the default assigned program"
        '
        'menuFileExecute
        '
        Me.menuFileExecute.Image = Global.My.Resources.Resources.Execute
        Me.menuFileExecute.Name = "menuFileExecute"
        Me.menuFileExecute.Size = New System.Drawing.Size(237, 22)
        Me.menuFileExecute.Text = "Execute"
        Me.menuFileExecute.ToolTipText = "Execute the currently selected items. This can be used to run programs without th" &
    "e default .exe or .com extension, or in an ADS"
        '
        'menuFileRunAs
        '
        Me.menuFileRunAs.Image = Global.My.Resources.Resources.RunAsAdmin
        Me.menuFileRunAs.Name = "menuFileRunAs"
        Me.menuFileRunAs.Size = New System.Drawing.Size(237, 22)
        Me.menuFileRunAs.Text = "Open As Admin"
        Me.menuFileRunAs.ToolTipText = "Attempts to open the currently selected items as Admin. If these are not programs" &
    ", they will be opened with the assigned program"
        '
        'menuFileOpenWith
        '
        Me.menuFileOpenWith.Image = Global.My.Resources.Resources.OpenWith
        Me.menuFileOpenWith.Name = "menuFileOpenWith"
        Me.menuFileOpenWith.Size = New System.Drawing.Size(237, 22)
        Me.menuFileOpenWith.Text = "Open With..."
        Me.menuFileOpenWith.ToolTipText = "Launches the Windows Open With dialog on the currently selected items"
        '
        'menuFileShowTarget
        '
        Me.menuFileShowTarget.Image = Global.My.Resources.Resources.OpenLocation
        Me.menuFileShowTarget.Name = "menuFileShowTarget"
        Me.menuFileShowTarget.Size = New System.Drawing.Size(237, 22)
        Me.menuFileShowTarget.Text = "Show Target"
        Me.menuFileShowTarget.ToolTipText = "Goes to the folder containing the target of the currently selected Symlink or Sho" &
    "rtcut"
        '
        'menuFileSeparator3
        '
        Me.menuFileSeparator3.Name = "menuFileSeparator3"
        Me.menuFileSeparator3.Size = New System.Drawing.Size(234, 6)
        '
        'menuFileRelaunch
        '
        Me.menuFileRelaunch.Image = Global.My.Resources.Resources.Admin
        Me.menuFileRelaunch.Name = "menuFileRelaunch"
        Me.menuFileRelaunch.Size = New System.Drawing.Size(237, 22)
        Me.menuFileRelaunch.Text = "Relaunch as Admin"
        Me.menuFileRelaunch.ToolTipText = "Relaunches FileBrowser in the current folder with Admin permissions"
        '
        'menuFileExit
        '
        Me.menuFileExit.Image = Global.My.Resources.Resources.Quit
        Me.menuFileExit.Name = "menuFileExit"
        Me.menuFileExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.menuFileExit.Size = New System.Drawing.Size(237, 22)
        Me.menuFileExit.Text = "Exit"
        '
        'menuEdit
        '
        Me.menuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuEditCut, Me.menuEditCopy, Me.menuEditSeparator1, Me.menuEditPasteFileBrowser, Me.menuEditPasteSystem, Me.menuEditSeparator2, Me.menuEditSelectAll, Me.menuEditDeselectAll, Me.menuEditInvert})
        Me.menuEdit.Name = "menuEdit"
        Me.menuEdit.Size = New System.Drawing.Size(39, 20)
        Me.menuEdit.Text = "&Edit"
        '
        'menuEditCut
        '
        Me.menuEditCut.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuEditCutFBReplace, Me.menuEditCutFBAdd, Me.menuEditCutSeparator1, Me.menuEditCutSysReplace, Me.menuEditCutSysAdd, Me.menuEditCutSeparator2, Me.menuEditCutBothReplace, Me.menuEditCutBothAdd})
        Me.menuEditCut.Image = Global.My.Resources.Resources.Cut
        Me.menuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuEditCut.Name = "menuEditCut"
        Me.menuEditCut.Size = New System.Drawing.Size(220, 22)
        Me.menuEditCut.Text = "Cu&t"
        '
        'menuEditCutFBReplace
        '
        Me.menuEditCutFBReplace.Name = "menuEditCutFBReplace"
        Me.menuEditCutFBReplace.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.menuEditCutFBReplace.Size = New System.Drawing.Size(301, 22)
        Me.menuEditCutFBReplace.Text = "Replace FileBrowser Clipboard"
        '
        'menuEditCutFBAdd
        '
        Me.menuEditCutFBAdd.Name = "menuEditCutFBAdd"
        Me.menuEditCutFBAdd.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.menuEditCutFBAdd.Size = New System.Drawing.Size(301, 22)
        Me.menuEditCutFBAdd.Text = "Add to FileBrowser Clipboard"
        '
        'menuEditCutSeparator1
        '
        Me.menuEditCutSeparator1.Name = "menuEditCutSeparator1"
        Me.menuEditCutSeparator1.Size = New System.Drawing.Size(298, 6)
        '
        'menuEditCutSysReplace
        '
        Me.menuEditCutSysReplace.Name = "menuEditCutSysReplace"
        Me.menuEditCutSysReplace.Size = New System.Drawing.Size(301, 22)
        Me.menuEditCutSysReplace.Text = "Replace System Clipboard"
        '
        'menuEditCutSysAdd
        '
        Me.menuEditCutSysAdd.Name = "menuEditCutSysAdd"
        Me.menuEditCutSysAdd.Size = New System.Drawing.Size(301, 22)
        Me.menuEditCutSysAdd.Text = "Add to System Clipboard"
        '
        'menuEditCutSeparator2
        '
        Me.menuEditCutSeparator2.Name = "menuEditCutSeparator2"
        Me.menuEditCutSeparator2.Size = New System.Drawing.Size(298, 6)
        '
        'menuEditCutBothReplace
        '
        Me.menuEditCutBothReplace.Name = "menuEditCutBothReplace"
        Me.menuEditCutBothReplace.Size = New System.Drawing.Size(301, 22)
        Me.menuEditCutBothReplace.Text = "Replace Both Clipboards"
        '
        'menuEditCutBothAdd
        '
        Me.menuEditCutBothAdd.Name = "menuEditCutBothAdd"
        Me.menuEditCutBothAdd.Size = New System.Drawing.Size(301, 22)
        Me.menuEditCutBothAdd.Text = "Add to Both Clipboards"
        '
        'menuEditCopy
        '
        Me.menuEditCopy.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuEditCopyFBReplace, Me.menuEditCopyFBAdd, Me.menuEditCopySeparator1, Me.menuEditCopySysReplace, Me.menuEditCopySysAdd, Me.menuEditCopySeparator2, Me.menuEditCopyBothReplace, Me.menuEditCopyBothAdd})
        Me.menuEditCopy.Image = Global.My.Resources.Resources.Copy
        Me.menuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuEditCopy.Name = "menuEditCopy"
        Me.menuEditCopy.Size = New System.Drawing.Size(220, 22)
        Me.menuEditCopy.Text = "&Copy"
        '
        'menuEditCopyFBReplace
        '
        Me.menuEditCopyFBReplace.Name = "menuEditCopyFBReplace"
        Me.menuEditCopyFBReplace.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.menuEditCopyFBReplace.Size = New System.Drawing.Size(302, 22)
        Me.menuEditCopyFBReplace.Text = "Replace FileBrowser Clipboard"
        '
        'menuEditCopyFBAdd
        '
        Me.menuEditCopyFBAdd.Name = "menuEditCopyFBAdd"
        Me.menuEditCopyFBAdd.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.menuEditCopyFBAdd.Size = New System.Drawing.Size(302, 22)
        Me.menuEditCopyFBAdd.Text = "Add to FileBrowser Clipboard"
        '
        'menuEditCopySeparator1
        '
        Me.menuEditCopySeparator1.Name = "menuEditCopySeparator1"
        Me.menuEditCopySeparator1.Size = New System.Drawing.Size(299, 6)
        '
        'menuEditCopySysReplace
        '
        Me.menuEditCopySysReplace.Name = "menuEditCopySysReplace"
        Me.menuEditCopySysReplace.Size = New System.Drawing.Size(302, 22)
        Me.menuEditCopySysReplace.Text = "Replace System Clipboard"
        '
        'menuEditCopySysAdd
        '
        Me.menuEditCopySysAdd.Name = "menuEditCopySysAdd"
        Me.menuEditCopySysAdd.Size = New System.Drawing.Size(302, 22)
        Me.menuEditCopySysAdd.Text = "Add to System Clipboard"
        '
        'menuEditCopySeparator2
        '
        Me.menuEditCopySeparator2.Name = "menuEditCopySeparator2"
        Me.menuEditCopySeparator2.Size = New System.Drawing.Size(299, 6)
        '
        'menuEditCopyBothReplace
        '
        Me.menuEditCopyBothReplace.Name = "menuEditCopyBothReplace"
        Me.menuEditCopyBothReplace.Size = New System.Drawing.Size(302, 22)
        Me.menuEditCopyBothReplace.Text = "Replace Both Clipboards"
        '
        'menuEditCopyBothAdd
        '
        Me.menuEditCopyBothAdd.Name = "menuEditCopyBothAdd"
        Me.menuEditCopyBothAdd.Size = New System.Drawing.Size(302, 22)
        Me.menuEditCopyBothAdd.Text = "Add to Both Clipboards"
        '
        'menuEditSeparator1
        '
        Me.menuEditSeparator1.Name = "menuEditSeparator1"
        Me.menuEditSeparator1.Size = New System.Drawing.Size(217, 6)
        '
        'menuEditPasteFileBrowser
        '
        Me.menuEditPasteFileBrowser.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuEditPasteFBNormal, Me.menuEditPasteFBHardlink, Me.menuEditPasteFBSymlink, Me.menuEditPasteFBShortcut, Me.menuEditPasteFBJunction})
        Me.menuEditPasteFileBrowser.Name = "menuEditPasteFileBrowser"
        Me.menuEditPasteFileBrowser.Size = New System.Drawing.Size(220, 22)
        Me.menuEditPasteFileBrowser.Text = "Paste from &FileBrowser"
        Me.menuEditPasteFileBrowser.ToolTipText = "Paste items in FileBrowser's Clipboard"
        '
        'menuEditPasteFBNormal
        '
        Me.menuEditPasteFBNormal.Image = Global.My.Resources.Resources.Paste
        Me.menuEditPasteFBNormal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuEditPasteFBNormal.Name = "menuEditPasteFBNormal"
        Me.menuEditPasteFBNormal.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.menuEditPasteFBNormal.Size = New System.Drawing.Size(164, 22)
        Me.menuEditPasteFBNormal.Text = "&Paste"
        Me.menuEditPasteFBNormal.ToolTipText = "Paste items in FileBrowser's Clipboard"
        '
        'menuEditPasteFBHardlink
        '
        Me.menuEditPasteFBHardlink.Image = Global.My.Resources.Resources.PasteHardlink
        Me.menuEditPasteFBHardlink.Name = "menuEditPasteFBHardlink"
        Me.menuEditPasteFBHardlink.Size = New System.Drawing.Size(164, 22)
        Me.menuEditPasteFBHardlink.Text = "Paste as &Hardlink"
        '
        'menuEditPasteFBSymlink
        '
        Me.menuEditPasteFBSymlink.Image = Global.My.Resources.Resources.PasteSymlink
        Me.menuEditPasteFBSymlink.Name = "menuEditPasteFBSymlink"
        Me.menuEditPasteFBSymlink.Size = New System.Drawing.Size(164, 22)
        Me.menuEditPasteFBSymlink.Text = "Paste as Sym&link"
        '
        'menuEditPasteFBShortcut
        '
        Me.menuEditPasteFBShortcut.Image = Global.My.Resources.Resources.PasteShortcut
        Me.menuEditPasteFBShortcut.Name = "menuEditPasteFBShortcut"
        Me.menuEditPasteFBShortcut.Size = New System.Drawing.Size(164, 22)
        Me.menuEditPasteFBShortcut.Text = "Paste as &Shortcut"
        '
        'menuEditPasteFBJunction
        '
        Me.menuEditPasteFBJunction.Image = Global.My.Resources.Resources.PasteJunction
        Me.menuEditPasteFBJunction.Name = "menuEditPasteFBJunction"
        Me.menuEditPasteFBJunction.Size = New System.Drawing.Size(164, 22)
        Me.menuEditPasteFBJunction.Text = "Paste as &Junction"
        '
        'menuEditPasteSystem
        '
        Me.menuEditPasteSystem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuEditPasteSysNormal, Me.menuEditPasteSysHardlink, Me.menuEditPasteSysSymlink, Me.menuEditPasteSysShortcut, Me.menuEditPasteSysJunction})
        Me.menuEditPasteSystem.Name = "menuEditPasteSystem"
        Me.menuEditPasteSystem.Size = New System.Drawing.Size(220, 22)
        Me.menuEditPasteSystem.Text = "Paste from &System"
        Me.menuEditPasteSystem.ToolTipText = "Paste items in System Clipboard"
        '
        'menuEditPasteSysNormal
        '
        Me.menuEditPasteSysNormal.Image = Global.My.Resources.Resources.Paste
        Me.menuEditPasteSysNormal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuEditPasteSysNormal.Name = "menuEditPasteSysNormal"
        Me.menuEditPasteSysNormal.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.menuEditPasteSysNormal.Size = New System.Drawing.Size(175, 22)
        Me.menuEditPasteSysNormal.Text = "&Paste"
        Me.menuEditPasteSysNormal.ToolTipText = "Paste items in System Clipboard"
        '
        'menuEditPasteSysHardlink
        '
        Me.menuEditPasteSysHardlink.Image = Global.My.Resources.Resources.PasteHardlink
        Me.menuEditPasteSysHardlink.Name = "menuEditPasteSysHardlink"
        Me.menuEditPasteSysHardlink.Size = New System.Drawing.Size(175, 22)
        Me.menuEditPasteSysHardlink.Text = "Paste as &Hardlink"
        '
        'menuEditPasteSysSymlink
        '
        Me.menuEditPasteSysSymlink.Image = Global.My.Resources.Resources.PasteSymlink
        Me.menuEditPasteSysSymlink.Name = "menuEditPasteSysSymlink"
        Me.menuEditPasteSysSymlink.Size = New System.Drawing.Size(175, 22)
        Me.menuEditPasteSysSymlink.Text = "Paste as Sym&link"
        '
        'menuEditPasteSysShortcut
        '
        Me.menuEditPasteSysShortcut.Image = Global.My.Resources.Resources.PasteShortcut
        Me.menuEditPasteSysShortcut.Name = "menuEditPasteSysShortcut"
        Me.menuEditPasteSysShortcut.Size = New System.Drawing.Size(175, 22)
        Me.menuEditPasteSysShortcut.Text = "Paste as &Shortcut"
        '
        'menuEditPasteSysJunction
        '
        Me.menuEditPasteSysJunction.Image = Global.My.Resources.Resources.PasteJunction
        Me.menuEditPasteSysJunction.Name = "menuEditPasteSysJunction"
        Me.menuEditPasteSysJunction.Size = New System.Drawing.Size(175, 22)
        Me.menuEditPasteSysJunction.Text = "Paste as &Junction"
        '
        'menuEditSeparator2
        '
        Me.menuEditSeparator2.Name = "menuEditSeparator2"
        Me.menuEditSeparator2.Size = New System.Drawing.Size(217, 6)
        '
        'menuEditSelectAll
        '
        Me.menuEditSelectAll.Image = Global.My.Resources.Resources.SelectAll
        Me.menuEditSelectAll.Name = "menuEditSelectAll"
        Me.menuEditSelectAll.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.menuEditSelectAll.Size = New System.Drawing.Size(220, 22)
        Me.menuEditSelectAll.Text = "Select &All"
        '
        'menuEditDeselectAll
        '
        Me.menuEditDeselectAll.Image = Global.My.Resources.Resources.SelectNone
        Me.menuEditDeselectAll.Name = "menuEditDeselectAll"
        Me.menuEditDeselectAll.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.menuEditDeselectAll.Size = New System.Drawing.Size(220, 22)
        Me.menuEditDeselectAll.Text = "&Deselect All"
        '
        'menuEditInvert
        '
        Me.menuEditInvert.Image = Global.My.Resources.Resources.InvertSelection
        Me.menuEditInvert.Name = "menuEditInvert"
        Me.menuEditInvert.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.menuEditInvert.Size = New System.Drawing.Size(220, 22)
        Me.menuEditInvert.Text = "&Invert Selection"
        '
        'menuGo
        '
        Me.menuGo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuGoBack, Me.menuGoForward, Me.menuGoUp, Me.menuGoRoot, Me.menuGoHome, Me.menuGoSeparator1, Me.menuGoRefresh, Me.menuGoStop})
        Me.menuGo.Name = "menuGo"
        Me.menuGo.Size = New System.Drawing.Size(34, 20)
        Me.menuGo.Text = "Go"
        '
        'menuGoBack
        '
        Me.menuGoBack.Image = Global.My.Resources.Resources.Back
        Me.menuGoBack.Name = "menuGoBack"
        Me.menuGoBack.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Left), System.Windows.Forms.Keys)
        Me.menuGoBack.Size = New System.Drawing.Size(175, 22)
        Me.menuGoBack.Text = "Back"
        '
        'menuGoForward
        '
        Me.menuGoForward.Image = Global.My.Resources.Resources.Forward
        Me.menuGoForward.Name = "menuGoForward"
        Me.menuGoForward.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Right), System.Windows.Forms.Keys)
        Me.menuGoForward.Size = New System.Drawing.Size(175, 22)
        Me.menuGoForward.Text = "Forward"
        '
        'menuGoUp
        '
        Me.menuGoUp.Image = Global.My.Resources.Resources.Up
        Me.menuGoUp.Name = "menuGoUp"
        Me.menuGoUp.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Up), System.Windows.Forms.Keys)
        Me.menuGoUp.Size = New System.Drawing.Size(175, 22)
        Me.menuGoUp.Text = "Up"
        Me.menuGoUp.ToolTipText = "Go to parent folder"
        '
        'menuGoRoot
        '
        Me.menuGoRoot.Image = Global.My.Resources.Resources.Root
        Me.menuGoRoot.Name = "menuGoRoot"
        Me.menuGoRoot.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.menuGoRoot.Size = New System.Drawing.Size(175, 22)
        Me.menuGoRoot.Text = "Root"
        Me.menuGoRoot.ToolTipText = "Go to current Drive Root"
        '
        'menuGoHome
        '
        Me.menuGoHome.Image = Global.My.Resources.Resources.Home
        Me.menuGoHome.Name = "menuGoHome"
        Me.menuGoHome.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.menuGoHome.Size = New System.Drawing.Size(175, 22)
        Me.menuGoHome.Text = "Home"
        Me.menuGoHome.ToolTipText = "Go to User's home folder"
        '
        'menuGoSeparator1
        '
        Me.menuGoSeparator1.Name = "menuGoSeparator1"
        Me.menuGoSeparator1.Size = New System.Drawing.Size(172, 6)
        '
        'menuGoRefresh
        '
        Me.menuGoRefresh.Image = Global.My.Resources.Resources.Refresh
        Me.menuGoRefresh.Name = "menuGoRefresh"
        Me.menuGoRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.menuGoRefresh.Size = New System.Drawing.Size(175, 22)
        Me.menuGoRefresh.Text = "Refresh"
        '
        'menuGoStop
        '
        Me.menuGoStop.Image = Global.My.Resources.Resources.Cancel
        Me.menuGoStop.Name = "menuGoStop"
        Me.menuGoStop.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.menuGoStop.Size = New System.Drawing.Size(175, 22)
        Me.menuGoStop.Text = "Stop"
        '
        'menuTools
        '
        Me.menuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuToolsSettings, Me.menuToolsContextMenu, Me.menuToolsSeparator1, Me.menuToolsColumns, Me.menuToolsResizeColumns, Me.menuToolsSaveColumns, Me.menuToolsRestoreDefault, Me.menuToolsSeparator2, Me.menuToolsUseShell})
        Me.menuTools.Name = "menuTools"
        Me.menuTools.Size = New System.Drawing.Size(46, 20)
        Me.menuTools.Text = "&Tools"
        '
        'menuToolsSettings
        '
        Me.menuToolsSettings.Image = Global.My.Resources.Resources.Settings
        Me.menuToolsSettings.Name = "menuToolsSettings"
        Me.menuToolsSettings.Size = New System.Drawing.Size(247, 22)
        Me.menuToolsSettings.Text = "Settings"
        Me.menuToolsSettings.ToolTipText = "Open or Show FileBrowser Settings dialog"
        '
        'menuToolsContextMenu
        '
        Me.menuToolsContextMenu.Image = Global.My.Resources.Resources.ContextConfig
        Me.menuToolsContextMenu.Name = "menuToolsContextMenu"
        Me.menuToolsContextMenu.Size = New System.Drawing.Size(247, 22)
        Me.menuToolsContextMenu.Text = "Context Menu Configuration"
        Me.menuToolsContextMenu.ToolTipText = "Open or Show Context Menu setup dialog"
        '
        'menuToolsSeparator1
        '
        Me.menuToolsSeparator1.Name = "menuToolsSeparator1"
        Me.menuToolsSeparator1.Size = New System.Drawing.Size(244, 6)
        '
        'menuToolsColumns
        '
        Me.menuToolsColumns.Image = Global.My.Resources.Resources.ColumnConfig
        Me.menuToolsColumns.Name = "menuToolsColumns"
        Me.menuToolsColumns.Size = New System.Drawing.Size(247, 22)
        Me.menuToolsColumns.Text = "Columns Configuration"
        Me.menuToolsColumns.ToolTipText = "Open or Show Column Hide/Show dialog"
        '
        'menuToolsResizeColumns
        '
        Me.menuToolsResizeColumns.Image = Global.My.Resources.Resources.ResizeColumns
        Me.menuToolsResizeColumns.Name = "menuToolsResizeColumns"
        Me.menuToolsResizeColumns.Size = New System.Drawing.Size(247, 22)
        Me.menuToolsResizeColumns.Text = "Resize All Columns"
        Me.menuToolsResizeColumns.ToolTipText = "Resizes all columns that are visible. Hidden columns (Width = 0) stay hidden"
        '
        'menuToolsSaveColumns
        '
        Me.menuToolsSaveColumns.Image = Global.My.Resources.Resources.Save
        Me.menuToolsSaveColumns.Name = "menuToolsSaveColumns"
        Me.menuToolsSaveColumns.Size = New System.Drawing.Size(247, 22)
        Me.menuToolsSaveColumns.Text = "Save Current Columns as Default"
        Me.menuToolsSaveColumns.ToolTipText = "Save the current view's columns as the default configuration"
        '
        'menuToolsRestoreDefault
        '
        Me.menuToolsRestoreDefault.Image = Global.My.Resources.Resources.RestoreColumns
        Me.menuToolsRestoreDefault.Name = "menuToolsRestoreDefault"
        Me.menuToolsRestoreDefault.Size = New System.Drawing.Size(247, 22)
        Me.menuToolsRestoreDefault.Text = "Restore Saved Columns"
        Me.menuToolsRestoreDefault.ToolTipText = "Restore the saved column configuration to the current view"
        '
        'menuToolsSeparator2
        '
        Me.menuToolsSeparator2.Name = "menuToolsSeparator2"
        Me.menuToolsSeparator2.Size = New System.Drawing.Size(244, 6)
        '
        'menuToolsUseShell
        '
        Me.menuToolsUseShell.CheckOnClick = True
        Me.menuToolsUseShell.Name = "menuToolsUseShell"
        Me.menuToolsUseShell.Size = New System.Drawing.Size(247, 22)
        Me.menuToolsUseShell.Text = "Use Windows Shell"
        Me.menuToolsUseShell.ToolTipText = "Enable this to use Windows' built-in copy/move functions. Disable to use custom F" &
    "ileBrowser methods"
        '
        'status
        '
        Me.status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statusLabel})
        Me.status.Location = New System.Drawing.Point(0, 476)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(838, 22)
        Me.status.TabIndex = 3
        '
        'statusLabel
        '
        Me.statusLabel.AutoToolTip = True
        Me.statusLabel.Name = "statusLabel"
        Me.statusLabel.Size = New System.Drawing.Size(0, 17)
        '
        'toolStripURL
        '
        Me.toolStripURL.AllowItemReorder = True
        Me.toolStripURL.CanOverflow = False
        Me.toolStripURL.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cbxURI, Me.btnGo})
        Me.toolStripURL.Location = New System.Drawing.Point(0, 24)
        Me.toolStripURL.Name = "toolStripURL"
        Me.toolStripURL.Size = New System.Drawing.Size(838, 25)
        Me.toolStripURL.TabIndex = 1
        '
        'cbxURI
        '
        Me.cbxURI.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxURI.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me.cbxURI.AutoToolTip = True
        Me.cbxURI.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.cbxURI.Name = "cbxURI"
        Me.cbxURI.Size = New System.Drawing.Size(800, 25)
        '
        'btnGo
        '
        Me.btnGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnGo.Image = Global.My.Resources.Resources.Go
        Me.btnGo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(23, 22)
        Me.btnGo.Text = "Go"
        '
        'bwLoadFolder
        '
        Me.bwLoadFolder.WorkerSupportsCancellation = True
        '
        'fswCurrent
        '
        Me.fswCurrent.EnableRaisingEvents = True
        Me.fswCurrent.SynchronizingObject = Me
        '
        'clipboardList
        '
        Me.clipboardList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clipboardList.ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colHeadItemPath, Me.colHeadItemType})
        Me.clipboardList.ListView.FullRowSelect = True
        Me.clipboardList.ListView.GridLines = True
        Me.clipboardList.ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.clipboardList.ListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.clipboardList.LabelPrefix = "Clipboard Items: "
        Me.clipboardList.Location = New System.Drawing.Point(439, -1)
        Me.clipboardList.Name = "clipboardList"
        Me.clipboardList.Size = New System.Drawing.Size(400, 26)
        Me.clipboardList.TabIndex = 1
        '
        'FileBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(838, 498)
        Me.Controls.Add(Me.clipboardList)
        Me.Controls.Add(Me.toolStripURL)
        Me.Controls.Add(Me.status)
        Me.Controls.Add(Me.menuStrip)
        Me.Controls.Add(Me.scMain)
        Me.MainMenuStrip = Me.menuStrip
        Me.Name = "FileBrowser"
        Me.Text = "FileBrowser"
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.menuStrip.ResumeLayout(False)
        Me.menuStrip.PerformLayout()
        Me.status.ResumeLayout(False)
        Me.status.PerformLayout()
        Me.toolStripURL.ResumeLayout(False)
        Me.toolStripURL.PerformLayout()
        CType(Me.fswCurrent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents menuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents menuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileCreateFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileCreateFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuFileCopyPath As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileRename As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileRecycle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileCopyTo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileMoveTo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuFileProperties As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileLaunch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileExecute As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileRunAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileOpenWith As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileShowTarget As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuFileRelaunch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCutFBReplace As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCutFBAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCutSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuEditCutSysReplace As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCutSysAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCutSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuEditCutBothReplace As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCutBothAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCopyFBReplace As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCopyFBAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCopySeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuEditCopySysReplace As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCopySysAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCopySeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuEditCopyBothReplace As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCopyBothAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuEditPasteFileBrowser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteFBNormal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteFBHardlink As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteFBSymlink As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteFBShortcut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteFBJunction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteSystem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteSysNormal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteSysHardlink As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteSysSymlink As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteSysShortcut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteSysJunction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuEditSelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditDeselectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditInvert As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoBack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoForward As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoUp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoRoot As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoHome As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuGoRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoStop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsContextMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsColumns As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsSaveColumns As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsRestoreDefault As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuToolsResizeColumns As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsUseShell As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents clipboardList As ExpandableList
    Friend WithEvents colHeadItemPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadItemType As System.Windows.Forms.ColumnHeader
    Friend WithEvents toolStripURL As System.Windows.Forms.ToolStrip
    Friend WithEvents cbxURI As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnGo As System.Windows.Forms.ToolStripButton
    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
    Friend WithEvents treeViewDirs As System.Windows.Forms.TreeView
    Friend WithEvents lstCurrent As System.Windows.Forms.ListView
    Friend WithEvents colHeadName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadExtension As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadLastModified As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadLastAccessed As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadCreated As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadDiskSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadSymlinkTarget As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadShortcutTarget As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadUrlTarget As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadLinkTarget As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadHardlinkCount As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadStreamCount As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadOpensWith As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadAttributes As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadDownloadURL As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadDownloadReferrer As System.Windows.Forms.ColumnHeader
    Friend WithEvents status As System.Windows.Forms.StatusStrip
    Friend WithEvents statusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ctxMenuL As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents bwLoadFolder As System.ComponentModel.BackgroundWorker
    Friend WithEvents fswCurrent As System.IO.FileSystemWatcher
End Class
