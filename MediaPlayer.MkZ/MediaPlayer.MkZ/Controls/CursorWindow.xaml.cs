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
using System.Windows.Shapes;

namespace MkZ.MediaPlayer.Controls
{
    /// <summary>
    /// Interaction logic for CursorWindow.xaml
    /// </summary>
    public partial class CursorWindow : Window
    {
        private Window _owner;

        public CursorWindow()
        {
            InitializeComponent();
        }

        public static void ShowCursor(Window parent)
        {
            CursorWindow wnd = new CursorWindow();
            wnd._owner = parent;
            //wnd.Owner = parent;
            //wnd.WindowStartupLocation = WindowStartupLocation.Manual;
            //wnd.Focusable = false;
            wnd.LoadCursor();
            //parent.Cursor = Cursors.None;
        }

        public void LoadCursor()
        {
            //Placement = PlacementMode.Relative;
            //PlacementTarget = _owner;
            //VerticalOffset = 0;
            //HorizontalOffset = 0;

            Cursor = Cursors.None;
            _owner.Cursor = Cursors.None;

            Focusable = false;
            //StaysOpen = true;

            _owner.MouseMove += Grid_MouseMove;
            _owner.MouseEnter += Grid_MouseEnter;
            _owner.MouseLeave += Grid_MouseLeave;
            _owner.SizeChanged += Grid_SizeChanged;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(_owner);
            //VerticalOffset = pos.Y + 1;
            //HorizontalOffset = pos.X + 1;
            pos = PointToScreen(pos);
            this.Top = pos.Y + 1;
            this.Left = pos.X + 1;
            //_cursor.SetValue(Canvas.LeftProperty, pos.X);
            //_cursor.SetValue(Canvas.TopProperty, pos.Y);
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //IsOpen = true;
            _cursor.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            //IsOpen = false;
            //_cursor.Visibility = Visibility.Hidden;
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
