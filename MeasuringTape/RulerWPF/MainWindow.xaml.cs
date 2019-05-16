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
        RulerVM _vm = new RulerVM();

        public MainWindow()
        {
            DataContext = _vm;
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //_canvasRuler.Draggable(true);
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
            _txtBounds.Text = string.Format("X: {0:0}, Y: {1:0}, Length: {2:0}, Angle: {3:0.0}°",
                loc.X, loc.Y, _canvasRuler.Width, _rotateTransform.Angle);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            DrawInfo();
        }

        Point _startPosition;
        private void window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            const double MIN_WIDTH = 100;

            Point currentPosition = Mouse.GetPosition(null);
            double diff = CalculateDiff(currentPosition);
            if (diff == 0)
                return;

            //Debug.WriteLine("Diff: " + diff);

            if (_vm.MouseMoveOp == MouseMoveOp.LeftSize || _vm.MouseMoveOp == MouseMoveOp.RightSize)
            {

                if (_vm.oWidth + diff > MIN_WIDTH) //min size
                {
                    _vm.oWidth += diff;
                   if (_vm.MouseMoveOp == MouseMoveOp.LeftSize)
                    {
                        _vm.oTranslateTransformX -= diff;
                    }

                    _vm.oThumbLeft = _vm.oWidth - 30;
                }

                DrawTicks();

            }
            else if (_vm.MouseMoveOp == MouseMoveOp.LeftRotate || _vm.MouseMoveOp == MouseMoveOp.RightRotate)
            {
                //Point currentPosition = Mouse.GetPosition(null);

                Vector startToCenter = (_vm.Origin(_canvasRuler) - _startPosition);
                Vector currToCenter = (_vm.Origin(_canvasRuler) - currentPosition);
                double deltaAngle = Vector.AngleBetween(startToCenter, currToCenter);

                SetAngle(_vm.oAngle + deltaAngle);// (e.VerticalChange < 0 ? 1 : -1));
            }
            else if (_vm.MouseMoveOp == MouseMoveOp.Move)
            {
                Vector diffV = currentPosition - _startPosition;
                _vm.oTranslateTransformX += diffV.X;
                _vm.oTranslateTransformY += diffV.Y;
            }

            _startPosition = currentPosition;
            //Point origin = new Point(_vm.oRenderTransformOrigin.X * _vm.oWidth, _vm.oRenderTransformOrigin.Y * _canvasRuler.ActualHeight);
            //_origin = _canvasRuler.PointToScreen(origin);

            DrawInfo();
        }

        private double CalculateDiff(Point currentPosition)
        {
            Vector startToCenter = (_vm.Origin(_canvasRuler) - _startPosition);
            Vector currToCenter = (_vm.Origin(_canvasRuler) - currentPosition);

            Vector diffVector = (currToCenter - startToCenter);
            double angle = Vector.AngleBetween(diffVector, currToCenter);

            double diff = diffVector.Length * Math.Cos(Math.PI * angle / 180.0);
            return diff;
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _vm.MouseMoveOp = MouseMoveOp.None;
            Mouse.Capture(null);
        }

        private void LeftGrip_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(sender as IInputElement))
            {
                _vm.MouseMoveOp = MouseMoveOp.LeftSize;
                _startPosition = Mouse.GetPosition(null);
                _vm.UpdateRenderTransformOrigin(new Point(1,0), _canvasRuler);
                //Point origin = new Point(_vm.oRenderTransformOrigin.X * _vm.oWidth, _vm.oRenderTransformOrigin.Y * _canvasRuler.ActualHeight);
                //_origin = _canvasRuler.PointToScreen(origin);
                e.Handled = true;
            }
        }

        private void rightGrip_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(sender as IInputElement))
            {
                _vm.MouseMoveOp = MouseMoveOp.RightSize;
                _startPosition = Mouse.GetPosition(null);
                _vm.UpdateRenderTransformOrigin(new Point(0,0), _canvasRuler);
                //Point origin = new Point(_vm.oRenderTransformOrigin.X * _vm.oWidth, _vm.oRenderTransformOrigin.Y * _canvasRuler.ActualHeight);
                //_origin = _canvasRuler.PointToScreen(origin);
                e.Handled = true;
            }
        }

        private void thumbRotateLeft_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(sender as IInputElement))
            {
                _vm.MouseMoveOp = MouseMoveOp.LeftRotate;
                _startPosition = Mouse.GetPosition(null);
                _vm.UpdateRenderTransformOrigin(new Point(1, 0), _canvasRuler);
                e.Handled = true;
            }
        }

        private void thumbRotateRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(sender as IInputElement))
            {
                _vm.MouseMoveOp = MouseMoveOp.RightRotate;
                _startPosition = Mouse.GetPosition(null);
                _vm.UpdateRenderTransformOrigin(new Point(0, 0), _canvasRuler);
                e.Handled = true;
            }
        }

        private void _canvasRuler_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source == _canvasRuler || e.Source is TextBlock)
            {
                if (Mouse.Capture(_canvasRuler))
                {
                    _vm.MouseMoveOp = MouseMoveOp.Move;
                    _startPosition = Mouse.GetPosition(null);
                    //_vm.UpdateRenderTransformOrigin(new Point(0, 0), _canvasRuler);
                    e.Handled = true;
                }
            }
        }

        private void SetAngle(double angle)
        {
            if (angle > 360)
                angle -= 360;
            if (angle < 0)
                angle += 360;

            CompensateOrigin(true);

            _vm.oAngle = angle;
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
                    //_vm.UpdateRenderTransformOrigin(new Point(0.5, 0.5), _canvasRuler);
                    //_moveTransform.X += 10; // ptCenter.X;
                    //_moveTransform.Y += ptCenter.Y;
                }
            }
        }

        private void btnClose_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
