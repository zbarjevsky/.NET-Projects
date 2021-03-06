using MkZ.WPF.Converters;
using MkZ.WPF.PropertyGrid;
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

namespace MkZ.WPF
{
    /// <summary>
    /// Interaction logic for CursorZoom.xaml
    /// </summary>
    public partial class CursorArrow : UserControl
    {
        private Grid _parentGrid = null;
        private double _sizeRatio = 20.0;

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register(nameof(Stroke), typeof(Brush), typeof(CursorArrow), 
                new PropertyMetadata(Brushes.Yellow));

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(CursorArrow), 
                new PropertyMetadata(Brushes.Goldenrod));

        public CursorArrow()
        {
            InitializeComponent();

            DataContext = this;

            //SetCursorPos(new Point(100, 100));
            //_cursor.Visibility = Visibility.Hidden;
        }

        public void Load_Cursor(Grid grid, double sizeRatio)
        {
            _parentGrid = grid;
            _sizeRatio = sizeRatio;

            Grid_SizeChanged(_parentGrid, null);

            _parentGrid.Cursor = Cursors.None;
            _cursor.Visibility = Visibility.Hidden;

            if (_parentGrid.RowDefinitions.Count > 0)
                Grid.SetRowSpan(this, _parentGrid.RowDefinitions.Count);
            if (_parentGrid.ColumnDefinitions.Count > 0)
                Grid.SetColumnSpan(this, _parentGrid.ColumnDefinitions.Count);

            _parentGrid.Children.Add(this);

            _parentGrid.MouseMove += Grid_MouseMove;
            _parentGrid.MouseEnter += Grid_MouseEnter;
            _parentGrid.MouseLeave += Grid_MouseLeave;
            _parentGrid.SizeChanged += Grid_SizeChanged;
        }

        public void BindToColor(object bindingSource, string bindingPath)
        {
            Binding binding = new Binding(bindingPath);
            binding.Source = bindingSource;
            this.SetBinding(StrokeProperty, binding);

            binding = new Binding(bindingPath);
            binding.Source = bindingSource;
            binding.Converter = new BrushOpacityConverter();
            binding.ConverterParameter = 0.5;
            this.SetBinding(FillProperty, binding);
        }

        public void SetCursorSize(double size)
        {
            _cursor.Width = size;
            _cursor.Height = size;
        }

        public void SetCursorPos(Point pos)
        {
            _cursor.SetValue(Canvas.LeftProperty, pos.X);
            _cursor.SetValue(Canvas.TopProperty, pos.Y);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = _parentGrid.ActualWidth;
            double height = _parentGrid.ActualHeight;
            double size = height; // Math.Min(width, height);
            size /= _sizeRatio;
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
            SetCursorPos(e.GetPosition(ContentPanel));
        }
    }
}
