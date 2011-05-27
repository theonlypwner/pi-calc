using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pi
{
	static class Program
	{
		public static MainForm MainForm1;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(MainForm1 = new MainForm());
		}
	}
}
