using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace WinFFAvi
{
	public class ProgramPreferences
	{
		public const string DEFAULT_SIZE = "1280x720";

		public ProgramPreferences()
		{
			LoadDefaultValues();
		}

		public void LoadDefaultValues()
		{
			foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
			{
				DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
				if (attr == null) continue;
				prop.SetValue(this, attr.Value);
			}
		}

		[Category("Program Preferences")]
		[Description("Set location of FFMpeg.exe file")]
		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DisplayName("Path to Convertor(FFMpeg.exe)")]
		[DefaultValue("C:\\Program Files\\WinFF\\ffmpeg.exe")]
		public string WinFF_Executable
		{
			get;
			set;
		}

		[Category("Program Preferences")]
		[Description("Maximum Recent Out Folders Number")]
		[DisplayName("Maximum Folders")]
		[DefaultValue(40)]
		public int MaximumFolders
		{
			get;
			set;
		}

		[Category("Program Preferences")]
		[Description("Default output size")]
		[DisplayName("Output Size")]
		[DefaultValue(DEFAULT_SIZE)]
		public string OutputSize
		{
			get;
			set;
		}

	}//end class ProgramPreferences

}//end class ProgramPreferences
