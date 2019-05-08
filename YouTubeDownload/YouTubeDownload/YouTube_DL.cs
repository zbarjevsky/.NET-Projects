using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouTubeDownload.Extensions;

namespace YouTubeDownload
{
    //https://github.com/ytdl-org/youtube-dl/blob/master/README.md#readme
    public class YouTube_DL
    {
        public const string DL = @"Dependencies/youtube-dl.exe";

        public string Description { get; private set; } = "";
        public double Progress { get; private set; } = 0;
        public bool NoPlayList { get; private set; } = true;
        public string Url { get; private set; } = "";
        public string OutputFolder { get; private set; } = "";

        public Action<string> OutputDataReceived = (OutputData) => { };
        public Action ProcessExited = () => { };

        public void Start(bool noPlayList, string outputFolder, string url)
        {
            NoPlayList = noPlayList;
            OutputFolder = outputFolder;
            Url = url;

            string sNoPlayList = noPlayList ? "--no-playlist" : "";
            Process p = YouTube_DL.Create(
                string.Format(" \"{0}\" {1} -o \"{2}\\%(title)s-%(id)s.%(ext)s\"",
                url, sNoPlayList, outputFolder));

            p.OutputDataReceived += DL_Process_OutputDataReceived;
            p.Exited += DL_Process_Exited;
            p.Start();
            p.BeginOutputReadLine();
        }

        private void DL_Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e == null || e.Data == null)
                return;

            string line = e.Data;

            //parse
            ParseDestination(line);
            ParseProgress(line);

            OutputDataReceived(e.Data);
        }

        private void ParseDestination(string line)
        {
            const string DST1 = "[download] Destination: ";
            int pos1 = line.IndexOf(DST1);
            if (pos1 >= 0)
            {
                Description = line.Substring(pos1 + DST1.Length);
            }

            const string DST2 = "[ffmpeg] Merging formats into ";
            pos1 = line.IndexOf(DST2);
            if (pos1 >= 0)
            {
                Description = line.Substring(pos1 + DST2.Length);
            }
        }

        private void ParseProgress(string line)
        {
            int pos1 = line.IndexOf("[download]");
            if (pos1 >= 0)
            {
                int pos2 = line.IndexOf("% of ");
                if (pos2 > 0)
                {
                    string sPercent = line.Substring(pos1 + 10, pos2 - (pos1 + 10)).Trim();
                    Progress = double.Parse(sPercent);
                }
            }
        }

        private void DL_Process_Exited(object sender, EventArgs e)
        {
            Process p = sender as Process;
            if (p != null)
            {
                p.OutputDataReceived -= DL_Process_OutputDataReceived;
                p.Exited -= DL_Process_Exited;
            }

            Progress = 0;

            ProcessExited();
        }

        private static Process Create(string arguments)
        {
            return new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo(DL, arguments)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
        }

        public static async Task Update()
        {
            await new Process
            {
                StartInfo = new ProcessStartInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DL), "-U")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            }.RunAndWaitForExitAsync();
        }

        public static string GetVersion()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DL);
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);
                return versionInfo.ProductName + " ver: " + versionInfo.ProductVersion;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
