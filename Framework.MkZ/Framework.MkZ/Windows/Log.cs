using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.Tools
{
    public class Log
    {
        private static string _fileName = null;
        private static string _dataFolder = null;

        public enum eLogTypes
        {
            DEBUG,
            INFO,
            ERROR
        }

        public static string DataFolder
        {
            get
            {
                return _dataFolder;
            }
        }

        static Log()
        {
            var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
            string commonPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dataFolder = Path.Combine(commonPath, "MkZ", assemblyName);
            Directory.CreateDirectory(_dataFolder);

            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("{0}_{1}.log", assemblyName, date);
            _fileName = Path.Combine(_dataFolder, fileName);
        }

        public static void d(string format, params object[] args)
        {
            w(eLogTypes.DEBUG, format, args);
        }

        public static void e(string format, params object[] args)
        {
            w(eLogTypes.ERROR, format, args);
        }

        public static void i(string format, params object[] args)
        {
            w(eLogTypes.INFO, format, args);
        }

        public static void w(eLogTypes logType, string format, params object[] args)
        {
            lock (_fileName)
            {
                try
                {
                    string text = string.Format(format, args);
                    string message = string.Format("{0} - {1} - {2}\n",
                        DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK"), logType, text);
                    File.AppendAllText(_fileName, message);
                    Debug.Write(message);
                }
                catch (Exception err)
                {
                    Debug.WriteLine("!!!Exception in Log!!! " + err.ToString());
                }            
            }
        }
    }
}
