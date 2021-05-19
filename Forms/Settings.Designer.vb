<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Settings
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
        Me.chkShowFoldersFirst = New System.Windows.Forms.CheckBox()
        Me.chkShowADSSeparate = New System.Windows.Forms.CheckBox()
        Me.chkShowHidden = New System.Windows.Forms.CheckBox()
        Me.chkShowSystem = New System.Windows.Forms.CheckBox()
        Me.chkShowDot = New System.Windows.Forms.CheckBox()
        Me.chkShowExtensions = New System.Windows.Forms.CheckBox()
        Me.chkDisableViewAutoUpdate = New System.Windows.Forms.CheckBox()
        Me.chkDisableTreeAutoUpdate = New System.Windows.Forms.CheckBox()
        Me.chkDisableUpdateCheck = New System.Windows.Forms.CheckBox()
        Me.chkWindowsShellDefaultValue = New System.Windows.Forms.CheckBox()
        Me.chkSpecificItemIcons = New System.Windows.Forms.CheckBox()
        Me.chkImageThumbs = New System.Windows.Forms.CheckBox()
        Me.chkOverlayReparse = New System.Windows.Forms.CheckBox()
        Me.chkOverlayHardlink = New System.Windows.Forms.CheckBox()
        Me.chkOverlayCompressed = New System.Windows.Forms.CheckBox()
        Me.chkOverlayEncrypted = New System.Windows.Forms.CheckBox()
        Me.chkOverlayOffline = New System.Windows.Forms.CheckBox()
        Me.chkHighlightCompressed = New System.Windows.Forms.CheckBox()
        Me.chkHighlightEncrypted = New System.Windows.Forms.CheckBox()
        Me.chkWindowMaximised = New System.Windows.Forms.CheckBox()
        Me.chkWindowRemember = New System.Windows.Forms.CheckBox()
        Me.grpWindowDefault = New System.Windows.Forms.GroupBox()
        Me.lblWindowDefaultHeight = New System.Windows.Forms.Label()
        Me.txtWindowDefaultHeight = New System.Windows.Forms.TextBox()
        Me.txtWindowDefaultWidth = New System.Windows.Forms.TextBox()
        Me.lblWindowDefaultWidth = New System.Windows.Forms.Label()
        Me.grpItemVisibility = New System.Windows.Forms.GroupBox()
        Me.grpWindow = New System.Windows.Forms.GroupBox()
        Me.grpSplitterDefault = New System.Windows.Forms.GroupBox()
        Me.txtSplitterDefaultSize = New System.Windows.Forms.TextBox()
        Me.chkSplitterRemember = New System.Windows.Forms.CheckBox()
        Me.grpOverlays = New System.Windows.Forms.GroupBox()
        Me.grpHighlighting = New System.Windows.Forms.GroupBox()
        Me.grpIcons = New System.Windows.Forms.GroupBox()
        Me.grpOther = New System.Windows.Forms.GroupBox()
        Me.grpDefaultDir = New System.Windows.Forms.GroupBox()
        Me.btnDefaultDirBrowse = New System.Windows.Forms.Button()
        Me.txtDefaultDir = New System.Windows.Forms.TextBox()
        Me.btnClearURIHistory = New System.Windows.Forms.Button()
        Me.chkRememberDir = New System.Windows.Forms.CheckBox()
        Me.lblSizeUnits = New System.Windows.Forms.Label()
        Me.cbxSizeUnits = New System.Windows.Forms.ComboBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnReload = New System.Windows.Forms.Button()
        Me.btnShowSettingsFile = New System.Windows.Forms.Button()
        Me.grpColumns = New System.Windows.Forms.GroupBox()
        Me.btnResetColumns = New System.Windows.Forms.Button()
        Me.chkSaveColumns = New System.Windows.Forms.CheckBox()
        Me.chkEnableIcons = New System.Windows.Forms.CheckBox()
        Me.grpTheme = New System.Windows.Forms.GroupBox()
        Me.cbxTheme = New System.Windows.Forms.ComboBox()
        Me.grpWindowDefault.SuspendLayout()
        Me.grpItemVisibility.SuspendLayout()
        Me.grpWindow.SuspendLayout()
        Me.grpSplitterDefault.SuspendLayout()
        Me.grpOverlays.SuspendLayout()
        Me.grpHighlighting.SuspendLayout()
        Me.grpIcons.SuspendLayout()
        Me.grpOther.SuspendLayout()
        Me.grpDefaultDir.SuspendLayout()
        Me.grpColumns.SuspendLayout()
        Me.grpTheme.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkShowFoldersFirst
        '
        Me.chkShowFoldersFirst.AutoSize = True
        Me.chkShowFoldersFirst.Location = New System.Drawing.Point(6, 19)
        Me.chkShowFoldersFirst.Name = "chkShowFoldersFirst"
        Me.chkShowFoldersFirst.Size = New System.Drawing.Size(112, 17)
        Me.chkShowFoldersFirst.TabIndex = 0
        Me.chkShowFoldersFirst.Text = "Show Folders First"
        Me.chkShowFoldersFirst.UseVisualStyleBackColor = True
        '
        'chkShowADSSeparate
        '
        Me.chkShowADSSeparate.AutoSize = True
        Me.chkShowADSSeparate.Location = New System.Drawing.Point(6, 42)
        Me.chkShowADSSeparate.Name = "chkShowADSSeparate"
        Me.chkShowADSSeparate.Size = New System.Drawing.Size(244, 17)
        Me.chkShowADSSeparate.TabIndex = 1
        Me.chkShowADSSeparate.Text = "Show Alternate Data Streams as separate files"
        Me.chkShowADSSeparate.UseVisualStyleBackColor = True
        '
        'chkShowHidden
        '
        Me.chkShowHidden.AutoSize = True
        Me.chkShowHidden.Location = New System.Drawing.Point(6, 65)
        Me.chkShowHidden.Name = "chkShowHidden"
        Me.chkShowHidden.Size = New System.Drawing.Size(127, 17)
        Me.chkShowHidden.TabIndex = 2
        Me.chkShowHidden.Text = "Show ""Hidden"" items"
        Me.chkShowHidden.UseVisualStyleBackColor = True
        '
        'chkShowSystem
        '
        Me.chkShowSystem.AutoSize = True
        Me.chkShowSystem.Location = New System.Drawing.Point(6, 88)
        Me.chkShowSystem.Name = "chkShowSystem"
        Me.chkShowSystem.Size = New System.Drawing.Size(127, 17)
        Me.chkShowSystem.TabIndex = 3
        Me.chkShowSystem.Text = "Show ""System"" items"
        Me.chkShowSystem.UseVisualStyleBackColor = True
        '
        'chkShowDot
        '
        Me.chkShowDot.AutoSize = True
        Me.chkShowDot.Location = New System.Drawing.Point(6, 111)
        Me.chkShowDot.Name = "chkShowDot"
        Me.chkShowDot.Size = New System.Drawing.Size(155, 17)
        Me.chkShowDot.TabIndex = 4
        Me.chkShowDot.Text = "Show items starting with ""."""
        Me.chkShowDot.UseVisualStyleBackColor = True
        '
        'chkShowExtensions
        '
        Me.chkShowExtensions.AutoSize = True
        Me.chkShowExtensions.Location = New System.Drawing.Point(6, 134)
        Me.chkShowExtensions.Name = "chkShowExtensions"
        Me.chkShowExtensions.Size = New System.Drawing.Size(122, 17)
        Me.chkShowExtensions.TabIndex = 5
        Me.chkShowExtensions.Text = "Show file extensions"
        Me.chkShowExtensions.UseVisualStyleBackColor = True
        '
        'chkDisableViewAutoUpdate
        '
        Me.chkDisableViewAutoUpdate.AutoSize = True
        Me.chkDisableViewAutoUpdate.Location = New System.Drawing.Point(6, 19)
        Me.chkDisableViewAutoUpdate.Name = "chkDisableViewAutoUpdate"
        Me.chkDisableViewAutoUpdate.Size = New System.Drawing.Size(193, 17)
        Me.chkDisableViewAutoUpdate.TabIndex = 0
        Me.chkDisableViewAutoUpdate.Text = "Disable Current View Auto-updating"
        Me.chkDisableViewAutoUpdate.UseVisualStyleBackColor = True
        '
        'chkDisableTreeAutoUpdate
        '
        Me.chkDisableTreeAutoUpdate.AutoSize = True
        Me.chkDisableTreeAutoUpdate.Location = New System.Drawing.Point(6, 42)
        Me.chkDisableTreeAutoUpdate.Name = "chkDisableTreeAutoUpdate"
        Me.chkDisableTreeAutoUpdate.Size = New System.Drawing.Size(200, 17)
        Me.chkDisableTreeAutoUpdate.TabIndex = 1
        Me.chkDisableTreeAutoUpdate.Text = "Disable Directory Tree Auto-updating"
        Me.chkDisableTreeAutoUpdate.UseVisualStyleBackColor = True
        '
        'chkDisableUpdateCheck
        '
        Me.chkDisableUpdateCheck.AutoSize = True
        Me.chkDisableUpdateCheck.Location = New System.Drawing.Point(6, 65)
        Me.chkDisableUpdateCheck.Name = "chkDisableUpdateCheck"
        Me.chkDisableUpdateCheck.Size = New System.Drawing.Size(133, 17)
        Me.chkDisableUpdateCheck.TabIndex = 2
        Me.chkDisableUpdateCheck.Text = "Disable Update Check"
        Me.chkDisableUpdateCheck.UseVisualStyleBackColor = True
        '
        'chkWindowsShellDefaultValue
        '
        Me.chkWindowsShellDefaultValue.AutoSize = True
        Me.chkWindowsShellDefaultValue.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkWindowsShellDefaultValue.Location = New System.Drawing.Point(6, 88)
        Me.chkWindowsShellDefaultValue.Name = "chkWindowsShellDefaultValue"
        Me.chkWindowsShellDefaultValue.Size = New System.Drawing.Size(195, 17)
        Me.chkWindowsShellDefaultValue.TabIndex = 3
        Me.chkWindowsShellDefaultValue.Text = """Use Windows Shell"" default value:"
        Me.chkWindowsShellDefaultValue.UseVisualStyleBackColor = True
        '
        'chkSpecificItemIcons
        '
        Me.chkSpecificItemIcons.AutoSize = True
        Me.chkSpecificItemIcons.Location = New System.Drawing.Point(6, 19)
        Me.chkSpecificItemIcons.Name = "chkSpecificItemIcons"
        Me.chkSpecificItemIcons.Size = New System.Drawing.Size(175, 17)
        Me.chkSpecificItemIcons.TabIndex = 0
        Me.chkSpecificItemIcons.Text = "Allow getting item-specific icons"
        Me.chkSpecificItemIcons.UseVisualStyleBackColor = True
        '
        'chkImageThumbs
        '
        Me.chkImageThumbs.AutoSize = True
        Me.chkImageThumbs.Location = New System.Drawing.Point(6, 42)
        Me.chkImageThumbs.Name = "chkImageThumbs"
        Me.chkImageThumbs.Size = New System.Drawing.Size(191, 17)
        Me.chkImageThumbs.TabIndex = 1
        Me.chkImageThumbs.Text = "Allow loading images as thumbnails"
        Me.chkImageThumbs.UseVisualStyleBackColor = True
        '
        'chkOverlayReparse
        '
        Me.chkOverlayReparse.AutoSize = True
        Me.chkOverlayReparse.Location = New System.Drawing.Point(6, 19)
        Me.chkOverlayReparse.Name = "chkOverlayReparse"
        Me.chkOverlayReparse.Size = New System.Drawing.Size(93, 17)
        Me.chkOverlayReparse.TabIndex = 0
        Me.chkOverlayReparse.Text = "Reparse Point"
        Me.chkOverlayReparse.UseVisualStyleBackColor = True
        '
        'chkOverlayHardlink
        '
        Me.chkOverlayHardlink.AutoSize = True
        Me.chkOverlayHardlink.Location = New System.Drawing.Point(6, 42)
        Me.chkOverlayHardlink.Name = "chkOverlayHardlink"
        Me.chkOverlayHardlink.Size = New System.Drawing.Size(65, 17)
        Me.chkOverlayHardlink.TabIndex = 1
        Me.chkOverlayHardlink.Text = "Hardlink"
        Me.chkOverlayHardlink.UseVisualStyleBackColor = True
        '
        'chkOverlayCompressed
        '
        Me.chkOverlayCompressed.AutoSize = True
        Me.chkOverlayCompressed.Location = New System.Drawing.Point(6, 65)
        Me.chkOverlayCompressed.Name = "chkOverlayCompressed"
        Me.chkOverlayCompressed.Size = New System.Drawing.Size(84, 17)
        Me.chkOverlayCompressed.TabIndex = 2
        Me.chkOverlayCompressed.Text = "Compressed"
        Me.chkOverlayCompressed.UseVisualStyleBackColor = True
        '
        'chkOverlayEncrypted
        '
        Me.chkOverlayEncrypted.AutoSize = True
        Me.chkOverlayEncrypted.Location = New System.Drawing.Point(6, 88)
        Me.chkOverlayEncrypted.Name = "chkOverlayEncrypted"
        Me.chkOverlayEncrypted.Size = New System.Drawing.Size(74, 17)
        Me.chkOverlayEncrypted.TabIndex = 3
        Me.chkOverlayEncrypted.Text = "Encrypted"
        Me.chkOverlayEncrypted.UseVisualStyleBackColor = True
        '
        'chkOverlayOffline
        '
        Me.chkOverlayOffline.AutoSize = True
        Me.chkOverlayOffline.Location = New System.Drawing.Point(6, 111)
        Me.chkOverlayOffline.Name = "chkOverlayOffline"
        Me.chkOverlayOffline.Size = New System.Drawing.Size(56, 17)
        Me.chkOverlayOffline.TabIndex = 4
        Me.chkOverlayOffline.Text = "Offline"
        Me.chkOverlayOffline.UseVisualStyleBackColor = True
        '
        'chkHighlightCompressed
        '
        Me.chkHighlightCompressed.AutoSize = True
        Me.chkHighlightCompressed.Location = New System.Drawing.Point(6, 19)
        Me.chkHighlightCompressed.Name = "chkHighlightCompressed"
        Me.chkHighlightCompressed.Size = New System.Drawing.Size(84, 17)
        Me.chkHighlightCompressed.TabIndex = 0
        Me.chkHighlightCompressed.Text = "Compressed"
        Me.chkHighlightCompressed.UseVisualStyleBackColor = True
        '
        'chkHighlightEncrypted
        '
        Me.chkHighlightEncrypted.AutoSize = True
        Me.chkHighlightEncrypted.Location = New System.Drawing.Point(6, 42)
        Me.chkHighlightEncrypted.Name = "chkHighlightEncrypted"
        Me.chkHighlightEncrypted.Size = New System.Drawing.Size(74, 17)
        Me.chkHighlightEncrypted.TabIndex = 1
        Me.chkHighlightEncrypted.Text = "Encrypted"
        Me.chkHighlightEncrypted.UseVisualStyleBackColor = True
        '
        'chkWindowMaximised
        '
        Me.chkWindowMaximised.AutoSize = True
        Me.chkWindowMaximised.Location = New System.Drawing.Point(6, 19)
        Me.chkWindowMaximised.Name = "chkWindowMaximised"
        Me.chkWindowMaximised.Size = New System.Drawing.Size(100, 17)
        Me.chkWindowMaximised.TabIndex = 0
        Me.chkWindowMaximised.Text = "Start Maximised"
        Me.chkWindowMaximised.UseVisualStyleBackColor = True
        '
        'chkWindowRemember
        '
        Me.chkWindowRemember.AutoSize = True
        Me.chkWindowRemember.Location = New System.Drawing.Point(6, 42)
        Me.chkWindowRemember.Name = "chkWindowRemember"
        Me.chkWindowRemember.Size = New System.Drawing.Size(142, 17)
        Me.chkWindowRemember.TabIndex = 1
        Me.chkWindowRemember.Text = "Remember Window Size"
        Me.chkWindowRemember.UseVisualStyleBackColor = True
        '
        'grpWindowDefault
        '
        Me.grpWindowDefault.Controls.Add(Me.lblWindowDefaultHeight)
        Me.grpWindowDefault.Controls.Add(Me.txtWindowDefaultHeight)
        Me.grpWindowDefault.Controls.Add(Me.txtWindowDefaultWidth)
        Me.grpWindowDefault.Controls.Add(Me.lblWindowDefaultWidth)
        Me.grpWindowDefault.Location = New System.Drawing.Point(6, 56)
        Me.grpWindowDefault.Name = "grpWindowDefault"
        Me.grpWindowDefault.Size = New System.Drawing.Size(137, 58)
        Me.grpWindowDefault.TabIndex = 2
        Me.grpWindowDefault.TabStop = False
        Me.grpWindowDefault.Text = "Default Size:"
        '
        'lblWindowDefaultHeight
        '
        Me.lblWindowDefaultHeight.AutoSize = True
        Me.lblWindowDefaultHeight.Location = New System.Drawing.Point(6, 37)
        Me.lblWindowDefaultHeight.Name = "lblWindowDefaultHeight"
        Me.lblWindowDefaultHeight.Size = New System.Drawing.Size(41, 13)
        Me.lblWindowDefaultHeight.TabIndex = 3
        Me.lblWindowDefaultHeight.Text = "Height:"
        '
        'txtWindowDefaultHeight
        '
        Me.txtWindowDefaultHeight.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtWindowDefaultHeight.Location = New System.Drawing.Point(53, 34)
        Me.txtWindowDefaultHeight.Name = "txtWindowDefaultHeight"
        Me.txtWindowDefaultHeight.Size = New System.Drawing.Size(81, 20)
        Me.txtWindowDefaultHeight.TabIndex = 1
        '
        'txtWindowDefaultWidth
        '
        Me.txtWindowDefaultWidth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtWindowDefaultWidth.Location = New System.Drawing.Point(53, 13)
        Me.txtWindowDefaultWidth.Name = "txtWindowDefaultWidth"
        Me.txtWindowDefaultWidth.Size = New System.Drawing.Size(81, 20)
        Me.txtWindowDefaultWidth.TabIndex = 0
        '
        'lblWindowDefaultWidth
        '
        Me.lblWindowDefaultWidth.AutoSize = True
        Me.lblWindowDefaultWidth.Location = New System.Drawing.Point(6, 16)
        Me.lblWindowDefaultWidth.Name = "lblWindowDefaultWidth"
        Me.lblWindowDefaultWidth.Size = New System.Drawing.Size(38, 13)
        Me.lblWindowDefaultWidth.TabIndex = 0
        Me.lblWindowDefaultWidth.Text = "Width:"
        '
        'grpItemVisibility
        '
        Me.grpItemVisibility.Controls.Add(Me.chkShowFoldersFirst)
        Me.grpItemVisibility.Controls.Add(Me.chkShowADSSeparate)
        Me.grpItemVisibility.Controls.Add(Me.chkShowHidden)
        Me.grpItemVisibility.Controls.Add(Me.chkShowSystem)
        Me.grpItemVisibility.Controls.Add(Me.chkShowDot)
        Me.grpItemVisibility.Controls.Add(Me.chkShowExtensions)
        Me.grpItemVisibility.Location = New System.Drawing.Point(12, 12)
        Me.grpItemVisibility.Name = "grpItemVisibility"
        Me.grpItemVisibility.Size = New System.Drawing.Size(256, 157)
        Me.grpItemVisibility.TabIndex = 1
        Me.grpItemVisibility.TabStop = False
        Me.grpItemVisibility.Text = "Item Visibility"
        '
        'grpWindow
        '
        Me.grpWindow.Controls.Add(Me.grpSplitterDefault)
        Me.grpWindow.Controls.Add(Me.chkSplitterRemember)
        Me.grpWindow.Controls.Add(Me.grpWindowDefault)
        Me.grpWindow.Controls.Add(Me.chkWindowMaximised)
        Me.grpWindow.Controls.Add(Me.chkWindowRemember)
        Me.grpWindow.Location = New System.Drawing.Point(274, 219)
        Me.grpWindow.Name = "grpWindow"
        Me.grpWindow.Size = New System.Drawing.Size(149, 185)
        Me.grpWindow.TabIndex = 7
        Me.grpWindow.TabStop = False
        Me.grpWindow.Text = "Window Settings"
        '
        'grpSplitterDefault
        '
        Me.grpSplitterDefault.Controls.Add(Me.txtSplitterDefaultSize)
        Me.grpSplitterDefault.Location = New System.Drawing.Point(6, 134)
        Me.grpSplitterDefault.Name = "grpSplitterDefault"
        Me.grpSplitterDefault.Size = New System.Drawing.Size(137, 45)
        Me.grpSplitterDefault.TabIndex = 4
        Me.grpSplitterDefault.TabStop = False
        Me.grpSplitterDefault.Text = "Default Size:"
        '
        'txtSplitterDefaultSize
        '
        Me.txtSplitterDefaultSize.Location = New System.Drawing.Point(5, 19)
        Me.txtSplitterDefaultSize.Name = "txtSplitterDefaultSize"
        Me.txtSplitterDefaultSize.Size = New System.Drawing.Size(127, 20)
        Me.txtSplitterDefaultSize.TabIndex = 0
        '
        'chkSplitterRemember
        '
        Me.chkSplitterRemember.AutoSize = True
        Me.chkSplitterRemember.Location = New System.Drawing.Point(6, 120)
        Me.chkSplitterRemember.Name = "chkSplitterRemember"
        Me.chkSplitterRemember.Size = New System.Drawing.Size(135, 17)
        Me.chkSplitterRemember.TabIndex = 3
        Me.chkSplitterRemember.Text = "Remember Splitter Size"
        Me.chkSplitterRemember.UseVisualStyleBackColor = True
        '
        'grpOverlays
        '
        Me.grpOverlays.Controls.Add(Me.chkOverlayReparse)
        Me.grpOverlays.Controls.Add(Me.chkOverlayHardlink)
        Me.grpOverlays.Controls.Add(Me.chkOverlayCompressed)
        Me.grpOverlays.Controls.Add(Me.chkOverlayEncrypted)
        Me.grpOverlays.Controls.Add(Me.chkOverlayOffline)
        Me.grpOverlays.Location = New System.Drawing.Point(274, 12)
        Me.grpOverlays.Name = "grpOverlays"
        Me.grpOverlays.Size = New System.Drawing.Size(149, 132)
        Me.grpOverlays.TabIndex = 2
        Me.grpOverlays.TabStop = False
        Me.grpOverlays.Text = "Thumbnail Overlays"
        '
        'grpHighlighting
        '
        Me.grpHighlighting.Controls.Add(Me.chkHighlightCompressed)
        Me.grpHighlighting.Controls.Add(Me.chkHighlightEncrypted)
        Me.grpHighlighting.Location = New System.Drawing.Point(274, 150)
        Me.grpHighlighting.Name = "grpHighlighting"
        Me.grpHighlighting.Size = New System.Drawing.Size(149, 63)
        Me.grpHighlighting.TabIndex = 5
        Me.grpHighlighting.TabStop = False
        Me.grpHighlighting.Text = "Item Highlighting"
        '
        'grpIcons
        '
        Me.grpIcons.Controls.Add(Me.chkSpecificItemIcons)
        Me.grpIcons.Controls.Add(Me.chkImageThumbs)
        Me.grpIcons.Location = New System.Drawing.Point(12, 175)
        Me.grpIcons.Name = "grpIcons"
        Me.grpIcons.Size = New System.Drawing.Size(256, 65)
        Me.grpIcons.TabIndex = 4
        Me.grpIcons.TabStop = False
        '
        'grpOther
        '
        Me.grpOther.Controls.Add(Me.grpDefaultDir)
        Me.grpOther.Controls.Add(Me.btnClearURIHistory)
        Me.grpOther.Controls.Add(Me.chkRememberDir)
        Me.grpOther.Controls.Add(Me.lblSizeUnits)
        Me.grpOther.Controls.Add(Me.cbxSizeUnits)
        Me.grpOther.Controls.Add(Me.chkDisableViewAutoUpdate)
        Me.grpOther.Controls.Add(Me.chkDisableTreeAutoUpdate)
        Me.grpOther.Controls.Add(Me.chkDisableUpdateCheck)
        Me.grpOther.Controls.Add(Me.chkWindowsShellDefaultValue)
        Me.grpOther.Location = New System.Drawing.Point(12, 246)
        Me.grpOther.Name = "grpOther"
        Me.grpOther.Size = New System.Drawing.Size(256, 287)
        Me.grpOther.TabIndex = 6
        Me.grpOther.TabStop = False
        Me.grpOther.Text = "Other"
        '
        'grpDefaultDir
        '
        Me.grpDefaultDir.Controls.Add(Me.btnDefaultDirBrowse)
        Me.grpDefaultDir.Controls.Add(Me.txtDefaultDir)
        Me.grpDefaultDir.Location = New System.Drawing.Point(6, 152)
        Me.grpDefaultDir.Name = "grpDefaultDir"
        Me.grpDefaultDir.Size = New System.Drawing.Size(244, 48)
        Me.grpDefaultDir.TabIndex = 7
        Me.grpDefaultDir.TabStop = False
        Me.grpDefaultDir.Text = "Default Directory:"
        '
        'btnDefaultDirBrowse
        '
        Me.btnDefaultDirBrowse.Location = New System.Drawing.Point(163, 19)
        Me.btnDefaultDirBrowse.Name = "btnDefaultDirBrowse"
        Me.btnDefaultDirBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnDefaultDirBrowse.TabIndex = 1
        Me.btnDefaultDirBrowse.Text = "Browse..."
        Me.btnDefaultDirBrowse.UseVisualStyleBackColor = True
        '
        'txtDefaultDir
        '
        Me.txtDefaultDir.Location = New System.Drawing.Point(6, 21)
        Me.txtDefaultDir.Name = "txtDefaultDir"
        Me.txtDefaultDir.Size = New System.Drawing.Size(151, 20)
        Me.txtDefaultDir.TabIndex = 0
        '
        'btnClearURIHistory
        '
        Me.btnClearURIHistory.Location = New System.Drawing.Point(6, 258)
        Me.btnClearURIHistory.Name = "btnClearURIHistory"
        Me.btnClearURIHistory.Size = New System.Drawing.Size(244, 23)
        Me.btnClearURIHistory.TabIndex = 8
        Me.btnClearURIHistory.Text = "Clear URI History"
        Me.btnClearURIHistory.UseVisualStyleBackColor = True
        '
        'chkRememberDir
        '
        Me.chkRememberDir.AutoSize = True
        Me.chkRememberDir.Location = New System.Drawing.Point(6, 138)
        Me.chkRememberDir.Name = "chkRememberDir"
        Me.chkRememberDir.Size = New System.Drawing.Size(139, 17)
        Me.chkRememberDir.TabIndex = 6
        Me.chkRememberDir.Text = "Remember last directory"
        Me.chkRememberDir.UseVisualStyleBackColor = True
        '
        'lblSizeUnits
        '
        Me.lblSizeUnits.AutoSize = True
        Me.lblSizeUnits.Location = New System.Drawing.Point(6, 114)
        Me.lblSizeUnits.Name = "lblSizeUnits"
        Me.lblSizeUnits.Size = New System.Drawing.Size(57, 13)
        Me.lblSizeUnits.TabIndex = 4
        Me.lblSizeUnits.Text = "Size Units:"
        '
        'cbxSizeUnits
        '
        Me.cbxSizeUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxSizeUnits.FormattingEnabled = True
        Me.cbxSizeUnits.Items.AddRange(New Object() {"Auto (Decimal - 1000)", "Auto (Binary    - 1024)", "bytes (8 bits)", "kB  (Decimal - 1000)", "KiB (Binary    - 1024)", "MB (Decimal - 1000)", "MiB (Binary    - 1024)", "GB  (Decimal - 1000)", "GiB (Binary    - 1024)", "TB  (Decimal - 1000)", "TiB (Binary    - 1024)", "PB  (Decimal - 1000)", "PiB (Binary    - 1024)"})
        Me.cbxSizeUnits.Location = New System.Drawing.Point(69, 111)
        Me.cbxSizeUnits.Name = "cbxSizeUnits"
        Me.cbxSizeUnits.Size = New System.Drawing.Size(181, 21)
        Me.cbxSizeUnits.TabIndex = 5
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(36, 539)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(117, 539)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "Force-Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnReload
        '
        Me.btnReload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReload.Location = New System.Drawing.Point(198, 539)
        Me.btnReload.Name = "btnReload"
        Me.btnReload.Size = New System.Drawing.Size(92, 23)
        Me.btnReload.TabIndex = 11
        Me.btnReload.Text = "Reload Settings"
        Me.btnReload.UseVisualStyleBackColor = True
        '
        'btnShowSettingsFile
        '
        Me.btnShowSettingsFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowSettingsFile.Location = New System.Drawing.Point(296, 539)
        Me.btnShowSettingsFile.Name = "btnShowSettingsFile"
        Me.btnShowSettingsFile.Size = New System.Drawing.Size(104, 23)
        Me.btnShowSettingsFile.TabIndex = 12
        Me.btnShowSettingsFile.Text = "Show Settings File"
        Me.btnShowSettingsFile.UseVisualStyleBackColor = True
        '
        'grpColumns
        '
        Me.grpColumns.Controls.Add(Me.btnResetColumns)
        Me.grpColumns.Controls.Add(Me.chkSaveColumns)
        Me.grpColumns.Location = New System.Drawing.Point(274, 410)
        Me.grpColumns.Name = "grpColumns"
        Me.grpColumns.Size = New System.Drawing.Size(149, 71)
        Me.grpColumns.TabIndex = 8
        Me.grpColumns.TabStop = False
        Me.grpColumns.Text = "Column Settings"
        '
        'btnResetColumns
        '
        Me.btnResetColumns.Location = New System.Drawing.Point(6, 42)
        Me.btnResetColumns.Name = "btnResetColumns"
        Me.btnResetColumns.Size = New System.Drawing.Size(137, 23)
        Me.btnResetColumns.TabIndex = 1
        Me.btnResetColumns.Text = "Reset column defaults"
        Me.btnResetColumns.UseVisualStyleBackColor = True
        '
        'chkSaveColumns
        '
        Me.chkSaveColumns.AutoSize = True
        Me.chkSaveColumns.Location = New System.Drawing.Point(6, 19)
        Me.chkSaveColumns.Name = "chkSaveColumns"
        Me.chkSaveColumns.Size = New System.Drawing.Size(122, 17)
        Me.chkSaveColumns.TabIndex = 0
        Me.chkSaveColumns.Text = "Save folder columns"
        Me.chkSaveColumns.UseVisualStyleBackColor = True
        '
        'chkEnableIcons
        '
        Me.chkEnableIcons.AutoSize = True
        Me.chkEnableIcons.Location = New System.Drawing.Point(21, 173)
        Me.chkEnableIcons.Name = "chkEnableIcons"
        Me.chkEnableIcons.Size = New System.Drawing.Size(111, 17)
        Me.chkEnableIcons.TabIndex = 3
        Me.chkEnableIcons.Text = "Enable Item Icons"
        Me.chkEnableIcons.UseVisualStyleBackColor = True
        '
        'grpTheme
        '
        Me.grpTheme.Controls.Add(Me.cbxTheme)
        Me.grpTheme.Location = New System.Drawing.Point(274, 487)
        Me.grpTheme.Name = "grpTheme"
        Me.grpTheme.Size = New System.Drawing.Size(149, 46)
        Me.grpTheme.TabIndex = 13
        Me.grpTheme.TabStop = False
        Me.grpTheme.Text = "Theme"
        '
        'cbxTheme
        '
        Me.cbxTheme.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbxTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxTheme.FormattingEnabled = True
        Me.cbxTheme.Items.AddRange(New Object() {"Default", "System Dark", "Dark", "Inverted", "Test"})
        Me.cbxTheme.Location = New System.Drawing.Point(6, 19)
        Me.cbxTheme.Name = "cbxTheme"
        Me.cbxTheme.Size = New System.Drawing.Size(137, 21)
        Me.cbxTheme.TabIndex = 0
        '
        'Settings
        '
        Me.AcceptButton = Me.btnClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(435, 574)
        Me.Controls.Add(Me.grpTheme)
        Me.Controls.Add(Me.grpColumns)
        Me.Controls.Add(Me.btnShowSettingsFile)
        Me.Controls.Add(Me.btnReload)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grpOther)
        Me.Controls.Add(Me.chkEnableIcons)
        Me.Controls.Add(Me.grpIcons)
        Me.Controls.Add(Me.grpHighlighting)
        Me.Controls.Add(Me.grpOverlays)
        Me.Controls.Add(Me.grpWindow)
        Me.Controls.Add(Me.grpItemVisibility)
        Me.Name = "Settings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.grpWindowDefault.ResumeLayout(False)
        Me.grpWindowDefault.PerformLayout()
        Me.grpItemVisibility.ResumeLayout(False)
        Me.grpItemVisibility.PerformLayout()
        Me.grpWindow.ResumeLayout(False)
        Me.grpWindow.PerformLayout()
        Me.grpSplitterDefault.ResumeLayout(False)
        Me.grpSplitterDefault.PerformLayout()
        Me.grpOverlays.ResumeLayout(False)
        Me.grpOverlays.PerformLayout()
        Me.grpHighlighting.ResumeLayout(False)
        Me.grpHighlighting.PerformLayout()
        Me.grpIcons.ResumeLayout(False)
        Me.grpIcons.PerformLayout()
        Me.grpOther.ResumeLayout(False)
        Me.grpOther.PerformLayout()
        Me.grpDefaultDir.ResumeLayout(False)
        Me.grpDefaultDir.PerformLayout()
        Me.grpColumns.ResumeLayout(False)
        Me.grpColumns.PerformLayout()
        Me.grpTheme.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkShowFoldersFirst As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowADSSeparate As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowHidden As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowSystem As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowDot As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowExtensions As System.Windows.Forms.CheckBox
    Friend WithEvents chkDisableViewAutoUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents chkDisableTreeAutoUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents chkDisableUpdateCheck As System.Windows.Forms.CheckBox
    Friend WithEvents chkWindowsShellDefaultValue As System.Windows.Forms.CheckBox
    Friend WithEvents chkSpecificItemIcons As System.Windows.Forms.CheckBox
    Friend WithEvents chkImageThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverlayReparse As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverlayHardlink As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverlayCompressed As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverlayEncrypted As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverlayOffline As System.Windows.Forms.CheckBox
    Friend WithEvents chkHighlightCompressed As System.Windows.Forms.CheckBox
    Friend WithEvents chkHighlightEncrypted As System.Windows.Forms.CheckBox
    Friend WithEvents chkWindowMaximised As System.Windows.Forms.CheckBox
    Friend WithEvents chkWindowRemember As System.Windows.Forms.CheckBox
    Friend WithEvents grpWindowDefault As System.Windows.Forms.GroupBox
    Friend WithEvents lblWindowDefaultWidth As System.Windows.Forms.Label
    Friend WithEvents txtWindowDefaultWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtWindowDefaultHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblWindowDefaultHeight As System.Windows.Forms.Label
    Friend WithEvents grpItemVisibility As System.Windows.Forms.GroupBox
    Friend WithEvents grpWindow As System.Windows.Forms.GroupBox
    Friend WithEvents grpOverlays As System.Windows.Forms.GroupBox
    Friend WithEvents grpHighlighting As System.Windows.Forms.GroupBox
    Friend WithEvents grpIcons As System.Windows.Forms.GroupBox
    Friend WithEvents grpOther As System.Windows.Forms.GroupBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnReload As System.Windows.Forms.Button
    Friend WithEvents btnShowSettingsFile As System.Windows.Forms.Button
    Friend WithEvents grpColumns As System.Windows.Forms.GroupBox
    Friend WithEvents chkSaveColumns As System.Windows.Forms.CheckBox
    Friend WithEvents btnResetColumns As System.Windows.Forms.Button
    Friend WithEvents cbxSizeUnits As System.Windows.Forms.ComboBox
    Friend WithEvents lblSizeUnits As System.Windows.Forms.Label
    Friend WithEvents chkRememberDir As System.Windows.Forms.CheckBox
    Friend WithEvents grpDefaultDir As System.Windows.Forms.GroupBox
    Friend WithEvents txtDefaultDir As System.Windows.Forms.TextBox
    Friend WithEvents btnDefaultDirBrowse As System.Windows.Forms.Button
    Friend WithEvents chkEnableIcons As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearURIHistory As System.Windows.Forms.Button
    Friend WithEvents chkSplitterRemember As System.Windows.Forms.CheckBox
    Friend WithEvents grpSplitterDefault As System.Windows.Forms.GroupBox
    Friend WithEvents txtSplitterDefaultSize As System.Windows.Forms.TextBox
    Friend WithEvents grpTheme As System.Windows.Forms.GroupBox
    Friend WithEvents cbxTheme As System.Windows.Forms.ComboBox
End Class
