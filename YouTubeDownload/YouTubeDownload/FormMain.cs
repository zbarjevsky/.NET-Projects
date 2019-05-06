using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeDownload
{
    public partial class FormMain : Form
    {
        string _folderName = "C:\\Temp\\YouTube2";

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            _folderName = Properties.Settings.Default.OutputFolder;
        }

        private void m_btnDownload_Click(object sender, EventArgs e)
        {
            string url = m_txtUrl.Text;
            Process p = YouTube_DL.Create(
                string.Format(" \"{0}\" --no-playlist -o \"{1}\\%(title)s-%(id)s.%(ext)s\"", url, _folderName));

            m_txtOutput.Text = "";

            p.OutputDataReceived += DL_Process_OutputDataReceived;
            p.Exited += DL_Process_Exited;
            p.Start();
            p.BeginOutputReadLine();

            m_btnDownload.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
        }

        private void DL_Process_Exited(object sender, EventArgs e)
        {
            Process p = sender as Process;
            if (p != null)
            {
                p.OutputDataReceived -= DL_Process_OutputDataReceived;
                p.Exited -= DL_Process_Exited;
            }

            this.BeginInvoke(new MethodInvoker(() => 
            {
                m_btnDownload.Enabled = true;
                Cursor = Cursors.Arrow;
                m_StatusProgress.Value = 0;
                m_Status1.Text = "Done";

                MessageBox.Show(this, "Operation Finished!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }));
        }

        private void DL_Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if(e != null && e.Data != null)
                this.BeginInvoke(new MethodInvoker(() => UpdateOutput(e.Data)));
        }

        private void m_btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(_folderName);
        }

        private void UpdateOutput(string line)
        {
            this.Cursor = Cursors.AppStarting;

            m_txtOutput.Text = (line + "\n") + m_txtOutput.Text;

            m_Status2.Text = line;

            //parse
            int pos1 = line.IndexOf("[download]");
            if(pos1 >= 0)
            {
                int pos2 = line.IndexOf("% of ", pos1 + 10);
                if(pos2 > 0)
                {
                    string sPercent = line.Substring(pos1 + 10, pos2 - (pos1 + 10)).Trim();
                    double percent = double.Parse(sPercent);
                    m_StatusProgress.Value = (int)percent;
                    m_Status1.Text = sPercent;
                }
                else
                {
                    m_Status1.Text = "Done.";
                }
            }
        }

        private void m_btnUpdate_Click(object sender, EventArgs e)
        {
            YouTube_DL.Update();
        }

        private void m_mnuToolsSettings_Click(object sender, EventArgs e)
        {

        }

        private void m_mnuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void m_mnuHelpAbout_Click(object sender, EventArgs e)
        {
            string ver1 = Assembly.GetEntryAssembly().GetName().Version.ToString();
            MessageBox.Show("YouTube Download\nMain ver: "+ver1+"\n" + YouTube_DL.GetVersion().ToString());
        }

        private void m_mnuToolsOutputFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog()
            {
                SelectedPath = _folderName,
                RootFolder = Environment.SpecialFolder.MyComputer
            };

            if(dlg.ShowDialog(this) == DialogResult.OK)
            {
                _folderName = dlg.SelectedPath;
                Properties.Settings.Default.OutputFolder = _folderName;
                Properties.Settings.Default.Save();
            }
        }
    }
}
