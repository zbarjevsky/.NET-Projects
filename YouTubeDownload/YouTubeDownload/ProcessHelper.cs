using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDownload
{
    public class ProcessHelper
    {
        private List<string> _lines = new List<string>(1024);

        public static List<string> GetStdOut(string fileName, string arguments)
        {
            return new ProcessHelper().GetStdOutImpl(fileName, arguments);
        }

        private List<string> GetStdOutImpl(string fileName, string arguments)
        {
            Process p = Create(fileName, arguments);

            p.OutputDataReceived += DL_Process_OutputDataReceived;
            p.Exited += DL_Process_Exited;
            p.Start();
            p.BeginOutputReadLine();

            p.WaitForExit(3000);

            return _lines;
        }

        private void DL_Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if(e != null && e.Data != null)
                _lines.Add(e.Data);
        }

        private void DL_Process_Exited(object sender, EventArgs e)
        {
            Process p = sender as Process;

            p.OutputDataReceived -= DL_Process_OutputDataReceived;
            p.Exited -= DL_Process_Exited;
        }

        private static Process Create(string fileName, string arguments)
        {
            return new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo(fileName, arguments)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
        }
    }
}
