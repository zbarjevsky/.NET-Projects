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
        //Random randomNumber = new Random();
        private StringBuilder m_sbErrorMessage;  // MCI Error message
        private StringBuilder m_sbReturnData;  // MCI return m_Data
        private string _command;  // String that holds the MCI command
        private string _fileNameAllias = string.Empty;
        //private IList<FileInfo> playlist;  // ListView as a playlist with the song path
		//public int NowPlaying { get; set; }
		public bool Paused { get; set; }
		//public bool Loop { get; set; }
		//public bool Shuffle { get; set; }

        [DllImport("winmm.dll")]
        private static extern int mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        [DllImport("winmm.dll")]
        public static extern int mciGetErrorString(int errCode, StringBuilder errMsg, int buflen);

        public string FileName {  get { return _fileNameAllias; } }

		private bool Command(string cmd, params object [] args)
		{
            if (string.IsNullOrWhiteSpace(_fileNameAllias))
                return true;

			_command = string.Format(cmd, args);
			int error = mciSendString(_command, null, 0, IntPtr.Zero);
			if (error == 0)
				return true;

			LogResult(error, _command);
			return false;
		}

		private string CommandGetData(string cmd, params object[] args)
		{
			if (string.IsNullOrWhiteSpace(_fileNameAllias))
				return string.Empty;

			_command = string.Format(cmd, args);
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
            m_sbReturnData = new StringBuilder(128);
        }

        #region Operations

        public void CmdClose()
        {
			Command("close {0}", _fileNameAllias);
			_fileNameAllias = string.Empty;
        }

        public bool Open(string fullPath, string fileNameAlias)
        {
            CmdClose();

            // Try to open as mpegvideo 
			_fileNameAllias = ReplaceSpaces(fileNameAlias);
            bool result = Command("open \"{0}\" type mpegvideo alias {1}", fullPath, _fileNameAllias);
            if (!result)
            {
                // Let MCI deside which file type the song is
                result = Command("open \"{0}\" alias {1}", fullPath, _fileNameAllias);
                return result;
            }
            else
                return true;      
        }


        public bool Play(string sFileName, string fileNameAlias)
        {
            if (!Open(sFileName, fileNameAlias))
                return false;

            bool result = CmdPlay();
            if (result)
            {
                return true;
            }
            else
            {
                CmdClose();
                return false;
            }
        }

        public bool CmdPlay()
        {
            return Command("play {0}", _fileNameAllias);
        }

        public bool CmdStop()
        {
            return Command("stop {0}", _fileNameAllias);
        }

        public bool CmdPause()
        {
            return Command("pause {0}", _fileNameAllias);
        }

        public bool CmdResume()
        {
            return Command("resume {0}", _fileNameAllias);
        }

        #endregion

        #region Status

        public string GetStatusMode()
		{
    		return CommandGetData("status {0} mode", _fileNameAllias);
		}

        public bool IsPlaying(string sMode = "")
        {
            if(string.IsNullOrWhiteSpace(sMode))
                sMode = GetStatusMode();
            return (sMode.Length == 7 && sMode.Substring(0, 7) == "playing");
        }

        public bool IsOpen(string sMode = "")
        {
            if (string.IsNullOrWhiteSpace(sMode))
                sMode = GetStatusMode();
            return (sMode.Length == 4 && sMode.Substring(0, 4) == "open");
        }

        public bool IsPaused(string sMode = "")
        {
            if (string.IsNullOrWhiteSpace(sMode))
                sMode = GetStatusMode();
            return (sMode.Length == 6 && sMode.Substring(0, 6) == "paused");
        }

        public bool IsStopped(string sMode = "")
        {
            if (string.IsNullOrWhiteSpace(sMode))
                sMode = GetStatusMode();
            return (sMode.Length == 7 && sMode.Substring(0, 7) == "stopped");
        }
        #endregion

        #region Logic

        public int GetCurentMilisecond()
        {
			string sPosition = CommandGetData("status {0} position", _fileNameAllias);
			if (string.IsNullOrWhiteSpace(sPosition))
				return 0;

			return int.Parse(sPosition);
        }

        public bool SetPosition(int miliseconds)
        {
            if (IsPlaying())
				return Command("play {0} from {1}", _fileNameAllias, miliseconds);
            else
				return Command("seek {0} to {1}", _fileNameAllias, miliseconds);
        }

        public int GetSongLenght()
        {
            if (!IsPlaying())
                return 0;

			string sLength = CommandGetData("status {0} length", _fileNameAllias);
			return int.Parse(sLength);
        }

        #endregion

        #region Audio
        public bool SetVolume(int volume)
        {
            if (volume >= 0 && volume <= 1000)
                return Command("setaudio {0} volume to {1}", _fileNameAllias, volume);

            return false;
        }

        public bool SetBalance(int balance, int volume)
        {
			const int max = 1000;
			double factor = volume / max; 

            if (balance >= 0 && balance <= max)
            {
				int left = (max - balance) * volume / max;
				bool result = Command("setaudio {0} left volume to {1}", _fileNameAllias, left);
				int right = balance * volume / max;
				result = Command("setaudio {0} right volume to {1}", _fileNameAllias, right);
                return true;
            }
            else
                return false;
        }

        #endregion

		private string GetErrorString(int error)
		{
			StringBuilder buffer = new StringBuilder(512);
			int returnValue = mciGetErrorString(error, buffer, buffer.Capacity);
			string err = buffer.ToString();
			return err;
		}

		private void LogResult(int error, string sCommand)
		{
			if (error != 0)
			{
				System.Diagnostics.Trace.WriteLine(sCommand);
				System.Diagnostics.Trace.WriteLine(GetErrorString(error));
			}
		}

		private string ReplaceSpaces(string fileName)
		{
			return fileName.Replace(' ', '_');
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
