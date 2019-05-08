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
    public enum DownloadState
    {
        None,
        InQueue,
        Working,
        Done,
    }

    public class DownloadData
    {
        public DownloadState State { get; set; } = DownloadState.None;
        public string OutputFolder { get; set; } = "";
        public string FileName { get; set; } = "";
        public bool NoPlayList { get; set; } = true;
        public string Url { get; set; } = "";
        public double Progress { get; set; } = 0;
    }

    //https://github.com/ytdl-org/youtube-dl/blob/master/README.md#readme
    public class YouTube_DL
    {
        public const string DL = @"Dependencies/youtube-dl.exe";

        public DownloadData Data = new DownloadData();

        public Action<string> OutputDataReceived = (OutputData) => { };
        public Action ProcessExited = () => { };

        public void Start(DownloadData data)
        {
            Data = data;
            Data.State = DownloadState.Working;

            string sNoPlayList = Data.NoPlayList ? "--no-playlist" : "";
            Process p = YouTube_DL.Create(
                string.Format(" \"{0}\" {1} -o \"{2}\\%(title)s-%(id)s.%(ext)s\"",
                Data.Url, sNoPlayList, Data.OutputFolder));

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
                Data.FileName = line.Substring(pos1 + DST1.Length);
            }

            const string DST2 = "[ffmpeg] Merging formats into ";
            pos1 = line.IndexOf(DST2);
            if (pos1 >= 0)
            {
                Data.FileName = line.Substring(pos1 + DST2.Length);
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
                    Data.Progress = double.Parse(sPercent);
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

            Data.Progress = 0;
            Data.State = DownloadState.Done;

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
