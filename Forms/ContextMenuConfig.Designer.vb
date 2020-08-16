<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ContextMenuConfig
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
        Me.lstMain = New System.Windows.Forms.ListView()
        Me.colHeadType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadText = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadIcon = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadAdminIcon = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadExtended = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadFilter = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadActionType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadActionSettings = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnShowConfig = New System.Windows.Forms.Button()
        Me.grpItemConfig = New System.Windows.Forms.GroupBox()
        Me.btnItemIconPick = New System.Windows.Forms.Button()
        Me.btnItemIconBrowse = New System.Windows.Forms.Button()
        Me.txtItemIconPath = New System.Windows.Forms.TextBox()
        Me.lblItemIconPath = New System.Windows.Forms.Label()
        Me.grpItemAction = New System.Windows.Forms.GroupBox()
        Me.lblItemActionType = New System.Windows.Forms.Label()
        Me.cbxItemActionType = New System.Windows.Forms.ComboBox()
        Me.lblItemActionFile = New System.Windows.Forms.Label()
        Me.txtItemActionFile = New System.Windows.Forms.TextBox()
        Me.lblItemActionArgs = New System.Windows.Forms.Label()
        Me.txtItemActionArgs = New System.Windows.Forms.TextBox()
        Me.cbxItemRestrict = New System.Windows.Forms.ComboBox()
        Me.chkItemRestrict = New System.Windows.Forms.CheckBox()
        Me.chkItemExtended = New System.Windows.Forms.CheckBox()
        Me.chkItemAdmin = New System.Windows.Forms.CheckBox()
        Me.txtItemFilter = New System.Windows.Forms.TextBox()
        Me.lblItemFilter = New System.Windows.Forms.Label()
        Me.txtItemText = New System.Windows.Forms.TextBox()
        Me.lblItemText = New System.Windows.Forms.Label()
        Me.cbxItemType = New System.Windows.Forms.ComboBox()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.grpItemConfig.SuspendLayout()
        Me.grpItemAction.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstMain
        '
        Me.lstMain.AllowColumnReorder = True
        Me.lstMain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colHeadType, Me.colHeadText, Me.colHeadIcon, Me.colHeadAdminIcon, Me.colHeadExtended, Me.colHeadFilter, Me.colHeadActionType, Me.colHeadActionSettings})
        Me.lstMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstMain.FullRowSelect = True
        Me.lstMain.GridLines = True
        Me.lstMain.HideSelection = False
        Me.lstMain.LabelEdit = True
        Me.lstMain.Location = New System.Drawing.Point(0, 0)
        Me.lstMain.Name = "lstMain"
        Me.lstMain.ShowItemToolTips = True
        Me.lstMain.Size = New System.Drawing.Size(969, 462)
        Me.lstMain.TabIndex = 0
        Me.lstMain.UseCompatibleStateImageBehavior = False
        Me.lstMain.View = System.Windows.Forms.View.Details
        '
        'colHeadType
        '
        Me.colHeadType.Tag = "colHeadType"
        Me.colHeadType.Text = "Type"
        Me.colHeadType.Width = 57
        '
        'colHeadText
        '
        Me.colHeadText.Tag = "colHeadText"
        Me.colHeadText.Text = "Text"
        Me.colHeadText.Width = 110
        '
        'colHeadIcon
        '
        Me.colHeadIcon.Tag = "colHeadIcon"
        Me.colHeadIcon.Text = "Icon"
        Me.colHeadIcon.Width = 300
        '
        'colHeadAdminIcon
        '
        Me.colHeadAdminIcon.Tag = "colHeadAdminIcon"
        Me.colHeadAdminIcon.Text = "Admin icon"
        Me.colHeadAdminIcon.Width = 64
        '
        'colHeadExtended
        '
        Me.colHeadExtended.Tag = "colHeadExtended"
        Me.colHeadExtended.Text = "Extended"
        Me.colHeadExtended.Width = 57
        '
        'colHeadFilter
        '
        Me.colHeadFilter.Tag = "colHeadFilter"
        Me.colHeadFilter.Text = "Filter"
        Me.colHeadFilter.Width = 185
        '
        'colHeadActionType
        '
        Me.colHeadActionType.Tag = "colHeadActionType"
        Me.colHeadActionType.Text = "Action Type"
        Me.colHeadActionType.Width = 110
        '
        'colHeadActionSettings
        '
        Me.colHeadActionSettings.Tag = "colHeadActionSettings"
        Me.colHeadActionSettings.Text = "Action Settings"
        Me.colHeadActionSettings.Width = 260
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnAdd.Location = New System.Drawing.Point(3, 3)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Add Item"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnDelete.Location = New System.Drawing.Point(3, 32)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete Item"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnMoveUp.Location = New System.Drawing.Point(84, 3)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveUp.TabIndex = 2
        Me.btnMoveUp.Text = "Move ▲"
        Me.btnMoveUp.UseVisualStyleBackColor = True
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnMoveDown.Location = New System.Drawing.Point(84, 32)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveDown.TabIndex = 3
        Me.btnMoveDown.Text = "Move ▼"
        Me.btnMoveDown.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnSave.Location = New System.Drawing.Point(165, 32)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(246, 32)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnShowConfig
        '
        Me.btnShowConfig.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnShowConfig.Location = New System.Drawing.Point(195, 3)
        Me.btnShowConfig.Name = "btnShowConfig"
        Me.btnShowConfig.Size = New System.Drawing.Size(96, 23)
        Me.btnShowConfig.TabIndex = 4
        Me.btnShowConfig.Text = "Show Config File"
        Me.btnShowConfig.UseVisualStyleBackColor = True
        '
        'grpItemConfig
        '
        Me.grpItemConfig.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpItemConfig.Controls.Add(Me.btnItemIconPick)
        Me.grpItemConfig.Controls.Add(Me.btnItemIconBrowse)
        Me.grpItemConfig.Controls.Add(Me.txtItemIconPath)
        Me.grpItemConfig.Controls.Add(Me.lblItemIconPath)
        Me.grpItemConfig.Controls.Add(Me.grpItemAction)
        Me.grpItemConfig.Controls.Add(Me.cbxItemRestrict)
        Me.grpItemConfig.Controls.Add(Me.chkItemRestrict)
        Me.grpItemConfig.Controls.Add(Me.chkItemExtended)
        Me.grpItemConfig.Controls.Add(Me.chkItemAdmin)
        Me.grpItemConfig.Controls.Add(Me.txtItemFilter)
        Me.grpItemConfig.Controls.Add(Me.lblItemFilter)
        Me.grpItemConfig.Controls.Add(Me.txtItemText)
        Me.grpItemConfig.Controls.Add(Me.lblItemText)
        Me.grpItemConfig.Controls.Add(Me.cbxItemType)
        Me.grpItemConfig.Location = New System.Drawing.Point(3, 61)
        Me.grpItemConfig.Name = "grpItemConfig"
        Me.grpItemConfig.Size = New System.Drawing.Size(327, 398)
        Me.grpItemConfig.TabIndex = 7
        Me.grpItemConfig.TabStop = False
        Me.grpItemConfig.Text = "Item Type:"
        '
        'btnItemIconPick
        '
        Me.btnItemIconPick.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnItemIconPick.Location = New System.Drawing.Point(162, 117)
        Me.btnItemIconPick.Name = "btnItemIconPick"
        Me.btnItemIconPick.Size = New System.Drawing.Size(75, 23)
        Me.btnItemIconPick.TabIndex = 6
        Me.btnItemIconPick.Text = "Pick Icon..."
        Me.btnItemIconPick.UseVisualStyleBackColor = True
        '
        'btnItemIconBrowse
        '
        Me.btnItemIconBrowse.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnItemIconBrowse.Location = New System.Drawing.Point(81, 117)
        Me.btnItemIconBrowse.Name = "btnItemIconBrowse"
        Me.btnItemIconBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnItemIconBrowse.TabIndex = 5
        Me.btnItemIconBrowse.Text = "Browse..."
        Me.btnItemIconBrowse.UseVisualStyleBackColor = True
        '
        'txtItemIconPath
        '
        Me.txtItemIconPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemIconPath.Location = New System.Drawing.Point(6, 94)
        Me.txtItemIconPath.Name = "txtItemIconPath"
        Me.txtItemIconPath.Size = New System.Drawing.Size(315, 20)
        Me.txtItemIconPath.TabIndex = 4
        '
        'lblItemIconPath
        '
        Me.lblItemIconPath.AutoSize = True
        Me.lblItemIconPath.Location = New System.Drawing.Point(6, 79)
        Me.lblItemIconPath.Name = "lblItemIconPath"
        Me.lblItemIconPath.Size = New System.Drawing.Size(56, 13)
        Me.lblItemIconPath.TabIndex = 3
        Me.lblItemIconPath.Text = "Icon Path:"
        '
        'grpItemAction
        '
        Me.grpItemAction.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpItemAction.Controls.Add(Me.lblItemActionType)
        Me.grpItemAction.Controls.Add(Me.cbxItemActionType)
        Me.grpItemAction.Controls.Add(Me.lblItemActionFile)
        Me.grpItemAction.Controls.Add(Me.txtItemActionFile)
        Me.grpItemAction.Controls.Add(Me.lblItemActionArgs)
        Me.grpItemAction.Controls.Add(Me.txtItemActionArgs)
        Me.grpItemAction.Location = New System.Drawing.Point(6, 254)
        Me.grpItemAction.Name = "grpItemAction"
        Me.grpItemAction.Size = New System.Drawing.Size(315, 138)
        Me.grpItemAction.TabIndex = 13
        Me.grpItemAction.TabStop = False
        Me.grpItemAction.Text = "Action"
        '
        'lblItemActionType
        '
        Me.lblItemActionType.AutoSize = True
        Me.lblItemActionType.Location = New System.Drawing.Point(6, 17)
        Me.lblItemActionType.Name = "lblItemActionType"
        Me.lblItemActionType.Size = New System.Drawing.Size(67, 13)
        Me.lblItemActionType.TabIndex = 0
        Me.lblItemActionType.Text = "Action Type:"
        '
        'cbxItemActionType
        '
        Me.cbxItemActionType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbxItemActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxItemActionType.FormattingEnabled = True
        Me.cbxItemActionType.Items.AddRange(New Object() {"Launch", "Run as Admin", "Execute", "Show in folder", "Windows Properties", "Launch Open With dialog", "Cut", "Copy", "Paste", "Paste as Hardlink", "Paste as Symlink", "Paste as Shortcut", "Paste as Junction", "Copy To", "Move To", "Delete to Recycle Bin (shift to delete permanently)", "Delete Permanently (shift to delete to recycle bin)"})
        Me.cbxItemActionType.Location = New System.Drawing.Point(6, 32)
        Me.cbxItemActionType.Name = "cbxItemActionType"
        Me.cbxItemActionType.Size = New System.Drawing.Size(303, 21)
        Me.cbxItemActionType.TabIndex = 1
        '
        'lblItemActionFile
        '
        Me.lblItemActionFile.AutoSize = True
        Me.lblItemActionFile.Location = New System.Drawing.Point(6, 57)
        Me.lblItemActionFile.Name = "lblItemActionFile"
        Me.lblItemActionFile.Size = New System.Drawing.Size(96, 13)
        Me.lblItemActionFile.TabIndex = 2
        Me.lblItemActionFile.Text = "Action File Pattern:"
        '
        'txtItemActionFile
        '
        Me.txtItemActionFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemActionFile.Location = New System.Drawing.Point(6, 72)
        Me.txtItemActionFile.Name = "txtItemActionFile"
        Me.txtItemActionFile.Size = New System.Drawing.Size(303, 20)
        Me.txtItemActionFile.TabIndex = 3
        '
        'lblItemActionArgs
        '
        Me.lblItemActionArgs.AutoSize = True
        Me.lblItemActionArgs.Location = New System.Drawing.Point(6, 96)
        Me.lblItemActionArgs.Name = "lblItemActionArgs"
        Me.lblItemActionArgs.Size = New System.Drawing.Size(130, 13)
        Me.lblItemActionArgs.TabIndex = 4
        Me.lblItemActionArgs.Text = "Action Arguments Pattern:"
        '
        'txtItemActionArgs
        '
        Me.txtItemActionArgs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemActionArgs.Location = New System.Drawing.Point(6, 111)
        Me.txtItemActionArgs.Name = "txtItemActionArgs"
        Me.txtItemActionArgs.Size = New System.Drawing.Size(303, 20)
        Me.txtItemActionArgs.TabIndex = 5
        '
        'cbxItemRestrict
        '
        Me.cbxItemRestrict.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbxItemRestrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxItemRestrict.FormattingEnabled = True
        Me.cbxItemRestrict.Items.AddRange(New Object() {"Files", "Directories", "Drives"})
        Me.cbxItemRestrict.Location = New System.Drawing.Point(105, 190)
        Me.cbxItemRestrict.Name = "cbxItemRestrict"
        Me.cbxItemRestrict.Size = New System.Drawing.Size(216, 21)
        Me.cbxItemRestrict.TabIndex = 10
        '
        'chkItemRestrict
        '
        Me.chkItemRestrict.AutoSize = True
        Me.chkItemRestrict.Location = New System.Drawing.Point(6, 192)
        Me.chkItemRestrict.Name = "chkItemRestrict"
        Me.chkItemRestrict.Size = New System.Drawing.Size(93, 17)
        Me.chkItemRestrict.TabIndex = 9
        Me.chkItemRestrict.Text = "Only show on:"
        Me.chkItemRestrict.UseVisualStyleBackColor = True
        '
        'chkItemExtended
        '
        Me.chkItemExtended.AutoSize = True
        Me.chkItemExtended.Location = New System.Drawing.Point(6, 169)
        Me.chkItemExtended.Name = "chkItemExtended"
        Me.chkItemExtended.Size = New System.Drawing.Size(166, 17)
        Me.chkItemExtended.TabIndex = 8
        Me.chkItemExtended.Text = "Show in Extended Only (Shift)"
        Me.chkItemExtended.UseVisualStyleBackColor = True
        '
        'chkItemAdmin
        '
        Me.chkItemAdmin.AutoSize = True
        Me.chkItemAdmin.Location = New System.Drawing.Point(6, 146)
        Me.chkItemAdmin.Name = "chkItemAdmin"
        Me.chkItemAdmin.Size = New System.Drawing.Size(147, 17)
        Me.chkItemAdmin.TabIndex = 7
        Me.chkItemAdmin.Text = "Show Admin icon/overlay"
        Me.chkItemAdmin.UseVisualStyleBackColor = True
        '
        'txtItemFilter
        '
        Me.txtItemFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemFilter.Location = New System.Drawing.Point(6, 228)
        Me.txtItemFilter.Name = "txtItemFilter"
        Me.txtItemFilter.Size = New System.Drawing.Size(315, 20)
        Me.txtItemFilter.TabIndex = 12
        '
        'lblItemFilter
        '
        Me.lblItemFilter.AutoSize = True
        Me.lblItemFilter.Location = New System.Drawing.Point(6, 213)
        Me.lblItemFilter.Name = "lblItemFilter"
        Me.lblItemFilter.Size = New System.Drawing.Size(162, 13)
        Me.lblItemFilter.TabIndex = 11
        Me.lblItemFilter.Text = "File Filter: (Separate filters with ';')"
        '
        'txtItemText
        '
        Me.txtItemText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemText.Location = New System.Drawing.Point(6, 55)
        Me.txtItemText.Name = "txtItemText"
        Me.txtItemText.Size = New System.Drawing.Size(315, 20)
        Me.txtItemText.TabIndex = 2
        '
        'lblItemText
        '
        Me.lblItemText.AutoSize = True
        Me.lblItemText.Location = New System.Drawing.Point(6, 40)
        Me.lblItemText.Name = "lblItemText"
        Me.lblItemText.Size = New System.Drawing.Size(31, 13)
        Me.lblItemText.TabIndex = 1
        Me.lblItemText.Text = "Text:"
        '
        'cbxItemType
        '
        Me.cbxItemType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbxItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxItemType.FormattingEnabled = True
        Me.cbxItemType.Items.AddRange(New Object() {"Menu Item", "Separator"})
        Me.cbxItemType.Location = New System.Drawing.Point(6, 15)
        Me.cbxItemType.Name = "cbxItemType"
        Me.cbxItemType.Size = New System.Drawing.Size(315, 21)
        Me.cbxItemType.TabIndex = 0
        '
        'scMain
        '
        Me.scMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.scMain.Location = New System.Drawing.Point(0, 0)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.lstMain)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.btnAdd)
        Me.scMain.Panel2.Controls.Add(Me.grpItemConfig)
        Me.scMain.Panel2.Controls.Add(Me.btnDelete)
        Me.scMain.Panel2.Controls.Add(Me.btnShowConfig)
        Me.scMain.Panel2.Controls.Add(Me.btnMoveUp)
        Me.scMain.Panel2.Controls.Add(Me.btnCancel)
        Me.scMain.Panel2.Controls.Add(Me.btnMoveDown)
        Me.scMain.Panel2.Controls.Add(Me.btnSave)
        Me.scMain.Size = New System.Drawing.Size(1306, 462)
        Me.scMain.SplitterDistance = 969
        Me.scMain.TabIndex = 0
        '
        'ContextMenuConfig
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1306, 462)
        Me.Controls.Add(Me.scMain)
        Me.Name = "ContextMenuConfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ContextMenuConfig"
        Me.grpItemConfig.ResumeLayout(False)
        Me.grpItemConfig.PerformLayout()
        Me.grpItemAction.ResumeLayout(False)
        Me.grpItemAction.PerformLayout()
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lstMain As System.Windows.Forms.ListView
    Friend WithEvents colHeadText As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadIcon As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadAdminIcon As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadExtended As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadFilter As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadActionType As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadActionSettings As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadType As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnShowConfig As System.Windows.Forms.Button
    Friend WithEvents grpItemConfig As System.Windows.Forms.GroupBox
    Friend WithEvents cbxItemType As System.Windows.Forms.ComboBox
    Friend WithEvents lblItemText As System.Windows.Forms.Label
    Friend WithEvents txtItemActionArgs As System.Windows.Forms.TextBox
    Friend WithEvents lblItemActionArgs As System.Windows.Forms.Label
    Friend WithEvents txtItemActionFile As System.Windows.Forms.TextBox
    Friend WithEvents lblItemActionFile As System.Windows.Forms.Label
    Friend WithEvents lblItemActionType As System.Windows.Forms.Label
    Friend WithEvents txtItemFilter As System.Windows.Forms.TextBox
    Friend WithEvents lblItemFilter As System.Windows.Forms.Label
    Friend WithEvents txtItemText As System.Windows.Forms.TextBox
    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
    Friend WithEvents chkItemAdmin As System.Windows.Forms.CheckBox
    Friend WithEvents chkItemExtended As System.Windows.Forms.CheckBox
    Friend WithEvents chkItemRestrict As System.Windows.Forms.CheckBox
    Friend WithEvents cbxItemRestrict As System.Windows.Forms.ComboBox
    Friend WithEvents cbxItemActionType As System.Windows.Forms.ComboBox
    Friend WithEvents grpItemAction As System.Windows.Forms.GroupBox
    Friend WithEvents lblItemIconPath As System.Windows.Forms.Label
    Friend WithEvents txtItemIconPath As System.Windows.Forms.TextBox
    Friend WithEvents btnItemIconBrowse As System.Windows.Forms.Button
    Friend WithEvents btnItemIconPick As System.Windows.Forms.Button
End Class
