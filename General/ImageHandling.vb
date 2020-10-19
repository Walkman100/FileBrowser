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

        Public Function ResizeImage(img As Image, newSize As Integer) As Image
            Dim rtnImg As New Bitmap(newSize, newSize)
            Using gr As Graphics = Graphics.FromImage(rtnImg)
                gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                gr.DrawImage(img, New Rectangle(0, 0, newSize, newSize), New Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel)
            End Using
            Return rtnImg
        End Function
    End Module

    Module ListViewImageLists
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
    End Module

    Module TreeViewImageLists
        Public Function CreateImageList(size As Integer) As ImageList
            Dim il As New ImageList() With {
                .ColorDepth = ColorDepth.Depth32Bit,
                .ImageSize = New Size(size, size)
            }

            If Helpers.GetOS() = OS.Windows Then
                ' Index 0: default folder icon
                il.Images.Add(GetIcon("%SystemRoot%\System32\imageres.dll,3", size).ToBitmap())
                ' Index 1: default folder icon - compressed
                il.Images.Add(AddOverlay(il.Images(0).Clone2(), My.Resources.Resources.Compress, True))
                ' Index 2: default folder icon - encrypted
                il.Images.Add(AddOverlay(il.Images(0).Clone2(), My.Resources.Resources.Encrypt, True))
                ' Index 3: default folder icon - symlink
                il.Images.Add(AddOverlay(il.Images(0).Clone2(), My.Resources.Resources.OverlaySymlink))
                ' Index 4: default folder icon - compressed & symlink
                il.Images.Add(AddOverlay(il.Images(1).Clone2(), My.Resources.Resources.OverlaySymlink))
                ' Index 5: default folder icon - encrypted & symlink
                il.Images.Add(AddOverlay(il.Images(2).Clone2(), My.Resources.Resources.OverlaySymlink))
            End If

            Return il
        End Function

        Private Function GetDefaultIndex(path As String) As Integer
            Dim index As Integer = 0
            Try ' in case File.GetAttributes fails
                Dim attrs As FileAttributes = File.GetAttributes(path)

                If attrs.HasFlag(FileAttributes.Compressed) Then
                    index = 1
                End If
                If attrs.HasFlag(FileAttributes.Encrypted) Then
                    index = 2
                End If
                If attrs.HasFlag(FileAttributes.ReparsePoint) Then
                    index += 3
                End If
            Catch : End Try
            Return index
        End Function

        Public Sub SetImage(node As TreeNode, imageList As ImageList, size As Integer)
            If Not Settings.SpecificItemIcons Then
                node.ImageIndex = GetDefaultIndex(node.FixedFullPath())
                Return
            End If

            Dim path As String = node.FixedFullPath()
            Dim folderIconPath As String = WalkmanLib.GetFolderIconPath(path)
            If folderIconPath = "no icon found" Then
                node.ImageIndex = GetDefaultIndex(path)
                Return
            End If

            Dim img As Image
            Try
                img = ResizeImage(Image.FromFile(folderIconPath), size)
            Catch
                Try : img = GetIcon(folderIconPath, size).ToBitmap()
                Catch : img = ResizeImage(New PictureBox().ErrorImage, size)
                End Try
            End Try
            img = AddOverlays(Filesystem.GetItemEntryInfo(New FileInfo(path)), img)

            imageList.Images.Add(path, img)
            node.ImageKey = path
        End Sub

        Public Sub ReleaseImage(node As TreeNode, imageList As ImageList)
            If node.ImageIndex = -1 Then ' -1 means the image does not use Index
                If imageList.Images.ContainsKey(node.FixedFullPath()) Then
                    imageList.Images.RemoveByKey(node.FixedFullPath())
                End If
            End If
        End Sub
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

        Public Function AddOverlays(itemInfo As Filesystem.EntryInfo, image As Image) As Image
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

            If Settings.OverlayHardlink AndAlso itemInfo.Type.HasFlag(Filesystem.EntryType.Hardlink) Then
                Return AddOverlay(image, My.Resources.Resources.OverlayHardlink)
            End If

            If Settings.OverlayOffline AndAlso itemInfo.Attributes.HasFlag(FileAttributes.Offline) Then
                Return AddOverlay(image, My.Resources.Resources.OverlayOffline)
            End If

            Return image
        End Function
    End Module
End Namespace
