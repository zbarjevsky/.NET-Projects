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
    public class GpsPointInfo
    {
        public DateTime Date;
        public bool IsActive;
        public char Latitude_hemisphere, Longtitude_hemisphere;
        public byte Unknown;
        public double Latitude, Longtitude;
        public double Speed;
        public double Bearing;

        public static GpsPointInfo Parse(uint offset, uint size, byte[] file)
        {
            byte[] data = new byte[size];
            Array.Copy(file, offset, data, 0, size);

            uint pos = 0;

            uint size1 = Box.ReadUintBE(data, pos); pos += 4;
            string type = Encoding.ASCII.GetString(data, (int)pos, 4); pos += 4;
            string magic = Encoding.ASCII.GetString(data, (int)pos, 4); pos += 4;

            if (size != size1 || type != "free" || magic != "GPS ")
                return null;

            GpsPointInfo gps = new GpsPointInfo();

            //# checking for weird Azdome 0xAA XOR "encrypted" GPS data. 
            //This portion is a quick fix.
            uint payload_size = 254;
            if (data[pos] == 0x05)
            {
                if (size < 254)
                    payload_size = size;

                byte[] payload = new byte[payload_size];

                pos += 6; //???
                for (int i = 0; i < payload_size; i++)
                {
                    payload[i] = (byte)(file[pos + i] ^ 0xAA);
                }
            }
            else if ((char)data[pos] == 'L')
            {
                pos = 48;

                //# Datetime data
                int hour = (int)Box.ReadUintLE(data, pos); pos += 4;
                int minute = (int)Box.ReadUintLE(data, pos); pos += 4;
                int second = (int)Box.ReadUintLE(data, pos); pos += 4;
                int year = (int)Box.ReadUintLE(data, pos); pos += 4;
                int month = (int)Box.ReadUintLE(data, pos); pos += 4;
                int day = (int)Box.ReadUintLE(data, pos); pos += 4;

                gps.Date = new DateTime(year, month, day, hour, minute, second);

                //# Coordinate data
                char active = (char)data[pos]; pos++;
                gps.IsActive = (active == 'A');

                gps.Latitude_hemisphere = (char)data[pos]; pos++;
                gps.Longtitude_hemisphere = (char)data[pos]; pos++;
                gps.Unknown = data[pos]; pos++;
                gps.Latitude = Box.ReadFloatLE(data, pos); pos += 4;
                gps.Longtitude = Box.ReadFloatLE(data, pos); pos += 4;
                gps.Speed = Box.ReadFloatLE(data, pos); pos += 4;
                gps.Bearing = Box.ReadFloatLE(data, pos); pos += 4;
            }

            return gps;
        }

        public override string ToString()
        {
            return (IsActive?"Active, ":"Inactive, ") + Date.ToString("yyyy/MM/dd HH:mm:ss") + ", Speed: " + Speed;
        }
    }
}
