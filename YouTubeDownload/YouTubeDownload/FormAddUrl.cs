using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeDownload
{
    public partial class FormAddUrl : Form
    {
        const string DNL_PREFIX = "Output Folder: ";
        public DownloadData Data { get; } = new DownloadData();

        public FormAddUrl(string outputFolder)
        {
            InitializeComponent();

            Data.OutputFolder = outputFolder;
            m_chkNoPlayList.Checked = true; // Properties.Settings.Default.NoPlayList;
        }

        private void FormAddUrl_Load(object sender, EventArgs e)
        {
            string text = m_txtUrl.Text;
            if (Clipboard.ContainsText(TextDataFormat.Text))
                text = Clipboard.GetText();

            if (IsValidYouTubeUrl(text))
                m_txtUrl.Text = text;

            UpdateOutputFolder(Data.OutputFolder);
            m_cmbFileName.SelectedIndex = 0;
            UpdateButtonsState();
        }

        private void m_btnAddUrl_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.NoPlayList = m_chkNoPlayList.Checked;
            Properties.Settings.Default.Save();

            Uri url = new Uri(m_txtUrl.Text);

            Data.Description = url.PathAndQuery;
            Data.Url = m_txtUrl.Text;
            Data.NoPlayList = m_chkNoPlayList.Checked;
            Data.AudioOnly = m_chkAudioOnly.Checked;
            Data.AdditionalParameters = m_cmbAdditionalParameters.Text;

            if (!string.IsNullOrWhiteSpace(m_cmbFileName.Text))
                Data.FileNameTemplate = m_cmbFileName.Text;
            else
                Data.FileNameTemplate = m_cmbFileName.Items[0].ToString();
        }

        private void m_txtUrl_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void m_btnBrowseForFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog()
            {
                RootFolder = Environment.SpecialFolder.MyComputer,
                SelectedPath = Data.OutputFolder,
                Description = "Select Folder to Download to:"
            };

            //select current folder
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                this.Invoke(new MethodInvoker(() => SendKeys.Send("{TAB}{TAB}{DOWN}{DOWN}{UP}{UP}")));
            });

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                UpdateOutputFolder(dlg.SelectedPath);
            }
        }

        private void m_lnkOutputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Data.OutputFolder);
        }

        private void m_lnkOutputFileName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ytdl-org/youtube-dl/blob/master/README.md#output-template-examples");
        }

        private void UpdateOutputFolder(string newFolder)
        {
            Data.OutputFolder = newFolder;
            Properties.Settings.Default.OutputFolder = Data.OutputFolder;
            Properties.Settings.Default.Save();

            m_lnkOutputFolder.Text = DNL_PREFIX + Data.OutputFolder;
            m_lnkOutputFolder.LinkArea = new LinkArea(DNL_PREFIX.Length, Data.OutputFolder.Length);
        }

        private void UpdateButtonsState()
        {
            m_btnAddUrl.Enabled = IsValidYouTubeUrl(m_txtUrl.Text);
        }

        private bool IsValidYouTubeUrl(string text)
        {
            Uri url = null;
            try { url = new Uri(text); } catch { }

            if (url == null || string.IsNullOrWhiteSpace(url.Host)) // != "www.youtube.com")
            {
                m_errorProvider.SetError(m_txtUrl, "invalid url");
                m_errorProvider.SetIconAlignment(m_txtUrl, ErrorIconAlignment.MiddleRight);
                return false;
            }
            else
            {
                m_errorProvider.SetError(m_txtUrl, "");
                return true;
            }
        }
    }
}
