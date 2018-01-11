using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MeditationStopWatch
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				Form frm = new FormStopWatch();
				Application.Run(frm);
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message, "Meditation");
			}
		}
	}
}
