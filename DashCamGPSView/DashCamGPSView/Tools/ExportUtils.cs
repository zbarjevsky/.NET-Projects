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
        public static void SaveGPSData(List<GpsPointData> points, string fileName)
        {
            string ext = Path.GetExtension(fileName);
            ExportType exportType = (ExportType)Enum.Parse(typeof(ExportType), ext.Trim('.'));

            if (exportType == ExportType.gpx)
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
            else if (exportType == ExportType.kml)
            {
                GPSDataParser.FileFormats.KML.kml kml = new GPSDataParser.FileFormats.KML.kml();
                kml.SaveToFile(points, fileName);

                //var coordinates = new SharpKml.Dom.CoordinateCollection();
                //foreach (GpsPointData i in points)
                //{
                //    coordinates.Add(new SharpKml.Base.Vector(i.Latitude, i.Longitude));
                //}

                //var style = new SharpKml.Dom.Style()
                //{
                //    Id = "lineStyleNormal",
                //    Line = new SharpKml.Dom.LineStyle() 
                //    { 
                //        Color = new SharpKml.Base.Color32(255, 255, 0, 0),
                //        Width = 4
                //    },
                //    Polygon = new SharpKml.Dom.PolygonStyle()
                //    {
                //        Color = new SharpKml.Base.Color32(255, 0, 255, 0)
                //    }
                //};

                //var lineString = new SharpKml.Dom.LineString()
                //{
                //    Extrude = true,
                //    Tessellate = true,
                //    AltitudeMode = SharpKml.Dom.AltitudeMode.Absolute,
                //    Coordinates = coordinates
                //};

                //var placemark = new SharpKml.Dom.Placemark
                //{
                //    Name = Path.GetFileNameWithoutExtension(fileName),
                //    StyleUrl = new Uri("#lineStyleNormal", UriKind.Relative),
                //    Geometry = lineString
                //};

                //placemark.AddStyle(style);

                //// This is the root element of the file
                //var kml = new SharpKml.Dom.Kml
                //{
                //    Feature = placemark,
                //};

                //// Package it all together...
                ////var document = new SharpKml.Dom.Document();
                ////document.AddFeature(placemark);
                ////document.AddStyle(style);

                //var serializer = new SharpKml.Base.Serializer();
                //serializer.Serialize(kml);
                //Console.WriteLine(serializer.Xml);

                //File.WriteAllText(fileName, serializer.Xml);
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
