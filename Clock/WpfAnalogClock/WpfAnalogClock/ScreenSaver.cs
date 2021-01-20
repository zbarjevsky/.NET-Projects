using System;
using System.Runtime.InteropServices;

namespace MkZ.WPF.WpfAnalogClock
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

        public static EXECUTION_STATE ResetIdleTimer(bool reset)
        {
            if(reset)
                return SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED);
            else
                return SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
    }
}
