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

namespace MZ.WPF
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
            DependencyProperty.Register("Maximum", typeof(double), typeof(GradientProgressBar), new PropertyMetadata(180.0));

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

        public GradientProgressBar()
        {
            InitializeComponent();

            DataContext = this;
        }

        #region INotifyPropertyChanged Members


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public class Theme
        {
            public int iBaseTickCount = 10;
            public int iHalfTickCount = 30;
            public int iFullTickCount = 60;

            public static Theme GetBase60Theme()
            {
                return new Theme()
                {
                    iBaseTickCount = 10,
                    iHalfTickCount = 30,
                    iFullTickCount = 60,
                };
            }

            public static Theme GetBase100Theme()
            {
                return new Theme()
                {
                    iBaseTickCount = 10,
                    iHalfTickCount = 50,
                    iFullTickCount = 100,
                };
            }
        }

        public Theme ProgressTheme { get; set; } = Theme.GetBase100Theme();

        private void DrawValue()
        {
            if (_canvas.ActualWidth == 0 || _canvas.ActualHeight == 0)
                return;

            double ratio = (Value - Minimum) / (Maximum - Minimum);
            double left = ratio * _canvas.ActualWidth;
            _rcGray.Margin = new Thickness(left, 0, 0, 0);
        }

        private void DrawTicks()
        {
            DrawValue();

            _canvas.Children.Clear();

            if (_canvas.ActualWidth == 0 || _canvas.ActualHeight == 0)
                return;

            double line_count = Maximum;
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

        private void Progress_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DrawValue();
        }

        private void Progress_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawTicks();
        }

        private void chk_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
