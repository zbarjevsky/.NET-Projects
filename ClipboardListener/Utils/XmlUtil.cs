using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace ClipboardManager
{
	public class XmlUtil
	{
		public static string GetStr(XmlNode nd, string Name)
		{
			return GetStr(nd, Name, "");
		}//end GetStr

		public static string GetStr(XmlNode nd, string Name, string sDefault)
		{
			try { XmlNode n = nd.SelectSingleNode(Name); return n.InnerText; }
			catch { return sDefault; }
		}//end GetStr

		public static string GetStrAtt(XmlNode nd, string Name, string sDefault)
		{
            try
            {
                if (nd.Attributes != null && nd.Attributes[Name] != null)
                    return nd.Attributes[Name].Value;
            }
            catch { }
            return sDefault;
        }//end GetStrAtt

		public static XmlNode UpdSubNode(XmlNode nd, string Name, string Value)
		{
			try
			{
				XmlNode n = nd.SelectSingleNode(Name);
				if (n == null)
					n = AddNewNode(nd, Name, Value);
				if ( Value.Length > 0 )
					n.InnerText = Value;
				return n;
			}//end try
			catch 
			{
				return null;
			}//end catch
		}//end UpdateSubNode

		public static XmlNode AddNewNode(XmlNode nd, string Name)
		{
			return AddNewNode(nd, Name, "");
		}//end AddNewNode

		public static XmlNode AddNewNode(XmlNode nd, string Name, string Value)
		{
			XmlNode n = nd.OwnerDocument.CreateNode(XmlNodeType.Element, Name, "");
			if (Value.Length > 0)
				n.InnerText = Value;
			return nd.AppendChild(n);
		}//end AddNewNode

		public static XmlAttribute UpdStrAtt(XmlNode nd, string AttName, string AttValue)
		{
			XmlAttribute att = nd.Attributes[AttName];
			if (att == null)
			{
				att = nd.OwnerDocument.CreateAttribute(AttName);
				att = nd.Attributes.Append(att);
			}//end if
			att.Value = AttValue;
			return att;
		}//end UpdStrAtt


		public static bool GetBool(XmlNode nd, string Name, bool bDefault)
		{
			try
			{
				string s = GetStr(nd, Name, bDefault.ToString());
				return Boolean.Parse(s);
			}//end try
			catch { return bDefault;  }
		}//end GetBool

		public static int GetInt(XmlNode nd, string Name, int iDefault)
		{
			try
			{
				string s = GetStr(nd, Name, iDefault.ToString());
				return Int32.Parse(s);
			}//end try
			catch { return iDefault; }
		}//end GetInt
	}//end class XmlUtil
}//end namespace ClipboardListener
