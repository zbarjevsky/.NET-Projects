using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MkZ.WPF
{
    public class FadeAnimationHelper
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly Control _container = null;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private readonly List<UIElement> _controlsToFade = new List<UIElement>();
        private readonly double _hideTimeOutSeconds = 2;

        public Action<UIElement> OnHideCompleted = (element) => { };
        //do not hide - can be usefull for media files with no video
        public Func<bool> CanHideControls = () => true;

        public FadeAnimationHelper(Control container, double hideTimeOutSeconds, params UIElement[] controls)
        {
            _container = container;
            _container.MouseMove += _container_MouseMove;
            _container.PreviewMouseDown += _container_PreviewMouseDown;

            _hideTimeOutSeconds = hideTimeOutSeconds;
            if (_hideTimeOutSeconds < 1)
                _hideTimeOutSeconds = 1;

            _controlsToFade.AddRange(controls);
            foreach (UIElement ctrl in _controlsToFade)
            {
                ctrl.MouseEnter += Ctrl_MouseEnter;
                ctrl.MouseLeave += Ctrl_MouseLeave;
            }

            _timer.Interval = TimeSpan.FromSeconds(0.233);
            _timer.Tick += timer_Tick;
            _timer.Start();

            _stopwatch.Restart();
        }

        public void AnimateFadeIn()
        {
            _stopwatch.Restart();
            if (!_timer.IsEnabled)
                _timer.Start();

            foreach (UIElement ctrl in _controlsToFade)
            {
                if (ctrl.Visibility != Visibility.Visible)
                {
                    VisibilityShowAnimation(ctrl, 0, null);
                }
            }
        }

        private bool _isInsideControl = false;
        private void Ctrl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _isInsideControl = true;
        }

        private void Ctrl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _isInsideControl = false;
        }

        private void _container_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AnimateFadeIn();
        }

        private void _container_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AnimateFadeIn();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_isInsideControl || !CanHideControls() || WPFUtils.GetInDesignMode())
                return;

            _timer.Stop();
            if(_stopwatch.ElapsedMilliseconds > 1000 * _hideTimeOutSeconds)
            {
                foreach (UIElement ctrl in _controlsToFade)
                {
                    VisibilityHideAnimation(ctrl, 0, Visibility.Hidden, OnHideCompleted);
                }

                return; //do not start timer
            }
            _timer.Start();
        }

        public static void FadeInOutAnimation(UIElement element, double showSeconds = 3)
        {
            VisibilityShowAnimation(element, 0, (e) =>
            {
                VisibilityHideAnimation(e, 3, Visibility.Hidden, null);
            });
        }

        public static void VisibilityShowAnimation(UIElement element, double delaySeconds, Action<UIElement> OnShowCompleted)
        {
            var animation = new DoubleAnimation
            {
                To = 1,
                BeginTime = TimeSpan.FromSeconds(delaySeconds),
                Duration = TimeSpan.FromSeconds(0.4),
                FillBehavior = FillBehavior.Stop
            };

            element.Opacity = 0;
            element.Visibility = Visibility.Visible;

            animation.Completed += (s, a) =>
            {
                element.Opacity = 1.0;
                element.Visibility = Visibility.Visible;

                OnShowCompleted?.Invoke(element);
            };

            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        public static void VisibilityHideAnimation(UIElement element, double delaySeconds, Visibility finalVisibility, Action<UIElement> OnHideCompleted)
        {
            var animation = new DoubleAnimation
            {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(delaySeconds),
                Duration = TimeSpan.FromSeconds(0.7),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) =>
            {
                element.Opacity = 0;
                element.Visibility = finalVisibility;

                OnHideCompleted?.Invoke(element);
            };

            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }

    public class GridLengthAnimationHelper
    {
        private readonly Control _container = null;
        private readonly RowDefinition _row;

        public GridLength InitialRowHeight { get; }

        public double InitialControlHeight { get; set; }

        public bool CanHide { get; set; } = false;

        public Action<Control> PostAnimationAction = (ctrl) => { };

        public GridLengthAnimationHelper(Control container, RowDefinition row)
        {
            _container = container;
            InitialControlHeight = _container.Height;
            _container.MouseMove += _container_MouseMove;
            _container.MouseLeave += _container_MouseLeave;

            _row = row;
            InitialRowHeight = _row.Height;
        }

        public void ShowRow(bool bShow, bool canHide)
        {
            CanHide = canHide;
            InitialControlHeight = _container.Height;
            Action postAction = () => PostAnimationAction(_container);

            if (bShow)
            {
                if (_row.Height.Value == 0) //show
                {
                    //_row.Height = InitialRowHeight;
                    GridLengthAnimation.AnimateRow(_row, InitialRowHeight, 500, postAction);
                }
            }
            else
            {
                if (_row.Height.Value == InitialRowHeight.Value) //hide
                {
                    //_row.Height = new GridLength(0);
                    GridLengthAnimation.AnimateRow(_row, new GridLength(0), 500, postAction);
                }
            }
        }

        private void _container_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = e.GetPosition(_row);

            if(_row.Height.Value == InitialRowHeight.Value) //fully visible
            {
                if (!CanHide)
                    return;

                if(pt.Y > InitialRowHeight.Value * 2.0)
                    if (e.OriginalSource is Grid || e.OriginalSource is MediaElement)
                        ShowRow(false, CanHide);
            }
            else if(_row.Height.Value == 0) //not visible - show
            {
                if (pt.Y < InitialRowHeight.Value / 2.0)
                    ShowRow(true, CanHide);
            }
        }

        private void _container_MouseLeave(object sender, MouseEventArgs e)
        {
            Point pt = e.GetPosition(_row);
            Debug.WriteLine("AnimationHelper::MouseLeave Pos: {0}, Source: {1}", pt, e.OriginalSource);
            if (CanHide && _row.Height.Value == InitialRowHeight.Value)
                ShowRow(false, CanHide);
        }
    }
}
