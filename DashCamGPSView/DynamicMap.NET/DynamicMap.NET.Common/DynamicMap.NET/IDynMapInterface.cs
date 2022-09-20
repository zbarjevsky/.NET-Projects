
using DynamicMap.NET.MapProviders;

namespace DynamicMap.NET
{
   public interface IDynMapInterface
   {
      PointLatLng Position
      {
         get;
         set;
      }

      DynPoint PositionPixel
      {
         get;
      }

      string CacheLocation
      {
         get;
         set;
      }

      bool IsDragging
      {
         get;
      }

      RectLatLng ViewArea
      {
         get;
      }

      DynMapProvider MapProvider
      {
         get;
         set;
      }

      bool CanDragMap
      {
         get;
         set;
      }

      RenderMode RenderMode
      {
         get;
      }

      // events
      event PositionChanged OnPositionChanged;
      event TileLoadComplete OnTileLoadComplete;
      event TileLoadStart OnTileLoadStart;
      event MapDrag OnMapDrag;
      event MapZoomChanged OnMapZoomChanged;
      event MapTypeChanged OnMapTypeChanged;

      void ReloadMap();

      PointLatLng FromLocalToLatLng(int x, int y);

      DynPoint FromLatLngToLocal(PointLatLng point);

#if !PocketPC
#if SQLite
      bool ShowExportDialog();
      bool ShowImportDialog();
#endif
#endif
   }
}
