using MkZ.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.RadexOneLib
{
    public class RadexOneDeviceLog
    {
        public const string FILE_NAME_MAIN = "RadiationLog_{0}_Main.xml";
        private const string FILE_NAME_BAK = "RadiationLog_{0}_{1}.xml";

        public RadexOneDeviceInfo RadexOneDevice { get; set; } = new RadexOneDeviceInfo();
        public List<RadiationDataPoint> RadiationData = new List<RadiationDataPoint>(1024);

        public RadexOneDeviceLog()
        {
        }

        public void UpdateDeviceInfo(RadexOneDeviceInfo dev, string dataFolder)
        {
            if (RadexOneDevice.SerialNumber != dev.SerialNumber)
                BackupAndClear(dataFolder);

            RadexOneDevice = dev;
        }

        public RadiationDataPoint AddRecord(RadexOneDeviceInfo device, RadiationDataPoint data, string dataFolder)
        {
            UpdateDeviceInfo(device, dataFolder);
            RadiationData.Add(data);
            return data;
        }

        public List<RadiationDataPoint> GetLastRecords(TimeSpan interval)
        {
            List<RadiationDataPoint> records = new List<RadiationDataPoint>();
            if (RadiationData.Count < 2)
                return RadiationData;

            DateTime last = RadiationData.Last().Date;
            DateTime first = last - interval;

            for (int i = 0; i < RadiationData.Count; i++)
            {
                if (RadiationData[i].Date > first)
                    records.Add(RadiationData[i]);
            }

            return records;
        }

        public void Load(string fileName)
        {
            RadexOneDeviceLog log = Open(fileName);
            if (log != null)
            {
                RadexOneDevice.SerialNumber = log.RadexOneDevice.SerialNumber.Clone();
                RadiationData = log.RadiationData;
            }
            else
            {
                RadexOneDevice = new RadexOneDeviceInfo();
                RadiationData.Clear();
            }
        }

        public void Merge(RadexOneDeviceLog db)
        {
            lock (RadiationData)
            {
                List<RadiationDataPoint> records = MergeLinear(RadiationData, db.RadiationData);

                RadiationData.Clear();
                RadiationData.AddRange(records);
            }
        }

        //assuming all DB are sorted by date
        public static List<RadiationDataPoint> MergeLinear(List<RadiationDataPoint> r1, List<RadiationDataPoint> r2)
        {
            if ((r1 == null || r1.Count == 0) && (r2 == null || r2.Count == 0))
                return new List<RadiationDataPoint>();
            if (r1 == null || r1.Count == 0)
                return new List<RadiationDataPoint>(r2);
            if (r2 == null || r2.Count == 0)
                return new List<RadiationDataPoint>(r1);

            List<RadiationDataPoint> records = new List<RadiationDataPoint>(r1.Count);
            RadiationDataPoint current = null, last = null;

            int i1 = 0, i2 = 0;
            while (i1 < r1.Count || i2 < r2.Count)
            {
                if (i1 < r1.Count && i2 < r2.Count)
                {
                    if (r1[i1].Date == r2[i2].Date)
                    {
                        current = r1[i1];
                        i1++;
                        i2++;
                    }
                    else if (r1[i1].Date < r2[i2].Date)
                    {
                        current = r1[i1];
                        i1++;
                    }
                    else
                    {
                        current = r2[i2];
                        i2++;
                    }
                }
                else if (i1 < r1.Count)
                {
                    current = r1[i1];
                    i1++;
                }
                else if (i2 < r2.Count)
                {
                    current = r2[i2];
                    i2++;
                }


                if (last == null || current.Date != last.Date)
                {
                    records.Add(current);
                    last = current;
                }
            }

            return records;
        }

        public static RadexOneDeviceLog Open(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            try
            {
                RadexOneDeviceLog log = XmlHelper.Open<RadexOneDeviceLog>(fileName);
                return log;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public void SaveMain(string dataFolder)
        {
            try
            {
                string fileName = string.Format(FILE_NAME_MAIN, RadexOneDevice.SerialNumber.ToString().Replace("/", ""));
                XmlHelper.Save(Path.Combine(dataFolder, fileName), this);
            }
            catch (Exception err)
            {
                Debug.WriteLine("Exception in SaveMain {0} --> {1}", dataFolder, err.ToString());
            }
        }

        public void SaveBackup(string dataFolder)
        {
            try
            {
                string fileName = string.Format(FILE_NAME_BAK, RadexOneDevice.SerialNumber.ToString().Replace("/", ""), DateTime.Now.ToString("yyyyMMdd-HHmmss-f"));
                XmlHelper.Save(Path.Combine(dataFolder, fileName), this);
            }
            catch (Exception err)
            {
                Debug.WriteLine("Exception in SaveBackup {0} --> {1}", dataFolder, err.ToString());
            }
        }


        public void BackupAndClear(string dataFolder)
        {
            SaveBackup(dataFolder);

            RadexOneDevice = new RadexOneDeviceInfo();
            RadiationData.Clear();
        }
    }
}
