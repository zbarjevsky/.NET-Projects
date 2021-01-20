using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            [Category("Clock"), TypeConverter(typeof(ExpandableObjectConverter))]
            [DisplayName("Clock Font")]
            public SerializableFontForWpf ClockFont
            {
                get { return _font; }
                set { SetProperty(ref _font, value); }
            }

            public double Zoom { get; set; } = 1.0;

            //location change from center
            public Point Offset { get; set; } = new Point();

            private SerializableBrush _background = new SerializableBrush(Brushes.Black);
            [Category("Clock"), TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush Background { get => _background; set => SetProperty(ref _background, value); }

            private SerializableBrush _foreground = new SerializableBrush(Brushes.Wheat);
            [Category("Clock"), TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush Foreground { get => _foreground; set => SetProperty(ref _foreground, value); }

            private SerializableBrush _hourHandBrush = new SerializableBrush(Brushes.Goldenrod);
            [Category("Clock"), TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush HourHandBrush { get => _hourHandBrush; set => SetProperty(ref _hourHandBrush, value); }

            private SerializableBrush _minuteHandBrush = new SerializableBrush(Brushes.DarkCyan);
            [Category("Clock"), TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush MinuteHandBrush { get => _minuteHandBrush; set => SetProperty(ref _minuteHandBrush, value); }

            private SerializableBrush _secondHandBrush = new SerializableBrush(Brushes.Red);
            [Category("Clock"), TypeConverter(typeof(ExpandableObjectConverter))]
            public SerializableBrush SecondHandBrush { get => _secondHandBrush; set => SetProperty(ref _secondHandBrush, value); }
        }

        public Grid Grid => _gridMain;

        public SimpleClockControl()
        {
            DataContext = new ClockConfig();

            InitializeComponent();

            UpdateHands();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.033);
            timer.Tick += timer_Tick;
            timer.Start();
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
    }
}
