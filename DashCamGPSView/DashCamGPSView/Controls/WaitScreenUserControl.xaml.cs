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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace DashCamGPSView.Controls
{
    /// <summary>
    /// Interaction logic for WaitScreenUserControl.xaml
    /// https://github.com/thomaslevesque/WpfAnimatedGif
    /// </summary>
    public partial class WaitScreenUserControl : UserControl
    {
        public WaitScreenUserControl()
        {
            InitializeComponent();
        }

        public void Show() { Show(RepeatBehavior.Forever); }

        public void ShowTime(TimeSpan time) 
        { 
            Show(new RepeatBehavior(time));
            ImageBehavior.AddAnimationCompletedHandler(animatedImage, Image_AnimationCompleted);
        }

        public void ShowCount(double count) 
        { 
            Show(new RepeatBehavior(count));
            ImageBehavior.AddAnimationCompletedHandler(animatedImage, Image_AnimationCompleted);
        }

        public void Hide() { Visibility = Visibility.Collapsed; }

        public void Show(RepeatBehavior behaviour) 
        {
            ImageBehavior.SetRepeatBehavior(animatedImage, new RepeatBehavior(0));
            ImageBehavior.SetRepeatBehavior(animatedImage, behaviour);
            
            Visibility = Visibility.Visible; 
        }

        private void Image_AnimationCompleted(object sender, RoutedEventArgs e)
        {
            ImageBehavior.RemoveAnimationCompletedHandler(animatedImage, Image_AnimationCompleted);
            this.Visibility = Visibility.Collapsed;
        }
    }
}
