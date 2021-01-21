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

namespace MkZ.WPF.Buttons
{
    /// <summary>
    /// Interaction logic for ButtonWithTextRepeat.xaml
    /// </summary>
    public partial class ButtonWithTextRepeat : RepeatButton
    {
        public string ButtonText
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(ButtonText), typeof(string), typeof(ButtonWithTextRepeat), new PropertyMetadata(""));

        public ButtonWithTextRepeat()
        {
            InitializeComponent();
        }
    }
}
