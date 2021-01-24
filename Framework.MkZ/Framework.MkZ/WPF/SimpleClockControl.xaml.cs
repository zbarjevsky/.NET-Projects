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
            private bool _isVisible = false;
            [Category("Clock")]
            public bool IsVisible
            {
                get { return _isVisible; }
                set { SetProperty(ref _isVisible, value); }
            }

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

            public OffsetAndZoom Bounds_FullScreen { get; set; } = new OffsetAndZoom();
            public OffsetAndZoom Bounds_Normal { get; set; } = new OffsetAndZoom();

            public bool IsValid()
            {
                return Foreground.C.A != 0 && HourHandBrush.C.A != 0;
            }
        }

        public Grid Grid => _gridMain;

        public ScrollDragZoomControl Zoomable { get; private set; }

        public static implicit operator ScrollDragZoomControl(SimpleClockControl clock) => clock.Zoomable;

        DispatcherTimer _timer = new DispatcherTimer();

        public SimpleClockControl()
        {
            DataContext = new ClockConfig();

            InitializeComponent();

            UpdateHands();

            _timer.Interval = TimeSpan.FromSeconds(0.266);
            _timer.Tick += timer_Tick;

            FadeAnimationHelper animationHelper = new FadeAnimationHelper(this, 2, _btnHide, _btnSettings);

            Zoomable = new ScrollDragZoomControl(this, null, true);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
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
            _second.Angle = now.TimeOfDay.TotalSeconds * 6.0;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
                _timer.Start();
            else
                _timer.Stop();
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow wnd = new OptionsWindow();
            wnd.Owner = Application.Current.MainWindow;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.Height = 450;
            wnd.Title = "Clock Settings";
            wnd.SetPropertiesObject(this.DataContext, "Clock Font");
            //wnd.ExpandAll();
            wnd.ShowDialog();
        }

        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ClockConfig config)
                config.IsVisible = false;
        }
    }
}
