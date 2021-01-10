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
        public CursorArrow()
        {
            InitializeComponent();
        }

        public void Load_Cursor(Grid grid, double width, double height)
        {
            _cursor.Width = width;
            _cursor.Height = height;

            grid.Cursor = Cursors.None;
            
            if(grid.RowDefinitions.Count>0)
                Grid.SetRowSpan(this, grid.RowDefinitions.Count);
            if(grid.ColumnDefinitions.Count > 0)
                Grid.SetColumnSpan(this, grid.ColumnDefinitions.Count);

            grid.Children.Add(this);

            grid.MouseMove += Grid_MouseMove;
            grid.MouseEnter += Grid_MouseEnter;
            grid.MouseLeave += Grid_MouseLeave;
        }

        public void SetSize(double width, double height)
        {
            _cursor.Width = width;
            _cursor.Height = height;
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
