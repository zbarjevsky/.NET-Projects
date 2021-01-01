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

namespace MkZ.MediaPlayer.Controls
{
    /// <summary>
    /// Interaction logic for PlayerControls.xaml
    /// </summary>
    public partial class PlayerControls : UserControl
    {
        public PlayerControls()
        {
            InitializeComponent();
        }

        private bool _resume = false;
        private void Slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            if(DataContext is VideoPlayerControlVM vm)
            {
                _resume = (vm.MediaState == MediaState.Play);
                if(_resume)
                    vm.Pause();
            }
        }

        private void Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (DataContext is VideoPlayerControlVM vm)
            {
                if (_resume)
                    vm.Play();
            }
        }
    }
}
