Imports System
Imports System.Windows.Forms

Class Input
    Public Shared Function GetInput(ByRef input As String, Optional windowTitle As String = Nothing, Optional header As String = Nothing, Optional content As String = Nothing) As DialogResult
        If OokiiDialogsLoaded() Then
            Return OokiiInputBox(input, windowTitle, header, content)
        Else
            Dim inputBoxPrompt As String = header
            If content IsNot Nothing Then
                inputBoxPrompt &= Environment.NewLine & content
            End If

            input = Microsoft.VisualBasic.InputBox(inputBoxPrompt, windowTitle, input)
            If String.IsNullOrEmpty(input) Then
                Return DialogResult.Cancel
            Else
                Return DialogResult.OK
            End If
        End If
    End Function

    Private Shared Function OokiiInputBox(ByRef input As String, Optional windowTitle As String = Nothing, Optional header As String = Nothing, Optional content As String = Nothing) As DialogResult
        Dim ooInput As New Ookii.Dialogs.InputDialog With {
            .Input = input,
            .WindowTitle = windowTitle,
            .MainInstruction = header,
            .Content = content
        }

        Dim returnResult = ooInput.ShowDialog(FileBrowser)
        input = ooInput.Input
        Return returnResult
    End Function

    Private Shared Function OokiiDialogsLoaded() As Boolean
        Try
            OokiiDialogsLoadedDelegate()
            Return True
        Catch ex As IO.FileNotFoundException When ex.FileName.StartsWith("Ookii.Dialogs")
            Return False
        Catch ex As Exception
            MessageBox.Show("Unexpected error loading Ookii.Dialogs.dll!" & Environment.NewLine & Environment.NewLine & ex.Message,
                            "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try
    End Function
    Private Shared Sub OokiiDialogsLoadedDelegate() ' because calling a not found class will fail the caller of the method not directly in the method
        Dim test = Ookii.Dialogs.TaskDialogIcon.Information
    End Sub
End Class
