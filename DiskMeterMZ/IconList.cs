using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace MkZ
{
	/// <summary>
	/// Summary description for TrayIconList.
	/// </summary>
	public class IconList
	{
		public IconList(string [] svIcons)
		{
			//sample: "DUMeterMZ.res.ico.ico_off.ico"
			string [] v = Assembly.GetExecutingAssembly().GetManifestResourceNames();
			m_vIcons = new Icon[svIcons.Length];

			for ( int i=0; i<svIcons.Length; i++ )
			{
				Stream s = Assembly.GetExecutingAssembly().
					GetManifestResourceStream(svIcons[i]);
				m_vIcons[i] = new Icon(s);
			}//end for
		}//end constructor

		private Icon [] m_vIcons;

		public Icon this[int idx]
		{
			get { return m_vIcons[idx]; }
		}//end index
	}//end class IconList
}//end namespace MZ
