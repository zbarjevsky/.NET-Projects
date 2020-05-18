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
        }

        private void FormBackupFolderProperties_FormClosed(object sender, FormClosedEventArgs e)
        {
            _fileProgress.IsCancel = true;
            if (_threadUpdateInfo != null && _threadUpdateInfo.IsAlive)
                _threadUpdateInfo.Abort();
        }

        private static string _srcBaseFolder = "";
        private void m_btnBrowseSrc_Click(object sender, EventArgs e)
        {
            this.BrowseForFolder(m_txtSrcFolder, _srcBaseFolder, "Select Backup Source Folder");
            _srcBaseFolder = m_txtSrcFolder.Text;
            m_explorerSrc.ShowFolder(_srcBaseFolder);
        }

        private static string _dstBaseFolder = "";
        private void m_btnBrowseDst_Click(object sender, EventArgs e)
        {
            this.BrowseForFolder(m_txtDstFolder, _dstBaseFolder, "Select Backup Destination Folder");
            _dstBaseFolder = m_txtDstFolder.Text;
            m_explorerDst.ShowFolder(_dstBaseFolder);
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

            m_btnOk.Enabled = entry.IsValid();
            UpdateInfo(entry);
        }

        private Thread _threadUpdateInfo = null;
        private void UpdateInfo(BackupEntry entry)
        {
            if(!Directory.Exists(entry.FolderSrc))
            {
                m_txtInfo.Text = "Cannot Find Source...";
                return;
            }

            m_explorerSrc.ShowFolder(entry.FolderSrc);
            if (Directory.Exists(entry.FolderDst))
                m_explorerDst.ShowFolder(entry.FolderDst);

            const double s1MB = 1024 * 1024;
            m_txtInfo.Text = "Calculating Folder Size...";

            _fileProgress.IsCancel = true; //cancel previous if running
            if (_threadUpdateInfo != null && _threadUpdateInfo.IsAlive)
                _threadUpdateInfo.Abort();

            _threadUpdateInfo = new Thread(() =>
            {
                long size = 0;
                List<BackupFile> fileList = BackupLogic.CollectFiles(entry, _fileProgress);

                int count = fileList.Count;

                _fileProgress.Reset("Calculating Folder Size ", count, 0, FileUtils.FileProgress.ReportOptions.ReportPercentChange);
                foreach (BackupFile file in fileList)
                {
                    if (_fileProgress.IsCancel)
                        break;

                    _fileProgress.Val++;
                    if (file.SrcIfo.Exists)
                        size += file.SrcIfo.Length;
                }
                _fileProgress.Val = 0;
                fileList.Clear();
                GC.Collect();

                CommonUtils.ExecuteOnUIThread(() =>
                {
                    m_txtInfo.Text = string.Format("Selected SRC files: {0:###,##0} size: {1:###,##0.0} MB", count, size / s1MB);
                }, this);
            });

            _threadUpdateInfo.IsBackground = true;
            _threadUpdateInfo.Name = "Update Info Thread";
            _threadUpdateInfo.Start();
        }
    }
}
