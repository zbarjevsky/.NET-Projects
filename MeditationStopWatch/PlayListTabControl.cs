using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeditationStopWatch
{
    public partial class PlayListTabControl : UserControl
    {
        private Options m_Options;

        public FileListControl PL { get { return m_mp3List; } }

        public PlayListTabControl()
        {
            InitializeComponent();
        }

        private void PlayListTabControl_Load(object sender, EventArgs e)
        {
            m_mp3List.OP.OnListChanged += () =>
            {
                if (m_tabPlayLists.SelectedIndex >= 0)
                {
                    PlayList list = m_tabPlayLists.SelectedTab.Tag as PlayList;
                    list.List = m_mp3List.GetFilelist();
                }
            };
        }

        public void Initialize(Options opt)
        {
            m_Options = opt;

            m_tabPlayLists.TabPages.Clear();
            m_tabPlayLists.SelectedIndex = -1;

            for (int i = 0; i < opt.PlayListCollection.Count; i++)
            {
                TabPage tab = new TabPage(opt.PlayListCollection[i].Name);
                tab.Tag = opt.PlayListCollection[i];
                tab.BackColor = Color.WhiteSmoke;
                m_tabPlayLists.TabPages.Add(tab);
            }

            if (m_tabPlayLists.TabPages.Count > 0)
                m_tabPlayLists.SelectedIndex = 0;
            m_tabPlayLists_SelectedIndexChanged(this, null);
        }

        private void m_tabPlayLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_tabPlayLists.SelectedIndex < 0)
            {
                m_mp3List.RemoveAll();
                return;
            }

            PlayList list = m_tabPlayLists.SelectedTab.Tag as PlayList;
            m_mp3List.ReloadList(list.List.ToArray(), false);
        }

        private void m_btnAddTab_Click(object sender, EventArgs e)
        {
            PlayList newList = new PlayList() { Name = "List"+m_tabPlayLists.TabPages.Count };
            TabPage newPage = new TabPage(newList.Name) { Tag = newList };
            newPage.BackColor = Color.WhiteSmoke;

            m_tabPlayLists.TabPages.Add(newPage);
            m_Options.PlayListCollection.Add(newList);
        }

        private void m_btnDelTab_Click(object sender, EventArgs e)
        {
            if (m_tabPlayLists.SelectedIndex >= 0)
            {
                m_tabPlayLists.TabPages.RemoveAt(m_tabPlayLists.SelectedIndex);
                m_Options.PlayListCollection.RemoveAt(m_tabPlayLists.SelectedIndex);

                int newIdx = m_tabPlayLists.SelectedIndex - 1;
                if (newIdx < 0) newIdx = 0;
                m_tabPlayLists.SelectedIndex = newIdx;
            }
        }

        private void m_btnEditTab_Click(object sender, EventArgs e)
        {
            if (m_tabPlayLists.SelectedIndex >= 0)
            {
                FormInPlaceEdit frm = new FormInPlaceEdit() { EditText = m_tabPlayLists.SelectedTab.Text };
                Point location = m_tabPlayLists.PointToScreen(m_tabPlayLists.GetTabRect(m_tabPlayLists.SelectedIndex).Location);
                location.Offset(4, 16);
                frm.Location = location;
                frm.OkAction = (text) =>
                {
                    m_tabPlayLists.SelectedTab.Text = frm.EditText;
                    m_Options.PlayListCollection[m_tabPlayLists.SelectedIndex].Name = frm.EditText;
                };
                frm.Show(this);
            }
        }
    }
}
