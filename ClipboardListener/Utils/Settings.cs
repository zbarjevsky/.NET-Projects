using ClipboardManager.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ClipboardManager.Utils
{
    public class Settings
    {
        public HotKeyTranslator m_HotKey = null;
        public int m_iHistoryLen = 20;
        public bool m_bShowSnapShot = true;
        public bool m_bShowDebug = true;
        private int m_iHotKeyAppId = new Random().Next(100, 500);
        public Encodings m_Encodings = new Encodings();
        public bool m_AutoReconnect = true;
        private bool m_WriteLogFile = false;
        public bool m_bAutoUAC = false;

        public bool WriteLogFile
        {
            get { return m_WriteLogFile; }
            set { m_WriteLogFile = value; Utils.Log.m_bWriteLog = value; }
        }//end WriteLogFile

        public Settings()
        {
            System.Diagnostics.Trace.WriteLine("HotKeyAppId: " + m_iHotKeyAppId);
        }//end constructor

        public void Save(string sFileName, ClipboardList listMain, ClipboardList listFavorites)
        {
            try
            {
                IZip zip = null;
                try { zip = new DotNetZip(GetZipFilePath(sFileName), true); }
                catch (Exception err)
                {
                    FormClipboard.TraceLn(true, "Settings", "Save",
                        "Create Zip: {0} Error: {1}", sFileName, err.Message);
                }//end catch

                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.CreateNode(XmlNodeType.Element, "Settings", "");
                root = doc.AppendChild(root);

                m_HotKey.Save(root);
                XmlUtil.AddNewNode(root, "HistoryLength", m_iHistoryLen.ToString());
                XmlUtil.AddNewNode(root, "ShowSnapShot", m_bShowSnapShot.ToString());
                XmlUtil.AddNewNode(root, "ShowDebugWindow", m_bShowDebug.ToString());
                XmlUtil.AddNewNode(root, "AutoReconnect", m_AutoReconnect.ToString());
                XmlUtil.AddNewNode(root, "WriteLogFile", m_WriteLogFile.ToString());
                XmlUtil.AddNewNode(root, "AutoUAC", m_bAutoUAC.ToString());
                m_Encodings.Save(root);
                listMain.Save(root, zip, Path.GetDirectoryName(sFileName));
                listFavorites.Save(root, zip, Path.GetDirectoryName(sFileName));

                doc.PreserveWhitespace = true;
                doc.Save(sFileName);

                zip.Add(sFileName);
                zip.Close();
                File.Delete(sFileName);
            }//end try
            catch (Exception err)
            {
                FormClipboard.TraceLn(true, "Settings", "Save", "Exception: {0}", err.Message);
            }//end catch
        }//end save

        public void Load(string sFileName, Form parent, ClipboardList listMain, ClipboardList listFavorites, Image icoDefault)
        {
            try
            {
                m_HotKey = new HotKeyTranslator(parent, m_iHotKeyAppId);

                try
                {
                    DotNetZip.UnZipFiles(GetZipFilePath(sFileName), Path.GetDirectoryName(GetZipFilePath(sFileName)));
                    //JavaUnZip unzip = new JavaUnZip(GetZipFilePath(sFileName));
                    //unzip.ExtractFiles(Path.GetDirectoryName(GetZipFilePath(sFileName)));
                }//end try
                catch (Exception err)
                {
                    FormClipboard.TraceLn(true, "Settings", "Load",
                        "Unzip: {0} Error: {1}", sFileName, err.Message);
                }//end catch

                XmlDocument doc = new XmlDocument();
                doc.Load(sFileName);
                XmlNode root = doc.SelectSingleNode("Settings");

                m_HotKey.Load(root);
                m_iHistoryLen = XmlUtil.GetInt(root, "HistoryLength", m_iHistoryLen);
                m_bShowSnapShot = XmlUtil.GetBool(root, "ShowSnapShot", m_bShowSnapShot);
                m_bShowDebug = XmlUtil.GetBool(root, "ShowDebugWindow", m_bShowDebug);
                m_AutoReconnect = XmlUtil.GetBool(root, "AutoReconnect", m_AutoReconnect);
                WriteLogFile = XmlUtil.GetBool(root, "WriteLogFile", m_WriteLogFile);
                m_bAutoUAC = XmlUtil.GetBool(root, "AutoUAC", m_bAutoUAC);
                m_Encodings.Load(root);
                listMain.Load(doc, icoDefault);
                listFavorites.Load(doc, icoDefault);

                File.Delete(sFileName);
            }//end try
            catch (Exception err)
            {
                FormClipboard.TraceLn(true, "Settings", "Load",
                    "{0} Error: {1}", sFileName, err.Message);
            }//end catch
        }//end Load

        private string GetZipFilePath(string sFileName)
        {
            return Path.ChangeExtension(sFileName, ".mzconfig");
        }//end GetZipFilePath
    }//end class Settings
}
