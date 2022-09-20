
namespace DynamicMap.NET.MapProviders
{
   using System;

   /// <summary>
   /// LithuaniaHybridOldMap, from 2010 data, provider
   /// </summary>
   public class LithuaniaHybridOldMapProvider : LithuaniaMapProviderBase
   {
      public static readonly LithuaniaHybridOldMapProvider Instance;

      LithuaniaHybridOldMapProvider()
      {
      }

      static LithuaniaHybridOldMapProvider()
      {
         Instance = new LithuaniaHybridOldMapProvider();
      }

      #region DynMapProvider Members

      readonly Guid id = new Guid("35C5C685-E868-4AC7-97BE-10A9A37A81B5");
      public override Guid Id
      {
         get
         {
            return id;
         }
      }

      readonly string name = "LithuaniaHybridMapOld";
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
               overlays = new DynMapProvider[] { LithuaniaOrtoFotoOldMapProvider.Instance, LithuaniaHybridMapProvider.Instance };
            }
            return overlays;
         }
      }

      #endregion
   }
}