using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MkZ.WPF
{
    public class GridLengthAnimation : AnimationTimeline
    {
        public static readonly DependencyProperty FromProperty;
        public static readonly DependencyProperty ToProperty;
        public static readonly DependencyProperty EasingFunctionProperty;

        static GridLengthAnimation()
        {
            FromProperty = DependencyProperty.Register("From", typeof(GridLength), typeof(GridLengthAnimation));
            ToProperty = DependencyProperty.Register("To", typeof(GridLength), typeof(GridLengthAnimation));
            EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(GridLengthAnimation));
        }

        public static void AnimateColumn(ColumnDefinition col, GridLength to, Action postAction = null)
        {
            var da = new GridLengthAnimation();

            da.Duration = TimeSpan.FromMilliseconds(300);

            da.From = col.Width;
            da.To = to;

            //var ef = new BounceEase();
            //ef.EasingMode = EasingMode.EaseOut;
            //da.EasingFunction = ef;

            da.FillBehavior = FillBehavior.Stop;
            //col.Width = da.To;

            da.Completed += (s, e) => 
            {
                if (postAction != null)
                    postAction();
                else
                    col.Width = to;
            };

            col.BeginAnimation(ColumnDefinition.WidthProperty, da);
        }

        public static void AnimateRow(RowDefinition row, GridLength to, double duration = 500, Action postAction = null)
        {
            GridLength from = new GridLength(row.ActualHeight, GridUnitType.Pixel);
            AnimateRow(row, from, to, duration, postAction);
        }

        public static void AnimateRow(RowDefinition row, GridLength from, GridLength to, double duration = 500, Action postAction = null)
        {
            var animation = new GridLengthAnimation();

            animation.Duration = TimeSpan.FromMilliseconds(duration);

            animation.From = from;
            animation.To = to;

            //https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/easing-functions
            var ef = new CubicEase();
            ef.EasingMode = EasingMode.EaseOut;
            animation.EasingFunction = ef;

            animation.FillBehavior = FillBehavior.Stop;
            //row.Height = animation.To;

            animation.Completed += (s, e) => 
            {
                row.Height = to;
                if (postAction != null)
                    postAction();
            };

            row.BeginAnimation(RowDefinition.HeightProperty, animation);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation();
        }

        public override Type TargetPropertyType
        {
            get { return typeof(GridLength); }
        }

        public IEasingFunction EasingFunction
        {
            get
            {
                return (IEasingFunction)GetValue(GridLengthAnimation.EasingFunctionProperty);
            }
            set
            {
                SetValue(GridLengthAnimation.EasingFunctionProperty, value);
            }

        }

        public GridLength From
        {
            get
            {
                return (GridLength)GetValue(GridLengthAnimation.FromProperty);
            }
            set
            {
                SetValue(GridLengthAnimation.FromProperty, value);
            }
        }

        public GridLength To
        {
            get
            {
                return (GridLength)GetValue(GridLengthAnimation.ToProperty);
            }
            set
            {
                SetValue(GridLengthAnimation.ToProperty, value);
            }
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            double fromValue = ((GridLength)GetValue(GridLengthAnimation.FromProperty)).Value;
            double toValue = ((GridLength)GetValue(GridLengthAnimation.ToProperty)).Value;

            IEasingFunction easingFunction = this.EasingFunction;

            double progress = (easingFunction != null) ? easingFunction.Ease(animationClock.CurrentProgress.Value) : animationClock.CurrentProgress.Value;

            if (fromValue > toValue)
            {
                return new GridLength((1 - progress) * (fromValue - toValue) + toValue, this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            }
            else
            {
                return new GridLength((progress) * (toValue - fromValue) + fromValue, this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            }
        }
    }
}
