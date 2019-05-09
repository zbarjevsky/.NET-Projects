using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeDownload
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            string ver1 = Assembly.GetEntryAssembly().GetName().Version.ToString();
            string ver2 = YouTube_DL.GetVersion().ToString();
            string ver3 = YouTube_DL.GetVersionFFMpeg().ToString();

            m_lblVer.Text = "YouTube Download ver: " + ver1;
            m_lnkYouTubeDL.Text = "youtube-dl ver: " + ver2;
            m_lnkFFMpeg.Text = "ffmpeg ver: " + ver3;
        }
    }
}
