using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MeditationStopWatch
{
    public partial class FileListControl : UserControl
    {
        private int _playingIdx = -1;

        public PlayListActions OP = new PlayListActions();

        public List<string> GetFilelist()
        {
            List<string> list = new List<string>(m_listFiles.Items.Count);
            for (int i = 0; i < m_listFiles.Items.Count; i++)
            {
                FileInfo f = m_listFiles.Items[i].Tag as FileInfo;
                list.Add(f.FullName);
            }
            return list;
        }

        public FileListControl()
        {
            InitializeComponent();
        }

        private void FileListControl_Load(object sender, EventArgs e)
        {
            m_mnuUp.Image = m_toolStripButton_Up.Image;
            m_mnuDown.Image = m_toolStripButton_Down.Image;

            m_mnuAdd.Image = m_toolStripButton_AddFiles.Image;
            m_mnuRemove.Image = m_toolStripButton_Remove.Image;
            m_mnuRemoveAll.Image = m_toolStripButton_RemoveAll.Image;
        }

        private void m_toolStripButton_AddFiles_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != m_openFileDialog.ShowDialog(this))
                return;

            AddToFileList(m_openFileDialog.FileNames, true);
            OnListChanged(_playingIdx);
        }

        private void m_toolStripButton_Remove_Click(object sender, EventArgs e)
        {
            RemoveSelectedFiles();
        }

        private void m_toolStripButton_RemoveAll_Click(object sender, EventArgs e)
        {
            RemoveAll();
        }

        private void m_toolStripButton_Up_Click(object sender, EventArgs e)
        {
            if (!EnableUp())
                return;

            int idx = m_listFiles.SelectedIndices[0];
            ListViewItem itm = m_listFiles.SelectedItems[0];
            m_listFiles.Items.RemoveAt(idx);
            m_listFiles.Items.Insert(idx - 1, itm);
            if (_playingIdx == idx)
                _playingIdx--;
        }

        private void m_toolStripButton_Down_Click(object sender, EventArgs e)
        {
            if (!EnableDown())
                return;

            int idx = m_listFiles.SelectedIndices[0];
            ListViewItem itm = m_listFiles.SelectedItems[0];
            m_listFiles.Items.RemoveAt(idx);
            m_listFiles.Items.Insert(idx + 1, itm);
            if (_playingIdx == idx)
                _playingIdx++;
        }

        public void ReloadList(string[] files, bool bPlayFirst)
        {
            OP.StopAction();
            m_listFiles.Items.Clear();
            AddToFileList(files, bPlayFirst);
            OnListChanged();
        }

        public void AddToFileList(string[] files, bool bPlayFirst)
        {
            if (files == null || files.Length == 0)
                return;

            int iFirstIdx = -1;
            foreach (string s in files)
            {
                FileInfo f = new FileInfo(s);
                if (!f.Exists)
                    continue;

                if (f.Extension.ToLower() != ".mp3")
                    continue;

                int idx = IndexOf(s);
                if (idx >= 0)
                {
                    if (iFirstIdx < 0)
                        iFirstIdx = idx;
                    continue;
                }

                ListViewItem i = m_listFiles.Items.Add(f.Name);
                i.ToolTipText = f.FullName;
                i.SubItems.Add(" --:-- ");
                i.SubItems.Add(string.Format("{0:N} MB", f.Length / (1024.0 * 1024.0)));
                i.Tag = f;
                if (iFirstIdx < 0)
                    iFirstIdx = i.Index;
            }
            if (bPlayFirst)
                OP.PlayAction(iFirstIdx);
        }

        public void UpdateFileTime(int idx, string time)
        {
            m_listFiles.Items[idx].SubItems[1].Text = time;
        }

        private int IndexOf(string sFileName)
        {
            for (int i = 0; i < m_listFiles.Items.Count; i++)
            {
                FileInfo f = m_listFiles.Items[i].Tag as FileInfo;
                if (f.FullName.ToLower() == sFileName.ToLower())
                    return i;
            }
            return -1;
        }

        private FileInfo FileFromIndex(int idx)
        {
            FileInfo f = m_listFiles.Items[idx].Tag as FileInfo;
            return f;
        }
        public int Count { get { return m_listFiles.Items.Count; } }

        public bool HasSelected { get { return m_listFiles.SelectedIndices.Count > 0; } }

        //return first if not selected
        public int GetSelectedFile()
        {
            if (m_listFiles.Items.Count == 0)
                return -1;

            if (m_listFiles.SelectedIndices.Count == 0)
                return 0;

            return m_listFiles.SelectedIndices[0];
        }

        public void RemoveSelectedFiles()
        {
            for (int i = m_listFiles.SelectedIndices.Count - 1; i >= 0; i--)
            {
                int idx = m_listFiles.SelectedIndices[i];
                if (idx == _playingIdx)
                {
                    OP.StopAction();
                    _playingIdx = -1;
                }
                m_listFiles.SelectedItems[i].Remove();
            }
            OnListChanged();
        }

        public void RemoveAll()
        {
            OP.StopAction();
            m_listFiles.Items.Clear();
            OnListChanged();
        }

        private bool EnableUp()
        {
            if (m_listFiles.SelectedIndices.Count != 1)
                return false;
            if (m_listFiles.SelectedIndices[0] < 1)
                return false;
            return true;
        }

        private bool EnableDown()
        {
            if (m_listFiles.SelectedIndices.Count != 1)
                return false;
            if (m_listFiles.SelectedIndices[0] > m_listFiles.Items.Count - 2)
                return false;
            return true;
        }

        private void m_listFiles_DoubleClick(object sender, EventArgs e)
        {
            if (m_listFiles.SelectedIndices.Count == 0)
                return;

            OP.PlayAction(m_listFiles.SelectedIndices[0]);
        }

        private void m_listFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListButtonsState();
            UpdatePlayingFile();
        }

        public void UpdatePlayingFile()
        {
            foreach (ListViewItem item in m_listFiles.Items)
            {
                Color crText = item.Index == _playingIdx ? Color.Red : SystemColors.WindowText;
                item.ForeColor = crText;
            }
        }

        internal FileInfo SelectPlayFile(int idx)
        {
            _playingIdx = idx;
            UpdatePlayingFile();
            return m_listFiles.Items[idx].Tag as FileInfo;
        }

        public int PrevIdx()
        {
            int idx = _playingIdx - 1;
            if (idx < 0) idx = m_listFiles.Items.Count - 1;
            return idx;
        }

        public int NextIdx()
        {
            int idx = _playingIdx + 1;
            if (idx >= m_listFiles.Items.Count) idx = 0;
            return idx;
        }

        public FileInfo PlayingFile
        {
            get
            {
                if (_playingIdx < 0) return null;
                return (m_listFiles.Items[_playingIdx].Tag as FileInfo);
            }
        }

        public void UpdatePlayerMenusState(bool bUpdateCheckedState, bool bPlay, bool bPause, bool bStop, bool bPrev, bool bNext)
        {
            if (bUpdateCheckedState)
                return;

            bool bHasFiles = Count > 0;

            m_mnuPlay.Enabled = bPlay && bHasFiles;
            m_mnuPause.Enabled = bPause && bHasFiles;
            m_mnuStop.Enabled = bStop && bHasFiles;
            m_mnuPrev.Enabled = bPrev && Count > 1;
            m_mnuNext.Enabled = bNext && Count > 1;
        }

        public void UpdateListButtonsState()
        {
            bool bHasFiles = Count > 0;

            m_toolStripButton_Up.Enabled = EnableUp();
            m_toolStripButton_Down.Enabled = EnableDown();

            m_toolStripButton_Remove.Enabled = HasSelected;
            m_toolStripButton_RemoveAll.Enabled = bHasFiles;

            m_mnuUp.Enabled = m_toolStripButton_Up.Enabled;
            m_mnuDown.Enabled = m_toolStripButton_Down.Enabled;

            m_mnuAdd.Enabled = m_toolStripButton_AddFiles.Enabled;
            m_mnuRemove.Enabled = m_toolStripButton_Remove.Enabled;
            m_mnuRemoveAll.Enabled = m_toolStripButton_RemoveAll.Enabled;
        }

        private void OnListChanged(int newIndex = -1)
        {
            _playingIdx = newIndex;
            UpdateListButtonsState();
            OP.OnListChanged();
        }

        private void m_mnuPlay_Click(object sender, EventArgs e)
        {
            if (m_listFiles.SelectedIndices.Count == 0)
                return;

            OP.PlayAction(m_listFiles.SelectedIndices[0]);
        }

        private void m_mnuPause_Click(object sender, EventArgs e)
        {
            OP.PauseAction();
        }

        private void m_mnuStop_Click(object sender, EventArgs e)
        {
            OP.StopAction();
        }

        private void m_mnuPrev_Click(object sender, EventArgs e)
        {
            OP.PrevAction();
        }

        private void m_mnuNext_Click(object sender, EventArgs e)
        {
            OP.NextAction();
        }
    }

    public class PlayListActions
    {
        public Action<int> PlayAction = (selectedIndex) => { };
        public Action StopAction = () => { };
        public Action PauseAction = () => { };
        public Action NextAction = () => { };
        public Action PrevAction = () => { };
        public Action OnListChanged = () => { };
    }
}
