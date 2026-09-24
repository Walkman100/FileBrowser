Imports System
Imports System.Collections.Generic
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
        Return String.Join(" ", arr.Select(Function(p) $"""{p}"""))
    End Function

    <Extension()>
    Public Function DisplayString(str As String) As String
        If str Is Nothing Then Return "NULL"
        If str = String.Empty Then Return "EMPTY"
        If str.Trim() = String.Empty Then Return "WHITESPACE"
        Return str
    End Function

    ''' <summary>Returns <see cref="TreeNode.FullPath"/> using node Name, with duplicate DirectorySeparatorChars removed</summary>
    <Extension()>
    Public Function FixedFullPath(node As TreeNode) As String
        Dim stringBuilder As New Text.StringBuilder()
        getNodeFullPathFromKey(node, stringBuilder, node.TreeView.PathSeparator)
        stringBuilder.Replace(Path.DirectorySeparatorChar & Path.DirectorySeparatorChar, Path.DirectorySeparatorChar)

        Return stringBuilder.ToString()
    End Function

    ''' <summary>Implementation of <see cref="TreeNode.GetFullPath"/> but using node Name instead of Text</summary>
    Private Sub getNodeFullPathFromKey(node As TreeNode, path As Text.StringBuilder, pathSeparator As String)
        If node.Parent IsNot Nothing Then
            getNodeFullPathFromKey(node.Parent, path, pathSeparator)
            path.Append(pathSeparator)
        End If
        path.Append(node.Name)
    End Sub

    <Extension()>
    Public Function OrderByAorD(Of TSource, TKey)(source As IEnumerable(Of TSource), sortOrder As SortOrder,
                                                  keySelector As Func(Of TSource, TKey)) As IOrderedEnumerable(Of TSource)
        Select Case sortOrder
            Case SortOrder.Ascending
                Return source.OrderBy(keySelector)
            Case SortOrder.Descending
                Return source.OrderByDescending(keySelector)
        End Select

        Return DirectCast(source, IOrderedEnumerable(Of TSource))
    End Function

    <Extension()>
    Public Function ThenByAorD(Of TSource, TKey)(source As IOrderedEnumerable(Of TSource), sortOrder As SortOrder,
                                                 keySelector As Func(Of TSource, TKey)) As IOrderedEnumerable(Of TSource)
        Select Case sortOrder
            Case SortOrder.Ascending
                Return source.ThenBy(keySelector)
            Case SortOrder.Descending
                Return source.ThenByDescending(keySelector)
        End Select

        Return source
    End Function
End Module
