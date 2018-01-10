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
		private Encodings m_Encodings = null;

		public FormEncodings(Encodings encodings)
		{
			m_Encodings = encodings;

			InitializeComponent();
		}//end constructor

		private void FormEncodings_Load(object sender, EventArgs e)
		{
			foreach ( Encodings.Item i in m_Encodings.m_vItems )
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
				Encodings.Item i = (Encodings.Item)itm.Tag;
				i.bEnable = itm.Checked;
			}//end foreach
		}

		private void m_btnCancel_Click(object sender, EventArgs e)
		{

		}
	}//end class FormEncodings
}//end namespace ClipboardListener