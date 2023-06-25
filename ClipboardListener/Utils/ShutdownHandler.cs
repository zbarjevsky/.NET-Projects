using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardManager
{
    public static class ShutdownHandler
    {
        public static bool AbortShutdownIfScheduled { get; set; } = false;
        private static volatile bool _bExitMonitoringShutdown = false;

        static ShutdownHandler()
        {
            SetPrivilege("SeShutdownPrivilege", 2);

            Thread thread = new Thread(new ThreadStart(MonitoringShutdownEvent));
            thread.IsBackground = true;
            thread.Name = "MonitoringShutdownEvent";
            thread.Start();
        }

        public static int AbortSystemShutdown(string lpMachineName = "")
        {
            return Win32_Shutdown.AbortSystemShutdown(lpMachineName);
        }

        public static void CancelMonitoringShutdown() { _bExitMonitoringShutdown = true; Thread.Sleep(33); }

        //return true to avoid base.WndProc
        //https://www.microsoftpressstore.com/articles/article.aspx?p=2201310&seqNum=3
        public static bool ProcessShutdownMessage(IntPtr hWnd, ref Message m)
        {
            ParseAndLogShutdownParameters(ref m);

            if (AbortShutdownIfScheduled && m.Msg == (int)Win32_Shutdown.eMsg.WM_QUERYENDSESSION)
            {
                Win32_Shutdown.ShutdownBlockReasonCreate(hWnd, "Block Unexpected Shutdown!");
                Utils.LogC.WriteLineF("ProcessShutdownMessage: ShutdownBlockReasonCreate(hWnd = 0x{0:X8})", (uint)hWnd);

                const string question = "Allow Windows Shutdown/Reboot/Log off?";
                DialogResult res = MessageBox.Show(question, Environment.UserName, 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, 
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                if (res == DialogResult.Yes)
                {
                    Utils.LogC.WriteLineF("ProcessShutdownMessage(yes - Shutdown) - DialogResult: " + res);
                    Win32_Shutdown.ShutdownBlockReasonDestroy(hWnd);
                    return false; //Allow Windows to shutdown
                }
                else //abort shutdown
                {
                    Utils.LogC.WriteLineF("ProcessShutdownMessage(No - Abort) - DialogResult: " + res);
                    return true;
                }
            }

            return false; //Allow Windows to shutdown
        }

        private static void MonitoringShutdownEvent()
        {
            ulong count = 0;
            while (!_bExitMonitoringShutdown)
            {
                try
                {
                    if (((count++) % 10) == 0 && AbortShutdownIfScheduled) //every 3 seconds
                    {
                        //will return non 0 if succeded
                        //if error - do nothing - no event log entry recorded
                        //do no use 'InitiateShutdown' it writes event log entry
                        if (AbortSystemShutdown(null) != 0) //shutdown aborted
                        {
                            Utils.LogC.WriteLineF("Shutdown schedule was detected and aborted!!!");
                        }
                    }
                    Thread.Sleep(30);
                }
                catch (Exception err)
                {
                    Win32Exception error = new Win32Exception(Marshal.GetLastWin32Error());
                    Utils.LogC.WriteLineF("ShutdownMonitoring exception: " + err.ToString());
                }
            }
        }

        private static void ProcessShutdownIfDetected()
        {
            int error_code =  (int)Win32_Shutdown.InitiateShutdown(null, "Testing for Shutdown", 
                1024 * 1024,
                Win32_Shutdown.ShutdownFlags.SHUTDOWN_RESTARTAPPS,
                Win32_Shutdown.ShutdownReasons.SHTDN_REASON_MAJOR_APPLICATION |
                Win32_Shutdown.ShutdownReasons.SHTDN_REASON_MINOR_INSTALLATION |
                Win32_Shutdown.ShutdownReasons.SHTDN_REASON_FLAG_PLANNED);

            if (error_code == 0) //succeded to schedule shutdown - abort and continue
            {
                AbortSystemShutdown(null);
            }
            else if (error_code == Win32_Shutdown.ERROR_SHUTDOWN_IS_SCHEDULED) //this is what I am expecting to abort
            {
                AbortSystemShutdown(null);
                Utils.LogC.WriteLineF("Shutdown schedule was detected and aborted!!!");
            }
            else
            {
                Win32Exception e = new Win32Exception(error_code);
                Utils.LogC.WriteLineF("Error Detecting Shutdown: " + e.Message);
            }
        }

        private static void ParseAndLogShutdownParameters(ref Message m)
        {
            try
            {
                string msg = "WndProc: ";
                if (m.Msg == (int)Win32_Shutdown.eMsg.WM_QUERYENDSESSION)
                    msg += "WM_QUERYENDSESSION";
                else if (m.Msg == (int)Win32_Shutdown.eMsg.WM_ENDSESSION)
                    msg += "WM_ENDSESSION";
                msg += string.Format(", LParam: 0x{0:X8}", (uint)m.LParam);
                if ((uint)m.LParam == 0) //
                    msg += ", NORMAL";
                if (((uint)m.LParam & (uint)Win32_Shutdown.eLParam.ENDSESSION_CLOSEAPP) != 0)
                    msg += ", ENDSESSION_CLOSEAPP";
                if (((uint)m.LParam & (uint)Win32_Shutdown.eLParam.ENDSESSION_CRITICAL) != 0)
                    msg += ", ENDSESSION_CRITICAL";
                if (((uint)m.LParam & (uint)Win32_Shutdown.eLParam.ENDSESSION_LOGOFF) != 0)
                    msg += ", ENDSESSION_LOGOFF";

                Utils.LogC.WriteLineF(msg);
            }
            catch (Exception err)
            {
                Utils.LogC.WriteLineF(err.ToString());
            }
        }

        private static void SetPrivilege(String privilegeName, Int32 state)
        {
            Assembly asm = Assembly.GetAssembly(typeof(System.Diagnostics.Process));
            if (asm == null) return;

            Type t = asm.GetType("System.Diagnostics.Process");
            if (t == null) return;

            MethodInfo mi = t.GetMethod("SetPrivilege", BindingFlags.Static | BindingFlags.NonPublic);
            if (mi == null) return;

            Object[] parameters = { privilegeName, state };
            mi.Invoke(null, parameters);
        }
    }

    //https://www.meziantou.net/2014/12/29/prevent-windows-shutdown-or-session-ending-in-net
    public class Win32_Shutdown
    {
        public const int ERROR_SHUTDOWN_IS_SCHEDULED = 1190;

        public enum eMsg : int
        {
            WM_QUERYENDSESSION = 0x0011,
            WM_ENDSESSION = 0x0016,
        }

        public enum eLParam : uint
        {
            ENDSESSION_NORMAL = 0,
            ENDSESSION_CLOSEAPP = 0x00000001, //the system is being serviced, or system resources are exhausted
            ENDSESSION_CRITICAL = 0x40000000, //the application is forced to shut down
            ENDSESSION_LOGOFF = 0x80000000, //the user is logging off
        }

        public const uint SHUTDOWN_NORETRY = 0x00000001;

        //Indicates that the system cannot be shut down and sets a reason string to be displayed to the user 
        //if system shutdown is initiated
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string reason);

        //Indicates that the system can be shut down
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShutdownBlockReasonDestroy(IntPtr hWnd);

        //This function sets a shutdown order for a process relative to the other processes in the system
        //method determines the order in which Windows sends the WM_ENDSESSION and WM_QUERYENDSESSION messages to the applications.
        //This is used in the case where an application A depends on an application B. 
        //The application A wishes to be informed before the application B that the machine is stopping 
        //to potentially block this stop. Thus application B remains alive as application A blocks the shutdown.
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessShutdownParameters(uint dwLevel, uint dwFlags);

        [DllImport("advapi32.dll", EntryPoint = "InitiateShutdown", SetLastError = true)]
        public static extern UInt32 InitiateShutdown(
            string lpMachineName, //The name of the computer to be shut down. If the value of this parameter is NULL, the local computer is shut down.
            string lpMessage, //The message to be displayed in the interactive shutdown dialog box.
            UInt32 dwGracePeriod, //The number of seconds to wait before shutting down the computer.
            ShutdownFlags dwShutdownFlags, //One or more bit flags that specify options for the shutdown.
            ShutdownReasons dwReason); //The reason for initiating the shutdown. 

        [DllImport("advapi32.dll", EntryPoint = "AbortSystemShutdown")]
        public static extern int AbortSystemShutdown(string lpMachineName);

        public enum ShutdownFlags : uint
        {
            SHUTDOWN_FORCE_OTHERS = 0x00000001, //  (0x1) All sessions are forcefully logged off. If this flag is not set and users other than the current user are logged on to the computer specified by the lpMachineName parameter, this function fails with a return value of ERROR_SHUTDOWN_USERS_LOGGED_ON.
            SHUTDOWN_FORCE_SELF = 0x00000002, //(0x2) Specifies that the originating session is logged off forcefully. If this flag is not set, the originating session is shut down interactively, so a shutdown is not guaranteed even if the function returns successfully.
            SHUTDOWN_GRACE_OVERRIDE = 0x00000020, // (0x20) Overrides the grace period so that the computer is shut down immediately.
            SHUTDOWN_HYBRID = 0x00000200, // (0x200) Beginning with InitiateShutdown running on Windows 8, you must include the SHUTDOWN_HYBRID flag with one or more of the flags in this table to specify options for the shutdown.
                                          //Beginning with Windows 8, InitiateShutdown always initiate a full system shutdown if the SHUTDOWN_HYBRID flag is absent.
            SHUTDOWN_INSTALL_UPDATES = 0x00000040, // (0x40) The computer installs any updates before starting the shutdown.
            SHUTDOWN_NOREBOOT = 0x00000010, // (0x10) The computer is shut down but is not powered down or rebooted.
            SHUTDOWN_POWEROFF = 0x00000008, // (0x8) The computer is shut down and powered down.
            SHUTDOWN_RESTART = 0x00000004, // (0x4) The computer is shut down and rebooted.
            SHUTDOWN_RESTARTAPPS = 0x00000080, // (0x80) The system is rebooted using the ExitWindowsEx function with the EWX_RESTARTAPPS flag.This restarts any applications that have been registered for restart using the RegisterApplicationRestart function.
        }

        //https://docs.microsoft.com/en-us/windows/desktop/Shutdown/system-shutdown-reason-codes
        public enum ShutdownReasons : uint
        {
            SHTDN_REASON_MAJOR_APPLICATION = 0x00040000, //Application issue.
            SHTDN_REASON_MAJOR_HARDWARE = 0x00010000, //Hardware issue.
            SHTDN_REASON_MAJOR_LEGACY_API = 0x00070000, //The InitiateSystemShutdown function was used instead of InitiateSystemShutdownEx.
            SHTDN_REASON_MAJOR_OPERATINGSYSTEM = 0x00020000, //Operating system issue.
            SHTDN_REASON_MAJOR_OTHER = 0x00000000, //Other issue.
            SHTDN_REASON_MAJOR_POWER = 0x00060000, //Power failure.
            SHTDN_REASON_MAJOR_SOFTWARE = 0x00030000, //Software issue.
            SHTDN_REASON_MAJOR_SYSTEM = 0x00050000, //System failure.

            SHTDN_REASON_MINOR_INSTALLATION = 0x00000002,

            SHTDN_REASON_FLAG_USER_DEFINED = 0x40000000, //The reason code is defined by the user. For more information, see Defining a Custom Reason Code. 
                                                         //If this flag is not present, the reason code is defined by the system.
            SHTDN_REASON_FLAG_PLANNED = 0x80000000, //The shutdown was planned. The system generates a System State Data (SSD) file. This file contains system state information such as the processes, threads, memory usage, and configuration. 
                                                    //If this flag is not present, the shutdown was unplanned. Notification and reporting options are controlled by a set of policies. For example, after logging in, the system displays a dialog box reporting the unplanned shutdown if the policy has been enabled. An SSD file is created only if the SSD policy is enabled on the system. The administrator can use Windows Error Reporting to send the SSD data to a central location, or to Microsoft.        
        }
    }
}
