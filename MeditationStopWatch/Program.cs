using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MeditationStopWatch
{
	static class Program
	{
        static Program()
        {
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((x, y) =>
			{
				var exception = y.ExceptionObject as Exception;

				if (exception is System.IO.FileNotFoundException)
					Console.WriteLine("Please make sure the DLL is in the same folder.");
				Console.WriteLine(exception);
			});
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            //MZ.Framework.Tools.DependenciesTool.LoadFrameworkMkZ_Dependency(Properties.Resources.Framework_MkZ);

			if (MkZ.Framework.Tools.AssemblyTools.UpdateAssemblyInfoVersion(args))
                return;

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
            return !(MkZ.Tools.SingleInstanceHelper.GlobalShowWindow(FormStopWatch.TITLE));
        }//end SingleInstance
    }
}
