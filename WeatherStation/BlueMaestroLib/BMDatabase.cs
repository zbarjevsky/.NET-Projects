using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


using MkZ.Bluetooth;
using MkZ.Physics;
using MkZ.Tools;

namespace MkZ.BlueMaestroLib
{
    public class BMDatabase
    {
        //public const int MIN_RECORDS_TO_FILTER = 1000;

        public BluetoothDevice Device { get; set; }

        public List<BMRecordCurrent> Records { get; } = new List<BMRecordCurrent>();

        //[XmlIgnore]
        //public UnitsDescriptor Units { get; set; } = new UnitsDescriptor();

        public static string DataFolder
        {
            get
            {
                return Log.DataFolder;
            }
        }

        public string GenerateFileName()
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("BMDatabase_{0}_{1}_{2}.xml", Device.Name, Device.Address, date);
            fileName = Path.Combine(DataFolder, fileName);
            return fileName;
        }

        //for serialization
        public BMDatabase()
        {

        }

        public BMDatabase(BluetoothDevice device)
        {
            Device = device;
        }

        public BMDatabase(BMDatabase db)
        {
            Device = db.Device;
            Records.AddRange(db.Records);
            //Units = new UnitsDescriptor(db.Units);
        }

        public BMRecordCurrent AddRecord(BluetoothDevice device, short rssi, DateTime recordDate, byte[] data)
        {
            BMRecordCurrent record = new BMRecordCurrent(device, rssi, recordDate, data);

            if (record.IsValid)
                lock (Records)
                {
                    BMRecordCurrent last = Records.LastOrDefault();
                    if (last != null && last == record) //not changed - update 
                    {
                        Records[Records.Count - 1] = record;
                    }
                    else //new record
                    {
                        if (last == null)
                        {
                            last = record;
                        }

                        Records.Add(record);
                    }
                }

            return record;
        }

        public void Merge(BMDatabase db)
        {
            lock (Records)
            {
                List<BMRecordCurrent> records = MergeLinear(Records, db.Records);
                
                Records.Clear();
                Records.AddRange(records);
            }
        }

        //takes 10x times more time than linear merge
        public static List<BMRecordCurrent> MergeSimple(List<BMRecordCurrent> r1, List<BMRecordCurrent> r2)
        {
            if ((r1 == null || r1.Count == 0) && (r2 == null || r2.Count == 0))
                return new List<BMRecordCurrent>();
            if (r1 == null || r1.Count == 0)
                return new List<BMRecordCurrent>(r2);
            if (r2 == null || r2.Count == 0)
                return new List<BMRecordCurrent>(r1);

            List<BMRecordCurrent> recordsIn = new List<BMRecordCurrent>(r1.Count);
            recordsIn.AddRange(r1);
            recordsIn.AddRange(r2);
            recordsIn.Sort((rec1, rec2) => rec1.Date.CompareTo(rec2.Date));

            //remove duplicates
            List<BMRecordCurrent> recordsOut = new List<BMRecordCurrent>(recordsIn.Count);
            recordsOut.Add(recordsIn[0]);
            for (int i = 1; i < recordsIn.Count; i++)
            {
                if (recordsIn[i].Date == recordsIn[i - 1].Date)
                    continue;
                recordsOut.Add(recordsIn[i]);
            }
            return recordsOut;
        }

        //assuming all DB are sorted by date
        public static List<BMRecordCurrent> MergeLinear(List<BMRecordCurrent> r1, List<BMRecordCurrent> r2)
        {
            if ((r1 == null || r1.Count == 0) && (r2 == null || r2.Count == 0))
                return new List<BMRecordCurrent>();
            if (r1 == null || r1.Count == 0)
                return new List<BMRecordCurrent>(r2);
            if (r2 == null || r2.Count == 0)
                return new List<BMRecordCurrent>(r1);

            List<BMRecordCurrent> records = new List<BMRecordCurrent>(r1.Count);
            BMRecordCurrent current = null, last = null;

            int i1 = 0, i2 = 0;
            while(i1 < r1.Count || i2 < r2.Count)
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

        public List<BMRecordCurrent> GetLastRecords(TimeSpan interval)
        {
            List<BMRecordCurrent> records = new List<BMRecordCurrent>();
            if (Records.Count < 2)
                return Records;

            DateTime last = Records.Last().Date;
            DateTime first = last - interval;

            for (int i = 0; i < Records.Count; i++)
            {
                if (Records[i].Date > first)
                    records.Add(Records[i]);
            }

            return records;
        }

        //public List<BMRecordCurrent> DilluteByPoint(List<BMRecordCurrent> recordsIn, int zoom)
        //{
        //    lock (recordsIn)
        //    {
        //        int bucketSize = (int)(zoom);
        //        if (bucketSize < 2 || bucketSize > (recordsIn.Count / 3)) //all records
        //            return new List<BMRecordCurrent>(recordsIn);

        //        DateTime first = recordsIn.First().Date;
        //        DateTime last = recordsIn.Last().Date;
        //        TimeSpan interval = last - first;

        //        List<BMRecordCurrent> recordsOut = new List<BMRecordCurrent>();
        //        for (int i = 0; i < recordsIn.Count - 1; i += bucketSize)
        //        {
        //            recordsOut.Add(IDataPoint.GetAverageValue<BMRecordCurrent>(recordsIn, i, bucketSize));
        //        }

        //        //anyway add last record as is
        //        recordsOut.Add(new BMRecordCurrent(recordsIn.Last()));

        //        return recordsOut;
        //    }
        //}

        //public static List<BMRecordCurrent> DilluteByTime(List<BMRecordCurrent> recordsIn, double bucketIntervalInSec) //default 15 min
        //{
        //    return IDataPoint.ThinningByTime<BMRecordCurrent>(recordsIn, bucketIntervalInSec);
        //    //lock (recordsIn)
        //    //{
        //    //    if (recordsIn.Count < MIN_RECORDS_TO_FILTER || bucketIntervalInSec <= 1.0) //return all records, convert units
        //    //        return new List<BMRecordCurrent>(recordsIn);

        //    //    DateTime first = recordsIn.First().Date;
        //    //    DateTime last = recordsIn.Last().Date;
        //    //    TimeSpan interval = last - first;

        //    //    int i = 0;
        //    //    int bucketStart = i;
        //    //    int bucketIndex = 0;

        //    //    List<BMRecordCurrent> recordsOut = new List<BMRecordCurrent>();

        //    //    for (; i < recordsIn.Count - 1; i++)
        //    //    {
        //    //        double secondsFromFirst = (recordsIn[i].Date - first).TotalSeconds;
        //    //        int idx = (int)(secondsFromFirst / bucketIntervalInSec);
        //    //        if (idx > bucketIndex) //next bucket
        //    //        {
        //    //            recordsOut.Add(GetAverageValue(recordsIn, bucketStart, i - bucketStart));
        //    //            bucketStart = i;
        //    //            bucketIndex = idx;
        //    //        }
        //    //    }

        //    //    //last bucket - always add last record as is
        //    //    if (bucketStart < recordsIn.Count - 2)
        //    //    {
        //    //        for (int j = bucketStart; j < recordsIn.Count; j++)
        //    //        {
        //    //            recordsOut.Add(new BMRecordCurrent(recordsIn[j]));
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        recordsOut.Add(new BMRecordCurrent(recordsIn.Last()));
        //    //    }

        //    //    return recordsOut;
        //    //}
        //}

        //private static BMRecordCurrent GetAverageValue(List<BMRecordCurrent> records, int start, int bucketSize)
        //{
        //    if (start >= records.Count || bucketSize <= 0)
        //        return new BMRecordCurrent();

        //    BMRecordCurrent rec = new BMRecordCurrent(records[start]);
        //    if (bucketSize == 1)
        //        return rec;

        //    int count = 1;
        //    int validCount = 1;
        //    for (int i = start + 1; i < records.Count && i < (start + bucketSize); i++, count++)
        //    {
        //        if (records[i].IsValid)
        //        {
        //            rec += records[i];
        //            validCount++;
        //        }
        //    }
        //    rec /= validCount;

        //    TimeSpan interval = records[start + count - 1].Date - records[start].Date;
        //    TimeSpan halfInterval = TimeSpan.FromMilliseconds(interval.TotalMilliseconds / 2);
        //    rec.Date = records[start].Date + halfInterval;

        //    if(interval.TotalSeconds < 1.0)
        //    {
        //        Log.d("Interval is too small: idx: {0} count: {1} time: {2}", start, bucketSize, interval);
        //    }

        //    return rec;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucketIntervalInSec">if == 1 - do not dillute</param>
        public void SaveAs(string fileName, double bucketIntervalInSec)
        {
            BMDatabase db = new BMDatabase(this);
            List<BMRecordCurrent> records = new List<BMRecordCurrent>(db.Records);
            db.Records.Clear();
            db.Records.AddRange(IDataPoint.ThinningByTime<BMRecordCurrent>(records, bucketIntervalInSec, eBucketingType.Average, false));

            Save(fileName, db);
        }

        public void SaveMain()
        {
            string fileName = string.Format("BMDatabase_{0}_{1}_Main.xml", Device.Name, Device.Address);
            fileName = Path.Combine(DataFolder, fileName);

            const double DILLUTE_INTERVAL = 300.0;
            SaveAs(fileName, DILLUTE_INTERVAL);
            //Save(fileName, this);
        }

        public static void Save(string fileName, BMDatabase db)
        {
            XmlHelper.Save(fileName, db);
        }

        public static BMDatabase Open(string fileName)
        {
            try
            {
                BMDatabase db = XmlHelper.Open<BMDatabase>(fileName);
                if (db.Device == null)
                    throw new Exception("Old file format");
                    
                return db;
            }
            catch (Exception err)
            {
                Log.e("File: {0} Error: {1}", fileName, err);
                return null;
            }
        }

        public override string ToString()
        {
            return string.Format("Count({0}) -- {1}", Records.Count, Device);
        }
    }
}
