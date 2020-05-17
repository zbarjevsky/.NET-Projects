using MZ.Tools;
using MZ.Tools.WinForms;
using SmartBackup.Settings;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartBackup
{
    public partial class FormBackupProgress : Form
    {
        BackupGroup _group;
        BackupLogic _logic;
        readonly BackupPriority _Priority = BackupPriority.All;
        List<BackupFile> _backupFilesList;

        public FormBackupProgress(BackupGroup group, BackupPriority priority)
        {
            _group = group;
            _Priority = priority;

            InitializeComponent();

            m_listFiles.SetDoubleBuffered(true);

            m_cmbOption.Items.Clear();
            m_cmbOption.Items.AddRange(Enum.GetNames(typeof(BackupOptions)).ToArray());
            m_cmbOption.SelectedIndex = 1;

            m_cmbViewFilter.SelectedIndex = 0; //all
        }

        private void FormBackupProgress_Load(object sender, EventArgs e)
        {
            _logic = new BackupLogic(_group, _Priority);
            UpdateDisplayList();
        }

        private void FormBackupProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_working)
            {
                DialogResult res = MessageBox.Show(this, "Backup In Progress, Abort?", "Warning",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                m_btnAbort_Click(sender, e);
            }

            m_listFiles.VirtualListSize = 0;
            _logic.Clear();
            _backupFilesList.Clear();
            GC.Collect();
        }

        private void m_listFiles_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            BackupFile file = _backupFilesList[e.ItemIndex];

            e.Item = new ListViewItem(e.ItemIndex + ". " + file.Err);
            e.Item.SubItems.Add(file.Src);
            e.Item.SubItems.Add(file.Dst);
            e.Item.SubItems.Add((file.SrcIfo.Length/1024.0).ToString("###,##0.0 k"));
            e.Item.ImageIndex = GetImageIndexFromStatus(file.Status);
        }

        private int GetImageIndexFromStatus(BackupStatus status)
        {
            switch (status)
            {
                case BackupStatus.None:
                    return 0;
                case BackupStatus.InProgress:
                    return 1;
                case BackupStatus.Done:
                    return 2;
                case BackupStatus.Error:
                    return 3;
                case BackupStatus.Any:
                default:
                    throw new IndexOutOfRangeException("Invalid status for icon: " + status);
            }
        }

        bool _working = false;
        private void PerformBackup(int startIndex, BackupOptions options)
        {
            _abort = false;
            _pause = false;
            _working = true;

            m_progressBar1.State = Windows7ProgressBar.ProgressBarState.Normal;

            Stopwatch stopper = new Stopwatch();
            stopper.Start();

            if (startIndex == 0)
                _processedBytes = 0;

            Thread threadBackup = new Thread(() =>
            {
                for (int i = startIndex; i < _backupFilesList.Count; i++)
                {
                    if (_abort || _pause)
                        break;

                    _startIndex = i;
                    BackupFile file = _backupFilesList[i];
                    if (file.IsBigFile())
                    {
                        CommonUtils.ExecuteOnUIThread(() =>
                        {
                            file.Status = BackupStatus.InProgress;
                            m_listFiles.EnsureVisible(_startIndex);
                        }, this);
                    }

                    if (file.PerformBackup(m_progrFile, this, options) == BackupStatus.Error)
                    {
                        Thread.Sleep(10);
                        file.PerformBackup(m_progrFile, this, options); //retry
                    }

                    _processedBytes += file.SrcIfo.Length;

                    if (i % 33 == 0 || file.IsBigFile())
                    {
                        _elapsed += stopper.Elapsed;
                        stopper.Restart();
                        UpdateUI(true);
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(0);
                    }
                }

                stopper.Stop();
                _elapsed += stopper.Elapsed;
                SystemSounds.Beep.Play();
                UpdateUI(false);
                UpdateBackupStatus();
                _working = false;
            });
            threadBackup.IsBackground = true;
            threadBackup.Name = "Backup Thread";
            threadBackup.Priority = ThreadPriority.Lowest;
            threadBackup.Start();
        }

        TimeSpan _elapsed = TimeSpan.FromSeconds(0);

        private void UpdateUI(bool isRunning)
        {
            CommonUtils.ExecuteOnUIThread(() =>
            {
                m_btnStart.Enabled = !isRunning;
                m_btnContinue.Enabled = !isRunning;
                m_btnAbort.Enabled = isRunning;
                m_btnPause.Enabled = isRunning;
                m_cmbViewFilter.Enabled = !isRunning;

                m_progressBar1.Value = (int)((long)m_progressBar1.Maximum * (long)(_startIndex+1) / _backupFilesList.Count);
                m_listFiles.EnsureVisible(_startIndex);
                TimeSpan estimate = TimeSpan.FromMilliseconds(m_progressBar1.Maximum * _elapsed.TotalMilliseconds / (m_progressBar1.Value + 1));

                m_txtStatus.Text = string.Format("Backing up file {0:###,##0} of {1:###,##0}, {2:###,##0.0} MB Copyied, Elapsed {3}, Total {4}",
                    (_startIndex + 1), _backupFilesList.Count, _processedBytes/BackupLogic.i1MB,
                    _elapsed.ToString("mm':'ss"), estimate.ToString("mm':'ss"));

                if (_abort)
                    m_progressBar1.Value = 0;
            }, this);
        }

        private void UpdateBackupStatus()
        {
            CommonUtils.ExecuteOnUIThread(() =>
            {
                int errCount = 0;
                for (int i = 0; i < _backupFilesList.Count; i++)
                {
                    if (_backupFilesList[i].Status == BackupStatus.Error)
                        errCount++;
                }

                if(errCount>0)
                    m_progressBar1.State = Windows7ProgressBar.ProgressBarState.Error;

                m_txtStatus.Text = string.Format(
                    "Backup status done: {0:###,##0} of {1:###,##0} files, {2:###,##0.0} MB Copyied, Errors: {3:###,##0}, Elapsed {4}",
                    (_startIndex + 1), _backupFilesList.Count, _processedBytes / BackupLogic.i1MB, errCount, _elapsed.ToString("mm':'ss"));
            }, this);
        }

        private int _startIndex = 0;
        private long _processedBytes = 0;
        private void m_btnStart_Click(object sender, EventArgs e)
        {
            m_cmbViewFilter.SelectedIndex = 0; //show all

            _elapsed = TimeSpan.FromSeconds(0);
            _logic.ResetStatus();

            BackupOptions options = (BackupOptions)Enum.Parse(typeof(BackupOptions), m_cmbOption.SelectedItem.ToString());
            PerformBackup(0, options);
        }

        bool _abort = false;
        private void m_btnAbort_Click(object sender, EventArgs e)
        {
            _abort = true;
        }

        bool _pause = false;
        private void m_btnPause_Click(object sender, EventArgs e)
        {
            _pause = true;
            m_progressBar1.State = Windows7ProgressBar.ProgressBarState.Pause;
        }

        private void m_btnContinue_Click(object sender, EventArgs e)
        {
            BackupOptions options = (BackupOptions)Enum.Parse(typeof(BackupOptions), m_cmbOption.SelectedItem.ToString());
            PerformBackup(_startIndex, options);
        }

        private void m_cmbViewFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateDisplayList();
        }

        private void UpdateDisplayList()
        {
            string status = "";
            BackupStatus backupStatus = BackupStatus.Any;
            if (m_cmbViewFilter.SelectedIndex == 0) //All
            {
                _backupFilesList = _logic.FilteredFileList(BackupStatus.Any);
            }
            else if (m_cmbViewFilter.SelectedIndex == 1) //Errors
            {
                status = " error";
                backupStatus = BackupStatus.Error;
                _backupFilesList = _logic.FilteredFileList(BackupStatus.Error);
            }
            else if (m_cmbViewFilter.SelectedIndex == 2) //Unprocessed
            {
                status = " unprocessed";
                backupStatus = BackupStatus.None;
                _backupFilesList = _logic.FilteredFileList(BackupStatus.None);
            }

            m_txtInfo.Text = "Loading...";
            Task task = new Task(() =>
            {
                string stat = _logic.GetDiskStatistics();
                string sizeInfo = _logic.CalculateSpaceNeeded(backupStatus);
                CommonUtils.ExecuteOnUIThread(() => { m_txtInfo.Text = sizeInfo; }, this);
            });
            task.Start();

            m_listFiles.VirtualListSize = _backupFilesList.Count;
            m_listFiles.Refresh();
            m_txtStatus.Text = "Found " + _backupFilesList.Count.ToString("###,###,##0 ") + status + " files to Backup";
        }
    }
}
