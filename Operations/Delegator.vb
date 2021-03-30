Imports System.IO

Namespace Operations
    Module Delegator
        Private Enum ADSFiles
            BothAreADS
            SourceIsADS
            TargetIsADS
            BothAreFiles
        End Enum

        Private Function GetIsADS(sourcePath As String, targetPath As String) As ADSFiles
            Dim sourcePathContainsADS As Boolean = Helpers.PathContainsADS(sourcePath)
            Dim targetPathContainsADS As Boolean = Helpers.PathContainsADS(targetPath)

            If sourcePathContainsADS AndAlso targetPathContainsADS Then
                Return ADSFiles.BothAreADS
            ElseIf sourcePathContainsADS Then
                Return ADSFiles.SourceIsADS
            ElseIf targetPathContainsADS Then
                Return ADSFiles.TargetIsADS
            Else
                Return ADSFiles.BothAreFiles
            End If
        End Function

        Private Function CleanDataADS(path As String) As String
            If Helpers.PathContainsADS(path) AndAlso Helpers.GetADSPathStream(path) = ":$DATA" Then
                path = Helpers.GetADSPathFile(path)
            End If

            Return path
        End Function

        Public Sub Rename(sourcePath As String, targetName As String)
            sourcePath = CleanDataADS(sourcePath)
            targetName = CleanDataADS(targetName)
            Dim fullTargetName = Path.GetDirectoryName(sourcePath) & Path.DirectorySeparatorChar & targetName

            Select Case GetIsADS(sourcePath, fullTargetName)
                Case ADSFiles.BothAreADS
                    ADSToADS.Move(sourcePath, fullTargetName)
                Case ADSFiles.SourceIsADS
                    ADSToFile.Move(sourcePath, fullTargetName)
                Case ADSFiles.TargetIsADS
                    FileToADS.Move(sourcePath, fullTargetName)
                Case ADSFiles.BothAreFiles
                    FileToFile.Rename(sourcePath, targetName)
            End Select
        End Sub

        Public Sub Move(sourcePath As String, targetPath As String, useShell As Boolean)
            sourcePath = CleanDataADS(sourcePath)
            targetPath = CleanDataADS(targetPath)

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
            sourcePath = CleanDataADS(sourcePath)
            targetPath = CleanDataADS(targetPath)

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
