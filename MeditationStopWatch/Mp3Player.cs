using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace MeditationStopWatch
{
	public class Mp3Player
	{
		private string _command;
		private bool _isOpen;

		private const string MP3 = "Mp3File";

		private const string Media = "MediaFile";
		private const string VIDEO = "mpegvideo";
		private const string AUDIO = "waveaudio";

		private string _FileType = MP3;

		[DllImport("winmm.dll")]
		private static extern int mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
		
		[DllImport("winmm.dll")]
		private static extern bool mciGetErrorString(int fdwError, StringBuilder lpszErrorText, long cchErrorText);

		public Mp3Player()
		{

		}

		public void Close()
		{
			_command = "close " + _FileType;
			int res = mciSendString(_command, null, 0, IntPtr.Zero);
			LogResult(res, _command);
			_isOpen = false;
		}

		public void Open(string sFileName)
		{
			_command = "open \"" + sFileName + "\" type " + VIDEO + " alias " + _FileType;
			int res = mciSendString(_command, null, 0, IntPtr.Zero);
			LogResult(res, _command);
			_isOpen = true;
		}

		public void Play(bool loop)
		{
			if (_isOpen)
			{
				_command = "play " + _FileType;
				if (loop)
					_command += " REPEAT";
				int res = mciSendString(_command, null, 0, IntPtr.Zero);
				LogResult(res, _command);
			}
		}

		public void Pause()
		{
			_command = "pause " + _FileType; ;
			int res = mciSendString(_command, null, 0, IntPtr.Zero);
			LogResult(res, _command);
		}

		public void Resume()
		{
			_command = "resume " + _FileType; ;
			int res = mciSendString(_command, null, 0, IntPtr.Zero);
			LogResult(res, _command);
		}

		public void Stop()
		{
			_command = "stop " + _FileType;
			int res = mciSendString(_command, null, 0, IntPtr.Zero);
			LogResult(res, _command);
		}

		public int Position()
		{
			if (_isOpen)
			{
				const int ms = 1000;
				StringBuilder buffer = new StringBuilder(129);
				_command = "Status " + _FileType + " position";
				int res = mciSendString(_command, buffer, 128, IntPtr.Zero);
				LogResult(res, _command);
				int seconds = GetIntFromString(buffer, ms);
				//System.Diagnostics.Trace.WriteLine("Pos = " + seconds);
				return seconds / ms;
			}
			return 0;
		}

		public int Length()
		{
			if (_isOpen)
			{
				const int ms = 1000;
				StringBuilder buffer = new StringBuilder(129);
				_command = "Status " + _FileType + " length";
				int res = mciSendString(_command, buffer, 128, IntPtr.Zero);
				LogResult(res, _command);
				int seconds = GetIntFromString(buffer, ms);
				System.Diagnostics.Trace.WriteLine("Len = " + seconds);
				return seconds / ms;
			}
			return 0;
		}

		public int Volume
		{
			get
			{
				StringBuilder buffer = new StringBuilder(16);
				_command = "Status " + _FileType + " volume";
				int res = mciSendString(_command, buffer, 128, IntPtr.Zero);
				LogResult(res, _command);
				int volume = GetIntFromString(buffer, 75);
				return volume;
			}

			set
			{
				_command = "setaudio " + _FileType + " Volume to " + value;
				int res = mciSendString(_command, null, 0, IntPtr.Zero);
				LogResult(res, _command);
			}
		}

		private int GetIntFromString(StringBuilder buffer, int iDefault)
		{
			string s = buffer.ToString();
			if (!string.IsNullOrEmpty(s))
			{
				try
				{
					return int.Parse(s);
				}
				catch
				{
					return iDefault;
				}
			}
			return iDefault;
		}

		private string GetErrorString(int error)
		{
			StringBuilder buffer = new StringBuilder(512);
			bool returnValue = mciGetErrorString(error, buffer, buffer.Capacity);
			string err = buffer.ToString();
			return err;
		}

		private void LogResult(int res, string sCommand)
		{
			if (res != 0)
			{
				System.Diagnostics.Trace.WriteLine(string.Format("res = {0}, command = {1}", res, sCommand));
				if ( MCIErrorStrings.MSIError.ContainsKey(res) )
					System.Diagnostics.Trace.WriteLine(string.Format("res = {0}, result = {1}", res, MCIErrorStrings.MSIError[res]));
				System.Diagnostics.Trace.WriteLine(GetErrorString(res));
			}
		}
	}

	internal class MCIErrorStrings
	{
		public static System.Collections.Generic.Dictionary<int, string> MSIError =
			new Dictionary<int, string>()
			{
				{256, "MCIERR_BASE"},
				{257, "MCIERR_INVALID_DEVICE_ID"},			
				{259, "MCIERR_UNRECOGNIZED_KEYWORD"},		
				{261, "MCIERR_UNRECOGNIZED_COMMAND"},		
				{262, "MCIERR_HARDWARE"},					
				{263, "MCIERR_INVALID_DEVICE_NAME"},		
				{264, "MCIERR_OUT_OF_MEMORY"},				
				{265, "MCIERR_DEVICE_OPEN"},				
				{266, "MCIERR_CANNOT_LOAD_DRIVER"},		
				{267, "MCIERR_MISSING_COMMAND_STRING"},	
				{268, "MCIERR_PARAM_OVERFLOW"},			
				{269, "MCIERR_MISSING_STRING_ARGUMENT"},	
				{270, "MCIERR_BAD_INTEGER"},				
				{271, "MCIERR_PARSER_INTERNAL"},			
				{272, "MCIERR_DRIVER_INTERNAL"},			
				{273, "MCIERR_MISSING_PARAMETER"},			
				{274, "MCIERR_UNSUPPORTED_FUNCTION"},		
				{275, "MCIERR_FILE_NOT_FOUND"},			
				{276, "MCIERR_DEVICE_NOT_READY"},			
				{277, "MCIERR_INTERNAL"},					
				{278, "MCIERR_DRIVER"},					
				{279, "MCIERR_CANNOT_USE_ALL"},			
				{280, "MCIERR_MULTIPLE"},					
				{281, "MCIERR_EXTENSION_NOT_FOUND"},		
				{282, "MCIERR_OUTOFRANGE"},				
				{283, "MCIERR_FLAGS_NOT_COMPATIBLE"},		
				{286, "MCIERR_FILE_NOT_SAVED"},			
				{287, "MCIERR_DEVICE_TYPE_REQUIRED"},		
				{288, "MCIERR_DEVICE_LOCKED"},				
				{289, "MCIERR_DUPLICATE_ALIAS"},			
				{290, "MCIERR_BAD_CONSTANT"},				
				{291, "MCIERR_MUST_USE_SHAREABLE"},		
				{292, "MCIERR_MISSING_DEVICE_NAME"},		
				{293, "MCIERR_BAD_TIME_FORMAT"},			
				{294, "MCIERR_NO_CLOSING_QUOTE"},			
				{295, "MCIERR_DUPLICATE_FLAGS"},			
				{296, "MCIERR_INVALID_FILE"},				
				{297, "MCIERR_NULL_PARAMETER_BLOCK"},		
				{298, "MCIERR_UNNAMED_RESOURCE"},			
				{299, "MCIERR_NEW_REQUIRES_ALIAS"},		
				{300, "MCIERR_NOTIFY_ON_AUTO_OPEN"},		
				{301, "MCIERR_NO_ELEMENT_ALLOWED"},		
				{302, "MCIERR_NONAPPLICABLE_FUNCTION"},	
				{303, "MCIERR_ILLEGAL_FOR_AUTO_OPEN"},		
				{304, "MCIERR_FILENAME_REQUIRED"},			
				{305, "MCIERR_EXTRA_CHARACTERS"},			
				{306, "MCIERR_DEVICE_NOT_INSTALLED"},		
				{307, "MCIERR_GET_CD"},					
				{308, "MCIERR_SET_CD"},					
				{309, "MCIERR_SET_DRIVE"},					
				{310, "MCIERR_DEVICE_LENGTH"},				
				{311, "MCIERR_DEVICE_ORD_LENGTH"},			
				{312, "MCIERR_NO_INTEGER"},				
				{320, "MCIERR_WAVE_OUTPUTSINUSE"},			
				{321, "MCIERR_WAVE_SETOUTPUTINUSE"},		
				{322, "MCIERR_WAVE_INPUTSINUSE"},			
				{323, "MCIERR_WAVE_SETINPUTINUSE"},		
				{324, "MCIERR_WAVE_OUTPUTUNSPECIFIED"},	
				{325, "MCIERR_WAVE_INPUTUNSPECIFIED"},		
				{326, "MCIERR_WAVE_OUTPUTSUNSUITABLE"},	
				{327, "MCIERR_WAVE_SETOUTPUTUNSUITABLE"},	
				{328, "MCIERR_WAVE_INPUTSUNSUITABLE"},		
				{329, "MCIERR_WAVE_SETINPUTUNSUITABLE"},	
				{336, "MCIERR_SEQ_DIV_INCOMPATIBLE"},		
				{337, "MCIERR_SEQ_PORT_INUSE"},			
				{338, "MCIERR_SEQ_PORT_NONEXISTENT"},		
				{339, "MCIERR_SEQ_PORT_MAPNODEVICE"},		
				{340, "MCIERR_SEQ_PORT_MISCERROR"},		
				{341, "MCIERR_SEQ_TIMER"},					
				{342, "MCIERR_SEQ_PORTUNSPECIFIED"},		
				{343, "MCIERR_SEQ_NOMIDIPRESENT"},			
				{346, "MCIERR_NO_WINDOW"},					
				{347, "MCIERR_CREATEWINDOW"},				
				{348, "MCIERR_FILE_READ"},					
				{349, "MCIERR_FILE_WRITE"},				
				{512, "MCIERR_CUSTOM_DRIVER_BASE"}		
			};

	}
}
