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

namespace MkZ.MediaPlayer.Controls
{
    public class AnimationHelper
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly Control _container = null;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private readonly List<UIElement> _controls = new List<UIElement>();
        private readonly double _hideTimeOutSeconds = 2;

        public Action<UIElement> OnHideCompleted = (element) => { };

        public AnimationHelper(Control container, double hideTimeOutSeconds, params UIElement[] controls)
        {
            _container = container;
            _container.MouseMove += _container_MouseMove;

            _hideTimeOutSeconds = hideTimeOutSeconds;
            if (_hideTimeOutSeconds < 1)
                _hideTimeOutSeconds = 1;

            _controls.AddRange(controls);
            foreach (UIElement ctrl in _controls)
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

            foreach (UIElement ctrl in _controls)
            {
                if (ctrl.Visibility != Visibility.Visible)
                {
                    VisibilityShowAnimation(ctrl);
                }
            }

            _container.Cursor = Cursors.Arrow;
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

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_isInsideControl)
                return;

            _timer.Stop();
            if(_stopwatch.ElapsedMilliseconds > 1000 * _hideTimeOutSeconds)
            {
                foreach (UIElement ctrl in _controls)
                {
                    VisibilityHideAnimation(ctrl, Visibility.Hidden);
                }

                _container.Cursor = Cursors.None;

                return; //do not start timer
            }
            _timer.Start();
        }

        private void VisibilityShowAnimation(UIElement element)
        {
            var animation = new DoubleAnimation
            {
                To = 1,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(0.4),
                FillBehavior = FillBehavior.Stop
            };

            element.Opacity = 0;
            element.Visibility = Visibility.Visible;

            animation.Completed += (s, a) =>
            {
                element.Opacity = 1.0;
                element.Visibility = Visibility.Visible;
            };

            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        private void VisibilityHideAnimation(UIElement element, Visibility finalVisibility = Visibility.Hidden)
        {
            var animation = new DoubleAnimation
            {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(0.7),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) =>
            {
                element.Opacity = 0;
                element.Visibility = finalVisibility;
                OnHideCompleted(element);
            };

            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }
}
