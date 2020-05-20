using MZ.Tools;
using SmartBackup.Settings;
using SmartBackup.Tools;
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
        const double s1MB = 1024 * 1024;

        readonly BackupEntry _entry;
        readonly CalculateSpaceTask _calculateSpaceTask;
        readonly FileUtils.FileProgress _fileProgress = new FileUtils.FileProgress();

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

            m_explorerSrc.OpenFolderAction = (fullPath) => { m_txtSrcFolder.Text = fullPath; };
            m_explorerDst.OpenFolderAction = (fullPath) => { m_txtDstFolder.Text = fullPath; };

            _fileProgress.OnPercentChange = (percent) =>
            {
                CommonUtils.ExecuteOnUIThread(() => 
                { 
                    m_txtInfo.Text = _fileProgress.ToString();
                    m_progressBar.Value = _fileProgress.Percent;
                }, this);
            };

            _fileProgress.OnValueChange = () =>
            {
                CommonUtils.ExecuteOnUIThread(() => 
                { 
                    m_txtInfo.Text = _fileProgress.ToString();
                    m_progressBar.Value = _fileProgress.Percent;
                }, this);
            };

            _calculateSpaceTask = new CalculateSpaceTask(_fileProgress);
            _calculateSpaceTask.OnThreadFinished = (size, count) =>
            {
                CommonUtils.ExecuteOnUIThread(() =>
                {
                    m_txtInfo.Text = string.Format("Selected SRC {0:###,##0} files, Total size: {1:###,##0.0} MB", count, size / s1MB);
                    m_progressBar.Value = 0;
                }, this);
            };
        }

        private void FormBackupFolderProperties_Load(object sender, EventArgs e)
        {
            m_txtSrcFolder.Text = _entry.FolderSrc;
            m_txtDstFolder.Text = _entry.FolderDst;

            m_cmbSearchOptions.SelectedItem = _entry.IncludeSubfolders;
            m_txtFileType.Text = _entry.FolderIncludeTypes;
            m_txtExcludeType.Text = _entry.FolderExcludeTypes;
            m_cmbPriority.SelectedItem = _entry.Priority;

            ValidateInput();

            m_txtDstFolder.Focus();
        }

        private void FormBackupFolderProperties_FormClosed(object sender, FormClosedEventArgs e)
        {
            _calculateSpaceTask.Abort();
        }

        private static string _srcBaseFolder = "";
        private void m_btnBrowseSrc_Click(object sender, EventArgs e)
        {
            this.BrowseForFolder(m_txtSrcFolder, _srcBaseFolder, "Select Backup Source Folder");
            _srcBaseFolder = m_txtSrcFolder.Text;
            m_explorerSrc.PopulateFiles(_srcBaseFolder);
        }

        private static string _dstBaseFolder = "";
        private void m_btnBrowseDst_Click(object sender, EventArgs e)
        {
            this.BrowseForFolder(m_txtDstFolder, _dstBaseFolder, "Select Backup Destination Folder");
            _dstBaseFolder = m_txtDstFolder.Text;
            m_explorerDst.PopulateFiles(_dstBaseFolder);
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            _entry.FolderSrc = m_txtSrcFolder.Text;
            _entry.FolderDst = m_txtDstFolder.Text;
            _entry.IncludeSubfolders = (SearchOption)m_cmbSearchOptions.SelectedItem;
            _entry.FolderIncludeTypes = m_txtFileType.Text;
            _entry.FolderExcludeTypes = m_txtExcludeType.Text;
            _entry.Priority = (BackupPriority)m_cmbPriority.SelectedItem;
        }

        private void m_btnStartBackup_Click(object sender, EventArgs e)
        {
            m_btnOk_Click(sender, e);
            DialogResult = DialogResult.Yes;
        }

        private void ValidateInput(object sender = null, EventArgs e = null)
        {
            BackupEntry entry = new BackupEntry()
            {
                FolderSrc = m_txtSrcFolder.Text,
                FolderDst = m_txtDstFolder.Text,
                IncludeSubfolders = (SearchOption)m_cmbSearchOptions.SelectedItem,
                FolderIncludeTypes = m_txtFileType.Text,
                FolderExcludeTypes = m_txtExcludeType.Text,
                Priority = (BackupPriority)m_cmbPriority.SelectedItem
            };

            string error;
            m_btnOk.Enabled = entry.IsValid(out error);
            m_btnStartBackup.Enabled = m_btnOk.Enabled;
            UpdateInfo(entry, error);
        }

        private void UpdateInfo(BackupEntry entry, string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                m_txtInfo.Text = error;
                errorProvider1.SetError(m_btnStartBackup, error);
                return;
            }

            errorProvider1.SetError(m_btnStartBackup, error); //clear error

            m_explorerSrc.PopulateFiles(entry.FolderSrc);
            m_explorerDst.PopulateFiles(entry.FolderDst);

            _calculateSpaceTask.Start(entry);
        }
    }
}
