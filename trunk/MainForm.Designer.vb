<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.MenuBar = New System.Windows.Forms.MenuStrip
        Me.lblCPUtxt = New System.Windows.Forms.Label
        Me.lblCPU = New System.Windows.Forms.Label
        Me.copyText = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.copyTextBtn = New System.Windows.Forms.ToolStripMenuItem
        Me.copyText.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuBar
        '
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Padding = New System.Windows.Forms.Padding(9, 3, 0, 3)
        Me.MenuBar.Size = New System.Drawing.Size(668, 24)
        Me.MenuBar.TabIndex = 0
        Me.MenuBar.Text = "MenuStrip1"
        '
        'lblCPUtxt
        '
        Me.lblCPUtxt.AutoSize = True
        Me.lblCPUtxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCPUtxt.Location = New System.Drawing.Point(13, 24)
        Me.lblCPUtxt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCPUtxt.Name = "lblCPUtxt"
        Me.lblCPUtxt.Size = New System.Drawing.Size(55, 20)
        Me.lblCPUtxt.TabIndex = 1
        Me.lblCPUtxt.Text = "CPU: "
        '
        'lblCPU
        '
        Me.lblCPU.AutoSize = True
        Me.lblCPU.ContextMenuStrip = Me.copyText
        Me.lblCPU.Location = New System.Drawing.Point(60, 24)
        Me.lblCPU.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCPU.Name = "lblCPU"
        Me.lblCPU.Size = New System.Drawing.Size(476, 20)
        Me.lblCPU.TabIndex = 1
        Me.lblCPU.Text = "Intel(R) Core(TM) i5 CPU750  @ 2.67GHz, 1.1v, 4 cores, 4 threads"
        '
        'copyText
        '
        Me.copyText.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.copyTextBtn})
        Me.copyText.Name = "copyText"
        Me.copyText.Size = New System.Drawing.Size(128, 26)
        '
        'copyTextBtn
        '
        Me.copyTextBtn.Name = "copyTextBtn"
        Me.copyTextBtn.Size = New System.Drawing.Size(152, 22)
        Me.copyTextBtn.Text = "Copy Text"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(668, 240)
        Me.Controls.Add(Me.lblCPU)
        Me.Controls.Add(Me.lblCPUtxt)
        Me.Controls.Add(Me.MenuBar)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenuStrip = Me.MenuBar
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "MainForm"
        Me.Text = "Pi Calculator"
        Me.copyText.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuBar As System.Windows.Forms.MenuStrip
    Friend WithEvents lblCPUtxt As System.Windows.Forms.Label
    Friend WithEvents lblCPU As System.Windows.Forms.Label
    Friend WithEvents copyText As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents copyTextBtn As System.Windows.Forms.ToolStripMenuItem

End Class
