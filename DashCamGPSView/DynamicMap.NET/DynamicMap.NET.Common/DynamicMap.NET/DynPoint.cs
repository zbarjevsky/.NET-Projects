
namespace DynamicMap.NET
{
   using System.Globalization;
   using System;
    using System.Collections.Generic;

   /// <summary>
   /// the point ;}
   /// </summary>
   [Serializable]
   public struct DynPoint
   {
      public static readonly DynPoint Empty = new DynPoint();

      private long x;
      private long y;

      public DynPoint(long x, long y)
      {
         this.x = x;
         this.y = y;
      }

      public DynPoint(DynSize sz)
      {
         this.x = sz.Width;
         this.y = sz.Height;
      }

      //public GPoint(int dw)
      //{
      //   this.x = (short) LOWORD(dw);
      //   this.y = (short) HIWORD(dw);
      //}

      public bool IsEmpty
      {
         get
         {
            return x == 0 && y == 0;
         }
      }

      public long X
      {
         get
         {
            return x;
         }
         set
         {
            x = value;
         }
      }

      public long Y
      {
         get
         {
            return y;
         }
         set
         {
            y = value;
         }
      }

      public static explicit operator DynSize(DynPoint p)
      {
         return new DynSize(p.X, p.Y);
      }

      public static DynPoint operator+(DynPoint pt, DynSize sz)
      {
         return Add(pt, sz);
      }

      public static DynPoint operator-(DynPoint pt, DynSize sz)
      {
         return Subtract(pt, sz);
      }

      public static bool operator==(DynPoint left, DynPoint right)
      {
         return left.X == right.X && left.Y == right.Y;
      }

      public static bool operator!=(DynPoint left, DynPoint right)
      {
         return !(left == right);
      }

      public static DynPoint Add(DynPoint pt, DynSize sz)
      {
         return new DynPoint(pt.X + sz.Width, pt.Y + sz.Height);
      }

      public static DynPoint Subtract(DynPoint pt, DynSize sz)
      {
         return new DynPoint(pt.X - sz.Width, pt.Y - sz.Height);
      }

      public override bool Equals(object obj)
      {
         if(!(obj is DynPoint))
            return false;
         DynPoint comp = (DynPoint) obj;
         return comp.X == this.X && comp.Y == this.Y;
      }

      public override int GetHashCode()
      {
         return (int)(x ^ y);
      }

      public void Offset(long dx, long dy)
      {
         X += dx;
         Y += dy;
      }

      public void Offset(DynPoint p)
      {
         Offset(p.X, p.Y);
      }
      public void OffsetNegative(DynPoint p)
      {
         Offset(-p.X, -p.Y);
      }

      public override string ToString()
      {
         return "{X=" + X.ToString(CultureInfo.CurrentCulture) + ",Y=" + Y.ToString(CultureInfo.CurrentCulture) + "}";
      }

      //private static int HIWORD(int n)
      //{
      //   return (n >> 16) & 0xffff;
      //}

      //private static int LOWORD(int n)
      //{
      //   return n & 0xffff;
      //}
   }

   internal class GPointComparer : IEqualityComparer<DynPoint>
   {
       public bool Equals(DynPoint x, DynPoint y)
       {
           return x.X == y.X && x.Y == y.Y;
       }

       public int GetHashCode(DynPoint obj)
       {
           return obj.GetHashCode();
       }
   }
}
