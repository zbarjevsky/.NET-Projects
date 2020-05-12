using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MZ.Tools
{
    public static class Trace
    {
        private static string _fileName;

        static Trace()
        {
            string fullName = Assembly.GetExecutingAssembly().Location;
            string fileName = Path.GetFileNameWithoutExtension(fullName) + ".log";
            string path = Path.GetDirectoryName(fullName);
            _fileName = Path.Combine(path, fileName);
        }

        public static void Debug(string format, params object [] args)
        {
            string msg = string.Format(format, args);
            System.Diagnostics.Debug.Write(msg);
            File.AppendAllText(_fileName, msg);
        }
    }
}
