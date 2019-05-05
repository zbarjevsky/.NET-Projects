using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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

        }

        private void m_btnDownload_Click(object sender, EventArgs e)
        {
            string url = m_txtUrl.Text;
            Process _process = YouTube_DL.Create(
                string.Format(" \"{0}\" --no-playlist -o \"{1}\\%(title)s-%(id)s.%(ext)s\"", url, _folderName));

            m_txtOutput.Text = "";

            _process.OutputDataReceived += DL_Process_OutputDataReceived;
            _process.Start();
            _process.BeginOutputReadLine();

            this.Cursor = Cursors.WaitCursor;
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
            this.Cursor = Cursors.Arrow;

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
    }
}
