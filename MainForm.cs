using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
	}
}
