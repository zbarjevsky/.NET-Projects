using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GPSDataParser.FileFormats.KML
{
    public class KmlPlacemarks
    {
    }

    [Serializable]
    public abstract class Placemark
    {
        [XmlElement("name", Order = 1)]
        public string name { get; set; }
        [XmlElement("description", Order = 2)]
        public string description { get; set; }
        [XmlElement("styleUrl", Order = 3)]
        public string styleUrl { get; set; }
        [XmlElement("ExtendedData", Order = 4)]
        public ExtendedData extendedData;

        public Placemark() { }

        public Placemark(string name = "", string description = "")
        {
            this.name = name;
            this.description = description;
        }

        [XmlType("ExtendedData")]
        public class ExtendedData
        {
            [XmlElement("Data")]
            public KmlData[] data = new KmlData[0];

            public class KmlData
            {
                [XmlAttribute("name")]
                public string name = "";
                [XmlElement("value")]
                public string value = "";
            }
        }
    }

    [Serializable]
    public class PlacemarkPoint : Placemark
    {
        [XmlElement("Point", Order = 5)]
        public KmlPoint point = new KmlPoint();

        public PlacemarkPoint() : base("Point", "One Point") { }

        public PlacemarkPoint(GpsPointData gps, int idx, string iconStyleMapId) : this()
        {
            name = "Point: " + idx;
            description = "1 route point";
            styleUrl = "#" + iconStyleMapId;
            extendedData = new ExtendedData()
            {
                data = new ExtendedData.KmlData[]
                {
                    new ExtendedData.KmlData() { name = "TimeStamp", value = gps.FixTime.ToString() },
                    new ExtendedData.KmlData() { name = "Speed", value = gps.SpeedMph.ToString("0.0 mph") },
                    new ExtendedData.KmlData() { name = "Heading", value = gps.Course.ToString("0.0") },
                    new ExtendedData.KmlData() { name = "Lattitude", value = gps.Latitude.ToString("0.000000")},
                    new ExtendedData.KmlData() { name = "Longtitude", value = gps.Longitude.ToString("0.000000")}
                }
            };
            point = new KmlPoint(gps);
        }

        [Serializable]
        public class KmlPoint
        {
            public string coordinates = "";

            public KmlPoint() { }

            public KmlPoint(GpsPointData gps = null)
            {
                if (gps != null)
                    coordinates = string.Format("{0},{1}", gps.Longitude, gps.Latitude);
            }
        }
    }

    [Serializable]
    public class PlacemarkPath : Placemark
    {
        [XmlElement("LineString", Order = 5)]
        public LineString lineString = new LineString();

        public PlacemarkPath() : base("Route", "Navigation Line")
        {

        }

        public PlacemarkPath(List<GpsPointData> route, string lineStyleMapId) : this()
        {
            name = "Route";
            description = "1 minute route";
            styleUrl = "#" + lineStyleMapId;
            extendedData = new ExtendedData()
            {
                data = new ExtendedData.KmlData[]
                {
                    new ExtendedData.KmlData() { name = "TimeStamp", value = "First Point time here"},
                    new ExtendedData.KmlData() { name = "Average Speed", value = "First Point Speed here"},
                }
            };
            lineString = new LineString(route);
        }

        public class LineString
        {
            public string extrude = "1";
            public string tessellate = "1";
            public string altitudeMode = "clampToGround"; //"absolute"
            public string coordinates;

            public LineString() { }

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
}
