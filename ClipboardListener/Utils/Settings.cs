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
using System.Xml.Serialization;
using Utils;

namespace ClipboardManager.Utils
{
    public class SettingsData
    {
        public HotKeyData HotKeyInfo { get; set; } = new HotKeyData();
        public int MenuMaxLen { get; set; } = 30;
        public int BufferMaxLen { get; set; } = 200;
        public bool ShowSnapShot { get; set; } = true;
        public bool ShowDebug { get; set; } = true;
        public bool IsAutoReconnect { get; set; } = true;
        [DisplayName("Automatically reset UAC")]
        public bool IsAutoUAC { get; set; } = false;
        public bool IsAbortShutdown { get; set; } = false;
        [DisplayName("Stop Services if Running")]
        public bool IsStopServices { get; set; } = false;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ServicesManipulatorSettings ServicesManipulatorSettings { get; set; } = new ServicesManipulatorSettings();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EncodingsList EncodingsList { get; set; } = new EncodingsList();

        [XmlIgnore] //read from registry and not from XML
        [Category("Startup Options")]
        [Description("Load application when Windows starts")]
        [DisplayName("Load with Windows")]
        public bool LoadWithWindows
        {
            get
            {
                var key = OpenRegKey(writable: false);
                if (key == null)
                    return false;
                bool run = (key.GetValue(_strAppKey) != null);
                key.Close();
                return run;
            }//end get

            set
            {
                var key = OpenRegKey(writable: true);
                if (key == null)
                    return;

                if (value)
                    key.SetValue(_strAppKey, "\"" + Application.ExecutablePath + "\"");
                else
                    key.DeleteValue(_strAppKey, false);
                key.Close();
            }//end set
        }//end LoadWithWindows

        public void Save(string fileName)
        {
            try
            {
                this.SaveAs(fileName);
            }
            catch (Exception err)
            {
                Debug.WriteLine("Save error: " + err);
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
                Debug.WriteLine("Load Error: " + err);
            }
        }

        private void UpdateEncodingsAfterLoadFromXml()
        {
            this.EncodingsList.UpdateEncodingsAfterLoadFromXml();
        }

        private void CopyFrom(SettingsData s)
        {
            HotKeyInfo = s.HotKeyInfo;
            MenuMaxLen = s.MenuMaxLen;
            BufferMaxLen = s.BufferMaxLen;
            ShowSnapShot = s.ShowSnapShot;
            ShowDebug = s.ShowDebug;
            EncodingsList = s.EncodingsList;
            IsAutoReconnect = s.IsAutoReconnect;
            IsAutoUAC = s.IsAutoUAC;
            IsAbortShutdown = s.IsAbortShutdown;
            IsStopServices = s.IsStopServices;
        }

        private const string _strAppKey = @"ClipboardHistoryMZ";
        private Microsoft.Win32.RegistryKey OpenRegKey(bool writable)
        {
            //const string REG_KEY = @"Software\Microsoft\Windows\CurrentVersion\Run";
            const string REG_KEY = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run";
            try
            {
                return Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REG_KEY, writable);
            }
            catch (Exception err)
            {
                CenteredMessageBox.MsgBoxErr(err.Message);
                return null;
            }
        }
    }

    public class Settings
    {
        public SettingsData I = new SettingsData();

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

                listMain.MAX_HISTORY = I.BufferMaxLen;
                listFavorites.MAX_HISTORY = I.BufferMaxLen;

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
