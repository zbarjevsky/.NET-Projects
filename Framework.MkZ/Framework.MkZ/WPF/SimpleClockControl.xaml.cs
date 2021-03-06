using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
using System.Xml.Serialization;
using MkZ.Windows;
using MkZ.WPF.Converters;
using MkZ.WPF.PropertyGrid;

namespace MkZ.WPF
{
    /// <summary>
    /// Interaction logic for SimpleClockControl.xaml
    /// </summary>
    public partial class SimpleClockControl : UserControl
    {
        public class ClockConfig : NotifyPropertyChangedImpl
        {
            private SerializableFontForWpf _font = new SerializableFontForWpf();
            [Category("Clock")]
            [TypeConverter(typeof(ExpandableObjectConverter))]
            [DisplayName("Clock Font")]
            public SerializableFontForWpf ClockFont
            {
                get { return _font; }
                set { SetProperty(ref _font, value); }
            }

            private SerializableBrush _background = new SerializableBrush(Brushes.Black);
            [Category("Clock")]
            //[TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush Background { get => _background; set => SetProperty(ref _background, value); }

            private SerializableBrush _foreground = new SerializableBrush(Brushes.Wheat);
            [Category("Clock")]
            //[TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush Foreground { get => _foreground; set => SetProperty(ref _foreground, value); }

            private SerializableBrush _tickColor = new SerializableBrush(Brushes.Wheat);
            [Category("Clock")]
            //[TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush TickColor { get => _tickColor; set => SetProperty(ref _tickColor, value); }

            private SerializableBrush _hourHandBrush = new SerializableBrush(Brushes.Goldenrod);
            [Category("Clock")]
            //[TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush HourHandBrush { get => _hourHandBrush; set => SetProperty(ref _hourHandBrush, value); }

            private SerializableBrush _minuteHandBrush = new SerializableBrush(Brushes.DarkCyan);
            [Category("Clock")]
            //[TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush MinuteHandBrush { get => _minuteHandBrush; set => SetProperty(ref _minuteHandBrush, value); }

            private SerializableBrush _secondHandBrush = new SerializableBrush(Brushes.Red);
            [Category("Clock")]
            //[TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush SecondHandBrush { get => _secondHandBrush; set => SetProperty(ref _secondHandBrush, value); }

            private SerializableBrush _knobBrush = new SerializableBrush(Brushes.Red);
            [Category("Clock")]
            //[TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush KnobBrush { get => _knobBrush; set => SetProperty(ref _knobBrush, value); }

            private double _refreshRate = 1000.0;
            [Category("Clock")]
            [DisplayName("Clock Refresh Rate (ms)")]
            public double RefreshTime
            {
                get { return _refreshRate; }
                set 
                {
                    if (value > 1000)
                        value = 1000.0;
                    if (value < 10)
                        value = 10;

                    SetProperty(ref _refreshRate, value); 
                }
            }

            public bool IsValid()
            {
                return Foreground.C.A != 0 && HourHandBrush.C.A != 0;
            }
        }

        public Grid Grid => _gridMain;

        public ScrollDragZoomControl Zoomable { get; private set; }

        public static implicit operator ScrollDragZoomControl(SimpleClockControl clock) => clock.Zoomable;

        DispatcherTimer _timer = new DispatcherTimer();

        private ClockConfig Config => DataContext as ClockConfig;

        public bool IsClockVisible
        {
            get { return (bool)GetValue(IsClockVisibleProperty); }
            set { SetValue(IsClockVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsClockVisibleProperty =
            DependencyProperty.Register(nameof(IsClockVisible), typeof(bool), typeof(SimpleClockControl),
                new PropertyMetadata(true, OnIsClockVisibleChanged));

        private static void OnIsClockVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is SimpleClockControl This)
            {
                This.Visibility = This.IsClockVisible ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public SimpleClockControl()
        {
            DataContext = new ClockConfig();

            InitializeComponent();

            UpdateHands();

            _timer.Interval = TimeSpan.FromMilliseconds(Config.RefreshTime);
            _timer.Tick += timer_Tick;

            FadeAnimationHelper animationHelper = new FadeAnimationHelper(this, 2, _btnHide, _btnSettings);

            Zoomable = new ScrollDragZoomControl(this, null, true);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _timer.Interval = TimeSpan.FromMilliseconds(Config.RefreshTime);
            Config.PropertyChanged += Config_PropertyChanged;
            CreateTickMarks();
        }

        private void Config_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ClockConfig.RefreshTime))
                _timer.Interval = TimeSpan.FromMilliseconds(Config.RefreshTime);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateHands();
        }

        private void UpdateHands()
        {
            DateTime now = DateTime.Now;

            _minute.Angle = now.TimeOfDay.TotalMinutes * 6.0;
            _hour.Angle = now.TimeOfDay.TotalHours * 30.0;

            double second = (Config.RefreshTime < 300) ? now.TimeOfDay.TotalSeconds : now.TimeOfDay.Seconds;
            _second.Angle = second * 6.0;
        }

        private void CreateTickMarks()
        {
            Point center = new Point(200, 200);
            for (int i = 0; i < 60; i++)
            {
                Path tick = new Path();
                tick.Stroke = Brushes.Red;
                if(i%5 == 0)
                    tick.StrokeThickness = 5;
                else
                    tick.StrokeThickness = 2;

                tick.SetBinding(Path.StrokeProperty, "TickColor.B");

                double angle = i * 6;

                tick.RenderTransform = new RotateTransform(angle, center.X, center.Y);

                GeometryGroup group = new GeometryGroup();
                group.Children.Add(new LineGeometry(new Point(center.X, -7), new Point(center.X, -20)));
                tick.Data = group;

                _canvas.Children.Add(tick);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IsClockVisible = (this.Visibility == Visibility.Visible);
            if (IsClockVisible)
                _timer.Start();
            else
                _timer.Stop();
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.ShowOptions(Application.Current.MainWindow, DataContext, "Clock Settings", 450, "Clock Font");
        }

        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            IsClockVisible = false;
        }
    }
}
