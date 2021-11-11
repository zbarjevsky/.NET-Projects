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
    public enum SpeedUnits
    {
        mph,
        kmh,
        knots
    }

    public class GpsPosition
    {
        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude;

        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude;

        public GpsPosition()
        {

        }

        public GpsPosition(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    public class GpsPointData
    {
        public GpsPointData(Rmc rmc)
        {
            FixTime = rmc.FixTime;
            Active = rmc.Active;
            Latitude = rmc.Latitude;
            Longitude = rmc.Longitude;
            GpsSpeed = rmc.Speed;
            Course = rmc.Course;
            MagneticVariation = rmc.MagneticVariation;
            Altitude = 0;
        }

        public GpsPointData(ViofoGpsPoint gps)
        {
            FixTime = gps.Date;
            Active = gps.IsActive;
            Latitude = gps.Latitude;
            Longitude = gps.Longtitude;
            GpsSpeed = gps.Speed;
            Course = gps.Bearing;
            MagneticVariation = 0;
            Altitude = 0;
        }

        public GpsPosition Position { get { return new GpsPosition(Latitude, Longitude); } }

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
        /// Altitude
        /// </summary>
        public double Altitude { get; }

        /// <summary>
        /// Speed over the ground in knots
        /// </summary>
        public double GpsSpeed { get; }

        /// <summary>
        /// speed in selected units
        /// </summary>
        public double GetSpeed(SpeedUnits speedUnits)
        {
            switch (speedUnits)
            {
                case SpeedUnits.mph: return SpeedMph;
                case SpeedUnits.kmh: return SpeedKmh;
                case SpeedUnits.knots:
                default:
                    return GpsSpeed;
            }
        }

        public double SpeedMph
        {
            get { return GpsSpeed * 1.15078; }
        }

        public double SpeedKmh
        {
            get { return GpsSpeed * 1.852; }
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
                ", Lat: " + SexagesimalAngle.ToString(Latitude) + ", Lon: " + SexagesimalAngle.ToString(Longitude) + 
                ", Date: " + FixTime.ToString("yyyy/MM/dd HH:mm:ss.fff");
            return gps;
        }

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
                if (msg != null)
                    data.Add(new GpsPointData(msg));
            }
            return data;
        }
    }
}
