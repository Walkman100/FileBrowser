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
    End Module

    Module ImageLists
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
                    il.Images.Add(AddOverlays(itemInfo, GetFolderImage(itemInfo, size, folderIcon.Clone2())))
                Else
                    il.Images.Add(AddOverlays(itemInfo, GetFileImage(itemInfo, size)))
                End If
            Next

            Return il
        End Function

        Private Function AddOverlays(itemInfo As Filesystem.EntryInfo, image As Image) As Image
            ' shortcut overlay is automatically applied by WalkmanLib.GetFileIcon / Icon.ExtractAssociatedIcon

            If Settings.OverlayCompressed AndAlso itemInfo.Attributes.HasFlag(FileAttributes.Compressed) Then
                image = AddOverlay(image, My.Resources.Resources.Compress, True)
            End If

            If Settings.OverlayEncrypted AndAlso itemInfo.Attributes.HasFlag(FileAttributes.Encrypted) Then
                image = AddOverlay(image, My.Resources.Resources.Encrypt, True)
            End If

            If Settings.OverlayReparse AndAlso itemInfo.Attributes.HasFlag(FileAttributes.ReparsePoint) Then
                Return AddOverlay(image, My.Resources.Resources.OverlaySymlink)
            End If

            If Settings.OverlayHardlink AndAlso itemInfo.HardlinkCount > 1 Then
                Return AddOverlay(image, My.Resources.Resources.OverlayHardlink)
            End If

            If Settings.OverlayOffline AndAlso itemInfo.Attributes.HasFlag(FileAttributes.Offline) Then
                Return AddOverlay(image, My.Resources.Resources.OverlayOffline)
            End If

            Return image
        End Function

        Private Function GetFolderImage(item As Filesystem.EntryInfo, size As Integer, defaultIcon As Image) As Image
            If Not Settings.SpecificItemIcons Then Return defaultIcon

            Dim folderIconPath As String = WalkmanLib.GetFolderIconPath(item.FullName)
            If folderIconPath = "no icon found" Then Return defaultIcon

            Try
                Return ResizeImage(Image.FromFile(folderIconPath), size)
            Catch : End Try

            Try
                Return GetIcon(folderIconPath, size).ToBitmap()
            Catch
                Return ResizeImage(New PictureBox().ErrorImage, size)
            End Try
        End Function
        Private Function GetFileImage(item As Filesystem.EntryInfo, size As Integer) As Image
            If Not Settings.SpecificItemIcons Then
                If size = 16 OrElse size = 32 Then
                    Return WalkmanLib.GetFileIcon(item.Extension, False, size = 16).ToBitmap()
                Else
                    Dim img As Image = WalkmanLib.GetFileIcon(item.Extension, False, size < 16).ToBitmap()
                    Return ResizeImage(img, size)
                End If
            End If

            If Settings.ImageThumbs Then
                If item.Size < 200000000 Then ' don't try load image if filesize is above 200MB
                    Try
                        Return ResizeImage(Image.FromFile(item.FullName), size)
                    Catch : End Try
                End If

                Try
                    Return GetIcon(item.FullName, size).ToBitmap()
                Catch : End Try
            End If

            If size = 16 Then
                Return WalkmanLib.GetFileIcon(item.FullName).ToBitmap()
            ElseIf size = 32 Then
                Return Icon.ExtractAssociatedIcon(item.FullName).ToBitmap()
            Else
                Dim img As Image = Icon.ExtractAssociatedIcon(item.FullName).ToBitmap()
                Return ResizeImage(img, size)
            End If
        End Function

        Public Sub SetImage(node As TreeNode, imageList As ImageList)
            If Not Settings.SpecificItemIcons Then
                node.ImageIndex = 0
                Return
            End If

            Dim path As String = node.FixedFullPath()
            Dim folderIconPath As String = WalkmanLib.GetFolderIconPath(path)
            If folderIconPath = "no icon found" Then
                node.ImageIndex = 0
                Return
            End If

            Dim img As Image

            Try
                img = ResizeImage(Image.FromFile(folderIconPath), 16)
            Catch
                Try : img = GetIcon(folderIconPath, 16).ToBitmap()
                Catch : img = ResizeImage(New PictureBox().ErrorImage, 16)
                End Try
            End Try

            node.ImageIndex = imageList.Images.AddGetKey(img)
        End Sub

        Private Function ResizeImage(img As Image, newSize As Integer) As Image
            Dim rtnImg As New Bitmap(newSize, newSize)
            Using gr As Graphics = Graphics.FromImage(rtnImg)
                gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                gr.DrawImage(img, New Rectangle(0, 0, newSize, newSize), New Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel)
            End Using
            Return rtnImg
        End Function
    End Module

    Module Overlays
        Public Function AddOverlay(image As Image, overlay As Bitmap, Optional resize As Boolean = False) As Image
            Using gr As Graphics = Graphics.FromImage(image)
                gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                If resize Then
                    overlay = New Bitmap(overlay, 8, 8)
                    gr.DrawImage(overlay, 8, 8, overlay.Width, overlay.Height)
                Else
                    gr.DrawImage(overlay, 0, 0, overlay.Width, overlay.Height)
                End If
            End Using

            Return image
        End Function

        Public Function AddAdminOverlay(image As Image) As Image
            Return AddOverlay(image, My.Resources.Resources.Admin, True)
        End Function
    End Module
End Namespace
