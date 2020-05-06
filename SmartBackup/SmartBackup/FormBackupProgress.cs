using MarkZ.Tools;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartBackup
{
    public partial class FormBackupProgress : Form
    {
        BackupGroup _group;
        BackupLogic _logic;

        public FormBackupProgress(BackupGroup group)
        {
            _group = group;

            InitializeComponent();

            m_listFiles.SetDoubleBuffered(true);
            m_cmbOption.SelectedIndex = 0;
        }

        private void FormBackupProgress_Load(object sender, EventArgs e)
        {
            _logic = new BackupLogic(_group);
            m_listFiles.VirtualListSize = _logic.FileList.Count;
            m_txtStatus.Text = "Prepared " + _logic.FileList.Count.ToString("###,###,###") + " files for Backup";
        }

        private void FormBackupProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_listFiles.VirtualListSize = 0;
            _logic.FileList.Clear();
            GC.Collect();
        }

        private void m_listFiles_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem("" + e.ItemIndex);
            e.Item.SubItems.Add(_logic.FileList[e.ItemIndex].Src);
            e.Item.SubItems.Add(_logic.FileList[e.ItemIndex].Dst);
            e.Item.SubItems.Add((_logic.FileList[e.ItemIndex].SrcIfo.Length/1024.0).ToString("###,##0.0 k"));
            e.Item.ImageIndex = (int)_logic.FileList[e.ItemIndex].Status;
        }

        private void PerformBackup(int startIndex)
        {
            _abort = false;
            _pause = false;

            Stopwatch stopper = new Stopwatch();
            stopper.Start();

            Task task = new Task(() =>
            {
                for (int i = startIndex; i < _logic.FileList.Count; i++)
                {
                    if (_abort || _pause)
                        break;

                    _startIndex = i;
                    _logic.FileList[i].PerformBackup(m_progrFile, this);

                    if (i % 33 == 0)
                    {
                        _elapsed += stopper.Elapsed;
                        stopper.Restart();
                        UpdateUI(true);
                    }
                }

                stopper.Stop();
                SystemSounds.Beep.Play();
                UpdateUI(false);
            });
            task.Start();
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

                m_progressBar1.Value = (int)((long)m_progressBar1.Maximum * (long)_startIndex / _logic.FileList.Count);
                m_listFiles.EnsureVisible(_startIndex);
                TimeSpan estimate = TimeSpan.FromMilliseconds(m_progressBar1.Maximum * _elapsed.TotalMilliseconds / (m_progressBar1.Value + 1));
                m_txtStatus.Text = string.Format("Backing up file {0:###,###} of {1:###,###}, Time {2}, Estimated {3}",
                    _startIndex, _logic.FileList.Count, _elapsed, estimate);

                if (_abort)
                    m_progressBar1.Value = 0;
            }, this);
        }

        private int _startIndex = 0;
        private void m_btnStart_Click(object sender, EventArgs e)
        {
            _elapsed = TimeSpan.FromSeconds(0);
            PerformBackup(0);
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
            PerformBackup(_startIndex);
        }
    }
}
