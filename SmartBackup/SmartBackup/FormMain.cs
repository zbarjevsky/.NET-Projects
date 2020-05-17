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
using MZ.Controls;
using MZ.ControlsWinForms;

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

        static int _indexNewGroup = 1;
        private void m_btnAdd_Click(object sender, EventArgs e)
        {
            BackupGroup group = new BackupGroup("New Group " + _indexNewGroup++);
            _settings.Add(group);
            ReloadTabs(group);
        }

        private void m_btnRemove_Click(object sender, EventArgs e)
        {
            if (m_tabMain.TabPages.Count > 1)
            {
                int next = m_tabMain.SelectedIndex;
                BackupGroup group = m_tabMain.SelectedTab.Tag as BackupGroup;
                _settings.Remove(group);
                m_tabMain.TabPages.Remove(m_tabMain.SelectedTab);
                if (next >= m_tabMain.TabPages.Count)
                    next--;

                m_tabMain.SelectedIndex = next;
            }
        }

        private void m_btnEdit_Click(object sender, EventArgs e)
        {
            if (m_tabMain.SelectedIndex >= 0)
            {
                Rectangle rect = m_tabMain.GetTabRect(m_tabMain.SelectedIndex);
                Point location = m_tabMain.PointToScreen(rect.Location);
                location.Offset(rect.Height, rect.Height); //image offset

                FormInPlaceEdit.ShowInPlaceEdit(m_tabMain.SelectedTab.Text, m_tabMain.Font, location, this,
                    (text) =>
                    {
                        m_tabMain.SelectedTab.Text = text;
                        BackupGroup group = m_tabMain.SelectedTab.Tag as BackupGroup;
                        group.Name = text;
                    });

                //InPlaceTextBox.ShowTextBox(m_tabMain.SelectedTab.Text, m_tabMain.Font, m_ToolStripMain, new Point(400,10),
                //    (text) =>
                //    {
                //        m_tabMain.SelectedTab.Text = text;
                //        BackupGroup group = m_tabMain.SelectedTab.Tag as BackupGroup;
                //        group.Name = text;
                //    });

            }
        }

        private void m_tabMain_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void m_tabMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < m_tabMain.TabCount; ++i)
            {
                if (m_tabMain.GetTabRect(i).Contains(e.Location))
                {
                    int idx = m_tabMain.SelectedIndex;
                    // Found it, do something
                    //...
                    m_btnEdit_Click(sender, e);
                    break;
                }
            }
        }
    }
}
