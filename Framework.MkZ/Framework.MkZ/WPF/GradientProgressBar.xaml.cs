using System;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Timers;
using System.ComponentModel;
using System.Windows.Resources;
using System.IO;

using System.Runtime.CompilerServices;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Diagnostics;

namespace MkZ.WPF
{
    /// <summary>
    /// Common Control - GradientProgressBar
    /// </summary>
    public partial class GradientProgressBar : UserControl, INotifyPropertyChanged
    {
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(GradientProgressBar), new UIPropertyMetadata(0.0, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GradientProgressBar This)
                This.DrawValue();
        }

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); DrawTicks(); OnPropertyChanged(); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(GradientProgressBar), new PropertyMetadata(180.0, OnMaximumChanged));

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GradientProgressBar This)
                This.DrawTicks();
        }

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); DrawTicks(); OnPropertyChanged(); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(GradientProgressBar), new PropertyMetadata(0.0));

        public SolidColorBrush TickColor
        {
            get { return (SolidColorBrush)GetValue(TickColorProperty); }
            set { SetValue(TickColorProperty, value); DrawTicks(); OnPropertyChanged(); }
        }

        public static readonly DependencyProperty TickColorProperty =
            DependencyProperty.Register("TickColor", typeof(SolidColorBrush), typeof(GradientProgressBar), new PropertyMetadata(Brushes.Green));

        public Visibility CheckBoxVisibility
        {
            get { return (Visibility)GetValue(CheckBoxVisibilityProperty); }
            set { SetValue(CheckBoxVisibilityProperty, value); DrawTicks(); OnPropertyChanged(); }
        }

        public static readonly DependencyProperty CheckBoxVisibilityProperty =
            DependencyProperty.Register("CheckBoxVisibility", typeof(Visibility), typeof(GradientProgressBar), new UIPropertyMetadata(Visibility.Collapsed));

        public string CheckBoxToolTip
        {
            get { return (string)GetValue(CheckBoxToolTipProperty); }
            set { SetValue(CheckBoxToolTipProperty, value); }
        }

        public static readonly DependencyProperty CheckBoxToolTipProperty =
            DependencyProperty.Register("CheckBoxToolTip", typeof(string), typeof(GradientProgressBar), new UIPropertyMetadata(""));

        public Action<bool> OnCheckClicked = (isChecked) => { };

        public bool IsChecked { get { return chk.IsChecked.Value; } set { chk.IsChecked = value; OnPropertyChanged(); } }

        public GradientProgressBar()
        {
            InitializeComponent();
            DrawTicks();
        }

        #region INotifyPropertyChanged Members


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public class TicksTheme
        {
            public int iBaseTickCount = 10;
            public int iHalfTickCount = 30;
            public int iFullTickCount = 60;

            public static TicksTheme GetBase60Theme()
            {
                return GetBaseTheme(60);
            }

            public static TicksTheme GetBase100Theme()
            {
                return GetBaseTheme(100);
            }

            public static TicksTheme GetBaseTheme(int max = 100, int min = 10)
            {
                return new TicksTheme()
                {
                    iBaseTickCount = min,
                    iHalfTickCount = max / 2,
                    iFullTickCount = max,
                };
            }
        }

        public TicksTheme ProgressTheme { get; set; } = TicksTheme.GetBase100Theme();

        private void DrawValue()
        {
            if (_canvas.ActualWidth == 0 || _canvas.ActualHeight == 0)
                return;

            double CORNER_RADIUS = _rcColor.CornerRadius.TopLeft;

            double ratio = (Value - Minimum) / (Maximum - Minimum);
            double left = ratio * _canvas.ActualWidth;
            _rcGray.Margin = new Thickness(left, 0, 0, 0);

            if(left <= CORNER_RADIUS) //adjust corner radius to hide black corners on the left side when value is close to 0
                _rcGray.CornerRadius = new CornerRadius(CORNER_RADIUS - left, CORNER_RADIUS, CORNER_RADIUS, CORNER_RADIUS - left);
            else
                _rcGray.CornerRadius = new CornerRadius(0, CORNER_RADIUS, CORNER_RADIUS, 0);
        }

        private void DrawTicks()
        {
            DrawValue();

            _canvas.Children.Clear();

            if (_canvas.ActualWidth == 0 || _canvas.ActualHeight == 0)
                return;

            double line_count = Maximum;
            if (line_count <= 1)
                line_count *= 100;

            double line_distance = _canvas.ActualWidth / line_count;
            double smallDelta = _canvas.ActualHeight / 10;
            double bigDelta = _canvas.ActualHeight / 4;

            for (int i = 0; i <= line_count; i++)
            {
                double thickness = -1;
                double delta = bigDelta;

                if (i % ProgressTheme.iBaseTickCount == 0)
                {
                    thickness = 1;
                    delta = bigDelta;
                }
                if (i % ProgressTheme.iHalfTickCount == 0)
                {
                    thickness = 2;
                    delta = bigDelta;
                }
                if (i % ProgressTheme.iFullTickCount == 0)
                {
                    thickness = 2;
                    delta = smallDelta;
                }

                if (thickness < 0)
                    continue; //skip line

                Line line = new Line();
                line.Stroke = TickColor;
                line.StrokeThickness = thickness;
                line.X1 = i * line_distance;
                line.X2 = line.X1;
                line.Y1 = delta;
                line.Y2 = _canvas.ActualHeight - delta;

                _canvas.Children.Add(line);
            }
        }

        private void Progress_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawTicks();
        }

        private void chk_Clicked(object sender, RoutedEventArgs e)
        {
            CheckBoxToolTip = chk.IsChecked.Value ? "Bell On" : "Bell Off";
            OnCheckClicked(chk.IsChecked.Value);
        }
    }
}
