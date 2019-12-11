using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GPSDataParser.FileFormats.KML
{
    [XmlRoot("kml", Namespace = "http://www.opengis.net/kml/2.2")]
    public class kml
    {
        public Placemark Placemark;

        public void SaveToFile(List<GpsPointData> route, string fileName)
        {
            Placemark = new Placemark(route, fileName);
            string xml = SerializeToString(this);
            File.WriteAllText(fileName, xml);
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

    public class Placemark
    {
        public string name;
        public string styleUrl;
        public Style Style;
        public LineString LineString;

        public Placemark()
        {

        }

        public Placemark(List<GpsPointData> route, string fileName)
        {
            name = Path.GetFileNameWithoutExtension(fileName);

            Style = new Style();
            styleUrl = "#" + Style.id;
            LineString = new LineString(route);
        }
    }

    public class Style
    {
        [XmlAttribute("id")]
        public string id = "lineStyleNormal";
        public LineStyle LineStyle = new LineStyle();
    }

    public class LineStyle
    {
        public string color = "ffff0000";
        public double width = 4;
    }

    public class LineString
    {
        public string extrude = "true";
        public string tessellate = "true";
        public string altitudeMode = "absolute";
        public string coordinates;

        public LineString()
        {

        }

        public LineString(List<GpsPointData> route)
        {
            coordinates = "";
            StringBuilder sb = new StringBuilder(1024);
            foreach (GpsPointData data in route)
            {
                sb.AppendFormat("{0}, {1}\r\n", data.Longitude, data.Latitude);
            }
            coordinates = sb.ToString().Trim('\r', '\n');
        }
    }
}
