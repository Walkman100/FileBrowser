Imports System
Imports System.IO
Imports Trinet.Core.IO.Ntfs

Namespace Operations
    Public Class ADSToFile
        Public Shared Sub Rename(sourcePath As String, targetName As String)
            Dim fullTargetName = Path.GetDirectoryName(sourcePath) & Path.DirectorySeparatorChar & targetName

            If Copy(sourcePath, fullTargetName) Then
                Delete({sourcePath}, False, True)
            End If
        End Sub

        Public Shared Sub Move(sourcePath As String, targetPath As String)
            If Copy(sourcePath, targetPath) Then
                Delete({sourcePath}, False, True)
            End If
        End Sub

        Public Shared Function Copy(sourcePath As String, targetPath As String) As Boolean
            Try
                Dim adsSource As New AlternateDataStreamInfo(Helpers.GetADSPathFile(sourcePath), Helpers.GetADSPathStream(sourcePath), Nothing, True)

                If Not WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) Then
                    File.Create(targetPath).Close()
                End If

                Using sourceStream As FileStream = adsSource.OpenRead()
                    Using targetStream As FileStream = File.Open(targetPath, FileMode.Truncate)
                        sourceStream.CopyTo(targetStream)
                    End Using
                End Using

                Return True
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
                Return False
            End Try
        End Function
    End Class
End Namespace
