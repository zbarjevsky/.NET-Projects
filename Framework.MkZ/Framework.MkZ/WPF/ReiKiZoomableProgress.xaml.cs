using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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


using MkZ.Windows;

namespace MkZ.WPF
{
    /// <summary>
    /// Interaction logic for ReiKiZoomableProgress.xaml
    /// </summary>
    public partial class ReiKiZoomableProgress : UserControl
    {
        public class ReiKiConfig : NotifyPropertyChangedImpl
        {
            public bool IsValid()
            {
                return true;
            }

            private bool _isVisible = false;
            [Category("ReiKi")]
            public bool IsVisible
            {
                get { return _isVisible; }
                set { SetProperty(ref _isVisible, value); }
            }

            public OffsetAndZoom Bounds_FullScreen { get; set; } = new OffsetAndZoom();
            public OffsetAndZoom Bounds_Normal { get; set; } = new OffsetAndZoom();

        }

        public ScrollDragZoomControl Zoomable { get; private set; }

        public static implicit operator ScrollDragZoomControl(ReiKiZoomableProgress reiKi) => reiKi.Zoomable;

        public ReiKiZoomableProgress()
        {
            DataContext = new ReiKiConfig();

            InitializeComponent();

            Zoomable = new ScrollDragZoomControl(this, null, true);
        }

        private void _control_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
