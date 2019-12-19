using GPSDataParser;
using GPSDataParser.FileFormats;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DashCamGPSView.Tools
{
    public enum ExportType
    {
        gpx,
        kml
    }

    public class ExportUtils
    {
        public static void SaveGPSData(List<GpsPointData> points, int filterIndex, string fileName)
        {
            string ext = Path.GetExtension(fileName);
            ExportType exportType = (ExportType)Enum.Parse(typeof(ExportType), ext.Trim('.'));

            if (filterIndex == 3)//(exportType == ExportType.gpx)
            {
                List<Waypoint> list = new List<Waypoint>();
                foreach (GpsPointData i in points)
                {
                    list.Add(new Waypoint(i));
                }

                GpxV2 v2 = new GpxV2();
                v2.waypoints = list.ToArray();
                string xml = SerializeToString(v2);
                File.WriteAllText(fileName, xml);
            }
            else if (filterIndex == 2) //simple KML (exportType == ExportType.kml)
            {
                kml kml = new kml();
                kml.SaveToFile(points, fileName);
            }
            else if (filterIndex == 1) //extended KML (exportType == ExportType.kml)
            {
                GPSDataParser.FileFormats.KML.KmlEx kml = new GPSDataParser.FileFormats.KML.KmlEx();
                kml.SaveToFile(points, fileName);
            }
        }

        public static string SerializeToString(object o)
        {
            string retVal = string.Empty;
            using (StringWriter writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                serializer.Serialize(writer, o);
                retVal = writer.ToString();
            }
            return retVal;
        }

    }
}
