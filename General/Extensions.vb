Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Module Extensions
    <Extension()>
    Public Sub DoubleBuffered(control As Control, enable As Boolean) ' thanks to https://stackoverflow.com/a/15268338/2999220
        Dim doubleBufferPropertyInfo = control.[GetType]().GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        doubleBufferPropertyInfo.SetValue(control, enable, Nothing)
    End Sub

    <Extension()>
    Public Function NameNoExt(fsi As FileSystemInfo) As String
        Return Path.GetFileNameWithoutExtension(fsi.FullName)
    End Function

    ''' <summary>Concatenates all elements of the array, with double-quotation marks (") around each path.</summary>
    <Extension()>
    Public Function PathsConcat(arr As String()) As String
        Return String.Join(" ", arr.Select(Function(p) """" & p & """"))
    End Function

    ''' <summary>Returns node.FullPath, with duplicate DirectorySeparatorChars removed</summary>
    <Extension()>
    Public Function FixedFullPath(node As TreeNode) As String
        Return node.FullPath.Replace(Path.DirectorySeparatorChar & Path.DirectorySeparatorChar, Path.DirectorySeparatorChar)
    End Function
End Module
