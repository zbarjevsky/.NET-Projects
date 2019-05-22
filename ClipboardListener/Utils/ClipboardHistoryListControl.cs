using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardManager.Utils
{
    public partial class ClipboardHistoryListControl : UserControl
    {
        private List<ClipboardList.ClipboardEntry> _history = new List<ClipboardList.ClipboardEntry>();

        public Action<ClipboardList.ClipboardEntry> AddToFavorites = (clp) => { };
        public Action<ClipboardList.ClipboardEntry> RemoveFromMain = (clp) => { };
        public Action<ClipboardList.ClipboardEntry> SelectMainEntry = (clp) => { };

        public ClipboardHistoryListControl()
        {
            InitializeComponent();
        }

        private void ClipboardHistoryListControl_Load(object sender, EventArgs e)
        {
            this.m_listHistory.SetDoubleBuffered(true);
        }

        private void ClipboardHistoryListView_DoubleClick(object sender, EventArgs e)
        {
            if (this.m_listHistory.SelectedIndices.Count == 0)
                return;
            _history[m_listHistory.SelectedIndices[0]].Put();
        }

        private void ClipboardHistoryListView_SizeChanged(object sender, EventArgs e)
        {
            if(this.m_listHistory.Columns.Count > 2)
                this.m_listHistory.Columns[2].Width = -1; 
        }

        public void UpdateHistoryList(ClipboardList log, bool update = true)
        {
            if (update)
            {
                _history = new List<ClipboardList.ClipboardEntry>(log.Count);
                for (int i = 0; i < log.Count; i++)
                {
                    _history.Add(log.GetEntry(i));
                }

                this.m_listHistory.VirtualListSize = log.Count;
                this.m_listHistory.SelectedIndices.Clear();
                if (_history.Count > 0)
                    this.m_listHistory.EnsureVisible(0);

                this.m_listHistory.Columns[2].Width = -1;
                this.m_listHistory.Refresh();
            }
        }

        private void ClipboardHistoryListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex < _history.Count)
            {
                string text = _history[e.ItemIndex].ToString();
                e.Item = new ListViewItem("", _history[e.ItemIndex]._icoItemType);
                e.Item.SubItems.Add(text.Length.ToString());
                e.Item.SubItems.Add(text.Trim().Replace("\\n", " ").Replace("\\r", ""));
                e.Item.ToolTipText = text;
            }
        }

        private void m_txtSearch_TextChanged(object sender, EventArgs e)
        {
            Find(m_txtSearch.Text);
        }

        private int _searchIndex = -1;
        private void m_btnFindNext_Click(object sender, EventArgs e)
        {
            Find(m_txtSearch.Text, _searchIndex + 1);
        }

        private void Find(string txt, int startIdx = 0)
        {
            m_listHistory.SelectedIndices.Clear();
            txt = txt.ToLower();
            if (string.IsNullOrWhiteSpace(txt))
                return;

            for (int i = startIdx; i < _history.Count; i++)
            {
                if (_history[i].ToString().ToLower().Contains(txt))
                {
                    _searchIndex = i;
                    m_listHistory.SelectedIndices.Add(i);
                    m_listHistory.EnsureVisible(i);
                    m_listHistory.Refresh();
                    m_errorProvider.SetError(m_txtSearch, "");
                    m_errorProvider.SetIconAlignment(m_txtSearch, ErrorIconAlignment.MiddleRight);
                    return;
                }
            }

            _searchIndex = -1;
            m_errorProvider.SetError(m_txtSearch, "Text not found: " + txt);
        }

        private void m_contextMenuStrip_ClipboardEntry_AddToFavorites_Click(object sender, EventArgs e)
        {
            if (this.m_listHistory.SelectedIndices.Count == 0)
                return;

            AddToFavorites(_history[m_listHistory.SelectedIndices[0]]);
        }

        private void m_contextMenuStrip_ClipboardEntry_Edit_Click(object sender, EventArgs e)
        {
            if (this.m_listHistory.SelectedIndices.Count == 0)
                return;

            SelectMainEntry(_history[m_listHistory.SelectedIndices[0]]);
        }

        private void m_contextMenuStrip_ClipboardEntry_Remove_Click(object sender, EventArgs e)
        {
            if (this.m_listHistory.SelectedIndices.Count == 0)
                return;

            RemoveFromMain(_history[m_listHistory.SelectedIndices[0]]);
        }
    }
}
