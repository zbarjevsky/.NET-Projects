﻿
namespace DynamicMap.NET.MapProviders
{
   using System;

   /// <summary>
   /// OpenCycleMap provider - http://www.opencyclemap.org
   /// </summary>
   public class OpenCycleMapProvider : OpenStreetMapProviderBase
   {
      public static readonly OpenCycleMapProvider Instance;

      OpenCycleMapProvider()
      {
         RefererUrl = "http://www.opencyclemap.org/";
      }

      static OpenCycleMapProvider()
      {
         Instance = new OpenCycleMapProvider();
      }

      #region DynMapProvider Members

      readonly Guid id = new Guid("D7E1826E-EE1E-4441-9F15-7C2DE0FE0B0A");
      public override Guid Id
      {
         get
         {
            return id;
         }
      }

      readonly string name = "OpenCycleMap";
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
         char letter = ServerLetters[DynMapProvider.GetServerNum(pos, 3)];
         return string.Format(UrlFormat, letter, zoom, pos.X, pos.Y);
      }

      static readonly string UrlFormat = "http://{0}.tile.opencyclemap.org/cycle/{1}/{2}/{3}.png";
   }
}
