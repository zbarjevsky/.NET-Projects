using SmartBackup.Settings;
using SmartBackup.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartBackup
{
    public partial class FormBackupFolderProperties : Form
    {
        readonly BackupEntry _entry;

        public FormBackupFolderProperties(BackupEntry entry)
        {
            _entry = entry;

            InitializeComponent();

            m_cmbPriority.Items.Clear();
            m_cmbPriority.Items.AddRange(Enum.GetValues(typeof(BackupPriority)).Cast<Object>().ToArray());
            m_cmbPriority.SelectedIndex = 1;
        }

        private void FormBackupFolderProperties_Load(object sender, EventArgs e)
        {
            m_txtDstFolder.Text = _entry.FolderDst;
            m_txtSource.Text = _entry.FolderSrc;
            m_txtFileType.Text = _entry.FolderIncludeTypes;
            m_txtExcludeType.Text = _entry.FolderExcludeTypes;
            m_cmbPriority.SelectedItem = _entry.Priority;

            ValidateInput();
        }

        private void m_btnBrowseSrc_Click(object sender, EventArgs e)
        {
            this.BrowseForFolder(m_txtSource, "Select Backup Source Folder");
        }

        private void m_btnBrowseDst_Click(object sender, EventArgs e)
        {
            this.BrowseForFolder(m_txtDstFolder, "Select Backup Folder");
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            _entry.FolderDst = m_txtDstFolder.Text;
            _entry.FolderSrc = m_txtSource.Text;
            _entry.FolderIncludeTypes = m_txtFileType.Text;
            _entry.FolderExcludeTypes = m_txtExcludeType.Text;
            _entry.Priority = (BackupPriority)m_cmbPriority.SelectedItem;
        }

        private void ValidateInput(object sender = null, EventArgs e = null)
        {
            BackupEntry entry = new BackupEntry()
            {
                FolderDst = m_txtDstFolder.Text,
                FolderSrc = m_txtSource.Text,
                FolderIncludeTypes = m_txtFileType.Text,
                FolderExcludeTypes = m_txtExcludeType.Text,
                Priority = (BackupPriority)m_cmbPriority.SelectedItem
            };

            m_btnOk.Enabled = entry.IsValid();
        }
    }
}
