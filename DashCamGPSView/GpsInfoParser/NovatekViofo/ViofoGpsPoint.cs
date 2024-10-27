using GPSDataParser;
using GPSDataParser.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovatekViofoGPSParser
{
    /// <summary>
    /// https://sergei.nz/extracting-gps-data-from-viofo-a119-and-other-novatek-powered-cameras/
    /// # Datetime data
    /// hour: unsigned little-endian int (4 bytes)
    /// minute: unsigned little-endian int (4 bytes)
    /// second: unsigned little-endian int (4 bytes)
    /// year: unsigned little-endian int (4 bytes)
    /// month: unsigned little-endian int (4 bytes)
    /// day: unsigned little-endian int (4 bytes)

    /// # Coordinate data
    /// active: string (1 byte) # satelite lock "A"=active, everything else (eg " ") lost reception
    /// latitude hemisphere: string (1 byte) # "N"=North or "S"=South
    /// longitude hemisphere: string (1 byte) # "E"=East or "W"=West
    /// unknown: string (1 byte) # No idea, always "0"? 
    /// latitude: little-endian float (4 bytes) # unusual format of DDDmm.mmmm D=degrees m=minutes
    /// longitude: little-endian float (4 bytes) # unusual format of DDDmm.mmmm D=degrees m=minutes
    /// speed: little-endian float (4 bytes) # Knots (the nautical kind)
    /// bearing: little-endian float (4 bytes) # degrees, not used in GPX.
    /// </summary>
    public class ViofoGpsPoint
    {
        public DateTime Date;
        public bool IsActive;
        public char Latitude_hemisphere, Longtitude_hemisphere;
        public byte Unknown;
        public double Latitude, Longtitude;
        public double Speed;
        public double Bearing;

        public static ViofoGpsPoint Parse(IBufferReader reader, uint expectedSize)
        {
            const uint OFFSET_V2_48 = 48, OFFSET_V1_16 = 16;
            const byte VIOFO_A119M2 = 44;

            long start = reader.Position;

            byte[] buffer = reader.ReadBuffer(expectedSize);
            
            reader.Position = start;

            uint size = reader.ReadUintBE();
            string type = reader.ReadString(4);
            string magic = reader.ReadString(4);

            if (expectedSize != size || type != "free" || magic != "GPS ")
                return null;

            ViofoGpsPoint gps = new ViofoGpsPoint();

            //# checking for weird Azdome 0xAA XOR "encrypted" GPS data. 
            //This portion is a quick fix.
            int payload_size = 254;
            byte c = reader.ReadByte();
            if (c == 0x05)
            {
                if (size < 254)
                    payload_size = (int)+size;

                reader.Position += 5; //???
                byte[] payload = reader.ReadBuffer(payload_size);
            }
            else if ((char)c == 'L')
            {
                reader.Position = start + OFFSET_V2_48;

                //# Datetime data
                int hour = (int)reader.ReadUintLE();
                int minute = (int)reader.ReadUintLE();
                int second = (int)reader.ReadUintLE();
                int year = (int)reader.ReadUintLE();
                int month = (int)reader.ReadUintLE();
                int day = (int)reader.ReadUintLE();

                try { gps.Date = new DateTime(2000 + year, month, day, hour, minute, second); }
                catch (Exception err) { Debug.WriteLine(err.ToString()); return null; }

                //# Coordinate data
                char active = (char)reader.ReadByte();
                gps.IsActive = (active == 'A');

                gps.Latitude_hemisphere = (char)reader.ReadByte(); 
                gps.Longtitude_hemisphere = (char)reader.ReadByte();
                gps.Unknown = reader.ReadByte();

                float lat = reader.ReadFloatLE();
                gps.Latitude = FixCoordinate(lat, gps.Latitude_hemisphere);

                float lon = reader.ReadFloatLE();
                gps.Longtitude = FixCoordinate(lon, gps.Longtitude_hemisphere);

                gps.Speed = reader.ReadFloatLE();
                gps.Bearing = reader.ReadFloatLE();

                return gps;
            }
            else if(c == 240 || c == VIOFO_A119M2)
            {
                reader.Position = start + OFFSET_V1_16;

                //# Datetime data
                int hour = (int)reader.ReadUintLE();
                int minute = (int)reader.ReadUintLE();
                int second = (int)reader.ReadUintLE();
                int year = (int)reader.ReadUintLE();
                int month = (int)reader.ReadUintLE();
                int day = (int)reader.ReadUintLE();

                try { gps.Date = new DateTime(2000 + year, month, day, hour, minute, second); }
                catch (Exception err) { Debug.WriteLine(err.ToString()); return null; }

                //# Coordinate data
                char active = (char)reader.ReadByte();
                gps.IsActive = (active == 'A');

                gps.Latitude_hemisphere = (char)reader.ReadByte();
                gps.Longtitude_hemisphere = (char)reader.ReadByte();
                gps.Unknown = reader.ReadByte();

                float lat = reader.ReadFloatLE();
                gps.Latitude = FixCoordinate(lat, gps.Latitude_hemisphere);

                float lon = reader.ReadFloatLE();
                gps.Longtitude = FixCoordinate(lon, gps.Longtitude_hemisphere);

                gps.Speed = reader.ReadFloatLE();
                gps.Bearing = reader.ReadFloatLE();

                return gps;
            }
            else if (c == 32)
            {
                reader.Position = start + OFFSET_V1_16 + 1;

                //# Datetime data
                int hour = (int)reader.ReadUintLE();
                int minute = (int)reader.ReadUintLE();
                int second = (int)reader.ReadUintLE();
                int year = (int)reader.ReadUintLE();
                int month = (int)reader.ReadUintLE();
                int day = (int)reader.ReadUintLE();

                try { gps.Date = new DateTime(2000 + year, month, day, hour, minute, second); }
                catch (Exception err) { Debug.WriteLine(err.ToString()); return null; }

                //# Coordinate data
                char active = (char)reader.ReadByte();
                gps.IsActive = (active == 'A');

                gps.Latitude_hemisphere = (char)reader.ReadByte();
                gps.Longtitude_hemisphere = (char)reader.ReadByte();
                gps.Unknown = reader.ReadByte();

                float lat = reader.ReadFloatLE();
                gps.Latitude = FixCoordinate(lat, gps.Latitude_hemisphere);

                float lon = reader.ReadFloatLE();
                gps.Longtitude = FixCoordinate(lon, gps.Longtitude_hemisphere);

                gps.Speed = reader.ReadFloatLE();
                gps.Bearing = reader.ReadFloatLE();

                return gps;
            }

            return null;
        }

        /// <summary>
        /// # Novatek stores coordinates in odd DDDmm.mmmm format
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="hemisphere"></param>
        /// <returns></returns>
        private static double FixCoordinate(double coord, char hemisphere)
        {
            double minutes = coord % 100.0;
            double degrees = coord - minutes;

            double coordinate = degrees / 100.0 + (minutes / 60.0);
            if (hemisphere == 'S' || hemisphere == 'W')
                return -1 * (coordinate);
            else
                return (coordinate);
        }

        public override string ToString()
        {
            return (IsActive?"Active, ":"Inactive, ") + Date.ToString("yyyy/MM/dd HH:mm:ss") + ", Speed: " + Speed;
        }
    }
}
