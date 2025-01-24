using Microsoft.Win32;
using MkZ.WPF;
using MultiPlayer.MkZ.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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
using Brushes = System.Windows.Media.Brushes;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Point = System.Windows.Point;

namespace MultiPlayer
{
    /// <summary>
    /// Interaction logic for VideoCommandsUserControl.xaml
    /// </summary>
    public partial class VideoCommandsUserControl : System.Windows.Controls.UserControl
    {
        public VideoCommandsVM VM { get; } = new VideoCommandsVM();

        public VideoCommandsUserControl()
        {
            this.DataContext = VM;

            InitializeComponent();
        }

        public void Init(VideoPlayerUserControl v)
        {
            VM.Init(v, this);
        }

        public void Update(OnePlayerSettings s, bool pop = false)
        {
            VM.Update(s, pop);
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VM.Volume_ValueChanged(sender, e);
        }

        private void Speed_Selected(object sender, SelectionChangedEventArgs e)
        {
            VM.SetSpeed(_speed.SelectedIndex);
        }

        private void Fit_Selected(object sender, SelectionChangedEventArgs e)
        {
            VM.Fit_Selected(sender, e);
        }
        private void Pos_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            VM.Pos_DragStarted(sender, e);
        }

        private void Pos_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VM.Pos_ValueChanged(sender, e);
        }

        private void Pos_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            VM.Pos_DragCompleted(sender, e);
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            VM.Maximize_Click(sender, e);
        }

        private void Pos_MouseMove(object sender, MouseEventArgs e)
        {
            VM.Pos_MouseMove(sender, e);
        }

        private void Pos_MouseLeave(object sender, MouseEventArgs e)
        {
            VM.Pos_MouseLeave(sender, e);
        }

        private void PrevFrame_Click(object sender, RoutedEventArgs e)
        {
            VM.PrevFrame_Click(sender, e);
        }

        private void NextFrame_Click(object sender, RoutedEventArgs e)
        {
            VM.NextFrame_Click(sender, e);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            VM.UserControl_SizeChanged(sender, e);
        }
    }
}
