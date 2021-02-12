using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MkZ.WPF.Buttons
{
    /// <summary>
    /// Interaction logic for ButtonImageToggle.xaml
    /// </summary>
    public partial class ButtonImageToggle : ToggleButton
    {
        public const string CLOCK_IMAGE_PATH = "Images/Clock48x48.png";

        public ImageSource ImageUnchecked
        {
            get { return (ImageSource)GetValue(UncheckedImageProperty); }
            set { SetValue(UncheckedImageProperty, value); }
        }

        public static readonly DependencyProperty UncheckedImageProperty =
            DependencyProperty.Register(nameof(ImageUnchecked), typeof(ImageSource), typeof(ButtonImageToggle), 
                new PropertyMetadata(GetFromResource(CLOCK_IMAGE_PATH)));

        public ImageSource ImageChecked
        {
            get { return (ImageSource)GetValue(CheckedImageProperty); }
            set { SetValue(CheckedImageProperty, value); }
        }

        public static readonly DependencyProperty CheckedImageProperty =
            DependencyProperty.Register(nameof(ImageChecked), typeof(ImageSource), typeof(ButtonImageToggle), 
                new PropertyMetadata(GetFromResource(CLOCK_IMAGE_PATH)));

        public ButtonImageToggle()
        {
            InitializeComponent();

            //var key = Resources["defaultClock"];
            //Resources["defaultClock"] = GetFromResource("Images/Clock48x48.png");
        }

        public static BitmapImage GetFromResource(string path)
        {
            return WPF_Helper.GetResourceImage(path);
        }
    }
}
