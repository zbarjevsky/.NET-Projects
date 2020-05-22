using MZ.Tools;
using MZ.WinForms;
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
        FileUtils.FileProgress _fileProgress = new FileUtils.FileProgress();

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
            m_listFiles.VirtualListSize = 0;

            m_btnAbort.Enabled = false;
            m_btnStart.Enabled = false;
            m_btnPause.Enabled = false;
            m_btnContinue.Enabled = false;
        }

        private void FormBackupProgress_Load(object sender, EventArgs e)
        {
            this.Visible = true;

            _fileProgress.OnPercentChange = (percent) =>
            {
                CommonUtils.ExecuteOnUIThread(() =>
                {
                    m_txtInfo.Text = _fileProgress.ToString();
                    m_progrFile.Value = percent;
                }, this);
                Application.DoEvents();
            };

            _fileProgress.OnValueChange = () =>
            {
                CommonUtils.ExecuteOnUIThread(() =>
                {
                    m_txtInfo.Text = _fileProgress.ToString();
                }, this);
                Application.DoEvents();
            };

            m_progrFile.ColorTheme.Part1_ActiveColor = Color.Violet;
            _logic = new BackupLogic(_group, _Priority, _fileProgress);
            EnableControls(false);
            UpdateDisplayList(calculateNeededSize:true);
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

            _fileProgress.Cancel = true;
            Thread.Sleep(10);

            m_listFiles.VirtualListSize = 0;
            _logic.Clear();
            _backupFilesList.Clear();
            GC.Collect();
        }

        private BackupOptions SelectedOption
        {
            get { return (BackupOptions)Enum.Parse(typeof(BackupOptions), m_cmbOption.SelectedItem.ToString()); }
        }

        private void m_listFiles_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            BackupFile file = _backupFilesList[e.ItemIndex];
            file.ValidateBackupNeeded(SelectedOption);

            e.Item = new ListViewItem(file.Index.ToString("###,##0").PadLeft(10) + ". " + file.Err);
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
                    return 4;
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

            m_progressBarMain.State = Windows7ProgressBar.ProgressBarState.Normal;

            m_cmbViewFilter.SelectedIndex = 0; //reset to all - perform backup on whole list
            UpdateDisplayList(calculateNeededSize:false);

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
                    int updateProgressCount = 128;
                    if (file.ValidateBackupNeeded(options))
                    {
                        updateProgressCount = 33;
                        if (file.IsBigFile())
                        {
                            updateProgressCount = 1;
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
                    }

                    _processedBytes += file.SrcIfo.Length;

                    if (i % updateProgressCount == 0)
                    {
                        _elapsed += stopper.Elapsed;
                        stopper.Restart();
                        UpdateUI(true);
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(3);
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
                EnableControls(isRunning);

                m_progressBarMain.Value = (int)((long)m_progressBarMain.Maximum * (long)(_startIndex+1) / _backupFilesList.Count);
                
                int visibleIndex = _startIndex + 3;
                if (visibleIndex >= _backupFilesList.Count)
                    visibleIndex = _backupFilesList.Count - 1;
                m_listFiles.EnsureVisible(visibleIndex);
                
                TimeSpan estimate = TimeSpan.FromMilliseconds(m_progressBarMain.Maximum * _elapsed.TotalMilliseconds / (m_progressBarMain.Value + 1));

                m_txtStatus.Text = string.Format("Backing up file {0:###,##0} of {1:###,##0}, {2:###,##0.0} MB Copyied, Elapsed {3}, Estimated Total {4}",
                    (_startIndex + 1), _backupFilesList.Count, _processedBytes/BackupLogic.i1MB,
                    _elapsed.ToString("mm':'ss"), estimate.ToString("mm':'ss"));

                if (_abort)
                    m_progressBarMain.Value = 0;
            }, this);
        }

        private void EnableControls(bool isRunning)
        {
            m_btnStart.Enabled = !isRunning;
            m_btnContinue.Enabled = !isRunning;
            m_btnAbort.Enabled = isRunning;
            m_btnPause.Enabled = isRunning;
            m_cmbViewFilter.Enabled = !isRunning;
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
                    m_progressBarMain.State = Windows7ProgressBar.ProgressBarState.Error;

                m_txtStatus.Text = string.Format(
                    "Backup status done: {0:###,##0} of {1:###,##0} files, {2:###,##0.0} MB Copyied, Errors: {3:###,##0}, Elapsed {4}",
                    (_startIndex + 1), _backupFilesList.Count, _processedBytes / BackupLogic.i1MB, errCount, _elapsed.ToString("mm':'ss"));
            }, this);
        }

        private int _startIndex = 0;
        private long _processedBytes = 0;
        private void m_btnStart_Click(object sender, EventArgs e)
        {
            _elapsed = TimeSpan.FromSeconds(0);
            _logic.ResetStatus();

            PerformBackup(0, SelectedOption);
        }

        bool _abort = false;
        private void m_btnAbort_Click(object sender, EventArgs e)
        {
            _abort = true;
            _fileProgress.Cancel = true;
        }

        bool _pause = false;
        private void m_btnPause_Click(object sender, EventArgs e)
        {
            _pause = true;
            m_progressBarMain.State = Windows7ProgressBar.ProgressBarState.Pause;
        }

        private void m_btnContinue_Click(object sender, EventArgs e)
        {
            PerformBackup(_startIndex, SelectedOption);
        }

        private void m_cmbViewFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateDisplayList(calculateNeededSize:true);
        }

        private void UpdateDisplayList(bool calculateNeededSize)
        {
            AbortCalculateSpaceNeededTask();

            string status = "";
            BackupStatus backupStatus = BackupStatus.Any;
            if (m_cmbViewFilter.SelectedIndex == 0) //All
            {
                _backupFilesList = _logic.FilteredFileList(SelectedOption, BackupStatus.Any);
            }
            else if (m_cmbViewFilter.SelectedIndex == 1) //Errors
            {
                status = " error";
                backupStatus = BackupStatus.Error;
                _backupFilesList = _logic.FilteredFileList(SelectedOption, BackupStatus.Error);
            }
            else if (m_cmbViewFilter.SelectedIndex == 2) //Unprocessed
            {
                status = " unprocessed";
                backupStatus = BackupStatus.None;
                _backupFilesList = _logic.FilteredFileList(SelectedOption, BackupStatus.None);
            }

            if(calculateNeededSize)
                StartCalculateSpaceNeededTask(backupStatus);

            m_listFiles.VirtualListSize = _backupFilesList.Count;
            m_listFiles.Refresh();
            m_txtStatus.Text = "Found " + _backupFilesList.Count.ToString("###,###,##0 ") + status + " files to Backup";
        }

        private Thread _threadCalculateSpace = null;
        private void AbortCalculateSpaceNeededTask()
        {
            if (_threadCalculateSpace != null && _threadCalculateSpace.IsAlive)
            {
                _fileProgress.Cancel = true;
                Thread.Sleep(64);
                _threadCalculateSpace.Abort();
                _threadCalculateSpace = null;
            }
        }

        private void StartCalculateSpaceNeededTask(BackupStatus backupStatus)
        {
            AbortCalculateSpaceNeededTask();

            m_txtInfo.Text = "Preparing to Calculate Space Needed...";
            m_btnAbort.Enabled = true;
            m_progrFile.ColorTheme.Part1_ActiveColor = Color.SkyBlue;

            _threadCalculateSpace = new Thread(() =>
            {
                string stat = _logic.GetDiskStatistics();
                _fileProgress.Reset("Calculate Space Needed: ", _backupFilesList.Count, 0, FileUtils.FileProgress.ReportOptions.ReportPercentChange);
                string sizeInfo = _logic.CalculateSpaceNeeded(backupStatus, _fileProgress);
                CommonUtils.ExecuteOnUIThread(() => { m_txtInfo.Text = sizeInfo; m_progrFile.Value = 0; }, this);
            });

            _threadCalculateSpace.IsBackground = true;
            _threadCalculateSpace.Name = "Calculate Space Thread";
            _threadCalculateSpace.Priority = ThreadPriority.Lowest;
            _threadCalculateSpace.Start();
        }
    }
}
