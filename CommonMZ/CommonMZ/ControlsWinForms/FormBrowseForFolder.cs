using MZ.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.ControlsWinForms
{
    public partial class FormBrowseForFolder : Form
    {
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
        }

        private void FormBrowseForFolder_Load(object sender, EventArgs e)
        {
            SelectInTree();
            m_treeFolders.AfterSelectAction = (path) => { m_txtSelectedFolder.Text = path; };
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

        }

        private void SelectInTree()
        {
            m_treeFolders.SelectFolder(m_txtSelectedFolder.Text);
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
            DialogResult res = MessageBox.Show(this,
                "Are you sure to delete: \n" + m_txtSelectedFolder.Text, "Delete Folder",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (res != DialogResult.OK)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                Directory.Delete(m_txtSelectedFolder.Text, true);

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
                this.Cursor = Cursors.Default;
            }
        }
    }
}
