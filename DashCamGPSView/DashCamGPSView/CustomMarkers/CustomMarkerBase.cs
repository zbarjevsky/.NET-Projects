using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace DashCamGPSView.CustomMarkers
{
    public class CustomMarkerBase : UserControl
    {
        private Popup _popup;
        private Label _label;
        private Image _image;
        private GMapControl _map;

        protected GMapMarker _marker;

        public void SetImage(Image image)
        {
            _image = image;
        }

        public virtual void UpdateOffset(double width, double heigth)
        {
            _marker.Offset = new Point(-width / 2, -heigth); //bottom-middle
        }

        public CustomMarkerBase(GMapControl map, GMapMarker marker, string title)
        {
            this._map = map;
            this._marker = marker;

            _popup = new Popup();
            _label = new Label();

            marker.Shape = this;

            this.Loaded += new RoutedEventHandler(MarkerControl_Loaded);
            this.Unloaded += new RoutedEventHandler(MarkerControl_Unloaded);
            this.SizeChanged += new SizeChangedEventHandler(MarkerControl_SizeChanged);
            this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
            this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
            this.MouseMove += new MouseEventHandler(MarkerControl_MouseMove);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(MarkerControl_MouseLeftButtonUp);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(MarkerControl_MouseLeftButtonDown);

            _popup.Placement = PlacementMode.Mouse;
            {
                _label.Background = Brushes.Blue;
                _label.Foreground = Brushes.White;
                _label.BorderBrush = Brushes.WhiteSmoke;
                _label.BorderThickness = new Thickness(2);
                _label.Padding = new Thickness(5);
                _label.FontSize = 22;
                _label.Content = title;
            }
            _popup.Child = _label;
        }

        void MarkerControl_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.Assert(_image != null);
            if (_image.Source.CanFreeze)
            {
                _image.Source.Freeze();
            }
            UpdateOffset(this.ActualWidth, this.ActualHeight);
        }

        void MarkerControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= new RoutedEventHandler(MarkerControl_Unloaded);
            this.Loaded -= new RoutedEventHandler(MarkerControl_Loaded);
            this.SizeChanged -= new SizeChangedEventHandler(MarkerControl_SizeChanged);
            this.MouseEnter -= new MouseEventHandler(MarkerControl_MouseEnter);
            this.MouseLeave -= new MouseEventHandler(MarkerControl_MouseLeave);
            this.MouseMove -= new MouseEventHandler(MarkerControl_MouseMove);
            this.MouseLeftButtonUp -= new MouseButtonEventHandler(MarkerControl_MouseLeftButtonUp);
            this.MouseLeftButtonDown -= new MouseButtonEventHandler(MarkerControl_MouseLeftButtonDown);

            _marker.Shape = null;
            _image.Source = null;
            _image = null;
            _popup = null;
            _label = null;
        }

        void MarkerControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateOffset(e.NewSize.Width, e.NewSize.Height);
        }

        void MarkerControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
            {
                Point p = e.GetPosition(_map);
                _marker.Position = _map.FromLocalToLatLng((int)p.X, (int)p.Y);
            }
        }

        void MarkerControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMouseCaptured)
            {
                Mouse.Capture(this);
            }
        }

        void MarkerControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Mouse.Capture(null);
            }
        }

        void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            _marker.ZIndex -= 10000;
            _popup.IsOpen = false;
        }

        void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            _marker.ZIndex += 10000;
            _popup.IsOpen = true;
        }
    }
}
