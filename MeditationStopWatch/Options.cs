using System;
using System.Globalization;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MeditationStopWatch
{
	[DefaultProperty("Interface")] //will show this property as selected value
	public class Options
	{
		public Options()
		{
			LoadDefaultValues();
		}//end Constructor

		public void LoadDefaultValues()
		{
			foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
			{
                var att = prop.Attributes[typeof(DefaultValueAttribute)];
                DefaultValueAttribute attr = att as DefaultValueAttribute;
				if (attr == null) continue;
				prop.SetValue(this, attr.Value);
			}
		}

		[Category("1. Play List")]
		[DisplayName("Play List")]
		[Description("Last Play List")]
		[DefaultValue(new string[] { })]
		public string[] PlayList { get; set; }

		[Category("1. Play List")]
		[DisplayName("Favorites List")]
		[Description("Favorites List")]
		[DefaultValue(new string[] { })]
		public string[] FavoritesList { get; set; }

		[Category("2. Misc")]
		[DisplayName("Last Image")]
		[Description("Image")]
		public string LastImageFile { get; set; }

		[Category("2. Misc")]
		[Description("Start up sound Voulume")]
		[DefaultValue(300)]
		public int Volume { get; set; }

		[Category("2. Misc")]
		[Description("Slide Show TimeOut in seconds")]
		[DefaultValue(7)]
		public int SlideShowTimeOut { get; set; }

		[Category("2. Misc")]
		[Description("Show file name over the image")]
		[DefaultValue(false)]
		public bool ShowFileName { get; set; }

		[Category("2. Misc")]
		[Description("Loop through all songs")]
		[DefaultValue(true)]
		public bool Loop { get; set; }

		[Browsable(false)]
		public bool BellAtTheEnd { get; set; }

		[Browsable(false)]
		[DefaultValue(180.0)]
		public double ProgressInterval { get; set; }

		#region Clock Options
		
		[Category("3. Clock Color Options")]
		[DisplayName("Clock Hour Hand Color")]
		[Description("Clock Hour Hand Color")]
		[DefaultValue(typeof(Color), "DarkGoldenrod")]
		public Color HourHandColor
		{
			get;
			set;
		}

		[Category("3. Clock Color Options")]
		[DisplayName("Clock Minute Hand Color")]
		[Description("Clock Minute Hand Color")]
		[DefaultValue(typeof(Color), "Goldenrod")]
		public Color MinuteHandColor
		{
			get;
			set;
		}

		[Category("3. Clock Color Options")]
		[DisplayName("Clock Second Hand Color")]
		[Description("Clock Second Hand Color")]
		[DefaultValue(typeof(Color), "Red")]
		public Color SecondHandColor
		{
			get;
			set;
		}

		[Category("3. Clock Color Options")]
		[DisplayName("Clock Ticks Color")]
		[Description("Clock Ticks Color")]
		[DefaultValue(typeof(Color), "Sienna")]
		public Color TicksColor
		{
			get;
			set;
		}

		[Category("3. Clock Color Options")]
		[DisplayName("Clock Clock Background")]
		[Description("Clock Clock Background")]
		[DefaultValue(typeof(Color), "Control")]
		public Color ClockBackground
		{
			get;
			set;
		}

		[Category("3. Clock Color Options")]
		[DisplayName("Digital Clock Text Color")]
		[Description("Digital Clock Text Color")]
		[DefaultValue(typeof(Color), "Red")]
		public Color DigitalClockTextColor
		{
			get;
			set;
		}

		[Category("3. Clock Color Options")]
		[DisplayName("Digital Clock Back Color")]
		[Description("Digital Clock Back Color")]
		[DefaultValue(typeof(Color), "DimGray")]
		public Color DigitalClockBackColor
		{
			get;
			set;
		}

        [Category("3. Clock Color Options")]
        [Description("Digital Clock Font")]
        [DefaultValue(typeof(Font), "Control.DefaultFont")]
        public Font DigitalClockFont
        {
            get;
            set;
        }

		#endregion

		[Category("4. Position")]
		[DisplayName("Picture Size")]
		[Description("Picture Size - Splitter Position")]
		[DefaultValue(450)]
		public int PictureWidth { get; set; }

		[Category("4. Position")]
		[DisplayName("Clock Height")]
		[Description("Clock Height - Splitter Position")]
		[DefaultValue(250)]
		public int ClockHeight { get; set; }

		[Category("4. Position")]
		[DisplayName("Window State")]
		[Description("Window State")]
		[DefaultValue(FormWindowState.Normal)]
		public FormWindowState WindowState { get; set; }

        [Category("4. Position")]
        [DisplayName("Window Rectangle")]
        [Description("Window Rectangle")]
        [DefaultValue(typeof(Rectangle), "100,100,800,600")]
        public Rectangle AppRectangle { get; set; }

        [Category("5. Color Options")]
        [Description("Options Description Background color")]
        [DefaultValue(typeof(Color), "Info")]
        public Color Background
        {
            get;
            set;
        }
    }
}
