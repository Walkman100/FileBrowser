Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports System.Xml

Public Class ContextMenuConfig
#Region "Helper Functions"
    Private Shared Function GetFilterText(itemInfo As CtxMenu.EntryInfo) As String
        Dim rtn As String = String.Empty
        If (itemInfo.FileOnly AndAlso itemInfo.DirectoryOnly AndAlso itemInfo.DriveOnly) OrElse
            (itemInfo.FileOnly AndAlso itemInfo.DirectoryOnly) OrElse
            (itemInfo.FileOnly AndAlso itemInfo.DriveOnly) OrElse
            (itemInfo.DirectoryOnly AndAlso itemInfo.DriveOnly) Then
            rtn = "No Items"
        ElseIf itemInfo.FileOnly Then
            rtn = "Files Only"
        ElseIf itemInfo.DirectoryOnly Then
            rtn = "Directories Only"
        ElseIf itemInfo.DriveOnly Then
            rtn = "Drives Only"
        End If

        If Not String.IsNullOrEmpty(itemInfo.Filter) Then
            If rtn = String.Empty Then
                rtn = "Files filtered by: " & itemInfo.Filter
            Else
                rtn &= ", filtered by: " & itemInfo.Filter
            End If
        End If

        Return rtn
    End Function

    Private Shared Function GetActionSettingsText(itemInfo As CtxMenu.EntryInfo) As String
        If String.IsNullOrEmpty(itemInfo.ActionArgs1) Then Return Nothing
        If String.IsNullOrEmpty(itemInfo.ActionArgs2) Then Return itemInfo.ActionArgs1
        If itemInfo.ActionType = CtxMenu.ActionType.Properties Then Return $"Path format: ""{itemInfo.ActionArgs1}"" Properties Tab: {itemInfo.ActionArgs2}"

        Return $"Path format: ""{itemInfo.ActionArgs1}"" Arguments format: ""{itemInfo.ActionArgs2}"""
    End Function

    Private Shared Function UpdateItem(item As ListViewItem, itemInfo As CtxMenu.EntryInfo) As ListViewItem
        item.Tag = itemInfo
        item.Text = itemInfo.EntryType.ToString()
        item.SubItems.Item(1).Text = itemInfo.Text
        item.SubItems.Item(2).Text = itemInfo.IconPath
        item.SubItems.Item(3).Text = itemInfo.AdminIcon.ToString()
        item.SubItems.Item(4).Text = itemInfo.IsSubItem.ToString()
        item.SubItems.Item(5).Text = itemInfo.Extended.ToString()
        item.SubItems.Item(6).Text = GetFilterText(itemInfo)
        item.SubItems.Item(7).Text = itemInfo.ActionType.ToString()
        item.SubItems.Item(8).Text = GetActionSettingsText(itemInfo)
        Return item
    End Function

    Private Shared Function CreateItem(itemInfo As CtxMenu.EntryInfo) As ListViewItem
        Return UpdateItem(New ListViewItem({"", "", "", "", "", "", "", "", ""}), itemInfo)
    End Function

    Private Shared Function GetItemInfo(item As ListViewItem) As CtxMenu.EntryInfo
        Return DirectCast(item.Tag, CtxMenu.EntryInfo)
    End Function
#End Region

#Region "Saving & Loading Data"
    Private _settingsPath As String

    Public Sub Init()
        Dim configFileName As String = "FileBrowser.ContextMenu.xml"

        If Helpers.GetOS() = OS.Windows Then
            If Not       Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS")) Then
                Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS"))
            End If
            _settingsPath =               Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS", configFileName)
        Else
            If Not       Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS")) Then
                Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS"))
            End If
            _settingsPath =               Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS", configFileName)
        End If

        If                    File.Exists(Path.Combine(Application.StartupPath, configFileName)) Then
            _settingsPath =               Path.Combine(Application.StartupPath, configFileName)
        ElseIf                File.Exists(configFileName) Then
            _settingsPath =  New FileInfo(configFileName).FullName
        End If

        If File.Exists(_settingsPath) Then
            LoadSettings()
        Else
            'LoadInitialSettings()
        End If
    End Sub

    Private Sub LoadSettings()
        Using reader As XmlReader = XmlReader.Create(_settingsPath)
            Try
                reader.Read()
            Catch ex As XmlException
                reader.Close()
                Return
            End Try

            Dim elementAttribute As String
            If reader.IsStartElement AndAlso reader.Name = "FileBrowser.ContextMenu" Then
                If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "Items" Then
                    lstMain.Items.Clear()
                    While reader.IsStartElement
                        If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "Item" Then
                            Dim itemInfo As New CtxMenu.EntryInfo

                            elementAttribute = reader("type")
                            If elementAttribute IsNot Nothing Then [Enum].TryParse(elementAttribute, itemInfo.EntryType)
                            elementAttribute = reader("text")
                            If elementAttribute IsNot Nothing Then itemInfo.Text = elementAttribute
                            elementAttribute = reader("icon")
                            If elementAttribute IsNot Nothing Then itemInfo.IconPath = elementAttribute
                            elementAttribute = reader("adminIcon")
                            If elementAttribute IsNot Nothing Then Boolean.TryParse(elementAttribute, itemInfo.AdminIcon)
                            elementAttribute = reader("isSubItem")
                            If elementAttribute IsNot Nothing Then Boolean.TryParse(elementAttribute, itemInfo.IsSubItem)
                            elementAttribute = reader("extended")
                            If elementAttribute IsNot Nothing Then Boolean.TryParse(elementAttribute, itemInfo.Extended)
                            elementAttribute = reader("fileOnly")
                            If elementAttribute IsNot Nothing Then Boolean.TryParse(elementAttribute, itemInfo.FileOnly)
                            elementAttribute = reader("directoryOnly")
                            If elementAttribute IsNot Nothing Then Boolean.TryParse(elementAttribute, itemInfo.DirectoryOnly)
                            elementAttribute = reader("driveOnly")
                            If elementAttribute IsNot Nothing Then Boolean.TryParse(elementAttribute, itemInfo.DriveOnly)
                            elementAttribute = reader("filter")
                            If elementAttribute IsNot Nothing Then itemInfo.Filter = elementAttribute
                            elementAttribute = reader("actionType")
                            If elementAttribute IsNot Nothing Then [Enum].TryParse(elementAttribute, itemInfo.ActionType)
                            elementAttribute = reader("actionArgs1")
                            If elementAttribute IsNot Nothing Then itemInfo.ActionArgs1 = elementAttribute
                            elementAttribute = reader("actionArgs2")
                            If elementAttribute IsNot Nothing Then itemInfo.ActionArgs2 = elementAttribute

                            lstMain.Items.Add(CreateItem(itemInfo))
                        End If
                    End While
                End If
                If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "Settings" Then
                    If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "ColumnSettings" Then
                        While reader.IsStartElement
                            If reader.Read AndAlso reader.IsStartElement() Then
                                Dim col As ColumnHeader = lstMain.Columns.Cast(Of ColumnHeader).First(Function(c As ColumnHeader)
                                                                                                          Return DirectCast(c.Tag, String) = "colHead" & reader.Name
                                                                                                      End Function)
                                If col IsNot Nothing Then
                                    elementAttribute = reader("index")
                                    If elementAttribute IsNot Nothing Then Integer.TryParse(elementAttribute, col.DisplayIndex)
                                    elementAttribute = reader("width")
                                    If elementAttribute IsNot Nothing Then Integer.TryParse(elementAttribute, col.Width)
                                End If
                            End If
                        End While
                    End If
                    If reader.Read AndAlso reader.IsStartElement AndAlso reader.Name = "WindowSize" Then
                        elementAttribute = reader("width")
                        If elementAttribute IsNot Nothing Then Integer.TryParse(elementAttribute, Me.Width)
                        elementAttribute = reader("height")
                        If elementAttribute IsNot Nothing Then Integer.TryParse(elementAttribute, Me.Height)
                    End If
                    If reader.Read AndAlso reader.IsStartElement AndAlso reader.Name = "SplitPosition" Then
                        elementAttribute = reader("pos")
                        If elementAttribute IsNot Nothing Then Integer.TryParse(elementAttribute, scMain.SplitterDistance)
                    End If
                End If
            End If
        End Using

        Dim items As New List(Of CtxMenu.EntryInfo)
        Dim lastParent As CtxMenu.EntryInfo? = Nothing
        For Each item As ListViewItem In lstMain.Items
            Dim itemInfo As CtxMenu.EntryInfo = GetItemInfo(item)

            If itemInfo.IsSubItem AndAlso lastParent IsNot Nothing Then
                If lastParent.Value.SubItems Is Nothing Then
                    Dim tmp = lastParent.Value
                    tmp.SubItems = New List(Of CtxMenu.EntryInfo) From {itemInfo}
                    lastParent = tmp
                Else
                    lastParent.Value.SubItems.Add(itemInfo)
                End If
            Else
                If lastParent IsNot Nothing Then
                    items.Add(lastParent.Value)
                End If
                lastParent = itemInfo
            End If
        Next
        If lastParent IsNot Nothing Then
            items.Add(lastParent.Value)
        End If

        FileBrowser.ctxMenu.BuildMenu(FileBrowser.ctxMenuL, items)
    End Sub

    Private Sub SaveSettings()
        Using writer As XmlWriter = XmlWriter.Create(_settingsPath, New XmlWriterSettings With {.Indent = True})
            writer.WriteStartDocument()
            writer.WriteStartElement("FileBrowser.ContextMenu")

            writer.WriteStartElement("Items")
            For Each item As CtxMenu.EntryInfo In lstMain.Items.Cast(Of ListViewItem).Select(AddressOf GetItemInfo)
                writer.WriteStartElement("Item")
                writer.WriteAttributeString("type", item.EntryType.ToString())
                writer.WriteAttributeString("text", item.Text)
                writer.WriteAttributeString("icon", item.IconPath)
                writer.WriteAttributeString("adminIcon", item.AdminIcon.ToString())
                writer.WriteAttributeString("isSubItem", item.IsSubItem.ToString())
                writer.WriteAttributeString("extended", item.Extended.ToString())
                writer.WriteAttributeString("fileOnly", item.FileOnly.ToString())
                writer.WriteAttributeString("directoryOnly", item.DirectoryOnly.ToString())
                writer.WriteAttributeString("driveOnly", item.DriveOnly.ToString())
                writer.WriteAttributeString("filter", item.Filter)
                writer.WriteAttributeString("actionType", item.ActionType.ToString())
                writer.WriteAttributeString("actionArgs1", item.ActionArgs1)
                writer.WriteAttributeString("actionArgs2", item.ActionArgs2)
                writer.WriteEndElement() ' Item
            Next
            writer.WriteEndElement() ' Items

            writer.WriteStartElement("Settings")

            writer.WriteStartElement("ColumnSettings")
            For Each column As ColumnHeader In lstMain.Columns
                ' column.Name isn't set, so the name is stored in the Tag, and we don't need "colHead"
                writer.WriteStartElement(DirectCast(column.Tag, String).Substring(7))
                writer.WriteAttributeString("index", column.DisplayIndex.ToString())
                writer.WriteAttributeString("width", column.Width.ToString())
                writer.WriteEndElement()
            Next
            writer.WriteEndElement() ' ColumnSettings

            writer.WriteStartElement("WindowSize")
            writer.WriteAttributeString("width", Me.Width.ToString())
            writer.WriteAttributeString("height", Me.Height.ToString())
            writer.WriteEndElement() ' WindowSize

            writer.WriteStartElement("SplitPosition")
            writer.WriteAttributeString("pos", scMain.SplitterDistance.ToString())
            writer.WriteEndElement() ' SplitPosition

            writer.WriteEndElement() ' Settings

            writer.WriteEndElement() ' FileBrowsser.ContextMenu
            writer.WriteEndDocument()
        End Using
    End Sub
#End Region

#Region "Item / Window Management"
    Private Sub MeShown() Handles Me.Shown
        lstMain.Tag = Settings.Theme.ListViewColumnColors
        AddHandler lstMain.DrawColumnHeader, AddressOf WalkmanLib.CustomPaint.ListView_DrawCustomColumnHeader
        AddHandler lstMain.DrawItem, AddressOf WalkmanLib.CustomPaint.ListView_DrawDefaultItem
        AddHandler lstMain.DrawSubItem, AddressOf WalkmanLib.CustomPaint.ListView_DrawDefaultSubItem

        WalkmanLib.ApplyTheme(Settings.Theme, Me)
        If Settings.Theme = WalkmanLib.Theme.Dark Then
            lstMain.OwnerDraw = True
        End If

        lstMain.DoubleBuffered(True)
        lstMain_SelectedIndexChanged()
        cbxItemType_SelectedIndexChanged()
        cbxItemActionType_SelectedIndexChanged()
        If Helpers.GetOS <> OS.Windows Then btnItemIconPick.Visible = False
    End Sub

    Private Sub lstMain_SelectedIndexChanged() Handles lstMain.SelectedIndexChanged
        btnDelete.Enabled = (lstMain.SelectedIndices.Count > 0)
        btnMoveUp.Enabled = (lstMain.SelectedIndices.Count > 0)
        btnMoveDown.Enabled = (lstMain.SelectedIndices.Count > 0)
        grpItemConfig.Enabled = (lstMain.SelectedIndices.Count > 0)

        If Not lstMain.SelectedIndices.Count > 0 Then
            cbxItemType.SelectedIndex = -1
            txtItemText.Text = Nothing
            txtItemIconPath.Text = Nothing
            chkItemAdmin.Checked = False
            chkItemIsSubItem.Checked = False
            chkItemExtended.Checked = False
            chkItemRestrict.Checked = False
            cbxItemRestrict.SelectedIndex = -1
            txtItemFilter.Text = Nothing
            cbxItemActionType.SelectedIndex = -1
            txtItemActionFile.Text = Nothing
            lblItemActionArgs.Text = "Action Arguments Pattern:"
            txtItemActionArgs.Text = Nothing
        Else
            LoadItemInfo(GetItemInfo(lstMain.SelectedItems.Item(0)))
        End If
    End Sub

    Private Sub btnAdd_Click() Handles btnAdd.Click
        lstMain.SelectedItems.Clear()
        Dim tmpListViewItem As ListViewItem = lstMain.Items.Add(
            CreateItem(New CtxMenu.EntryInfo() With {.ActionType = CtxMenu.ActionType.None})
        )

        tmpListViewItem.Selected = True
        tmpListViewItem.Focused = True
    End Sub

    Private Sub lstMain_KeyUp(sender As Object, e As KeyEventArgs) Handles lstMain.KeyUp
        If e.KeyCode = Keys.Delete Then
            btnDelete_Click()
        End If
    End Sub
    Private Sub btnDelete_Click() Handles btnDelete.Click
        lstMain.BeginUpdate()
        For Each item As ListViewItem In lstMain.SelectedItems
            item.Remove()
        Next
        lstMain.EndUpdate()
    End Sub

    Private Sub btnMoveUp_Click() Handles btnMoveUp.Click
        lstMain.BeginUpdate()
        allowEdit = False

        For Each item As ListViewItem In lstMain.SelectedItems
            Dim oldIndex As Integer = item.Index
            item.Remove()

            If oldIndex = 0 Then
                lstMain.Items.Add(item)
            Else
                lstMain.Items.Insert(oldIndex - 1, item)
            End If
        Next

        lstMain.EndUpdate()
        allowEdit = True
    End Sub

    Private Sub btnMoveDown_Click() Handles btnMoveDown.Click
        allowEdit = False
        lstMain.BeginUpdate()

        For Each item As ListViewItem In lstMain.SelectedItems.Cast(Of ListViewItem).Reverse()
            Dim oldIndex As Integer = item.Index
            item.Remove()

            If oldIndex = lstMain.Items.Count Then
                lstMain.Items.Insert(0, item)
            Else
                lstMain.Items.Insert(oldIndex + 1, item)
            End If
        Next

        lstMain.EndUpdate()
        allowEdit = True
    End Sub

    Private Sub btnSave_Click() Handles btnSave.Click
        SaveSettings()
        LoadSettings() ' reload context menu
        Me.Close()
    End Sub

    Private Sub btnCancel_Click() Handles btnCancel.Click
        LoadSettings()
        Me.Close()
    End Sub

    Private Sub btnShowConfig_Click() Handles btnShowConfig.Click
        Helpers.ShowFileExternal(_settingsPath)
    End Sub
#End Region

#Region "Editing ListView Data"
    Private Sub LoadItemInfo(item As CtxMenu.EntryInfo)
        cbxItemType.SelectedIndex = item.EntryType
        txtItemText.Text = item.Text
        txtItemIconPath.Text = item.IconPath
        chkItemAdmin.Checked = item.AdminIcon
        chkItemIsSubItem.Checked = item.IsSubItem
        chkItemExtended.Checked = item.Extended
        chkItemRestrict.Checked = item.FileOnly OrElse item.DirectoryOnly OrElse item.DriveOnly
        cbxItemRestrict.SelectedIndex = If(item.FileOnly, 0, If(item.DirectoryOnly, 1, If(item.DriveOnly, 2, -1)))
        txtItemFilter.Text = item.Filter
        cbxItemActionType.SelectedIndex = item.ActionType
        txtItemActionFile.Text = item.ActionArgs1
        txtItemActionArgs.Text = item.ActionArgs2
    End Sub

    '  HACK(ish): Make sure properties aren't overwritten when selecting multiple items...
    Dim allowEdit As Boolean = True
    Private Sub lstMain_KeyOrMouseDown() Handles lstMain.MouseDown, lstMain.KeyDown
        allowEdit = False
    End Sub
    Private Sub lstMain_KeyOrMouseUp() Handles lstMain.MouseUp, lstMain.KeyUp
        allowEdit = True
    End Sub

    Private Sub cbxItemType_SelectedIndexChanged() Handles cbxItemType.SelectedIndexChanged
        lblItemText.Enabled = (cbxItemType.SelectedIndex = 0)
        txtItemText.Enabled = (cbxItemType.SelectedIndex = 0)
        lblItemIconPath.Enabled = (cbxItemType.SelectedIndex = 0)
        txtItemIconPath.Enabled = (cbxItemType.SelectedIndex = 0)
        btnItemIconBrowse.Enabled = (cbxItemType.SelectedIndex = 0)
        btnItemIconPick.Enabled = (cbxItemType.SelectedIndex = 0)
        chkItemAdmin.Enabled = (cbxItemType.SelectedIndex = 0)
        chkItemIsSubItem.Enabled = (cbxItemType.SelectedIndex = 0)
        chkItemExtended.Enabled = (cbxItemType.SelectedIndex = 0)
        chkItemRestrict.Enabled = (cbxItemType.SelectedIndex = 0)
        cbxItemRestrict.Enabled = cbxItemType.SelectedIndex = 0 AndAlso chkItemRestrict.Checked
        lblItemFilter.Enabled = (cbxItemType.SelectedIndex = 0)
        txtItemFilter.Enabled = (cbxItemType.SelectedIndex = 0)
        grpItemAction.Enabled = (cbxItemType.SelectedIndex = 0)

        If allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.EntryType = DirectCast(cbxItemType.SelectedIndex, CtxMenu.EntryType)

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub

    Private Sub txtItemText_TextChanged() Handles txtItemText.TextChanged
        If allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.Text = txtItemText.Text

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub
    Private Sub txtItemIconPath_TextChanged() Handles txtItemIconPath.TextChanged
        Try
            imgItemIcon.Image = ImageHandling.GetIcon(txtItemIconPath.Text)?.ToBitmap()
        Catch
            imgItemIcon.ImageLocation = Environment.ExpandEnvironmentVariables(txtItemIconPath.Text)
        End Try

        If chkItemAdmin.Checked AndAlso imgItemIcon.Image IsNot Nothing Then
            imgItemIcon.Image = ImageHandling.AddAdminOverlay(imgItemIcon.Image)
        End If

        If allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.IconPath = txtItemIconPath.Text

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub
    Private Sub btnItemIconBrowse_Click() Handles btnItemIconBrowse.Click
        Dim ofd As New OpenFileDialog With {
            .Title = "Select Menu Icon:",
            .Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.ico;*.icl;*.exe;*.dll|Icon Files|*.ico;*.icl;*.exe;*.dll|All Files|*.*",
            .FileName = ImageHandling.TransformResourcePath(If(txtItemIconPath.Text, ""))
        }
        If Not String.IsNullOrEmpty(txtItemIconPath.Text) Then
            ofd.InitialDirectory = Path.GetDirectoryName(Environment.ExpandEnvironmentVariables(ImageHandling.TransformResourcePath(txtItemIconPath.Text)))
        End If

        If ofd.ShowDialog = DialogResult.OK Then
            txtItemIconPath.Text = ofd.FileName
        End If
    End Sub
    Private Sub btnItemIconPick_Click() Handles btnItemIconPick.Click
        Dim iconIndex As Integer
        Dim iconResource As String = ImageHandling.TransformResourcePath(txtItemIconPath.Text, iconIndex)

        If WalkmanLib.PickIconDialogShow(iconResource, iconIndex, Me.Handle) Then
            txtItemIconPath.Text = iconResource & "," & iconIndex
        End If
    End Sub
    Private Sub chkItemAdmin_CheckedChanged() Handles chkItemAdmin.CheckedChanged
        txtItemIconPath_TextChanged() ' update admin icon in preview

        If allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.AdminIcon = chkItemAdmin.Checked

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub
    Private Sub chkItemIsSubItem_CheckedChanged() Handles chkItemIsSubItem.CheckedChanged
        If allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.IsSubItem = chkItemIsSubItem.Checked

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub
    Private Sub chkItemExtended_CheckedChanged() Handles chkItemExtended.CheckedChanged
        If allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.Extended = chkItemExtended.Checked

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub
    Private Sub chkItemRestrict_CheckedChanged() Handles chkItemRestrict.CheckedChanged
        cbxItemRestrict.Enabled = chkItemRestrict.Checked

        If chkItemRestrict.Checked Then
            If cbxItemRestrict.SelectedIndex <> -1 Then cbxItemRestrict_SelectedIndexChanged()
        ElseIf allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.FileOnly = False
                info.DirectoryOnly = False
                info.DriveOnly = False

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub
    Private Sub cbxItemRestrict_SelectedIndexChanged() Handles cbxItemRestrict.SelectedIndexChanged
        If allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.FileOnly = (cbxItemRestrict.SelectedIndex = 0)
                info.DirectoryOnly = (cbxItemRestrict.SelectedIndex = 1)
                info.DriveOnly = (cbxItemRestrict.SelectedIndex = 2)

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub
    Private Sub txtItemFilter_TextChanged() Handles txtItemFilter.TextChanged
        If allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.Filter = txtItemFilter.Text

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub
    Private Sub cbxItemActionType_SelectedIndexChanged() Handles cbxItemActionType.SelectedIndexChanged
        Select Case cbxItemActionType.SelectedIndex
            Case 0, 1, 2, 4
                lblItemActionFile.Enabled = True
                txtItemActionFile.Enabled = True
                toolStripItemActionFileBtnInsert.Enabled = True
                lblItemActionArgs.Enabled = True
                txtItemActionArgs.Enabled = True
                toolStripItemActionArgsBtnInsert.Enabled = True
            Case 3, 5, 6
                lblItemActionFile.Enabled = True
                txtItemActionFile.Enabled = True
                toolStripItemActionFileBtnInsert.Enabled = True
                lblItemActionArgs.Enabled = False
                txtItemActionArgs.Enabled = False
                toolStripItemActionArgsBtnInsert.Enabled = False
            Case Else
                lblItemActionFile.Enabled = False
                txtItemActionFile.Enabled = False
                toolStripItemActionFileBtnInsert.Enabled = False
                lblItemActionArgs.Enabled = False
                txtItemActionArgs.Enabled = False
                toolStripItemActionArgsBtnInsert.Enabled = False
        End Select

        If cbxItemActionType.SelectedIndex = 4 Then
            lblItemActionArgs.Text = "Properties Window Tab:"
        Else
            lblItemActionArgs.Text = "Action Arguments Pattern:"
        End If

        If allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.ActionType = DirectCast(cbxItemActionType.SelectedIndex, CtxMenu.ActionType)

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub
    Private Sub txtItemActionFile_TextChanged() Handles txtItemActionFile.TextChanged
        If allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.ActionArgs1 = txtItemActionFile.Text

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub
    Private Sub txtItemActionArgs_TextChanged() Handles txtItemActionArgs.TextChanged
        If allowEdit Then
            lstMain.BeginUpdate()
            For Each item As ListViewItem In lstMain.SelectedItems
                Dim info As CtxMenu.EntryInfo = GetItemInfo(item)
                info.ActionArgs2 = txtItemActionArgs.Text

                UpdateItem(item, info)
            Next
            lstMain.EndUpdate()
        End If
    End Sub

    Private Sub toolStripItemActionFileInsertPath_Click(sender As Object, e As EventArgs) Handles _
            toolStripItemActionFileInsertPath.Click,        toolStripItemActionArgsInsertPath.Click,
            toolStripItemActionFileInsertDirectory.Click,   toolStripItemActionArgsInsertDirectory.Click,
            toolStripItemActionFileInsertName.Click,        toolStripItemActionArgsInsertName.Click,
            toolStripItemActionFileInsertNameNoExt.Click,   toolStripItemActionArgsInsertNameNoExt.Click,
            toolStripItemActionFileInsertExt.Click,         toolStripItemActionArgsInsertExt.Click,
            toolStripItemActionFileInsertOpenWith.Click,    toolStripItemActionArgsInsertOpenWith.Click,
            toolStripItemActionFileInsertTarget.Click,      toolStripItemActionArgsInsertTarget.Click,
            toolStripItemActionFileInsertWalkmanUtils.Click,toolStripItemActionArgsInsertWalkmanUtils.Click
        Dim tSI As ToolStripItem = CType(sender, ToolStripItem)
        Dim text As String = Nothing

        Select Case tSI.Name.Substring(23)
            Case "InsertPath"
                text = "{path}"
            Case "InsertDirectory"
                text = "{directory}"
            Case "InsertName"
                text = "{name}"
            Case "InsertNameNoExt"
                text = "{namenoext}"
            Case "InsertExt"
                text = "{fileext}"
            Case "InsertOpenWith"
                text = "{openwith}"
            Case "InsertTarget"
                text = "{target}"
            Case "InsertWalkmanUtils"
                text = "{walkmanutils}"
        End Select

        Dim parent As ToolStripDropDownButton = CType(tSI.OwnerItem, ToolStripDropDownButton)
        If parent Is toolStripItemActionFileBtnInsert Then
            txtItemActionFile.Text &= text
        ElseIf parent Is toolStripItemActionArgsBtnInsert Then
            txtItemActionArgs.Text &= text
        End If
    End Sub
#End Region
End Class