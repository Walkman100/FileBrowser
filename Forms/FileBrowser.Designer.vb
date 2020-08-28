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
        Me.menuStrip = New System.Windows.Forms.MenuStrip()
        Me.menuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuFileSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuFileSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuEditPasteAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuGo = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuToolsUseShell = New System.Windows.Forms.ToolStripMenuItem()
        Me.status = New System.Windows.Forms.StatusStrip()
        Me.statusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.toolStripURL = New System.Windows.Forms.ToolStrip()
        Me.cbxURI = New System.Windows.Forms.ToolStripComboBox()
        Me.menuFileExecute = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnGo = New System.Windows.Forms.ToolStripButton()
        Me.menuFileCreate = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileRename = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileRecycle = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileCopyTo = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileMoveTo = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileProperties = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileLaunch = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileShowTarget = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteAsHardlink = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteAsSymlink = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteAsShortcut = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditPasteAsJunction = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditDeselectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuEditInvert = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoBack = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoForward = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoUp = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoRoot = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoHome = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuGoStop = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsContextMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsColumns = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuToolsResizeColumns = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.menuStrip.SuspendLayout()
        Me.status.SuspendLayout()
        Me.toolStripURL.SuspendLayout()
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
        Me.colHeadName.Text = "Name"
        Me.colHeadName.Width = 100
        '
        'colHeadExtension
        '
        Me.colHeadExtension.Text = "Extension"
        Me.colHeadExtension.Width = 59
        '
        'colHeadLastModified
        '
        Me.colHeadLastModified.Text = "Last Modified"
        Me.colHeadLastModified.Width = 100
        '
        'colHeadLastAccessed
        '
        Me.colHeadLastAccessed.Text = "Last Accessed"
        Me.colHeadLastAccessed.Width = 100
        '
        'colHeadCreated
        '
        Me.colHeadCreated.Text = "Created"
        Me.colHeadCreated.Width = 100
        '
        'colHeadSize
        '
        Me.colHeadSize.Text = "Size"
        Me.colHeadSize.Width = 100
        '
        'colHeadDiskSize
        '
        Me.colHeadDiskSize.Text = "Size on Disk"
        Me.colHeadDiskSize.Width = 100
        '
        'colHeadAttributes
        '
        Me.colHeadAttributes.Text = "Attributes"
        Me.colHeadAttributes.Width = 100
        '
        'colHeadLinkTarget
        '
        Me.colHeadLinkTarget.Text = "Link Target"
        Me.colHeadLinkTarget.Width = 100
        '
        'colHeadSymlinkTarget
        '
        Me.colHeadSymlinkTarget.Text = "Symlink Target"
        Me.colHeadSymlinkTarget.Width = 100
        '
        'colHeadShortcutTarget
        '
        Me.colHeadShortcutTarget.Text = "Shortcut Target"
        Me.colHeadShortcutTarget.Width = 100
        '
        'colHeadUrlTarget
        '
        Me.colHeadUrlTarget.Text = "URL Target"
        Me.colHeadUrlTarget.Width = 100
        '
        'colHeadHardlinkCount
        '
        Me.colHeadHardlinkCount.Text = "Hardlink Count"
        Me.colHeadHardlinkCount.Width = 100
        '
        'colHeadStreamCount
        '
        Me.colHeadStreamCount.Text = "Stream Count"
        Me.colHeadStreamCount.Width = 100
        '
        'colHeadOpensWith
        '
        Me.colHeadOpensWith.Text = "Opens With"
        Me.colHeadOpensWith.Width = 100
        '
        'colHeadDownloadURL
        '
        Me.colHeadDownloadURL.Text = "Download URL"
        Me.colHeadDownloadURL.Width = 100
        '
        'colHeadDownloadReferrer
        '
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
        Me.menuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuFileCreate, Me.menuFileSeparator1, Me.menuFileRename, Me.menuFileRecycle, Me.menuFileDelete, Me.menuFileCopyTo, Me.menuFileMoveTo, Me.menuFileSeparator2, Me.menuFileProperties, Me.menuFileLaunch, Me.menuFileShowTarget, Me.menuFileSeparator3, Me.menuFileExit})
        Me.menuFile.Name = "menuFile"
        Me.menuFile.Size = New System.Drawing.Size(37, 20)
        Me.menuFile.Text = "&File"
        '
        'menuFileSeparator1
        '
        Me.menuFileSeparator1.Name = "menuFileSeparator1"
        Me.menuFileSeparator1.Size = New System.Drawing.Size(234, 6)
        '
        'menuFileSeparator2
        '
        Me.menuFileSeparator2.Name = "menuFileSeparator2"
        Me.menuFileSeparator2.Size = New System.Drawing.Size(234, 6)
        '
        'menuFileSeparator3
        '
        Me.menuFileSeparator3.Name = "menuFileSeparator3"
        Me.menuFileSeparator3.Size = New System.Drawing.Size(234, 6)
        '
        'menuEdit
        '
        Me.menuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuEditCut, Me.menuEditCopy, Me.menuEditPaste, Me.menuEditSeparator1, Me.menuEditPasteAs, Me.menuEditSeparator2, Me.menuEditSelectAll, Me.menuEditDeselectAll, Me.menuEditInvert})
        Me.menuEdit.Name = "menuEdit"
        Me.menuEdit.Size = New System.Drawing.Size(39, 20)
        Me.menuEdit.Text = "&Edit"
        '
        'menuEditSeparator1
        '
        Me.menuEditSeparator1.Name = "menuEditSeparator1"
        Me.menuEditSeparator1.Size = New System.Drawing.Size(217, 6)
        '
        'menuEditPasteAs
        '
        Me.menuEditPasteAs.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuEditPasteAsHardlink, Me.menuEditPasteAsSymlink, Me.menuEditPasteAsShortcut, Me.menuEditPasteAsJunction})
        Me.menuEditPasteAs.Name = "menuEditPasteAs"
        Me.menuEditPasteAs.Size = New System.Drawing.Size(220, 22)
        Me.menuEditPasteAs.Text = "Paste As"
        '
        'menuEditSeparator2
        '
        Me.menuEditSeparator2.Name = "menuEditSeparator2"
        Me.menuEditSeparator2.Size = New System.Drawing.Size(217, 6)
        '
        'menuGo
        '
        Me.menuGo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuGoBack, Me.menuGoForward, Me.menuGoUp, Me.menuGoRoot, Me.menuGoHome, Me.menuGoSeparator1, Me.menuGoRefresh, Me.menuGoStop})
        Me.menuGo.Name = "menuGo"
        Me.menuGo.Size = New System.Drawing.Size(34, 20)
        Me.menuGo.Text = "Go"
        '
        'menuGoSeparator1
        '
        Me.menuGoSeparator1.Name = "menuGoSeparator1"
        Me.menuGoSeparator1.Size = New System.Drawing.Size(177, 6)
        '
        'menuTools
        '
        Me.menuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuToolsSettings, Me.menuToolsContextMenu, Me.menuToolsColumns, Me.menuToolsSeparator1, Me.menuToolsResizeColumns, Me.menuToolsUseShell})
        Me.menuTools.Name = "menuTools"
        Me.menuTools.Size = New System.Drawing.Size(46, 20)
        Me.menuTools.Text = "&Tools"
        '
        'menuToolsSeparator1
        '
        Me.menuToolsSeparator1.Name = "menuToolsSeparator1"
        Me.menuToolsSeparator1.Size = New System.Drawing.Size(224, 6)
        '
        'menuToolsUseShell
        '
        Me.menuToolsUseShell.CheckOnClick = True
        Me.menuToolsUseShell.Name = "menuToolsUseShell"
        Me.menuToolsUseShell.Size = New System.Drawing.Size(227, 22)
        Me.menuToolsUseShell.Text = "Use Windows Shell"
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
        Me.cbxURI.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.cbxURI.Name = "cbxURI"
        Me.cbxURI.Size = New System.Drawing.Size(800, 25)
        '
        'menuFileExecute
        '
        Me.menuFileExecute.Name = "menuFileExecute"
        Me.menuFileExecute.Size = New System.Drawing.Size(184, 22)
        Me.menuFileExecute.Text = "Execute"
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
        'menuFileCreate
        '
        Me.menuFileCreate.Image = Global.My.Resources.Resources.NewFile
        Me.menuFileCreate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuFileCreate.Name = "menuFileCreate"
        Me.menuFileCreate.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.menuFileCreate.Size = New System.Drawing.Size(237, 22)
        Me.menuFileCreate.Text = "Create..."
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
        '
        'menuFileDelete
        '
        Me.menuFileDelete.Image = Global.My.Resources.Resources.Delete
        Me.menuFileDelete.Name = "menuFileDelete"
        Me.menuFileDelete.Size = New System.Drawing.Size(237, 22)
        Me.menuFileDelete.Text = "Delete Permanently (Shift+Del)"
        '
        'menuFileCopyTo
        '
        Me.menuFileCopyTo.Image = Global.My.Resources.Resources.CopyTo
        Me.menuFileCopyTo.Name = "menuFileCopyTo"
        Me.menuFileCopyTo.Size = New System.Drawing.Size(237, 22)
        Me.menuFileCopyTo.Text = "Copy To..."
        '
        'menuFileMoveTo
        '
        Me.menuFileMoveTo.Image = Global.My.Resources.Resources.MoveTo
        Me.menuFileMoveTo.Name = "menuFileMoveTo"
        Me.menuFileMoveTo.Size = New System.Drawing.Size(237, 22)
        Me.menuFileMoveTo.Text = "Move To..."
        '
        'menuFileProperties
        '
        Me.menuFileProperties.Image = Global.My.Resources.Resources.Properties
        Me.menuFileProperties.Name = "menuFileProperties"
        Me.menuFileProperties.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Enter), System.Windows.Forms.Keys)
        Me.menuFileProperties.Size = New System.Drawing.Size(237, 22)
        Me.menuFileProperties.Text = "Properties"
        '
        'menuFileLaunch
        '
        Me.menuFileLaunch.Image = Global.My.Resources.Resources.Run
        Me.menuFileLaunch.Name = "menuFileLaunch"
        Me.menuFileLaunch.Size = New System.Drawing.Size(237, 22)
        Me.menuFileLaunch.Text = "Launch"
        '
        'menuFileShowTarget
        '
        Me.menuFileShowTarget.Image = Global.My.Resources.Resources.OpenLocation
        Me.menuFileShowTarget.Name = "menuFileShowTarget"
        Me.menuFileShowTarget.Size = New System.Drawing.Size(237, 22)
        Me.menuFileShowTarget.Text = "Show Target"
        '
        'menuFileExit
        '
        Me.menuFileExit.Image = Global.My.Resources.Resources.Quit
        Me.menuFileExit.Name = "menuFileExit"
        Me.menuFileExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.menuFileExit.Size = New System.Drawing.Size(237, 22)
        Me.menuFileExit.Text = "Exit"
        '
        'menuEditCut
        '
        Me.menuEditCut.Image = Global.My.Resources.Resources.Cut
        Me.menuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuEditCut.Name = "menuEditCut"
        Me.menuEditCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.menuEditCut.Size = New System.Drawing.Size(220, 22)
        Me.menuEditCut.Text = "Cu&t"
        '
        'menuEditCopy
        '
        Me.menuEditCopy.Image = Global.My.Resources.Resources.Copy
        Me.menuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuEditCopy.Name = "menuEditCopy"
        Me.menuEditCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.menuEditCopy.Size = New System.Drawing.Size(220, 22)
        Me.menuEditCopy.Text = "&Copy"
        '
        'menuEditPaste
        '
        Me.menuEditPaste.Image = Global.My.Resources.Resources.Paste
        Me.menuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuEditPaste.Name = "menuEditPaste"
        Me.menuEditPaste.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.menuEditPaste.Size = New System.Drawing.Size(220, 22)
        Me.menuEditPaste.Text = "&Paste"
        '
        'menuEditPasteAsHardlink
        '
        Me.menuEditPasteAsHardlink.Image = Global.My.Resources.Resources.PasteHardlink
        Me.menuEditPasteAsHardlink.Name = "menuEditPasteAsHardlink"
        Me.menuEditPasteAsHardlink.Size = New System.Drawing.Size(119, 22)
        Me.menuEditPasteAsHardlink.Text = "Hardlink"
        '
        'menuEditPasteAsSymlink
        '
        Me.menuEditPasteAsSymlink.Image = Global.My.Resources.Resources.PasteSymlink
        Me.menuEditPasteAsSymlink.Name = "menuEditPasteAsSymlink"
        Me.menuEditPasteAsSymlink.Size = New System.Drawing.Size(119, 22)
        Me.menuEditPasteAsSymlink.Text = "Symlink"
        '
        'menuEditPasteAsShortcut
        '
        Me.menuEditPasteAsShortcut.Image = Global.My.Resources.Resources.PasteShortcut
        Me.menuEditPasteAsShortcut.Name = "menuEditPasteAsShortcut"
        Me.menuEditPasteAsShortcut.Size = New System.Drawing.Size(119, 22)
        Me.menuEditPasteAsShortcut.Text = "Shortcut"
        '
        'menuEditPasteAsJunction
        '
        Me.menuEditPasteAsJunction.Image = Global.My.Resources.Resources.PasteJunction
        Me.menuEditPasteAsJunction.Name = "menuEditPasteAsJunction"
        Me.menuEditPasteAsJunction.Size = New System.Drawing.Size(119, 22)
        Me.menuEditPasteAsJunction.Text = "Junction"
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
        Me.menuEditDeselectAll.Text = "Deselect All"
        '
        'menuEditInvert
        '
        Me.menuEditInvert.Image = Global.My.Resources.Resources.InvertSelection
        Me.menuEditInvert.Name = "menuEditInvert"
        Me.menuEditInvert.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.menuEditInvert.Size = New System.Drawing.Size(220, 22)
        Me.menuEditInvert.Text = "Invert Selection"
        '
        'menuGoBack
        '
        Me.menuGoBack.Image = Global.My.Resources.Resources.Back
        Me.menuGoBack.Name = "menuGoBack"
        Me.menuGoBack.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Left), System.Windows.Forms.Keys)
        Me.menuGoBack.Size = New System.Drawing.Size(180, 22)
        Me.menuGoBack.Text = "Back"
        '
        'menuGoForward
        '
        Me.menuGoForward.Image = Global.My.Resources.Resources.Forward
        Me.menuGoForward.Name = "menuGoForward"
        Me.menuGoForward.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Right), System.Windows.Forms.Keys)
        Me.menuGoForward.Size = New System.Drawing.Size(180, 22)
        Me.menuGoForward.Text = "Forward"
        '
        'menuGoUp
        '
        Me.menuGoUp.Image = Global.My.Resources.Resources.Up
        Me.menuGoUp.Name = "menuGoUp"
        Me.menuGoUp.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Up), System.Windows.Forms.Keys)
        Me.menuGoUp.Size = New System.Drawing.Size(180, 22)
        Me.menuGoUp.Text = "Up"
        '
        'menuGoRoot
        '
        Me.menuGoRoot.Image = Global.My.Resources.Resources.Root
        Me.menuGoRoot.Name = "menuGoRoot"
        Me.menuGoRoot.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.menuGoRoot.Size = New System.Drawing.Size(180, 22)
        Me.menuGoRoot.Text = "Root"
        '
        'menuGoHome
        '
        Me.menuGoHome.Image = Global.My.Resources.Resources.Home
        Me.menuGoHome.Name = "menuGoHome"
        Me.menuGoHome.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.menuGoHome.Size = New System.Drawing.Size(180, 22)
        Me.menuGoHome.Text = "Home"
        '
        'menuGoRefresh
        '
        Me.menuGoRefresh.Image = Global.My.Resources.Resources.Refresh
        Me.menuGoRefresh.Name = "menuGoRefresh"
        Me.menuGoRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.menuGoRefresh.Size = New System.Drawing.Size(180, 22)
        Me.menuGoRefresh.Text = "Refresh"
        '
        'menuGoStop
        '
        Me.menuGoStop.Image = Global.My.Resources.Resources.Cancel
        Me.menuGoStop.Name = "menuGoStop"
        Me.menuGoStop.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.menuGoStop.Size = New System.Drawing.Size(180, 22)
        Me.menuGoStop.Text = "Stop"
        '
        'menuToolsSettings
        '
        Me.menuToolsSettings.Image = Global.My.Resources.Resources.Settings
        Me.menuToolsSettings.Name = "menuToolsSettings"
        Me.menuToolsSettings.Size = New System.Drawing.Size(227, 22)
        Me.menuToolsSettings.Text = "Settings"
        '
        'menuToolsContextMenu
        '
        Me.menuToolsContextMenu.Image = Global.My.Resources.Resources.ContextConfig
        Me.menuToolsContextMenu.Name = "menuToolsContextMenu"
        Me.menuToolsContextMenu.Size = New System.Drawing.Size(227, 22)
        Me.menuToolsContextMenu.Text = "Context Menu Configuration"
        '
        'menuToolsColumns
        '
        Me.menuToolsColumns.Image = Global.My.Resources.Resources.ColumnConfig
        Me.menuToolsColumns.Name = "menuToolsColumns"
        Me.menuToolsColumns.Size = New System.Drawing.Size(227, 22)
        Me.menuToolsColumns.Text = "Columns Configuration"
        '
        'menuToolsResizeColumns
        '
        Me.menuToolsResizeColumns.Image = Global.My.Resources.Resources.ResizeColumns
        Me.menuToolsResizeColumns.Name = "menuToolsResizeColumns"
        Me.menuToolsResizeColumns.Size = New System.Drawing.Size(227, 22)
        Me.menuToolsResizeColumns.Text = "Resize All Columns"
        '
        'FileBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(838, 498)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents treeViewDirs As System.Windows.Forms.TreeView
    Friend WithEvents lstCurrent As System.Windows.Forms.ListView
    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
    Friend WithEvents ctxMenuL As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents menuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents menuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileCreate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuEditSelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents status As System.Windows.Forms.StatusStrip
    Friend WithEvents toolStripURL As System.Windows.Forms.ToolStrip
    Friend WithEvents cbxURI As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnGo As System.Windows.Forms.ToolStripButton
    Friend WithEvents menuGo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuFileRecycle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileRename As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileCopyTo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileMoveTo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuFileProperties As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileLaunch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileExecute As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileShowTarget As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuEditDeselectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditInvert As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteAsHardlink As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteAsSymlink As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteAsShortcut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuEditPasteAsJunction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoBack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoForward As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoUp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoRoot As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoHome As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuGoRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGoStop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsContextMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsColumns As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsResizeColumns As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsUseShell As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolsSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents statusLabel As System.Windows.Forms.ToolStripStatusLabel
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
End Class
