﻿using System;
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
            if (!SingleInstance())
                return; //already running

            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				Form frm = new FormStopWatch();
				Application.Run(frm);
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message, "Media Player - Time", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private static bool SingleInstance()
        {
            return !(MZ.Tools.SingleInstanceHelper.GlobalShowWindow(FormStopWatch.TITLE));
        }//end SingleInstance
    }
}
