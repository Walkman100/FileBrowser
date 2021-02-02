Imports System
Imports System.IO
Imports Trinet.Core.IO.Ntfs

Namespace Operations
    Public Class ADSToFile
        Public Shared Sub Move(sourcePath As String, targetPath As String)
            Copy(sourcePath, targetPath, Sub(e)
                                             If e.Error Is Nothing Then Delete({sourcePath}, False, True)
                                         End Sub)
        End Sub

        Public Shared Sub Copy(sourcePath As String, targetPath As String, Optional onComplete As Action(Of ComponentModel.RunWorkerCompletedEventArgs) = Nothing)
            Try
                Dim adsSource As New AlternateDataStreamInfo(Helpers.GetADSPathFile(sourcePath), Helpers.GetADSPathStream(sourcePath), Nothing, True)

                If Not WalkmanLib.IsFileOrDirectory(targetPath).HasFlag(PathEnum.Exists) Then
                    File.Create(targetPath).Close()
                End If

                Dim sourceStream As FileStream = Nothing
                Dim targetStream As FileStream = Nothing

                Try
                    sourceStream = adsSource.OpenRead()
                    targetStream = File.OpenWrite(targetPath)
                    WalkmanLib.StreamCopy(sourceStream, targetStream, "Copying " & sourcePath & " to " & targetPath & "...",
                                          "Stream to File copy", Sub(s, e)
                                                                     If e.Error IsNot Nothing Then
                                                                         FileBrowser.ErrorParser(e.Error)
                                                                     End If
                                                                     onComplete?(e)
                                                                 End Sub)
                Catch
                    sourceStream?.Dispose()
                    targetStream?.Dispose()
                    Throw
                End Try
            Catch ex As Exception
                FileBrowser.ErrorParser(ex)
            End Try
        End Sub
    End Class
End Namespace
