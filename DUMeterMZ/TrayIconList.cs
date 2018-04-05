using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace DUMeterMZ
{
	/// <summary>
	/// Summary description for TrayIconList.
	/// </summary>
	public class TrayIconList
	{
		public TrayIconList()
		{
			string [] v = Assembly.GetExecutingAssembly().GetManifestResourceNames();
			m_vIcons = new Icon[4];
			Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("DUMeterMZ.res.ico.ico_both.ico");
			m_vIcons[0] = new Icon(s);
			s = Assembly.GetExecutingAssembly().GetManifestResourceStream("DUMeterMZ.res.ico.ico_ul.ico");
			m_vIcons[1] = new Icon(s);
			s = Assembly.GetExecutingAssembly().GetManifestResourceStream("DUMeterMZ.res.ico.ico_dl.ico");
			m_vIcons[2] = new Icon(s);
			s = Assembly.GetExecutingAssembly().GetManifestResourceStream("DUMeterMZ.res.ico.ico_off.ico");
			m_vIcons[3] = new Icon(s);
		}//end constructor

		private Icon [] m_vIcons;

		public Icon this[int idx]
		{
			get { return m_vIcons[idx]; }
		}//end index

		public Icon Get(float up, float down, float linespeed)
		{
			float minRcv = linespeed / 100; //less than 1 percent
			//upload usually significantly lower than download
			float minSnd = linespeed / 10; //less than 1 percent of upload

			if (up > minSnd && down > minRcv)
				return m_vIcons[0];
			else if ( up > minSnd )
				return m_vIcons[1];
			else if ( down > minRcv )
				return m_vIcons[2];
			else 
				return m_vIcons[3];
		}//end Get
	}//end class TrayIconList
}
