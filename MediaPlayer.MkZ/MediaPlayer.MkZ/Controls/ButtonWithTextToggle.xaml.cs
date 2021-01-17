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
    /// Interaction logic for ButtonWithTextToggle.xaml
    /// </summary>
    public partial class ButtonWithTextToggle : ToggleButton
    {
        public string ButtonTextUnchecked
        {
            get { return (string)GetValue(UncheckedTextProperty); }
            set { SetValue(UncheckedTextProperty, value); }
        }

        public static readonly DependencyProperty UncheckedTextProperty =
            DependencyProperty.Register(nameof(ButtonTextUnchecked), typeof(string), typeof(ButtonWithTextToggle), new PropertyMetadata(""));

        public string ButtonTextChecked
        {
            get { return (string)GetValue(CheckedTextProperty); }
            set { SetValue(CheckedTextProperty, value); }
        }

        public static readonly DependencyProperty CheckedTextProperty =
            DependencyProperty.Register(nameof(ButtonTextChecked), typeof(string), typeof(ButtonWithTextToggle), new PropertyMetadata(""));

        public ButtonWithTextToggle()
        {
            InitializeComponent();
        }
    }
}
