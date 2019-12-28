using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DashCamGPSView.Tools
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

        public static void AnimateColumn(ColumnDefinition col, GridLength from, GridLength to, Action postAction = null)
        {
            var da = new GridLengthAnimation();

            da.Duration = TimeSpan.FromMilliseconds(300);

            da.From = from;
            da.To = to;

            //var ef = new BounceEase();
            //ef.EasingMode = EasingMode.EaseOut;
            //da.EasingFunction = ef;

            da.FillBehavior = FillBehavior.Stop;
            col.Width = da.To;

            da.Completed += (s, e) => { if (postAction != null) postAction(); };

            col.BeginAnimation(ColumnDefinition.WidthProperty, da);
        }

        public static void AnimateRow(RowDefinition row, GridLength to, Action postAction = null)
        {
            var animation = new GridLengthAnimation();

            animation.Duration = TimeSpan.FromMilliseconds(500);

            animation.From = row.Height;
            animation.To = to;

            //var ef = new BounceEase();
            //ef.EasingMode = EasingMode.EaseOut;
            //da.EasingFunction = ef;

            animation.FillBehavior = FillBehavior.Stop;
            //row.Height = animation.To;

            animation.Completed += (s, e) => 
            {
                if (postAction != null)
                    postAction();
                else
                    row.Height = to;
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
