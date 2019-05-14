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

namespace RulerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //AddAdorner(_ruler);
            DrawTicks();
        }

        //private void AddAdorner(UIElement element)
        //{
        //    AdornerLayer adornerlayer = AdornerLayer.GetAdornerLayer(element);
        //    if (adornerlayer.GetAdorners(element) == null || adornerlayer.GetAdorners(element).Length == 0)
        //    {
        //        RotateResizeAdorner adorner = new RotateResizeAdorner(element);
        //        adornerlayer.Add(adorner);
        //    }
        //}

        //private void RemoveAllAdorners()
        //{
        //    foreach (UIElement element in _canvas.Children)
        //    {
        //        AdornerLayer adornerlayer = AdornerLayer.GetAdornerLayer(element);
        //        var adorners = adornerlayer.GetAdorners(element);
        //        if (adorners != null)
        //        {
        //            for (int i = adorners.Length - 1; i >= 0; i--)
        //            {
        //                adornerlayer.Remove(adorners[i]);
        //            }
        //        }
        //    }
        //}
        private void _ruler_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void DrawTicks()
        {
            _tics.Children.Clear();
            double line_count = _canvas.ActualWidth / 10;
            double line_offset = (_canvas.ActualWidth - 3) / line_count;
            double smallDelta = _canvas.ActualHeight / 4;
            double bigDelta = _canvas.ActualHeight / 3;
            double tickHeight = _canvas.ActualHeight / 2;

            for (int i = 0; i < line_count + 1; i++)
            {
                bool odd = i % 2 == 1;
                bool ten = i%10 == 0;
                double delta = odd ? smallDelta : bigDelta;
                if (ten) delta = tickHeight;

                    Line line = new Line();
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 1;
                line.X1 = 1 + i * line_offset;
                line.X2 = line.X1;
                line.Y1 = 0;
                line.Y2 = delta;

                _tics.Children.Add(line);

                if (ten)
                {
                    TextBlock txt = new TextBlock();
                    txt.Text = i.ToString();
                    Canvas.SetLeft(txt, line.X2 - 8);
                    Canvas.SetTop(txt, line.Y2);
                    _tics.Children.Add(txt);
                }
            }
        }

        Point _startPosition;
        bool _isResizingR = false, _isResizingL = false;
        private void resizeGrip_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(rightGrip))
            {
                _isResizingR = true;
                _startPosition = Mouse.GetPosition(this);
                e.Handled = true;
            }
        }

        private void window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_isResizingR || _isResizingL)
            {
                Point currentPosition = Mouse.GetPosition(this);
                double diffX = currentPosition.X - _startPosition.X;
                double diffY = currentPosition.Y - _startPosition.Y;
                _startPosition = currentPosition;
                if(diffX == 0)
                    return;

                if (_isResizingR)
                {
                    if (_canvas.Width + diffX > 100) //min size
                        _canvas.Width += diffX;
                }
                else
                {
                    if (_canvas.Width - diffX > 100) //min size
                    {
                        //this.Left += diffX;
                        _canvas.Width -= diffX;
                        double left = Canvas.GetLeft(_canvas);
                        Canvas.SetLeft(_canvas, left + diffX);
                    }
                }

                DrawTicks();
            }
        }

        private void resizeGrip_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isResizingR)
            {
                _isResizingR = false;
                Mouse.Capture(null);
            }
        }

        private void canvas_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!e.OriginalSource.Equals(rightGrip) && !e.OriginalSource.Equals(leftGrip))
                this.DragMove();
        }

        private void LeftGrip_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(leftGrip))
            {
                _isResizingL = true;
                _startPosition = Mouse.GetPosition(this);
                e.Handled = true;
            }
        }

        private void LeftGrip_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isResizingL)
            {
                _isResizingL = false;
                Mouse.Capture(null);
            }
        }
    }
}
