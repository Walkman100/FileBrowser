Imports System.Drawing
Imports System.Windows.Forms

Public Class ColumnConfig
    Inherits Form

    Private WithEvents lst As ListView
    Private WithEvents btn As Button

    Sub New()
        lst = New ListView With {
            .View = View.Details,
            .Dock = DockStyle.Fill,
            .HeaderStyle = ColumnHeaderStyle.Nonclickable,
            .AllowColumnReorder = False,
            .CheckBoxes = True,
            .GridLines = True
        }
        lst.Columns.Add("Column").Width = 140

        btn = New Button With {
            .Text = "Close",
            .Location = New Point(0, -10)
        }

        Text = "Column Visibility"
        StartPosition = FormStartPosition.CenterParent
        Height = 370
        Width = 160
        CancelButton = btn
        AcceptButton = btn

        Controls.Add(lst)
        Controls.Add(btn)

        For Each column As ColumnHeader In FileBrowser.lstCurrent.Columns
            lst.Items.Add(column.Text).Checked = (column.Width > 0)
        Next
    End Sub

    Sub MeShown() Handles Me.Shown
        ' only add handler after ListViewItems are created and shown
        AddHandler lst.ItemChecked, AddressOf lst_ItemChecked
        CenterToParent()
    End Sub

    Sub btn_Click() Handles btn.Click
        Close()
    End Sub

    Sub lst_ItemChecked(sender As Object, e As ItemCheckedEventArgs)
        FileBrowser.lstCurrent.Columns(e.Item.Index).Width = If(e.Item.Checked, 100, 0)
    End Sub
End Class
