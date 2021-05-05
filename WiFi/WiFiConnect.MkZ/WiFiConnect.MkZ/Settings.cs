using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MkZ.Tools;
using WiFiConnect.MkZ.Controls;

namespace WiFiConnect.MkZ
{
    public class Settings
    {
        public List<PingPoint> BufferFull { get; set; }

        [XmlIgnore]
        public List<PingPoint> BufferPings { get; set; }

        public static string DataFolder
        {
            get
            {
                return Log.DataFolder;
            }
        }

        public Settings()
        {
            BufferFull = new List<PingPoint>();
            BufferPings = new List<PingPoint>();
        }

        public void Save()
        {
            string fileName = string.Format("WiFi_Settings_Main.xml");
            fileName = Path.Combine(DataFolder, fileName);

            XmlHelper.Save(fileName, this);
        }

        public void Load()
        {
            string fileName = string.Format("WiFi_Settings_Main.xml");
            fileName = Path.Combine(DataFolder, fileName);

            Settings s = XmlHelper.Open<Settings>(fileName);
            BufferFull.AddRange(s.BufferFull);
            BufferPings.Clear();
        }

        public void Save(string fileName)
        {
            XmlHelper.Save(fileName, this);
        }
    }
}
