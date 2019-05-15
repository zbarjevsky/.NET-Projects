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
        const double rightMargin = 100;
        const double leftMargin = 300;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //AddAdorner(_ruler);
            DrawTicks();
        }

        private void CanvasRuler_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!e.OriginalSource.Equals(rightGrip) && !e.OriginalSource.Equals(leftGrip))
                this.DragMove();
        }

        private void DrawTicks()
        {
            _tics.Children.Clear();
            double line_count = _canvasRuler.ActualWidth / 10;
            double line_offset = (_canvasRuler.ActualWidth - 3) / line_count;
            double smallDelta = _canvasRuler.ActualHeight / 4;
            double bigDelta = _canvasRuler.ActualHeight / 3;
            double tickHeight = _canvasRuler.ActualHeight / 2;

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

                double left = Canvas.GetLeft(_canvasRuler);
                if (_isResizingR)
                {
                    if (_canvasRuler.Width + diffX > 100) //min size
                        _canvasRuler.Width += diffX;
                    this.Width = leftMargin + _canvasRuler.Width + rightMargin;
                }
                else
                {
                    if (_canvasRuler.Width - diffX > 100) //min size
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

        private void LeftGrip_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isResizingL)
            {
                _isResizingL = false;
                Mouse.Capture(null);

                double left = Canvas.GetLeft(_canvasRuler);
                Canvas.SetLeft(_canvasRuler, leftMargin);

                this.Width = leftMargin + _canvasRuler.Width + rightMargin;
                this.Left -= leftMargin - left;
            }
        }
    }
}
