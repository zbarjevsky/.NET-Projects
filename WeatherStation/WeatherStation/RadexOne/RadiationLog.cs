using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using MkZ.Physics;
using MkZ.Tools;
using RadexOneLib;

namespace MkZ.RadexOne
{
    public class RadiationDataPoint : Physics.IDataPoint
    {
        public DateTime Date { get; set; } = DateTime.Now;

        public double CPM, RATE, DOSE, Threshold = 80;

        public bool IsValid => RATE != 0.0;

        public double GetValue<T>(IUnitBase<T> measurementType) where T : struct, IConvertible
        {
            return measurementType.Convert(RATE);
        }

        public override string ToString()
        {
            return string.Format("{0} - Rate: {1:0.00} µSv/h, CPM: {2}, Dose: {3:##0.00} µSv, Threshold: {4} CPM", 
                Date.ToString("s"), RATE, CPM, DOSE, Threshold);
        }
    }

    public class RadiationLog
    {
        private const string FILE_NAME = "C:\\Temp\\Radex\\RadiationLog.xml";
        private const string FILE_NAME_OLD = "C:\\Temp\\Radex\\RadiationLog_{0}.xml";

        public RadexSerialNumber SerialNumber { get; set; }
        public List<RadiationDataPoint> Log = new List<RadiationDataPoint>(1024);

        public RadiationLog()
        {
            string dir = Path.GetDirectoryName(FILE_NAME);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public void UpdateSerialNumber(RadexSerialNumber sn)
        {
            if (SerialNumber == null)
            {
                SerialNumber = sn;
            }
            else if (SerialNumber != sn)
            {
                Clear();
                SerialNumber = sn;
            }
        }

        public void Load()
        {
            try
            {
                RadiationLog log = XmlHelper.Open<RadiationLog>(FILE_NAME);
                SerialNumber = log.SerialNumber;
                Log = log.Log;
            }
            catch (Exception err)
            {
            }
        }

        public void Save()
        {
            try
            { 
                XmlHelper.Save(FILE_NAME, this);
            }
            catch (Exception err)
            {
            }
        }

        internal void Clear()
        {
            string fileName = string.Format(FILE_NAME_OLD, DateTime.Now.ToString("yyyyMMdd-HHmmss-f"));
            XmlHelper.Save(fileName, this);

            Log.Clear();
        }
    }
}
