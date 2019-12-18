using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GPSDataParser.FileFormats.KML
{

    [Serializable]
    public class FolderPath
    {
        [XmlElement("name")]
        public string name = "route";

        [XmlElement("Placemark")]
        public PlacemarkPath placemarkPath = new PlacemarkPath();

        public FolderPath() //: base("Route")
        {

        }

        public FolderPath(List<GpsPointData> route, string lineStyleMapId) : this()
        {
            placemarkPath = new PlacemarkPath(route, lineStyleMapId);
        }
    }

    [Serializable]
    [XmlType("Folder")]
    public class FolderPoints
    {
        [XmlElement("name")]
        public string name = "points";

        [XmlElement("Placemark")]
        public PlacemarkPoint[] placemarkPoint;

        public FolderPoints() { }

        public FolderPoints(List<GpsPointData> route, string iconStyleMapName) : this()
        {
            placemarkPoint = new PlacemarkPoint[route.Count];
            for (int i = 0; i < route.Count; i++)
            {
                placemarkPoint[i] = new PlacemarkPoint(route[i], i, iconStyleMapName);
            }
        }
    }
}
