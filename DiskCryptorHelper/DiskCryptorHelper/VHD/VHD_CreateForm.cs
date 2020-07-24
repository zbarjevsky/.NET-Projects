using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using MZ.WPF.MessageBox;

namespace VhdApiExample
{
    public partial class VHD_CreateForm : Form
    {
        public Medo.IO.VirtualDisk Disk;
        Stopwatch _stopwatch = new Stopwatch();
        TimeSpan _creationTime;
        List<double> _listEstimateSec = new List<double>(1024);
        double _averageEstimateSec = 0;

        public VHD_CreateForm()
        {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
        }

        private void VHD_CreateForm_Load(object sender, EventArgs e)
        {
            m_lblStatus.Visible = false;
        }

        private void buttonFileBrowse_Click(object sender, EventArgs e)
        {
            if (saveDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtFileName.Text = saveDialog.FileName;
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            txtFileName.Enabled = false;
            buttonFileBrowse.Enabled = false;
            m_numDiskSize.Enabled = false;
            buttonCreate.Enabled = false;
            m_cnkPreallocate.Enabled = false;
            m_progrCreation.Visible = true;
            m_progrCompletion.Visible = true;
            m_lblStatus.Visible = true;
            _stopwatch.Restart();

            const long GB = 1024 * 1024 * 1024;
            long size = (long)m_numDiskSize.Value * GB;

            Thread thread = new Thread(new ThreadStart(() => { bw_DoWork(txtFileName.Text, size, m_cnkPreallocate.Checked); }));
            thread.Name = "Creating VHD";
            thread.IsBackground = true;
            thread.Start();
        }

        private void bw_DoWork(string fileName, long diskSize, bool preAllocate)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            this.Disk = new Medo.IO.VirtualDisk(fileName);

            Medo.IO.VirtualDiskCreateOptions opt = preAllocate ? 
                Medo.IO.VirtualDiskCreateOptions.FullPhysicalAllocation : Medo.IO.VirtualDiskCreateOptions.None;

            this.Disk.CreateAsync(diskSize, opt);

            try
            {
                var progress = this.Disk.GetCreateProgress();
                while (!progress.IsDone)
                {
                    bw_ProgressChanged(progress.ProgressPercentage);
                    System.Threading.Thread.Sleep(250);
                    progress = this.Disk.GetCreateProgress();
                }
            }
            catch (Exception err)
            {
                PopUp.Error(err.ToString(), Text);
            }
            bw_RunWorkerCompleted();
        }

        private void bw_ProgressChanged(double progress)
        {
            this.Invoke(new MethodInvoker(() => 
            {
                const string fmt = "hh\\:mm\\:ss";
                if (progress > 1 && progress < 100)
                {
                    m_progrCreation.Style = ProgressBarStyle.Continuous;
                    m_progrCreation.Value = (int)progress;
                    m_progrCompletion.Value = 0;

                    //start estimating after 8 seconds up to 120 sec
                    if (_stopwatch.Elapsed.TotalSeconds > 8 && _stopwatch.Elapsed.TotalSeconds < 120) 
                    {
                        double estimate = (int)(_stopwatch.Elapsed.TotalSeconds / (progress / 100.0));
                        _listEstimateSec.Add(estimate);
                        _averageEstimateSec = _listEstimateSec.Average();
                    }
                }
                else
                {
                    m_progrCreation.Style = ProgressBarStyle.Marquee;
                }

                if (progress < 100) //until 100%
                    _creationTime = _stopwatch.Elapsed; //time took to reach 100%

                //sometimes it stuck on 100% long time (probably formatting) - estimate progress
                if(progress == 100)
                {
                    double completionProgress = 100.0 * _stopwatch.Elapsed.TotalSeconds / _averageEstimateSec;
                    completionProgress %= 100; //circulating
                    m_progrCompletion.Value = (int)completionProgress;

                    m_lblStatus.Text = string.Format("Progr done at: {0}\n{1}/{2} ({3} %)",
                        _creationTime.ToString(fmt), 
                        _stopwatch.Elapsed.ToString(fmt), TimeSpan.FromSeconds(_averageEstimateSec).ToString(fmt),
                        completionProgress.ToString("0.000"));
                }
                else
                {
                    m_lblStatus.Text = string.Format("{0} ({1} %)\nEstimate: {2}",
                        _stopwatch.Elapsed.ToString(fmt), progress.ToString("0.000"),
                        TimeSpan.FromSeconds(_averageEstimateSec).ToString(fmt));
                }
            }));
        }

        private void bw_RunWorkerCompleted()
        {
            PopUp.Information("VHD Created: " + txtFileName.Text);
            this.DialogResult = DialogResult.OK;
        }
    }
}
