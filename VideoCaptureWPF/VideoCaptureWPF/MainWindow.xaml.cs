using DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MkZ.WPF.VideoCapture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CaptureLogic _captureLogic;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> listDevices = CaptureLogic.EnumCaptureDevices();

            Window wnd = Application.Current.MainWindow;
            var wih = new WindowInteropHelper(wnd);
            IntPtr hWnd = wih.Handle;
            _captureLogic = new CaptureLogic(_mainGrid, listDevices[1]);
        }

        //private async void Click()
        //{
        //    StackPanel sp = new StackPanel();
        //    FontIcon fi = new FontIcon()
        //    {
        //        FontFamily = new FontFamily("Segoe UI Emoji"),
        //        Glyph = "\U0001F42F",
        //        FontSize = 50
        //    };
        //    sp.Children.Add(fi);
        //    TextBlock tb = new TextBlock();
        //    tb.HorizontalAlignment = HorizontalAlignment.Center;
        //    tb.Text = "You clicked on the Button !";
        //    sp.Children.Add(tb);
        //    ContentDialog cd = new ContentDialog()
        //    {
        //        Title = "Information",
        //        Content = sp,
        //        CloseButtonText = "Ok"
        //    };
        //    cd.XamlRoot = this.Content.XamlRoot;
        //    var res = await cd.ShowAsync();
        //}

        private async void GrabImage()
        {

            image1.Source = _captureLogic.GrabImage();

            //   <Image Source = "Assets/butterfly.png" RenderTransformOrigin = "0.5,0.5" >   
            //     <Image.RenderTransform>   
            //       <ScaleTransform ScaleY = "-1" > </ScaleTransform>    
            //     </Image.RenderTransform>
            //   </Image>

            image1.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform scaleTransform1 = new ScaleTransform();
            //myScaleTransform.ScaleX = -1;
            scaleTransform1.ScaleY = -1;
            TransformGroup transformGroup1 = new TransformGroup();
            transformGroup1.Children.Add(scaleTransform1);
            image1.RenderTransform = transformGroup1;
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            //myButton.Content = "Clicked";
            //Click();
            GrabImage();
        }

        private void btnOverlay_Click(object sender, RoutedEventArgs e)
        {
            bool bOverlay = _captureLogic.ToggleOverlay();
            btnOverlay.Content = bOverlay?"Set Overlay": "Remove Overlay";
        }
    }
}
