using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            _canvasRuler.Draggable(true);
            DrawTicks();
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
            loc.Offset(_moveTransform.X, _moveTransform.Y);

            loc = this.PointToScreen(loc);
            _txtBounds.Text = string.Format("X: {0:0}, Y: {1:0}, Length: {2:0}, Angle: {3:0.000}",
                loc.X, loc.Y, _canvasRuler.Width, _rotateTransform.Angle);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            DrawInfo();
        }

        Point _startPosition, _center;
        bool _isResizingR = false, _isResizingL = false, _isRotatingL = false;
        bool _mouseMove = false;
        private void window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            const double MIN_WIDTH = 100;

            if (_mouseMove)
                return;
            _mouseMove = true;

            Point currentPosition = Mouse.GetPosition(null);
            if (_isResizingR || _isResizingL)
            {
                double diff = CalculateDiff(currentPosition);
                if(diff == 0)
                {
                    _mouseMove = false;
                    return;
                }

                Debug.WriteLine("Diff: " + diff);

                double left = Canvas.GetLeft(_canvasRuler);
                if (_canvasRuler.Width + diff > MIN_WIDTH) //min size
                {
                    if (_isResizingL)
                    {
                        _canvasRuler.Width += diff;
                        _moveTransform.X -= diff;
                    }
                    else
                    {
                        _canvasRuler.Width += diff;
                    }

                    Canvas.SetLeft(thumbRotateRight, _canvasRuler.Width - 30);
                    Canvas.SetLeft(btnClose, _canvasRuler.Width - 30);
                }

                DrawTicks();

                _startPosition = currentPosition;
            }
            else if (_isRotatingL)
            {
                //Point currentPosition = Mouse.GetPosition(null);

                Vector startToCenter = (_center - _startPosition);
                Vector currToCenter = (_center - currentPosition);

                //Vector diffVector = (currToCenter - startToCenter);
                double deltaAngle = Vector.AngleBetween(startToCenter, currToCenter);

                SetAngle(_rotateTransform.Angle + deltaAngle);// (e.VerticalChange < 0 ? 1 : -1));

                _startPosition = currentPosition;
            }

            _center = _canvasRuler.PointToScreen(new Point(_canvasRuler.Width / 2, _canvasRuler.Height / 2));

            DrawInfo();
            _mouseMove = false;
        }

        private double CalculateDiff(Point currentPosition)
        {
            Vector startToCenter = (_center - _startPosition);
            Vector currToCenter = (_center - currentPosition);

            Vector diffVector = (currToCenter - startToCenter);
            double angle = Vector.AngleBetween(diffVector, currToCenter);

            double diff = diffVector.Length * Math.Cos(Math.PI * angle / 180.0);
            return diff;
        }

        private void rightGrip_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(rightGrip))
            {
                _isResizingR = true;
                _startPosition = Mouse.GetPosition(null);
                _center = _canvasRuler.PointToScreen(new Point(_canvasRuler.Width / 2, _canvasRuler.Height / 2));
                _canvasRuler.RenderTransformOrigin = new Point(0,0);
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
                _startPosition = Mouse.GetPosition(null);
                _center = _canvasRuler.PointToScreen(new Point(_canvasRuler.Width / 2, _canvasRuler.Height / 2));
                _canvasRuler.RenderTransformOrigin = new Point(1,0);
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

        private void _canvasRuler_MouseMove(object sender, MouseEventArgs e)
        {
            DrawInfo();
        }

        private void thumbRotateLeft_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(thumbRotateLeft))
            {
                _startPosition = Mouse.GetPosition(null);
                _isRotatingL = true;
            }
        }

        private void thumbRotateLeft_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isRotatingL)
            {
                Mouse.Capture(null);
            }
            _isRotatingL = false;
       }

        private void thumbRotateLeft_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            //Point currentPosition = Mouse.GetPosition(null);

            //Vector startToCenter = (_center - _startPosition);
            //Vector currToCenter = (_center - currentPosition);

            //Vector diffVector = (currToCenter - startToCenter);
            //double deltaAngle = Vector.AngleBetween(startToCenter, currToCenter);

            //SetAngle(_rotateTransform.Angle + deltaAngle);// (e.VerticalChange < 0 ? 1 : -1));

            //_startPosition = currentPosition;
        }

        private void thumbRotateRight_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            SetAngle(_rotateTransform.Angle + (e.VerticalChange > 0 ? 1 : -1));
        }

        private void SetAngle(double angle)
        {
            if (angle > 360)
                angle -= 360;
            if (angle < 0)
                angle += 360;

            CompensateOrigin(true);

            _rotateTransform.Angle = angle;
        }

        private void CompensateOrigin(bool moveToCenter)
        {
            Point ptLeftTop = _canvasRuler.PointToScreen(new Point());
            Point ptCenter = new Point(_canvasRuler.Width / 2, _canvasRuler.Height / 2);
            Vector delta = ptCenter - ptLeftTop;

            if(moveToCenter)
            {
                //if (_canvasRuler.RenderTransformOrigin.X != 0.5 && _canvasRuler.RenderTransformOrigin.Y != 0.5)
                {
                    _canvasRuler.RenderTransformOrigin = new Point(0.95, 0.5);
                    //_moveTransform.X += 10; // ptCenter.X;
                    //_moveTransform.Y += ptCenter.Y;
                }
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
