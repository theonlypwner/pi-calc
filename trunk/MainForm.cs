﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace Pi
{
	public partial class MainForm : Form
	{
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
			i2 = 0; // now counting elements to pop
			if (i < 268435456) this.Close(); // You should have at least 256 MB of RAM
			if (i <= 536870912) i2 = 5; // (256MB to 512MB) not suitable for more than 16M
			i = i2 > 0 ? 1 : 0;
			while (i2-- > 0) cmbPrecision.Items.RemoveAt(cmbPrecision.Items.Count - 1);
			if(i > 1) maxValChanged(this, null); // popped elements, update maximum
			// Store progress size difference
			progress.Tag = this.Width - progress.Width;
			// Looks like it's the stupid comboboxes once again!
			cmbPrecision.SelectedIndex = 7; // 1K
			cmbDScale.SelectedIndex = 0; // K; not k
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
		}

		private void btnGo_Click(object sender, EventArgs e)
		{

		}

		private void btnStop_Click(object sender, EventArgs e)
		{

		}

		// calcProgress()...

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
