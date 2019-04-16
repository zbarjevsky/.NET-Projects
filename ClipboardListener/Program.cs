using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;
using ClipboardManager.Properties;

namespace ClipboardManager
{
	static class Program
	{
		//static Mutex m_SingleInstance = null;
		static int m_iFailCount = 0;

//#if (DEBUG)
//		static string AppName = "ClipboardManager(Debug)";
//#else
//		static string AppName = "ClipboardManager";
//#endif

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			if ( !SingleInstance() )
				return; //already running

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

RunAgain:
			try
			{
				Application.Run(new FormClipboard());
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

        private static bool SingleInstance()
        {
            return !(SingleInstanceHelper.GlobalShowWindow(FormClipboard.TITLE));

            //try
            //{
            //    bool createdNew = false;
            //    m_SingleInstance = new Mutex(true, AppName, out createdNew);
            //    LogEventNfo("Started successfully");
            //    return createdNew;
            //}//end try
            //catch (Exception err)
            //{
            //    LogEventErr("Failure to start: " + err.Message);
            //    return false;
            //}//end catch
        }//end SingleInstance

        public static string GetUserPath()
        {
            string path = null; // Settings.Default.UserPath;
            if (string.IsNullOrEmpty(path))
                path = Application.UserAppDataPath;
            return path;
        }
	}//end class Program
}//end namespace ClipboardListener