using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.Windows.Forms;

namespace ClipboardManager
{
	// Creates a  message filter.
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ClipboardMessageFilter : IMessageFilter
	{
		FormClipboard _frm;

		public ClipboardMessageFilter(FormClipboard frm)
		{
			_frm = frm;
		}

		int count = 0;
		public bool PreFilterMessage(ref Message m)
		{
			System.Diagnostics.Debug.WriteLine(string.Format("{0}. FILTER: PreFilterMessage({1}-{2})",
				count++, m.Msg, WindowsMessages.Message(m.Msg)));
			return _frm.ProcessWindowsMessage(ref m);
		}
	}

}
