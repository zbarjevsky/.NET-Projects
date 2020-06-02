using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DUMeterMZ
{
	public partial class FormAbout : Form
	{
		public FormAbout()
		{
			InitializeComponent();
		}//end constructor

		private void FormAbout_Load(object sender, EventArgs e)
		{
			m_lblAbout.Text = "DU MeterMZ v"+Application.ProductVersion;

			m_richTextBoxAbout.AppendText("01 Jan 2003: Created\n");
			m_richTextBoxAbout.AppendText("....................\n");
			m_richTextBoxAbout.AppendText("14 Jun 2006: Migrated to VS2005\n");
			m_richTextBoxAbout.AppendText("14 Jul 2006: Added time tics to reports\n");
			m_richTextBoxAbout.AppendText("19 Aug 2006: Added icons to reports\n");
			m_richTextBoxAbout.AppendText("19 Aug 2006: Added border style to settings\n");
			m_richTextBoxAbout.AppendText("19 Aug 2006: Added reset position menu item\n");
			m_richTextBoxAbout.AppendText("24 Aug 2006: Improved selection mechanism in reports\n");
            m_richTextBoxAbout.AppendText("01 Sep 2006: About Form\n");
            m_richTextBoxAbout.AppendText("04 May 2007: Fixed 'save location' bug\n");
            m_richTextBoxAbout.AppendText("09 May 2007: Reimplemented Ping mechanism\n");
            m_richTextBoxAbout.AppendText("05 Oct 2008: Reimplemented Log mechanism + Generics\n");
            m_richTextBoxAbout.AppendText("14 Jul 2013: Compiled for Windows 7 x64\n");
            m_richTextBoxAbout.AppendText("17 Jun 2015: Fixed 'Sleep Mode' bug\n");
			m_richTextBoxAbout.AppendText("17 Jun 2016: Fixed 'Long Tooltip' bug\n");
			m_richTextBoxAbout.AppendText("29 Apr 2020: Implemented Single Instance, User Folder, VS2019");
			m_richTextBoxAbout.AppendText("\n");
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
}//end namespace DUMeterMZ