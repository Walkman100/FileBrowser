Imports System
Imports System.Drawing
Imports System.IO

Public Class ImageHandling
    Public Shared Function TransformResourcePath(iconResource As String, Optional ByRef iconIndex As Integer = 0) As String
        If iconResource.Contains(",") Then
            If Microsoft.VisualBasic.IsNumeric(iconResource.Substring(iconResource.LastIndexOf(",") + 1)) Then
                iconIndex = Integer.Parse(iconResource.Substring(iconResource.LastIndexOf(",") + 1))

                iconResource = iconResource.Remove(iconResource.LastIndexOf(","))
            End If
        End If

        Return iconResource
    End Function

    Public Shared Function GetIcon(resourcePath As String) As Icon
        If resourcePath Is Nothing Then Return Nothing

        Dim iconIndex As Integer = 0
        resourcePath = TransformResourcePath(resourcePath, iconIndex)
        resourcePath = Environment.ExpandEnvironmentVariables(resourcePath)

        If File.Exists(resourcePath) Then
            Return WalkmanLib.ExtractIconByIndex(resourcePath, iconIndex, 16)
        Else
            Return Nothing
        End If
    End Function
End Class
