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
using WpfAnalogClock.Tools;

namespace WpfAnalogClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Visibility BtnVisibility { get; set; }

        private DateTime _timeLastMove = DateTime.Now;

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();

            BtnVisibility = Visibility.Visible;
            OptionsData.Instance.OnPropertyChange = (prop) => { UpdateOptions(false); };

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 240);

            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if(this.IsMouseOver)
                _timeLastMove = DateTime.Now;

            if ((DateTime.Now - _timeLastMove).TotalMilliseconds > 800)
            {
                if (BtnVisibility != Visibility.Hidden)
                {
                    BtnVisibility = Visibility.Hidden;
                    PropertyChanged(this, new PropertyChangedEventArgs("BtnVisibility"));
                }
            }
        }

        private void ClockGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowStyle == WindowStyle.None && e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MainWindow_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(Width + e.Delta < 200 || Width + e.Delta > 1600)
                return;
            if(Height + e.Delta < 200 || Height + e.Delta > 1000)
                return;

            this.Width += e.Delta;
            this.Height += e.Delta;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateOptions(bool fromControls)
        {
            if (fromControls)
            {
                OptionsData.Instance.DigitalClockVisibility = clock.DigitalClockVisibility;
                OptionsData.Instance.DigitalClockColor = clock.DigitalClockColor.ToWinformsColor();

                OptionsData.Instance.OptionsButtonColor = btnOptions.Background.ToWinformsColor();
                OptionsData.Instance.CloseButtonColor = btnClose.Background.ToWinformsColor();
            }
            else
            {
                clock.DigitalClockVisibility = OptionsData.Instance.DigitalClockVisibility;
                clock.DigitalClockColor = OptionsData.Instance.DigitalClockColor.ToWpfBrush();
                btnOptions.Background = OptionsData.Instance.OptionsButtonColor.ToWpfBrush();
                btnClose.Background = OptionsData.Instance.CloseButtonColor.ToWpfBrush();

                ScreenSaver.ResetIdleTimer(OptionsData.Instance.DisableScreenSaver);
                clock.DisableScreensaver = OptionsData.Instance.DisableScreenSaver;
            }
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            UpdateOptions(true);
            OptionsData.Instance.ShowOptions(this);
            UpdateOptions(false);
        }

        private void ClockGrid_MouseMove(object sender, MouseEventArgs e)
        {
            _timeLastMove = DateTime.Now;
            if (BtnVisibility != Visibility.Visible)
            {
                BtnVisibility = Visibility.Visible;
                PropertyChanged(this, new PropertyChangedEventArgs("BtnVisibility"));
            }
        }

        private void txtOptions_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtOptions.Width = 10 + txtOptions.Text.Length * 9;
        }
    }
}
