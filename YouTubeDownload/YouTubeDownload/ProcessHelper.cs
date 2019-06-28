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
            return new ProcessHelper().GetStdOutImpl(fileName, arguments, -1);
        }

        //https://stackoverflow.com/questions/1259084/what-encoding-code-page-is-cmd-exe-using/17177904#17177904
        //https://stackoverflow.com/questions/38533903/set-c-sharp-console-application-to-unicode-output
        private List<string> GetStdOutImpl(string fileName, string arguments, int timeoutMs)
        {
            Process p = Create(fileName, arguments, Encoding.UTF8);

            p.OutputDataReceived += DL_Process_OutputDataReceived;
            p.Exited += DL_Process_Exited;
            p.Start();
            p.BeginOutputReadLine();

            p.WaitForExit(timeoutMs);

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

        public static Process Create(string fileName, string arguments, Encoding enc)
        {
            return new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo(fileName, arguments)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = enc
                }
            };
        }
    }
}
