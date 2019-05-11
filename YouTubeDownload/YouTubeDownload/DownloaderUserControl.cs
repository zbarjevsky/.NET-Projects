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

namespace YouTubeDownload
{
    public partial class DownloaderUserControl : UserControl
    {
        YouTube_DL _youTube_DL = new YouTube_DL();

        public string Description { get { return _youTube_DL.Data.Description; } }
        public double Progress { get { return _youTube_DL.Data.Progress; } }

        public DownloadState State { get { return _youTube_DL.Data.State; } }

        public Action<string> OutputDataReceived = (OutputData) => { };
        public Action ProcessExited = () => { };

        public DownloaderUserControl()
        {
            InitializeComponent();
        }

        private void DownloaderUserControl_Load(object sender, EventArgs e)
        {
            _youTube_DL.OutputDataReceived = DL_Process_OutputDataReceived;
            _youTube_DL.ProcessExited = DL_Process_Exited;

            m_lnkDestination.LinkArea = new LinkArea();
        }

        private void m_lnkDestination_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(!File.Exists(_youTube_DL.Data.Description))
            {
                if(!string.IsNullOrWhiteSpace(_youTube_DL.Data.Description))
                    MessageBox.Show(this, "Not Ready: \n"+ _youTube_DL.Data.Description, Text, 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string link = m_lnkDestination.Text.Substring(m_lnkDestination.LinkArea.Start);
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
                Process.Start(_youTube_DL.Data.OutputFolder);
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Start(DownloadData data)
        {
            _youTube_DL.Start(data);
        }

        public void Stop()
        {
            _youTube_DL.Stop();
        }

        private void DL_Process_Exited()
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                this.Cursor = Cursors.Arrow;

                m_ProgressBar.Value = (int)_youTube_DL.Data.Progress;
                ProcessExited();
            }));
        }

        private void DL_Process_OutputDataReceived(string line)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                this.Cursor = Cursors.AppStarting;

                UpdateOutput(line);
                OutputDataReceived(line);
            }));
        }

        private void UpdateOutput(string line)
        {
            m_txtOutput.Text = (line + "\n") + m_txtOutput.Text;

            m_lblStatus.Text = "Status: " + line;

            m_ProgressBar.Value = (int)_youTube_DL.Data.Progress;

            const string PREFIX = "Description: ";
            if (!string.IsNullOrWhiteSpace(_youTube_DL.Data.Description))
            {
                m_lnkDestination.Text = PREFIX + _youTube_DL.Data.Description;
                m_lnkDestination.LinkArea = new LinkArea(PREFIX.Length, _youTube_DL.Data.Description.Length);
            }
        }
    }
}
