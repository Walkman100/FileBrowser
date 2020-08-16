Imports System.Linq
Imports System.Windows.Forms

Public Class ContextMenuConfig
#Region "Helper Functions"
    Private Function GetFilterText(itemInfo As CtxMenu.EntryInfo) As String
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

    Private Function GetActionSettingsText(itemInfo As CtxMenu.EntryInfo) As String
        If String.IsNullOrEmpty(itemInfo.ActionArgs1) Then Return Nothing
        If String.IsNullOrEmpty(itemInfo.ActionArgs2) Then Return itemInfo.ActionArgs1

        Return $"Path format: ""{itemInfo.ActionArgs1}"" Arguments format: ""{itemInfo.ActionArgs2}"""
    End Function

    Private Function UpdateItem(item As ListViewItem, itemInfo As CtxMenu.EntryInfo) As ListViewItem
        item.Tag = itemInfo
        item.Text = itemInfo.EntryType.ToString()
        item.SubItems.Item(1).Text = itemInfo.Text
        item.SubItems.Item(2).Text = itemInfo.IconPath
        item.SubItems.Item(3).Text = itemInfo.AdminIcon.ToString()
        item.SubItems.Item(4).Text = itemInfo.Extended.ToString()
        item.SubItems.Item(5).Text = GetFilterText(itemInfo)
        item.SubItems.Item(6).Text = itemInfo.ActionType.ToString()
        item.SubItems.Item(7).Text = GetActionSettingsText(itemInfo)
        Return item
    End Function

    Private Function CreateItem(itemInfo As CtxMenu.EntryInfo) As ListViewItem
        Return UpdateItem(New ListViewItem({"", "", "", "", "", "", "", ""}), itemInfo)
    End Function

    Private Function GetItemInfo(item As ListViewItem) As CtxMenu.EntryInfo
        Return DirectCast(item.Tag, CtxMenu.EntryInfo)
    End Function
#End Region

#Region "Item / Window Management"
    Private Sub WndShown() Handles Me.Shown
        lstMain.DoubleBuffered(True)
        lstMain_SelectedIndexChanged()
        cbxItemType_SelectedIndexChanged()
        cbxItemActionType_SelectedIndexChanged()
    End Sub

    Private Sub lstMain_SelectedIndexChanged() Handles lstMain.SelectedIndexChanged
        btnDelete.Enabled = (lstMain.SelectedIndices.Count > 0)
        btnMoveUp.Enabled = (lstMain.SelectedIndices.Count > 0)
        btnMoveDown.Enabled = (lstMain.SelectedIndices.Count > 0)
        grpItemConfig.Enabled = (lstMain.SelectedIndices.Count > 0)

        If Not lstMain.SelectedIndices.Count > 0 Then
            cbxItemType.SelectedIndex = -1
            txtItemText.Text = Nothing
            chkItemAdmin.Checked = False
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

        '               VB.Net declares arrays Index-based... (0 = 1 item)
        Dim selectedItemArray(lstMain.SelectedItems.Count - 1) As ListViewItem
        lstMain.SelectedItems.CopyTo(selectedItemArray, 0)

        For Each item As ListViewItem In selectedItemArray.Reverse()
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

    End Sub

    Private Sub btnCancel_Click() Handles btnCancel.Click

    End Sub

    Private Sub btnShowConfig_Click() Handles btnShowConfig.Click

    End Sub
#End Region

#Region "Editing ListView Data"
    Private Sub LoadItemInfo(item As CtxMenu.EntryInfo)
        cbxItemType.SelectedIndex = item.EntryType
        txtItemText.Text = item.Text
        chkItemAdmin.Checked = item.AdminIcon
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
        chkItemAdmin.Enabled = (cbxItemType.SelectedIndex = 0)
        chkItemExtended.Enabled = (cbxItemType.SelectedIndex = 0)
        chkItemRestrict.Enabled = (cbxItemType.SelectedIndex = 0)
        cbxItemRestrict.Enabled = If(cbxItemType.SelectedIndex <> 0, False, chkItemRestrict.Checked)
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
    Private Sub chkItemAdmin_CheckedChanged() Handles chkItemAdmin.CheckedChanged
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

        If allowEdit AndAlso Not chkItemRestrict.Checked Then
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
                lblItemActionArgs.Enabled = True
                txtItemActionArgs.Enabled = True
            Case 3, 5
                lblItemActionFile.Enabled = True
                txtItemActionFile.Enabled = True
                lblItemActionArgs.Enabled = False
                txtItemActionArgs.Enabled = False
            Case Else
                lblItemActionFile.Enabled = False
                txtItemActionFile.Enabled = False
                lblItemActionArgs.Enabled = False
                txtItemActionArgs.Enabled = False
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
#End Region
End Class