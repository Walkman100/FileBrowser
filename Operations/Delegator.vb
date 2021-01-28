Namespace Operations
    Module Delegator
        Private Enum ADSFiles
            BothAreADS
            SourceIsADS
            TargetIsADS
            BothAreFiles
        End Enum

        Private Function GetIsADS(sourcePath As String, targetPath As String) As ADSFiles
            If Helpers.PathContainsADS(sourcePath) AndAlso Helpers.GetADSPathStream(sourcePath) <> ":$DATA" AndAlso
                    Helpers.PathContainsADS(targetPath) AndAlso Helpers.GetADSPathStream(sourcePath) <> ":$DATA" Then
                Return ADSFiles.BothAreADS
            ElseIf Helpers.PathContainsADS(sourcePath) AndAlso Helpers.GetADSPathStream(sourcePath) <> ":$DATA" Then
                Return ADSFiles.SourceIsADS
            ElseIf Helpers.PathContainsADS(targetPath) AndAlso Helpers.GetADSPathStream(targetPath) <> ":$DATA" Then
                Return ADSFiles.TargetIsADS
            Else
                Return ADSFiles.BothAreFiles
            End If
        End Function

        Public Sub Rename(sourcePath As String, targetName As String)
            Select Case GetIsADS(sourcePath, targetName)
                Case ADSFiles.BothAreADS
                    ADSToADS.Rename(sourcePath, targetName)
                Case ADSFiles.SourceIsADS
                    ADSToFile.Rename(sourcePath, targetName)
                Case ADSFiles.TargetIsADS
                    FileToADS.Rename(sourcePath, targetName)
                Case ADSFiles.BothAreFiles
                    FileToFile.Rename(sourcePath, targetName)
            End Select
        End Sub

        Public Sub Move(sourcePath As String, targetPath As String, useShell As Boolean)
            Select Case GetIsADS(sourcePath, targetPath)
                Case ADSFiles.BothAreADS
                    ADSToADS.Move(sourcePath, targetPath)
                Case ADSFiles.SourceIsADS
                    ADSToFile.Move(sourcePath, targetPath)
                Case ADSFiles.TargetIsADS
                    FileToADS.Move(sourcePath, targetPath)
                Case ADSFiles.BothAreFiles
                    FileToFile.Move(sourcePath, targetPath, useShell)
            End Select
        End Sub

        Public Sub Copy(sourcePath As String, targetPath As String, useShell As Boolean)
            Select Case GetIsADS(sourcePath, targetPath)
                Case ADSFiles.BothAreADS
                    ADSToADS.Copy(sourcePath, targetPath)
                Case ADSFiles.SourceIsADS
                    ADSToFile.Copy(sourcePath, targetPath)
                Case ADSFiles.TargetIsADS
                    FileToADS.Copy(sourcePath, targetPath)
                Case ADSFiles.BothAreFiles
                    FileToFile.Copy(sourcePath, targetPath, useShell)
            End Select
        End Sub
    End Module
End Namespace
