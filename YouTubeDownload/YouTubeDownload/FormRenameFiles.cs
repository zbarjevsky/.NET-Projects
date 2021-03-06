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

using MkZ.Tools;

namespace YouTubeDownload
{
    public partial class FormRenameFiles : Form
    {
        private const string DNL_PREFIX = "Working Folder: ";
        private string _folderName = "C:\\Temp\\YouTube";

        public FormRenameFiles(string initialFolder)
        {
            InitializeComponent();

            UpdateOutputFolder(initialFolder);
        }

        private void FormRenameFiles_Load(object sender, EventArgs e)
        {

        }

        private void m_btnBrowseForFolder_Click(object sender, EventArgs e)
        {
            this.BrowseForFolder(ref _folderName, _folderName, "Select Folder to Work on:");
            if(!string.IsNullOrWhiteSpace(_folderName))
                UpdateOutputFolder(_folderName);
        }

        private void m_lnkOutputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_btnBrowseForFolder_Click(sender, e);
        }

        private void UpdateOutputFolder(string newFolder)
        {
            _folderName = newFolder.Trim('\"').Trim('\\');

            m_lnkOutputFolder.Text = DNL_PREFIX + _folderName;
            m_lnkOutputFolder.LinkArea = new LinkArea(DNL_PREFIX.Length, _folderName.Length);

            UpdateFilesList(_folderName);
        }

        private void UpdateFilesList(string folderName)
        {
            m_listFileNames.Items.Clear();

            DirectoryInfo di = new DirectoryInfo(folderName);
            var files = di.GetFiles("*.mp3");
            foreach (var file in files)
            {
                string name = Path.GetFileNameWithoutExtension(file.FullName);
                string ext = Path.GetExtension(file.FullName);

                string[] parts = name.Split(new string[] { "--" }, StringSplitOptions.None);
                TimeSpan dur = TimeSpan.FromSeconds(double.Parse(parts[1]));

                string duration = dur.ToString("mm'-'ss");
                //Debug.WriteLine("Duration: " + duration);

                string newName = string.Format("{0} {1} {2}", parts[0], duration, parts[2]);
                if (newName.Length > 62)
                    newName = newName.Substring(0, 62);
                newName += ext;

                Debug.WriteLine("New Name: " + newName);

                ListViewItem item = m_listFileNames.Items.Add(newName);
                item.SubItems.Add(name);
                //file.MoveTo(Path.Combine(folder, newName));
            }
        }

        private void RenameFiles(string folder)
        {
            DirectoryInfo di = new DirectoryInfo(folder);
            var files = di.GetFiles("*.mp3");
            foreach (var file in files)
            {
                string name = Path.GetFileNameWithoutExtension(file.FullName);
                string ext = Path.GetExtension(file.FullName);

                string[] parts = name.Split(new string[] { "--" }, StringSplitOptions.None);
                TimeSpan dur = TimeSpan.FromSeconds(double.Parse(parts[1]));

                string duration = dur.ToString("mm'-'ss");
                //Debug.WriteLine("Duration: " + duration);

                string newName = string.Format("{0} {1} {2}", parts[0], duration, parts[2]);
                if (newName.Length > 62)
                    newName = newName.Substring(0, 62);
                newName += ext;

                Debug.WriteLine("New Name: " + newName);
                //file.MoveTo(Path.Combine(folder, newName));
            }
        }

        private void m_btnApply_Click(object sender, EventArgs e)
        {

        }

        private void m_btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
