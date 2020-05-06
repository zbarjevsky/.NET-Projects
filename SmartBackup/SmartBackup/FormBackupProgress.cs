using MarkZ.Tools;
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

        private void m_btnStart_Click(object sender, EventArgs e)
        {
            _abort = false;
            m_btnStart.Enabled = false;
            Stopwatch stopper = new Stopwatch();

            stopper.Start();
            for (int i = 0; i < _logic.FileList.Count; i++)
            {
                if (_abort)
                    break;

                _logic.FileList[i].PerformBackup(m_progrFile);

                if(i%33 == 0)
                {
                    m_progressBar1.Value = (int)m_progressBar1.Maximum * i / _logic.FileList.Count;
                    m_listFiles.EnsureVisible(i);
                    TimeSpan estimate = TimeSpan.FromMilliseconds(m_progressBar1.Maximum * stopper.ElapsedMilliseconds / (m_progressBar1.Value+1));
                    m_txtStatus.Text = string.Format("Backing up file {0:###,###} of {1:###,###}, Time {2}, Estimated {3}", 
                        i, _logic.FileList.Count, stopper.Elapsed, estimate);
                    Application.DoEvents();
                }
            }

            stopper.Stop();
            SystemSounds.Beep.Play();
            m_progressBar1.Value = 0;
            m_btnStart.Enabled = true;
        }

        bool _abort = false;
        private void m_btnAbort_Click(object sender, EventArgs e)
        {
            _abort = true;
        }

        private void m_btnPause_Click(object sender, EventArgs e)
        {
            _abort = true;
        }

        private void m_btnContinue_Click(object sender, EventArgs e)
        {

        }
    }
}
