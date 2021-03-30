Imports System
Imports System.IO
Imports Trinet.Core.IO.Ntfs

Namespace Operations
    Public Class FileToADS
        Public Shared Sub Move(sourcePath As String, targetPath As String)
            Copy(sourcePath, targetPath, Sub(e)
                                             If e.Error Is Nothing Then Delete({sourcePath}, skipDialog:=True)
                                         End Sub)
        End Sub

        Public Shared Sub Copy(sourcePath As String, targetPath As String, Optional onComplete As Action(Of ComponentModel.RunWorkerCompletedEventArgs) = Nothing)
            Dim targetFile As String = Helpers.GetADSPathFile(targetPath)
            Dim targetStreamName As String = Helpers.GetADSPathStream(targetPath)

            Try
                If Not WalkmanLib.IsFileOrDirectory(targetFile).HasFlag(PathEnum.Exists) Then
                    File.Create(targetFile).Close()
                End If
                Dim adsTarget As AlternateDataStreamInfo = GetAlternateDataStream(targetFile, targetStreamName, FileMode.CreateNew)

                Dim sourceStream As FileStream = Nothing
                Dim targetStream As FileStream = Nothing

                Try
                    sourceStream = File.OpenRead(sourcePath)
                    targetStream = adsTarget.OpenWrite()
                    WalkmanLib.StreamCopy(sourceStream, targetStream, $"Copying {sourcePath} to {targetPath}...",
                                          "File to Stream copy", Sub(s, e)
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
