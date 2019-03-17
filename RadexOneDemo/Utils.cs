using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadexOneDemo
{
    public class Utils
    {
        public static void ExecuteOnUiThreadInvoke(Form app, Action action)
        {
            ExecuteOnUiThreadInvoke(app, () => { action(); return 0; });
        }

        public static TResult ExecuteOnUiThreadInvoke<TResult>(Form app, Func<TResult> func)
        {
            if (app.InvokeRequired)
            {
                return (TResult)app.Invoke(func);
            }
            else
            {
                return func();
            }
        }

        //http://stackoverflow.com/questions/6394304/algorithm-how-do-i-fade-from-red-to-green-via-yellow-using-rgb-values

        public static Color GetBlendedColor(int percentage)
        {
            if (percentage < 50)
                return Interpolate(Color.Red, Color.Yellow, percentage / 50.0);
            else
                return Interpolate(Color.Yellow, Color.Green, (percentage - 50) / 50.0);
        }

        private static Color Interpolate(Color color1, Color color2, double fraction)
        {
            double r = Interpolate(color1.R, color2.R, fraction);
            double g = Interpolate(color1.G, color2.G, fraction);
            double b = Interpolate(color1.B, color2.B, fraction);
            return Color.FromArgb((int)Math.Round(r), (int)Math.Round(g), (int)Math.Round(b));
        }

        private static double Interpolate(double d1, double d2, double fraction)
        {
            return d1 + (d2 - d1) * fraction;
        }
    }

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
            if(_stream == null)
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
                Debug.WriteLine("Exception: " +err.Message);
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

    public static class ModifyProgressBarColor
    {
        public enum State
        {
            Green = 1,  //normal
            Red = 2,    //error
            Yellow = 3  //warning
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, State state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
            pBar.Invalidate();
        }
    }
}
