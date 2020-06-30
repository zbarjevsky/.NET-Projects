using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClipboardManager
{
	public partial class FormFavorites : Form
	{
		private ClipboardList m_ClipboardFavorites = null;

		public FormFavorites(ClipboardList list, ImageList imageList)
		{
			m_ClipboardFavorites = list;

			InitializeComponent();

			m_listFavorites.SmallImageList = imageList;
		}//end constructor

		private void FormFavorites_Load(object sender, EventArgs e)
		{
			m_listFavorites_SizeChanged(sender, e);
			for (int i = 0; i < m_ClipboardFavorites.Count; i++)
			{
				ClipboardEntryLogic entry = m_ClipboardFavorites.GetEntry(i);
				string sText = entry.ShortDesc();
				int ico = entry._icoItemType;

				ListViewItem itm = m_listFavorites.Items.Add(sText, ico);
				itm.Tag = entry;
			}//end for
			m_listFavorites_SelectedIndexChanged(null, null);
		}//end FormFavorites_Load

		private void m_btnCancel_Click(object sender, EventArgs e)
		{

		}//end m_btnCancel_Click

		private void m_btnRemove_Click(object sender, EventArgs e)
		{
			if (m_listFavorites.SelectedItems.Count == 0)
				return;

			for (int i = m_listFavorites.SelectedItems.Count - 1; i >= 0; i--)
			{
				ListViewItem itm = m_listFavorites.SelectedItems[i];
				ClipboardEntryLogic entry = (ClipboardEntryLogic)itm.Tag;
				int idx = m_ClipboardFavorites.FindEntry(entry);
				m_ClipboardFavorites.RemoveAt(idx);
				itm.Remove();
			}//end for
		}//end m_btnRemove_Click

		private void m_btnUp_Click(object sender, EventArgs e)
		{
			MoveSelectedItem(true);
		}//end m_btnUp_Click

		private void m_btnDown_Click(object sender, EventArgs e)
		{
			MoveSelectedItem(false);
		}//end m_btnDown_Click

		private void MoveSelectedItem(bool bUp)
		{
			if (m_listFavorites.SelectedItems.Count == 0)
				return;

			m_listFavorites.BeginUpdate();
			ListViewItem itm = m_listFavorites.SelectedItems[0];
			ClipboardEntryLogic entry = (ClipboardEntryLogic)itm.Tag;
			int idx = m_ClipboardFavorites.FindEntry(entry);
			m_ClipboardFavorites.RemoveAt(idx);
			itm.Remove();

			int nextIdx = bUp ? idx - 1 : idx + 1;

			m_listFavorites.Items.Insert(nextIdx, itm);
			m_ClipboardFavorites.Insert(nextIdx, entry);
			m_listFavorites.EndUpdate();
		}//end MoveSelectedItem

		private void m_listFavorites_SizeChanged(object sender, EventArgs e)
		{
			m_columnHeader1.Width = m_listFavorites.Width - 22;
		}//end m_listFavorites_SizeChanged

		private void m_listFavorites_SelectedIndexChanged(object sender, EventArgs e)
		{
			int max = m_listFavorites.Items.Count;
			int sel = m_listFavorites.SelectedIndices.Count;

			bool bEnableRemove = sel>0;
			if (bEnableRemove != m_btnRemove.Enabled)
				m_btnRemove.Enabled = bEnableRemove; 

			bool bEnableUp = sel == 1 && m_listFavorites.SelectedIndices[0] > 0;
			if (m_btnUp.Enabled != bEnableUp)
				m_btnUp.Enabled = bEnableUp;

			bool bEnableDown = sel == 1 && m_listFavorites.SelectedIndices[0] < max - 1;
			if (m_btnDown.Enabled != bEnableDown)
				m_btnDown.Enabled = bEnableDown;
		}//end m_listFavorites_SelectedIndexChanged

		private void m_listFavorites_DoubleClick(object sender, EventArgs e)
		{
			if ( m_listFavorites.SelectedItems.Count == 0 )
				return;

			try
			{
				ClipboardEntryLogic entry = (ClipboardEntryLogic)m_listFavorites.SelectedItems[0].Tag;
				entry.Put();
			}//end try
			catch ( Exception err )
			{
				System.Diagnostics.Trace.WriteLine("m_listFavorites_DoubleClick::Error: " + err.Message);
				FormClipboard.TraceLn(true, "FormFavorites", "m_listFavorites_DoubleClick",
					"Error: {0}", err.Message);
			}//end catch
			this.Close();
		}
	}//end class FormFavorites
}//end namespace ClipboardListener