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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouTubeDownload.Utils;

namespace YouTubeDownload
{
    public partial class FormMain : Form
    {
        private const string DNL_PREFIX = "Output Folder: ";
        private string _folderName = "C:\\Temp\\YouTube2";

        private bool _pause = false;
        private ProgressBar _progressBarInPlace;

        public FormMain()
        {
            InitializeComponent();

            m_listUrls.SetDoubleBuffered(true);

            _progressBarInPlace = new ProgressBar();
            _progressBarInPlace.Parent = m_listUrls;
            _progressBarInPlace.Name = "progr1";
            _progressBarInPlace.Visible = false;
            _progressBarInPlace.Maximum = 100;
            _progressBarInPlace.Step = 1;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_DownloaderUserControl.OutputDataReceived = DL_Process_OutputDataReceived;
            m_DownloaderUserControl.ProcessExited = DL_Process_Exited;

            UpdateOutputFolder(Properties.Settings.Default.OutputFolder);

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

        private void m_mnuFileAdd_Click(object sender, EventArgs e)
        {
            m_btnAddUrl_Click(sender, e);
        }

        private void m_btnAddUrl_Click(object sender, EventArgs e)
        {
            FormAddUrl frm = new FormAddUrl(_folderName);
            if (frm.ShowDialog(this) != DialogResult.OK)
                return;

            int urlIdx = FindDataInList(frm.Data.Url);
            if (urlIdx >= 0)
            {
                //m_listUrls.Items[urlIdx].Tag = frm.Data;

                MessageBox.Show(this, "Url already in list", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _pause = false;

            //foreach (EncodingInfo enc in Encoding.GetEncodings())
            //{
            DownloadData data = frm.Data.Clone();
            data.State = DownloadState.InQueue;
            //data.Encoding = enc.GetEncoding();

            ListViewItem item = new ListViewItem(data.State.ToString());
            item.SubItems.Add(data.Description);
            item.SubItems.Add(data.Url);
            item.Tag = data;

            m_listUrls.Items.Add(item);
            //}

            UpdateButtonsState();
            StartDownloadNext();
        }

        public void ListViewShowProgress(ListViewItem item = null)
        {
            if(item == null)
            {
                _progressBarInPlace.Visible = false;
                return;
            }

            DownloadData data = item.Tag as DownloadData;

            Rectangle r = item.Bounds;
            //position progress bar at the bottom of the cell
            r.X += 4;
            r.Width = m_listUrls.Columns[0].Width - 8;
            r.Y += r.Height - 4;
            r.Height = 4;

            _progressBarInPlace.Bounds = r;
            _progressBarInPlace.Value = (int)data.Progress;
            _progressBarInPlace.Style = _progressBarInPlace.Value == 0 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
            _progressBarInPlace.Visible = true;
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
                if (data.State == DownloadState.InQueue)
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
            ListViewItem activeData = null;
            foreach (ListViewItem item in m_listUrls.Items)
            {
                DownloadData data = item.Tag as DownloadData;
                item.SubItems[0].Text = data.State.ToString();
                item.SubItems[1].Text = Path.GetFileName(data.Description.Trim('"'));
                item.SubItems[2].Text = data.Url;

                if (data.State == DownloadState.Working)
                    activeData = item;
            }

            ListViewShowProgress(activeData);

            UpdateButtonsState();
        }

        private void UpdateButtonsState()
        {
            bool bHasSelection = m_listUrls.SelectedItems.Count > 0;

            m_btnUpdate.Enabled = m_DownloaderUserControl.State != DownloadState.Working;

            m_btnAddUrl.Enabled = true;
            m_btnRemove.Enabled = bHasSelection;
            m_btnClearList.Enabled = m_listUrls.Items.Count > 0;

            m_ctxmnuAddUrl.Enabled = m_btnAddUrl.Enabled;
            m_ctxmnuOpenOutputFolder.Enabled = bHasSelection;
            m_ctxmnuOpenSelectedFile.Enabled = bHasSelection;
            m_ctxmnuRemoveSelected.Enabled = bHasSelection;
            m_ctxmnuDownloadAgain.Enabled = bHasSelection;

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
                m_btnPause.Text = _pause ? "Start" : "Abort";
                m_btnPause.ImageIndex = _pause ? 0 : 1;
            }
        }

        private int FindDataInList(string url)
        {
            for (int i = 0; i < m_listUrls.Items.Count; i++)
            {
                ListViewItem item = (ListViewItem) m_listUrls.Items[i];
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
                ListViewItem item = (ListViewItem) m_listUrls.Items[i];
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
                //MessageBox.Show(this, "Operation Finished!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            m_StatusProgress.Value = (int) m_DownloaderUserControl.Progress;
            m_Status1.Text = m_DownloaderUserControl.Description;

            UpdateUrlList();
        }

        private void m_btnUpdate_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string ver1 = YouTube_DL.GetVersion();
            List<string> result = YouTube_DL.Update();
            string ver2 = YouTube_DL.GetVersion();

            string message = "Update DL finished.\nWas: " + ver1 + "\nNew: " + ver2 + "\n\nUpdater Output:";
            foreach (string line in result)
                message += "\n " + line;

            Cursor = Cursors.Arrow;
            MessageBox.Show(message, "Update DL", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void m_mnuToolsUpdateDL_Click(object sender, EventArgs e)
        {
            m_btnUpdate_Click(sender, e);
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

            //select current folder
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                this.Invoke(new MethodInvoker(() => SendKeys.Send("{TAB}{TAB}{DOWN}{DOWN}{UP}{UP}")));
            });

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                UpdateOutputFolder(dlg.SelectedPath);
            }
        }

        private void UpdateOutputFolder(string newFolder)
        {
            _folderName = newFolder;
            Properties.Settings.Default.OutputFolder = _folderName;
            Properties.Settings.Default.Save();

            m_lnkOutputFolder.Text = DNL_PREFIX + _folderName;
            m_lnkOutputFolder.LinkArea = new LinkArea(DNL_PREFIX.Length, _folderName.Length);
        }

        private void m_btnBrowseForFolder_Click(object sender, EventArgs e)
        {
            m_mnuToolsOutputFolder_Click(sender, e);
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

        //private void m_txtUrl_TextChanged(object sender, EventArgs e)
        //{
        //    UpdateButtonsState();
        //}

        private void m_mnuOpenSelectedFile_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedItems.Count == 0)
                return;

            DownloadData data = m_listUrls.SelectedItems[0].Tag as DownloadData;
            if (data != null && data.State != DownloadState.Failed)
            {
                Process.Start(data.Description);
            }
        }

        private void m_mnuOpenOutputFolder_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedItems.Count == 0)
                return;

            DownloadData data = m_listUrls.SelectedItems[0].Tag as DownloadData;
            if (data != null && data.State != DownloadState.Failed)
            {
                Process.Start(data.OutputFolder);
            }
        }

        private void m_listUrls_DoubleClick(object sender, EventArgs e)
        {
        }

        private void m_listUrls_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (m_listUrls.SelectedItems.Count == 0)
            {
                m_mnuFileAdd_Click(sender, e);
            }
            else
            {
                m_mnuOpenSelectedFile_Click(sender, e);
            }
        }

        private void m_ctxmnuDownloadAgain_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedItems.Count == 0)
                return;

            DownloadData data = m_listUrls.SelectedItems[0].Tag as DownloadData;
            if(data != null)
            {
                if(data.State == DownloadState.Working )
                {
                    m_DownloaderUserControl.Stop();
                }
                data.State = DownloadState.InQueue;
                UpdateButtonsState();
                StartDownloadNext();
            }
        }
    }
}