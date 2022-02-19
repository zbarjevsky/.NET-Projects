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

namespace MkZ.WeatherStation.BlueMaestro.UX
{
    /// <summary>
    /// Interaction logic for BMDeviceRecordUserControl.xaml
    /// </summary>
    public partial class BMDeviceRecordUserControl : UserControl
    {
        public BMDeviceRecordUserControl()
        {
            InitializeComponent();
        }

        //IsSelected="{Binding Path=IsSelected, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type ListViewItem}}, 
        //Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"

        //public static readonly DependencyProperty IsSelectedProperty =
        //    DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(BMDeviceRecordUserControl), null);

        //public bool IsSelected
        //{
        //    get
        //    {
        //        return (bool)GetValue(IsSelectedProperty);
        //    }

        //    set
        //    {
        //        SetValue(IsSelectedProperty, value);
        //    }
        //}
    }
}
