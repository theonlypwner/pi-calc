using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Threading;

namespace Pi
{


	public partial class MainForm : Form
	{
		protected Thread t;
		protected CalculatePi calc;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Text = "Pi Calculator (" + IntToCPUCount(1, true) + ")";
			// CPU Information
			ObjectQuery q = new ObjectQuery("SELECT * FROM Win32_Processor");
			ManagementObjectSearcher p = new ManagementObjectSearcher(q);
			long i = 0, i2 = 0;
			string s = "";
			foreach (ManagementObject cpu in p.Get()){
				i += (int)cpu["NumberOfCores"]; // counts cores
				i2 += (int)cpu["NumberOfLogicalProcessors"]; // counts threads
				if (s == "") s = cpu["Name"].ToString().Replace("   ", "") + ", " + ((UInt16)cpu["CurrentVoltage"] / 10) + "v";
			}
			lblCPU.Text = s + ", " + i + " cores, " + i2 + " threads";
			// OS Information
			q = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
			p = new ManagementObjectSearcher(q);
			ManagementObject os = (ManagementObject)p.Get().GetEnumerator().Current;
			lblOS.Text = os["Caption"] + " " + os["Version"] + " " + os["OSArchitecture"];
			// Memory Information
			q = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
			p = new ManagementObjectSearcher(q);
			i = 0; s = "";
			foreach (ManagementObject mm in p.Get()) {
				i += (long)mm["Capacity"];
				if (s == "") {
					UInt16 mt = (UInt16)mm["MemoryType"];
					switch (mt) {
						case 20: s = "DDR"; break;
						case 21: s = "DDR2"; break;
						default:
							if(mt >= 1 && mt <= 19) s = "Unknown"; // non-DDR memory
							else s = "DDR3?"; //  Assume unknown memory (case 0, and 22+) to be DDR3
							break;
					}
				}
			}
			char[] sf = {'k', 'm', 'g'};
			i2 = 0;
			while ((i / (Math.Pow(1024, i2)) >= 1024) && sf.Length > i2) i2++;
			lblMemory.Text = s + " " + (i / Math.Pow(1024 , i2)) + (i2 > 0 ? sf[i2 - 1].ToString() : "") + "B";
			// Store progress size difference
			progress.Tag = this.Width - progress.Width;
		}

		/// <summary>Converts an integer to the number of threads (CPU thread count: 0, single, double, triple, quad, five...)</summary>
		/// <param name="i">The integer to convert</param>
		/// <param name="capital">If the result should be capital</param>
		private string IntToCPUCount(int i, bool capital = false) { 
			string result = i.ToString();
			switch(i){
				case 1 : result = "single"; break;
				case 2: result = "double"; break;
				case 3: result = "triple"; break;
				case 4: result = "quad"; break;
				case 5: result = "five"; break;
				case 6: result = "six"; break;
				case 7: result = "seven"; break;
				case 8: result = "eight"; break;
				case 9: result = "nine"; break;
				case 10: result = "ten"; break;
				case 11: result = "eleven"; break;
				case 12: result = "twelve"; break;
			}
			result += " thread" + (i == 1 ? "": "s");
			System.Globalization.TextInfo txtinfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
		    if(capital) result = txtinfo.ToTitleCase(result);
			else result = result.ToLower();
			return result;
		}

		// Fix for the size of the progressbar in the status bar
		private void MainForm_SizeChanged(object sender, EventArgs e){
			progress.Size = new System.Drawing.Size(this.Width - (int)progress.Tag, progress.Height);
		}

		private void copyTextBtn_Click(object sender, EventArgs e)
		{
			// Copy the static label text
			Clipboard.SetText(((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl.Text);
		}

		private void numPrecision_ValueChanged(object sender, EventArgs e)
		{
			if (numPrecision.Value % KprecisionP(2) == 0) { 
				// M
				string j = Convert.ToString((uint)(numPrecision.Value / KprecisionP(2)));
				foreach (string i in cmbPrecision.Items) if (i.IndexOf('M') > 0 && i.Replace("M", "") == j){
					cmbPrecision.SelectedItem = i;
					return;
				}
			}
			if (numPrecision.Value % KprecisionP() == 0) {
				string j = Convert.ToString((uint)(numPrecision.Value / KprecisionP()));
				foreach (string i in cmbPrecision.Items) if (i.IndexOf('K') > 0 && i.Replace("K", "") == j){
					cmbPrecision.SelectedItem = i;
					return;
				}
			}
			foreach (string i in cmbPrecision.Items) if (i.IndexOf('M') < 0 && i.IndexOf('K') < 0 && i == numPrecision.Value.ToString()){
				cmbPrecision.SelectedItem = i;
				return;
			}
			cmbPrecision.SelectedItem = "?";
		}

		private void precisionComboChanged(object sender, EventArgs e) // cmbPrecision.SelectedIndexChanged, cmbDScale.SelectedIndexChanged
		{
			if (cmbPrecision.SelectedIndex == 0) numPrecision_ValueChanged(sender, e);
			else if (cmbPrecision.SelectedItem.ToString().IndexOf('M') > 0)
				numPrecision.Value = Convert.ToInt32(cmbPrecision.SelectedItem.ToString().Replace("M", "")) * KprecisionP(2);
			else if (cmbPrecision.SelectedItem.ToString().IndexOf('K') > 0)
				numPrecision.Value = Convert.ToInt32(cmbPrecision.SelectedItem.ToString().Replace("K", "")) * KprecisionP();
			else numPrecision.Value = Convert.ToInt32(cmbPrecision.SelectedItem);
		}

		public int KprecisionP(byte pow = 1) {
			return (int)Math.Pow(cmbDScale.SelectedIndex == 0 ? 1024 : 1000, pow);
		}

		private void maxValChanged(object sender, EventArgs e) // cmbDScale.SelectedIndexChanged
		{
			string l = (string)cmbPrecision.Items[cmbPrecision.Items.Count - 1];
			if(l.IndexOf('M') > 0){
				l = l.Replace("M", "");
				l = (Convert.ToInt32(l) * KprecisionP(2)).ToString();
			}
			else if (l.IndexOf("K") > 0){
				l = l.Replace("K", "");
				l = (Convert.ToInt32(l) * KprecisionP()).ToString();
			}
			numPrecision.Maximum = Convert.ToDecimal(l);
			precisionComboChanged(sender, e);
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			// update GUI to allow stop
			btnGo.Enabled = false;
			btnStop.Enabled = true;
			// lock GUI
			numPrecision.Enabled = cmbDScale.Enabled = cmbPrecision.Enabled = false;
			// say processing
			txtResult.Text = "Processing";
			// update progress bar
			progress.Value = 0;
			progressText.Text = "0%";
			
			// create the thread
			calc = new CalculatePi((int)numPrecision.Value);
			calc.onProgress += new ByteHandler(calcProgress);
			calc.onComplete += new EventHandler(btnStop_Click);
			t = new Thread(calc.process);
			t.Name = "Pi Calculator Calculation Thread";
			// start thread
			t.Start();
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			if (btnGo.Enabled || t == null) return;
			if (InvokeRequired) {
				Invoke(new EventHandler(btnStop_Click), sender, e);
				return;
			}
			// update GUI to allow start
			btnGo.Enabled = true;
			btnStop.Enabled = false;
			// unlock GUI
			numPrecision.Enabled = cmbDScale.Enabled = cmbPrecision.Enabled = true;
			// fill progress bar
			progress.Value = 100;
			// update progress text
			progressText.Text = "Idle";
			// delete thread
			t.Abort();
			t = null;
			// retrieve data
			/*
			' retrieve data and delete piCalc
		Dim r As CalculatePi.TimedResult = piCalc.ResultDataNoDelete(If(cmbBuffer.SelectedIndex > 0, CalculatePi.ResultType.First2K, CalculatePi.ResultType.BufferOnly))
		txtResult.Text = If(sender Is btnStop, "Calculation was stopped" & CrLf, "") & r.s
		' ticks = 100-nanoseconds
		r.timeDiff = New TimeSpan(Now.Ticks - r.timeStart)
		lblDisplay.Text = r.timeDiff.Seconds.ToString.PadLeft(2, "0"c) & "s " & r.timeDiff.Milliseconds.ToString.PadLeft(3, "0"c) & "ms " & _
		 CStr(Math.Round(r.timeDiff.Ticks * 10) Mod 1000).PadLeft(3, "0"c) & "µs"
		piCalc.diffTicks = New TimeSpan(Now.Ticks - piCalc.startTicks)
		lblCalc.Text = piCalc.diffTicks.Hours.ToString.PadLeft(2, "0"c) & "h " & piCalc.diffTicks.Minutes.ToString.PadLeft(2, "0"c) & "m " & _
		 piCalc.diffTicks.Seconds.ToString.PadLeft(2, "0"c) & "s " & piCalc.diffTicks.Milliseconds.ToString.PadLeft(3, "0"c) & "ms " & _
		 CStr(Math.Round(piCalc.diffTicks.Ticks * 10) Mod 1000).PadLeft(3, "0"c) & "µs"
		If cmbBuffer.SelectedIndex > 1 Then	' save to file
			If sender Is btnStop Then
				MsgBox("Calculation was stopped; not saving your calculation", MsgBoxStyle.Critical, "Cannot save file!")
			Else
				saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
				If saveDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
					Dim sPath As String = saveDialog.FileName
					If sPath.Length > 33 Then sPath = "..." & sPath.Substring(sPath.Length - 30)
					MsgBox(sPath, MsgBoxStyle.Information, "File saved to:")
					r = piCalc.ResultData(CalculatePi.ResultType.All)
					File.WriteAllText(saveDialog.FileName, r.s)
				End If
			End If
		End If
		*/
			// delete calculator
			calc = null;
		}

		protected void calcProgress(byte p) {
			if (InvokeRequired) { Invoke(new ByteHandler(calcProgress), p); return; }
			progress.Value = p;
			progressText.Text = p.ToString().PadLeft(2, '0') + "%";
		}

		private void menuClose_Click(object sender, EventArgs e) { this.Close(); }

		public bool creditsShown = false;
		private void menuCredits_Click(object sender, EventArgs e)
		{
			if (creditsShown) return;
			Credits c = new Credits(this);
			c.Show();
		}
	}
}
