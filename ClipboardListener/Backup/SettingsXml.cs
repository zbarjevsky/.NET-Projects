using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ClipboardManager
{
	class SettingsXml
	{
		public static void Load(ClipboardList list, FormClipboard.Settings sett, string sFileName)
		{
		}//end Load

		//save only Text or Rtf
		//file type save as Text only - as file path
		public static void Save(ClipboardList list, FormClipboard.Settings sett, string sFileName)
		{
		}//end Save

		private string[] code = null;
		private Hashtable decode = null;
		private void Init()
		{
			code = new string[256];
			decode = new Hashtable(256);
			for (int i = 0; i < 256; i++)
			{
				code[i] = i.ToString("X");
				decode[code[i]] = i;
			}//end for
		}//end Init

		private string Encode(byte[] data)
		{
			StringBuilder sb = new StringBuilder(data.Length * 2);
			for ( int i=0; i<data.Length; i++ )
				sb.Append(code[data[i]]);
			return sb.ToString();
		}//end Encode

		private byte[] Decode(string s)
		{
			byte[] data = new byte[s.Length / 2];
			for (int i = 0; i < data.Length; i++)
			{
				string b = s.Substring(i*2, 2);
				data[i] = (byte)decode[b];
			}//end for
			return data;
		}//end Decode
	}//end class SettingsXml
}//end namespace ClipboardListener
