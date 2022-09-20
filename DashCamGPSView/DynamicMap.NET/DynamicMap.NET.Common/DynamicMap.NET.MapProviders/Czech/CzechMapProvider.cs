
namespace DynamicMap.NET.MapProviders
{
   using System;
   using DynamicMap.NET.Projections;

   public abstract class CzechMapProviderBase : DynMapProvider
   {
      public CzechMapProviderBase()
      {
         RefererUrl = "http://www.mapy.cz/";
         Area = new RectLatLng(51.2024819920053, 11.8401353319027, 7.22833716731277, 2.78312271922872);
      }

        #region DynMapProvider Members
        public override Guid Id
      {
         get
         {
            throw new NotImplementedException();
         }
      }

      public override string Name
      {
         get
         {
            throw new NotImplementedException();
         }
      }

      public override PureProjection Projection
      {
         get
         {
            return MercatorProjection.Instance;
         }
      }

      DynMapProvider[] overlays;
      public override DynMapProvider[] Overlays
      {
         get
         {
            if(overlays == null)
            {
               overlays = new DynMapProvider[] { this };
            }
            return overlays;
         }
      }

      public override PureImage GetTileImage(DynPoint pos, int zoom)
      {
         throw new NotImplementedException();
      }
      #endregion
   }

   /// <summary>
   /// CzechMap provider, http://www.mapy.cz/
   /// </summary>
   public class CzechMapProvider : CzechMapProviderBase
   {
      public static readonly CzechMapProvider Instance;

      CzechMapProvider()
      {
      }

      static CzechMapProvider()
      {
         Instance = new CzechMapProvider();
      }

        #region DynMapProvider Members

        readonly Guid id = new Guid("13AB92EF-8C3B-4FAC-B2CD-2594C05F8BFC");
      public override Guid Id
      {
         get
         {
            return id;
         }
      }

      readonly string name = "CzechMap";
      public override string Name
      {
         get
         {
            return name;
         }
      }

      public override PureImage GetTileImage(DynPoint pos, int zoom)
      {
         string url = MakeTileImageUrl(pos, zoom, LanguageStr);

         return GetTileImageUsingHttp(url);
      }

      #endregion

      string MakeTileImageUrl(DynPoint pos, int zoom, string language)
      {
         // ['base-m','ophoto-m','turist-m','army2-m']
         // http://m3.mapserver.mapy.cz/base-m/14-8802-5528

         return string.Format(UrlFormat, GetServerNum(pos, 3) + 1, zoom, pos.X, pos.Y);
      }

      static readonly string UrlFormat = "http://m{0}.mapserver.mapy.cz/base-m/{1}-{2}-{3}";
   }
}