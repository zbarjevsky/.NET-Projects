using System;
using System.Collections.Generic;
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

namespace MkZ.WPF.Buttons
{
    /// <summary>
    /// Interaction logic for ButtonIcon16x16.xaml
    /// </summary>
    public partial class ButtonWithIcon : Button
    {
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(ButtonWithIcon), new PropertyMetadata(""));

        public ButtonWithIcon()
        {
            InitializeComponent();
        }
    }
}
