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
                    if (list == null)
                    {
                        list = new PlayList();
                        m_tabPlayLists.SelectedTab.Tag = list;
                    }
                    list.List = m_mp3List.GetFilelist();
                }
            };
        }

        public void Initialize(Options opt)
        {
            m_Options = opt;

            m_tabPlayLists.Font = m_Options.PlayListFont;
            m_mp3List.Font = m_Options.PlayListFont;
            m_mp3List.AdjustToNewSize();

            if (HasSameTabs(m_Options.PlayListCollection) && m_tabPlayLists.SelectedIndex == m_Options.PlayListCollection.SelectedIndex)
                return;

            int selectTab = m_Options.PlayListCollection.SelectedIndex;
            m_tabPlayLists.TabPages.Clear();

            for (int i = 0; i < opt.PlayListCollection.Count; i++)
            {
                TabPage tab = new TabPage(opt.PlayListCollection[i].Name);
                tab.Tag = opt.PlayListCollection[i];
                tab.BackColor = Color.WhiteSmoke;
                m_tabPlayLists.TabPages.Add(tab);
            }

            if (m_tabPlayLists.TabPages.Count > 0 && selectTab < m_tabPlayLists.TabPages.Count)
                m_tabPlayLists.SelectedIndex = selectTab;
            m_tabPlayLists_SelectedIndexChanged(this, null);
        }

        private bool HasSameTabs(PlayLists collection)
        {
            if (m_tabPlayLists.TabCount != collection.Count)
                return false;

            for (int i = 0; i < collection.Count; i++)
            {
                PlayList list = m_tabPlayLists.TabPages[i].Tag as PlayList;
                if (list != null && !list.ListEquals(collection[i]))
                    return false;
            }

            return true;
        }

        private void m_tabPlayLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_tabPlayLists.SelectedIndex < 0)
            {
                m_mp3List.RemoveAll();
                return;
            }

            m_Options.PlayListCollection.SelectedIndex = m_tabPlayLists.SelectedIndex;
            PlayList list = m_tabPlayLists.SelectedTab.Tag as PlayList;
            m_mp3List.ReloadList(list, false);
        }

        private void m_btnAddTab_Click(object sender, EventArgs e)
        {
            PlayList newList = new PlayList() { Name = "Play List "+(m_tabPlayLists.TabPages.Count+1) };
            TabPage newPage = new TabPage(newList.Name) { Tag = newList };
            newPage.BackColor = Color.WhiteSmoke;

            m_tabPlayLists.TabPages.Add(newPage);
            m_Options.PlayListCollection.Add(newList);

            //select new page
            m_tabPlayLists.SelectedTab = newPage;
            m_btnEditTab_Click(sender, e);
        }

        private void m_btnDelTab_Click(object sender, EventArgs e)
        {
            if (m_tabPlayLists.SelectedIndex >= 0)
            {
                int newIdx = m_tabPlayLists.SelectedIndex - 1;
                m_Options.PlayListCollection.RemoveAt(m_tabPlayLists.SelectedIndex);
                m_tabPlayLists.TabPages.RemoveAt(m_tabPlayLists.SelectedIndex);

                if (newIdx < 0) newIdx = 0;
                m_tabPlayLists.SelectedIndex = newIdx;
            }
        }

        private void m_btnEditTab_Click(object sender, EventArgs e)
        {
            if (m_tabPlayLists.SelectedIndex >= 0)
            {
                FormInPlaceEdit frm = new FormInPlaceEdit()
                {
                    Font = m_tabPlayLists.Font,
                    EditText = m_tabPlayLists.SelectedTab.Text
                };

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
