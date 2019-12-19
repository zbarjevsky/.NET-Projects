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
        public StyleIcon style1IconNormal;
        [XmlElement("Style", Order = 4)]
        public StyleIcon style1IconHighLight;
        [XmlElement("StyleMap", Order = 5)]
        public StyleMap style1Map;

        [XmlElement("Style", Order = 6)]
        public StyleIcon style2IconNormal;
        [XmlElement("Style", Order = 7)]
        public StyleIcon style2IconHighLight;
        [XmlElement("StyleMap", Order = 8)]
        public StyleMap style2Map;

        [XmlElement("Style", Order = 9)]
        public StyleLine style3LineNormal;
        [XmlElement("Style", Order = 10)]
        public StyleLine style3LineHighLight;
        [XmlElement("StyleMap", Order = 11)]
        public StyleMap style3Map;

        [XmlElement("Folder", Order = 12)]
        public FolderPath path { get; set; }
        [XmlElement("Folder", Order = 13)]
        public FolderPoints points { get; set; }

        public Document() { }

        public Document(List<GpsPointData> route, string fileName)
        {
            name = Path.GetFileNameWithoutExtension(fileName);
            description = name;

            StyleGroupIcon.ConfigureGroup("icon-1739-0288D1", ref style1IconNormal, ref style1IconHighLight, ref style1Map);
            StyleGroupIcon.ConfigureGroup("icon-1899-0288D1", ref style2IconNormal, ref style2IconHighLight, ref style2Map);
            StyleGroupLine.ConfigureGroup("line-FF0000-1000", ref style3LineNormal, ref style3LineHighLight, ref style3Map);

            path = new FolderPath(route, style3Map.id);
            points = new FolderPoints(route, style1Map.id);
        }
    }
}
