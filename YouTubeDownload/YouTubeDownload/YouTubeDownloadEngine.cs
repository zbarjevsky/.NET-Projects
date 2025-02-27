﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using MkZ.Extensions;

namespace YouTubeDownload
{
    //https://github.com/ytdl-org/youtube-dl/blob/master/README.md#readme
    //https://github.com/yt-dlp/yt-dlp#new-features
    public class YouTubeDownloadEngine
    {
        public const string FF = @"Engine/youtube-dl/ffmpeg.exe";

        public static readonly string[] ENGINES = 
        {
            @"Engine\yt-dlp\yt-dlp.exe",
            @"Engine\youtube-dl\youtube-dl.exe",
        };

        //public const string DL = @"Engine/youtube-dl/youtube-dl.exe";
        //public const string DLP = @"Engine/yt-dlp/yt-dlp.exe";

        public DownloadData Data = new DownloadData();

        public Action<string> OutputDataReceived = (OutputData) => { };
        public Action ProcessExited = () => { };

        private Process _DL_Process = null;

        public void Start(DownloadData data, bool noWindow)
        {
            Data = data;
            Data.State = eDownloadState.Working;

            string pathToEngine = YouTubeDownloadEngine.ENGINES[data.SelectedEngineIndex];
            string parameters = PrepareCommanLine(data, out string exePath);

            _DL_Process = ProcessHelper.Create(exePath, parameters, data.Encoding, noWindow);

            _DL_Process.OutputDataReceived += DL_Process_OutputDataReceived;
            _DL_Process.ErrorDataReceived += DL_Process_OutputDataReceived;
            _DL_Process.Exited += DL_Process_Exited;
            _DL_Process.Start();

            _DL_Process.BeginOutputReadLine();
        }

        public static string PrepareCommanLine(DownloadData data, out string exePath)
        {
            string pathToEngine = YouTubeDownloadEngine.ENGINES[data.SelectedEngineIndex];
            exePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), pathToEngine);

            string parameters = "--encoding UTF8 ";
            parameters += data.NoPlayList ? "--no-playlist " : " ";
            parameters += data.AudioOnly ? " --extract-audio --audio-format mp3 " : " ";
            parameters += data.AdditionalParameters;

            string outParams = $" {parameters} -o \"{data.OutputFolder}\\{data.FileNameTemplate}\" \"{data.Url}\"";

            return outParams;
        }

        public void Stop()
        {
            if(_DL_Process != null && !_DL_Process.HasExited)
                _DL_Process.Kill();
        }

        private string _line = "";
        private void DL_Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e == null || e.Data == null)
                return;

            if (_line == e.Data)
                return;
            _line = e.Data;

            ProcessOutputLine(_line);
        }

        private void ProcessOutputLine(string line)
        { 
            //parse
            ParseDestination(line);
            ParseProgress(line);
            ParseStatus(line);
            ParsePlayList(line);

            OutputDataReceived(line);
        }

        private void DL_Process_Exited(object sender, EventArgs e)
        {
            int exitCode = -1;
            if (_DL_Process != null)
            {
                _DL_Process.ErrorDataReceived -= DL_Process_OutputDataReceived;
                _DL_Process.OutputDataReceived -= DL_Process_OutputDataReceived;
                _DL_Process.Exited -= DL_Process_Exited;
                try { exitCode = _DL_Process.ExitCode; }
                catch (Exception err) { Debug.WriteLine("DL_Process_Exited: " + err); }            
            }

            _line = "";
            Data.Progress = 0;
            if (Data.State == eDownloadState.Working)
            {
                Data.State = (exitCode == 1 || exitCode == 0) ? eDownloadState.Succsess : eDownloadState.InQueue;
            }

            ProcessExited();
        }

        private void ParseDestination(string line)
        {
            const string DST1 = "[download] Destination: ";
            int pos1 = line.IndexOf(DST1);
            if (pos1 >= 0)
            {
                Data.Description = line.Substring(pos1 + DST1.Length);
            }

            const string DST2 = "[ffmpeg] Merging formats into ";
            pos1 = line.IndexOf(DST2);
            if (pos1 >= 0)
            {
                Data.Description = line.Substring(pos1 + DST2.Length);
            }

            const string DST3 = "[ffmpeg] Destination: ";
            pos1 = line.IndexOf(DST3);
            if (pos1 >= 0)
            {
                Data.Description = line.Substring(pos1 + DST3.Length);
            }
        }

        private void ParsePlayList(string line)
        {
            const string DST1 = "[youtube:playlist] Downloading playlist ";
            int pos1 = line.IndexOf(DST1);
            if (pos1 >= 0)
            {
                Data.PlayListDescription = line.Substring(pos1 + DST1.Length);
            }

            const string DST2 = "[download] Downloading playlist: ";
            pos1 = line.IndexOf(DST2);
            if (pos1 >= 0)
            {
                Data.PlayListDescription = line.Substring(pos1 + DST2.Length);
            }

            const string DST3 = "[youtube:playlist] playlist ";
            pos1 = line.IndexOf(DST3);
            if (pos1 >= 0)
            {
                int pos2 = line.LastIndexOf(": Collected ");
                if(pos2>0)
                    Data.PlayListDescription = line.Substring(pos2+2);
                else
                    Data.PlayListDescription = line.Substring(pos1 + DST3.Length);
            }

            const string DST4 = "[download] Downloading video ";
            pos1 = line.IndexOf(DST4);
            if (pos1 >= 0)
            {
                Data.PlayListProgress = line.Substring(pos1 + 11);
            }
        }

        private void ParseStatus(string line)
        {
            const string SUFFIX = "has already been downloaded and merged";
            int pos = line.IndexOf(SUFFIX);
            if (pos > 0)
            {
                Data.State = eDownloadState.Skipped;
                Data.Description = line.Substring(11, line.Length - SUFFIX.Length - 11);
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

        public static List<string> Update(string pathToEngine)
        {
            return ProcessHelper.GetStdOut(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathToEngine), "-U", true);
        }

        public static string GetVersion(string pathToEngine)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathToEngine);
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
                List<string> ver = ProcessHelper.GetStdOut(path, " -version", true);

                return ver.Aggregate("\n", (current, s) => current + (" " + s + Environment.NewLine));
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
