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

            Canvas.SetTop(_canvasRuler, Properties.Settings.Default.Location.Y);
            Canvas.SetLeft(_canvasRuler, Properties.Settings.Default.Location.X);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DrawTicks();

            Rect bounds = new Rect(
                new Point(SystemParameters.VirtualScreenLeft, SystemParameters.VirtualScreenTop),
                new Size(SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenHeight));

            //full virtual screen - 2 displays
            this.Left = bounds.Left;
            this.Top = bounds.Top;
            this.Width = bounds.Width;
            this.Height = bounds.Height;

            _vm.PropertyChanged += (o, propName) => { DrawInfo(); };
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void UnitsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mnu = sender as MenuItem;
            int units = int.Parse(mnu.Tag.ToString());
            _vm.MeasurementUnits = (MeasurementUnits)units;
            DrawTicks();
        }

        private void AngleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _vm.UpdateRenderTransformOrigin(new Point(), _canvasRuler);

            MenuItem mnu = sender as MenuItem;
            int angle = int.Parse(mnu.Tag.ToString());
            _vm.oAngle = angle;
        }

        private void DrawTicks()
        {
            _tics.Children.Clear();

            RulerTicsData r = new RulerTicsData(_vm.MeasurementUnits, _canvasRuler);
            for (int i = 1; i < r.tick_count; i++)
            {
                double tickSize = r.tick0Height;
                if (i % 2 == 0)
                {
                    tickSize = r.tick1Height;
                }
                if (i % r.tickHalfCount == 0)
                {
                    tickSize = r.tickHalfHeight;
                }
                if (i % r.tickTextCount == 0)
                {
                    tickSize = r.tickTextHeight;
                }

                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 0.8;
                line.X1 = 1 + i * r.tick_width;
                line.X2 = line.X1;
                line.Y1 = 0;
                line.Y2 = tickSize;

                _tics.Children.Add(line);

                if (i % r.tickTextCount == 0) //add text
                {
                    TextBlock txt = new TextBlock();
                    txt.FontSize = 16;
                    txt.TextAlignment = TextAlignment.Center;
                    txt.Width = 60;
                    txt.Text = (i / r.tick_text_scale).ToString();
                    Canvas.SetLeft(txt, line.X1 - 30);
                    Canvas.SetTop(txt, line.Y2);
                    _tics.Children.Add(txt);
                }
            }
        }

        private void DrawInfo()
        {
            var src = PresentationSource.FromVisual(_canvasRuler);
            if (src == null)
                return;

            string units = _vm.MeasurementUnits.GetDescription();
            RulerTicsData r = new RulerTicsData(_vm.MeasurementUnits, _canvasRuler);

            Point loc = _canvasRuler.PointToScreen(new Point());
            _txtBounds.Text = string.Format("X: {0:0}, Y: {1:0}, Length: {2:0.0} {3}, Angle: {4:0.0}°",
                loc.X, loc.Y, r.WidthInSelectedUnits(_vm.oWidth),  units, _vm.oAngle);
            Properties.Settings.Default.Location = new System.Drawing.Point((int)loc.X, (int)loc.Y);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            DrawInfo();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            double delta = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) ? 25 : 1;
            bool changeSize = (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift));

            if(changeSize) // width or angle
            {
                if (e.Key == Key.Right)
                    _vm.oWidth += delta;
                if (e.Key == Key.Left)
                    _vm.oWidth -= delta;
                if (e.Key == Key.Up)
                    _vm.oAngle -= delta;
                if (e.Key == Key.Down)
                    _vm.oAngle += delta;

                DrawTicks();
            }
            else //move
            {
                if (e.Key == Key.Right)
                    _vm.oTranslateTransformX += delta;
                if (e.Key == Key.Left)
                    _vm.oTranslateTransformX -= delta;
                if (e.Key == Key.Up)
                    _vm.oTranslateTransformY -= delta;
                if (e.Key == Key.Down)
                    _vm.oTranslateTransformY += delta;
            }

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
            if (distance > 200) //too far - disengage
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
                }

                DrawTicks();
            }
            else if (_vm.MouseMoveOp == MouseMoveOp.LeftRotate || _vm.MouseMoveOp == MouseMoveOp.RightRotate)
            {
                //Point currentPosition = Mouse.GetPosition(null);

                Vector startToCenter = (_vm.Origin(_canvasRuler) - _startPosition);
                Vector currToCenter = (_vm.Origin(_canvasRuler) - currentPosition);
                double deltaAngle = Vector.AngleBetween(startToCenter, currToCenter);

                _vm.oAngle += deltaAngle;
            }
            else if (_vm.MouseMoveOp == MouseMoveOp.Move)
            {
                Vector diffV = currentPosition - _startPosition;
                _vm.oTranslateTransformX += diffV.X;
                _vm.oTranslateTransformY += diffV.Y;
            }

            _startPosition = currentPosition;
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
            if(_vm.MouseMoveOp == MouseMoveOp.LeftRotate || _vm.MouseMoveOp == MouseMoveOp.RightRotate)
                _vm.oAngle = RulerVM.SnapToGrid(_vm.oAngle);

            _vm.UpdateCurrentOperation(MouseMoveOp.None, null);
            Mouse.Capture(null);
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
            if(e.ClickCount == 2)
            {
                switch (_vm.oAngle)
                {
                    case 0:
                        _vm.oAngle = 90;
                        break;
                    case 90:
                    default:
                        _vm.oAngle = 0;
                        break;
                }
            }
            else if (e.Source == _canvasRuler || e.Source is TextBlock)
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

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _canvasRuler.Opacity = 0.1;
            AboutWindow wnd = new AboutWindow();
            wnd.CenterTo(_canvasRuler);
            wnd.ShowDialog();
            _canvasRuler.Opacity = 1.0;
        }
    }
}
