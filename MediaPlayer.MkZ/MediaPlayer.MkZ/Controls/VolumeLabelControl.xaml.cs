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
using System.Windows.Threading;

using MkZ.WPF;

namespace MkZ.MediaPlayer.Controls
{
    /// <summary>
    /// Interaction logic for FadingTextControl.xaml
    /// </summary>
    public partial class VolumeLabelControl : UserControl
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private int _counter = 0;
        private bool _bAllowShow = false, _bAllowHide = false;

        public VolumeLabelControl()
        {
            InitializeComponent();
            
            _timer.Interval = TimeSpan.FromSeconds(0.233);
            _timer.Tick += timer_Tick;
            _timer.Start();

            _bAllowShow = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            _counter++;
            if(_bAllowHide && _counter > 5 )
            {
                _bAllowHide = false;
                FadeAnimationHelper.VisibilityHideAnimation(this, 0, Visibility.Hidden, (e1) => { _bAllowShow = true; });
            }
            _timer.Start();
        }

        private void TextBlock_TextUpdated(object sender, DataTransferEventArgs e)
        {
            _counter = 0;
            if (_bAllowShow)
            {
                _bAllowShow = false;
                FadeAnimationHelper.VisibilityShowAnimation(this, 0, (e1) => { _bAllowHide = true; });
            }
        }
    }
}
