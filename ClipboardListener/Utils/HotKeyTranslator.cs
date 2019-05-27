using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace ClipboardManager
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class HotKeyData
    {
        [XmlIgnore]
        public int AppId { get; set; } = new Random().Next(100, 500);
        public Keys HotKey { get; set; } = Keys.Q;
        public bool m_ctrlHotKey { get; set; } = true;
        public bool m_shiftHotKey { get; set; } = false;
        public bool m_altHotKey { get; set; } = false;
        public bool m_winHotKey { get; set; } = false;
        public bool m_bUseHotKey { get; set; } = true;

        public HotKeyData() { }
        public HotKeyData(int appId) { AppId = appId; }

        public void Reset()
        {
            HotKeyData key = new HotKeyData(AppId);
#if DEBUG
            HotKey      = Keys.W; // default HotKey for Debug is Ctrl+W
#else
			HotKey		= Keys.Q; // default HotKey is Ctrl+Q
#endif
            m_ctrlHotKey = key.m_ctrlHotKey;
            m_shiftHotKey = key.m_shiftHotKey;
            m_altHotKey = key.m_altHotKey;
            m_winHotKey = key.m_winHotKey;

            m_bUseHotKey = key.m_bUseHotKey;
        }//end Reset

        public HotKeyData Clone()
        {
            HotKeyData key = new HotKeyData(AppId);

            key.m_bUseHotKey = this.m_bUseHotKey;

            key.HotKey = HotKey;

            key.m_ctrlHotKey = m_ctrlHotKey;
            key.m_shiftHotKey = m_shiftHotKey;
            key.m_altHotKey = m_altHotKey;
            key.m_winHotKey = m_winHotKey;

            return key;
        }//end Clone

        public void SetHotKey(System.Windows.Forms.KeyEventArgs e)
        {
            SetHotKey(e.KeyCode,
                (e.Modifiers & Keys.Control) != 0 ? true : false,
                (e.Modifiers & Keys.Shift) != 0 ? true : false,
                (e.Modifiers & Keys.Alt) != 0 ? true : false,
                ((e.Modifiers & Keys.LWin) != 0 || (e.Modifiers & Keys.RWin) != 0) ? true : false);
        }//end SetHotKey

        public void SetHotKey(Keys c, bool bCtrl, bool bShift, bool bAlt, bool bWindows)
        {
            HotKey = c;
            m_ctrlHotKey = bCtrl;
            m_shiftHotKey = bShift;
            m_altHotKey = bAlt;
            m_winHotKey = bWindows;
        }//end SetHotKey

        public override string ToString()
        {
            String sHotKey = "";
            if (m_ctrlHotKey)
                sHotKey += "Ctrl";

            if (m_shiftHotKey)
            {
                if (sHotKey.Length != 0)
                    sHotKey += " + ";
                sHotKey += "Shift";
            }//end if

            if (m_altHotKey)
            {
                if (sHotKey.Length != 0)
                    sHotKey += " + ";
                sHotKey += "Alt";
            }//end if

            if (m_winHotKey)
            {
                if (sHotKey.Length != 0)
                    sHotKey += " + ";
                sHotKey += "Win";
            }//end if

            if (sHotKey.Length != 0)
                sHotKey += " + ";

            sHotKey += HotKey.ToString();

            return sHotKey;
        }//end ToString
    }

    public static class HotKeyHelper
	{
		//private int m_Id = 101;

		//private Form m_Parent = null;

		//protected Keys m_HotKey;
		//protected bool m_ctrlHotKey, m_shiftHotKey, m_altHotKey, m_winHotKey;

		//public bool m_bUseHotKey = false;

		//public HotKeyTranslator(Form parent, int id)
		//{
		//	m_Id			= id;
		//	m_Parent		= parent;

		//	Reset();

		//	SetHotKey(m_HotKey, m_ctrlHotKey, m_shiftHotKey, m_altHotKey, m_winHotKey);
		//}//end constructor

//		internal void Reset()
//		{
//#if DEBUG
//			m_HotKey		= Keys.W; // default HotKey is Ctrl+W
//#else
//			m_HotKey		= Keys.Q; // default HotKey is Ctrl+Q
//#endif
//			m_ctrlHotKey	= true;
//			m_shiftHotKey	= false;
//			m_altHotKey		= false;
//			m_winHotKey		= false;

//			m_bUseHotKey	= false;
//		}//end Reset

		//public void Save(XmlNode ndParent)
		//{
		//	XmlNode nd = XmlUtil.AddNewNode(ndParent, "HotKey");
		//	XmlUtil.AddNewNode(nd, "UseHotKey", m_bUseHotKey.ToString());
		//	XmlUtil.AddNewNode(nd, "KeyCode", m_HotKey.ToString());

		//	XmlUtil.AddNewNode(nd, "Ctrl", m_ctrlHotKey.ToString());
		//	XmlUtil.AddNewNode(nd, "Shift", m_shiftHotKey.ToString());
		//	XmlUtil.AddNewNode(nd, "Alt", m_altHotKey.ToString());
		//	XmlUtil.AddNewNode(nd, "Win", m_winHotKey.ToString());
		//}//end Save

		//public void Load(XmlNode ndParent)
		//{
		//	XmlNode nd = ndParent.SelectSingleNode("HotKey");
		//	if (nd == null)
		//		return;

		//	string sKey = XmlUtil.GetStr(nd, "KeyCode", m_HotKey.ToString());
		//	m_HotKey = ((Keys)(new KeysConverter().ConvertFromString(sKey)));

		//	m_ctrlHotKey	= XmlUtil.GetBool(nd, "Ctrl", m_ctrlHotKey);
		//	m_shiftHotKey	= XmlUtil.GetBool(nd, "Shift", m_shiftHotKey);
		//	m_altHotKey		= XmlUtil.GetBool(nd, "Alt", m_altHotKey);
		//	m_winHotKey		= XmlUtil.GetBool(nd, "Win", m_winHotKey);

		//	m_bUseHotKey	= XmlUtil.GetBool(nd, "UseHotKey", m_bUseHotKey);
		//}//end Load

		//public void SetHotKey(System.Windows.Forms.KeyEventArgs e)
		//{
		//	SetHotKey(e.KeyCode,
		//		(e.Modifiers & Keys.Control) !=0 ? true : false,
		//		(e.Modifiers & Keys.Shift) != 0 ? true : false,
		//		(e.Modifiers & Keys.Alt) != 0 ? true : false,
		//		((e.Modifiers & Keys.LWin) != 0 || (e.Modifiers & Keys.RWin) != 0) ? true : false);
		//}//end SetHotKey

		//public void SetHotKey(Keys c, bool bCtrl, bool bShift, bool bAlt, bool bWindows)
		//{
		//	m_HotKey		= c;
		//	m_ctrlHotKey	= bCtrl;
		//	m_shiftHotKey	= bShift;
		//	m_altHotKey		= bAlt;
		//	m_winHotKey		= bWindows;
		//}//end SetHotKey

		public static bool RegisterHotKey(this HotKeyData key, Form parent)
		{
			if ( !key.m_bUseHotKey )
				return true;

			// update HotKey
			NativeWIN32.KeyModifiers modifiers = NativeWIN32.KeyModifiers.None;

			if (key.m_ctrlHotKey)
				modifiers |= NativeWIN32.KeyModifiers.Control;
			if (key.m_shiftHotKey)
				modifiers |= NativeWIN32.KeyModifiers.Shift;
			if (key.m_altHotKey)
				modifiers |= NativeWIN32.KeyModifiers.Alt;
			if (key.m_winHotKey)
				modifiers |= NativeWIN32.KeyModifiers.Windows;

			return NativeWIN32.RegisterHotKey(parent.Handle, key.AppId, modifiers, key.HotKey);
		}//end RegisterHotKey

		public static void UnregisterHotKey(this HotKeyData key, Form parent)
		{
			NativeWIN32.UnregisterHotKey(parent.Handle, key.AppId);
		}//end UnregisterHotKey

		//public override string ToString()
		//{
		//	String sHotKey = "";
		//	if ( m_ctrlHotKey )
		//		sHotKey += "Ctrl";

		//	if ( m_shiftHotKey )
		//	{
		//		if ( sHotKey.Length != 0 )
		//			sHotKey += " + ";
		//		sHotKey += "Shift";
		//	}//end if

		//	if ( m_altHotKey )
		//	{
		//		if ( sHotKey.Length != 0 )
		//			sHotKey += " + ";
		//		sHotKey += "Alt";
		//	}//end if

		//	if ( m_winHotKey )
		//	{
		//		if ( sHotKey.Length != 0 )
		//			sHotKey += " + ";
		//		sHotKey += "Win";
		//	}//end if

		//	if (sHotKey.Length != 0)
		//		sHotKey += " + ";

		//	sHotKey += m_HotKey.ToString();

		//	return sHotKey;
		//}//end ToString

		//internal HotKeyHelper Clone()
		//{
		//	HotKeyHelper key = new HotKeyTranslator(this.m_Parent, this.m_Id);

		//	key.m_bUseHotKey	= this.m_bUseHotKey;

		//	key.m_HotKey		= m_HotKey;

		//	key.m_ctrlHotKey	= m_ctrlHotKey;
		//	key.m_shiftHotKey	= m_shiftHotKey;
		//	key.m_altHotKey		= m_altHotKey;
		//	key.m_winHotKey		= m_winHotKey;

		//	return key;
		//}//end Clone
	}//end class HotKeyTranslator
}//end namespace ClipboardListener
