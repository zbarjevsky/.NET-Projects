using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClipboardManager
{
	public partial class FormAbout : Form
	{
		public FormAbout()
		{
			InitializeComponent();
		}//end constructor

		private void FormAbout_Load(object sender, EventArgs e)
		{
			m_lblAbout.Text = "Clipboard Manager v"+Application.ProductVersion;

			m_richTextBoxAbout.Text = "";
			m_richTextBoxAbout.AppendText("08 Oct 2020: Auto version update - todays date\n");
			m_richTextBoxAbout.AppendText("11 Apr 2020: Services Monitoring and stopping\n");
			m_richTextBoxAbout.AppendText("21 Jun 2016: Added save/restore Desktop icons positions\n");
			m_richTextBoxAbout.AppendText("25 Mar 2011: Using Zip of .Net 3.5 instead of JavaZip\n");
			m_richTextBoxAbout.AppendText("27 Apr 2007: Added tooltips for long strings\n");
			m_richTextBoxAbout.AppendText("27 Apr 2007: Added log file\n");
            m_richTextBoxAbout.AppendText("13 Apr 2007: Fixed LastEntry bug\n");
            m_richTextBoxAbout.AppendText("08 Apr 2007: Implemented(partially) Save As...\n");
			m_richTextBoxAbout.AppendText("22 Feb 2007: Implemented GetText from EditBox in Finder Tool\n");
			m_richTextBoxAbout.AppendText("02 Feb 2007: Added Explicit Garbage Collection\n");
			m_richTextBoxAbout.AppendText("02 Feb 2007: Added Finder Tool\n");
			m_richTextBoxAbout.AppendText("18 Jan 2007: Added timer for reconnect\n");
			m_richTextBoxAbout.AppendText("11 Sep 2006: Added reregister menu item\n");
			m_richTextBoxAbout.AppendText("19 Jul 2006: Added Encoding\n");
			m_richTextBoxAbout.AppendText("19 Jul 2006: Added System Menu Exit + About\n");
			m_richTextBoxAbout.AppendText("15 Jul 2006: Released Unused Memory\n");
			m_richTextBoxAbout.AppendText("15 Jul 2006: About Form\n");
			m_richTextBoxAbout.AppendText("14 Jul 2006: Tools -> Reverse Char Order\n");
			m_richTextBoxAbout.AppendText("14 Jul 2006: Spliiter panel for Clipboard/Snapshot views\n");
            m_richTextBoxAbout.AppendText("14 Jul 2006: Possibility to Hide/Show debug and snapshot windows\n");
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