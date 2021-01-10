using MkZ.MediaPlayer.Controls;
using MkZ.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MkZ.WPF
{
    /// <summary>
    /// Interaction logic for PopupInfoWindow.xaml
    /// </summary>
    public partial class PopupVolumeControlWindow : Window
    {
        private AnimationHelper _controlsHideAndShow;

        public TimeSpan CloseTimeOut { get; set; } = TimeSpan.FromSeconds(2);

        public string InfoText
        {
            get { return (string)GetValue(InfoTextProperty); }
            set { SetValue(InfoTextProperty, value); }
        }

        public static readonly DependencyProperty InfoTextProperty =
            DependencyProperty.Register("InfoText", typeof(string), typeof(PopupVolumeControlWindow), new PropertyMetadata(""));

        private static PopupVolumeControlWindow wndVolume = null;
        public static void Show(Control ancor, object dataContext)
        {
            if (wndVolume != null)
                return;
            //estimate height - assuming volume button is in bottom-right position
            double sizeY = ancor.TranslatePoint(new Point(), Application.Current.MainWindow).Y / 2.0;
            if (sizeY < 160) sizeY = 160;
            double sizeX = sizeY / 4.0;

            Point location = ancor.PointToScreen(new Point(0, 0));
            location.Y -= sizeY;
            location.X -= (sizeX)/2.0;

            wndVolume = new PopupVolumeControlWindow(WindowStartupLocation.Manual, location, GetParentWindow(ancor));
            
            wndVolume.Width = sizeX;
            wndVolume.Height = sizeY;

            wndVolume.Foreground = ancor.Foreground;
            wndVolume.DataContext = dataContext;
            wndVolume.Closed += (s1, e1) => { wndVolume = null; };

            wndVolume.ShowFadeIn();
        }

        private static Window GetParentWindow(UIElement element)
        {
            var parent = LogicalTreeHelper.GetParent(element);
            while (parent != null && !(parent is Window))
            {
                parent = LogicalTreeHelper.GetParent(parent);
            }
            return parent as Window;
        }

        public PopupVolumeControlWindow() 
        {
            InitializeComponent(WindowStartupLocation.CenterScreen, new Point());
        }

        public PopupVolumeControlWindow(WindowStartupLocation startupLocation, Point location, Window owner)
        {
            this.Owner = owner;
            InitializeComponent(startupLocation, location);
        }

        //for WinForms
        public PopupVolumeControlWindow(WindowStartupLocation startupLocation, Point location, IntPtr ownerHandle)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            helper.Owner = ownerHandle;
            InitializeComponent(startupLocation, location);
        }

        private void InitializeComponent(WindowStartupLocation startupLocation, Point location)
        {
            InitializeComponent();

            this.WindowStartupLocation = startupLocation;

            this.Draggable(true);

            this.Left = location.X;
            this.Top = location.Y;

            _progress.ProgressTheme = GradientProgressBar.TicksTheme.GetBase100Theme();
            _progress.Maximum = 1;
            _progress.TickColor = Brushes.Navy;

            _controlsHideAndShow = new AnimationHelper(this, 2, this);
            _controlsHideAndShow.OnHideCompleted = (element) =>
            {
                if (element is Window wnd)
                    wnd.Close();
            }; 
        }

        private void ShowFadeIn()
        {
            this.Opacity = 0;
            this.Show();
            this.Visibility = Visibility.Hidden;
            _controlsHideAndShow.AnimateFadeIn();
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int delta = e.Delta / Math.Abs(e.Delta);

            PropertyInfo prop = DataContext.GetType().GetProperty("Volume");
            double volume = (double)prop.GetValue(DataContext);
            volume += 0.02 * delta;
            prop.SetValue(DataContext, volume);
        }
    }
}
