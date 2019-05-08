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

namespace YouTubeDownload
{
    public partial class DownloaderUserControl : UserControl
    {
        YouTube_DL _youTube_DL = new YouTube_DL();

        public string Description { get { return _youTube_DL.Description; } }
        public double Progress { get { return _youTube_DL.Progress; } }

        public bool IsReady { get; private set; } = false;

        public Action<string> OutputDataReceived = (OutputData) => { };
        public Action ProcessExited = () => { };

        public DownloaderUserControl()
        {
            InitializeComponent();
        }

        private void DownloaderUserControl_Load(object sender, EventArgs e)
        {
            m_lnkDestination.LinkArea = new LinkArea();
            m_lnkOutputFolder.LinkArea = new LinkArea();
        }

        private void m_lnkDestination_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(!IsReady)
            {
                MessageBox.Show(this, "Not Ready", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Process.Start(_youTube_DL.OutputFolder);
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Start(bool noPlayList, string outputFolder, string url)
        {
            IsReady = false;

            _youTube_DL.OutputDataReceived = DL_Process_OutputDataReceived;
            _youTube_DL.ProcessExited = DL_Process_Exited;

            _youTube_DL.Start(noPlayList, outputFolder, url);

            const string PREFIX = "Downloading to: ";
            m_lnkOutputFolder.Text = PREFIX + outputFolder;
            m_lnkOutputFolder.LinkArea = new LinkArea(PREFIX.Length, outputFolder.Length);
        }

        private void DL_Process_Exited()
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                IsReady = true;
                m_ProgressBar.Value = (int)_youTube_DL.Progress;

                ProcessExited();
            }));
        }

        private void DL_Process_OutputDataReceived(string line)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                UpdateOutput(line);
                OutputDataReceived(line);
            }));
        }

        private void UpdateOutput(string line)
        {
            this.Cursor = Cursors.AppStarting;

            m_txtOutput.Text = (line + "\n") + m_txtOutput.Text;

            m_lblStatus.Text = "Status: " + line;

            m_ProgressBar.Value = (int)_youTube_DL.Progress;

            const string PREFIX = "Description: ";
            if (!string.IsNullOrWhiteSpace(_youTube_DL.Description))
            {
                m_lnkDestination.Text = PREFIX + _youTube_DL.Description;
                m_lnkDestination.LinkArea = new LinkArea(PREFIX.Length, _youTube_DL.Description.Length);
            }
        }
    }
}
