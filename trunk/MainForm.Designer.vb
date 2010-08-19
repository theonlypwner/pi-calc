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
		Me.menuSave = New System.Windows.Forms.ToolStripMenuItem
		Me.cmbBuffer = New System.Windows.Forms.ToolStripComboBox
		Me.lblCPUtitle = New System.Windows.Forms.Label
		Me.lblCPU = New System.Windows.Forms.Label
		Me.copyText = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.copyTextBtn = New System.Windows.Forms.ToolStripMenuItem
		Me.lblOStitle = New System.Windows.Forms.Label
		Me.lblOS = New System.Windows.Forms.Label
		Me.lblMemoryTitle = New System.Windows.Forms.Label
		Me.lblMemory = New System.Windows.Forms.Label
		Me.lblSep = New System.Windows.Forms.Label
		Me.lblDigits = New System.Windows.Forms.Label
		Me.numPrecision = New System.Windows.Forms.NumericUpDown
		Me.cmbPrecision = New System.Windows.Forms.ComboBox
		Me.cmbDScale = New System.Windows.Forms.ComboBox
		Me.btnGo = New System.Windows.Forms.Button
		Me.btnStop = New System.Windows.Forms.Button
		Me.StatusBar = New System.Windows.Forms.StatusStrip
		Me.progressText = New System.Windows.Forms.ToolStripStatusLabel
		Me.progress = New System.Windows.Forms.ToolStripProgressBar
		Me.lblCalcTitle = New System.Windows.Forms.Label
		Me.lblDispTitle = New System.Windows.Forms.Label
		Me.lblCalc = New System.Windows.Forms.Label
		Me.lblDisplay = New System.Windows.Forms.Label
		Me.lblCRCtitle = New System.Windows.Forms.Label
		Me.lblCRC = New System.Windows.Forms.Label
		Me.txtResult = New System.Windows.Forms.TextBox
		Me.lblPriority = New System.Windows.Forms.Label
		Me.lblPriorityTitle = New System.Windows.Forms.Label
		Me.MenuBar.SuspendLayout()
		Me.copyText.SuspendLayout()
		CType(Me.numPrecision, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.StatusBar.SuspendLayout()
		Me.SuspendLayout()
		'
		'MenuBar
		'
		Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuFile, Me.cmbBuffer})
		Me.MenuBar.Location = New System.Drawing.Point(0, 0)
		Me.MenuBar.Name = "MenuBar"
		Me.MenuBar.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
		Me.MenuBar.Size = New System.Drawing.Size(551, 27)
		Me.MenuBar.TabIndex = 0
		'
		'menuFile
		'
		Me.menuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuSave})
		Me.menuFile.Name = "menuFile"
		Me.menuFile.Size = New System.Drawing.Size(37, 23)
		Me.menuFile.Text = "File"
		'
		'menuSave
		'
		Me.menuSave.Name = "menuSave"
		Me.menuSave.Size = New System.Drawing.Size(98, 22)
		Me.menuSave.Text = "Save"
		'
		'cmbBuffer
		'
		Me.cmbBuffer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbBuffer.Items.AddRange(New Object() {"No buffer", "Buffer in memory", "First 2000 to display", "Save as File"})
		Me.cmbBuffer.Name = "cmbBuffer"
		Me.cmbBuffer.Size = New System.Drawing.Size(121, 23)
		'
		'lblCPUtitle
		'
		Me.lblCPUtitle.AutoSize = True
		Me.lblCPUtitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblCPUtitle.ForeColor = System.Drawing.SystemColors.HotTrack
		Me.lblCPUtitle.Location = New System.Drawing.Point(37, 30)
		Me.lblCPUtitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblCPUtitle.Name = "lblCPUtitle"
		Me.lblCPUtitle.Size = New System.Drawing.Size(47, 16)
		Me.lblCPUtitle.TabIndex = 1
		Me.lblCPUtitle.Text = "CPU: "
		Me.lblCPUtitle.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblCPU
		'
		Me.lblCPU.AutoSize = True
		Me.lblCPU.ContextMenuStrip = Me.copyText
		Me.lblCPU.ForeColor = System.Drawing.SystemColors.HotTrack
		Me.lblCPU.Location = New System.Drawing.Point(85, 30)
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
		'lblOStitle
		'
		Me.lblOStitle.AutoSize = True
		Me.lblOStitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblOStitle.ForeColor = System.Drawing.SystemColors.HotTrack
		Me.lblOStitle.Location = New System.Drawing.Point(47, 46)
		Me.lblOStitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblOStitle.Name = "lblOStitle"
		Me.lblOStitle.Size = New System.Drawing.Size(37, 16)
		Me.lblOStitle.TabIndex = 1
		Me.lblOStitle.Text = "OS: "
		Me.lblOStitle.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblOS
		'
		Me.lblOS.AutoSize = True
		Me.lblOS.ContextMenuStrip = Me.copyText
		Me.lblOS.ForeColor = System.Drawing.SystemColors.HotTrack
		Me.lblOS.Location = New System.Drawing.Point(85, 46)
		Me.lblOS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblOS.Name = "lblOS"
		Me.lblOS.Size = New System.Drawing.Size(271, 16)
		Me.lblOS.TabIndex = 1
		Me.lblOS.Text = "Microsoft Windows 7 Ultimate  6.1.7600 64-bit"
		'
		'lblMemoryTitle
		'
		Me.lblMemoryTitle.AutoSize = True
		Me.lblMemoryTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblMemoryTitle.ForeColor = System.Drawing.SystemColors.HotTrack
		Me.lblMemoryTitle.Location = New System.Drawing.Point(13, 62)
		Me.lblMemoryTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblMemoryTitle.Name = "lblMemoryTitle"
		Me.lblMemoryTitle.Size = New System.Drawing.Size(71, 16)
		Me.lblMemoryTitle.TabIndex = 1
		Me.lblMemoryTitle.Text = "Memory: "
		Me.lblMemoryTitle.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblMemory
		'
		Me.lblMemory.AutoSize = True
		Me.lblMemory.ContextMenuStrip = Me.copyText
		Me.lblMemory.ForeColor = System.Drawing.SystemColors.HotTrack
		Me.lblMemory.Location = New System.Drawing.Point(85, 62)
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
		Me.lblSep.Location = New System.Drawing.Point(0, 84)
		Me.lblSep.Name = "lblSep"
		Me.lblSep.Size = New System.Drawing.Size(556, 2)
		Me.lblSep.TabIndex = 2
		'
		'lblDigits
		'
		Me.lblDigits.AutoSize = True
		Me.lblDigits.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblDigits.ForeColor = System.Drawing.Color.YellowGreen
		Me.lblDigits.Location = New System.Drawing.Point(13, 91)
		Me.lblDigits.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblDigits.Name = "lblDigits"
		Me.lblDigits.Size = New System.Drawing.Size(52, 16)
		Me.lblDigits.TabIndex = 3
		Me.lblDigits.Text = "Digits:"
		Me.lblDigits.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'numPrecision
		'
		Me.numPrecision.Location = New System.Drawing.Point(72, 89)
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
		Me.cmbPrecision.Location = New System.Drawing.Point(178, 89)
		Me.cmbPrecision.Name = "cmbPrecision"
		Me.cmbPrecision.Size = New System.Drawing.Size(60, 24)
		Me.cmbPrecision.TabIndex = 5
		'
		'cmbDScale
		'
		Me.cmbDScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbDScale.FormattingEnabled = True
		Me.cmbDScale.Items.AddRange(New Object() {"K: 1024; M - 1024K", "k: 1000; m - 1000k"})
		Me.cmbDScale.Location = New System.Drawing.Point(244, 89)
		Me.cmbDScale.Name = "cmbDScale"
		Me.cmbDScale.Size = New System.Drawing.Size(121, 24)
		Me.cmbDScale.TabIndex = 6
		'
		'btnGo
		'
		Me.btnGo.Location = New System.Drawing.Point(371, 91)
		Me.btnGo.Name = "btnGo"
		Me.btnGo.Size = New System.Drawing.Size(75, 23)
		Me.btnGo.TabIndex = 7
		Me.btnGo.Text = "Calculate"
		Me.btnGo.UseVisualStyleBackColor = True
		'
		'btnStop
		'
		Me.btnStop.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnStop.Enabled = False
		Me.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.btnStop.Location = New System.Drawing.Point(452, 91)
		Me.btnStop.Name = "btnStop"
		Me.btnStop.Size = New System.Drawing.Size(75, 23)
		Me.btnStop.TabIndex = 7
		Me.btnStop.Text = "Break"
		Me.btnStop.UseVisualStyleBackColor = True
		'
		'StatusBar
		'
		Me.StatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.progressText, Me.progress})
		Me.StatusBar.Location = New System.Drawing.Point(0, 373)
		Me.StatusBar.Name = "StatusBar"
		Me.StatusBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
		Me.StatusBar.Size = New System.Drawing.Size(551, 22)
		Me.StatusBar.SizingGrip = False
		Me.StatusBar.TabIndex = 8
		'
		'progressText
		'
		Me.progressText.AutoSize = False
		Me.progressText.Name = "progressText"
		Me.progressText.Size = New System.Drawing.Size(35, 17)
		Me.progressText.Text = "100%"
		Me.progressText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'progress
		'
		Me.progress.Name = "progress"
		Me.progress.Size = New System.Drawing.Size(510, 16)
		Me.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
		Me.progress.Tag = "<width difference>"
		Me.progress.Value = 100
		'
		'lblCalcTitle
		'
		Me.lblCalcTitle.AutoSize = True
		Me.lblCalcTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblCalcTitle.ForeColor = System.Drawing.SystemColors.MenuHighlight
		Me.lblCalcTitle.Location = New System.Drawing.Point(13, 117)
		Me.lblCalcTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblCalcTitle.Name = "lblCalcTitle"
		Me.lblCalcTitle.Size = New System.Drawing.Size(128, 16)
		Me.lblCalcTitle.TabIndex = 3
		Me.lblCalcTitle.Text = "Calculation Time:"
		Me.lblCalcTitle.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblDispTitle
		'
		Me.lblDispTitle.AutoSize = True
		Me.lblDispTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblDispTitle.ForeColor = System.Drawing.SystemColors.MenuHighlight
		Me.lblDispTitle.Location = New System.Drawing.Point(37, 133)
		Me.lblDispTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblDispTitle.Name = "lblDispTitle"
		Me.lblDispTitle.Size = New System.Drawing.Size(104, 16)
		Me.lblDispTitle.TabIndex = 3
		Me.lblDispTitle.Text = "Display Time:"
		Me.lblDispTitle.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblCalc
		'
		Me.lblCalc.AutoSize = True
		Me.lblCalc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblCalc.ForeColor = System.Drawing.SystemColors.ActiveCaption
		Me.lblCalc.Location = New System.Drawing.Point(149, 116)
		Me.lblCalc.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblCalc.Name = "lblCalc"
		Me.lblCalc.Size = New System.Drawing.Size(184, 16)
		Me.lblCalc.TabIndex = 3
		Me.lblCalc.Text = "00h 00m 00s 000ms 000µs"
		Me.lblCalc.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblDisplay
		'
		Me.lblDisplay.AutoSize = True
		Me.lblDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblDisplay.ForeColor = System.Drawing.SystemColors.ActiveCaption
		Me.lblDisplay.Location = New System.Drawing.Point(149, 133)
		Me.lblDisplay.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblDisplay.Name = "lblDisplay"
		Me.lblDisplay.Size = New System.Drawing.Size(124, 16)
		Me.lblDisplay.TabIndex = 3
		Me.lblDisplay.Text = "00s 000ms 000µs"
		Me.lblDisplay.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblCRCtitle
		'
		Me.lblCRCtitle.AutoSize = True
		Me.lblCRCtitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblCRCtitle.ForeColor = System.Drawing.SystemColors.MenuHighlight
		Me.lblCRCtitle.Location = New System.Drawing.Point(398, 133)
		Me.lblCRCtitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblCRCtitle.Name = "lblCRCtitle"
		Me.lblCRCtitle.Size = New System.Drawing.Size(43, 16)
		Me.lblCRCtitle.TabIndex = 3
		Me.lblCRCtitle.Text = "CRC:"
		Me.lblCRCtitle.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblCRC
		'
		Me.lblCRC.AutoSize = True
		Me.lblCRC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblCRC.ForeColor = System.Drawing.SystemColors.ActiveCaption
		Me.lblCRC.Location = New System.Drawing.Point(449, 133)
		Me.lblCRC.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblCRC.Name = "lblCRC"
		Me.lblCRC.Size = New System.Drawing.Size(70, 16)
		Me.lblCRC.TabIndex = 3
		Me.lblCRC.Text = "Unknown"
		Me.lblCRC.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'txtResult
		'
		Me.txtResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtResult.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.txtResult.Location = New System.Drawing.Point(12, 152)
		Me.txtResult.Multiline = True
		Me.txtResult.Name = "txtResult"
		Me.txtResult.ReadOnly = True
		Me.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtResult.Size = New System.Drawing.Size(527, 218)
		Me.txtResult.TabIndex = 9
		'
		'lblPriority
		'
		Me.lblPriority.AutoSize = True
		Me.lblPriority.ForeColor = System.Drawing.SystemColors.HotTrack
		Me.lblPriority.Location = New System.Drawing.Point(486, 62)
		Me.lblPriority.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblPriority.Name = "lblPriority"
		Me.lblPriority.Size = New System.Drawing.Size(52, 16)
		Me.lblPriority.TabIndex = 1
		Me.lblPriority.Text = "Normal"
		'
		'lblPriorityTitle
		'
		Me.lblPriorityTitle.AutoSize = True
		Me.lblPriorityTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblPriorityTitle.ForeColor = System.Drawing.SystemColors.HotTrack
		Me.lblPriorityTitle.Location = New System.Drawing.Point(407, 62)
		Me.lblPriorityTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblPriorityTitle.Name = "lblPriorityTitle"
		Me.lblPriorityTitle.Size = New System.Drawing.Size(65, 16)
		Me.lblPriorityTitle.TabIndex = 1
		Me.lblPriorityTitle.Text = "Priority: "
		Me.lblPriorityTitle.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnStop
		Me.ClientSize = New System.Drawing.Size(551, 395)
		Me.Controls.Add(Me.MenuBar)
		Me.Controls.Add(Me.btnStop)
		Me.Controls.Add(Me.txtResult)
		Me.Controls.Add(Me.btnGo)
		Me.Controls.Add(Me.cmbPrecision)
		Me.Controls.Add(Me.numPrecision)
		Me.Controls.Add(Me.lblDigits)
		Me.Controls.Add(Me.lblCalcTitle)
		Me.Controls.Add(Me.lblSep)
		Me.Controls.Add(Me.lblDisplay)
		Me.Controls.Add(Me.lblCalc)
		Me.Controls.Add(Me.lblDispTitle)
		Me.Controls.Add(Me.lblMemoryTitle)
		Me.Controls.Add(Me.cmbDScale)
		Me.Controls.Add(Me.lblPriorityTitle)
		Me.Controls.Add(Me.lblCRCtitle)
		Me.Controls.Add(Me.lblCRC)
		Me.Controls.Add(Me.lblOS)
		Me.Controls.Add(Me.StatusBar)
		Me.Controls.Add(Me.lblCPUtitle)
		Me.Controls.Add(Me.lblMemory)
		Me.Controls.Add(Me.lblOStitle)
		Me.Controls.Add(Me.lblCPU)
		Me.Controls.Add(Me.lblPriority)
		Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.MainMenuStrip = Me.MenuBar
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.MinimumSize = New System.Drawing.Size(567, 414)
		Me.Name = "MainForm"
		Me.Text = "Pi Calculator"
		Me.MenuBar.ResumeLayout(False)
		Me.MenuBar.PerformLayout()
		Me.copyText.ResumeLayout(False)
		CType(Me.numPrecision, System.ComponentModel.ISupportInitialize).EndInit()
		Me.StatusBar.ResumeLayout(False)
		Me.StatusBar.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents MenuBar As System.Windows.Forms.MenuStrip
	Friend WithEvents lblCPUtitle As System.Windows.Forms.Label
	Friend WithEvents lblCPU As System.Windows.Forms.Label
	Friend WithEvents copyText As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents copyTextBtn As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents lblOStitle As System.Windows.Forms.Label
	Friend WithEvents lblOS As System.Windows.Forms.Label
	Friend WithEvents lblMemoryTitle As System.Windows.Forms.Label
	Friend WithEvents lblMemory As System.Windows.Forms.Label
	Friend WithEvents menuFile As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents lblSep As System.Windows.Forms.Label
	Friend WithEvents lblDigits As System.Windows.Forms.Label
	Friend WithEvents numPrecision As System.Windows.Forms.NumericUpDown
	Friend WithEvents cmbPrecision As System.Windows.Forms.ComboBox
	Friend WithEvents cmbDScale As System.Windows.Forms.ComboBox
	Friend WithEvents btnGo As System.Windows.Forms.Button
	Friend WithEvents btnStop As System.Windows.Forms.Button
	Friend WithEvents cmbBuffer As System.Windows.Forms.ToolStripComboBox
	Friend WithEvents StatusBar As System.Windows.Forms.StatusStrip
	Friend WithEvents progress As System.Windows.Forms.ToolStripProgressBar
	Friend WithEvents lblCalcTitle As System.Windows.Forms.Label
	Friend WithEvents lblDispTitle As System.Windows.Forms.Label
	Friend WithEvents progressText As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents lblCalc As System.Windows.Forms.Label
	Friend WithEvents lblDisplay As System.Windows.Forms.Label
	Friend WithEvents lblCRCtitle As System.Windows.Forms.Label
	Friend WithEvents lblCRC As System.Windows.Forms.Label
	Friend WithEvents txtResult As System.Windows.Forms.TextBox
	Friend WithEvents lblPriority As System.Windows.Forms.Label
	Friend WithEvents lblPriorityTitle As System.Windows.Forms.Label
	Friend WithEvents menuSave As System.Windows.Forms.ToolStripMenuItem

End Class
