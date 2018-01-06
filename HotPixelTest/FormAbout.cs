using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HotPixelTest
{
	public partial class FormAbout : Form
	{
		public FormAbout()
		{
			InitializeComponent();
		}//end constructor

		private void FormAbout_Load(object sender, EventArgs e)
		{
			m_lblAbout.Text = "Dead Pixel Test v" + Application.ProductVersion;

			m_richTextBoxAbout.Text = "";
			m_richTextBoxAbout.AppendText("29 Jul 2010: Created\n");
			m_richTextBoxAbout.AppendText("29 Jul 2010: Fixed Memory Leak\n");
			m_richTextBoxAbout.AppendText("30 Jul 2010: Added persistency to file name\n");
			m_richTextBoxAbout.AppendText("30 Jul 2010: Added Abort button\n");
			m_richTextBoxAbout.AppendText("30 Jul 2010: Added Save As button\n");
			m_richTextBoxAbout.AppendText("02 Nov 2010: Added EXIF info\n");
		}//end FormAbout_Load

		private void m_btnOK_Click(object sender, EventArgs e)
		{

		}//end m_btnOK_Click

		private void m_lnkMailTo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if ( e == null || e.Button == MouseButtons.Left )
				System.Diagnostics.Process.Start("mailto:zbarjevsky@gmail.com");
		}

		private void m_contextMenuStrip_Mail_Copy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText("zbarjevsky@gmail.com", TextDataFormat.UnicodeText);
		}//end m_contextMenuStrip_Mail_Copy_Click

		private void m_contextMenuStrip_Mail_Send_Click(object sender, EventArgs e)
		{
			m_lnkMailTo_LinkClicked(sender, null);
		}
	}//end class FormAbout
}//end namespace ClipboardListener