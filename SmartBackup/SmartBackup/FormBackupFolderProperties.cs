using MZ.Tools;
using SmartBackup.Settings;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            m_cmbSearchOptions.Items.Clear();
            m_cmbSearchOptions.Items.AddRange(Enum.GetValues(typeof(SearchOption)).Cast<Object>().ToArray());
            m_cmbSearchOptions.SelectedIndex = 1;
        }

        private void FormBackupFolderProperties_Load(object sender, EventArgs e)
        {
            m_txtDstFolder.Text = _entry.FolderDst;
            m_txtSource.Text = _entry.FolderSrc;

            m_cmbSearchOptions.SelectedItem = _entry.IncludeSubfolders;
            m_txtFileType.Text = _entry.FolderIncludeTypes;
            m_txtExcludeType.Text = _entry.FolderExcludeTypes;
            m_cmbPriority.SelectedItem = _entry.Priority;

            ValidateInput();
        }

        private static string _srcBaseFolder = "";
        private void m_btnBrowseSrc_Click(object sender, EventArgs e)
        {
            this.BrowseForFolder(m_txtSource, _srcBaseFolder, "Select Backup Source Folder");
            _srcBaseFolder = m_txtSource.Text;
        }

        private static string _dstBaseFolder = "";
        private void m_btnBrowseDst_Click(object sender, EventArgs e)
        {
            this.BrowseForFolder(m_txtDstFolder, _dstBaseFolder, "Select Backup Destination Folder");
            _dstBaseFolder = m_txtDstFolder.Text;
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            _entry.FolderDst = m_txtDstFolder.Text;
            _entry.FolderSrc = m_txtSource.Text;
            _entry.IncludeSubfolders = (SearchOption)m_cmbSearchOptions.SelectedItem;
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
                IncludeSubfolders = (SearchOption)m_cmbSearchOptions.SelectedItem,
                FolderIncludeTypes = m_txtFileType.Text,
                FolderExcludeTypes = m_txtExcludeType.Text,
                Priority = (BackupPriority)m_cmbPriority.SelectedItem
            };

            m_btnOk.Enabled = entry.IsValid();
            UpdateInfo(entry);
        }

        private Thread _threadUpdateInfo = null;
        private void UpdateInfo(BackupEntry entry)
        {
            const double s1MB = 1024 * 1024;
            m_txtInfo.Text = "Calculating Folder Size...";

            if (_threadUpdateInfo != null && _threadUpdateInfo.IsAlive)
                _threadUpdateInfo.Abort();

            _threadUpdateInfo = new Thread(() => 
            {
                long size = 0;
                List<BackupFile> fileList = BackupLogic.CollectFiles(entry);
                int count = fileList.Count;
               foreach (BackupFile file in fileList)
                {
                    size += file.SrcIfo.Length;
                }
                fileList.Clear();
                GC.Collect();

                CommonUtils.ExecuteOnUIThread(() => 
                {
                    m_txtInfo.Text = string.Format("Selected SRC files: {0:###,##0} size: {1:###,##0.0} MB", count, size/s1MB);
                }, this);
            });

            _threadUpdateInfo.IsBackground = true;
            _threadUpdateInfo.Name = "Update Info Thread";
            _threadUpdateInfo.Start();
        }
    }
}
