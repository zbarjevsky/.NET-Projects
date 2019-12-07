using NmeaParser.Nmea;
using NovatekViofoGPSParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSDataParser
{
    public class GpsPointData
    {
        public static List<GpsPointData> Convert(List<NmeaMessage> list)
        {
            if (list == null || list.Count == 0)
                return null;

            List<GpsPointData> data = new List<GpsPointData>();
            foreach (NmeaMessage msg in list)
            {
                if (msg != null)
                    data.Add(new GpsPointData(msg as Rmc));
            }
            return data;
        }

        public static List<GpsPointData> Convert(List<ViofoGpsPoint> list)
        {
            if (list == null || list.Count == 0)
                return null;

            List<GpsPointData> data = new List<GpsPointData>();
            foreach (ViofoGpsPoint msg in list)
            {
                if(msg != null)
                    data.Add(new GpsPointData(msg));
            }
            return data;
        }

        public GpsPointData(Rmc rmc)
        {
            FixTime = rmc.FixTime;
            Active = rmc.Active;
            Latitude = rmc.Latitude;
            Longitude = rmc.Longitude;
            Speed = rmc.Speed;
            Course = rmc.Course;
            MagneticVariation = rmc.MagneticVariation;
        }

        public GpsPointData(ViofoGpsPoint gps)
        {
            FixTime = gps.Date;
            Active = gps.IsActive;
            Latitude = gps.Latitude;
            Longitude = gps.Longtitude;
            Speed = gps.Speed;
            Course = gps.Bearing;
            MagneticVariation = 0;
        }

        /// <summary>
        /// Fix Time
        /// </summary>
        public DateTime FixTime { get; }

        /// <summary>
        /// Gets a value whether the device is active
        /// </summary>
        public bool Active { get; }

        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Speed over the ground in knots
        /// </summary>
        public double Speed { get; }

        public double SpeedMph
        {
            get { return Speed * 1.15078; }
        }

        public double SpeedKmh
        {
            get { return Speed * 1.852; }
        }

        /// <summary>
        /// Track angle in degrees True
        /// </summary>
        public double Course { get; }

        /// <summary>
        /// Magnetic Variation
        /// </summary>
        public double MagneticVariation { get; }

        public override string ToString()
        {
            string gps = (Active ? "Active, Speed: " : "Inactive, Speed: ") + SpeedMph.ToString("0.0") + " mph" +
                ", Lat: " + SexagesimalAngle.ToString(Latitude) + ", Lon: " + SexagesimalAngle.ToString(Longitude);
            return gps;
        }
    }
}
