Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Namespace ImageHandling
    Module ImageHandling
        Public Function TransformResourcePath(iconResource As String, Optional ByRef iconIndex As Integer = 0) As String
            If iconResource.Contains(",") Then
                If Microsoft.VisualBasic.IsNumeric(iconResource.Substring(iconResource.LastIndexOf(",") + 1)) Then
                    iconIndex = Integer.Parse(iconResource.Substring(iconResource.LastIndexOf(",") + 1))

                    iconResource = iconResource.Remove(iconResource.LastIndexOf(","))
                End If
            End If

            Return iconResource
        End Function

        Public Function GetIcon(resourcePath As String, Optional size As Integer = 16) As Icon
            If String.IsNullOrEmpty(resourcePath) Then Return Nothing

            Dim iconIndex As Integer = 0
            resourcePath = TransformResourcePath(resourcePath, iconIndex)
            resourcePath = Environment.ExpandEnvironmentVariables(resourcePath)

            Return WalkmanLib.ExtractIconByIndex(resourcePath, iconIndex, CType(size, UInteger))
        End Function

        Public Function GetImageList(items As ListView.ListViewItemCollection, size As Integer, Optional setItemIndexes As Boolean = False) As ImageList
            Dim il As New ImageList With {
                .ImageSize = New Size(size, size),
                .ColorDepth = ColorDepth.Depth32Bit
            }
            Dim folderIcon As Image = Nothing
            If Helpers.GetOS() = OS.Windows Then
                folderIcon = GetIcon("%SystemRoot%\System32\imageres.dll,3", size).ToBitmap()
            End If

            For i = 0 To items.Count - 1
                If setItemIndexes Then items(i).ImageIndex = i
                Dim itemInfo As Filesystem.EntryInfo = FileBrowser.GetItemInfo(items(i))

                If itemInfo.Attributes.HasFlag(FileAttributes.Directory) Then
                    il.Images.Add(GetFolderImage(itemInfo, size, folderIcon))
                Else
                    il.Images.Add(GetFileImage(itemInfo, size))
                End If
            Next

            Return il
        End Function

        Private Function GetFolderImage(item As Filesystem.EntryInfo, size As Integer, defaultIcon As Image) As Image
            If Not Settings.SpecificItemIcons Then
                Return defaultIcon
            Else
                Dim folderIconPath As String = WalkmanLib.GetFolderIconPath(item.FullName)
                If folderIconPath = "no icon found" Then
                    Return defaultIcon
                Else
                    Try
                        Return Image.FromFile(folderIconPath)
                    Catch : End Try

                    Try
                        Return GetIcon(folderIconPath, size).ToBitmap()
                    Catch
                        Return New PictureBox().ErrorImage
                    End Try
                End If
            End If
        End Function

        Private Function GetFileImage(item As Filesystem.EntryInfo, size As Integer) As Image
            If Not Settings.SpecificItemIcons Then
                Dim tempFile As String = Path.GetTempFileName()
                File.Move(tempFile, Path.ChangeExtension(tempFile, item.Extension))
                tempFile = Path.ChangeExtension(tempFile, item.Extension)

                Dim img As Image = Icon.ExtractAssociatedIcon(tempFile).ToBitmap()
                File.Delete(tempFile)
                Return img
            Else
                If Settings.ImageThumbs Then
                    If item.Size < 200000000 Then ' don't try load image if filesize is above 200MB
                        Try
                            Return Image.FromFile(item.FullName)
                        Catch : End Try
                    End If

                    Try
                        Return GetIcon(item.FullName, size).ToBitmap()
                    Catch : End Try
                End If

                Return Icon.ExtractAssociatedIcon(item.FullName).ToBitmap()
            End If
        End Function
    End Module

    Module Overlays
        Public Function AddOverlay(image As Image, overlay As Bitmap, Optional resize As Boolean = False) As Image
            Dim gr As Graphics = Graphics.FromImage(image)

            If resize Then
                overlay = New Bitmap(overlay, 8, 8)
                gr.DrawImage(overlay, 8, 8, overlay.Width, overlay.Height)
            Else
                gr.DrawImage(overlay, 0, 0, overlay.Width, overlay.Height)
            End If

            Return image
        End Function

        Public Function AddAdminOverlay(image As Image) As Image
            Return AddOverlay(image, My.Resources.Resources.Admin, True)
        End Function
    End Module
End Namespace
