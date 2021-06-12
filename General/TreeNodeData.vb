Imports System
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms

Public Class TreeNodeData
    Public ReadOnly Property Owner As TreeNode
    Public Sub New(owner As TreeNode, settings As Settings,
                   loadAction As Func(Of TreeNode, CancellationToken, Task()),
                   unloadAction As Action(Of TreeNode))
        Me.Owner = owner
        Me.Owner.Tag = Me

        Me.settings = settings
        Me.loadAction = loadAction
        Me.unloadAction = unloadAction
    End Sub
    Public Shared Function AssignData(treeNode As TreeNode, settings As Settings,
                                      loadAction As Func(Of TreeNode, CancellationToken, Task()),
                                      unloadAction As Action(Of TreeNode)) As TreeNodeData
        Return New TreeNodeData(treeNode, settings, loadAction, unloadAction)
    End Function
    Public Shared Function GetData(treeNode As TreeNode) As TreeNodeData
        Return DirectCast(treeNode.Tag, TreeNodeData)
    End Function

    Private loadingTask As Task = Nothing
    Private cancelTokenSource As CancellationTokenSource = Nothing
    Private cancelToken As CancellationToken = Nothing
    Private subTasks As Task()
    Private WithEvents fsw As IO.FileSystemWatcher
    Private ReadOnly settings As Settings
    Private ReadOnly loadAction As Func(Of TreeNode, CancellationToken, Task())
    Private ReadOnly unloadAction As Action(Of TreeNode)

    Private Sub fsw_ItemChanged() Handles fsw.Changed, fsw.Created, fsw.Deleted, fsw.Renamed
        If Not Me.settings.DisableTreeAutoUpdate Then
            fsw.EnableRaisingEvents = False

            If cancelTokenSource IsNot Nothing Then
                cancelTokenSource.Cancel()

                If loadingTask IsNot Nothing Then loadingTask.Wait()
                WaitForSubTasks.Wait()
            End If
            Load()
        End If
    End Sub

    Private Sub internalLoad()
        Try
            subTasks = loadAction.Invoke(Owner, cancelToken)
            fsw.EnableRaisingEvents = True
        Catch
            subTasks = Nothing
            fsw.EnableRaisingEvents = False
        End Try
        loadingTask = Nothing

        If subTasks IsNot Nothing Then
            WaitForSubTasks().ContinueWith(Sub()
                                               cancelTokenSource = Nothing
                                               cancelToken = Nothing
                                           End Sub, TaskScheduler.Default)
        Else
            cancelTokenSource = Nothing
            cancelToken = Nothing
        End If
    End Sub

    Public Function Load() As Task
        cancelTokenSource = New CancellationTokenSource()
        cancelToken = cancelTokenSource.Token
        fsw = New IO.FileSystemWatcher(Owner.FixedFullPath()) With {
            .NotifyFilter = IO.NotifyFilters.DirectoryName
        }
        loadingTask = Task.Run(AddressOf internalLoad)

        Return loadingTask
    End Function

    Public Async Function Unload() As Task
        If fsw IsNot Nothing Then fsw.EnableRaisingEvents = False
        fsw = Nothing

        If cancelTokenSource IsNot Nothing Then
            cancelTokenSource.Cancel()

            If loadingTask IsNot Nothing Then Await loadingTask
            Await WaitForSubTasks()
        End If

        unloadAction.Invoke(Owner)
    End Function

    Public Function GetLoadTask() As Task
        Return loadingTask
    End Function

    Public Async Function WaitForSubTasks() As Task
        If subTasks IsNot Nothing Then
            If subTasks(0) IsNot Nothing Then
                Await subTasks(0)
            End If
            If subTasks(1) IsNot Nothing Then
                Await subTasks(1)
            End If
        End If
    End Function

    Public Sub CancelLoad()
        If cancelTokenSource IsNot Nothing Then
            cancelTokenSource.Cancel()
        End If
    End Sub
End Class
