Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class FileBrowser
    Private Sub FileBrowser_Load() Handles Me.Shown

    End Sub

#Region "TreeView"
    Private Sub treeViewDirs_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treeViewDirs.AfterSelect

    End Sub
    Private Sub treeViewDirs_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles treeViewDirs.BeforeExpand

    End Sub
    Private Sub treeViewDirs_AfterLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles treeViewDirs.AfterLabelEdit

    End Sub
#End Region

#Region "ListView"
    Private Sub lstCurrent_ItemActivate() Handles lstCurrent.ItemActivate

    End Sub
    Private Sub lstCurrent_AfterLabelEdit(sender As Object, e As LabelEditEventArgs) Handles lstCurrent.AfterLabelEdit

    End Sub
    Private Sub lstCurrent_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstCurrent.ColumnClick

    End Sub
#End Region

#Region "ToolStrips"
    Private Sub menuFileCreate_Click() Handles menuFileCreate.Click

    End Sub
    Private Sub menuFileRename_Click() Handles menuFileRename.Click

    End Sub
    Private Sub menuFileRecycle_Click() Handles menuFileRecycle.Click

    End Sub
    Private Sub menuFileDelete_Click() Handles menuFileDelete.Click

    End Sub
    Private Sub menuFileCopyTo_Click() Handles menuFileCopyTo.Click

    End Sub
    Private Sub menuFileMoveTo_Click() Handles menuFileMoveTo.Click

    End Sub
    Private Sub menuFileProperties_Click() Handles menuFileProperties.Click

    End Sub
    Private Sub menuFileLaunch_Click() Handles menuFileLaunch.Click

    End Sub
    Private Sub menuFileShowTarget_Click() Handles menuFileShowTarget.Click

    End Sub
    Private Sub menuFileExit_Click() Handles menuFileExit.Click

    End Sub

    Private Sub menuEditCut_Click() Handles menuEditCut.Click

    End Sub
    Private Sub menuEditCopy_Click() Handles menuEditCopy.Click

    End Sub
    Private Sub menuEditPaste_Click() Handles menuEditPaste.Click

    End Sub
    Private Sub menuEditPasteAsHardlink_Click() Handles menuEditPasteAsHardlink.Click

    End Sub
    Private Sub menuEditPasteAsSymlink_Click() Handles menuEditPasteAsSymlink.Click

    End Sub
    Private Sub menuEditPasteAsShortcut_Click() Handles menuEditPasteAsShortcut.Click

    End Sub
    Private Sub menuEditPasteAsJunction_Click() Handles menuEditPasteAsJunction.Click

    End Sub
    Private Sub menuEditSelectAll_Click() Handles menuEditSelectAll.Click

    End Sub
    Private Sub menuEditDeselectAll_Click() Handles menuEditDeselectAll.Click

    End Sub
    Private Sub menuEditInvert_Click() Handles menuEditInvert.Click

    End Sub

    Private Sub menuGoBack_Click() Handles menuGoBack.Click

    End Sub
    Private Sub menuGoForward_Click() Handles menuGoForward.Click

    End Sub
    Private Sub menuGoUp_Click() Handles menuGoUp.Click

    End Sub
    Private Sub menuGoRoot_Click() Handles menuGoRoot.Click

    End Sub
    Private Sub menuGoHome_Click() Handles menuGoHome.Click

    End Sub
    Private Sub menuGoRefresh_Click() Handles menuGoRefresh.Click

    End Sub
    Private Sub menuGoStop_Click() Handles menuGoStop.Click

    End Sub

    Private Sub menuToolsSettings_Click() Handles menuToolsSettings.Click

    End Sub
    Private Sub menuToolsContextMenu_Click() Handles menuToolsContextMenu.Click

    End Sub
    Private Sub menuToolsColumns_Click() Handles menuToolsColumns.Click

    End Sub
    Private Sub menuToolsResizeColumns_Click() Handles menuToolsResizeColumns.Click

    End Sub

    Private Sub btnGo_Click() Handles btnGo.Click

    End Sub

    Private Sub cbxURI_KeyUp(sender As Object, e As KeyEventArgs) Handles cbxURI.KeyUp

    End Sub
#End Region

#Region "Other UI Methods"
    Private Sub FileBrowser_Resize() Handles MyBase.Resize

    End Sub
    Private Sub handle_SelectedItemChanged() Handles lstCurrent.SelectedIndexChanged, treeViewDirs.AfterSelect

    End Sub

    Private Sub ShowContext(sender As Object, pos As Point)

    End Sub
    Private Sub handleMouseUp(sender As Object, e As MouseEventArgs) Handles lstCurrent.MouseUp, treeViewDirs.MouseUp

    End Sub
    Private Sub handleKeyUp(sender As Object, e As KeyEventArgs) Handles lstCurrent.KeyUp, treeViewDirs.KeyUp

    End Sub
    Private Sub ctxMenuL_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ctxMenuL.ItemClicked

    End Sub

    Public Sub RestartAsAdmin()

    End Sub

    Public Sub ErrorParser(ex As Exception)

    End Sub

    Private Sub menuFileCreate_Click(sender As Object, e As EventArgs) Handles menuFileCreate.Click

    End Sub

    Private Sub menuEditCut_Click(sender As Object, e As EventArgs) Handles menuEditCut.Click

    End Sub

    Private Sub menuEditCopy_Click(sender As Object, e As EventArgs) Handles menuEditCopy.Click

    End Sub

    Private Sub menuEditPaste_Click(sender As Object, e As EventArgs) Handles menuEditPaste.Click

    End Sub
#End Region
End Class
