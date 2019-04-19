using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace ClipboardManager.Utils
{
    public class Log
    {
        static StreamWriter m_Log = null;
        public static bool m_bWriteLog = true;

        public static void LogEventNfo(string msg)
        {
            LogEvent(msg, EventLogEntryType.Information);
        }//end LogEventNfo

        public static void LogEventErr(string msg)
        {
            LogEvent(msg, EventLogEntryType.Error);
        }//end LogEventErr

        public static void LogEvent(string msg, EventLogEntryType type)
        {
            try
            {
                EventLog.WriteEntry(FormClipboard.TITLE, msg, type);
            }//end try
            catch (Exception err)
            {
                MessageBoxIcon icn = type == EventLogEntryType.Information ?
                    MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show("Cannot write event log message:"+msg+"\r\nError: " + err.Message, 
                    FormClipboard.TITLE, MessageBoxButtons.OK, icn);
            }//end catch
        }//end LogEvent

        public static bool CreateLog()
        {
            if (m_Log != null)
                return true;

            try
            {
                string sFileName = Path.Combine(Path.GetDirectoryName(Application.LocalUserAppDataPath), FormClipboard.TITLE);
                string sLogFile = sFileName + ".log";

                FileInfo fi = new FileInfo(sLogFile);
                //restrict log size to 1M
                if (fi.Exists && fi.Length > 1024 * 1024)
                {
                    string sBackupFile = sFileName + ".1.log";
                    if (File.Exists(sBackupFile))
                        File.Delete(sBackupFile);
                    fi.MoveTo(sBackupFile);
                }//end if

                m_Log = new StreamWriter(sLogFile, true, Encoding.UTF8);
                WriteLine("=========================");
                WriteLine("[Log][CreateLog] - Log file: " + sLogFile);

                return true;
            }//end try
            catch (Exception err)
            {
                m_Log = null;
                System.Diagnostics.EventLog.WriteEntry(FormClipboard.TITLE,
                    "Exception creating log: " + err.Message + "\n",
                    System.Diagnostics.EventLogEntryType.Error);

                return false;
            }//end catch
        }//end CreateLog

        public static void CloseLog()
        {
            if (m_Log != null)
            {
                m_Log.Flush();
                m_Log.Close();
            }
            m_Log = null;
        }

        public static void FlushLog(bool bFlush = true)
        {
            if (m_Log != null && bFlush)
            {
                m_Log.FlushAsync();
            }
        }

        private static string GetFormattedDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", DateTimeFormatInfo.InvariantInfo);
        }

        public static string Write(string format, params object[] p)
        {
            return WriteLog(false, false, format, p);
        }

        public static string WriteLine(string format, params object[] p)
        {
            return WriteLog(false, true, format, p);
        }

        //write log line line and flush
        public static string WriteLineF(string format, params object[] p)
        {
            return WriteLog(true, true, format, p);
        }

        public static string WriteLineF(bool bFlush, string format, params object[] p)
        {
            return WriteLog(bFlush, true, format, p);
        }

        private static string WriteLog(bool bFlush, bool addNewLine, string format, params object[] p)
        {
            try
            {
                if (CreateLog())
                {
                    string msg = string.Format(format, p);
                    string log = GetFormattedDate() + " - " + msg;
                    if (addNewLine)
                        log += Environment.NewLine;

                    m_Log.Write(log);

                    if (bFlush)
                        m_Log.Flush();

                    Debug.Write(log);
                    return log;
                }
                return "Log Not Initialized";
            }
            catch (Exception err)
            {
                string msg = Environment.NewLine + "WriteLog Exception: " + err.ToString();
                Debug.Write(msg);
                File.AppendAllText(@"C:\Temp\ClipboardManagerLog11.txt", msg);
                return "WriteLog Exception: " + err.Message;
            }
        }
    }
}
