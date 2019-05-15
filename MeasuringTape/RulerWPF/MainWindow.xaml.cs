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
        const double rightMargin = 10;
        const double leftMargin = 600;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //AddAdorner(_ruler);
            DrawTicks();
            _canvasRuler.Draggable(true);
        }

        private void DrawTicks()
        {
            _tics.Children.Clear();

            double divider = 10;
            double tick_count = _canvasRuler.ActualWidth / divider;
            double tick_offset = _canvasRuler.ActualWidth / tick_count;

            double tick1Size = _canvasRuler.ActualHeight / 4;
            double tick2Size = _canvasRuler.ActualHeight / 3;
            double tick3Size = _canvasRuler.ActualHeight / 3;

            double tick1Interval = 1;
            double tick2Interval = 5;
            double tick3Interval = 10;

            for (int i = 1; i < tick_count; i++)
            {
                double tickSize = tick1Size;
                if (i % tick2Interval == 0)
                {
                    tickSize = tick2Size;
                }
                if (i % tick3Interval == 0)
                {
                    tickSize = tick3Size;
                }

                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 1;
                line.X1 = 1 + i * tick_offset;
                line.X2 = line.X1;
                line.Y1 = 0;
                line.Y2 = tickSize;

                _tics.Children.Add(line);

                if (i % tick3Interval == 0) //add text
                {
                    TextBlock txt = new TextBlock();
                    txt.FontSize = 16;
                    txt.Text = (divider * i).ToString();
                    Canvas.SetLeft(txt, line.X2 - 8);
                    Canvas.SetTop(txt, line.Y2);
                    _tics.Children.Add(txt);
                }
            }

            DrawInfo();
        }

        private void DrawInfo()
        {
            Point loc = new Point(Canvas.GetLeft(_canvasRuler), Canvas.GetTop(_canvasRuler));
            TranslateTransform tr = _canvasRuler.RenderTransform as TranslateTransform;
            if (tr != null)
                loc.Offset(tr.X, tr.Y);

            loc = this.PointToScreen(loc);
            _txtBounds.Text = string.Format("X: {0:0}, Y: {1:0}, Length: {2:0}",
                loc.X, loc.Y, _canvasRuler.Width);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            DrawInfo();
        }

        Point _startPosition;
        bool _isResizingR = false, _isResizingL = false;
        private void window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            const double MIN_WIDTH = 100;

            if (_isResizingR || _isResizingL)
            {
                Point currentPosition = Mouse.GetPosition(this);
                double diffX = currentPosition.X - _startPosition.X;
                double diffY = currentPosition.Y - _startPosition.Y;
                _startPosition = currentPosition;
                if(diffX == 0)
                    return;

                double left = Canvas.GetLeft(_canvasRuler);
                if (_isResizingR)
                {
                    if (_canvasRuler.Width + diffX > MIN_WIDTH) //min size
                        _canvasRuler.Width += diffX;
                    //this.Width = leftMargin + _canvasRuler.Width + rightMargin;
                }
                else
                {
                    if (_canvasRuler.Width - diffX > MIN_WIDTH) //min size
                    {
                        _canvasRuler.Width -= diffX;
                        Canvas.SetLeft(_canvasRuler, left + diffX);
                    }
                }

                //left = Canvas.GetLeft(_canvasRuler);
                //Canvas.SetLeft(_canvasRuler, leftMargin);
                //this.Left -= (leftMargin - left);

                DrawTicks();
            }
            DrawInfo();
        }

        private void rightGrip_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(rightGrip))
            {
                _isResizingR = true;
                _startPosition = Mouse.GetPosition(this);
                e.Handled = true;
            }
        }

        private void rightGrip_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isResizingR)
            {
                _isResizingR = false;
                Mouse.Capture(null);
            }
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

        private void _canvasRuler_MouseMove(object sender, MouseEventArgs e)
        {
            DrawInfo();
        }

        private void LeftGrip_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isResizingL)
            {
                _isResizingL = false;
                Mouse.Capture(null);

                //double left = Canvas.GetLeft(_canvasRuler);
                //Canvas.SetLeft(_canvasRuler, leftMargin);

                //this.Width = leftMargin + _canvasRuler.Width + rightMargin;
                //this.Left -= leftMargin - left;
            }
        }
    }
}
