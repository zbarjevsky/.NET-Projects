using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MkZ.WPF.Controls
{
    /// <summary>
    /// Interaction logic for MagnifyingGlassUserControl.xaml
    /// </summary>
    public partial class MagnifyingGlassUserControl : UserControl
    {
        const double ANIM_THRESHOLD = 20; //minimum distance to show animation

        public Point Center { get; private set; }
        public Visual ObjectToMagnify { get { return MagnifierBrush.Visual; } set { MagnifierBrush.Visual = value; } }
        public Brush MagnifierColor { get { return MagnifierCircle.Stroke; } set { MagnifierCircle.Stroke = value; } }
        public Size MagnifierSize
        {
            get
            {
                return new Size(MagnifierCircle.Width, MagnifierCircle.Height);
            }

            set
            {
                MagnifierCircle.Width = value.Width;
                MagnifierCircle.Height = value.Height;

                MagnifierInnerCircle.Width = value.Width - 4;
                MagnifierInnerCircle.Height = value.Height - 4;
            }
        }

        public MagnifyingGlassUserControl()
        {
            InitializeComponent();
        }

        public double ZoomFactor = 0.3;

        public void SetPosition(Point destination)
        {
            Center = destination;

            double length = MagnifierCircle.ActualWidth * ZoomFactor;
            double radius = length / 2.0;
            MagnifierBrush.Viewbox = new Rect(Center.X - radius, Center.Y - radius, length, length);

            MagnifierCircle.SetValue(Canvas.LeftProperty, Center.X - MagnifierCircle.ActualWidth / 2);
            MagnifierCircle.SetValue(Canvas.TopProperty, Center.Y - MagnifierCircle.ActualHeight / 2);

            MagnifierInnerCircle.SetValue(Canvas.LeftProperty, Center.X - MagnifierInnerCircle.ActualWidth / 2);
            MagnifierInnerCircle.SetValue(Canvas.TopProperty, Center.Y - MagnifierInnerCircle.ActualHeight / 2);
        }

        private void ContentPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isAnimating)
                return;

            SetPosition(e.GetPosition(ContentPanel));
        }

        private void ContentPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            MagnifierCircle.Visibility = Visibility.Visible;
            MagnifierInnerCircle.Visibility = Visibility.Visible;
        }

        private void ContentPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            MagnifierCircle.Visibility = Visibility.Hidden;
            MagnifierInnerCircle.Visibility = Visibility.Hidden;
        }

        private bool _isAnimating = false;
        public void AnimateToPoint(Point start, Point destination, Action postAction = null)
        {
            _isAnimating = true;
            MagnifierCircle.Visibility = Visibility.Visible;
            MagnifierInnerCircle.Visibility = Visibility.Visible;

            Task task = Task.Factory.StartNew(() =>
            {
                Vector delta = destination - start;

                if(delta.Length <= ANIM_THRESHOLD)
                {
                    WPFUtils.ExecuteOnUiThreadInvoke(() => SetPosition(destination));
                }
                else //animate
                {
                    double iterationCount = Math.Ceiling(delta.Length / ANIM_THRESHOLD); // every N points with 10 msec interval

                    double deltaX = delta.X / iterationCount;
                    double deltaY = delta.Y / iterationCount;

                    for (int i = 0; i < iterationCount; i++)
                    {
                        Point pos1 = new Point(start.X + i * deltaX, start.Y + i * deltaY);
                        WPFUtils.ExecuteOnUiThreadInvoke(() => SetPosition(pos1));
                        Thread.Sleep(20);
                    }
                }

                _isAnimating = false;

                if (postAction != null)
                    postAction();
            });
        }

        //public void PositionAnimation(
        //    Point pos1, Point pos2, double delay1 = 2, double duration1 = 1,
        //    bool autoReverse = false, Action postAction = null)
        //{
        //    SetPosition(pos1);
        //    DependencyObject ctrl = MagnifierCircle;

        //    _isAnimating = true;
        //    MagnifierCircle.Visibility = Visibility.Visible;
        //    MagnifierInnerCircle.Visibility = Visibility.Visible;

        //    //start animation
        //    TimeSpan duration = TimeSpan.FromSeconds(duration1); //animate for 1 second
        //    TimeSpan delay = TimeSpan.FromSeconds(delay1);

        //    DoubleAnimation moveXAnimation = new DoubleAnimation();
        //    moveXAnimation.From = pos1.X;
        //    moveXAnimation.To = pos2.X;
        //    moveXAnimation.AutoReverse = autoReverse;
        //    moveXAnimation.BeginTime = delay;
        //    moveXAnimation.Duration = duration;
        //    moveXAnimation.FillBehavior = FillBehavior.Stop;
        //    Storyboard.SetTarget(moveXAnimation, ctrl);
        //    Storyboard.SetTargetProperty(moveXAnimation, new PropertyPath(Canvas.LeftProperty));

        //    DoubleAnimation moveYAnimation = new DoubleAnimation();
        //    moveYAnimation.From = pos1.Y;
        //    moveYAnimation.To = pos2.Y;
        //    moveYAnimation.AutoReverse = autoReverse;
        //    moveYAnimation.BeginTime = delay;
        //    moveYAnimation.Duration = duration;
        //    moveYAnimation.FillBehavior = FillBehavior.Stop;
        //    Storyboard.SetTarget(moveYAnimation, ctrl);
        //    Storyboard.SetTargetProperty(moveYAnimation, new PropertyPath(Canvas.TopProperty));

        //    Storyboard sb = new Storyboard();
        //    sb.Children.Add(moveXAnimation);
        //    sb.Children.Add(moveYAnimation);

        //    sb.Completed += (s, e) => { _isAnimating = false; if (postAction != null) postAction(); };

        //    sb.Begin();
        //}
    }
}
