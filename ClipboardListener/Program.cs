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
		static Mutex m_SingleInstance = null;
		static int m_iFailCount = 0;
        static StreamWriter m_Log = null;
        public static bool m_bWriteLog = true;

#if (DEBUG)
		static string AppName = "ClipboardManager(Debug)";
#else
		static string AppName = "ClipboardManager";
#endif

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

            CreateLog();
RunAgain:
			try
			{
				Application.Run(new FormClipboard());
			}//end try
			catch ( Exception err )
			{
                Log("{0} : ", "[Main] Exeption: " + err.ToString()+"\n", true);

				m_iFailCount++;
                LogEventErr("Exception(No:" + m_iFailCount + ") in main: " + err.Message);
				if ( m_iFailCount < 4 )
					goto RunAgain;
			}//end catch

            if ( m_Log != null )
                m_Log.Close();
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

        static void LogEventNfo(string msg)
        {
            LogEvent(msg, EventLogEntryType.Information);
        }//end LogEventNfo

        static void LogEventErr(string msg)
        {
            LogEvent(msg, EventLogEntryType.Error);
        }//end LogEventErr

        static void LogEvent(string msg, EventLogEntryType type)
        {
            try
            {
                EventLog.WriteEntry(AppName, msg, type);
            }//end try
            catch (Exception)
            {
                MessageBoxIcon icn = type == EventLogEntryType.Information ? 
                    MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, icn);
            }//end catch
        }//end LogEvent

        static void CreateLog()
        {
            try
            {
                string path = Application.LocalUserAppDataPath;
                string sFileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + AppName;
                string sLogFile = Path.Combine(path, AppName+".log");

                FileInfo fi = new FileInfo(sLogFile);
                //restrict log size to 1M
                if ( fi.Exists && fi.Length > 1024*1024 )
                {
                    string sBackupFile = sFileName+".1.log";
                    if ( File.Exists(sBackupFile) )
                        File.Delete(sBackupFile);
                    fi.MoveTo(sBackupFile);
                }//end if

                m_Log = new StreamWriter(sLogFile, true, Encoding.UTF8);
                Log("", "\n=========================\n", false);
                Log("{0} : ", "[Main] Log file: " + sFileName + "\n", false);
            }//end try
            catch ( Exception err )
            {
                m_Log = null;
				System.Diagnostics.EventLog.WriteEntry(AppName,
                    "Exception creating log: " + err.Message + "\n",
					System.Diagnostics.EventLogEntryType.Error);
            }//end catch
        }//end CreateLog

		//if sDateFmt contains {0} parameter - date will be added
        //if sMessage is empty and bFlush - do flush only
        //if sMessage is empty and bFlush is false - return formatted date only
        public static string Log(string sDateFmt, string sMessage, bool bFlush)
        {
            string sDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff", DateTimeFormatInfo.InvariantInfo);
            
            if ( m_Log == null )
                return sDate;
            
            if ( m_bWriteLog )
            {
				if ( !string.IsNullOrEmpty(sMessage) )
				{
					string sLog = sMessage;
					try { sLog = string.Format(sDateFmt, sDate)+sMessage; }
					catch { sLog = "Bad date format: "+sDateFmt+"\n"+sMessage; }
					m_Log.Write(sLog);
				}//end if

                if ( bFlush )
                    m_Log.Flush();
            }//end if

            return sDate;
        }//end Log
	}//end class Program
}//end namespace ClipboardListener