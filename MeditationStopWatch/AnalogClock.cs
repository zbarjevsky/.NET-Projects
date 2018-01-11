using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AnalogClockControl
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
		const float PI=3.141592654F;

		DateTime dateTime;

		float fRadius;
		float fCenterX;
		float fCenterY;
		float fCenterCircleRadius;

		float fHourLength;
		float fMinLength;
		float fSecLength;

		float fHourThickness;
		float fMinThickness;
		float fSecThickness;
		float fTicksThickness;

		bool bDraw5MinuteTicks=true;
		bool bDraw1MinuteTicks=true;

		Color hrColor=Color.DarkMagenta ;
		Color minColor=Color.Green ;
		Color secColor=Color.Red ;
		Color circleColor=Color.Red;
		Color ticksColor=Color.Black;

		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

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
			dateTime = DateTime.Now;
			this.AnalogClock_Resize(null, e);
		}

		private void AnalogClock_Load(object sender, System.EventArgs e)
		{
			dateTime=DateTime.Now;
			this.AnalogClock_Resize(sender,e);
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			this.dateTime=DateTime.Now;
			this.Refresh();
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

		private void DrawLine(float fThickness, float fLength, Color color, float fRadians, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawLine(new Pen( color, fThickness ),
				fCenterX - (float)(fLength/9*System.Math.Sin(fRadians)), 
				fCenterY + (float)(fLength/9*System.Math.Cos(fRadians)), 
				fCenterX + (float)(fLength*System.Math.Sin(fRadians)), 
				fCenterY - (float)(fLength*System.Math.Cos(fRadians)));
		}

		private void DrawTicsLine(Graphics g, int minute, float f1, float f2)
		{
			g.DrawLine(new Pen(ticksColor, fTicksThickness),
				fCenterX + (float)(this.fRadius / f1 * System.Math.Sin(minute * 6 * PI / 180)),
				fCenterY - (float)(this.fRadius / f1 * System.Math.Cos(minute * 6 * PI / 180)),
				fCenterX + (float)(this.fRadius / f2 * System.Math.Sin(minute * 6 * PI / 180)),
				fCenterY - (float)(this.fRadius / f2 * System.Math.Cos(minute * 6 * PI / 180)));
		}

		private void DrawPolygon(float fThickness, float fLength, Color color, float fRadians, System.Windows.Forms.PaintEventArgs e)
		{

			PointF A=new PointF( (float)(fCenterX+ fThickness*2*System.Math.Sin(fRadians+PI/2)), 
				(float)(fCenterY - fThickness*2*System.Math.Cos(fRadians+PI/2)) );
			PointF B=new PointF( (float)(fCenterX+ fThickness*2*System.Math.Sin(fRadians-PI/2)),
				(float)(fCenterY - fThickness*2*System.Math.Cos(fRadians-PI/2)) );
			PointF C=new PointF( (float)(fCenterX+ fLength*System.Math.Sin(fRadians)), 
				(float) (fCenterY - fLength*System.Math.Cos(fRadians)) );
			PointF D=new PointF( (float)(fCenterX- fThickness*4*System.Math.Sin(fRadians)), 
				(float)(fCenterY + fThickness*4*System.Math.Cos(fRadians) ));
			PointF[] points={A,D,B,C};
			e.Graphics.FillPolygon( new SolidBrush(color), points );
		}

		private void AnalogClock_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			float fRadHr=(dateTime.Hour%12+dateTime.Minute/60F) *30*PI/180;
			float fRadMin=(dateTime.Minute)*6*PI/180;
			float fRadSec=(dateTime.Second)*6*PI/180;

			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

			for(int i=0;i<60;i++)
			{
				if ( this.bDraw5MinuteTicks==true && i%5==0 ) // Draw 5 minute ticks
				{
					DrawTicsLine(e.Graphics, i, 1.00F, 1.25F);
				}
				else if ( this.bDraw1MinuteTicks==true ) // draw 1 minute ticks
				{
					DrawTicsLine(e.Graphics, i, 1.0F, 1.05F);
				}
			}

			DrawPolygon(this.fHourThickness, this.fHourLength, hrColor, fRadHr, e);
			DrawPolygon(this.fMinThickness, this.fMinLength, minColor, fRadMin, e);
			DrawLine(this.fSecThickness, this.fSecLength, secColor, fRadSec, e);

			//draw circle at center
			e.Graphics.FillEllipse( new SolidBrush( circleColor ), fCenterX-fCenterCircleRadius/2, fCenterY-fCenterCircleRadius/2, fCenterCircleRadius, fCenterCircleRadius);
		}

		private void AnalogClock_Resize(object sender, System.EventArgs e)
		{
            //this.Width = this.Height;
            float diameter = Math.Min(Width, Height) - this.Margin.Left;

			this.fRadius = diameter / 2;
			this.fCenterX = this.ClientSize.Width/2;
			this.fCenterY = this.ClientSize.Height/2;
			this.fHourLength = (float)diameter / 2/1.65F;
			this.fMinLength = (float)diameter / 2/1.20F;
			this.fSecLength = (float)diameter / 2/1.05F;
			this.fHourThickness = (float)diameter / 100;
			this.fMinThickness = (float)diameter / 150;
			this.fSecThickness = (float)diameter / 200;
			this.fTicksThickness = fSecThickness;
			this.fCenterCircleRadius = diameter / 50;

			this.Refresh();
		}
	
		public Color HourHandColor
		{
			get { return this.hrColor; }
			set { this.hrColor=value; }
		}

		public Color MinuteHandColor
		{
			get { return this.minColor; }
			set { this.minColor=value; }
		}

		public Color SecondHandColor
		{
			get { return this.secColor; }
			set { this.secColor=value;
				  this.circleColor=value; }
		}

		public Color TicksColor
		{
			get { return this.ticksColor; }
			set { this.ticksColor=value; }
		}

		public bool Draw1MinuteTicks
		{
			get { return this.bDraw1MinuteTicks; }
			set { this.bDraw1MinuteTicks=value; }
		}

		public bool Draw5MinuteTicks
		{
			get { return this.bDraw5MinuteTicks; }
			set { this.bDraw5MinuteTicks=value; }
		}

	}
}
