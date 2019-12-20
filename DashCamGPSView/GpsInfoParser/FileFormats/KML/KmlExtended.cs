using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
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

            //create kmz
            string kmzFile = Path.GetFileNameWithoutExtension(fileName);
            kmzFile = Path.Combine(Path.GetDirectoryName(fileName), kmzFile + ".kmz");
            CreateZipFile(fileName, kmzFile);
        }

        public static void CreateZipFile(string fileName, string zipName)
        {
            if (File.Exists(zipName))
                File.Delete(zipName);

            //Creates a new, blank zip file to work with - the file will be
            //finalized when the using statement completes
            using (ZipArchive newFile = ZipFile.Open(zipName, ZipArchiveMode.Create))
            {
                newFile.CreateEntryFromFile(fileName, Path.GetFileName(fileName), CompressionLevel.Optimal);
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

    [Serializable]
    [XmlRoot("Document")]
    public class Document
    {
        [XmlElement("name", Order = 1)]
        public string name { get; set; } = "Route";
        [XmlElement("description", Order = 2)]
        public string description { get; set; } = "";

        [XmlElement("Style", typeof(StyleIcon), Order = 6)]
        [XmlElement("StyleMap", typeof(StyleMap), Order = 6)]
        public List<BaseIdTag> StylesIcons { get; set; } = new List<BaseIdTag>();

        [XmlElement("Style", typeof(StyleLine), Order = 7)]
        [XmlElement("StyleMap", typeof(StyleMap), Order = 7)]
        public List<BaseIdTag> StylesLines { get; set; } = new List<BaseIdTag>();

        [XmlElement("Folder", Order = 12)]
        public FolderPath path { get; set; }
        [XmlElement("Folder", Order = 13)]
        public FolderPoints points { get; set; }

        public Document() { }

        public Document(List<GpsPointData> route, string fileName)
        {
            name = Path.GetFileNameWithoutExtension(fileName);
            description = name;

            StyleGroupIcon grpIconGreen = new StyleGroupIcon(System.Drawing.Color.Green);
            StylesIcons.AddRange(grpIconGreen.ToArray());

            StyleGroupIcon grpIconYellow = new StyleGroupIcon(System.Drawing.Color.Yellow);
            StylesIcons.AddRange(grpIconYellow.ToArray());

            StyleGroupIcon grpIconRed = new StyleGroupIcon(System.Drawing.Color.Red);
            StylesIcons.AddRange(grpIconRed.ToArray());

            StyleGroupLine grpLine = new StyleGroupLine(System.Drawing.Color.DarkOliveGreen);
            StylesLines.AddRange(grpLine.ToArray());

            path = new FolderPath(route, grpLine.style3Map.id);
            points = new FolderPoints(route, grpIconGreen.style1Map.id, grpIconYellow.style1Map.id, grpIconRed.style1Map.id);
        }
    }
}
