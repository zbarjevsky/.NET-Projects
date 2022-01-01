using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            string verDL = YouTubeDownloadEngine.GetVersion(YouTubeDownloadEngine.DL);
            string verDLP = YouTubeDownloadEngine.GetVersion(YouTubeDownloadEngine.DLP);
            string ver3 = YouTubeDownloadEngine.GetVersionFFMpeg();

            m_lblVer1.Text = "YouTube Download ver: " + ver1;
            m_lnkYouTubeDL.Text = "youtube-dl ver: " + verDL;
            m_lnkYouTubeDLP.Text = "yt-dlp ver: " + verDLP;
            m_lnkFFMpeg.Text = "ffmpeg";
            m_txtVer.Text = ver3;
        }

        private void m_lnkYouTubeDL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Process.Start("https://github.com/ytdl-org/youtube-dl/blob/master/README.md#readme");
            Process.Start("https://yt-dl.org/");
        }

        private void m_lnkFFMpeg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://ffmpeg.org/");
        }
    }
}
