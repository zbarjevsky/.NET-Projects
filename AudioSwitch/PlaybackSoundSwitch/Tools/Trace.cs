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
        private static string _fileName = "PlaybackSoundSwitch.log";

        static Trace()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _fileName = Path.Combine(path, _fileName);
        }

        public static void Debug(string format, params object [] args)
        {
            string msg = string.Format(format, args);
            File.AppendAllText(_fileName, msg);
        }
    }
}
