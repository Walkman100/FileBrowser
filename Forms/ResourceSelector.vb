Imports System
Imports System.Linq
Imports System.Windows.Forms

Public Class ResourceSelector
    Sub New(theme As WalkmanLib.Theme)
        ' This call is required by the designer.
        InitializeComponent()

        lstResources.DoubleBuffered(True)

        AddHandler lstResources.DrawItem, AddressOf WalkmanLib.CustomPaint.ListView_DrawDefaultItem
        AddHandler lstResources.DrawSubItem, AddressOf WalkmanLib.CustomPaint.ListView_DrawDefaultSubItem
        AddHandler lstResources.DrawColumnHeader, AddressOf WalkmanLib.CustomPaint.ListView_DrawCustomColumnHeader
        lstResources.Tag = theme.ListViewColumnColors
        WalkmanLib.ApplyTheme(theme, Me, True)
        If components IsNot Nothing Then WalkmanLib.ApplyTheme(theme, components.Components, True)

        lstResources.SmallImageList = New ImageList() With {
            .ColorDepth = ColorDepth.Depth32Bit,
            .ImageSize = New Drawing.Size(16, 16)
        }
    End Sub
    Private Sub lstResources_ItemActivate() Handles lstResources.ItemActivate
        btnSelect.PerformClick()
    End Sub

    Private Sub Selector_Load() Handles Me.Shown
        lstResources.Items.Clear()
        lstResources.SmallImageList.Images.Clear()

        Dim resset = My.Resources.Resources.ResourceManager.GetResourceSet(Globalization.CultureInfo.InvariantCulture, True, False)
        Dim bitmapProperties = resset.Cast(Of Collections.DictionaryEntry)().Where(Function(kv) TypeOf kv.Value Is Drawing.Bitmap).OrderBy(Function(kv) kv.Key)

        For Each item As Collections.DictionaryEntry In bitmapProperties
            Dim name As String = DirectCast(item.Key, String)
            Dim image As Drawing.Bitmap = DirectCast(item.Value, Drawing.Bitmap)

            lstResources.SmallImageList.Images.Add(name, image)
            lstResources.Items.Add(name, name).Tag = name
        Next
    End Sub

    Public ReadOnly Property SelectedResource As String
        Get
            If lstResources.SelectedItems.Count <> 1 Then
                Return Nothing
            Else
                Return lstResources.SelectedItems(0).Text
            End If
        End Get
    End Property
End Class
