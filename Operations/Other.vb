Namespace Operations
    Public Class Other
        ' cMBb = CustomMsgBoxBtn
        Public Const cMBbRelaunch As String = "Relaunch as Admin"
        Public Const cMBbRunSysTool As String = "Run System Tool as Admin"
        Public Const cMBbCancel As String = "Cancel"
        Public Const cMBTitle As String = "Access denied!"

        Friend Shared Function Win32FromHResult(HResult As Integer) As Integer
            'getting Win32 error from HResult:
            ' https://docs.microsoft.com/en-us/dotnet/standard/io/handling-io-errors#handling-ioexception
            ' https://devblogs.microsoft.com/oldnewthing/20061103-07/?p=29133
            ' https://stackoverflow.com/a/426467/2999220
            Return (HResult And &HFFFF)
        End Function

        '32 (0x20) = ERROR_SHARING_VIOLATION: The process cannot access the file because it is being used by another process.
        Friend Const shareViolation As Integer = &H20
    End Class
End Namespace
