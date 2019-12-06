
namespace GMap.NET
{
   using System;
   using System.Globalization;

    /// <summary>
    /// the point of coordinates
    /// </summary>
    [Serializable]
    public struct PointLatLng
    {
        public static readonly PointLatLng Empty = new PointLatLng();
        private double lat;
        private double lng;

        bool NotEmpty;

        public PointLatLng(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
            NotEmpty = true;
        }

        /// <summary>
        /// returns true if coordinates wasn't assigned
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return !NotEmpty;
            }
        }

        public double Lat
        {
            get
            {
                return this.lat;
            }
            set
            {
                this.lat = value;
                NotEmpty = true;
            }
        }

        public double Lng
        {
            get
            {
                return this.lng;
            }
            set
            {
                this.lng = value;
                NotEmpty = true;
            }
        }

        public static PointLatLng operator +(PointLatLng pt, SizeLatLng sz)
        {
            return Add(pt, sz);
        }

        public static PointLatLng operator -(PointLatLng pt, SizeLatLng sz)
        {
            return Subtract(pt, sz);
        }

        public static SizeLatLng operator -(PointLatLng pt1, PointLatLng pt2)
        {
            return new SizeLatLng(pt1.Lat - pt2.Lat, pt2.Lng - pt1.Lng);
        }

        public static bool operator ==(PointLatLng left, PointLatLng right)
        {
            return ((left.Lng == right.Lng) && (left.Lat == right.Lat));
        }

        public static bool operator !=(PointLatLng left, PointLatLng right)
        {
            return !(left == right);
        }

        public static PointLatLng Add(PointLatLng pt, SizeLatLng sz)
        {
            return new PointLatLng(pt.Lat - sz.HeightLat, pt.Lng + sz.WidthLng);
        }

        public static PointLatLng Subtract(PointLatLng pt, SizeLatLng sz)
        {
            return new PointLatLng(pt.Lat + sz.HeightLat, pt.Lng - sz.WidthLng);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PointLatLng))
            {
                return false;
            }
            PointLatLng tf = (PointLatLng)obj;
            return (((tf.Lng == this.Lng) && (tf.Lat == this.Lat)) && tf.GetType().Equals(base.GetType()));
        }

        public void Offset(PointLatLng pos)
        {
            this.Offset(pos.Lat, pos.Lng);
        }

        public void Offset(double lat, double lng)
        {
            this.Lng += lng;
            this.Lat -= lat;
        }

        public override int GetHashCode()
        {
            return (this.Lng.GetHashCode() ^ this.Lat.GetHashCode());
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Lat={0}, Lng={1}}}", LocationToString(this.Lat), LocationToString(this.Lng));
        }

        private string LocationToString(double x)
        {
            return SexagesimalAngle.FromDouble(x).ToString();
        }
    }
    public class SexagesimalAngle
    {
        public bool IsNegative { get; set; }
        public int Degrees { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Milliseconds { get; set; }

        public static SexagesimalAngle FromDouble(double angleInDegrees)
        {
            //ensure the value will fall within the primary range [-180.0..+180.0]
            while (angleInDegrees < -180.0)
                angleInDegrees += 360.0;

            while (angleInDegrees > 180.0)
                angleInDegrees -= 360.0;

            var result = new SexagesimalAngle();

            //switch the value to positive
            result.IsNegative = angleInDegrees < 0;
            angleInDegrees = Math.Abs(angleInDegrees);

            //gets the degree
            result.Degrees = (int)Math.Floor(angleInDegrees);
            var delta = angleInDegrees - result.Degrees;

            //gets minutes and seconds
            var seconds = (int)Math.Floor(3600.0 * delta);
            result.Seconds = seconds % 60;
            result.Minutes = (int)Math.Floor(seconds / 60.0);
            delta = delta * 3600.0 - seconds;

            //gets fractions
            result.Milliseconds = (int)(1000.0 * delta);

            return result;
        }

        public override string ToString()
        {
            var degrees = this.IsNegative
                ? -this.Degrees
                : this.Degrees;

            return string.Format(
                "{0}° {1:00}' {2:00}\"",
                degrees,
                this.Minutes,
                this.Seconds);
        }

        public string ToString(string format)
        {
            switch (format)
            {
                case "NS":
                    return string.Format(
                        "{0}° {1:00}' {2:00}\".{3:000} {4}",
                        this.Degrees,
                        this.Minutes,
                        this.Seconds,
                        this.Milliseconds,
                        this.IsNegative ? 'S' : 'N');

                case "WE":
                    return string.Format(
                        "{0}° {1:00}' {2:00}\".{3:000} {4}",
                        this.Degrees,
                        this.Minutes,
                        this.Seconds,
                        this.Milliseconds,
                        this.IsNegative ? 'W' : 'E');

                default:
                    throw new NotImplementedException();
            }
        }
    }
}