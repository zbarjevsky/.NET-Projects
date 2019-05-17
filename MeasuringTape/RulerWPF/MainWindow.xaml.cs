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
            DrawTicks();

            Rect bounds = new Rect(
                new Point(SystemParameters.VirtualScreenLeft, SystemParameters.VirtualScreenTop),
                new Size(SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenHeight));
            this.Left = bounds.Left;
            this.Top = bounds.Top;
            this.Width = bounds.Width;
            this.Height = bounds.Height;
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

            //double tick1Interval = 1;
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
            Point loc = _canvasRuler.PointToScreen(new Point());
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
            if(_vm.MouseMoveOp == MouseMoveOp.None)
                return;

            Point currentPosition = Mouse.GetPosition(null);
            double diff = CalculateDiff(currentPosition);
            if (diff == 0) //no change
                return;

            double distance = _vm.Distance(currentPosition, _vm.CurrentElement);
            Debug.WriteLine("Distance: " + distance);
            if (distance > 150) //too far - disengage
            {
                OnPreviewMouseLeftButtonUp(null, null);
                return;
            }

            const double MIN_WIDTH = 100;
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

                _vm.SetAngle(_vm.oAngle + deltaAngle, false);
            }
            else if (_vm.MouseMoveOp == MouseMoveOp.Move)
            {
                Vector diffV = currentPosition - _startPosition;
                _vm.oTranslateTransformX += diffV.X;
                _vm.oTranslateTransformY += diffV.Y;
            }

            _startPosition = currentPosition;

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
            _vm.UpdateCurrentOperation(MouseMoveOp.None, null);
            _vm.SetAngle(_vm.oAngle, true);
            Mouse.Capture(null);
            DrawInfo();
        }

        private void LeftGrip_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(sender as IInputElement))
            {
                _vm.UpdateCurrentOperation(MouseMoveOp.LeftSize, leftGrip);
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
                _vm.UpdateCurrentOperation(MouseMoveOp.RightSize, rightGrip);
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
                _vm.UpdateCurrentOperation(MouseMoveOp.LeftRotate, thumbRotateLeft);
                _startPosition = Mouse.GetPosition(null);
                _vm.UpdateRenderTransformOrigin(new Point(1, 0), _canvasRuler);
                e.Handled = true;
            }
        }

        private void thumbRotateRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(sender as IInputElement))
            {
                _vm.UpdateCurrentOperation(MouseMoveOp.RightRotate, thumbRotateRight);
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
                    _vm.UpdateCurrentOperation(MouseMoveOp.Move, _canvasRuler);
                    _startPosition = Mouse.GetPosition(null);
                    //_vm.UpdateRenderTransformOrigin(new Point(0, 0), _canvasRuler);
                    e.Handled = true;
                }
            }
        }

        private void btnClose_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
