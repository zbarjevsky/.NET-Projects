using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerBT.Utils
{
    public class Log
    {
        private static string _fileName;

        public static string DataFolder
        {
            get
            {
                string commonPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string dataFolder = Path.Combine(commonPath, "MarkZ", "BarometerBT");
                Directory.CreateDirectory(dataFolder);
                return dataFolder;
            }
        }

        static Log()
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("BarometerLog_{0}.log", date);
            _fileName = Path.Combine(DataFolder, fileName);
        }

        public static void d(string format, params object[] args)
        {
            w("DEBUG", format, args);
        }

        public static void e(string format, params object[] args)
        {
            w("ERROR", format, args);
        }

        public static void i(string format, params object[] args)
        {
            w("INFO", format, args);
        }

        public static void w(string type, string format, params object[] args)
        {
            string text = string.Format(format, args);
            string message = string.Format("{0} - {1} - {2}\n", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK"), type, text);
            File.AppendAllText(_fileName, message);
            Debug.Write(message);
        }
    }
}
