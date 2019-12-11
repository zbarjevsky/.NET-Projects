using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GPSDataParser.FileFormats
{

    //Schema: http://www.topografix.com/GPX/1/1/
    //GPX is the standardized file format for GPS file exchanges.
    //A GPX file can contain a lot of different kinds of information.
    //Take a look at the schema <here>. In general, the major things that you will work with are:
    //
    //Waypoints
    //A waypoint is a specific position that is manually marked by a user for future reference. 
    //So when you get to the suspension bridge, mark a waypoint and you can find it again later as well as tell everyone else about it.

    //Tracks
    //Tracks are where you've been. When I want to mark out a trail for users of my application, 
    //I set up my GPS on my bike and just go for a ride. 
    //GPS antennae have come a long way in the last few years and my inexpensive Garmin eTrex keeps pretty accurate markers 
    //even when I'm in a deep draw.When I get home I have a complete listing of a few hundred points on my route, 
    //depending on how far apart or how long to wait I've preset my GPS to mark between saved track points.

    //Routes
    //A route is what you load into your GPS. 
    //It's essentially a list of positions you build by looking at a map or a file you get from someone else's track. 
    //When loaded, it will direct you to each point along the route in the appropriate order.

    //My Garmin saves files in a GDB format which is proprietary for the product. 
    //I load this file onto a machine with Garmin MapSource and immediately save the file as a GPX. This gets it into the standardized format that nearly all other GPS units and mapping software can use and I'm ready to load my data. At the end of this post is a well formed (but incomplete) GPX file. The original file had about 6,500 lines in it.

   [XmlRoot("gpx")]
    public class Gpx
    {
        [XmlElement("wpt")] 
        public Waypoint [] waypoints;
    }

    [XmlRoot("gpx", Namespace = "http://www.topografix.com/GPX/1/0")]
    public class GpxV1 : Gpx 
    { 
    }

    [XmlRoot("gpx", Namespace = "http://www.topografix.com/GPX/1/1")]
    public class GpxV2 : Gpx 
    { 
    }

    [XmlRoot("wpt")]
    public class Waypoint
    {
        [XmlAttribute("lat")]
        public double lat;
        [XmlAttribute("lon")]
        public double lon;

        private GpsPointData data;

        public Waypoint()
        {

        }

        public Waypoint(GpsPointData i)
        {
            this.data = i;
            lat = i.Latitude;
            lon = i.Longitude;
        }
    }
}
