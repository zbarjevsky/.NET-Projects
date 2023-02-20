using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.RadexOneLib
{
    public class RadexOneSerialNumber
    {
        public uint year { get; set; }
        public uint month { get; set; }
        public uint day { get; set; }

        public uint verMajor { get; set; }
        public uint verMinor { get; set; }

        public uint snPart1 { get; set; }
        public uint snPart2 { get; set; }

        public RadexOneSerialNumber()
        {

        }

        public RadexOneSerialNumber(RadexOneSerialNumber sn)
        {
            year = sn.year;
            month = sn.month;
            day = sn.day;

            verMajor = sn.verMajor;
            verMinor = sn.verMinor;
            
            snPart1 = sn.snPart1;
            snPart2 = sn.snPart2;
        }

        public RadexOneSerialNumber Clone()
        {
            return new RadexOneSerialNumber(this);
        }

        public static RadexOneSerialNumber ParseResponse(ResponceBase responce)
        {
            RadexOneSerialNumber sn = new RadexOneSerialNumber()
            {
                year = responce.GetUInt16(28),
                month = responce.GetUInt8(30),
                day = responce.GetUInt8(31),

                verMajor = responce.GetUInt8(32),
                verMinor = responce.GetUInt8(33),

                snPart1 = responce.GetUInt16(34),
                snPart2 = responce.GetUInt32(24)
            };
            return sn;
        }

        public static bool operator !=(RadexOneSerialNumber sn1, RadexOneSerialNumber sn2)
        {
            return !(sn1 == sn2);
        }

        public static bool operator ==(RadexOneSerialNumber sn1, RadexOneSerialNumber sn2)
        {
            if (IsNull(sn1) && IsNull(sn2))
                return true;
            if (IsNull(sn1) || IsNull(sn2))
                return false;

            return sn1.Equals(sn2);
        }

        public override string ToString()
        {
            string date = string.Format("{0:D2}{1:D2}{2:D2}", day, month, year);
            return string.Format("S/N {0}-{1:D4}-{2:D6}  v. {3}.{4}", date, snPart1, snPart2, verMajor, verMinor);
        }

        public override bool Equals(object obj)
        {
            RadexOneSerialNumber sn2 = obj as RadexOneSerialNumber;
            if (IsNull(sn2))
                return false;

            return
                year == sn2.year &&
                month == sn2.month &&
                day == sn2.day &&
                verMajor == sn2.verMajor &&
                verMinor == sn2.verMinor &&
                snPart1 == sn2.snPart1 &&
                snPart2 == sn2.snPart2;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private static bool IsNull(object obj)
        {
            return (obj == null);
        }
    }
}
