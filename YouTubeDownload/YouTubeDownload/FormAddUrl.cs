using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;


using MkZ.Windows;

namespace YouTubeDownload
{
    public partial class FormAddUrl : Form
    {
        const string DNL_PREFIX = "Output Folder: ";
        public DownloadData Data { get; } = new DownloadData();

        public FormAddUrl(string outputFolder, int selectedEngineIndex)
        {
            InitializeComponent();

            m_cmbEngine.Items.Clear();
            m_cmbEngine.Items.AddRange(YouTubeDownloadEngine.ENGINES);
            m_cmbEngine.SelectedIndex = selectedEngineIndex;

            Data.SelectedEngineIndex = selectedEngineIndex;
            Data.OutputFolder = outputFolder;
            Data.NoPlayList = true; // Properties.Settings.Default.NoPlayList;

            try { Data.Url = (string)Clipboard.GetData(DataFormats.UnicodeText.ToString()); }
            catch { }
            
            UpdateData(true);
        }

        public FormAddUrl(DownloadData data)
        {
            InitializeComponent();

            m_cmbEngine.Items.Clear();
            m_cmbEngine.Items.AddRange(YouTubeDownloadEngine.ENGINES);
            m_cmbEngine.SelectedIndex = data.SelectedEngineIndex;

            m_btnAddUrl.Text = "Update && &Start";
            Data = data.Clone();
            UpdateData(true);
        }

        private void FormAddUrl_Load(object sender, EventArgs e)
        {
            string text = m_txtUrl.Text;

            if (IsValidUrl(text))
                m_txtUrl.Text = text;

            UpdateOutputFolder(Data.OutputFolder);
            UpdateButtonsState();
        }

        private void m_btnAddUrl_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.NoPlayList = m_chkNoPlayList.Checked;
            Properties.Settings.Default.Save();

            UpdateData(false);
        }

        private void m_txtUrl_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void m_btnBrowseForFolder_Click(object sender, EventArgs e)
        {
            string selectedPath = Data.OutputFolder;
            this.BrowseForFolder(ref selectedPath, Data.OutputFolder);
            UpdateOutputFolder(selectedPath);
        }

        private void m_lnkOutputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Data.OutputFolder);
        }

        private void m_lnkOutputFileName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ytdl-org/youtube-dl/blob/master/README.md#output-template-examples");
        }

        private void UpdateData(bool fromData)
        {
            if(fromData)
            {
                m_txtUrl.Text = Data.Url;
                m_chkNoPlayList.Checked = Data.NoPlayList;
                m_chkAudioOnly.Checked = Data.AudioOnly;
                m_cmbAdditionalParameters.Text = Data.AdditionalParameters;
                m_cmbEngine.SelectedIndex = Data.SelectedEngineIndex;
                UpdateFileName(Data.FileNameTemplate);
            }
            else
            {
                Uri url = new Uri(m_txtUrl.Text);

                Data.Description = url.PathAndQuery;
                Data.Url = m_txtUrl.Text;
                Data.NoPlayList = m_chkNoPlayList.Checked;
                Data.AudioOnly = m_chkAudioOnly.Checked;
                Data.AdditionalParameters = m_cmbAdditionalParameters.Text;
                Data.SelectedEngineIndex = m_cmbEngine.SelectedIndex;

                if (!string.IsNullOrWhiteSpace(m_cmbFileNameTemplate.Text))
                    Data.FileNameTemplate = m_cmbFileNameTemplate.Text;
                else
                    Data.FileNameTemplate = m_cmbFileNameTemplate.Items[0].ToString();
            }
        }

        private void UpdateFileName(string format)
        {
            List<string> items = new List<string>(m_cmbFileNameTemplate.Items.OfType<string>());
            int idx = items.IndexOf(format);
            if (idx < 0)
                idx = m_cmbFileNameTemplate.Items.Add(format);
            m_cmbFileNameTemplate.SelectedIndex = idx;
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
            m_btnAddUrl.Enabled = IsValidUrl(m_txtUrl.Text);
        }

        private bool IsValidUrl(string text)
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
