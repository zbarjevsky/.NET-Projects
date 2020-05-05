using SmartBackup.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartBackup
{
    public partial class FormBackupProgress : Form
    {
        BackupGroup _group;

        public FormBackupProgress(BackupGroup group)
        {
            _group = group;

            InitializeComponent();
        }

        private void FormBackupProgress_Load(object sender, EventArgs e)
        {
            m_listFiles.VirtualListSize = 1000000;
        }

        private void FormBackupProgress_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void m_listFiles_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem("" + e.ItemIndex);
            e.Item.SubItems.Add("Bla Bla Bla");
            e.Item.SubItems.Add("Meow Meow Meow");
            e.Item.ImageIndex = 1;
        }
    }
}
