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
			foreach (EncodingItemData i in m_Encodings.Encodings )
			{
				ListViewItem itm = m_listEncodings.Items.Add(i.sName);
				itm.Tag = i;

				itm.SubItems.Add(i.e.CodePage.ToString());
				itm.SubItems.Add(i.e.BodyName);
				itm.SubItems.Add(i.e.HeaderName);
				itm.SubItems.Add(i.e.WebName);

				itm.Checked = i.bEnable;
			}//end foreach
		}//end FormEncodings_Load

		private void m_btnOK_Click(object sender, EventArgs e)
		{
			foreach ( ListViewItem itm in m_listEncodings.Items )
			{
                EncodingItemData i = itm.Tag as EncodingItemData;
				i.bEnable = itm.Checked;
			}//end foreach
		}

		private void m_btnCancel_Click(object sender, EventArgs e)
		{

		}
	}//end class FormEncodings
}//end namespace ClipboardListener