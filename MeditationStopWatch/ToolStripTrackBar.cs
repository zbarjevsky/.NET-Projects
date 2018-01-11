using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace MeditationStopWatch
{
	[System.ComponentModel.DesignerCategory("code")]
	[ToolStripItemDesignerAvailability(
		ToolStripItemDesignerAvailability.ToolStrip |
		ToolStripItemDesignerAvailability.StatusStrip)]
	internal partial class ToolStripTrackBar : ToolStripControlHost
	{
		public ToolStripTrackBar() :
			base(CreateControlInstance()) { }

		private static Control CreateControlInstance()
		{
			TrackBar t = new TrackBar();
			t.AutoSize = false;
			t.Height = 25;
			t.TickStyle = TickStyle.BottomRight;
			t.TickFrequency = 100;
			t.Minimum = 0;
			t.Maximum = 1000;
			t.Value = 0;
			return t;
		}

		public TrackBar TrackBar
		{
			get { return Control as TrackBar; }
		}

		[DefaultValue(0)]
		[Category("1. TrackBar")]
		public int Value
		{
			get { return TrackBar.Value; }
			set { TrackBar.Value = value; }
		}

		[DefaultValue(0)]
		[Category("1. TrackBar")]
		public int Minimum
		{
			get { return TrackBar.Minimum; }
			set { TrackBar.Minimum = value; }
		}

		[DefaultValue(100)]
		[Category("1. TrackBar")]
		public int Maximum
		{
			get { return TrackBar.Maximum; }
			set { TrackBar.Maximum = value; }
		}

		[DefaultValue(1)]
		[Category("1. TrackBar")]
		public int SmallChange
		{
			get { return TrackBar.SmallChange; }
			set { TrackBar.SmallChange = value; }
		}

		[DefaultValue(10)]
		[Category("1. TrackBar")]
		public int LargeChange
		{
			get { return TrackBar.LargeChange; }
			set { TrackBar.LargeChange = value; }
		}

		protected override void OnSubscribeControlEvents(Control control)
		{
			base.OnSubscribeControlEvents(control);
			((TrackBar)control).ValueChanged += trackBar_ValueChanged;
		}

		protected override void OnUnsubscribeControlEvents(Control control)
		{
			base.OnUnsubscribeControlEvents(control);
			((TrackBar)control).ValueChanged -= trackBar_ValueChanged;
		}

		protected void trackBar_ValueChanged(object sender, EventArgs e)
		{
			if (ValueChanged != null) ValueChanged(sender, e);
		}

		public event EventHandler ValueChanged;

		protected override Size DefaultSize
		{
			get { return new Size(300, 16); }
		}
	}

}
