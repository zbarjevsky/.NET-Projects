using MZ.Tools.WinForms;
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
        BackupGroup _group;
        BackupLogic _logic;
        readonly BackupPriority _Priority = BackupPriority.All;

        public FormBackupProgress(BackupGroup group, BackupPriority priority)
        {
            _group = group;
            _Priority = priority;

            InitializeComponent();

            m_listFiles.SetDoubleBuffered(true);

            m_cmbOption.Items.Clear();
            m_cmbOption.Items.AddRange(Enum.GetNames(typeof(BackupOptions)).ToArray());
            m_cmbOption.SelectedIndex = 1;
        }

        private void FormBackupProgress_Load(object sender, EventArgs e)
        {
            _logic = new BackupLogic(_group, _Priority);
            m_listFiles.VirtualListSize = _logic.FileList.Count;
            m_txtStatus.Text = "Found " + _logic.FileList.Count.ToString("###,###,###") + " files to Backup";

            m_txtInfo.Text = "Loading...";
            Task task = new Task(() => 
            { 
                string stat = _logic.GetDiskStatistics();
                long size = _logic.CalculateSpaceNeeded();
                Utils.ExecuteOnUIThread(() => 
                {
                    m_txtInfo.Text = string.Format("{0} - Estimated Space Needed: {1:###,##0.0} MB", stat, size/BackupLogic.i1MB);
               }, this);
            });
            task.Start();
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
            _logic.FileList.Clear();
            GC.Collect();
        }

        private void m_listFiles_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            BackupFile file = _logic.FileList[e.ItemIndex];

            e.Item = new ListViewItem(e.ItemIndex + ". " + file.Err);
            e.Item.SubItems.Add(file.Src);
            e.Item.SubItems.Add(file.Dst);
            e.Item.SubItems.Add((file.SrcIfo.Length/1024.0).ToString("###,##0.0 k"));
            e.Item.ImageIndex = (int)file.Status;
        }

        bool _working = false;
        private void PerformBackup(int startIndex, BackupOptions options)
        {
            _abort = false;
            _pause = false;
            _working = true;

            Stopwatch stopper = new Stopwatch();
            stopper.Start();

            Thread threadBackup = new Thread(() =>
            {
                for (int i = startIndex; i < _logic.FileList.Count; i++)
                {
                    if (_abort || _pause)
                        break;

                    _startIndex = i;
                    BackupFile file = _logic.FileList[i];
                    if (file.IsBigFile())
                    {
                        Utils.ExecuteOnUIThread(() =>
                        {
                            file.Status = BackupStatus.InProgress;
                            m_listFiles.EnsureVisible(_startIndex);
                        }, this);
                    }

                    if (file.PerformBackup(m_progrFile, this, options) == BackupStatus.Error)
                    {
                        Thread.Sleep(10);
                        file.PerformBackup(m_progrFile, this, options);
                    }

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
            Utils.ExecuteOnUIThread(() =>
            {
                m_btnStart.Enabled = !isRunning;
                m_btnContinue.Enabled = !isRunning;
                m_btnAbort.Enabled = isRunning;
                m_btnPause.Enabled = isRunning;

                m_progressBar1.Value = (int)((long)m_progressBar1.Maximum * (long)(_startIndex+1) / _logic.FileList.Count);
                m_listFiles.EnsureVisible(_startIndex);
                TimeSpan estimate = TimeSpan.FromMilliseconds(m_progressBar1.Maximum * _elapsed.TotalMilliseconds / (m_progressBar1.Value + 1));

                m_txtStatus.Text = string.Format("Backing up file {0:###,###} of {1:###,###}, Elapsed {2}, Total {3}",
                    (_startIndex + 1), _logic.FileList.Count, _elapsed.ToString("mm':'ss"), estimate.ToString("mm':'ss"));

                if (_abort)
                    m_progressBar1.Value = 0;
            }, this);
        }

        private void UpdateBackupStatus()
        {
            Utils.ExecuteOnUIThread(() =>
            {
                int errCount = 0;
                for (int i = 0; i < _logic.FileList.Count; i++)
                {
                    if (_logic.FileList[i].Status == BackupStatus.Error)
                        errCount++;
                }

                m_txtStatus.Text = string.Format("Backup status done: {0:###,##0} of {1:###,##0}, Errors: {2:###,##0}, Elapsed {3}",
                    (_startIndex + 1), _logic.FileList.Count, errCount, _elapsed.ToString("mm':'ss"));
            }, this);
        }

        private int _startIndex = 0;
        private void m_btnStart_Click(object sender, EventArgs e)
        {
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
        }

        private void m_btnContinue_Click(object sender, EventArgs e)
        {
            BackupOptions options = (BackupOptions)Enum.Parse(typeof(BackupOptions), m_cmbOption.SelectedItem.ToString());
            PerformBackup(_startIndex, options);
        }
    }
}
