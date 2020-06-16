using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace MeditationStopWatch
{
	public class MCIPLayer
	{
        public enum PlayingMode
        {
            none,
            playing,
            open,
            paused,
            stopped,
            error
        }

        //Random randomNumber = new Random();
        private StringBuilder m_sbErrorMessage;  // MCI Error message
        //private StringBuilder m_sbReturnData;  // MCI return m_Data
        //private string _command;  // String that holds the MCI command
        private string _fileNameAllias = string.Empty;
        public string FileNameAllias { get { return _fileNameAllias; } }
        //private IList<FileInfo> playlist;  // ListView as a playlist with the song path
		//public int NowPlaying { get; set; }
		public bool Paused { get; set; }
		//public bool Loop { get; set; }
		//public bool Shuffle { get; set; }

        [DllImport("winmm.dll")]
        private static extern int mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        [DllImport("winmm.dll")]
        public static extern int mciGetErrorString(int errCode, StringBuilder errMsg, int buflen);
        //[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetShortPathName(
        //         [MarshalAs(UnmanagedType.LPTStr)]
        //           string path,
        //         [MarshalAs(UnmanagedType.LPTStr)]
        //           StringBuilder shortPath,
        //         int shortPathLength
        //         );

        public string FileName {  get { return _fileNameAllias; } }

		private bool ExecuteCommandPut(string cmd, params object [] args)
		{
            if (string.IsNullOrWhiteSpace(_fileNameAllias))
                return true;

			string _command = string.Format(cmd, args);
			int error = mciSendString(_command, null, 0, IntPtr.Zero);
			if (error == 0)
				return true;

			LogResult(error, _command);
			return false;
		}

		private static string ExecuteCommandGet(string cmdFmt, string fileNameAllias)
		{
			if (string.IsNullOrWhiteSpace(fileNameAllias))
				return string.Empty;

			string _command = string.Format(cmdFmt, fileNameAllias);
            StringBuilder m_sbReturnData = new StringBuilder(128);
            int error = mciSendString(_command, m_sbReturnData, m_sbReturnData.Capacity, IntPtr.Zero);
			if (error == 0)
			{
				return m_sbReturnData.ToString();
			}

			LogResult(error, _command);
			return string.Empty;
		}

        // When creating a new player object you have to pass to it a ListView object
        // that will hold all the information about the songs in the playlist
		public MCIPLayer(/*IList<FileInfo> pl*/)
        {
            //playlist = pl;
            //NowPlaying = 0;
            //Loop = false;
            //Shuffle = true;
            Paused = false;
            m_sbErrorMessage = new StringBuilder(128);
            //m_sbReturnData = new StringBuilder(128);
        }

        #region Operations

        public void CmdClose()
        {
			ExecuteCommandPut("close {0}", _fileNameAllias);
			_fileNameAllias = string.Empty;
        }

        public bool Open(string fullPath, string fileNameAlias)
        {
            CmdClose();

            // Try to open as mpegvideo 
			_fileNameAllias = ReplaceSpaces(fileNameAlias);
            bool result = ExecuteCommandPut("open \"{0}\" type mpegvideo alias {1}", fullPath, _fileNameAllias);
            if (result)
                return true;

            // Let MCI deside which file type the song is
            result = ExecuteCommandPut("open \"{0}\" alias {1}", fullPath, _fileNameAllias);
            return result;
        }


        public bool Play(string sFileName, string fileNameAlias)
        {
            if (!Open(sFileName, fileNameAlias))
                return false;

            if (CmdPlay())
                return true;

            CmdClose();
            return false;
        }

        public bool CmdPlay()
        {
            return ExecuteCommandPut("play {0}", _fileNameAllias);
        }

        public bool CmdStop()
        {
            return ExecuteCommandPut("stop {0}", _fileNameAllias);
        }

        public bool CmdPause()
        {
            return ExecuteCommandPut("pause {0}", _fileNameAllias);
        }

        public bool CmdResume()
        {
            return ExecuteCommandPut("resume {0}", _fileNameAllias);
        }

        #endregion

        #region Status

        public PlayingMode GetStatusMode() { return GetStatusMode(_fileNameAllias); }

        public static PlayingMode GetStatusMode(string fileNameAllias)
        {
            string sMode = ExecuteCommandGet("status {0} mode", fileNameAllias);
            if (string.IsNullOrWhiteSpace(sMode))
                return PlayingMode.none;

            PlayingMode mode = (PlayingMode)Enum.Parse(typeof(PlayingMode), sMode);
            return mode;
        }

        //public static bool IsPlaying(string fileNameAllias, string sMode)
        //{
        //    if(string.IsNullOrWhiteSpace(sMode))
        //        sMode = GetStatusMode(fileNameAllias);
        //    return (sMode.Length == 7 && sMode.Substring(0, 7) == "playing");
        //}
        #endregion

        #region Logic

        public int GetCurentMilisecond()
        {
			string sPosition = ExecuteCommandGet("status {0} position", _fileNameAllias);
			if (string.IsNullOrWhiteSpace(sPosition))
				return 0;

			return int.Parse(sPosition);
        }

        public bool SetPosition(int miliseconds)
        {
            if (GetStatusMode() == PlayingMode.playing)
				return ExecuteCommandPut("play {0} from {1}", _fileNameAllias, miliseconds);
            else
				return ExecuteCommandPut("seek {0} to {1}", _fileNameAllias, miliseconds);
        }

        public int GetSongLenght()
        {
            if (GetStatusMode() != PlayingMode.playing)
                return 0;

			string sLength = ExecuteCommandGet("status {0} length", _fileNameAllias);
			return int.Parse(sLength);
        }

        #endregion

        #region Audio
        public bool SetVolume(int volume)
        {
            if (volume >= 0 && volume <= 1000)
                return ExecuteCommandPut("setaudio {0} volume to {1}", _fileNameAllias, volume);

            return false;
        }

        public bool SetBalance(int balance, int volume)
        {
			const int max = 1000;
			double factor = volume / max; 

            if (balance >= 0 && balance <= max)
            {
				int left = (max - balance) * volume / max;
				bool result = ExecuteCommandPut("setaudio {0} left volume to {1}", _fileNameAllias, left);
				int right = balance * volume / max;
				result = ExecuteCommandPut("setaudio {0} right volume to {1}", _fileNameAllias, right);
                return true;
            }
            else
                return false;
        }

        #endregion

		private static string GetErrorString(int error)
		{
			StringBuilder buffer = new StringBuilder(512);
			int returnValue = mciGetErrorString(error, buffer, buffer.Capacity);
			string err = buffer.ToString();
			return err;
		}

		private static void LogResult(int error, string sCommand)
		{
			if (error != 0)
			{
				System.Diagnostics.Trace.WriteLine("Command: " + sCommand);
				System.Diagnostics.Trace.WriteLine("ERROR: " + GetErrorString(error));
			}
		}

		private string ReplaceSpaces(string fileName)
		{
			return fileName.Replace(' ', '_').Replace('-', '_');
		}


		//public int GetNextSong(bool previous)
		//{
		//    if (Shuffle)
		//    {
		//        int i;
		//        if (playlist.Count == 1)
		//            return 0;
		//        while (true)
		//        {
		//            i = randomNumber.Next(playlist.Count);
		//            if (i != NowPlaying)
		//                return i;
		//        }
		//    }
		//    else if (Loop && !previous)
		//    {
		//        if (NowPlaying == playlist.Count - 1)
		//            return 0;
		//        else
		//            return NowPlaying + 1;
		//    }
		//    else if (Loop && previous)
		//    {
		//        if (NowPlaying == 0)
		//            return playlist.Count - 1;
		//        else
		//            return NowPlaying - 1;
		//    }
		//    else
		//    {
		//        if (previous)
		//        {
		//            if (NowPlaying != 0)
		//                return NowPlaying - 1;
		//            else
		//                return 0;
		//        }
		//        else
		//        {
		//            if (NowPlaying != playlist.Count - 1)
		//                return NowPlaying + 1;
		//            else
		//                return 0;
		//        }
		//    }
		//}

	}
}
