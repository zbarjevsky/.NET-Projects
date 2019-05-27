using ClipboardManager.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ClipboardManager.Utils
{
    public class SettingsData
    {
        public HotKeyData HotKey { get; set; } = new HotKeyData();
        public int m_iMenuMaxLen { get; set; } = 30;
        public int m_iBufferMaxLen { get; set; } = 200;
        public bool m_bShowSnapShot { get; set; } = true;
        public bool m_bShowDebug { get; set; } = true;
        public int m_iHotKeyAppId { get; set; } = new Random().Next(100, 500);
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EncodingsList m_Encodings { get; set; } = new EncodingsList();
        public bool m_AutoReconnect { get; set; } = true;
        public bool m_WriteLogFile { get; set; } = false;
        public bool m_bAutoUAC { get; set; } = false;
        public bool m_bAbortShutdown { get; set; } = false;
        public bool m_bStopServices { get; set; } = false;

        public void Save(string fileName)
        {
            try
            {
                this.SaveAs(fileName);
            }
            catch (Exception err)
            {
                Debug.WriteLine("Save error: "+err);
            }
        }

        public void Load(string fileName)
        {
            try
            {
                SettingsData s = this.Open(fileName);
                if (s == null)
                    s = new SettingsData();
                s.UpdateEncodingsAfterLoadFromXml();

                this.CopyFrom(s);
            }
            catch (Exception err)
            {
                Debug.WriteLine("Load Error: "+err);
            }
        }

        private void UpdateEncodingsAfterLoadFromXml()
        {
            this.m_Encodings.UpdateEncodingsAfterLoadFromXml();
        }

        private void CopyFrom(SettingsData s)
        {
            HotKey = s.HotKey;
            m_iMenuMaxLen = s.m_iMenuMaxLen;
            m_iBufferMaxLen = s.m_iBufferMaxLen;
            m_bShowSnapShot = s.m_bShowSnapShot;
            m_bShowDebug = s.m_bShowDebug;
            m_iHotKeyAppId = s.m_iHotKeyAppId;
            m_Encodings = s.m_Encodings;
            m_AutoReconnect = s.m_AutoReconnect;
            m_WriteLogFile = s.m_WriteLogFile;
            m_bAutoUAC = s.m_bAutoUAC;
            m_bAbortShutdown = s.m_bAbortShutdown;
            m_bStopServices = s.m_bStopServices;
        }
}

    public class Settings
    {
        //public HotKeyTranslator m_HotKey = null;
        //public int m_iMenuMaxLen = 30;
        //public int m_iBufferMaxLen = 200;
        //public bool m_bShowSnapShot = true;
        //public bool m_bShowDebug = true;
        //private int m_iHotKeyAppId = new Random().Next(100, 500);
        //public Encodings m_Encodings = new Encodings();
        //public bool m_AutoReconnect = true;
        //private bool m_WriteLogFile = false;
        //public bool m_bAutoUAC = false;
        //public bool m_bAbortShutdown = false;
        //public bool m_bStopServices = false;

        public SettingsData I = new SettingsData();

        public bool WriteLogFile
        {
            get { return I.m_WriteLogFile; }
            set { I.m_WriteLogFile = value; Utils.Log.m_bWriteLog = value; }
        }//end WriteLogFile

        public Settings()
        {
            System.Diagnostics.Trace.WriteLine("HotKeyAppId: " + I.m_iHotKeyAppId);
        }//end constructor

        public void Save(string sHistoryFileName, string sSettingsFileName, 
            ClipboardList listMain, ClipboardList listFavorites)
        {
            I.Save(sSettingsFileName);

            try
            {
                IZip zip = null;
                try { zip = new DotNetZip(GetZipFilePath(sHistoryFileName), true); }
                catch (Exception err)
                {
                    FormClipboard.TraceLn(true, "Settings", "Save",
                        "Create Zip: {0} Error: {1}", sHistoryFileName, err.Message);
                }//end catch

                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.CreateNode(XmlNodeType.Element, "Settings", "");
                root = doc.AppendChild(root);

                listMain.Save(root, zip, Path.GetDirectoryName(sHistoryFileName));
                listFavorites.Save(root, zip, Path.GetDirectoryName(sHistoryFileName));

                doc.PreserveWhitespace = true;
                doc.Save(sHistoryFileName);

                zip.Add(sHistoryFileName);
                zip.Close();
                File.Delete(sHistoryFileName);
            }//end try
            catch (Exception err)
            {
                FormClipboard.TraceLn(true, "Settings", "Save", "Exception: {0}", err.Message);
            }//end catch
        }//end save

        public bool Load(string sHistoryFileName, string sSettingsFileName, 
            Form parent, 
            ClipboardList listMain, ClipboardList listFavorites, Image icoDefault)
        {
            I.Load(sSettingsFileName);
            return Import(sHistoryFileName, listMain, listFavorites, icoDefault);
        }//end Load

        public bool Import(string sHistoryFileName, ClipboardList listMain, ClipboardList listFavorites, Image icoDefault)
        {
            try
            {
                try
                {
                    DotNetZip.UnZipFiles(GetZipFilePath(sHistoryFileName), Path.GetDirectoryName(GetZipFilePath(sHistoryFileName)));
                }//end try
                catch (Exception err)
                {
                    FormClipboard.TraceLn(true, "Settings", "Load",
                        "Unzip: {0} Error: {1}", sHistoryFileName, err.Message);
                }//end catch

                XmlDocument doc = new XmlDocument();
                doc.Load(sHistoryFileName);
                XmlNode root = doc.SelectSingleNode("Settings");

                listMain.Load(doc, icoDefault);
                listFavorites.Load(doc, icoDefault);

                listMain.MAX_HISTORY = I.m_iBufferMaxLen;
                listFavorites.MAX_HISTORY = I.m_iBufferMaxLen;

                //File.Delete(sHistoryFileName);
                return true;
            }//end try
            catch (Exception err)
            {
                FormClipboard.TraceLn(true, "Settings", "Load",
                    "{0} Error: {1}", sHistoryFileName, err.Message);
                return false;
            }//end catch
        }

        private string GetZipFilePath(string sFileName)
        {
            return Path.ChangeExtension(sFileName, ".mzconfig");
        }//end GetZipFilePath
    }//end class Settings
}
