using Demo.WindowsPresentation.CustomMarkers;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace DashCamGPSView
{
    /// <summary>
    /// the custom map f GMapControl 
    /// </summary>
    public class Map : GMapControl
    {
        public long ElapsedMilliseconds;

        //PointLatLng start;
        //PointLatLng end;

        // marker
        GMapMarker currentMarker;

        // zones list
        List<GMapMarker> Circles = new List<GMapMarker>();

        public Map()
        {
            // set cache mode only if no internet avaible
            if (!Stuff.PingNetwork("pingtest.com"))
            {
                this.Manager.Mode = AccessMode.CacheOnly;
                MessageBox.Show("No internet connection available, going to CacheOnly mode.", "GMap.NET - Demo.WindowsPresentation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // config map
            this.MapProvider = GMapProviders.GoogleMap;
            this.Zoom = 15;
            this.Position = new PointLatLng(45, -93);

            //this.ScaleMode = ScaleModes.Dynamic;

            // map events
            this.OnPositionChanged += new PositionChanged(MainMap_OnCurrentPositionChanged);
            this.OnTileLoadComplete += new TileLoadComplete(MainMap_OnTileLoadComplete);
            this.OnTileLoadStart += new TileLoadStart(MainMap_OnTileLoadStart);
            this.OnMapTypeChanged += new MapTypeChanged(MainMap_OnMapTypeChanged);
            this.MouseMove += new System.Windows.Input.MouseEventHandler(MainMap_MouseMove);
            this.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(MainMap_MouseLeftButtonDown);
            this.MouseEnter += new MouseEventHandler(MainMap_MouseEnter);

            // set current marker
            currentMarker = new GMapMarker(this.Position);
            {
                currentMarker.Shape = new CustomMarkerRed(this, currentMarker, "custom position marker");
                currentMarker.Offset = new System.Windows.Point(-15, -15);
                currentMarker.ZIndex = int.MaxValue;
                this.Markers.Add(currentMarker);
            }

        }

        void MainMap_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Focus();
        }

        #region -- performance test--
        public RenderTargetBitmap ToImageSource(FrameworkElement obj)
        {
            // Save current canvas transform
            Transform transform = obj.LayoutTransform;
            obj.LayoutTransform = null;

            // fix margin offset as well
            Thickness margin = obj.Margin;
            obj.Margin = new Thickness(0, 0, margin.Right - margin.Left, margin.Bottom - margin.Top);

            // Get the size of canvas
            System.Windows.Size size = new System.Windows.Size(obj.Width, obj.Height);

            // force control to Update
            obj.Measure(size);
            obj.Arrange(new Rect(size));

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(obj);

            if (bmp.CanFreeze)
            {
                bmp.Freeze();
            }

            // return values as they were before
            obj.LayoutTransform = transform;
            obj.Margin = margin;

            return bmp;
        }

        double NextDouble(Random rng, double min, double max)
        {
            return min + (rng.NextDouble() * (max - min));
        }

        Random r = new Random();

        //int tt = 0;
        //void timer_Tick(object sender, EventArgs e)
        //{
        //    var pos = new PointLatLng(NextDouble(r, MainMap.ViewArea.Top, MainMap.ViewArea.Bottom), NextDouble(r, MainMap.ViewArea.Left, MainMap.ViewArea.Right));
        //    GMapMarker m = new GMapMarker(pos);
        //    {
        //        var s = new Test((tt++).ToString());

        //        var image = new Image();
        //        {
        //            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.LowQuality);
        //            image.Stretch = Stretch.None;
        //            image.Opacity = s.Opacity;

        //            image.MouseEnter += new System.Windows.Input.MouseEventHandler(image_MouseEnter);
        //            image.MouseLeave += new System.Windows.Input.MouseEventHandler(image_MouseLeave);

        //            image.Source = ToImageSource(s);
        //        }

        //        m.Shape = image;

        //        m.Offset = new System.Windows.Point(-s.Width, -s.Height);
        //    }
        //    MainMap.Markers.Add(m);

        //    if (tt >= 333)
        //    {
        //        timer.Stop();
        //        tt = 0;
        //    }
        //}

        void image_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Image img = sender as Image;
            img.RenderTransform = null;
        }

        void image_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Image img = sender as Image;
            img.RenderTransform = new ScaleTransform(1.2, 1.2, 12.5, 12.5);
        }

        DispatcherTimer timer = new DispatcherTimer();
        #endregion

        #region -- transport demo --
        //BackgroundWorker transport = new BackgroundWorker();

        //readonly List<VehicleData> trolleybus = new List<VehicleData>();
        //readonly Dictionary<int, GMapMarker> trolleybusMarkers = new Dictionary<int, GMapMarker>();

        //readonly List<VehicleData> bus = new List<VehicleData>();
        //readonly Dictionary<int, GMapMarker> busMarkers = new Dictionary<int, GMapMarker>();

        //bool firstLoadTrasport = true;

        //void transport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    using (Dispatcher.DisableProcessing())
        //    {
        //        lock (trolleybus)
        //        {
        //            foreach (VehicleData d in trolleybus)
        //            {
        //                GMapMarker marker;

        //                if (!trolleybusMarkers.TryGetValue(d.Id, out marker))
        //                {
        //                    marker = new GMapMarker(new PointLatLng(d.Lat, d.Lng));
        //                    marker.Tag = d.Id;
        //                    marker.Shape = new CircleVisual(marker, Brushes.Red);

        //                    trolleybusMarkers[d.Id] = marker;
        //                    MainMap.Markers.Add(marker);
        //                }
        //                else
        //                {
        //                    marker.Position = new PointLatLng(d.Lat, d.Lng);
        //                    var shape = (marker.Shape as CircleVisual);
        //                    {
        //                        shape.Text = d.Line;
        //                        shape.Angle = d.Bearing;
        //                        shape.Tooltip.SetValues("TrolleyBus", d);

        //                        if (shape.IsChanged)
        //                        {
        //                            shape.UpdateVisual(false);
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        lock (bus)
        //        {
        //            foreach (VehicleData d in bus)
        //            {
        //                GMapMarker marker;

        //                if (!busMarkers.TryGetValue(d.Id, out marker))
        //                {
        //                    marker = new GMapMarker(new PointLatLng(d.Lat, d.Lng));
        //                    marker.Tag = d.Id;

        //                    var v = new CircleVisual(marker, Brushes.Blue);
        //                    {
        //                        v.Stroke = new Pen(Brushes.Gray, 2.0);
        //                    }
        //                    marker.Shape = v;

        //                    busMarkers[d.Id] = marker;
        //                    MainMap.Markers.Add(marker);
        //                }
        //                else
        //                {
        //                    marker.Position = new PointLatLng(d.Lat, d.Lng);
        //                    var shape = (marker.Shape as CircleVisual);
        //                    {
        //                        shape.Text = d.Line;
        //                        shape.Angle = d.Bearing;
        //                        shape.Tooltip.SetValues("Bus", d);

        //                        if (shape.IsChanged)
        //                        {
        //                            shape.UpdateVisual(false);
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (firstLoadTrasport)
        //        {
        //            firstLoadTrasport = false;
        //        }
        //    }
        //}

        //void transport_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    while (!transport.CancellationPending)
        //    {
        //        try
        //        {
        //            lock (trolleybus)
        //            {
        //                Stuff.GetVilniusTransportData(Demo.WindowsForms.TransportType.TrolleyBus, string.Empty, trolleybus);
        //            }

        //            lock (bus)
        //            {
        //                Stuff.GetVilniusTransportData(Demo.WindowsForms.TransportType.Bus, string.Empty, bus);
        //            }

        //            transport.ReportProgress(100);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine("transport_DoWork: " + ex.ToString());
        //        }
        //        Thread.Sleep(3333);
        //    }
        //    trolleybusMarkers.Clear();
        //    busMarkers.Clear();
        //}

        #endregion

        // add objects and zone around them
        //void AddDemoZone(double areaRadius, PointLatLng center, List<PointAndInfo> objects)
        //{
        //    var objectsInArea = from p in objects
        //                        where MainMap.MapProvider.Projection.GetDistance(center, p.Point) <= areaRadius
        //                        select new
        //                        {
        //                            Obj = p,
        //                            Dist = MainMap.MapProvider.Projection.GetDistance(center, p.Point)
        //                        };
        //    if (objectsInArea.Any())
        //    {
        //        var maxDistObject = (from p in objectsInArea
        //                             orderby p.Dist descending
        //                             select p).First();

        //        // add objects to zone
        //        foreach (var o in objectsInArea)
        //        {
        //            GMapMarker it = new GMapMarker(o.Obj.Point);
        //            {
        //                it.ZIndex = 55;
        //                var s = new CustomMarkerDemo(this, it, o.Obj.Info + ", distance from center: " + o.Dist + "km.");
        //                it.Shape = s;
        //            }

        //            MainMap.Markers.Add(it);
        //        }

        //        // add zone circle
        //        //if(false)
        //        {
        //            GMapMarker it = new GMapMarker(center);
        //            it.ZIndex = -1;

        //            Circle c = new Circle();
        //            c.Center = center;
        //            c.Bound = maxDistObject.Obj.Point;
        //            c.Tag = it;
        //            c.IsHitTestVisible = false;

        //            UpdateCircle(c);
        //            Circles.Add(it);

        //            it.Shape = c;
        //            MainMap.Markers.Add(it);
        //        }
        //    }
        //}

        // calculates circle radius
        //void UpdateCircle(Circle c)
        //{
        //    var pxCenter = MainMap.FromLatLngToLocal(c.Center);
        //    var pxBounds = MainMap.FromLatLngToLocal(c.Bound);

        //    double a = (double)(pxBounds.X - pxCenter.X);
        //    double b = (double)(pxBounds.Y - pxCenter.Y);
        //    var pxCircleRadius = Math.Sqrt(a * a + b * b);

        //    c.Width = 55 + pxCircleRadius * 2;
        //    c.Height = 55 + pxCircleRadius * 2;
        //    (c.Tag as GMapMarker).Offset = new System.Windows.Point(-c.Width / 2, -c.Height / 2);
        //}

        void MainMap_OnMapTypeChanged(GMapProvider type)
        {
            //sliderZoom.Minimum = MainMap.MinZoom;
            //sliderZoom.Maximum = MainMap.MaxZoom;
        }

        void MainMap_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(this);
            currentMarker.Position = this.FromLocalToLatLng((int)p.X, (int)p.Y);
        }

        // move current marker with left holding
        void MainMap_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point p = e.GetPosition(this);
                currentMarker.Position = this.FromLocalToLatLng((int)p.X, (int)p.Y);
            }
        }

        // zoo max & center markers
        private void button13_Click(object sender, RoutedEventArgs e)
        {
            this.ZoomAndCenterMarkers(null);

            /*
            PointAnimation panMap = new PointAnimation();
            panMap.Duration = TimeSpan.FromSeconds(1);
            panMap.From = new Point(MainMap.Position.Lat, MainMap.Position.Lng);
            panMap.To = new Point(0, 0);
            Storyboard.SetTarget(panMap, MainMap);
            Storyboard.SetTargetProperty(panMap, new PropertyPath(GMapControl.MapPointProperty));

            Storyboard panMapStoryBoard = new Storyboard();
            panMapStoryBoard.Children.Add(panMap);
            panMapStoryBoard.Begin(this);
             */
        }

        // tile louading starts
        void MainMap_OnTileLoadStart()
        {
            //System.Windows.Forms.MethodInvoker m = delegate ()
            //{
            //    progressBar1.Visibility = Visibility.Visible;
            //};

            //try
            //{
            //    this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, m);
            //}
            //catch
            //{
            //}
        }

        // tile loading stops
        void MainMap_OnTileLoadComplete(long ElapsedMilliseconds)
        {
            //MainMap.ElapsedMilliseconds = ElapsedMilliseconds;

            //System.Windows.Forms.MethodInvoker m = delegate ()
            //{
            //    progressBar1.Visibility = Visibility.Hidden;
            //    groupBox3.Header = "loading, last in " + MainMap.ElapsedMilliseconds + "ms";
            //};

            //try
            //{
            //    this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, m);
            //}
            //catch
            //{
            //}
        }

        // current location changed
        void MainMap_OnCurrentPositionChanged(PointLatLng point)
        {
            //mapgroup.Header = "gmap: " + point;
        }

#if DEBUG
        private int counter;
        readonly Typeface tf = new Typeface("GenericSansSerif");
        readonly System.Windows.FlowDirection fd = new System.Windows.FlowDirection();

        /// <summary>
        /// any custom drawing here
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            DateTime _start = DateTime.Now;

            base.OnRender(drawingContext);

            DateTime _end = DateTime.Now;
            int _delta = (int)((_end - _start).TotalMilliseconds);

            FormattedText text = new FormattedText(string.Format(CultureInfo.InvariantCulture, "{0:0.0}", Zoom) + "z, " + 
                MapProvider + ", refresh: " + counter++ + ", load: " + ElapsedMilliseconds + "ms, render: " + _delta + "ms", 
                CultureInfo.InvariantCulture, fd, tf, 20, Brushes.Blue);
            drawingContext.DrawText(text, new Point(text.Height, text.Height));
            text = null;
        }
#endif
    }
}
