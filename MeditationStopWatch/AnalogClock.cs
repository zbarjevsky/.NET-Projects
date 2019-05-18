using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using MeditationStopWatch;

namespace MeditationStopWatch
{
	/// <summary>
	/// Control name: Analog Clock Control
	/// Description: A customizable and resizable clock control
	/// Developed by: Syed Mehroz Alam
	/// Email: smehrozalam@yahoo.com
	/// URL: Programming Home "http://www.geocities.com/smehrozalam/"
	/// </summary>
	public class AnalogClock : PictureBox
	{
        const double DEGREE_TO_RAD = Math.PI / 180.0;
        const double DEGREE90_TO_RAD = Math.PI / 2;

        DateTime _dateTime = DateTime.Now;

		float _fRadius;
        PointF _Center;
		float _fCenterCircleRadius;

		float _fHourLength;
		float _fMinLength;
		float _fSecLength;

		float _fHourThickness;
		float _fMinThickness;
		float _fSecThickness;
		float _fTicksThickness;

		bool _bDraw5MinuteTicks=true;
		bool _bDraw1MinuteTicks=true;

		Color _hrColor=Color.DarkMagenta ;
		Color _minColor=Color.Green ;
		Color _secColor=Color.Red ;
		Color _circleColor=Color.Red;
        Color _ticksColor = Color.Black;
        Color _ticksBackColor = Color.Transparent;

        private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

        public Color HourHandColor
        {
            get { return this._hrColor; }
            set { this._hrColor = value; }
        }

        public Color MinuteHandColor
        {
            get { return this._minColor; }
            set { this._minColor = value; }
        }

        public Color SecondHandColor
        {
            get { return this._secColor; }
            set
            {
                this._secColor = value;
                this._circleColor = value;
            }
        }

        public Color TicksColor
        {
            get { return this._ticksColor; }
            set { this._ticksColor = value; }
        }

        public Color TicksBackColor
        {
            get { return this._ticksBackColor; }
            set { this._ticksBackColor = value; }
        }

        public bool Draw1MinuteTicks
        {
            get { return this._bDraw1MinuteTicks; }
            set { this._bDraw1MinuteTicks = value; }
        }

        public bool Draw5MinuteTicks
        {
            get { return this._bDraw5MinuteTicks; }
            set { this._bDraw5MinuteTicks = value; }
        }

        public bool SuspendScreenSaver { get; set; } = false;

		public AnalogClock()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// AnalogClock
			// 
			this.Name = "AnalogClock";
			this.Resize += new System.EventHandler(this.AnalogClock_Resize);
			//this.Load += new System.EventHandler(this.AnalogClock_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.AnalogClock_Paint);

		}
		#endregion

		protected override void OnLoadCompleted(AsyncCompletedEventArgs e)
		{
			base.OnLoadCompleted(e);
			_dateTime = DateTime.Now;
			this.AnalogClock_Resize(null, e);
		}

		private void AnalogClock_Load(object sender, System.EventArgs e)
		{
			_dateTime=DateTime.Now;
			this.AnalogClock_Resize(sender,e);
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			this._dateTime=DateTime.Now;
			this.Refresh();

            if(SuspendScreenSaver)
                ScreenSaver.ResetIdleTimer();
        }

		public void Start()
		{
			timer1.Enabled=true;
			this.Refresh();
		}

		public void Stop()
		{
			timer1.Enabled=false;
		}

        public void AdjustClockSize(int delta, Rectangle max)
        {
            const double scaleUp = 1.1;
            const double scaleDown = 0.9;

            delta /= Math.Abs(delta);
            if (delta > 0)
            {
                if (this.Height > scaleDown * max.Height)
                    return;
                if (this.Width > scaleDown * max.Width)
                    return;
            }

            if (this.Width <= this.MinimumSize.Width && delta < 0) //minimum size is 20
                return;

            double scale = delta > 0 ? scaleUp : scaleDown;
            int width = (int)(scale * this.Width);
            if (width > max.Width)
                width = max.Width;

            int height = width;
            if (height > max.Height)
                height = max.Height;

            int deltaX = (this.Width - width) / 2;
            int deltaY = deltaX;

            //restrict on bounds
            if (this.Right + deltaX > max.Width || this.Left + deltaX < 0)
                deltaX = -deltaX;
            if (this.Bottom + deltaY > max.Height || this.Top + deltaY < 0)
                deltaY = -deltaY;

            Point location = this.Location;
            location.Offset(deltaX, deltaY);

            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.Bounds = new Rectangle(location, new Size(width, height));
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }

        private void DrawLine(float fThickness, float fLength, Color color, double fRadians, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawLine(new Pen( color, fThickness ),
				_Center.X - (float)(fLength/9*System.Math.Sin(fRadians)), 
				_Center.Y + (float)(fLength/9*System.Math.Cos(fRadians)), 
				_Center.X + (float)(fLength*System.Math.Sin(fRadians)), 
				_Center.Y - (float)(fLength*System.Math.Cos(fRadians)));
		}

		private void DrawTicsLine(Graphics g, int minute, float f1, float f2)
		{
            double angle = minute * 6 * DEGREE_TO_RAD;
            Point p1 = new Point((int)(_Center.X + this._fRadius / f1 * System.Math.Sin(angle)),
                                 (int)(_Center.Y - this._fRadius / f1 * System.Math.Cos(angle)));

            Point p2 = new Point((int)(_Center.X + this._fRadius / f2 * System.Math.Sin(angle)), 
                                 (int)(_Center.Y - this._fRadius / f2 * System.Math.Cos(angle)));

            g.DrawLine(new Pen(_ticksBackColor, 2*_fTicksThickness), p1.X, p1.Y, p2.X, p2.Y);
            g.DrawLine(new Pen(_ticksColor, _fTicksThickness), p1.X, p1.Y, p2.X, p2.Y);
        }

        private Color InvertColor(Color c)
        {
            const int MAX = 255;
            return Color.FromArgb(MAX - c.R, MAX - c.G, MAX - c.B);
        }

        private void DrawAngledClockHand(float fThickness, float fLength, Color color, double fAngle, System.Windows.Forms.PaintEventArgs e)
		{
			PointF bottom = new PointF( (float)(_Center.X - fThickness * 4 * System.Math.Sin(fAngle)), 
				                 (float)(_Center.Y + fThickness * 4 * System.Math.Cos(fAngle) ));
			PointF bottomRight = new PointF( (float)(_Center.X + fThickness * 2 * System.Math.Sin(fAngle + DEGREE90_TO_RAD)), 
				                 (float)(_Center.Y - fThickness * 2 * System.Math.Cos(fAngle + DEGREE90_TO_RAD)) );
			PointF bottomLeft = new PointF( (float)(_Center.X + fThickness * 2 * System.Math.Sin(fAngle - DEGREE90_TO_RAD)),
				                 (float)(_Center.Y - fThickness * 2 * System.Math.Cos(fAngle - DEGREE90_TO_RAD)) );

            PointF topCenter = new PointF((float)(_Center.X + (fLength - fThickness) * System.Math.Sin(fAngle)),
                                          (float)(_Center.Y - (fLength - fThickness) * System.Math.Cos(fAngle)));

            PointF top = new PointF((float)(topCenter.X + 2 * fThickness * System.Math.Sin(fAngle)),
                                    (float)(topCenter.Y - 2 * fThickness * System.Math.Cos(fAngle)));
            PointF topRight = new PointF((float)(topCenter.X + fThickness * System.Math.Sin(fAngle + DEGREE90_TO_RAD)),
                                         (float)(topCenter.Y - fThickness * System.Math.Cos(fAngle + DEGREE90_TO_RAD)));
            PointF topLeft = new PointF((float)(topCenter.X + fThickness * System.Math.Sin(fAngle - DEGREE90_TO_RAD)),
                                        (float)(topCenter.Y - fThickness * System.Math.Cos(fAngle - DEGREE90_TO_RAD)));

            PointF[] points={bottomRight,bottom,bottomLeft,topLeft, top, topRight};

            e.Graphics.FillPolygon( new SolidBrush(color), points );
		}

		private void AnalogClock_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			double fRadHr=(_dateTime.Hour%12+_dateTime.Minute/60F) * 30 * DEGREE_TO_RAD;
            double fRadMin =(_dateTime.Minute + _dateTime.Second/60F) * 6 * DEGREE_TO_RAD;
            double fRadSec =(_dateTime.Second) * 6 * DEGREE_TO_RAD;

			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

			for(int i=0;i<60;i++)
			{
				if ( this._bDraw5MinuteTicks==true && i%5==0 ) // Draw 5 minute ticks
				{
					DrawTicsLine(e.Graphics, i, 1.00F, 1.25F);
				}
				else if ( this._bDraw1MinuteTicks==true ) // draw 1 minute ticks
				{
					DrawTicsLine(e.Graphics, i, 1.0F, 1.05F);
				}
			}

			DrawAngledClockHand(this._fHourThickness, this._fHourLength, _hrColor, fRadHr, e);
            DrawAngledClockHand(this._fMinThickness, this._fMinLength, _minColor, fRadMin, e);
			DrawLine(this._fSecThickness, this._fSecLength, _secColor, fRadSec, e);

			//draw circle at center
			e.Graphics.FillEllipse( new SolidBrush( _circleColor ), _Center.X-_fCenterCircleRadius/2, _Center.Y-_fCenterCircleRadius/2, _fCenterCircleRadius, _fCenterCircleRadius);
		}

		private void AnalogClock_Resize(object sender, System.EventArgs e)
		{
            //this.Width = this.Height;
            float diameter = Math.Min(Width, Height) - this.Margin.Left;

			this._fRadius = diameter / 2F;
			this._Center.X = this.ClientSize.Width/2F;
			this._Center.Y = this.ClientSize.Height/2F;
			this._fHourLength = _fRadius * 0.65F;
			this._fMinLength = _fRadius * 0.90F;
			this._fSecLength = _fRadius * 0.93F;
            this._fHourThickness = diameter / 100F;
			this._fMinThickness = diameter / 150F;
			this._fSecThickness = diameter / 200F;
			this._fTicksThickness = _fSecThickness;
			this._fCenterCircleRadius = diameter / 50F;

			this.Refresh();
		}	
	}
}
