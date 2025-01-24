using MultiPlayer.MkZ.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MultiPlayer
{
    /// <summary>
    /// Interaction logic for PopUpWindow.xaml
    /// </summary>
    public partial class PopUpWindow : Window
    {
        private bool AllowClose = false;

        public bool IsFullScreen => (this.WindowState == WindowState.Maximized && this.WindowStyle == WindowStyle.None);

        public PopUpWindow()
        {
            InitializeComponent();
        }

        public void InitWindow(Window main)
        {
            this.WindowState = main.WindowState;
            this.WindowStyle = WindowStyle.SingleBorderWindow;

            //position
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                this.ResizeMode = ResizeMode.CanResize;

                this.Left = main.Left;
                this.Top = main.Top;
                this.Width = main.ActualWidth;
                this.Height = main.ActualHeight;
            }
        }

        public void LoadSettings(OnePlayerSettings settings)
        {
            _video.LoadSetting(settings, true);
        }

        public void MaximizeToggle()
        {
            Maximize(!IsFullScreen);
        }

        public void Maximize(bool maximize)
        {
            if (maximize)
            {
                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.ResizeMode = ResizeMode.CanResize;
            }
        }

        public void Exit()
        {
            AllowClose = true;
            _video.Clear();
            this.Close();
        }

        public void ClearVideoControl()
        {
            _video.Clear();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None && e.Key == Key.Escape)
            {
                //if full screen - exit full screen on ESC
                if (IsFullScreen)
                    MaximizeToggle();
                else
                    this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true; //do not close it - will reuse it
                _video._commands.VM.MaximizeToggle(hide:true);
            }
        }
    }
}
