<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExpandableList
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lstMain = New System.Windows.Forms.ListView()
        Me.btnExpandCollapse = New System.Windows.Forms.Button()
        Me.lblItemCount = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lstMain
        '
        Me.lstMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstMain.HideSelection = False
        Me.lstMain.Location = New System.Drawing.Point(1, 1)
        Me.lstMain.Name = "lstMain"
        Me.lstMain.Size = New System.Drawing.Size(627, 350)
        Me.lstMain.TabIndex = 0
        Me.lstMain.UseCompatibleStateImageBehavior = False
        Me.lstMain.View = System.Windows.Forms.View.Details
        '
        'btnExpandCollapse
        '
        Me.btnExpandCollapse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExpandCollapse.Location = New System.Drawing.Point(628, 0)
        Me.btnExpandCollapse.Name = "btnExpandCollapse"
        Me.btnExpandCollapse.Size = New System.Drawing.Size(20, 20)
        Me.btnExpandCollapse.TabIndex = 2
        Me.btnExpandCollapse.Text = "▼"
        Me.btnExpandCollapse.UseVisualStyleBackColor = True
        '
        'lblItemCount
        '
        Me.lblItemCount.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblItemCount.AutoSize = True
        Me.lblItemCount.Location = New System.Drawing.Point(5, 169)
        Me.lblItemCount.Name = "lblItemCount"
        Me.lblItemCount.Size = New System.Drawing.Size(44, 13)
        Me.lblItemCount.TabIndex = 3
        Me.lblItemCount.Text = "Items: 0"
        '
        'ExpandableList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblItemCount)
        Me.Controls.Add(Me.btnExpandCollapse)
        Me.Controls.Add(Me.lstMain)
        Me.Name = "ExpandableList"
        Me.Size = New System.Drawing.Size(648, 352)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstMain As System.Windows.Forms.ListView
    Friend WithEvents btnExpandCollapse As System.Windows.Forms.Button
    Friend WithEvents lblItemCount As System.Windows.Forms.Label
End Class
