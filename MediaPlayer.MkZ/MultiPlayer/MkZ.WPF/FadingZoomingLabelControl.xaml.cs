﻿using System;
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
using System.Windows.Threading;

using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Size = System.Windows.Size;
using UserControl = System.Windows.Controls.UserControl;

namespace MkZ.WPF.Controls
{
    /// <summary>
    /// Interaction logic for FadingTextControl.xaml
    /// </summary>
    public partial class FadingAutoZoomingLabelControl : UserControl
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private int _counter = 0;
        private bool _bAllowShow = false, _bAllowHide = false, bMouseOver = false;

        private FrameworkElement _parent;
        private Size _parentSize;
        private Size _initialSize;
        private Thickness _initialMargin;

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(FadingAutoZoomingLabelControl), 
                new PropertyMetadata("", OnTextUpdate, OnCoerceValue));

        private static void OnTextUpdate(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        //get notified before set value - use it even if same value
        private static object OnCoerceValue(DependencyObject d, object baseValue)
        {
            if (d is FadingAutoZoomingLabelControl This)
                This.TextBlock_TextUpdated(d, null);

            return baseValue;
        }

        public FadingAutoZoomingLabelControl()
        {
            DataContext = this;

            InitializeComponent();
            
            _timer.Interval = TimeSpan.FromSeconds(0.333);
            _timer.Tick += timer_Tick;

            if(!WPFUtils.GetInDesignMode())
                _timer.Start();

            _bAllowShow = true;

            this.Draggable(true, new Thickness(4));
        }

        private void _control_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Parent is FrameworkElement parent)
            {
                _parent = parent;
                _parentSize = new Size(_parent.ActualWidth, _parent.ActualHeight);
                _initialSize = new Size(this.ActualWidth, this.ActualHeight);
                _initialMargin = this.Margin;

                Resize(_parentSize);

                _parent.SizeChanged += Parent_SizeChanged;
            }
        }

        private void _control_Unloaded(object sender, RoutedEventArgs e)
        {
            if(_parent != null)
                _parent.SizeChanged -= Parent_SizeChanged;
            _parent = null;
        }

        //Automatically zoom and margin if parent size changes
        private void Parent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size newSize = e.NewSize;
            Size zoom = new Size(e.NewSize.Width / _parentSize.Width, e.NewSize.Height / _parentSize.Height);

            //this.Width = 0.1 * _parentSize.Width; // zoom.Width * _initialSize.Width;
            //this.Height = 0.05 * newSize.Height; // zoom.Height * _initialSize.Height;
            //if (this.MinHeight > this.Height)
            //    this.Height = this.MinHeight;
            Resize(newSize);

            //this.Margin = new Thickness(
            //    _initialMargin.Left * zoom.Width,
            //    _initialMargin.Top * zoom.Height,
            //    _initialMargin.Right * zoom.Width,
            //    _initialMargin.Bottom * zoom.Height);
        }

        private void Resize(Size newSize)
        {
            this.Height = 0.05 * newSize.Height; // zoom.Height * _initialSize.Height;
            if (this.MinHeight > this.Height)
                this.Height = this.MinHeight;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int timeOut = 5;
            if (bMouseOver)
                timeOut = 50;

            _timer.Stop();
            _counter++;
            if(_bAllowHide && _counter > timeOut)
            {
                _bAllowHide = false;
                FadeAnimationHelper.VisibilityHideAnimation(this, 0, Visibility.Hidden, (e1) => { _bAllowShow = true; });
            }
            _timer.Start();
        }

        private void _control_MouseEnter(object sender, MouseEventArgs e)
        {
            bMouseOver = true;
            _counter = 0;
        }

        private void _control_MouseMove(object sender, MouseEventArgs e)
        {
            _counter = 0;
        }

        private void _control_MouseLeave(object sender, MouseEventArgs e)
        {
            bMouseOver = false;
            _counter = 0;
        }

        private void TextBlock_TextUpdated(object sender, DataTransferEventArgs e)
        {
            _counter = 0;
            if (_bAllowShow)
            {
                _bAllowShow = false;
                FadeAnimationHelper.VisibilityShowAnimation(this, 0, (e1) => { _bAllowHide = true; });
            }
        }
    }
}
