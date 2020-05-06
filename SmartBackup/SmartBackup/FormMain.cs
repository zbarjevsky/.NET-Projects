using MarkZ.Controls;
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
    public partial class FormMain : Form
    {
        private readonly BackupSettings _settings;
        public const string TITLE = "Smart Backup MZ";

        public FormMain()
        {
            InitializeComponent();

            _settings = BackupSettings.Load();
            ReloadTabs();

            this.Text = TITLE;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _settings.Save();
        }

        private void ReloadTabs(BackupGroup group = null)
        {
            m_tabMain.TabPages.Clear();
            foreach (BackupGroup item in _settings.BackupGroups)
            {
                TabPage page = new TabPage(item.Name);
                page.Tag = item;
                page.ImageIndex = 5;
                BackupListUserControl ctrl = new BackupListUserControl(item);
                ctrl.Dock = DockStyle.Fill;
                page.Controls.Add(ctrl);
                m_tabMain.TabPages.Add(page);

                if (group != null && item.Name == group.Name)
                {
                    m_tabMain.SelectTab(page);
                }
            }
        }

        private void m_btnAdd_Click(object sender, EventArgs e)
        {
            BackupGroup group = new BackupGroup("New Group");
            _settings.Add(group);
            ReloadTabs(group);
        }

        private void m_btnRemove_Click(object sender, EventArgs e)
        {
            if (m_tabMain.TabPages.Count > 1)
                m_tabMain.TabPages.Remove(m_tabMain.SelectedTab);
        }

        private void m_btnEdit_Click(object sender, EventArgs e)
        {
            if (m_tabMain.SelectedIndex >= 0)
            {
                FormInPlaceEdit frm = new FormInPlaceEdit()
                {
                    Font = m_tabMain.Font,
                    EditText = m_tabMain.SelectedTab.Text
                };

                Point location = m_tabMain.PointToScreen(m_tabMain.GetTabRect(m_tabMain.SelectedIndex).Location);
                location.Offset(4, 16);
                frm.Location = location;
                frm.OkAction = (text) =>
                {
                    m_tabMain.SelectedTab.Text = frm.EditText;
                    BackupGroup group = m_tabMain.SelectedTab.Tag as BackupGroup;
                    group.Name = frm.EditText;
                };
                frm.Show(this);
            }

        }
    }
}
