Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Trinet.Core.IO.Ntfs

Namespace Helpers
    Module Helpers
        Public Sub ShowFileExternal(filePath As String)
            Select Case GetOS()
                Case WalkmanLib.OS.Windows
                    Launch.LaunchItem(filePath, "explorer.exe", "/select, ""{path}""")
                Case WalkmanLib.OS.Linux
                    Launch.LaunchItem(filePath, "xdg-open", "{directory}")
                Case WalkmanLib.OS.MacOS 'https://stackoverflow.com/q/39214539/2999220
                    Launch.LaunchItem(filePath, "open", "-R {path}")
            End Select
        End Sub

        ''' <summary>Creates an exact copy of a <see cref="Image"/>.</summary>
        ''' <returns>The <see cref="Image"/> <see cref="Image.Clone"/> creates, cast as <see cref="Image"/> instead of <see cref="Object"/>.</returns>
        Public Function Clone(img As Image) As Image
            Return DirectCast(img.Clone(), Image)
        End Function

        Public Sub ApplyColumns(fb As FileBrowser, colLst As List(Of Settings.Column))
            Threading.Tasks.Parallel.ForEach(colLst,
                                             New Threading.Tasks.ParallelOptions With {.MaxDegreeOfParallelism = Environment.ProcessorCount},
                                             Sub(col)
                                                 With fb.lstCurrent.Columns.Cast(Of ColumnHeader).
                                                         First(Function(c) DirectCast(c.Tag, String).ToLowerInvariant() = col.SaveName.ToLowerInvariant())
                                                     .DisplayIndex = col.DisplayIndex
                                                     .Width = col.Width
                                                 End With
                                             End Sub)
        End Sub

        Public Sub BeginUpdate(control As System.ComponentModel.Component)
            If control Is Nothing Then Return

            Select Case control.GetType()
                Case GetType(TreeView)
                    DirectCast(control, TreeView).BeginUpdate()
                Case GetType(ListView)
                    DirectCast(control, ListView).BeginUpdate()
                Case GetType(ListBox)
                    DirectCast(control, ListBox).BeginUpdate()
                Case GetType(ComboBox)
                    DirectCast(control, ComboBox).BeginUpdate()
                Case GetType(ToolStripComboBox)
                    DirectCast(control, ToolStripComboBox).BeginUpdate()
            End Select
        End Sub
        Public Sub EndUpdate(control As System.ComponentModel.Component)
            If control Is Nothing Then Return

            Select Case control.GetType()
                Case GetType(TreeView)
                    DirectCast(control, TreeView).EndUpdate()
                Case GetType(ListView)
                    DirectCast(control, ListView).EndUpdate()
                Case GetType(ListBox)
                    DirectCast(control, ListBox).EndUpdate()
                Case GetType(ComboBox)
                    DirectCast(control, ComboBox).EndUpdate()
                Case GetType(ToolStripComboBox)
                    DirectCast(control, ToolStripComboBox).EndUpdate()
            End Select
        End Sub

        Public Class FreezeUpdate
            Implements IDisposable
            Private ReadOnly frozenControl As System.ComponentModel.Component
            Private disposed As Boolean = False

            Public Sub New(control As System.ComponentModel.Component)
                frozenControl = control
                BeginUpdate(control)
            End Sub

            Protected Overridable Overloads Sub Dispose(disposing As Boolean)
                If Not disposed Then
                    If disposing Then EndUpdate(frozenControl)
                    disposed = True
                End If
            End Sub

            Public Overloads Sub Dispose() Implements IDisposable.Dispose
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
            Protected Overrides Sub Finalize()
                Dispose(False)
                MyBase.Finalize()
            End Sub
        End Class

        Public Function Invoke(Of T)(control As Control, method As Func(Of T)) As T
            Return DirectCast(control.Invoke(method), T)
        End Function

        Private osCache As WalkmanLib.OS?
        Public Function GetOS() As WalkmanLib.OS
            If osCache Is Nothing Then
                osCache = WalkmanLib.GetOS()
            End If
            Return osCache.Value
        End Function
    End Module

    Module GetFileInfo
        Public Function GetUrlTarget(path As String) As String
            Using fs As StreamReader = File.OpenText(path)
                While Not fs.EndOfStream
                    Dim line As String = fs.ReadLine()
                    If line?.StartsWith("URL=", True, Globalization.CultureInfo.InvariantCulture) Then
                        Return line.Substring(4)
                    End If
                End While

                Return Nothing
            End Using
        End Function

        Private Const WindowsDownloadADS As String = "Zone.Identifier"
        Private Const XDGOriginADS As String = "xdg.origin.url"
        Private Const XDGReferrerADS As String = "xdg.referrer.url"

        Public Function GetDownloadURL(path As String) As String
            Try
                If AlternateDataStreamExists(path, WindowsDownloadADS) Then
                    Using fs As StreamReader = GetAlternateDataStream(path, WindowsDownloadADS).OpenText()
                        While Not fs.EndOfStream
                            Dim line As String = fs.ReadLine()
                            If line?.StartsWith("HostUrl=", True, Globalization.CultureInfo.InvariantCulture) Then
                                Return line.Substring(8)
                            End If
                        End While
                    End Using
                ElseIf AlternateDataStreamExists(path, XDGOriginADS) Then
                    Using fs As StreamReader = GetAlternateDataStream(path, XDGOriginADS).OpenText()
                        While Not fs.EndOfStream
                            Dim line As String = fs.ReadLine()
                            If Not String.IsNullOrWhiteSpace(line) Then
                                Return line.Trim()
                            End If
                        End While
                    End Using
                End If
            Catch : End Try
            Return Nothing
        End Function

        Public Function GetDownloadReferrer(path As String) As String
            Try
                If AlternateDataStreamExists(path, WindowsDownloadADS) Then
                    Using fs As StreamReader = GetAlternateDataStream(path, WindowsDownloadADS).OpenText()
                        While Not fs.EndOfStream
                            Dim line As String = fs.ReadLine()
                            If line?.StartsWith("ReferrerUrl=", True, Globalization.CultureInfo.InvariantCulture) Then
                                Return line.Substring(12)
                            End If
                        End While
                    End Using
                ElseIf AlternateDataStreamExists(path, XDGReferrerADS) Then
                    Using fs As StreamReader = GetAlternateDataStream(path, XDGReferrerADS).OpenText()
                        While Not fs.EndOfStream
                            Dim line As String = fs.ReadLine()
                            If Not String.IsNullOrWhiteSpace(line) Then
                                Return line.Trim()
                            End If
                        End While
                    End Using
                End If
            Catch : End Try
            Return Nothing
        End Function

        Public Function ConvSize(size As Double) As String
            Select Case Settings.SizeUnits
                Case 0 ' Auto (Decimal - 1000)
                    Return ConvSize(size, 10, False)
                Case 1 ' Auto (Binary  - 1024)
                    Return ConvSize(size, 10, True)
                Case 2 ' bytes (8 bits)
                    Return ConvSize(size, 0, False)
                Case 3 ' kB  (Decimal - 1000)
                    Return ConvSize(size, 1, False)
                Case 4 ' KiB (Binary  - 1024)
                    Return ConvSize(size, 1, True)
                Case 5 ' MB  (Decimal - 1000)
                    Return ConvSize(size, 2, False)
                Case 6 ' MiB (Binary  - 1024)
                    Return ConvSize(size, 2, True)
                Case 7 ' GB  (Decimal - 1000)
                    Return ConvSize(size, 3, False)
                Case 8 ' GiB (Binary  - 1024)
                    Return ConvSize(size, 3, True)
                Case 9 ' TB  (Decimal - 1000)
                    Return ConvSize(size, 4, False)
                Case 10 'TiB (Binary  - 1024)
                    Return ConvSize(size, 4, True)
                Case 11 'PB  (Decimal - 1000)
                    Return ConvSize(size, 5, False)
                Case 12 'PiB (Binary  - 1024)
                    Return ConvSize(size, 5, True)
            End Select
            Return Nothing
        End Function

        ''' <param name="magnitude">
        ''' Order of magnitude, e.g. 0 for bytes, 1 for KB, 2 for MB
        ''' <br/>Use 10 for auto-selection
        ''' </param>
        Private Function ConvSize(size As Double, magnitude As Integer, binary As Boolean) As String
            Dim mult As Integer = If(binary, 1024, 1000)

            If magnitude = 10 Then
                For magnitude = 5 To 0 Step -1
                    If size > mult ^ magnitude Then Exit For
                Next
            End If

            size /= mult ^ magnitude
            If binary Then size = Math.Truncate(size * 1000 ^ magnitude) / 1000 ^ magnitude

            Dim postString As String = ""
            Select Case magnitude
                Case 0 : postString = " bytes"
                Case 1 : postString = If(binary, " KiB", " kB")
                Case 2 : postString = If(binary, " MiB", " MB")
                Case 3 : postString = If(binary, " GiB", " GB")
                Case 4 : postString = If(binary, " TiB", " TB")
                Case 5 : postString = If(binary, " PiB", " PB")
            End Select

            Return size.ToString("#,##0.### ### ### ### ### ### ### ###").Trim() & postString
        End Function

        ''' <summary>
        ''' <see cref="Path.GetFileName"/> except it works with ADSs
        ''' <br />Returns the file name and extension of the specified path string.
        ''' </summary>
        ''' <param name="fullPath">The path string from which to obtain the file name and extension.</param>
        ''' <returns>
        ''' The characters after the last directory character in <paramref name="fullPath"/>. If the last character of <paramref name="fullPath"/> is a directory separator character,
        ''' this method returns <see cref="[String].Empty"/>. If <paramref name="fullPath"/> is <see langword="Nothing"/>, this method returns <see langword="Nothing"/>.
        ''' </returns>
        Public Function GetFileName(fullPath As String) As String
            If fullPath Is Nothing Then Return Nothing

            Dim lDSC As Integer = fullPath.LastIndexOf(Path.DirectorySeparatorChar)
            If lDSC = -1 Then Return String.Empty

            Return fullPath.Substring(lDSC + 1)
        End Function
    End Module

    Module ADSHelpers
        Public Function GetADSFile(adsInfo As AlternateDataStreamInfo) As String
            Return Path.GetFileName(adsInfo.FilePath)
        End Function

        Public Function GetADSDirectory(adsInfo As AlternateDataStreamInfo) As String
            Return Path.GetDirectoryName(adsInfo.FilePath)
        End Function

        Public Function GetADSPath(adsInfo As AlternateDataStreamInfo) As String
            Return adsInfo.FilePath & ":" & adsInfo.Name
        End Function

        Public Function PathContainsADS(path As String) As Boolean
            Return path.Count(Function(c As Char) c = ":"c) > 1
        End Function

        Public Function GetADSPathStream(adsPath As String) As String
            Return adsPath.Substring(adsPath.IndexOf(":"c, 2) + 1) ' start at 2 to ignore DriveSeparator
        End Function

        Public Function GetADSPathFile(adsPath As String) As String
            Return adsPath.Remove(adsPath.IndexOf(":"c, 2)) ' start at 2 to ignore DriveSeparator
        End Function
    End Module
End Namespace
