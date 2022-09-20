
namespace DynamicMap.NET.WindowsForms
{
   using System;
   using System.Drawing;
   using System.Runtime.Serialization;
   using System.Windows.Forms;
   using DynamicMap.NET.ObjectModel;

   /// <summary>
   /// DynamicMap.NET overlay
   /// </summary>
   [Serializable]
#if !PocketPC
   public class DynMapOverlay : ISerializable, IDeserializationCallback, IDisposable
#else
   public class DynMapOverlay: IDisposable
#endif
    {
        bool isVisibile = true;

      /// <summary>
      /// is overlay visible
      /// </summary>
      public bool IsVisibile
      {
         get
         {
            return isVisibile;
         }
         set
         {
            if(value != isVisibile)
            {
               isVisibile = value;

               if(Control != null)
               {
                  if(isVisibile)
                  {
                     Control.HoldInvalidation = true;
                     {
                        ForceUpdate();
                     }
                     Control.Refresh();
                  }
                  else
                  {                   
                      if (Control.IsMouseOverMarker)
                      {
                          Control.IsMouseOverMarker = false;
                      }

                      if (Control.IsMouseOverPolygon)
                      {
                          Control.IsMouseOverPolygon = false;
                      }

                      if (Control.IsMouseOverRoute)
                      {
                          Control.IsMouseOverRoute = false;
                      }
#if !PocketPC
                      Control.RestoreCursorOnLeave();
#endif

                     if(!Control.HoldInvalidation)
                     {
                        Control.Invalidate();
                     }
                  }
               }
            }
         }
      }

        bool isHitTestVisible = true;
        /// <summary>
        /// HitTest visibility for entire overlay
        /// </summary>
        public bool IsHitTestVisible
        {
            get { return isHitTestVisible; }
            set { isHitTestVisible = value; }
        }

        bool isZoomSignificant = true;
        /// <summary>
        /// if false don't consider contained objects when box zooming
        /// </summary>
        public bool IsZoomSignificant
        {
            get { return isZoomSignificant; }
            set { isZoomSignificant = value; }
        }

        /// <summary>
        /// overlay Id
        /// </summary>
      public string Id;

      /// <summary>
      /// list of markers, should be thread safe
      /// </summary>
      public readonly ObservableCollectionThreadSafe<DynMapMarker> Markers = new ObservableCollectionThreadSafe<DynMapMarker>();

      /// <summary>
      /// list of routes, should be thread safe
      /// </summary>
      public readonly ObservableCollectionThreadSafe<DynMapRoute> Routes = new ObservableCollectionThreadSafe<DynMapRoute>();

      /// <summary>
      /// list of polygons, should be thread safe
      /// </summary>
      public readonly ObservableCollectionThreadSafe<DynMapPolygon> Polygons = new ObservableCollectionThreadSafe<DynMapPolygon>();

      DynMapControl control;
      public DynMapControl Control
      {
         get
         {
            return control;
         }
         internal set
         {
            control = value;
         }
      }

      public DynMapOverlay()
      {
         CreateEvents();
      }

      public DynMapOverlay(string id)
      {
         Id = id;
         CreateEvents();
      }

      void CreateEvents()
      {
         Markers.CollectionChanged += new NotifyCollectionChangedEventHandler(Markers_CollectionChanged);
         Routes.CollectionChanged += new NotifyCollectionChangedEventHandler(Routes_CollectionChanged);
         Polygons.CollectionChanged += new NotifyCollectionChangedEventHandler(Polygons_CollectionChanged);
      }

      void ClearEvents()
      {
         Markers.CollectionChanged -= new NotifyCollectionChangedEventHandler(Markers_CollectionChanged);
         Routes.CollectionChanged -= new NotifyCollectionChangedEventHandler(Routes_CollectionChanged);
         Polygons.CollectionChanged -= new NotifyCollectionChangedEventHandler(Polygons_CollectionChanged);
      }

      public void Clear()
      {
         Markers.Clear();
         Routes.Clear();
         Polygons.Clear();
      }

      void Polygons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
      {
         if(e.NewItems != null)
         {
            foreach(DynMapPolygon obj in e.NewItems)
            {
               if(obj != null)
               {
                  obj.Overlay = this;
                  if(Control != null)
                  {
                     Control.UpdatePolygonLocalPosition(obj);
                  }
               }
            }
         }

         if(Control != null)
         {
            if(e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Reset)
            {
               if(Control.IsMouseOverPolygon)
               {
                  Control.IsMouseOverPolygon = false;
#if !PocketPC
                  Control.RestoreCursorOnLeave();
#endif
               }
            }

            if(!Control.HoldInvalidation)
            {
               Control.Invalidate();
            }
         }
      }

      void Routes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
      {
         if(e.NewItems != null)
         {
            foreach(DynMapRoute obj in e.NewItems)
            {
               if(obj != null)
               {
                  obj.Overlay = this;
                  if(Control != null)
                  {
                     Control.UpdateRouteLocalPosition(obj);
                  }
               }
            }
         }

         if(Control != null)
         {
            if(e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Reset)
            {
               if(Control.IsMouseOverRoute)
               {
                  Control.IsMouseOverRoute = false;
#if !PocketPC
                  Control.RestoreCursorOnLeave();
#endif
               }
            }

            if(!Control.HoldInvalidation)
            {
               Control.Invalidate();
            }
         }
      }

      void Markers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
      {
         if(e.NewItems != null)
         {
            foreach(DynMapMarker obj in e.NewItems)
            {
               if(obj != null)
               {
                  obj.Overlay = this;
                  if(Control != null)
                  {
                     Control.UpdateMarkerLocalPosition(obj);
                  }
               }
            }
         }

         if(Control != null)
         {
            if(e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Reset)
            {
               if(Control.IsMouseOverMarker)
               {
                  Control.IsMouseOverMarker = false;
#if !PocketPC
                  Control.RestoreCursorOnLeave();
#endif
               }
            }

            if(!Control.HoldInvalidation)
            {
               Control.Invalidate();
            }
         }
      }

      /// <summary>
      /// updates local positions of objects
      /// </summary>
      internal void ForceUpdate()
      {
         if(Control != null)
         {
            foreach(DynMapMarker obj in Markers)
            {
               if(obj.IsVisible)
               {
                  Control.UpdateMarkerLocalPosition(obj);
               }
            }

            foreach(DynMapPolygon obj in Polygons)
            {
               if(obj.IsVisible)
               {
                  Control.UpdatePolygonLocalPosition(obj);
               }
            }

            foreach(DynMapRoute obj in Routes)
            {
               if(obj.IsVisible)
               {
                  Control.UpdateRouteLocalPosition(obj);
               }
            }
         }
      }

      /// <summary>
      /// renders objects/routes/polygons
      /// </summary>
      /// <param name="g"></param>
      public virtual void OnRender(Graphics g)
      {
         if(Control != null)
         {
            if(Control.RoutesEnabled)
            {
               foreach(DynMapRoute r in Routes)
               {
                  if(r.IsVisible)
                  {
                     r.OnRender(g);
                  }
               }
            }

            if(Control.PolygonsEnabled)
            {
               foreach(DynMapPolygon r in Polygons)
               {
                  if(r.IsVisible)
                  {
                     r.OnRender(g);
                  }
               }
            }

            if(Control.MarkersEnabled)
            {
               // markers
               foreach(DynMapMarker m in Markers)
               {
                  //if(m.IsVisible && (m.DisableRegionCheck || Control.Core.currentRegion.Contains(m.LocalPosition.X, m.LocalPosition.Y)))
                  if(m.IsVisible || m.DisableRegionCheck)
                  {
                     m.OnRender(g);
                  }
               }

               // tooltips above
               foreach(DynMapMarker m in Markers)
               {
                  //if(m.ToolTip != null && m.IsVisible && Control.Core.currentRegion.Contains(m.LocalPosition.X, m.LocalPosition.Y))
                  if(m.ToolTip != null && m.IsVisible)
                  {
                     if(!string.IsNullOrEmpty(m.ToolTipText) && (m.ToolTipMode == MarkerTooltipMode.Always || (m.ToolTipMode == MarkerTooltipMode.OnMouseOver && m.IsMouseOver)))
                     {
                        m.ToolTip.OnRender(g);
                     }
                  }
               }
            }
         }
      }

#if !PocketPC
      #region ISerializable Members

      /// <summary>
      /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
      /// </summary>
      /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
      /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
      /// <exception cref="T:System.Security.SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("Id", this.Id);
         info.AddValue("IsVisible", this.IsVisibile);

         DynMapMarker[] markerArray = new DynMapMarker[this.Markers.Count];
         this.Markers.CopyTo(markerArray, 0);
         info.AddValue("Markers", markerArray);

         DynMapRoute[] routeArray = new DynMapRoute[this.Routes.Count];
         this.Routes.CopyTo(routeArray, 0);
         info.AddValue("Routes", routeArray);

         DynMapPolygon[] polygonArray = new DynMapPolygon[this.Polygons.Count];
         this.Polygons.CopyTo(polygonArray, 0);
         info.AddValue("Polygons", polygonArray);         
      }

      private DynMapMarker[] deserializedMarkerArray;
      private DynMapRoute[] deserializedRouteArray;
      private DynMapPolygon[] deserializedPolygonArray;

      /// <summary>
      /// Initializes a new instance of the <see cref="DynMapOverlay"/> class.
      /// </summary>
      /// <param name="info">The info.</param>
      /// <param name="context">The context.</param>
      protected DynMapOverlay(SerializationInfo info, StreamingContext context)
      {
         this.Id = info.GetString("Id");
         this.IsVisibile = info.GetBoolean("IsVisible");

         this.deserializedMarkerArray = Extensions.GetValue<DynMapMarker[]>(info, "Markers", new DynMapMarker[0]);
         this.deserializedRouteArray = Extensions.GetValue<DynMapRoute[]>(info, "Routes", new DynMapRoute[0]);
         this.deserializedPolygonArray = Extensions.GetValue<DynMapPolygon[]>(info, "Polygons", new DynMapPolygon[0]);

         CreateEvents();
      }

      #endregion

      #region IDeserializationCallback Members

      /// <summary>
      /// Runs when the entire object graph has been deserialized.
      /// </summary>
      /// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
      public void OnDeserialization(object sender)
      {
         // Populate Markers
         foreach(DynMapMarker marker in deserializedMarkerArray)
         {
            marker.Overlay = this;
            this.Markers.Add(marker);
         }

         // Populate Routes
         foreach(DynMapRoute route in deserializedRouteArray)
         {
            route.Overlay = this;
            this.Routes.Add(route);
         }

         // Populate Polygons
         foreach(DynMapPolygon polygon in deserializedPolygonArray)
         {
            polygon.Overlay = this;
            this.Polygons.Add(polygon);
         }
      }

      #endregion
#endif

      #region IDisposable Members

      bool disposed = false;

      public void Dispose()
      {
         if(!disposed)
         {
            disposed = true;

            ClearEvents();

            foreach(var m in Markers)
            {
               m.Dispose();
            }

            foreach(var r in Routes)
            {
               r.Dispose();
            }

            foreach(var p in Polygons)
            {
               p.Dispose();
            }

            Clear();
         }
      }

      #endregion
   }
}