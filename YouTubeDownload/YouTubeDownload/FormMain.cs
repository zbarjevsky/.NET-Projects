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
        const string DNL_PREFIX = "Download to: ";
        string _folderName = "C:\\Temp\\YouTube2";

        private bool _pause = false;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_DownloaderUserControl.OutputDataReceived = DL_Process_OutputDataReceived;
            m_DownloaderUserControl.ProcessExited = DL_Process_Exited;

            _folderName = Properties.Settings.Default.OutputFolder;
            m_lnkOutputFolder.Text = DNL_PREFIX + _folderName;
            m_lnkOutputFolder.LinkArea = new LinkArea(DNL_PREFIX.Length, _folderName.Length);

            m_chkNoPlayList.Checked = Properties.Settings.Default.NoPlayList;

            UpdateButtonsState();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_DownloaderUserControl.State == DownloadState.Working)
            {
               DialogResult res = MessageBox.Show(this, "Download in progress, Terminate?", "Closing...", 
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            m_DownloaderUserControl.Stop();
        }

        private void m_chkNoPlayList_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.NoPlayList = m_chkNoPlayList.Checked;
            Properties.Settings.Default.Save();
        }

        private void m_btnAddUrl_Click(object sender, EventArgs e)
        {
            int urlIdx = FindDataInList(m_txtUrl.Text);
            if (urlIdx >= 0)
            {
                DownloadData data1 = m_listUrls.Items[urlIdx].Tag as DownloadData;
                data1.NoPlayList = m_chkNoPlayList.Checked;
                data1.ExtractAudio = m_chkAudioOnly.Checked;

                MessageBox.Show(this, "Url already in list", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _pause = false;

            DownloadData data = new DownloadData()
            {
                State = DownloadState.InQueue,
                NoPlayList = m_chkNoPlayList.Checked,
                ExtractAudio = m_chkAudioOnly.Checked,
                OutputFolder = _folderName,
                Url = m_txtUrl.Text
            };

            ListViewItem item = new ListViewItem(data.State.ToString());
            item.SubItems.Add(data.FileName);
            item.SubItems.Add(data.Url);
            item.Tag = data;

            m_listUrls.Items.Add(item);
            m_txtUrl.Text = "";

            UpdateButtonsState();
            StartDownloadNext();
        }

        private bool StartDownloadNext()
        {
            if (m_DownloaderUserControl.State == DownloadState.Working)
                return false;
            if (_pause)
                return false;

            foreach (ListViewItem item in m_listUrls.Items)
            {
                DownloadData data = item.Tag as DownloadData;
                if(data.State == DownloadState.InQueue)
                {
                    this.Cursor = Cursors.AppStarting;
                    m_DownloaderUserControl.Start(data);
                    UpdateUrlList();
                    return true;
                }
            }

            UpdateButtonsState();
            return false;
        }

        private void UpdateUrlList()
        {
            foreach (ListViewItem item in m_listUrls.Items)
            {
                DownloadData data = item.Tag as DownloadData;
                item.SubItems[0].Text = data.State.ToString();
                item.SubItems[1].Text = Path.GetFileName(data.FileName.Trim('"'));
                item.SubItems[2].Text = data.Url;
            }
            UpdateButtonsState();
        }

        private void UpdateButtonsState()
        {
            bool bHasSelection = m_listUrls.SelectedItems.Count > 0;

            m_btnUpdate.Enabled = m_DownloaderUserControl.State != DownloadState.Working;

            m_btnAddUrl.Enabled = !string.IsNullOrWhiteSpace(m_txtUrl.Text.Trim());
            m_btnRemove.Enabled = bHasSelection;
            m_btnClearList.Enabled = m_listUrls.Items.Count > 0;

            int queueIdx = FindStateInList(DownloadState.InQueue);
            int workIdx = FindStateInList(DownloadState.Working);
            m_btnPause.Enabled = queueIdx >= 0 || workIdx >= 0; //there are items in queue
            if (queueIdx < 0 && workIdx < 0)
            {
                m_btnPause.Text = "...";
                m_btnPause.ImageIndex = -1;
            }
            else
            {
                m_btnPause.Text = _pause ? "Start" : "Pause";
                m_btnPause.ImageIndex = _pause ? 0 : 1;
            }
        }

        private int FindDataInList(string url)
        {
            for (int i = 0; i < m_listUrls.Items.Count; i++)
            {
                ListViewItem item = (ListViewItem)m_listUrls.Items[i];
                DownloadData data = item.Tag as DownloadData;
                if (data.Url == url)
                    return i;
            }
            return -1;
        }

        private int FindStateInList(DownloadState state)
        {
            for (int i = 0; i < m_listUrls.Items.Count; i++)
            {
                ListViewItem item = (ListViewItem)m_listUrls.Items[i];
                DownloadData data = item.Tag as DownloadData;
                if (data.State == state)
                    return i;
            }
            return -1;
        }

        private void DL_Process_Exited()
        {
            Cursor = Cursors.Arrow;
            m_StatusProgress.Value = 0;
            m_Status1.Text = "Done";

            //if(m_DownloaderUserControl.State == DownloadState.Failed)

            UpdateUrlList();
            if (!StartDownloadNext())
            {
                this.Cursor = Cursors.Arrow;
                MessageBox.Show(this, "Operation Finished!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DL_Process_OutputDataReceived(string line)
        {
            UpdateOutput(line);
        }

        private void m_lnkOutputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(_folderName);
        }

        private void UpdateOutput(string line)
        {
            this.Cursor = Cursors.AppStarting;

            m_Status2.Text = line;

            m_StatusProgress.Value = (int)m_DownloaderUserControl.Progress;
            m_Status1.Text = m_DownloaderUserControl.Description;

            UpdateUrlList();
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
            FormAbout about = new FormAbout();
            about.ShowDialog(this);
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
                m_lnkOutputFolder.Text = DNL_PREFIX + _folderName;
                m_lnkOutputFolder.LinkArea = new LinkArea(DNL_PREFIX.Length, _folderName.Length);
            }
        }

        private void m_btnClearList_Click(object sender, EventArgs e)
        {
            m_listUrls.Items.Clear();
        }

        private void m_btnPause_Click(object sender, EventArgs e)
        {
            _pause = !_pause;
            if (_pause)
            {
                m_DownloaderUserControl.Stop();
            }

            UpdateButtonsState();
            StartDownloadNext();
        }

        private void m_btnRemove_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedItems.Count == 0)
                return;

            m_listUrls.SelectedItems[0].Remove();
            UpdateUrlList();
        }

        private void m_listUrls_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void m_txtUrl_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }
    }
}
