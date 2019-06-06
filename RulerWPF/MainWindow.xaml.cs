using System;
using System.Diagnostics;
using System.Globalization;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
            Tools.GraphicsHelper.UpdateSystemDpi();

            DrawTicks();

            Rect bounds = ChangeWindowSize();

            System.Drawing.Point location = Properties.Settings.Default.Location;
            if (location.X < 0 || location.X > bounds.Right)
                location.X = 300;
            if (location.Y < 0 || location.Y > bounds.Bottom)
                location.Y = 300;

            Canvas.SetLeft(_canvasRuler, location.X);
            Canvas.SetTop(_canvasRuler, location.Y);

            _vm.PropertyChanged += (o, propName) => { DrawInfo(); };

            SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void SystemParameters_StaticPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Debug.WriteLine("SystemParameters_StaticPropertyChanged" + e.PropertyName);
            if(e.PropertyName == "VirtualScreenHeight" || e.PropertyName == "VirtualScreenWidth")
            {
                ChangeWindowSize(); 
            }
            Tools.GraphicsHelper.UpdateSystemDpi();
        }

        private Rect ChangeWindowSize()
        {
            Rect bounds = new Rect(
                new Point(SystemParameters.VirtualScreenLeft, SystemParameters.VirtualScreenTop),
                new Size(SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenHeight));

            //full virtual screen - 2 displays
            if (this.Left != bounds.Left)
                this.Left = bounds.Left;
            if (this.Top != bounds.Top)
                this.Top = bounds.Top;
            if (this.Width != bounds.Width)
                this.Width = bounds.Width;
            if (this.Height != bounds.Height)
                this.Height = bounds.Height;

            return bounds;
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
                double tickHeght = r.tick0Height;
                if (i % 2 == 0)
                {
                    tickHeght = r.tick1Height;
                }
                if (i % (int)r.tickHalfCount == 0)
                {
                    tickHeght = r.tickHalfHeight;
                }
                if (i % (int)r.ticksPerTextLabelCount == 0)
                {
                    tickHeght = r.tickTextHeight;
                }

                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 0.8;
                line.X1 = (1 + i * r.tick_width) / r.tick_to_device_scale;
                line.X2 = line.X1;
                line.Y1 = 0;
                line.Y2 = tickHeght;

                _tics.Children.Add(line);

                if (i % (int)r.ticksPerTextLabelCount == 0) //add text
                {
                    TextBlock txt = new TextBlock();
                    txt.FontSize = 16;
                    txt.TextAlignment = TextAlignment.Center;
                    txt.Width = 60;
                    txt.Text = (i / r.tick_text_scale).ToString(CultureInfo.InvariantCulture);
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
            double scale = Utils.ScaleFromGraphics();
            double width = _vm.oWidth * scale;

            double wpfScale = src.CompositionTarget.TransformToDevice.M11;

            _txtBounds.Text = string.Format("X: {0:0}, Y: {1:0}, Length: {2:0.0} {3}, Angle: {4:0.0}°, Scale: {5:0.00}, WPF Scale: {6:0.00}",
                loc.X, loc.Y, r.WidthInSelectedUnits(width),  units, _vm.oAngle, scale, wpfScale);

            Properties.Settings.Default.Location = new System.Drawing.Point((int)loc.X, (int)loc.Y);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            DrawInfo();
            Dispatcher.BeginInvoke((Action)(() => ChangeWindowSize()));
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            double delta = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) ? 25 : 1;
            bool changeSize = (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift));

            double scale = Utils.ScaleFromGraphics();
            delta /= scale;

            if (changeSize) // width or angle
            {
                _vm.UpdateRenderTransformOrigin(new Point(), _canvasRuler);
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

        Point _mouseDownPosition;
        private void window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if(_vm.MouseMoveOp == MouseMoveOp.None)
                return;

            Point currentPosition = Utils.GetMousePosition();
            Vector movedBy = currentPosition - _mouseDownPosition;
            if ((int)movedBy.Length == 0) //no change
                return;

            double distance = _vm.Distance(currentPosition, _vm.CurrentElement);
            //Debug.WriteLine("Distance: " + distance);
            if (distance > 200) //too far - disengage
            {
                OnPreviewMouseLeftButtonUp(null, null);
                SystemSounds.Asterisk.Play();
                return;
            }

            Vector diff = CalculateDiffByDirection(currentPosition);

            const double MIN_WIDTH = 100;
            if (_vm.MouseMoveOp == MouseMoveOp.LeftSize || _vm.MouseMoveOp == MouseMoveOp.RightSize)
            {
                if (_vm.oWidth + diff.X > MIN_WIDTH) //min size
                {
                    _vm.oWidth += diff.X;
                   if (_vm.MouseMoveOp == MouseMoveOp.LeftSize)
                    {
                        _vm.oTranslateTransformX -= diff.X;
                    }
                }

                DrawTicks();
            }
            else if (_vm.MouseMoveOp == MouseMoveOp.LeftRotate || _vm.MouseMoveOp == MouseMoveOp.RightRotate)
            {
                Point originOnScreen = Utils.ScaleToWPF_DPI(_vm.OriginOnScreen(_canvasRuler));
                Vector startToCenter = (originOnScreen -_mouseDownPosition);
                startToCenter.Normalize();
                Vector currToCenter = (originOnScreen - currentPosition);
                currToCenter.Normalize();
                double deltaAngle = Vector.AngleBetween(startToCenter, currToCenter);

                Debug.WriteLine(string.Format("delta angle: {0:0.00}, moved by: {1:0.00}", deltaAngle, movedBy.Length));

                _vm.oAngle += deltaAngle;
            }
            else if (_vm.MouseMoveOp == MouseMoveOp.Move)
            {
                _vm.oTranslateTransformX += movedBy.X;
                _vm.oTranslateTransformY += movedBy.Y;
            }

            _mouseDownPosition = currentPosition;
        }

        private Vector CalculateDiffByDirection(Point currentPosition)
        {
            Point originOnScreen = _vm.OriginOnScreen(_canvasRuler);
            Vector startToCenter = (originOnScreen - _mouseDownPosition);
            Vector currToCenter = (originOnScreen - currentPosition);

            Vector diffVector = (currToCenter - startToCenter);
            double angle = Vector.AngleBetween(diffVector, currToCenter);
            //Debug.WriteLine("delta angle1: " + angle);

            double angleRad = Math.PI * angle / 180.0;
            double diffX = diffVector.Length * Math.Cos(angleRad);
            double diffY = diffVector.Length * Math.Sin(angleRad);
            return new Vector(diffX, diffY);
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
                _mouseDownPosition = Utils.GetMousePosition();
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
                _mouseDownPosition = Utils.GetMousePosition();
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
                _mouseDownPosition = Utils.GetMousePosition();
                _vm.UpdateRenderTransformOrigin(new Point(1, 0), _canvasRuler);
                e.Handled = true;
            }
        }

        private void thumbRotateRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(sender as IInputElement))
            {
                _vm.UpdateCurrentOperation(MouseMoveOp.RightRotate, thumbRotateRight);
                _mouseDownPosition = Utils.GetMousePosition();
                _vm.UpdateRenderTransformOrigin(new Point(0, 0), _canvasRuler);
                e.Handled = true;
            }
        }

        private void _canvasRuler_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                switch ((int)_vm.oAngle)
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
                    _mouseDownPosition = Utils.GetMousePosition();
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
