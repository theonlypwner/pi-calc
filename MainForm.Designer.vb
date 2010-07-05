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
        Me.menuFile = New System.Windows.Forms.ToolStripMenuItem
        Me.lblCPUtxt = New System.Windows.Forms.Label
        Me.lblCPU = New System.Windows.Forms.Label
        Me.copyText = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.copyTextBtn = New System.Windows.Forms.ToolStripMenuItem
        Me.lblOStxt = New System.Windows.Forms.Label
        Me.lblOS = New System.Windows.Forms.Label
        Me.lblMemorytxt = New System.Windows.Forms.Label
        Me.lblMemory = New System.Windows.Forms.Label
        Me.lblSep = New System.Windows.Forms.Label
        Me.lblDigits = New System.Windows.Forms.Label
        Me.numPrecision = New System.Windows.Forms.NumericUpDown
        Me.cmbPrecision = New System.Windows.Forms.ComboBox
        Me.cmbDScale = New System.Windows.Forms.ComboBox
        Me.MenuBar.SuspendLayout()
        Me.copyText.SuspendLayout()
        CType(Me.numPrecision, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuBar
        '
        Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuFile})
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.MenuBar.Size = New System.Drawing.Size(552, 24)
        Me.MenuBar.TabIndex = 0
        Me.MenuBar.Text = "MenuStrip1"
        '
        'menuFile
        '
        Me.menuFile.Name = "menuFile"
        Me.menuFile.Size = New System.Drawing.Size(37, 20)
        Me.menuFile.Text = "File"
        '
        'lblCPUtxt
        '
        Me.lblCPUtxt.AutoSize = True
        Me.lblCPUtxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCPUtxt.Location = New System.Drawing.Point(37, 24)
        Me.lblCPUtxt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCPUtxt.Name = "lblCPUtxt"
        Me.lblCPUtxt.Size = New System.Drawing.Size(47, 16)
        Me.lblCPUtxt.TabIndex = 1
        Me.lblCPUtxt.Text = "CPU: "
        Me.lblCPUtxt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCPU
        '
        Me.lblCPU.AutoSize = True
        Me.lblCPU.ContextMenuStrip = Me.copyText
        Me.lblCPU.Location = New System.Drawing.Point(85, 24)
        Me.lblCPU.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCPU.Name = "lblCPU"
        Me.lblCPU.Size = New System.Drawing.Size(389, 16)
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
        Me.copyTextBtn.Size = New System.Drawing.Size(127, 22)
        Me.copyTextBtn.Text = "Copy Text"
        '
        'lblOStxt
        '
        Me.lblOStxt.AutoSize = True
        Me.lblOStxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOStxt.Location = New System.Drawing.Point(47, 40)
        Me.lblOStxt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOStxt.Name = "lblOStxt"
        Me.lblOStxt.Size = New System.Drawing.Size(37, 16)
        Me.lblOStxt.TabIndex = 1
        Me.lblOStxt.Text = "OS: "
        Me.lblOStxt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblOS
        '
        Me.lblOS.AutoSize = True
        Me.lblOS.ContextMenuStrip = Me.copyText
        Me.lblOS.Location = New System.Drawing.Point(85, 40)
        Me.lblOS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOS.Name = "lblOS"
        Me.lblOS.Size = New System.Drawing.Size(271, 16)
        Me.lblOS.TabIndex = 1
        Me.lblOS.Text = "Microsoft Windows 7 Ultimate  6.1.7600 64-bit"
        '
        'lblMemorytxt
        '
        Me.lblMemorytxt.AutoSize = True
        Me.lblMemorytxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMemorytxt.Location = New System.Drawing.Point(13, 56)
        Me.lblMemorytxt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMemorytxt.Name = "lblMemorytxt"
        Me.lblMemorytxt.Size = New System.Drawing.Size(71, 16)
        Me.lblMemorytxt.TabIndex = 1
        Me.lblMemorytxt.Text = "Memory: "
        Me.lblMemorytxt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMemory
        '
        Me.lblMemory.AutoSize = True
        Me.lblMemory.ContextMenuStrip = Me.copyText
        Me.lblMemory.Location = New System.Drawing.Point(85, 56)
        Me.lblMemory.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMemory.Name = "lblMemory"
        Me.lblMemory.Size = New System.Drawing.Size(106, 16)
        Me.lblMemory.TabIndex = 1
        Me.lblMemory.Text = "DDR3-1333 8GB"
        '
        'lblSep
        '
        Me.lblSep.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSep.Location = New System.Drawing.Point(0, 78)
        Me.lblSep.Name = "lblSep"
        Me.lblSep.Size = New System.Drawing.Size(557, 2)
        Me.lblSep.TabIndex = 2
        '
        'lblDigits
        '
        Me.lblDigits.AutoSize = True
        Me.lblDigits.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDigits.Location = New System.Drawing.Point(13, 85)
        Me.lblDigits.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDigits.Name = "lblDigits"
        Me.lblDigits.Size = New System.Drawing.Size(52, 16)
        Me.lblDigits.TabIndex = 3
        Me.lblDigits.Text = "Digits:"
        Me.lblDigits.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'numPrecision
        '
        Me.numPrecision.Location = New System.Drawing.Point(72, 83)
        Me.numPrecision.Maximum = New Decimal(New Integer() {269484032, 0, 0, 0})
        Me.numPrecision.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.numPrecision.Name = "numPrecision"
        Me.numPrecision.Size = New System.Drawing.Size(100, 22)
        Me.numPrecision.TabIndex = 4
        Me.numPrecision.Value = New Decimal(New Integer() {1024, 0, 0, 0})
        '
        'cmbPrecision
        '
        Me.cmbPrecision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPrecision.FormattingEnabled = True
        Me.cmbPrecision.Items.AddRange(New Object() {"?", "32", "64", "128", "256", "512", "1K", "2K", "4K", "8K", "16K", "32K", "64K", "128K", "256K", "512K", "1M", "2M", "4M", "8M", "16M", "32M", "64M", "128M", "256M", "257M"})
        Me.cmbPrecision.Location = New System.Drawing.Point(178, 83)
        Me.cmbPrecision.Name = "cmbPrecision"
        Me.cmbPrecision.Size = New System.Drawing.Size(60, 24)
        Me.cmbPrecision.TabIndex = 5
        '
        'cmbDScale
        '
        Me.cmbDScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDScale.FormattingEnabled = True
        Me.cmbDScale.Items.AddRange(New Object() {"K: 1024; M - 1024K", "k: 1000; m - 1000k"})
        Me.cmbDScale.Location = New System.Drawing.Point(244, 83)
        Me.cmbDScale.Name = "cmbDScale"
        Me.cmbDScale.Size = New System.Drawing.Size(121, 24)
        Me.cmbDScale.TabIndex = 6
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 197)
        Me.Controls.Add(Me.cmbPrecision)
        Me.Controls.Add(Me.numPrecision)
        Me.Controls.Add(Me.lblDigits)
        Me.Controls.Add(Me.lblSep)
        Me.Controls.Add(Me.lblMemorytxt)
        Me.Controls.Add(Me.cmbDScale)
        Me.Controls.Add(Me.lblOS)
        Me.Controls.Add(Me.MenuBar)
        Me.Controls.Add(Me.lblCPUtxt)
        Me.Controls.Add(Me.lblMemory)
        Me.Controls.Add(Me.lblOStxt)
        Me.Controls.Add(Me.lblCPU)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuBar
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "Pi Calculator"
        Me.MenuBar.ResumeLayout(False)
        Me.MenuBar.PerformLayout()
        Me.copyText.ResumeLayout(False)
        CType(Me.numPrecision, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuBar As System.Windows.Forms.MenuStrip
    Friend WithEvents lblCPUtxt As System.Windows.Forms.Label
    Friend WithEvents lblCPU As System.Windows.Forms.Label
    Friend WithEvents copyText As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents copyTextBtn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblOStxt As System.Windows.Forms.Label
    Friend WithEvents lblOS As System.Windows.Forms.Label
    Friend WithEvents lblMemorytxt As System.Windows.Forms.Label
    Friend WithEvents lblMemory As System.Windows.Forms.Label
    Friend WithEvents menuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblSep As System.Windows.Forms.Label
    Friend WithEvents lblDigits As System.Windows.Forms.Label
    Friend WithEvents numPrecision As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbPrecision As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDScale As System.Windows.Forms.ComboBox

End Class
