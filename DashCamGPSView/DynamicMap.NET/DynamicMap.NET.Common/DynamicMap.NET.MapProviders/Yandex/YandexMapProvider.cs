﻿
namespace DynamicMap.NET.MapProviders
{
   using System;
   using DynamicMap.NET.Projections;
   using DynamicMap.NET.Internals;

    public abstract class YandexMapProviderBase : DynMapProvider
    {
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
                return MercatorProjectionYandex.Instance;
            }
        }

        DynMapProvider[] overlays;
        public override DynMapProvider[] Overlays
        {
            get
            {
                if (overlays == null)
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

        public readonly string Server = "maps.yandex.net";      // Stuff.EncriptedPasswordString("MECxW6okUK3Ir7a9ue/vIA==");
        public readonly string ServerRu = "maps.yandex.ru";     // Stuff.EncriptedPasswordString("MECxW6okUK0FRlRPbF0BQg==");
        public readonly string ServerCom = "maps.yandex.com";   // Stuff.EncriptedPasswordString("MECxW6okUK2JNHOW5AuimA==");     

        protected string Version = "4.6.9";
    }

   /// <summary>
   /// YenduxMap provider
   /// </summary>
   public class YandexMapProvider : YandexMapProviderBase
   {
      public static readonly YandexMapProvider Instance;

      YandexMapProvider()
      {
         RefererUrl = "http://" + ServerCom + "/";
      }

      static YandexMapProvider()
      {
         Instance = new YandexMapProvider();
      }

      #region DynMapProvider Members

      readonly Guid id = new Guid("82DC969D-0491-40F3-8C21-4D90B67F47EB");
      public override Guid Id
      {
         get
         {
            return id;
         }
      }

      readonly string name = "YandexMap";
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
         return string.Format(UrlFormat, UrlServer, GetServerNum(pos, 4) + 1, Version, pos.X, pos.Y, zoom, language, Server);
      }

      static readonly string UrlServer = "vec";
      static readonly string UrlFormat = "http://{0}0{1}.{7}/tiles?l=map&v={2}&x={3}&y={4}&z={5}&lang={6}";               
   }
}