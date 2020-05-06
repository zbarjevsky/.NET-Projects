using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartBackup.Settings;
using System.Media;

namespace SmartBackup
{
    public partial class BackupListUserControl : UserControl
    {
        private BackupGroup _settings = null;

        public BackupListUserControl(BackupGroup group = null)
        {
            _settings = group;
            InitializeComponent();
        }

        private void BackupListUserControl_Load(object sender, EventArgs e)
        {
            m_listBackup.ExpandAllGroups();
            if(!this.DesignMode)
                ReloadList(null);
        }

        private void m_btnAdd_Click(object sender, EventArgs e)
        {
            BackupEntry entry = new BackupEntry();
            FormBackupFolderProperties frm = new FormBackupFolderProperties(entry);
            if (frm.ShowDialog(this) != DialogResult.OK)
                return;

            _settings.Add(entry);
            ReloadList(entry);
        }

        private void m_btnRemove_Click(object sender, EventArgs e)
        {
            if (m_listBackup.SelectedItems.Count > 0)
            {
                List<ListViewItem> sel = new List<ListViewItem>(m_listBackup.SelectedItems.Cast<ListViewItem>());
                foreach (ListViewItem item in sel)
                {
                    BackupEntry entry = item.Tag as BackupEntry;
                    m_listBackup.Items.Remove(item);
                    _settings.Remove(entry);
                }
            }
        }

        private void m_listBackup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                m_btnEdit_Click(sender, e);
        }

        private void m_btnEdit_Click(object sender, EventArgs e)
        {
            if (m_listBackup.SelectedItems.Count > 0)
            {
                BackupEntry entry = m_listBackup.SelectedItems[0].Tag as BackupEntry;
                FormBackupFolderProperties frm = new FormBackupFolderProperties(entry);
                if (frm.ShowDialog(this) != DialogResult.OK)
                    return;

                ReloadList(entry);
            }
        }

        private void m_listBackup_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void ReloadList(BackupEntry selectEntry)
        {
            int selectedIdx = -1;
            m_listBackup.Items.Clear();
            foreach (BackupEntry entry in _settings.BackupList)
            {
                ListViewItem item = m_listBackup.Items.Add(entry.IsImportant.ToString());
                item.Tag = entry;
                item.SubItems.Add(entry.FolderSrc);
                item.SubItems.Add(entry.FolderDst);
                item.Group = entry.IsImportant ? m_listBackup.Groups[0] : m_listBackup.Groups[1];
                if (selectEntry != null && selectEntry.FolderSrc == entry.FolderSrc)
                {
                    item.Selected = true;
                    selectedIdx = item.Index;
                }
            }

            if (selectedIdx >= 0)
                m_listBackup.EnsureVisible(selectedIdx);

            UpdateUI();
        }

        private void UpdateUI()
        {
            if (m_listBackup.SelectedItems.Count > 0)
            {
                m_btnEdit.Enabled = true;
                m_btnRemove.Enabled = true;
            }
            else
            {
                m_btnEdit.Enabled = false;
                m_btnRemove.Enabled = false;
            }
        }

        private void m_btnBackupAll_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            m_btnBackupAll.Enabled = false;
            Application.DoEvents();

            FormBackupProgress frm = new FormBackupProgress(_settings);
            frm.ShowDialog(this);

            this.Cursor = Cursors.Arrow;
            m_btnBackupAll.Enabled = true;
        }
    }
}
