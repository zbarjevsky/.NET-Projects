using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;

using MZ.Tools;
using Utils;
using MZ.Framework.Tools;

namespace ClipboardManager
{
	static class Program
	{
		//static Mutex m_SingleInstance = null;
		static int m_iFailCount = 0;

        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((x, y) =>
            {
                var exception = y.ExceptionObject as Exception;

                if (exception is System.IO.FileNotFoundException)
                    Console.WriteLine("Please make sure the DLL is in the same folder.");
                MessageBox.Show(exception.ToString(), "ClipboardManager - UnhandledException", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            });
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main(string [] args)
		{
            if (AssemblyTools.UpdateAssemblyInfoVersion(args))
                return;

            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

RunAgain:
			try
			{
                //single instance
                using (var mutex = new System.Threading.Mutex(true, FormClipboard.TITLE, out bool result))
                {
                    if (!result)
                    {
                        CenteredMessageBox.MsgBoxErr("Another instance of "+ FormClipboard.TITLE + " is already running.", FormClipboard.TITLE);
                        return;
                    }
                    Application.Run(new FormClipboard());
                }
			}//end try
			catch ( Exception err )
			{
                Utils.Log.WriteLineF("[Main] Exeption: " + err.ToString());

				m_iFailCount++;
                Utils.Log.LogEventErr("Exception(No:" + m_iFailCount + ") in main: " + err.Message);
				if ( m_iFailCount < 4 )
					goto RunAgain;
			}//end catch

            Utils.Log.CloseLog();
		}//end Main

        public static string GetUserPath()
        {
            string path = null; // Settings.Default.UserPath;
            if (string.IsNullOrEmpty(path))
                path = Application.UserAppDataPath;
            return path;
        }
	}//end class Program
}//end namespace ClipboardListener