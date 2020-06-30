using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ControlModule.Views
{
    /// <summary>
    /// Spinning Busy Indicator Control.
    /// </summary>
    public partial class CircularProgressBar : UserControl
    {

        /// <summary>
        /// Startup time in miliseconds, default is a second.
        /// </summary>
        public static readonly DependencyProperty StartupDelayProperty =
            DependencyProperty.Register(
                "StartupDelay",
                typeof(int),
                typeof(CircularProgressBar),
                new PropertyMetadata(1000));

        /// <summary>
        /// Spinning Speed. Default is 60, that's one rotation per second.
        /// </summary>
        public static readonly DependencyProperty RotationsPerMinuteProperty =
            DependencyProperty.Register(
                "RotationsPerMinute",
                typeof(double),
                typeof(CircularProgressBar),
                new PropertyMetadata(60.0));

        /// <summary>
        /// Timer for the Animation.
        /// </summary>
        private readonly DispatcherTimer animationTimer;

        /// <summary>
        /// Mouse Cursor.
        /// </summary>
        //private Cursor originalCursor;

        /// <summary>
        /// Initializes a new instance of the CircularProgressBar class.
        /// </summary>
        public CircularProgressBar()
        {
            InitializeComponent();

            this.animationTimer = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher);
        }

        /// <summary>
        /// Gets or sets the startup time in miliseconds, default is a second.
        /// </summary>
        public int StartupDelay
        {
            get
            {
                return (int)this.GetValue(StartupDelayProperty);
            }

            set
            {
                this.SetValue(StartupDelayProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the spinning speed. Default is 60, that's one rotation per second.
        /// </summary>
        public double RotationsPerMinute
        {
            get
            {
                return (double)this.GetValue(RotationsPerMinuteProperty);
            }

            set
            {
                this.SetValue(RotationsPerMinuteProperty, value);
            }
        }

        /// <summary>
        /// Startup Delay.
        /// </summary>
        private void StartWithDelay()
        {
            //this.originalCursor = Mouse.OverrideCursor;
            //Mouse.OverrideCursor = Cursors.Wait;

            // Startup
            this.animationTimer.Interval = new TimeSpan(0, 0, 0, 0, this.StartupDelay);
            this.animationTimer.Tick += this.StartSpinning;
            this.animationTimer.Start();
        }

        /// <summary>
        /// Start Spinning.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event Arguments.</param>
        private void StartSpinning(object sender, EventArgs e)
        {
            this.animationTimer.Stop();
            this.animationTimer.Tick -= this.StartSpinning;

            // 60 secs per minute, 1000 millisecs per sec, 10 rotations per full circle:
            this.animationTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)(6000 / this.RotationsPerMinute));
            this.animationTimer.Tick += this.HandleAnimationTick;
            this.animationTimer.Start();
            this.Opacity = 1;

            //Mouse.OverrideCursor = originalCursor;
        }

        /// <summary>
        /// The control became invisible: stop spinning (animation consumes CPU).
        /// </summary>
        private void StopSpinning()
        {
            this.animationTimer.Stop();
            this.animationTimer.Tick -= this.HandleAnimationTick;
            this.Opacity = 0;
        }

        /// <summary>
        /// Apply a single rotation transformation.
        /// </summary>
        /// <param name="sender">Sender of the Event: the Animation Timer.</param>
        /// <param name="e">Event arguments.</param>
        private void HandleAnimationTick(object sender, EventArgs e)
        {
            this.SpinnerRotate.Angle = (this.SpinnerRotate.Angle + 30) % 360; 
        }

        /// <summary>
        /// Control was unloaded: stop spinning.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            this.StopSpinning();
        }

        /// <summary>
        /// Visibility property was changed: start or stop spinning.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void HandleVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Don't give the developer a headache.
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            bool isVisible = (bool)e.NewValue;

            if (isVisible)
            {
                this.StartWithDelay();
            }
            else
            {
                this.StopSpinning();
            }
        }
    }
}