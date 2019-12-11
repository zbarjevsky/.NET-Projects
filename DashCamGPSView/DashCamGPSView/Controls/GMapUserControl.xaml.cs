using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using DashCamGPSView.CustomMarkers;
using GPSDataParser;
using DashCamGPSView.Tools;

namespace DashCamGPSView.Controls
{
    /// <summary>
    /// Interaction logic for GMapUserControl.xaml
    /// </summary>
    public partial class GMapUserControl : UserControl, INotifyPropertyChanged
    {
        //PointLatLng start;
        //PointLatLng end;

        // marker
        GMapMarker _currentMarker;
        CustomMarkerRed _currentMarkerUI;

        // zones list
        List<GMapMarker> Circles = new List<GMapMarker>();

        public double Zoom
        {
            get { return GMap.Zoom; }
            set { GMap.Zoom = value; OnPropertyChanged(); }
        }

        public int MinZoom
        {
            get { return GMap.MinZoom; }
            set { GMap.MinZoom = value; OnPropertyChanged(); }
        }

        public int MaxZoom
        {
            get { return GMap.MaxZoom; }
            set { GMap.MaxZoom = value; OnPropertyChanged(); }
        }

        public PointLatLng Position
        {
            get { return GMap.Position; }
            set 
            { 
                GMap.Position = value; 
                OnPropertyChanged(); 
            }
        }

        public void UpdateRouteAndCar(DashCamFileInfo dashCamFileInfo, int idx)
        {
            if (dashCamFileInfo == null || dashCamFileInfo.GpsInfo == null)
            {
                _route.UpdateRouteAndCar(null, -1, null);
                return;
            }

            GPSDataParser.GpsPointData inf = dashCamFileInfo.GpsInfo[idx];
            PointLatLng currentPosition = new PointLatLng(inf.Latitude, inf.Longitude);
            GMap.Position = currentPosition;

            _route.UpdateRouteAndCar(dashCamFileInfo.GpsInfo, idx, GMap);
        }

        //internal void UpdateCarPosition(PointLatLng pointLatLng, double course)
        //{
        //    GPoint pt = GMap.FromLatLngToLocal(pointLatLng);
        //    _car.UpdatePosition(pt.X, pt.Y, course);
        //    GMap.Position = pointLatLng;
        //}

        public PointLatLng FromLocalToLatLng(int x, int y)
        {
            return GMap.FromLocalToLatLng(x, y);
        }

        public GMapUserControl()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            cmbMapType.Items.Add(GMapProviders.GoogleMap);
            cmbMapType.Items.Add(GMapProviders.GoogleTerrainMap);
            cmbMapType.Items.Add(GMapProviders.GoogleSatelliteMap);
            cmbMapType.Items.Add(GMapProviders.BingMap);
            cmbMapType.Items.Add(GMapProviders.BingHybridMap);
            cmbMapType.Items.Add(GMapProviders.BingSatelliteMap);
            //cmbMapType.Items.Add(GMapProviders.YahooMap);
            //cmbMapType.Items.Add(GMapProviders.YahooHybridMap);
            //cmbMapType.Items.Add(GMapProviders.YahooSatelliteMap);
            //cmbMapType.Items.Add(GMapProviders.OviMap);
            //cmbMapType.Items.Add(GMapProviders.OviHybridMap);
            //cmbMapType.Items.Add(GMapProviders.OviSatelliteMap);
            //cmbMapType.Items.Add(GMapProviders.NearMap);
            //cmbMapType.Items.Add(GMapProviders.NearHybridMap);
            //cmbMapType.Items.Add(GMapProviders.NearSatelliteMap);

            // set cache mode only if no internet avaible
            if (!Stuff.PingNetwork("pingtest.com"))
            {
                GMap.Manager.Mode = AccessMode.CacheOnly;
                MessageBox.Show("No internet connection available, going to CacheOnly mode.", "GMap.NET - Demo.WindowsPresentation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            //default config map
            GMap.MapProvider = GMapProviders.GoogleMap;

            //this.ScaleMode = ScaleModes.Dynamic;
            GMap.ShowCenter = false;

            // map events
            GMap.Loaded += MainMap_Loaded;
            GMap.OnPositionChanged += MainMap_OnCurrentPositionChanged;
            GMap.OnTileLoadStart += MainMap_OnTileLoadStart;
            GMap.OnMapTypeChanged += MainMap_OnMapTypeChanged;
            GMap.MouseMove += MainMap_MouseMove;
            GMap.MouseLeftButtonDown += MainMap_MouseLeftButtonDown;
            GMap.MouseEnter += MainMap_MouseEnter;
            GMap.OnMapZoomChanged += MainMap_ZoomChanged;

            // set current marker
            _currentMarker = new GMapMarker(GMap.Position);
            _currentMarkerUI = new CustomMarkerRed(GMap, _currentMarker, "custom position marker");
            _currentMarkerUI.Visibility = Visibility.Hidden;
            _currentMarker.ZIndex = int.MaxValue;
            GMap.Markers.Add(_currentMarker);
        }

        private void MainMap_Loaded(object sender, RoutedEventArgs e)
        {
            GMap.Zoom = 4;
            GMap.Position = new PointLatLng(45, -93);
        }

        void MainMap_MouseEnter(object sender, MouseEventArgs e)
        {
            GMap.Focus();
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

        void MainMap_ZoomChanged()
        {
             _route.UpdateRouteAndCarRefresh(GMap);

           //sliderZoom.Minimum = MainMap.MinZoom;
            //sliderZoom.Maximum = MainMap.MaxZoom;
        }

        void MainMap_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(GMap);
            _currentMarker.Position = GMap.FromLocalToLatLng((int)p.X, (int)p.Y);
        }

        // move current marker with left holding
        void MainMap_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point p = e.GetPosition(this);
                _currentMarker.Position = GMap.FromLocalToLatLng((int)p.X, (int)p.Y);
            }
        }

        // zoo max & center markers
        private void button13_Click(object sender, RoutedEventArgs e)
        {
            GMap.ZoomAndCenterMarkers(null);

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

        // current location changed
        void MainMap_OnCurrentPositionChanged(PointLatLng point)
        {
            _route.UpdateRouteAndCarRefresh(GMap);
            //mapgroup.Header = "gmap: " + point;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class Map: GMapControl
    {
#if DEBUG
        private long _elapsedMilliseconds;

        private int _counter;
        readonly Typeface _tf = new Typeface("GenericSansSerif");

        public Map()
        {
            this.OnTileLoadComplete += MainMap_OnTileLoadComplete;
        }

        // tile loading stops
        void MainMap_OnTileLoadComplete(long elapsedMilliseconds)
        {
            _elapsedMilliseconds = elapsedMilliseconds;

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

            string msg = string.Format(CultureInfo.InvariantCulture,
               "{0:0.0} z, {1}, refresh: {2}, load: {3} ms, render: {4} ms",
               Zoom, this.MapProvider, _counter++, _elapsedMilliseconds, _delta);

            FormattedText text = new FormattedText(msg, CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight, _tf, 20, Brushes.DarkOliveGreen);

            drawingContext.DrawText(text, new Point(4, 50));

            text = null;
        }
#endif
    }
}
