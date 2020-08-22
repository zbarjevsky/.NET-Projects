﻿using ClipboardManager.Zip;
using MZ.Tools;
using MZ.WPF;
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
        [Browsable(false)]
        public bool ShowSnapShot { get; set; } = true;
        [Browsable(false)]
        public bool ShowDebug { get; set; } = true;
        public bool IsAutoReconnect { get; set; } = true;
        [DisplayName("Automatically reset UAC")]
        public bool IsAutoUAC { get; set; } = false;
        public bool IsAbortShutdown { get; set; } = false;

        private bool _isCorrectMouse = false;
        [Description("Correct Mouse Pointer if Stuck Between Multiple Monitors")]
        public bool IsCorrectMouse { get { return _isCorrectMouse; } set { _isCorrectMouse = value; UpdateMouseCorrection(_isCorrectMouse); } }

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

                //update selected encodings
                s.UpdateEncodingsAfterLoadFromXml();

                //always add "SMS Agent Host"
                s.ServicesManipulatorSettings.UpdateListAfterLoad();

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
            IsAutoReconnect = s.IsAutoReconnect;
            IsAutoUAC = s.IsAutoUAC;
            IsAbortShutdown = s.IsAbortShutdown;
            IsCorrectMouse = s.IsCorrectMouse;
            ServicesManipulatorSettings = s.ServicesManipulatorSettings;
            EncodingsList = s.EncodingsList;
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

        public static void UpdateMouseCorrection(bool isCorrectMouse)
        {
            NonStickMouse.Instance.EnableMouseCorrection(isCorrectMouse);
        }
    }

    public class Settings
    {
        public SettingsData globalSettings = new SettingsData();

        public void Save(string sHistoryFileName, string sSettingsFileName, 
            ClipboardList listMain, ClipboardList listFavorites)
        {
            globalSettings.Save(sSettingsFileName);

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

                listMain.Save(root, zip);
                listFavorites.Save(root, zip);

                doc.PreserveWhitespace = true;
                XmlUtil.SaveXmlDocFormatted(doc, sHistoryFileName);

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
            globalSettings.Load(sSettingsFileName);
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

                listMain.Load(doc, icoDefault, Path.GetDirectoryName(sHistoryFileName));
                listFavorites.Load(doc, icoDefault, Path.GetDirectoryName(sHistoryFileName));

                listMain.MAX_HISTORY = globalSettings.BufferMaxLen;
                listFavorites.MAX_HISTORY = globalSettings.BufferMaxLen;

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
