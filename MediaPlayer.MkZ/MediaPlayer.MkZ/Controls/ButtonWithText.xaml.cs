using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MkZ.MediaPlayer.Controls
{
    /// <summary>
    /// Interaction logic for ButtonSkipForward.xaml
    /// </summary>
    public partial class ButtonWithText : RepeatButton
    {
        public string ButtonText
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(ButtonText), typeof(string), typeof(ButtonWithText), new PropertyMetadata(""));

        //public double TextFontSize
        //{
        //    get { return (double)GetValue(TextFontSizeProperty); }
        //    set { SetValue(TextFontSizeProperty, value); }
        //}

        //public static readonly DependencyProperty TextFontSizeProperty =
        //    DependencyProperty.Register(nameof(FontSize), typeof(string), typeof(ButtonWithText), new PropertyMetadata(""));

        public ButtonWithText()
        {
            InitializeComponent();

            ButtonText = "++";
        }
    }
}
