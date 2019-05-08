using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeDownload
{
    public partial class FormMain : Form
    {
        string _folderName = "C:\\Temp\\YouTube2";

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            _folderName = Properties.Settings.Default.OutputFolder;
            m_lblOutputFolder.Text = "Download To: " + _folderName;
        }

        private void m_btnDownload_Click(object sender, EventArgs e)
        {
            m_DownloaderUserControl.OutputDataReceived = DL_Process_OutputDataReceived;
            m_DownloaderUserControl.ProcessExited = DL_Process_Exited;

            m_DownloaderUserControl.Start(m_chkNoPlayList.Checked, _folderName, m_txtUrl.Text);

            m_btnDownload.Enabled = false;
            m_chkNoPlayList.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
        }

        private void DL_Process_Exited()
        {
            m_btnDownload.Enabled = true;
            m_chkNoPlayList.Enabled = true;
            Cursor = Cursors.Arrow;
            m_StatusProgress.Value = 0;
            m_Status1.Text = "Done";

            MessageBox.Show(this, "Operation Finished!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DL_Process_OutputDataReceived(string line)
        {
            UpdateOutput(line);
        }

        private void m_btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(_folderName);
        }

        private void UpdateOutput(string line)
        {
            this.Cursor = Cursors.AppStarting;

            m_Status2.Text = line;

            m_StatusProgress.Value = (int)m_DownloaderUserControl.Progress;
            m_Status1.Text = m_DownloaderUserControl.Description;
        }

        private void m_btnUpdate_Click(object sender, EventArgs e)
        {
            YouTube_DL.Update();
        }

        private void m_mnuToolsSettings_Click(object sender, EventArgs e)
        {

        }

        private void m_mnuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void m_mnuHelpAbout_Click(object sender, EventArgs e)
        {
            string ver1 = Assembly.GetEntryAssembly().GetName().Version.ToString();
            MessageBox.Show("YouTube Download\nMain ver: "+ver1+"\n" + YouTube_DL.GetVersion().ToString());
        }

        private void m_mnuToolsOutputFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog()
            {
                RootFolder = Environment.SpecialFolder.MyComputer,
                SelectedPath = _folderName,
                Description = "Select Folder to Download to:"
            };

            if(dlg.ShowDialog(this) == DialogResult.OK)
            {
                _folderName = dlg.SelectedPath;
                Properties.Settings.Default.OutputFolder = _folderName;
                Properties.Settings.Default.Save();
                m_lblOutputFolder.Text = "Download To: " + _folderName;
            }
        }
    }
}
