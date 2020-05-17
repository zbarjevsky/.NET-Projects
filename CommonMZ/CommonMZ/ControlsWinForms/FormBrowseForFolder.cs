using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            m_treeFolders.OpenFolder = (path) => { m_txtSelectedFolder.Text = path; };
        }

        private void m_btnNewFolder_Click(object sender, EventArgs e)
        {

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
    }
}
