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
using System.Windows.Shell;
using YouTubeDownload.Utils;


using MkZ.Tools;

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
            if (m_DownloaderUserControl.State == eDownloadState.Working)
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
            data.State = eDownloadState.InQueue;
            //data.Encoding = enc.GetEncoding();

            ListViewItem item = new ListViewItem(data.State.ToString());
            item.SubItems.Add(data.Description);
            item.SubItems.Add(data.Url);
            item.Tag = data;

            m_listUrls.Items.Add(item);
            //}

            UpdateButtonsState();
            StartDownloadNext(true);
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

        private bool StartDownloadNext(bool noWindow)
        {
            if (m_DownloaderUserControl.State == eDownloadState.Working)
                return false;
            if (_pause)
                return false;

            foreach (ListViewItem item in m_listUrls.Items)
            {
                DownloadData data = item.Tag as DownloadData;
                if (data.State == eDownloadState.InQueue)
                {
                    this.Cursor = Cursors.AppStarting;
                    m_DownloaderUserControl.Start(data, noWindow);
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

                if (data.State == eDownloadState.Working)
                    activeData = item;
            }

            ListViewShowProgress(activeData);

            UpdateButtonsState();
        }

        private void UpdateButtonsState()
        {
            bool bHasSelection = m_listUrls.SelectedItems.Count > 0;

            m_btnUpdate.Enabled = m_DownloaderUserControl.State != eDownloadState.Working;
            m_btnUpdateTB.Enabled = m_btnUpdate.Enabled;

            m_btnAddUrl.Enabled = true;
            m_btnRemove.Enabled = bHasSelection;
            m_btnClearList.Enabled = m_listUrls.Items.Count > 0;

            m_ctxmnuAddUrl.Enabled = m_btnAddUrl.Enabled;
            m_btnNewDownload.Enabled = m_btnAddUrl.Enabled;
            m_ctxmnuOpenOutputFolder.Enabled = bHasSelection;
            m_ctxmnuOpenSelectedFile.Enabled = bHasSelection;
            m_ctxmnuRemoveSelected.Enabled = bHasSelection;
            m_ctxmnuMoveUP.Enabled = bHasSelection;
            m_ctxmnuMoveDOWN.Enabled = bHasSelection;
            m_ctxmnuEdit.Enabled = bHasSelection;
            m_ctxmnuDownloadAgain.Enabled = bHasSelection;
            m_ctxmnuCMD.Enabled = bHasSelection;
            m_ctxmnuCopyFileName.Enabled = bHasSelection;
            m_ctxmnuCopyURL.Enabled = bHasSelection;

            int queueIdx = FindStateInList(eDownloadState.InQueue);
            int workIdx = FindStateInList(eDownloadState.Working);
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

        private int FindStateInList(eDownloadState state)
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
            if (!StartDownloadNext(true))
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

            string message = "Update DL finished.\nWas: " + ver1 + "\nNew: " + ver2;
            if (result != null && result.Count > 0)
            {
                message += "\n\nUpdater Output:";
                foreach (string line in result)
                    message += "\n " + line;
            }

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
            this.BrowseForFolder(ref _folderName, _folderName, "Select Folder to Download to:");
            UpdateOutputFolder(_folderName);
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
            StartDownloadNext(true);
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

            try
            {
                DownloadData data = m_listUrls.SelectedItems[0].Tag as DownloadData;
                if (data != null && data.State != eDownloadState.Failed)
                {
                    if (File.Exists(data.Description.Trim('"')))
                        Process.Start(data.Description.Trim('"'));
                    else
                        Process.Start(Path.GetDirectoryName(data.Description.Trim('"')));
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void m_mnuOpenOutputFolder_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedItems.Count == 0)
                return;

            DownloadData data = m_listUrls.SelectedItems[0].Tag as DownloadData;
            if (data != null && data.State != eDownloadState.Failed)
            {
                if(File.Exists(data.Description.Trim('"')))
                    FileUtils.ShowInFolder(data.Description.Trim('"'));
                else
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

        private void m_ctxmnuCMD_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedItems.Count == 0)
                return;

            DownloadData data = m_listUrls.SelectedItems[0].Tag as DownloadData;
            string parameters = YouTube_DL.PrepareCommanLine(data, out string exePath);

            string fileNameBat = "C:\\Temp\\RunIt.bat";
            File.WriteAllText(fileNameBat, "\"" + exePath + "\" " + parameters + "\npause\n");

            Process p = Process.Start(fileNameBat);
        }

        private void m_ctxmnuDownloadAgain_Click(object sender, EventArgs e)
        {
            DownloadAgain(true);
        }

        private void DownloadAgain(bool noWindow)
        {
            if (m_listUrls.SelectedItems.Count == 0)
                return;

            DownloadData data = m_listUrls.SelectedItems[0].Tag as DownloadData;
            if (data != null)
            {
                if (data.State == eDownloadState.Working)
                {
                    m_DownloaderUserControl.Stop();
                }
                data.State = eDownloadState.InQueue;
                UpdateButtonsState();
                StartDownloadNext(noWindow);
            }
        }

        private void m_ctxmnuCoypyFileName_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedItems.Count == 0)
                return;

            DownloadData data = m_listUrls.SelectedItems[0].Tag as DownloadData;
            if (data != null)
            {
                Clipboard.SetText(data.Description);
            }
        }

        private void m_ctxmnuCopyURL_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedItems.Count == 0)
                return;

            DownloadData data = m_listUrls.SelectedItems[0].Tag as DownloadData;
            if (data != null)
            {
                Clipboard.SetText(data.Url);
            }
        }

        private void m_ctxmnuCopyCommandLine_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedItems.Count == 0)
                return;

            DownloadData data = m_listUrls.SelectedItems[0].Tag as DownloadData;
            if (data != null)
            {
                string cmd = YouTube_DL.PrepareCommanLine(data, out string exePath);
                Clipboard.SetText(exePath + " " + cmd);
            }
        }

        private void m_ctxmnuEdit_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedItems.Count == 0)
                return;

            ListViewItem selected = m_listUrls.SelectedItems[0];
            DownloadData data = selected.Tag as DownloadData;
            if (data != null)
            {
                FormAddUrl frm = new FormAddUrl(data);
                if (frm.ShowDialog(this) != DialogResult.OK)
                    return;

                data = frm.Data.Clone();
                data.State = eDownloadState.InQueue;

                selected.Text = data.State.ToString();
                selected.SubItems[1].Text = (data.Description);
                selected.SubItems[2].Text = (data.Url);
                selected.Tag = data;

                UpdateButtonsState();
                StartDownloadNext(true);
            }
        }

        private void m_ctxmnuMoveUP_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedIndices.Count == 0 || m_listUrls.SelectedIndices[0] < 1)
                return;

            ListViewItem selected = m_listUrls.SelectedItems[0];
            int idx = selected.Index;

            m_listUrls.Items.RemoveAt(selected.Index);
            m_listUrls.Items.Insert(idx - 1, selected);
        }

        private void m_ctxmnuMoveDOWN_Click(object sender, EventArgs e)
        {
            if (m_listUrls.SelectedIndices.Count == 0 || m_listUrls.SelectedIndices[0] >= m_listUrls.Items.Count - 1)
                return;

            ListViewItem selected = m_listUrls.SelectedItems[0];
            int idx = selected.Index;

            m_listUrls.Items.RemoveAt(selected.Index);
            m_listUrls.Items.Insert(idx + 1, selected);
        }

        private void m_mnuSaveList_Click(object sender, EventArgs e)
        {
            DownloadList.Save(m_listUrls);
        }

        private void m_mnuLoadList_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "Saved Download Lists *.xml |*.xml| All Files (*.*)|*.*";
            dlg.FileName = @"*.xml";
            dlg.InitialDirectory = DownloadList._outPath;
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                DownloadList list = new DownloadList();
                list.Load(dlg.FileName);

                foreach (DownloadData data in list.Downloads)
                {
                    //data.State = eDownloadState.InQueue;
                    //data.Encoding = enc.GetEncoding();

                    ListViewItem item = new ListViewItem(data.State.ToString());
                    item.SubItems.Add(data.Description);
                    item.SubItems.Add(data.Url);
                    item.Tag = data;

                    m_listUrls.Items.Add(item);

                }

                UpdateButtonsState();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "YTD", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void m_listUrls_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is ListViewItem item)
            {
                if (item.Tag is DownloadData data)
                {
                    string fileName = data.Description.Trim('"').Trim('\\');
                    if (File.Exists(fileName))
                    {
                        string[] files = new string[] { fileName };
                        this.DoDragDrop(new DataObject(DataFormats.FileDrop, files), DragDropEffects.Copy);
                    }
                }
            }
        }

        private void m_btnRenameFIles_Click(object sender, EventArgs e)
        {
            FormRenameFiles frm = new FormRenameFiles(_folderName);
            frm.ShowDialog(this);
        }
    }
}