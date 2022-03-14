using MkZ.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MkZ.Physics
{
    public enum eBucketingType
    {
        Average,
        Maximum
    }

    public abstract class IDataPoint
    {
        [XmlIgnore]
        public abstract bool IsValid { get; }
        public abstract DateTime Date { get; set; }
        [XmlIgnore]
        public abstract double[] Values { get; protected set; }
        public abstract double GetValue<T>(IUnitBase<T> measurementType) where T : struct, IConvertible;

        public void CopyFrom(IDataPoint copyData) 
        {
            Date = copyData.Date;
            Values = new double[copyData.Values.Length];
            Array.Copy(copyData.Values, Values, copyData.Values.Length);
        }

        public const int MIN_RECORDS_TO_FILTER = 1000;

        public static List<T> ThinningByTime<T>(List<T> recordsIn, double bucketIntervalInSec, eBucketingType bucketingType, bool includeInvalidPoints) where T : IDataPoint, new()
        {
            lock (recordsIn)
            {
                if (recordsIn.Count < MIN_RECORDS_TO_FILTER || bucketIntervalInSec <= 1.0) //return all records, convert units
                    return new List<T>(recordsIn);

                DateTime first = recordsIn.First().Date;
                DateTime last = recordsIn.Last().Date;
                TimeSpan interval = last - first;

                int i = 0;
                int bucketStart = i;
                int bucketIndex = 0;

                List<T> recordsOut = new List<T>();

                for (; i < recordsIn.Count - 1; i++)
                {
                    double secondsFromFirst = (recordsIn[i].Date - first).TotalSeconds;
                    int idx = (int)(secondsFromFirst / bucketIntervalInSec);
                    if (idx > bucketIndex) //next bucket
                    {
                         List<T> rec = GetBucketValue(recordsIn, bucketStart, i - bucketStart, bucketingType, includeInvalidPoints);

                        recordsOut.AddRange(rec);
                        bucketStart = i;
                        bucketIndex = idx;
                    }
                }

                //last bucket - always add last records as is
                if (bucketStart < recordsIn.Count - 2)
                {
                    for (int j = bucketStart; j < recordsIn.Count; j++)
                    {
                        T rec = new T();
                        rec.CopyFrom(recordsIn[j]);
                        recordsOut.Add(rec);
                    }
                }
                else
                {
                    T rec = new T();
                    rec.CopyFrom(recordsIn.Last());
                    recordsOut.Add(rec);
                }

                return recordsOut;
            }
        }

        private static List<T> GetBucketValue<T>(List<T> records, int start, int bucketSize, eBucketingType bucketingType, bool includeInvalidPoints) where T : IDataPoint, new()
        {
            List<T> list = new List<T>();
            if (start >= records.Count || bucketSize <= 0)
                return list;

            if (bucketSize == 1)
            {
                T rec0 = new T();
                rec0.CopyFrom(records[start]);
                list.Add(rec0);
                return list;
            }

            if (!includeInvalidPoints)
            {
                T rec0 = GetBucketValueForValidRecords(records, start, bucketSize, bucketingType);
                list.Add(rec0);
                return list;
            }

            //break into sub buckets if there are invalid items
            //each invalid item will be inserted, other will be calculated sub bucket value
            int subBucketStart = start;
            int subBucketSize = 0;
            for (int i = start; i < records.Count && i < (start + bucketSize); i++)
            {
                if (!records[i].IsValid)
                {
                    if (subBucketSize > 0)
                    {
                        T rec1 = GetBucketValueForValidRecords(records, subBucketStart, subBucketSize, bucketingType);
                        list.Add(rec1);
                    }

                    subBucketStart = i + 1;
                    subBucketSize = 0;

                    T rec0 = new T();
                    rec0.CopyFrom(records[i]);
                    list.Add(rec0);

                    continue;
                }

                subBucketSize++;
            }

            if (subBucketSize > 0)
            {
                T rec1 = GetBucketValueForValidRecords(records, subBucketStart, subBucketSize, bucketingType);
                list.Add(rec1);
            }

            return list;
        }

        //use to calculate one value for backet that has valid values only
        private static T GetBucketValueForValidRecords<T>(List<T> records, int start, int bucketSize, eBucketingType bucketingType) where T : IDataPoint, new()
        {
            List<T> list = new List<T>();
            if (start >= records.Count || bucketSize <= 0)
                return default(T);

            T rec = new T();
            rec.CopyFrom(records[start]);
            if (bucketSize == 1)
                return rec;

            int count = 1;
            int validCount = 1;
            for (int i = start + 1; i < records.Count && i < (start + bucketSize); i++, count++)
            {
                if (records[i].IsValid)
                {
                    validCount++;
                    if (bucketingType == eBucketingType.Maximum)
                    {
                        for (int v = 0; v < rec.Values.Length; v++)
                        {
                            rec.Values[v] = Math.Max(rec.Values[v], records[i].Values[v]);
                        }
                    }
                    else if (bucketingType == eBucketingType.Average)
                    {
                        for (int v = 0; v < rec.Values.Length; v++)
                        {
                            rec.Values[v] += records[i].Values[v];
                        }
                    }
                }
            }

            if (bucketingType == eBucketingType.Average)
            {
                for (int v = 0; v < rec.Values.Length; v++)
                {
                    rec.Values[v] /= validCount;
                }

                TimeSpan interval = records[start + count - 1].Date - records[start].Date;
                TimeSpan halfInterval = TimeSpan.FromMilliseconds(interval.TotalMilliseconds / 2);
                rec.Date = records[start].Date + halfInterval;

                if (interval.TotalSeconds < 1.0)
                {
                    Log.d("Interval is too small: idx: {0} count: {1} time: {2}", start, bucketSize, interval);
                }
            }
            else if (bucketingType == eBucketingType.Maximum)
            {
                //rec.Date = records[start].Date; //already set
            }

            return rec;
        }
    }
}
