using MZ.Tools;
using MZ.WinForms;
using SmartBackup.Settings;
using SmartBackup.Tools;
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
        readonly BackupGroup _group;
        readonly BackupLogic _logic;
        readonly BackupPriority _Priority = BackupPriority.All;
        readonly FileUtils.FileProgress _fileProgress;
        readonly CalculateSpaceDifferenceTask _calculateSpaceDifferenceTask;

        private List<BackupFile> _backupFilesList;

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

            _fileProgress = new FileUtils.FileProgress(m_progressFile, this);
            _logic = new BackupLogic(_group);

            _calculateSpaceDifferenceTask = new CalculateSpaceDifferenceTask(_fileProgress);
            _calculateSpaceDifferenceTask.OnThreadFinished = (status, error) => 
            {
                CommonUtils.ExecuteOnUIThread(() => 
                {
                    m_txtInfo.Text = status; 
                    m_progressFile.Value = 0;
                    errorProvider1.SetError(m_txtInfo, error);
                }, this);
            };
        }

        private void FormBackupProgress_Load(object sender, EventArgs e)
        {
            this.Visible = true;

            _fileProgress.OnChange = (status) => { m_txtInfo.Text = status; };

            m_progressFile.ColorTheme.Part1_ActiveColor = Color.Violet;
            _logic.Load(_Priority, _fileProgress);
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
            if(_logic != null)
                _logic.Clear();
            if(_backupFilesList != null)
                _backupFilesList.Clear();
            GC.Collect();
        }

        private BackupOptions SelectedOverwriteOption
        {
            get { return (BackupOptions)Enum.Parse(typeof(BackupOptions), m_cmbOption.SelectedItem.ToString()); }
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
                    file.BigFileSizeThreshold = bigFileSizeThreshold;

                    int updateProgressCount = 128;
                    if (file.ValidateBackupNeeded(options))
                    {
                        updateProgressCount = 3;
                        if (file.IsBigFile())
                        {
                            updateProgressCount = 1;
                            CommonUtils.ExecuteOnUIThread(() =>
                            {
                                file.Status = BackupStatus.InProgress;
                                m_listFiles.EnsureVisible(_startIndex);
                                m_listFiles.Refresh();
                            }, this);
                        }

                        if (file.PerformBackup(m_progressFile, this, options) == BackupStatus.Error)
                        {
                            Thread.Sleep(10);
                            file.PerformBackup(m_progressFile, this, options); //retry
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
                m_listFiles.Refresh();

                TimeSpan estimate = TimeSpan.FromMilliseconds(m_progressBarMain.Maximum * _elapsed.TotalMilliseconds / (m_progressBarMain.Value + 1));

                m_txtStatus.Text = string.Format("Copying file {0:###,##0} of {1:###,##0}, {2:###,##0.0} MB Done, Elapsed {3}, Estimated Total {4}",
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
            m_progressBarMain.State = Windows7ProgressBar.ProgressBarState.Pause;
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
            m_txtStatus.Text = "Found " + _backupFilesList.Count.ToString("###,###,##0 ") + status + " files to Backup";
        }

        private void AbortCalculateSpaceNeededTask()
        {
            _calculateSpaceDifferenceTask.Abort();
        }

        private void StartCalculateSpaceNeededTask(BackupStatus backupStatus)
        {
            AbortCalculateSpaceNeededTask();

            m_txtInfo.Text = "Preparing to Calculate Space Needed...";
            m_btnAbort.Enabled = true;
            m_progressFile.ColorTheme.Part1_ActiveColor = Color.SkyBlue;

            _calculateSpaceDifferenceTask.Start(_backupFilesList, backupStatus, SelectedOverwriteOption);
        }
    }
}
