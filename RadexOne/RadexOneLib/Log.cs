using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadexOneLib
{
    public class Log
    {
        private const int MAX_LINES = 100 * 1000, MAX_FLUSH = 200;
        private static int _lineCount = 0, _logFileIndex = 1;
        private static StreamWriter _stream = null;
        private static DateTime _timeCreated = DateTime.Now;
        private const string LogFolder = @"C:\Temp\Radex\";

        static Log()
        {
            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);

            CreateNewLogFile("Starting application - new log file", _logFileIndex++);
        }

        public static void Close()
        {
            if (_stream == null)
                return;

            lock (LogFolder)
            {
                _stream.Flush();
                _stream.Close();
                _stream.Dispose();
                _stream = null;
            }
        }

        public static void WriteLine(string line)
        {
            try
            {
                lock (LogFolder)
                {
                    if (_stream == null)
                        return;

                    string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff || ");

                    //Debug.WriteLine(time+line);
                    _stream.WriteLine(time + line);

                    _lineCount++;
                    if (_lineCount > MAX_LINES) //every 100,000 lines
                    {
                        CreateNewLogFile("Previous file is too big(>" + MAX_LINES + " lines), splitting...", _logFileIndex++);
                    }

                    if (_lineCount % MAX_FLUSH == 0) //every 200 lines
                    {
                        _stream.FlushAsync();
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine("Exception: " + err.Message);
            }
        }

        private static void CreateNewLogFile(string firstLine, int index)
        {
            Close();

            lock (LogFolder)
            {
                string logFileName = string.Format(@"{0}Radex{1}({2:000}).log", LogFolder, _timeCreated.ToString("yyyy-MM-dd HH-mm-ss"), index);
                _stream = File.AppendText(logFileName);
            }

            _lineCount = 0; //new file
            WriteLine(firstLine);
        }
    }
}
