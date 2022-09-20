
namespace DynamicMap.NET.MapProviders
{
   using System;

   /// <summary>
   /// OpenStreetMapQuestSattelite provider - http://wiki.openstreetmap.org/wiki/MapQuest
   /// </summary>
   public class OpenStreetMapQuestSatteliteProvider : OpenStreetMapProviderBase
   {
      public static readonly OpenStreetMapQuestSatteliteProvider Instance;

      OpenStreetMapQuestSatteliteProvider()
      {
         Copyright = string.Format("© MapQuest - Map data ©{0} MapQuest, OpenStreetMap", DateTime.Today.Year);
      }

      static OpenStreetMapQuestSatteliteProvider()
      {
         Instance = new OpenStreetMapQuestSatteliteProvider();
      }

      #region DynMapProvider Members

      readonly Guid id = new Guid("E590D3B1-37F4-442B-9395-ADB035627F67");
      public override Guid Id
      {
         get
         {
            return id;
         }
      }

      readonly string name = "OpenStreetMapQuestSattelite";
      public override string Name
      {
         get
         {
            return name;
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
         string url = MakeTileImageUrl(pos, zoom, string.Empty);

         return GetTileImageUsingHttp(url);
      }

      #endregion

      string MakeTileImageUrl(DynPoint pos, int zoom, string language)
      {
         return string.Format(UrlFormat, GetServerNum(pos, 3) + 1, zoom, pos.X, pos.Y);
      }

      static readonly string UrlFormat = "http://otile{0}.mqcdn.com/tiles/1.0.0/sat/{1}/{2}/{3}.jpg";
   }
}
