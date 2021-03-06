﻿using System;
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

        public PlacemarkPoint(List<GpsPointData> gps, int idx,
            string iconStyleMapNameGreen, string iconStyleMapNameYellow, string iconStyleMapNameRed) 
            : this()
        {
            TimeSpan duration = (gps[idx].FixTime - gps[0].FixTime);
            
            name = "Location: " + idx;
            description = string.Format("Point {0} from start", duration);
            styleUrl = "#" + PointColor(gps[idx], iconStyleMapNameGreen, iconStyleMapNameYellow,  iconStyleMapNameRed);
            extendedData = new ExtendedData()
            {
                data = new ExtendedData.KmlData[]
                {
                    new ExtendedData.KmlData() { name = "TimeStamp", value = gps[idx].FixTime.ToString() },
                    new ExtendedData.KmlData() { name = "Speed", value = gps[idx].SpeedMph.ToString("0.0 mph") },
                    new ExtendedData.KmlData() { name = "Heading", value = gps[idx].Course.ToString("0.0") },
                    new ExtendedData.KmlData() { name = "Lattitude", value = gps[idx].Latitude.ToString("0.000000")},
                    new ExtendedData.KmlData() { name = "Longtitude", value = gps[idx].Longitude.ToString("0.000000")}
                }
            };
            point = new KmlPoint(gps[idx]);
        }

        private string PointColor(GpsPointData gps, string iconStyleMapNameGreen, string iconStyleMapNameYellow, string iconStyleMapNameRed)
        {
            if (gps.SpeedMph < 20)
                return iconStyleMapNameGreen;
            if (gps.SpeedMph > 60)
                return iconStyleMapNameRed;
            return iconStyleMapNameYellow;
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
    public class PlacemarkRoute : Placemark
    {
        [XmlElement("LineString", Order = 5)]
        public LineString lineString = new LineString();

        public PlacemarkRoute() : base("Route", "Navigation Line")
        {

        }

        public PlacemarkRoute(List<GpsPointData> route, string lineStyleMapId) : this()
        {
            TimeSpan duration = route.Last().FixTime - route.First().FixTime;

            name = "Route";
            description = "Duration: " + duration;
            styleUrl = "#" + lineStyleMapId;
            extendedData = new ExtendedData()
            {
                data = new ExtendedData.KmlData[]
                {
                    new ExtendedData.KmlData() { name = "Start Time", value = route.First().FixTime.ToString("s") },
                    new ExtendedData.KmlData() { name = "Average Speed", value = AverageSpeed(route).ToString("0.0 mph") },
                }
            };
            lineString = new LineString(route);
        }

        private double AverageSpeed(List<GpsPointData> route)
        {
            double speed = 0;
            foreach (GpsPointData item in route)
            {
                speed += item.SpeedMph;
            }
            speed /= route.Count;
            return speed;
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
