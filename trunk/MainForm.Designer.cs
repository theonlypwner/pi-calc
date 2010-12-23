namespace Pi
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.MenuBar = new System.Windows.Forms.MenuStrip();
			this.menuBuffer = new System.Windows.Forms.ToolStripMenuItem();
			this.cmbBuffer = new System.Windows.Forms.ToolStripComboBox();
			this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
			this.btnStop = new System.Windows.Forms.Button();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.cmbPrecision = new System.Windows.Forms.ComboBox();
			this.numPrecision = new System.Windows.Forms.NumericUpDown();
			this.lblDigits = new System.Windows.Forms.Label();
			this.lblCalcTitle = new System.Windows.Forms.Label();
			this.lblSep = new System.Windows.Forms.Label();
			this.lblDisplay = new System.Windows.Forms.Label();
			this.lblCalc = new System.Windows.Forms.Label();
			this.lblDispTitle = new System.Windows.Forms.Label();
			this.lblMemoryTitle = new System.Windows.Forms.Label();
			this.cmbDScale = new System.Windows.Forms.ComboBox();
			this.lblPriorityTitle = new System.Windows.Forms.Label();
			this.lblCRCtitle = new System.Windows.Forms.Label();
			this.lblCRC = new System.Windows.Forms.Label();
			this.lblOS = new System.Windows.Forms.Label();
			this.lblCPUtitle = new System.Windows.Forms.Label();
			this.lblMemory = new System.Windows.Forms.Label();
			this.lblOStitle = new System.Windows.Forms.Label();
			this.lblCPU = new System.Windows.Forms.Label();
			this.lblPriority = new System.Windows.Forms.Label();
			this.StatusBar = new System.Windows.Forms.StatusStrip();
			this.progressText = new System.Windows.Forms.ToolStripStatusLabel();
			this.progress = new System.Windows.Forms.ToolStripProgressBar();
			this.CopyInfoTextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyTextBtn = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
			this.MenuBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPrecision)).BeginInit();
			this.StatusBar.SuspendLayout();
			this.CopyInfoTextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuBar
			// 
			this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBuffer,
            this.cmbBuffer,
            this.menuClose});
			this.MenuBar.Location = new System.Drawing.Point(0, 0);
			this.MenuBar.Name = "MenuBar";
			this.MenuBar.Size = new System.Drawing.Size(551, 27);
			this.MenuBar.TabIndex = 0;
			this.MenuBar.Text = "MenuBar";
			// 
			// menuBuffer
			// 
			this.menuBuffer.Name = "menuBuffer";
			this.menuBuffer.Size = new System.Drawing.Size(67, 23);
			this.menuBuffer.Text = "Buffer in:";
			// 
			// cmbBuffer
			// 
			this.cmbBuffer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBuffer.Items.AddRange(new object[] {
            "Buffer in memory",
            "First 2000 to display",
            "Save as File"});
			this.cmbBuffer.Name = "cmbBuffer";
			this.cmbBuffer.Size = new System.Drawing.Size(150, 23);
			// 
			// menuClose
			// 
			this.menuClose.Name = "menuClose";
			this.menuClose.Size = new System.Drawing.Size(48, 23);
			this.menuClose.Text = "Close";
			// 
			// btnStop
			// 
			this.btnStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnStop.Enabled = false;
			this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnStop.Location = new System.Drawing.Point(452, 91);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 29;
			this.btnStop.Text = "Break";
			this.btnStop.UseVisualStyleBackColor = true;
			// 
			// txtResult
			// 
			this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtResult.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.txtResult.Location = new System.Drawing.Point(12, 152);
			this.txtResult.Multiline = true;
			this.txtResult.Name = "txtResult";
			this.txtResult.ReadOnly = true;
			this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtResult.Size = new System.Drawing.Size(527, 218);
			this.txtResult.TabIndex = 31;
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(371, 91);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 30;
			this.btnGo.Text = "Calculate";
			this.btnGo.UseVisualStyleBackColor = true;
			// 
			// cmbPrecision
			// 
			this.cmbPrecision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPrecision.FormattingEnabled = true;
			this.cmbPrecision.Items.AddRange(new object[] {
            "?",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1K",
            "2K",
            "4K",
            "8K",
            "16K",
            "32K",
            "64K",
            "128K",
            "256K",
            "512K",
            "1M",
            "2M",
            "4M",
            "8M",
            "16M",
            "32M",
            "64M",
            "128M",
            "256M",
            "257M"});
			this.cmbPrecision.Location = new System.Drawing.Point(178, 89);
			this.cmbPrecision.Name = "cmbPrecision";
			this.cmbPrecision.Size = new System.Drawing.Size(60, 24);
			this.cmbPrecision.TabIndex = 27;
			// 
			// numPrecision
			// 
			this.numPrecision.Location = new System.Drawing.Point(72, 89);
			this.numPrecision.Maximum = new decimal(new int[] {
            269484032,
            0,
            0,
            0});
			this.numPrecision.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.numPrecision.Name = "numPrecision";
			this.numPrecision.Size = new System.Drawing.Size(100, 22);
			this.numPrecision.TabIndex = 26;
			this.numPrecision.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			// 
			// lblDigits
			// 
			this.lblDigits.AutoSize = true;
			this.lblDigits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDigits.ForeColor = System.Drawing.Color.YellowGreen;
			this.lblDigits.Location = new System.Drawing.Point(13, 91);
			this.lblDigits.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDigits.Name = "lblDigits";
			this.lblDigits.Size = new System.Drawing.Size(52, 16);
			this.lblDigits.TabIndex = 23;
			this.lblDigits.Text = "Digits:";
			this.lblDigits.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblCalcTitle
			// 
			this.lblCalcTitle.AutoSize = true;
			this.lblCalcTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCalcTitle.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.lblCalcTitle.Location = new System.Drawing.Point(13, 117);
			this.lblCalcTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCalcTitle.Name = "lblCalcTitle";
			this.lblCalcTitle.Size = new System.Drawing.Size(128, 16);
			this.lblCalcTitle.TabIndex = 22;
			this.lblCalcTitle.Text = "Calculation Time:";
			this.lblCalcTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblSep
			// 
			this.lblSep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblSep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblSep.Location = new System.Drawing.Point(0, 84);
			this.lblSep.Name = "lblSep";
			this.lblSep.Size = new System.Drawing.Size(556, 2);
			this.lblSep.TabIndex = 18;
			// 
			// lblDisplay
			// 
			this.lblDisplay.AutoSize = true;
			this.lblDisplay.ContextMenuStrip = this.CopyInfoTextMenu;
			this.lblDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDisplay.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.lblDisplay.Location = new System.Drawing.Point(149, 133);
			this.lblDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDisplay.Name = "lblDisplay";
			this.lblDisplay.Size = new System.Drawing.Size(124, 16);
			this.lblDisplay.TabIndex = 21;
			this.lblDisplay.Text = "00s 000ms 000µs";
			this.lblDisplay.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblCalc
			// 
			this.lblCalc.AutoSize = true;
			this.lblCalc.ContextMenuStrip = this.CopyInfoTextMenu;
			this.lblCalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCalc.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.lblCalc.Location = new System.Drawing.Point(149, 116);
			this.lblCalc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCalc.Name = "lblCalc";
			this.lblCalc.Size = new System.Drawing.Size(184, 16);
			this.lblCalc.TabIndex = 25;
			this.lblCalc.Text = "00h 00m 00s 000ms 000µs";
			this.lblCalc.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblDispTitle
			// 
			this.lblDispTitle.AutoSize = true;
			this.lblDispTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDispTitle.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.lblDispTitle.Location = new System.Drawing.Point(37, 133);
			this.lblDispTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDispTitle.Name = "lblDispTitle";
			this.lblDispTitle.Size = new System.Drawing.Size(104, 16);
			this.lblDispTitle.TabIndex = 24;
			this.lblDispTitle.Text = "Display Time:";
			this.lblDispTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblMemoryTitle
			// 
			this.lblMemoryTitle.AutoSize = true;
			this.lblMemoryTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMemoryTitle.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblMemoryTitle.Location = new System.Drawing.Point(13, 62);
			this.lblMemoryTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblMemoryTitle.Name = "lblMemoryTitle";
			this.lblMemoryTitle.Size = new System.Drawing.Size(71, 16);
			this.lblMemoryTitle.TabIndex = 12;
			this.lblMemoryTitle.Text = "Memory: ";
			this.lblMemoryTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cmbDScale
			// 
			this.cmbDScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDScale.FormattingEnabled = true;
			this.cmbDScale.Items.AddRange(new object[] {
            "K: 1024; M - 1024K",
            "k: 1000; m - 1000k"});
			this.cmbDScale.Location = new System.Drawing.Point(244, 89);
			this.cmbDScale.Name = "cmbDScale";
			this.cmbDScale.Size = new System.Drawing.Size(121, 24);
			this.cmbDScale.TabIndex = 28;
			// 
			// lblPriorityTitle
			// 
			this.lblPriorityTitle.AutoSize = true;
			this.lblPriorityTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPriorityTitle.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblPriorityTitle.Location = new System.Drawing.Point(407, 62);
			this.lblPriorityTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPriorityTitle.Name = "lblPriorityTitle";
			this.lblPriorityTitle.Size = new System.Drawing.Size(65, 16);
			this.lblPriorityTitle.TabIndex = 13;
			this.lblPriorityTitle.Text = "Priority: ";
			this.lblPriorityTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblCRCtitle
			// 
			this.lblCRCtitle.AutoSize = true;
			this.lblCRCtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCRCtitle.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.lblCRCtitle.Location = new System.Drawing.Point(398, 133);
			this.lblCRCtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCRCtitle.Name = "lblCRCtitle";
			this.lblCRCtitle.Size = new System.Drawing.Size(43, 16);
			this.lblCRCtitle.TabIndex = 20;
			this.lblCRCtitle.Text = "CRC:";
			this.lblCRCtitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblCRC
			// 
			this.lblCRC.AutoSize = true;
			this.lblCRC.ContextMenuStrip = this.CopyInfoTextMenu;
			this.lblCRC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCRC.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.lblCRC.Location = new System.Drawing.Point(449, 133);
			this.lblCRC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCRC.Name = "lblCRC";
			this.lblCRC.Size = new System.Drawing.Size(70, 16);
			this.lblCRC.TabIndex = 19;
			this.lblCRC.Text = "Unknown";
			this.lblCRC.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblOS
			// 
			this.lblOS.AutoSize = true;
			this.lblOS.ContextMenuStrip = this.CopyInfoTextMenu;
			this.lblOS.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblOS.Location = new System.Drawing.Point(85, 46);
			this.lblOS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblOS.Name = "lblOS";
			this.lblOS.Size = new System.Drawing.Size(271, 16);
			this.lblOS.TabIndex = 10;
			this.lblOS.Text = "Microsoft Windows 7 Ultimate  6.1.7600 64-bit";
			// 
			// lblCPUtitle
			// 
			this.lblCPUtitle.AutoSize = true;
			this.lblCPUtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCPUtitle.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblCPUtitle.Location = new System.Drawing.Point(37, 30);
			this.lblCPUtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCPUtitle.Name = "lblCPUtitle";
			this.lblCPUtitle.Size = new System.Drawing.Size(47, 16);
			this.lblCPUtitle.TabIndex = 11;
			this.lblCPUtitle.Text = "CPU: ";
			this.lblCPUtitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblMemory
			// 
			this.lblMemory.AutoSize = true;
			this.lblMemory.ContextMenuStrip = this.CopyInfoTextMenu;
			this.lblMemory.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblMemory.Location = new System.Drawing.Point(85, 62);
			this.lblMemory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblMemory.Name = "lblMemory";
			this.lblMemory.Size = new System.Drawing.Size(106, 16);
			this.lblMemory.TabIndex = 16;
			this.lblMemory.Text = "DDR3-1333 8GB";
			// 
			// lblOStitle
			// 
			this.lblOStitle.AutoSize = true;
			this.lblOStitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblOStitle.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblOStitle.Location = new System.Drawing.Point(47, 46);
			this.lblOStitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblOStitle.Name = "lblOStitle";
			this.lblOStitle.Size = new System.Drawing.Size(37, 16);
			this.lblOStitle.TabIndex = 17;
			this.lblOStitle.Text = "OS: ";
			this.lblOStitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblCPU
			// 
			this.lblCPU.AutoSize = true;
			this.lblCPU.ContextMenuStrip = this.CopyInfoTextMenu;
			this.lblCPU.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblCPU.Location = new System.Drawing.Point(85, 30);
			this.lblCPU.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCPU.Name = "lblCPU";
			this.lblCPU.Size = new System.Drawing.Size(389, 16);
			this.lblCPU.TabIndex = 15;
			this.lblCPU.Text = "Intel(R) Core(TM) i5 CPU750  @ 2.67GHz, 1.1v, 4 cores, 4 threads";
			// 
			// lblPriority
			// 
			this.lblPriority.AutoSize = true;
			this.lblPriority.ContextMenuStrip = this.CopyInfoTextMenu;
			this.lblPriority.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblPriority.Location = new System.Drawing.Point(486, 62);
			this.lblPriority.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPriority.Name = "lblPriority";
			this.lblPriority.Size = new System.Drawing.Size(52, 16);
			this.lblPriority.TabIndex = 14;
			this.lblPriority.Text = "Normal";
			// 
			// StatusBar
			// 
			this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressText,
            this.progress});
			this.StatusBar.Location = new System.Drawing.Point(0, 373);
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.StatusBar.Size = new System.Drawing.Size(551, 22);
			this.StatusBar.SizingGrip = false;
			this.StatusBar.TabIndex = 32;
			this.StatusBar.Text = "StatusBar";
			// 
			// progressText
			// 
			this.progressText.AutoSize = false;
			this.progressText.Name = "progressText";
			this.progressText.Size = new System.Drawing.Size(35, 17);
			this.progressText.Text = "100%";
			this.progressText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// progress
			// 
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(510, 16);
			this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progress.Tag = "<width difference>";
			this.progress.Value = 100;
			// 
			// CopyInfoTextMenu
			// 
			this.CopyInfoTextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTextBtn});
			this.CopyInfoTextMenu.Name = "copyText";
			this.CopyInfoTextMenu.Size = new System.Drawing.Size(128, 26);
			this.CopyInfoTextMenu.Text = "CopyInfoTextMenu";
			// 
			// copyTextBtn
			// 
			this.copyTextBtn.Name = "copyTextBtn";
			this.copyTextBtn.Size = new System.Drawing.Size(127, 22);
			this.copyTextBtn.Text = "Copy Text";
			// 
			// SaveDialog
			// 
			this.SaveDialog.DefaultExt = "txt";
			this.SaveDialog.Filter = "Text File|*.txt|Binary file|*.bin|Other|*.*";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(551, 395);
			this.Controls.Add(this.StatusBar);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.txtResult);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.cmbPrecision);
			this.Controls.Add(this.numPrecision);
			this.Controls.Add(this.lblDigits);
			this.Controls.Add(this.lblCalcTitle);
			this.Controls.Add(this.lblSep);
			this.Controls.Add(this.lblDisplay);
			this.Controls.Add(this.lblCalc);
			this.Controls.Add(this.lblDispTitle);
			this.Controls.Add(this.lblMemoryTitle);
			this.Controls.Add(this.cmbDScale);
			this.Controls.Add(this.lblPriorityTitle);
			this.Controls.Add(this.lblCRCtitle);
			this.Controls.Add(this.lblCRC);
			this.Controls.Add(this.lblOS);
			this.Controls.Add(this.lblCPUtitle);
			this.Controls.Add(this.lblMemory);
			this.Controls.Add(this.lblOStitle);
			this.Controls.Add(this.lblCPU);
			this.Controls.Add(this.lblPriority);
			this.Controls.Add(this.MenuBar);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MenuBar;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MinimumSize = new System.Drawing.Size(567, 414);
			this.Name = "MainForm";
			this.Text = "Pi Calculator (changed in code at runtime)";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.MenuBar.ResumeLayout(false);
			this.MenuBar.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPrecision)).EndInit();
			this.StatusBar.ResumeLayout(false);
			this.StatusBar.PerformLayout();
			this.CopyInfoTextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MenuBar;
		internal System.Windows.Forms.ToolStripMenuItem menuBuffer;
		internal System.Windows.Forms.ToolStripComboBox cmbBuffer;
		internal System.Windows.Forms.ToolStripMenuItem menuClose;
		internal System.Windows.Forms.Button btnStop;
		internal System.Windows.Forms.TextBox txtResult;
		internal System.Windows.Forms.Button btnGo;
		internal System.Windows.Forms.ComboBox cmbPrecision;
		internal System.Windows.Forms.NumericUpDown numPrecision;
		internal System.Windows.Forms.Label lblDigits;
		internal System.Windows.Forms.Label lblCalcTitle;
		internal System.Windows.Forms.Label lblSep;
		internal System.Windows.Forms.Label lblDisplay;
		internal System.Windows.Forms.Label lblCalc;
		internal System.Windows.Forms.Label lblDispTitle;
		internal System.Windows.Forms.Label lblMemoryTitle;
		internal System.Windows.Forms.ComboBox cmbDScale;
		internal System.Windows.Forms.Label lblPriorityTitle;
		internal System.Windows.Forms.Label lblCRCtitle;
		internal System.Windows.Forms.Label lblCRC;
		internal System.Windows.Forms.Label lblOS;
		internal System.Windows.Forms.Label lblCPUtitle;
		internal System.Windows.Forms.Label lblMemory;
		internal System.Windows.Forms.Label lblOStitle;
		internal System.Windows.Forms.Label lblCPU;
		internal System.Windows.Forms.Label lblPriority;
		private System.Windows.Forms.StatusStrip StatusBar;
		internal System.Windows.Forms.ToolStripStatusLabel progressText;
		internal System.Windows.Forms.ToolStripProgressBar progress;
		internal System.Windows.Forms.ContextMenuStrip CopyInfoTextMenu;
		internal System.Windows.Forms.ToolStripMenuItem copyTextBtn;
		internal System.Windows.Forms.SaveFileDialog SaveDialog;
	}
}

