using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MeditationStopWatch
{
    [FlagsAttribute]
    public enum EXECUTION_STATE : uint
    {
        ES_SYSTEM_REQUIRED = 0x00000001,
        ES_DISPLAY_REQUIRED = 0x00000002,
        // Legacy flag, should not be used.
        // ES_USER_PRESENT   = 0x00000004,
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
    }

    public static class ScreenSaver
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        public static EXECUTION_STATE ResetIdleTimer()
        {
            return SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED);
        }
    }

    //public static class ScreenSaver
    //{
    //	// Signatures for unmanaged calls
    //	[DllImport("user32.dll", CharSet = CharSet.Auto)]
    //	private static extern bool SystemParametersInfo(int uAction, int uParam, ref int lpvParam, int flags);

    //	[DllImport("user32.dll", CharSet = CharSet.Auto)]
    //	private static extern bool SystemParametersInfo(int uAction, int uParam, ref bool lpvParam, int flags);

    //	[DllImport("user32.dll", CharSet = CharSet.Auto)]
    //	private static extern int PostMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

    //	[DllImport("user32.dll", CharSet = CharSet.Auto)]
    //	private static extern IntPtr OpenDesktop(string hDesktop, int Flags, bool Inherit, uint DesiredAccess);

    //	[DllImport("user32.dll", CharSet = CharSet.Auto)]
    //	private static extern bool CloseDesktop(IntPtr hDesktop);

    //	[DllImport("user32.dll", CharSet = CharSet.Auto)]
    //	private static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDesktopWindowsProc callback, IntPtr lParam);

    //	[DllImport("user32.dll", CharSet = CharSet.Auto)]
    //	private static extern bool IsWindowVisible(IntPtr hWnd);

    //	[DllImport("user32.dll", CharSet = CharSet.Auto)]
    //	public static extern IntPtr GetForegroundWindow();

    //	// Callbacks
    //	private delegate bool EnumDesktopWindowsProc(IntPtr hDesktop, IntPtr lParam);

    //	// Constants
    //	private const int SPI_GETSCREENSAVERACTIVE = 16;
    //	private const int SPI_SETSCREENSAVERACTIVE = 17;
    //	private const int SPI_GETSCREENSAVERTIMEOUT = 14;
    //	private const int SPI_SETSCREENSAVERTIMEOUT = 15;
    //	private const int SPI_GETSCREENSAVERRUNNING = 114;
    //	private const int SPIF_SENDWININICHANGE = 2;

    //	private const uint DESKTOP_WRITEOBJECTS = 0x0080;
    //	private const uint DESKTOP_READOBJECTS = 0x0001;
    //	private const int WM_CLOSE = 16;

    //	private const int STOPPED = 0;
    //	private const int RUNNING = 1;
    //	private const int FALSE = 0;
    //	private const int TRUE = 1;

    //	// Returns TRUE if the screen saver is active (enabled, but not necessarily running).
    //	public static bool GetScreenSaverActive()
    //	{
    //		bool isActive = false;

    //		bool result = SystemParametersInfo(SPI_GETSCREENSAVERACTIVE, 0, ref isActive, 0);
    //		LogError(result, "GetScreenSaverActive()");

    //		return isActive;
    //	}

    //	// Pass in TRUE(1) to activate or FALSE(0) to deactivate the screen saver.
    //	public static void SetScreenSaverActive(bool Active)
    //	{
    //		int nullVar = 0;

    //		bool result = SystemParametersInfo(SPI_SETSCREENSAVERACTIVE, Active ? 1 : 0, ref nullVar, SPIF_SENDWININICHANGE);
    //		LogError(result, "SetScreenSaverActive()");
    //	}

    //	// Returns the screen saver timeout setting, in seconds
    //	public static Int32 GetScreenSaverTimeout()
    //	{
    //		Int32 value = 0;

    //		bool result = SystemParametersInfo(SPI_GETSCREENSAVERTIMEOUT, 0, ref value, 0);
    //		LogError(result, "GetScreenSaverTimeout()");

    //		return value;
    //	}

    //	// Pass in the number of seconds to set the screen saver timeout value.
    //	public static void SetScreenSaverTimeout(Int32 Value)
    //	{
    //		int nullVar = 0;

    //		bool result = SystemParametersInfo(SPI_SETSCREENSAVERTIMEOUT, Value, ref nullVar, SPIF_SENDWININICHANGE);
    //		LogError(result, "SetScreenSaverTimeout()");
    //	}

    //	// Returns TRUE if the screen saver is actually running
    //	public static bool GetScreenSaverRunning()
    //	{
    //		bool isRunning = false;

    //		bool result = SystemParametersInfo(SPI_GETSCREENSAVERRUNNING, 0, ref isRunning, 0);
    //		LogError(result, "GetScreenSaverRunning()");

    //		return isRunning;
    //	}

    //	// From Microsoft's Knowledge Base article #140723: http://support.microsoft.com/kb/140723
    //	// "How to force a screen saver to close once started in Windows NT, Windows 2000, and Windows Server 2003"

    //	public static void KillScreenSaver()
    //	{
    //		IntPtr hDesktop = OpenDesktop("Screen-saver", 0, false, DESKTOP_READOBJECTS | DESKTOP_WRITEOBJECTS);
    //		if (hDesktop != IntPtr.Zero)
    //		{
    //			EnumDesktopWindows(hDesktop, new EnumDesktopWindowsProc(KillScreenSaverFunc), IntPtr.Zero);
    //			CloseDesktop(hDesktop);
    //		}
    //		else
    //		{
    //			PostMessage(GetForegroundWindow(), WM_CLOSE, 0, 0);
    //		}
    //	}

    //	private static bool KillScreenSaverFunc(IntPtr hWnd, IntPtr lParam)
    //	{
    //		if (IsWindowVisible(hWnd))
    //			PostMessage(hWnd, WM_CLOSE, 0, 0);
    //		return true;
    //	}

    //	private static void LogError(bool result, string command)
    //	{
    //		if (result) return;

    //		System.Diagnostics.Trace.WriteLine("Error in: " + command);
    //	}
    //}
}
