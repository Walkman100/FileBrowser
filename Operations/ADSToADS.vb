Imports System
Imports System.IO
Imports Trinet.Core.IO.Ntfs

Namespace Operations
    Public Class ADSToADS
        Public Shared Sub Move(sourcePath As String, targetPath As String)
            If Copy(sourcePath, targetPath) Then
                Delete({sourcePath}, False, True)
            End If
        End Sub

        Public Shared Function Copy(sourcePath As String, targetPath As String) As Boolean
            Dim targetFile As String = Helpers.GetADSPathFile(targetPath)
            Dim targetStreamName As String = Helpers.GetADSPathStream(targetPath)

            Try
                Dim adsSource As New AlternateDataStreamInfo(Helpers.GetADSPathFile(sourcePath), Helpers.GetADSPathStream(sourcePath), Nothing, True)

                If Not WalkmanLib.IsFileOrDirectory(targetFile).HasFlag(PathEnum.Exists) Then
                    File.Create(targetFile).Close()
                End If
                Dim adsTarget As AlternateDataStreamInfo = GetAlternateDataStream(targetFile, targetStreamName, FileMode.CreateNew)

                Using sourceStream As FileStream = adsSource.OpenRead()
                    Using targetStream As FileStream = adsTarget.OpenWrite()
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
