Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms

Public Class ExpandableList
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        lstMain.DoubleBuffered(True)
        MeHeightDiff = Me.Height - lstMain.Height + 28 ' column header height

        lstMain.Visible = False
        lblItemCount.Visible = True
    End Sub

    Private Const ItemSize As Integer = 17
    Private ReadOnly MeHeightDiff As Integer ' set in Me.New()

    Public ReadOnly Property ListView As ListView
        Get
            Return lstMain
        End Get
    End Property
    Public Property Items As List(Of ListViewItem)
        Get
            Return New List(Of ListViewItem)(lstMain.Items.Cast(Of ListViewItem))
        End Get
        Set(value As List(Of ListViewItem))
            lstMain.BeginUpdate()
            lstMain.Items.Clear()
            lstMain.Items.AddRange(value.ToArray())
            lstMain.EndUpdate()
            lblItemCount.Text = "Items: " & lstMain.Items.Count
            setHeight()
        End Set
    End Property

    Private isExpanded As Boolean = False
    Private oldHeight As Integer = -1

    Private Sub setHeight()
        If isExpanded Then
            Dim newHeight As Integer = MeHeightDiff + (ItemSize * lstMain.Items.Count)
            Me.Height = newHeight
        End If
    End Sub

    Private Sub btnExpandCollapse_Click(sender As Object, e As EventArgs) Handles btnExpandCollapse.Click
        If Not isExpanded Then
            oldHeight = Me.Height

            btnExpandCollapse.Text = "▲"
        Else
            If oldHeight = -1 Then Throw New InvalidOperationException("Control is in Expanded state with no saved size!")
            Me.Height = oldHeight
            oldHeight = -1

            btnExpandCollapse.Text = "▼"
        End If

        isExpanded = Not isExpanded
        lblItemCount.Visible = Not isExpanded
        lstMain.Visible = isExpanded
        setHeight()
    End Sub
End Class