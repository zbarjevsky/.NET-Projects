using MZ.Tools;
using SimpleBackup.Settings;
using SimpleBackup.Tools;
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

namespace SimpleBackup
{
    public partial class FormBackupFolderProperties : Form
    {
        const double s1MB = 1024 * 1024;

        readonly BackupEntry _entry;
        readonly CalculateOccupiedSpaceTask _calculateSpaceTask;
        readonly FileUtils.FileProgress _fileProgress;

        private static string _srcBaseFolder = "";
        private static string _dstBaseFolder = "";

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

            m_explorerSrc.OpenFolderAction = (fullPath) => { _srcBaseFolder = fullPath; };
            m_explorerDst.OpenFolderAction = (fullPath) => { _dstBaseFolder = fullPath; };

            _fileProgress = new FileUtils.FileProgress(m_progressBar, this);
            _fileProgress.OnChange = (status) => { m_txtInfo.Text = status; };

            _calculateSpaceTask = new CalculateOccupiedSpaceTask(_fileProgress);
            _calculateSpaceTask.OnThreadFinished = (size, count) =>
            {
                CommonUtils.ExecuteOnUIThread(() =>
                {
                    string diskInfo = BackupLogic.GetDiskFreeSpace(_dstBaseFolder, out long freeSpace);
                    m_txtInfo.Text = string.Format("Selected SRC {0:###,##0} files, Total size: {1:###,##0.0} MB, {2}", 
                        count, size / s1MB, diskInfo);
                    m_progressBar.Value = 0;
                }, this);
            };
        }

        private void FormBackupFolderProperties_Load(object sender, EventArgs e)
        {
            _srcBaseFolder = _entry.FolderSrc;
            _dstBaseFolder = _entry.FolderDst;

            m_cmbSearchOptions.SelectedItem = _entry.IncludeSubfolders;
            m_txtFileType.Text = _entry.FolderIncludeTypes;
            m_txtExcludeType.Text = _entry.FolderExcludeTypes;
            m_cmbPriority.SelectedItem = _entry.Priority;

            this.Visible = true;

            ValidateInput();
        }

        private void FormBackupFolderProperties_FormClosed(object sender, FormClosedEventArgs e)
        {
            _calculateSpaceTask.Abort();
        }

        private void m_btnBrowseSrc_Click(object sender, EventArgs e)
        {
            this.BrowseForFolder(ref _srcBaseFolder, _srcBaseFolder, "Select Backup Source Folder");
            m_explorerSrc.PopulateFiles(_srcBaseFolder);
        }

        private void m_btnBrowseDst_Click(object sender, EventArgs e)
        {
            this.BrowseForFolder(ref _dstBaseFolder, _dstBaseFolder, "Select Backup Destination Folder");
            m_explorerDst.PopulateFiles(_dstBaseFolder);
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            _entry.FolderSrc = _srcBaseFolder;
            _entry.FolderDst = _dstBaseFolder;
            _entry.IncludeSubfolders = (SearchOption)m_cmbSearchOptions.SelectedItem;
            _entry.FolderIncludeTypes = m_txtFileType.Text;
            _entry.FolderExcludeTypes = m_txtExcludeType.Text;
            _entry.Priority = (BackupPriority)m_cmbPriority.SelectedItem;

            _entry.SelectedFoldersAndFilesList.AllInSrcFolder = m_explorerSrc.IsAllChecked();
            _entry.SelectedFoldersAndFilesList.Names.Clear();
            if (!_entry.SelectedFoldersAndFilesList.AllInSrcFolder)
            {
                _entry.SelectedFoldersAndFilesList.Names.AddRange(m_explorerSrc.GetCheckedFiles());
            }
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
                FolderSrc = _srcBaseFolder,
                FolderDst = _dstBaseFolder,
                IncludeSubfolders = (SearchOption)m_cmbSearchOptions.SelectedItem,
                FolderIncludeTypes = m_txtFileType.Text,
                FolderExcludeTypes = m_txtExcludeType.Text,
                Priority = (BackupPriority)m_cmbPriority.SelectedItem
            };

            UpdateInfo(entry);
        }

        private void UpdateInfo(BackupEntry entry)
        {
            m_btnOk.Enabled = entry.IsValid(out string error);
            m_btnStartBackup.Enabled = m_btnOk.Enabled;

            errorProvider1.SetError(m_btnStartBackup, error); //clear or set error

            m_explorerSrc.PopulateFiles(entry.FolderSrc);
            m_explorerDst.PopulateFiles(entry.FolderDst);

            UpdateCheckedFiles(entry.FolderSrc);

            if (Directory.Exists(entry.FolderSrc))
            {
                m_txtInfo.Text = "Calculating Space Needed...";
                _calculateSpaceTask.Start(entry);
                Application.DoEvents();
            }
            else
            {
                m_txtInfo.Text = error;
            }
        }

        private void UpdateCheckedFiles(string fullPath)
        {
            if (fullPath == _entry.FolderSrc) //original path
            {
                m_explorerSrc.SetCheckedFiles(_entry.SelectedFoldersAndFilesList.Names, _entry.SelectedFoldersAndFilesList.AllInSrcFolder);
            }
        }
    }
}
