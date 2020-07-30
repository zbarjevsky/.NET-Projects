using MZ.Tools;
using MZ.WinForms;
using SimpleBackup.Settings;
using SimpleBackup.Tools;
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
using static MZ.Tools.FileUtils;

namespace SimpleBackup
{
    public partial class FormBackupProgress : Form
    {
        readonly BackupGroup _group;
        readonly BackupLogic _logic;
        readonly BackupPriority _Priority = BackupPriority.All;
        readonly FileUtils.FileProgress _fileProgress;
        readonly CalculateSpaceDifferenceTask _calculateSpaceDifferenceTask;

        private List<BackupFile> _backupFilesList;

        private BackupOptions SelectedOverwriteOption
        {
            get { return (BackupOptions)m_cmbOption.SelectedItem; }
        }

        public FormBackupProgress(BackupGroup group, BackupPriority priority)
        {
            _group = group;
            _Priority = priority;

            InitializeComponent();

            m_listFiles.SetDoubleBuffered(true);

            m_cmbOption.Items.Clear();
            m_cmbOption.Items.AddRange(Enum.GetValues(typeof(BackupOptions)).Cast<object>().ToArray());
            m_cmbOption.SelectedIndex = 1;

            m_cmbViewFilter.SelectedIndex = 0; //all
            m_listFiles.VirtualListSize = 0;
            m_listFiles.SetDoubleBuffered(true);

            m_btnAbort.Enabled = false;
            m_btnStart.Enabled = false;
            m_btnPause.Enabled = false;
            m_btnContinue.Enabled = false;

            _fileProgress = new FileUtils.FileProgress(m_progressFile, this);
            _fileProgress.OnChange = (status) => { m_txtInfoTop.Text = status; };
            _logic = new BackupLogic(_group);

            _calculateSpaceDifferenceTask = new CalculateSpaceDifferenceTask(_fileProgress);
            _calculateSpaceDifferenceTask.OnThreadFinished = (status, error) => 
            {
                CommonUtils.ExecuteOnUIThread(() => 
                {
                    m_txtInfoTop.Text = status;
                    m_progressFile.Value = 0;
                    errorProvider1.SetError(m_txtInfoTop, error);
                    m_lblStatusProgress.Visible = false;
                    EnableControls(true, false);
                }, this);
            };
        }

        private void FormBackupProgress_Load(object sender, EventArgs e)
        {
            this.Visible = true;

            _fileProgress.OnChange = (status) => 
            { 
                m_lblStatusProgress.Text = status; 
            };

            m_btnPrepare_Click(sender, e);
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

            Clear();
        }

        private void Clear()
        {
            _fileProgress.Cancel = true;
            Thread.Sleep(10);

            AbortCalculateSpaceNeededTask();

            m_listFiles.VirtualListSize = 0;
            if (_logic != null)
                _logic.Clear();
            if (_backupFilesList != null)
                _backupFilesList.Clear();
            GC.Collect();
        }

        private void m_btnPrepare_Click(object sender, EventArgs e)
        {
            Clear();

            m_progressFile.ColorTheme.Part1_ActiveColor = Color.BlueViolet;

            EnableControls(false, false, false);
            m_btnAbort.Enabled = true;

            m_txtInfoTop.Text = "Collecting file Information...";
            m_txtInfoBottom.Text = "Please Wait...";
            m_btnClose.Text = "Close";

            m_lblStatusProgress.Visible = true;

            _logic.Load(_Priority, _fileProgress, (error) =>
            {
                CommonUtils.ExecuteOnUIThread(() => 
                {
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        m_lblStatusProgress.Visible = false;
                        EnableControls(true, false, true);
                        UpdateDisplayList(calculateNeededSize: true);
                    }
                    else
                    {
                        m_lblStatusProgress.Text = error;
                        EnableControls(false, false, true);
                    }
                }, this);
            });
        }

        private void m_listFiles_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            BackupFile file = _backupFilesList[e.ItemIndex];
            file.ValidateBackupNeeded(SelectedOverwriteOption);

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
            DriveInfo drive = BackupLogic.GetDriveInfo(_backupFilesList[0].Src);
            if(drive == null)
            {
                MessageBox.Show(this, "Source Drive is not accessible: " + _backupFilesList[0].Src);
                return;
            }

            const double ONE_MB = 1024 * 1024;
            double bigFileSizeThreshold = (drive.DriveType == DriveType.Network) ? 0.6 * ONE_MB : 12 * ONE_MB;
            int refreshUICount = (drive.DriveType == DriveType.Network) ? 3 : 33;

            _abort = false;
            _pause = false;
            _working = true;

            m_progressBarMain.State = Windows7ProgressBar.ProgressBarState.Normal;
            m_progressBarMain.Maximum = _backupFilesList.Count;
            m_progressBarMain.Minimum = 0;
            m_progressBarMain.Value = startIndex;

            m_chartProgress.GraphBackColor = Color.LightBlue;
            m_chartProgress.GraphMainColor = Color.Blue;

            m_progressFile.Style = ProgressBarStyle.Blocks;
            FileProgress progressCopyBigFile = new FileProgress(m_progressFile, this, NotifyOptions.NotifyPercentChange);
            progressCopyBigFile.OnChange = (status) =>
            {
                m_lblProgressFile.Text = string.Format("{0}%", progressCopyBigFile.Percent);
                m_lblSpeedFile.Text = progressCopyBigFile.SubStatus;
                if (_abort || _pause)
                    progressCopyBigFile.Cancel = true;
            };

            m_cmbViewFilter.SelectedIndex = 0; //reset to all - perform backup on whole list
            UpdateDisplayList(calculateNeededSize:false);

            _progressSpeedData.InitCounting(startIndex == 0);

            Thread threadBackup = new Thread(() =>
            {
                for (int i = startIndex; i < _backupFilesList.Count; i++)
                {
                    if (_abort || _pause)
                        break;

                    BackupFile file = _backupFilesList[i];
                    file.BigFileSizeThreshold = bigFileSizeThreshold;

                    int updateProgressCount = 128;
                    if (file.ValidateBackupNeeded(options))
                    {
                        updateProgressCount = refreshUICount;
                        if (file.IsBigFile())
                        {
                            updateProgressCount = 1;
                            CommonUtils.ExecuteOnUIThread(() =>
                            {
                                file.Status = BackupStatus.InProgress;
                                m_listFiles.EnsureVisible(i);
                                m_listFiles.Refresh();
                            }, this);
                        }

                        if (file.PerformBackup(progressCopyBigFile, options) == BackupStatus.Error)
                        {
                            Thread.Sleep(10);
                            file.PerformBackup(progressCopyBigFile, options); //retry
                        }
                    }

                    _startIndex = i;
                    _progressSpeedData.AddCount(file.SrcIfo.Length, i, _backupFilesList.Count);

                    if (i % updateProgressCount == 0)
                    {
                        UpdateUI(true);
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(3);
                    }
                }

                _progressSpeedData.StopCounting();
                SystemSounds.Beep.Play();
                UpdateUI(false);
                OnBackupDone();
                _working = false;
            });
            threadBackup.IsBackground = true;
            threadBackup.Name = "Backup Thread";
            threadBackup.Priority = ThreadPriority.Lowest;
            threadBackup.Start();
        }

        TransferSpeedCounter _progressSpeedData = new TransferSpeedCounter();

        private void UpdateUI(bool isRunning)
        {
            CommonUtils.ExecuteOnUIThread(() =>
            {
                EnableControls(true, isRunning);

                m_progressBarMain.Value = _startIndex;

                TimeSpan estimatedTotal = _progressSpeedData.EstimatedTotal(m_progressBarMain);

                m_txtInfoBottom.Text = string.Format(
                    "Copying file {0:###,##0} of {1:###,##0}, {2:###,##0.0} MB Done, Elapsed {3}, Total Estimated: {4}, Speed {5:###,##0.0} KB/s Average Speed {6:###,##0.0} KB/s",
                    (_startIndex + 1), _backupFilesList.Count, _progressSpeedData.iBytesCount/ BackupLogic.i1MB,
                    _progressSpeedData.ElapsedTime.ToString("h':'mm':'ss"),  estimatedTotal.ToString("h':'mm':'ss"),
                    _progressSpeedData.SpeedInervalKB(TimeSpan.FromSeconds(10)), _progressSpeedData.SpeedAvgKB());

                int visibleIndex = _startIndex + 1;
                if (visibleIndex >= _backupFilesList.Count)
                    visibleIndex = _backupFilesList.Count - 1;
                
                if (visibleIndex >= 0 && visibleIndex < _backupFilesList.Count)
                {
                    m_listFiles.EnsureVisible(visibleIndex);
                    m_listFiles.Invalidate();
                }

                int max = 100;
                List<double> speeds = _progressSpeedData.SpeedHistory(_backupFilesList.Count, ref max);
                m_chartProgress.SetHistory(speeds, max, _backupFilesList[_startIndex].Src);

                if (_abort)
                    m_progressBarMain.Value = 0;

                //Application.DoEvents();
            }, this);
        }

        private void EnableControls(bool bEnable, bool isRunning, bool prepare = true)
        {
            m_btnPrepare.Enabled = prepare && !isRunning;
            m_btnStart.Enabled = bEnable && !isRunning;
            m_btnContinue.Enabled = bEnable && !isRunning;
            m_btnAbort.Enabled = bEnable && isRunning;
            m_btnPause.Enabled = bEnable && isRunning;
            m_cmbViewFilter.Enabled = bEnable && !isRunning;
        }

        private void OnBackupDone()
        {
            Windows7ProgressBar.ProgressBarState state = Windows7ProgressBar.ProgressBarState.Normal;
            if (_abort)
                state = Windows7ProgressBar.ProgressBarState.Error;
            if (_pause)
                state = Windows7ProgressBar.ProgressBarState.Pause;

            CommonUtils.ExecuteOnUIThread(() =>
            {
                int errCount = 0;
                for (int i = 0; i < _backupFilesList.Count; i++)
                {
                    if (_backupFilesList[i].Status == BackupStatus.Error)
                        errCount++;
                }

                SetProgressState(state);

                m_btnClose.Text = "Done";

                if (errCount>0)
                    m_progressBarMain.State = Windows7ProgressBar.ProgressBarState.Error;

                m_txtInfoBottom.Text = string.Format(
                    "Backup status done: {0:###,##0} of {1:###,##0} files, {2:###,##0.0} MB Copyied, Errors: {3:###,##0}, Elapsed {4}, Average Speed {5:0.0} KB/s",
                    (_startIndex + 1), _backupFilesList.Count, 
                    _progressSpeedData.iBytesCount / BackupLogic.i1MB, errCount, 
                    _progressSpeedData.ElapsedTime.ToString("mm':'ss"), _progressSpeedData.SpeedAvgKB());
            }, this);
        }

        private void SetProgressState(Windows7ProgressBar.ProgressBarState state)
        {
            if (state == Windows7ProgressBar.ProgressBarState.Pause)
            {
                m_progressBarMain.State = state;
                m_chartProgress.GraphBackColor = Color.LightYellow;
                m_chartProgress.GraphMainColor = Color.Goldenrod;
            }
            else if (state == Windows7ProgressBar.ProgressBarState.Normal)
            {
                m_progressBarMain.State = state;
                m_chartProgress.GraphBackColor = Color.PaleGreen;
                m_chartProgress.GraphMainColor = Color.Green;
            }
            else
            {
                m_progressBarMain.State = state;
                m_chartProgress.GraphBackColor = Color.Pink;
                m_chartProgress.GraphMainColor = Color.Red;
            }
        }

        private int _startIndex = 0;
        private void m_btnStart_Click(object sender, EventArgs e)
        {
            _logic.ResetStatus();

            PerformBackup(0, SelectedOverwriteOption);
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
        }

        private void m_btnContinue_Click(object sender, EventArgs e)
        {
            PerformBackup(_startIndex, SelectedOverwriteOption);
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
                _backupFilesList = _logic.FilteredFileList(SelectedOverwriteOption, BackupStatus.Any);
            }
            else if (m_cmbViewFilter.SelectedIndex == 1) //Errors
            {
                status = " error";
                backupStatus = BackupStatus.Error;
                _backupFilesList = _logic.FilteredFileList(SelectedOverwriteOption, BackupStatus.Error);
            }
            else if (m_cmbViewFilter.SelectedIndex == 2) //Unprocessed
            {
                status = " unprocessed";
                backupStatus = BackupStatus.None;
                _backupFilesList = _logic.FilteredFileList(SelectedOverwriteOption, BackupStatus.None);
            }

            if(calculateNeededSize)
                StartCalculateSpaceNeededTask(backupStatus);

            m_listFiles.VirtualListSize = _backupFilesList.Count;
            m_listFiles.Refresh();
            m_txtInfoBottom.Text = "Found " + _backupFilesList.Count.ToString("###,###,##0 ") + status + " files to Backup";
        }

        private void AbortCalculateSpaceNeededTask()
        {
            _calculateSpaceDifferenceTask.Abort();
        }

        private void StartCalculateSpaceNeededTask(BackupStatus backupStatus)
        {
            AbortCalculateSpaceNeededTask();

            m_txtInfoTop.Text = "Preparing to Calculate Space Needed...";
            m_btnAbort.Enabled = true;
            m_progressFile.ColorTheme.Part1_ActiveColor = Color.SkyBlue;

            m_btnAbort.Enabled = true;
            _fileProgress.OnChange = (status) => { m_txtInfoTop.Text = status; };
            _calculateSpaceDifferenceTask.Start(_backupFilesList, backupStatus, SelectedOverwriteOption);
        }
    }
}
