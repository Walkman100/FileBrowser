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
        End If
    End Sub

    Private Sub btnAdd_Click() Handles btnAdd.Click
        lstMain.SelectedItems.Clear()
        Dim tmpListViewItem As ListViewItem = lstMain.Items.Add(CreateItem(New CtxMenu.EntryInfo))
        tmpListViewItem.Selected = True
        tmpListViewItem.Focused = True
    End Sub

    Private Sub btnDelete_Click() Handles btnDelete.Click
        For Each item As ListViewItem In lstMain.SelectedItems
            item.Remove()
        Next
    End Sub

    Private Sub btnMoveUp_Click() Handles btnMoveUp.Click

    End Sub

    Private Sub btnMoveDown_Click() Handles btnMoveDown.Click

    End Sub

    Private Sub btnSave_Click() Handles btnSave.Click

    End Sub

    Private Sub btnCancel_Click() Handles btnCancel.Click

    End Sub

    Private Sub btnShowConfig_Click() Handles btnShowConfig.Click

    End Sub
#End Region

#Region "Editing Data"
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
    End Sub

    Private Sub txtItemText_TextChanged() Handles txtItemText.TextChanged

    End Sub

    Private Sub chkItemAdmin_CheckedChanged() Handles chkItemAdmin.CheckedChanged

    End Sub

    Private Sub chkItemExtended_CheckedChanged() Handles chkItemExtended.CheckedChanged

    End Sub

    Private Sub chkItemRestrict_CheckedChanged() Handles chkItemRestrict.CheckedChanged
        cbxItemRestrict.Enabled = chkItemRestrict.Checked
    End Sub

    Private Sub cbxItemRestrict_SelectedIndexChanged() Handles cbxItemRestrict.SelectedIndexChanged

    End Sub

    Private Sub txtItemFilter_TextChanged() Handles txtItemFilter.TextChanged

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
    End Sub

    Private Sub txtItemActionFile_TextChanged() Handles txtItemActionFile.TextChanged

    End Sub

    Private Sub txtItemActionArgs_TextChanged() Handles txtItemActionArgs.TextChanged

    End Sub
#End Region
End Class