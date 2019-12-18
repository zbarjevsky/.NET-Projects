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
    public class BaseIdTag
    {
        [XmlAttribute("id")]
        public string id = "";
    }

    //https://developers.google.com/kml/documentation/kmlreference
    //https://www.gpsvisualizer.com/map_input?form=googleearth
    //https://mygeodata.cloud/converter/nmea-to-kml

    [XmlRoot("kml", Namespace = "http://www.opengis.net/kml/2.2")]
    public class KmlEx
    {
        [XmlElement("Document")]
        public Document document;

        public void SaveToFile(List<GpsPointData> route, string fileName)
        {
            document = new Document(route, fileName);
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

    [Serializable]
    [XmlRoot("Document")]
    public class Document
    {
        [XmlElement("name", Order = 1)]
        public string name { get; set; } = "Route";
        [XmlElement("description", Order = 2)]
        public string description { get; set; } = "";

        [XmlElement("Style", Order = 3)]
        public StyleGroupIcon style1 { get; set; }
        [XmlElement("Style", Order = 4)]
        public StyleGroupIcon style2 { get; set; }
        [XmlElement("Style", Order = 5)]
        public StyleGroupLine style3 { get; set; }

        [XmlElement("Folder", Order = 6)]
        public FolderPath path { get; set; }
        [XmlElement("Folder", Order = 7)]
        public FolderPoints points { get; set; }

        public Document() { }

        public Document(List<GpsPointData> route, string fileName)
        {
            name = Path.GetFileNameWithoutExtension(fileName);
            description = name;

            style1 = new StyleGroupIcon("icon-1739-0288D1");
            style2 = new StyleGroupIcon("icon-1899-0288D1");
            style3 = new StyleGroupLine("line-FF0000-1000");

            path = new FolderPath(route, style1.name);
            points = new FolderPoints(route, style2.name);
        }
    }
}
