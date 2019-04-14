using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiskCryptorHelper
{
    public static class Log
    {
        private static string _fileName;

        static Log()
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _fileName = Path.Combine(dir, "DiskCryptorLog.txt");
        }

        public static void Write(string format, params object[] p)
        {
            WriteLog(false, format, p);
        }

        public static void WriteLine(string format, params object[] p)
        {
            WriteLog(true, format, p);
        }

        private static void WriteLog(bool addNewLine, string format, params object[] p)
        {
            try
            {
                string msg = string.Format(format, p);
                string log = DateTime.Now.ToString("u") + " - " + msg;
                if (addNewLine)
                    log += Environment.NewLine;
                File.AppendAllText(_fileName, log);
            }
            catch (Exception err)
            {
                File.AppendAllText(@"C:\Temp\Log11.txt", Environment.NewLine + err.ToString());
            }
        }
    }
}
