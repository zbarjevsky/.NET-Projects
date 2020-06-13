using MZ.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.WinForms
{
    public partial class FormBrowseForFolder : Form
    {
        FileUtils.FileProgress _fileProgress;

        public string SelectedFolder 
        { 
            get { return m_txtSelectedFolder.Text; } 
            set { m_txtSelectedFolder.Text = value; SelectInTree(); }
        }

        public string Description 
        { 
            get { return m_txtDescription.Text; } 
            set { m_txtDescription.Text = value; }
        }

        public FormBrowseForFolder()
        {
            InitializeComponent();

            _fileProgress = new FileUtils.FileProgress(m_progressBar, this);
        }

        private void FormBrowseForFolder_Load(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Cursor = Cursors.WaitCursor;
            EnableControls(false, "Loading, Please wait...");
            m_btnCancel.Enabled = false;
            Application.DoEvents();

            SelectInTree();
            m_treeFolders.AfterSelectAction = (path) => { m_txtSelectedFolder.Text = path; };

            EnableControls(true, "");
            this.Cursor = Cursors.Default;
            _fileProgress.OnChange = (status) => { m_lblMessage.Text = status; };
        }

        private void m_btnNewFolder_Click(object sender, EventArgs e)
        {
            string oldFolder = m_txtSelectedFolder.Text;
            string newFolder = Path.Combine(oldFolder, "NewFolder");
            Directory.CreateDirectory(newFolder);
            m_treeFolders.RefreshFolder(oldFolder); //load new folder
            m_treeFolders.SelectFolder(newFolder);
            m_treeFolders.EditFolder(newFolder);
            m_txtSelectedFolder.Text = newFolder;
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {

        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            _fileProgress.Cancel = true;
        }

        private void SelectInTree()
        {
            m_treeFolders.SelectFolder(m_txtSelectedFolder.Text);
        }

        private void m_mnuSelect_Click(object sender, EventArgs e)
        {
            m_btnOk_Click(sender, e);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void m_mnuRefresh_Click(object sender, EventArgs e)
        {
            m_treeFolders.RefreshFolder(m_txtSelectedFolder.Text);
        }

        private void m_mnuRename_Click(object sender, EventArgs e)
        {
            m_treeFolders.EditFolder(m_txtSelectedFolder.Text);
        }

        private void m_mnuDelete_Click(object sender, EventArgs e)
        {
            //MZ.WPF.MessageBox.PopUp.PopUpResult res = MZ.WPF.MessageBox.PopUp.Question(
            //    "Confirm folder delete: \n" + m_txtSelectedFolder.Text, "Delete Folder");
            //if (res != WPF.MessageBox.PopUp.PopUpResult.OK)
            //    return;

            DialogResult res = MessageBox.Show(this,
                "Are you sure to delete: \n" + m_txtSelectedFolder.Text, "Delete Folder",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (res != DialogResult.OK)
                return;

            try
            {
                this.Cursor = Cursors.AppStarting;

                string prompt = "Delteting folder... \n(" + m_txtSelectedFolder.Text + ") \n";
                EnableControls(false, prompt);

                _fileProgress.ResetToMarquee(prompt);

                List<string> files = FileUtils.GetFiles(m_txtSelectedFolder.Text, "*.*", SearchOption.AllDirectories, _fileProgress).ToList();
                if (_fileProgress.Cancel)
                    return;

                if (files.Count > 0)
                {
                    _fileProgress.ResetToBlocks(prompt, files.Count, 0);
                    for (int i = 0; i < files.Count; i++)
                    {
                        if (_fileProgress.Cancel)
                            return;

                        _fileProgress.Value++;
                        File.Delete(files[i]);
                    }
                }

                //delete empty folders
                Directory.Delete(m_txtSelectedFolder.Text, true);

                if (_fileProgress.Elapsed.TotalSeconds > 10)
                    SystemSounds.Beep.Play();

                string parent = Path.GetDirectoryName(m_txtSelectedFolder.Text);
                m_treeFolders.RefreshFolder(parent);
            }
            catch (Exception err)
            {
                MessageBox.Show(this,
                    "Cannot delete: \n" + m_txtSelectedFolder.Text + "\nError: " + err.Message, 
                    "Delete Folder",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            } 
            finally
            {
                m_progressBar.Value = 0;
                EnableControls(true);
                this.Cursor = Cursors.Default;
            }
        }

        private void EnableControls(bool bEnable, string message = "Wait...")
        {
            m_btnRefresh.Enabled = bEnable;
            m_btnNewFolder.Enabled = bEnable;
            m_btnCancel.Enabled = true; //bEnable;
            m_btnOk.Enabled = bEnable;
            m_mnuRefresh.Enabled = bEnable;
            m_mnuRename.Enabled = bEnable;
            m_mnuDelete.Enabled = bEnable;
            m_treeFolders.Enabled = bEnable;

            m_lblMessage.Visible = !bEnable;
            m_lblMessage.Text = message;
        }
    }
}
