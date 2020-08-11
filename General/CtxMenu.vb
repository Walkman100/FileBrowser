Imports System
Imports System.Collections.Generic

Public Class CtxMenu
    Public Enum ActionType
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be an array with two strings</summary>
        Launch
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be an array with two strings</summary>
        Admin
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be an array with two strings</summary>
        Execute
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        Show
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be an array with two strings</summary>
        Properties
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        OpenWith

        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        Cut
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        Copy
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        Paste

        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        PasteAsHardlink
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        PasteAsSymlink
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        PasteAsShortcut
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be a string</summary>
        PasteAsJunction

        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be Nothing</summary>
        CopyTo
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be Nothing</summary>
        MoveTo
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be Nothing</summary>
        DeleteToRecycleBin
        ''' <summary><see cref="EntryInfo.ActionArgs"/> should be Nothing</summary>
        DeletePermanently
    End Enum

    Structure EntryInfo
        Public IconPath As String
        Public Text As String
        Public AdminIcon As Boolean

        'when to show
        Public Extended As Boolean
        Public DirectoryOnly As Boolean
        Public DriveOnly As Boolean
        Public FileOnly As Boolean
        ''' <summary>
        ''' Item filters separated by ";".
        ''' See https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator#pattern-options for filter info
        ''' </summary>
        Public Filter As String

        Public ActionType As ActionType
        Public ActionArgs As Object
    End Structure

    Private entryDict As New Dictionary(Of Integer, EntryInfo)
End Class
