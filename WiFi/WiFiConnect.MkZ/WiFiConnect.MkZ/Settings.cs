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

        public List<PingPoint> GetBuffer(bool bPingBuffer)
        {
            if (bPingBuffer)
                return BufferPings;
            return BufferFull;
        }

        private TimeSpan _maxBufferSize = TimeSpan.FromHours(1);
        public void UpdateBuffers(PingPoint ping, TimeSpan maxBufferSize)
        {
            _maxBufferSize = maxBufferSize;

            if (ping != null)
            {
                BufferPings.Add(new PingPoint(ping));
                UpdateFullPerMinute(ping);
            }

            while (BufferFull.Count > 100 * 1000)
                BufferFull.RemoveAt(0);

            DateTime now = DateTime.Now;
            while (BufferPings.Count > 0 && (now - BufferPings[0].Date) > _maxBufferSize)
                BufferPings.RemoveAt(0);
        }

        private void UpdateFullPerMinute(PingPoint ping)
        {
            if(BufferFull.Count == 0)
            {
                BufferFull.Add(ping);
            }
            else
            {
                PingPoint prev = BufferFull.Last();
                if (ping.Value > 0 && ping.Date.Date == prev.Date.Date && 
                    ping.Date.Hour == prev.Date.Hour && ping.Date.Minute == prev.Date.Minute)
                {
                    prev.Value = Math.Max(prev.Value, ping.Value);
                    prev.Error = Math.Max(prev.Error, ping.Error);
                }
                else
                {
                    BufferFull.Add(ping);
                }
            }
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

            BufferPings.Clear();
            BufferFull.Clear();

            if (File.Exists(fileName))
            {
                Settings s = XmlHelper.Open<Settings>(fileName);
                BufferFull.AddRange(s.BufferFull);
            }
        }

        public void Save(string fileName)
        {
            XmlHelper.Save(fileName, this);
        }
    }
}
