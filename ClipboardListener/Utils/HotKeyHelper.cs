using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;


using ClipboardManager.Utils;
using MkZ.Tools;
using MkZ.Windows.Win32API;

namespace ClipboardManager
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class HotKeyData
    {
        [XmlIgnore]
        public int AppId { get; set; } = new Random().Next(100, 500);
        public Keys KeyData { get; set; } = Keys.Control | Keys.Q;
        public bool UseHotKey { get; set; } = true;

        public HotKeyData() { }
        public HotKeyData(int appId) { AppId = appId; }

        public void Reset()
        {
            HotKeyData key = new HotKeyData(AppId);
#if DEBUG
            KeyData = Keys.Control | Keys.W; // default HotKey for Debug is Ctrl+W
#else
			KeyData	= Keys.Control | Keys.Q; // default HotKey is Ctrl+Q
#endif
            UseHotKey = key.UseHotKey;
        }//end Reset

        public HotKeyData Clone()
        {
            HotKeyData key = new HotKeyData(AppId);

            key.UseHotKey = this.UseHotKey;

            key.KeyData = KeyData;

            return key;
        }//end Clone

        public override string ToString()
        {
            String sHotKey = "";
            if (KeyData.HasFlag(Keys.Control))
                sHotKey += "Ctrl";

            if (KeyData.HasFlag(Keys.Shift))
            {
                if (sHotKey.Length != 0)
                    sHotKey += " + ";
                sHotKey += "Shift";
            }//end if

            if (KeyData.HasFlag(Keys.Alt))
            {
                if (sHotKey.Length != 0)
                    sHotKey += " + ";
                sHotKey += "Alt";
            }//end if

            if (KeyData.HasFlag(Keys.LWin))
            {
                if (sHotKey.Length != 0)
                    sHotKey += " + ";
                sHotKey += "Win";
            }//end if

            if (sHotKey.Length != 0)
                sHotKey += " + ";

            sHotKey += KeyData.ToString().Substring(0,1);

            return sHotKey;
        }//end ToString
    }

    public static class HotKeyHelper
	{
		public static bool RegisterHotKey(this HotKeyData keyData, Form parent)
		{
			if ( !keyData.UseHotKey )
				return true;

			// update HotKey
			User32HotKey.KeyModifiers modifiers = User32HotKey.KeyModifiers.None;

			if (keyData.KeyData.HasFlag(Keys.Control))
				modifiers |= User32HotKey.KeyModifiers.Control;
			if (keyData.KeyData.HasFlag(Keys.Shift))
				modifiers |= User32HotKey.KeyModifiers.Shift;
			if (keyData.KeyData.HasFlag(Keys.Alt))
				modifiers |= User32HotKey.KeyModifiers.Alt;
			if (keyData.KeyData.HasFlag(Keys.LWin) || keyData.KeyData.HasFlag(Keys.RWin))
				modifiers |= User32HotKey.KeyModifiers.Windows;

            Keys key = (Keys)((int)keyData.KeyData & 0x0000FFFF); //filter out modifiers

            LogC.WriteLine("RegisterHotKey: " + keyData);
            return User32HotKey.RegisterHotKey(parent.Handle, keyData.AppId, modifiers, key);
		}//end RegisterHotKey

		public static void UnregisterHotKey(this HotKeyData keyData, Form parent)
		{
            LogC.WriteLine("UnregisterHotKey: " + keyData);
            User32HotKey.UnregisterHotKey(parent.Handle, keyData.AppId);
		}//end UnregisterHotKey
	}//end class HotKeyTranslator
}//end namespace ClipboardListener
