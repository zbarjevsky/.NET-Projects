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
    /// Interaction logic for CursorZoom.xaml
    /// </summary>
    public partial class CursorArrow : UserControl
    {
        private Grid _parentGrid = null;

        public CursorArrow()
        {
            InitializeComponent();
        }

        public void Load_Cursor(Grid grid)
        {
            _parentGrid = grid;

            Grid_SizeChanged(_parentGrid, null);

            _parentGrid.Cursor = Cursors.None;
            
            if(_parentGrid.RowDefinitions.Count>0)
                Grid.SetRowSpan(this, _parentGrid.RowDefinitions.Count);
            if(_parentGrid.ColumnDefinitions.Count > 0)
                Grid.SetColumnSpan(this, _parentGrid.ColumnDefinitions.Count);

            _parentGrid.Children.Add(this);

            _parentGrid.MouseMove += Grid_MouseMove;
            _parentGrid.MouseEnter += Grid_MouseEnter;
            _parentGrid.MouseLeave += Grid_MouseLeave;
            _parentGrid.SizeChanged += Grid_SizeChanged;
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = _parentGrid.ActualWidth;
            double height = _parentGrid.ActualHeight;
            double size = Math.Min(width, height);
            size /= 20;
            if (size < 30)
                size = 30;
            if (size > 100)
                size = 100;

            _cursor.Width = size;
            _cursor.Height = size;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            _cursor.Visibility = Visibility.Hidden;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            _cursor.Visibility = Visibility.Visible;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(ContentPanel);
            _cursor.SetValue(Canvas.LeftProperty, pos.X);
            _cursor.SetValue(Canvas.TopProperty, pos.Y);
        }
    }
}
