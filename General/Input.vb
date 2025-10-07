Imports System.Windows.Forms

Class Input
    Public Shared Function GetInput(ByRef input As String, Optional windowTitle As String = Nothing, Optional header As String = Nothing, Optional content As String = Nothing) As DialogResult
        Return WalkmanLib.InputDialog(input, Settings.Theme, header, windowTitle, content, ownerForm:=FileBrowser)
    End Function
End Class
