using ListViewExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClipboardManager
{
	public partial class FormEncodings : Form
	{
		private EncodingsList m_Encodings = null;

        public FormEncodings(EncodingsList encodings)
		{
			m_Encodings = encodings;

			InitializeComponent();
		}//end constructor

		private void FormEncodings_Load(object sender, EventArgs e)
		{
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            m_listEncodings.SetListViewColumnSorter(0, SortOrder.Ascending);

            foreach (EncodingItemData i in m_Encodings.Encodings )
			{
				ListViewItem itm = m_listEncodings.Items.Add(i.EncodingName);
				itm.Tag = i;

				itm.SubItems.Add(i.Enc.CodePage.ToString());
				itm.SubItems.Add(i.Enc.BodyName);
				itm.SubItems.Add(i.Enc.HeaderName);
				itm.SubItems.Add(i.Enc.WebName);

				itm.Checked = i.ShowInMenu;
			}//end foreach

            m_listEncodings.Sort();
        }//end FormEncodings_Load

		private void m_btnOK_Click(object sender, EventArgs e)
		{
			foreach ( ListViewItem itm in m_listEncodings.Items )
			{
                EncodingItemData i = itm.Tag as EncodingItemData;
				i.ShowInMenu = itm.Checked;
			}//end foreach
		}

		private void m_btnCancel_Click(object sender, EventArgs e)
		{

		}
    }//end class FormEncodings
}//end namespace ClipboardListener