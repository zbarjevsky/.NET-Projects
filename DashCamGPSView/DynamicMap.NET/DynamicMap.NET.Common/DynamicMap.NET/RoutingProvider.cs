﻿
namespace DynamicMap.NET
{
   /// <summary>
   /// routing interface
   /// </summary>
   public interface RoutingProvider
   {
      /// <summary>
      /// get route between two points
      /// </summary>
      MapRoute GetRoute(PointLatLng start, PointLatLng end, bool avoidHighways, bool walkingMode, int zoom, bool getInstructions = false);

      /// <summary>
      /// get route between two points
      /// </summary>
      MapRoute GetRoute(string start, string end, bool avoidHighways, bool walkingMode, int zoom);
   }
}
