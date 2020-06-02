using System;
using System.Globalization;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Design;

namespace DUMeterMZ
{
	//line speed in KBits
	public enum LineSpeed
	{
		Modem_16k		= 16640,
		Modem_56k		= 56000,
		ISDN_128k		= 128000,
		DSL_1MB			= 1024 * 1024,
		LAN_10MB		= 10 * DSL_1MB,
		LAN_50MB	    = 50 * DSL_1MB,
		LAN_100MB		= 100 * DSL_1MB,
		LAN_256MB		= 256 * DSL_1MB,
		LAN_1GB			= 1024 * DSL_1MB,
	}//end enum LineSpeed

	public enum SpeedUnits
	{
		[Description("KBits.")]
		KBits = 8,
		[Description("KBytes.")]
		KBytes = 1,
	}//end SpeedUnits

	[DefaultProperty("Interface")] //will show this as selected value
	public class Options
	{
		public Options()
		{
			UploadScaling = 1;
		}//end Constructor

		private LineSpeed speed = LineSpeed.DSL_1MB;

		[Category("1. Interface Options")]
		[DisplayName("Line Speed")]
		public LineSpeed LineSpeed
		{
			get { return speed; }
			set { speed = value; }
		}//end LineSpeed

		[Category("1. Interface Options")]
		[DisplayName("Upload Scaling")]
		[DefaultValue(1)]
		[Editor(typeof(NumericUpDown), typeof(UITypeEditor))]
		public int UploadScaling
		{
			get;
			set;
		}

		private SpeedUnits speed_units = SpeedUnits.KBits;

		[Category("1. Interface Options")]
		[Description("Please select speed units")]
		[DisplayName("Speed Units")]
		public SpeedUnits SpeedUnits
		{
			get { return speed_units; }
			set { speed_units = value; }
		}//end SpeedUnits

		private Color downcolor = Color.Green;
		private Color upcolor = Color.Red;
		private Color bothcolor = Color.Yellow;
		private Color textcolor = Color.Black;
		private Color bc = SystemColors.Info;
		private Color lines = Color.DimGray;
		private Color selcolor = Color.Orange;

		[Category("5. Color Options")]
		[Description("Color of grid lines")]
		public Color Lines
		{
			get { return lines; }
			set { lines = value; }
		}

		[Category("5. Color Options")]
		[Description("Color of selection in reports")]
		public Color Selection
		{
			get { return selcolor; }
			set { selcolor = value; }
		}

		[Category("5. Color Options")]
		[Description("Background color of graph and reports")]
		public Color Background
		{
			get { return bc; }
			set { bc = value; }
		}

		[Category("5. Color Options")]
		[Description("Color of text and Grid")]
		public Color Text
		{
			get { return textcolor; }
			set { textcolor = value; }
		}

		[Category("5. Color Options")]
		[Description("Color of download graph lines")]
		public Color Down
		{
			get { return downcolor; }
			set { downcolor = value; }
		}

		[Category("5. Color Options")]
		[Description("Color of upload graph lines")]
		public Color Up
		{
			get { return upcolor; }
			set { upcolor = value; }
		}

		[Category("5. Color Options")]
		[Description("Color of overlapping up/down graph lines")]
		public Color Both
		{
			get { return bothcolor; }
			set { bothcolor = value; }
		}

		private string instancename = "";

		[Category("1. Interface Options")]
		[DisplayName("Interface")]
		[Description("Please select an adapter to connect")]
		[TypeConverter(typeof(Options.InstanceConverter))]
		public string Interface
		{
			get { return instancename; }
			set { instancename = value; }
		}

		class InstanceConverter : TypeConverter
		{
			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				ArrayList vals = new ArrayList();
				string[] instnames = new PerformanceCounterCategory("Network Interface", sname).GetInstanceNames();
				foreach (string i in instnames)
					vals.Add(i);
				return new StandardValuesCollection(vals);
			}//end GetStandardValues

			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}//end GetStandardValuesSupported
		}//end class InstanceConverter

		internal static string sname = ".";

		private string pcname = ".";

		[Category("1. Interface Options")]
		[Description("Select the PC you would like to monitor")]
		[DisplayName("Machine Name")]
		[TypeConverter(typeof(Options.MachineConverter))]
		public string MachineName
		{
			get { return pcname; }
			set
			{
				pcname = value;
				sname = value;
			}//end set
		}//end MachineName

		private class MachineConverter : TypeConverter
		{
			[StructLayout(LayoutKind.Sequential)]
			internal class ServerInfo
			{
				public int platformid = 0;
				[MarshalAs(UnmanagedType.LPWStr)]
				public string name = ".";
				public int majorver = 0;
				public int minorver = 0;
				public int type = 0;
				[MarshalAs(UnmanagedType.LPWStr)]
				public string comment = "";
			}//end class ServerInfo

			[DllImport("Netapi32")]
			private static extern int NetServerEnum(
			   string servername, //must be null
			   int level, //101
			   out IntPtr buffer,
			   int maxlen, //out
			   out int entriesread, //out
			   out int totalentries, //out
			   int servertype, //in 3 = all
			   string domain, //null primary
			   int resumehandle); //must be 0

			[DllImport("Netapi32")]
			private static extern int NetApiBufferFree(IntPtr ptr);

			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				int eread = 0, total = 0;
				IntPtr buffer;
				int l = 200;
				int size = Marshal.SizeOf(typeof(ServerInfo));
				int res = NetServerEnum(
					null,
					101, //Return server names, types, and associated software
					out buffer,
					size * l,
					out eread,
					out total,
					3,
					null,
					0);

				System.Diagnostics.Trace.WriteLine("Total Computers: " + total);

				StringCollection arr = new StringCollection();
				arr.Add("."); //default value
				try
				{
					IntPtr p = buffer;
					for (int i = 0; i < eread; i++)
					{
						ServerInfo si = Marshal.PtrToStructure(p, typeof(ServerInfo)) as ServerInfo;
						if (si.majorver > 4)
							arr.Add(si.name);
						System.Diagnostics.Trace.WriteLine("Computer: " + si.name);
						p = (IntPtr)((int)p + size);
					}//end for
				}//end try
				catch (Exception) { }

				res = NetApiBufferFree(buffer);
				return new StandardValuesCollection(arr);
			}//end GetStandardValues

			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}//end GetStandardValuesSupported
		}//end class MachineConverter

		private double opac = 0.7F;

		[Category("3. Visual Options")]
		[TypeConverter(typeof(System.Windows.Forms.OpacityConverter))]
		public double Transparency
		{
			get { return opac; }
			set { opac = value; }
		}

		private double overflow = 0.2F;

		[Category("3. Visual Options")]
		[TypeConverter(typeof(System.Windows.Forms.OpacityConverter))]
		[Description("Show m_bmpGraph overflow line speed")]
		public double Overflow
		{
			get { return overflow; }
			set { overflow = value; }
		}

		private Point m_Location = new Point(0, 0);

		[Browsable(false)]
		[Description("Initial location")]
		public Point Location
		{
			get { return m_Location; }
			set { m_Location = value; }
		}

		private Size m_Size = new Size(180, 50);

		[Browsable(false)]
		public Size Size
		{
			get { return m_Size; }
			set { m_Size = value; }
		}//end Size

		private bool loadwin = false;
		private bool checkreg = false;
        const string REG_KEY = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run";

		[Category("4. Windows Options")]
		[Description("Load application when windows starts")]
		[DisplayName("Load with Windows")]
		public bool LoadWithWindows
		{
			get
			{
				if (!checkreg)
				{
                    if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REG_KEY).
						GetValue("DUMeterMZ") != null)
						loadwin = true;
					checkreg = true;
				}

				return loadwin;
			}
			set
			{
				if (value)
					Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REG_KEY, true).
					   SetValue("DUMeterMZ", "\"" + Application.ExecutablePath + "\"");
				else
					Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REG_KEY, true).
					   DeleteValue("DUMeterMZ", false);
				loadwin = value;
			}
		}

		private BorderStyle graphBorderStyle = BorderStyle.None;

		[Category("3. Visual Options")]
		[DisplayName("Graph BorderStyle")]
		[Description("Graph BorderStyle - border style to display for graph")]
		public BorderStyle BorderStyle
		{
			get { return graphBorderStyle; }
			set { graphBorderStyle = value; }
		}

		private bool alwaysshowtext = true;

		[Category("3. Visual Options")]
		[DisplayName("Always Show Text")]
		[Description("If false - show text only when hover the application window")]
		public bool AlwaysShowText
		{
			get { return alwaysshowtext; }
			set { alwaysshowtext = value; }
		}

		private bool m_bShowWindowCaption = false;

		[Category("3. Visual Options")]
		[DisplayName("Show Window Caption")]
		[Description("Show Window Caption with border")]
		public bool ShowWindowCaption
		{
			get { return m_bShowWindowCaption; }
			set { m_bShowWindowCaption = value; }
		}//end ShowWindowCaption

		public bool m_bShowWindowBorder = true;

		[Browsable(false)]
		public bool ShowWindowBorder
		{
			get { return m_bShowWindowBorder; }
			set { m_bShowWindowBorder = value; }
		}//end ShowWindowBorder

		private bool m_bShowLineSpeedLabel = false;

		[Category("3. Visual Options")]
		[DisplayName("Show Line Speed on left")]
		[Description("Show Line Speed Label on left side of graph")]
		public bool ShowLineSpeedLabel
		{
			get { return m_bShowLineSpeedLabel; }
			set { m_bShowLineSpeedLabel = value; }
		}

		private string ping = "www.clearforest.com";

		[Category("6. Ping Options")]
		[DisplayName("Ping Server")]
		public string PingEndPoint
		{
			get { return ping; }
			set { ping = value; }
		}

		private int pinginterval = 500;

		[Description("Period in millisenconds")]
		[Category("6. Ping Options")]
		[DisplayName("Ping Interval")]
		public int PingInterval
		{
			get { return pinginterval; }
			set { pinginterval = value; }
		}

		private bool hidewhenidle = false;

		[Category("3. Visual Options")]
		[DisplayName("Hide when Idle")]
		public bool HideWhenIdle
		{
			get { return hidewhenidle; }
			set { hidewhenidle = value; }
		}

		private int loginterval = 60;

		/// <summary>
		/// Log interval in seconds
		/// </summary>
		[ReadOnly(true),
		 Description("Sampling interval in seconds"),
		 Category("2. Log Options"),
		 DisplayName("Sampling Interval")]
		public int LogInterval
		{
			get { return loginterval; }
			set { loginterval = value; }
		}

		[Category("2. Log Options")]
		[DisplayName("EMail for IP")]
		[Description("EMail address to send IP information")]
		public string EMailForIP { get; set; }

		[Category("2. Log Options")]
		[DisplayName("GMAIL from")]
		[Description("use this gmail account to send email")]
		public string EMailFrom { get; set; }

		[Category("2. Log Options")]
		[DisplayName("GMAIL password")]
		[Description("use this gmail account to send email")]
		[PasswordPropertyText(true)]
		public string EMailFromPwd { get; set; }
	}
}
