Namespace Operations
    Module Delegator
        Public Sub Rename(sourcePath As String, targetName As String)
            FileToFile.Rename(sourcePath, targetName)
        End Sub

        Public Sub Move(sourcePath As String, targetPath As String, useShell As Boolean)
            FileToFile.Move(sourcePath, targetPath, useShell)
        End Sub

        Public Sub Copy(sourcePath As String, targetPath As String, useShell As Boolean)
            FileToFile.Copy(sourcePath, targetPath, useShell)
        End Sub
    End Module
End Namespace
