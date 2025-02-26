using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;


using MkZ.Tools;
using System.Threading;

namespace YouTubeDownload
{
    public partial class DownloaderUserControl : UserControl
    {
        YouTubeDownloadEngine _engine = new YouTubeDownloadEngine();
        Stopwatch _stopwatch = new Stopwatch();

        public string Description { get { return _engine.Data.Description; } }
        public double Progress { get { return _engine.Data.Progress; } }

        public eDownloadState State { get { return _engine.Data.State; } }

        public Action<string> OutputDataReceived = (OutputData) => { };
        public Action ProcessExited = () => { };

        public DownloaderUserControl()
        {
            InitializeComponent();
        }

        private void DownloaderUserControl_Load(object sender, EventArgs e)
        {
            _engine.OutputDataReceived = DL_Process_OutputDataReceived;
            _engine.ProcessExited = DL_Process_Exited;

            m_lnkDestination.LinkArea = new LinkArea();
            m_lnkDestination.Text = "...";

            m_lblPlayListStatus.Text = "";
            m_lblStatus.Text = "";
        }

        private void m_lnkDestination_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string link = m_lnkDestination.Text.Substring(m_lnkDestination.LinkArea.Start).Trim('"');
            if (string.IsNullOrWhiteSpace(link))
                return;
            
            if(!File.Exists(link))
            {
                if (MessageBox.Show(this, "File Not Found: " + link + "\nOpen Output Directory Instead?", 
                    "Open File",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;

                string dir = Path.GetDirectoryName(link);
                if(!Directory.Exists(dir))
                    return;

                link = dir;
            }

            try
            {
                Process.Start(link);
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_lnkOutputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(_engine.Data.OutputFolder);
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Start(DownloadData data, bool noWindow)
        {
            _timerUpdateProgress.Start();
           _engine.Start(data, noWindow);
        }

        public void Stop()
        {
            _engine.Stop();
        }

        private void DL_Process_Exited()
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                this.Cursor = Cursors.Arrow;
                _timerUpdateProgress.Stop();
                UpdateOutput("======================= Done: "+ _engine.Data.State + " ========================");
                m_ProgressBar.Style = ProgressBarStyle.Continuous;
                m_ProgressBar.Value = (int)_engine.Data.Progress;
                m_ProgressBar.ShowInTaskbar = false;
                m_lblTime.Text = "...";
                ProcessExited();
            }));
        }

        private void DL_Process_OutputDataReceived(string line)
        {
            //lock (this)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    this.Cursor = Cursors.AppStarting;

                    UpdateOutput(line);
                    OutputDataReceived(line);
                }));
            }
            Task.Delay(1).Wait();
        }

        private bool _isInUpdate = false;
        private void UpdateOutput(string line)
        {
            if (_isInUpdate)
                return;
            _isInUpdate = true;

            _stopwatch.Restart();
            m_lblTime.Text = "Downloading... ";

            if (m_txtOutput.Text.Length > 12000)
                m_txtOutput.Text = m_txtOutput.Text.Substring(0, 10000);

            m_txtOutput.Text = (line + "\n") + m_txtOutput.Text;

            m_lblStatus.Text = "Status: " + line;

            m_ProgressBar.Value = (int)_engine.Data.Progress;
            m_ProgressBar.State = Windows7ProgressBar.ProgressBarState.Normal;
            if(m_ProgressBar.Value == 0)
            {
                m_ProgressBar.ShowInTaskbar = true;
                m_ProgressBar.Style = ProgressBarStyle.Marquee;
            }
            else if(m_ProgressBar.Value == 100)
            {
                m_ProgressBar.ShowInTaskbar = false;
            }
            else
            {
                m_ProgressBar.ShowInTaskbar = true;
                m_ProgressBar.Style = ProgressBarStyle.Continuous;
            }

            const string PREFIX = "Description: ";
            if (!string.IsNullOrWhiteSpace(_engine.Data.Description))
            {
                m_lnkDestination.Text = PREFIX + _engine.Data.Description;
                m_lnkDestination.LinkArea = new LinkArea(PREFIX.Length, _engine.Data.Description.Length);
            }

            m_lblPlayListStatus.Text = _engine.Data.PlayListProgress;

            _isInUpdate = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_stopwatch.Elapsed.TotalMilliseconds > 1800)
            {
                m_lblTime.Text = "Waiting... " + _stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
                m_ProgressBar.State = Windows7ProgressBar.ProgressBarState.Pause;
            }
        }
    }
}
