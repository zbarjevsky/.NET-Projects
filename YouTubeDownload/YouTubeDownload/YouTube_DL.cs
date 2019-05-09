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
        Skipped,
        Succsess,
        Failed,
        Stopped,
    }

    public class DownloadData
    {
        public DownloadState State { get; set; } = DownloadState.None;
        public string OutputFolder { get; set; } = "";
        public string FileName { get; set; } = "";
        public bool NoPlayList { get; set; } = true;
        public bool ExtractAudio { get; set; } = false;
        public string Url { get; set; } = "";
        public double Progress { get; set; } = 0;
    }

    //https://github.com/ytdl-org/youtube-dl/blob/master/README.md#readme
    public class YouTube_DL
    {
        public const string DL = @"Dependencies/youtube-dl.exe";
        public const string FF = @"Dependencies/ffmpeg.exe";

        public DownloadData Data = new DownloadData();

        public Action<string> OutputDataReceived = (OutputData) => { };
        public Action ProcessExited = () => { };

        private Process _DL_Process = null;

        public void Start(DownloadData data)
        {
            Data = data;
            Data.State = DownloadState.Working;

            string parameters = Data.NoPlayList ? "--no-playlist" : "";
            parameters += Data.ExtractAudio ? " --extract-audio --audio-format mp3" : "";

            _DL_Process = YouTube_DL.Create(
                string.Format(" \"{0}\" {1} -o \"{2}\\%(title)s-%(id)s.%(ext)s\"",
                Data.Url, parameters, Data.OutputFolder));

            _DL_Process.OutputDataReceived += DL_Process_OutputDataReceived;
            _DL_Process.Exited += DL_Process_Exited;
            _DL_Process.Start();
            _DL_Process.BeginOutputReadLine();
        }

        public void Stop()
        {
            if(_DL_Process != null && !_DL_Process.HasExited)
                _DL_Process.Kill();
        }

        private void DL_Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e == null || e.Data == null)
                return;

            string line = e.Data;

            //parse
            ParseDestination(line);
            ParseProgress(line);
            ParseStatus(line);

            OutputDataReceived(e.Data);
        }

        private void DL_Process_Exited(object sender, EventArgs e)
        {
            int exitCode = -1;
            if (_DL_Process != null)
            {
                _DL_Process.OutputDataReceived -= DL_Process_OutputDataReceived;
                _DL_Process.Exited -= DL_Process_Exited;
                exitCode = _DL_Process.ExitCode;
            }

            Data.Progress = 0;
            if (Data.State == DownloadState.Working)
            {
                Data.State = (exitCode == 0) ? DownloadState.Succsess : DownloadState.InQueue;
            }

            ProcessExited();
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

            const string DST3 = "[ffmpeg] Destination: ";
            pos1 = line.IndexOf(DST3);
            if (pos1 >= 0)
            {
                Data.FileName = line.Substring(pos1 + DST3.Length);
            }
        }

        private void ParseStatus(string line)
        {
            const string SUFFIX = "has already been downloaded and merged";
            int pos = line.IndexOf(SUFFIX);
            if (pos > 0)
            {
                Data.State = DownloadState.Skipped;
                Data.FileName = line.Substring(11, line.Length - SUFFIX.Length - 11);
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

        internal static string GetVersionFFMpeg()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FF);
                List<string> ver = ProcessHelper.GetStdOut(path, " -version");

                return ver.Aggregate("\n", (current, s) => current + (" " + s + Environment.NewLine));
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
