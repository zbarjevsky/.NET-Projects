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

namespace DiskCryptorHelper
{
    public static class ShutdownHandler
    {
        public static volatile bool AbortShutdownIfScheduled = false;
        public static volatile bool ExitMonitoringShutdown = false;

        public static void AbortSystemShutdown()
        {
            if(AbortSystemShutdown(null) == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            } 
        }

        static ShutdownHandler()
        {
            Thread thread = new Thread(new ThreadStart(MonitoringShutdownEvent));
            thread.IsBackground = true;
            thread.Name = "MonitoringShutdownEvent";
            thread.Start();

            SetPrivilege("SeShutdownPrivilege", 2);
        }

        private static void MonitoringShutdownEvent()
        {
            while (!ExitMonitoringShutdown)
            {
                try
                {
                    if (AbortShutdownIfScheduled)
                        ProcessShutdownIfDetected();

                    Thread.Sleep(33);
                }
                catch (Exception err)
                {
                    Debug.WriteLine("ShutdownMonitoring: " + err.ToString());
                }
            }
        }

        private static void ProcessShutdownIfDetected()
        {
            int error_code =  (int)InitiateShutdown(null, "Testing for Shutdown", 
                1024 * 1024,
                ShutdownFlags.SHUTDOWN_RESTARTAPPS,
                ShutdownReasons.SHTDN_REASON_MAJOR_APPLICATION |
                ShutdownReasons.SHTDN_REASON_MINOR_INSTALLATION |
                ShutdownReasons.SHTDN_REASON_FLAG_PLANNED);

            if (error_code == 0) //succeded to schedule shutdown - abort and continue
            {
                AbortSystemShutdown(null);
            }
            else if (error_code == ERROR_SHUTDOWN_IS_SCHEDULED) //this is what I am expecting to abort
            {
                AbortSystemShutdown(null);
                File.AppendAllText("C:\\Temp\\Log11.txt",
                    DateTime.Now.ToString("u") + " - Shutdown schedule was detected and aborted!!!\r\n");
            }
            else
            {
                Win32Exception e = new Win32Exception(error_code);
                File.AppendAllText("C:\\Temp\\Log11.txt", 
                    DateTime.Now.ToString("u") + " - Error Detecting Shutdown: " + e.Message + "\r\n");
            }
        }

        //                    if (e.Reason == SessionEndReasons.SystemShutdown && Settings.Default.PreventShutdown)
        //            {
        //                if (System.Windows.Forms.MessageBox.Show("Would you like to cancel Shutdown?", "DiskCryptor Helper",
        //                    MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1,
        //                    System.Windows.Forms.MessageBoxOptions.ServiceNotification) == DialogResult.Yes)
        //                {
        //                    e.Cancel = true;
        //                    Process.Start("shutdown", "/a");
        //                }
        //}

        private const int ERROR_SHUTDOWN_IS_SCHEDULED = 1190;

        [DllImport("advapi32.dll", EntryPoint = "InitiateShutdown", SetLastError = true)]
        static extern UInt32 InitiateShutdown(
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

        #region System Metrics Declarations
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);
        public enum SystemMetric
        {
            SM_CXSCREEN = 0,  // 0x00
            SM_CYSCREEN = 1,  // 0x01
            SM_CXVSCROLL = 2,  // 0x02
            SM_CYHSCROLL = 3,  // 0x03
            SM_CYCAPTION = 4,  // 0x04
            SM_CXBORDER = 5,  // 0x05
            SM_CYBORDER = 6,  // 0x06
            SM_CXDLGFRAME = 7,  // 0x07
            //SM_CXFIXEDFRAME = 7,  // 0x07
            SM_CYDLGFRAME = 8,  // 0x08
            //SM_CYFIXEDFRAME = 8,  // 0x08
            SM_CYVTHUMB = 9,  // 0x09
            SM_CXHTHUMB = 10, // 0x0A
            SM_CXICON = 11, // 0x0B
            SM_CYICON = 12, // 0x0C
            SM_CXCURSOR = 13, // 0x0D
            SM_CYCURSOR = 14, // 0x0E
            SM_CYMENU = 15, // 0x0F
            SM_CXFULLSCREEN = 16, // 0x10
            SM_CYFULLSCREEN = 17, // 0x11
            SM_CYKANJIWINDOW = 18, // 0x12
            SM_MOUSEPRESENT = 19, // 0x13
            SM_CYVSCROLL = 20, // 0x14
            SM_CXHSCROLL = 21, // 0x15
            SM_DEBUG = 22, // 0x16
            SM_SWAPBUTTON = 23, // 0x17
            SM_CXMIN = 28, // 0x1C
            SM_CYMIN = 29, // 0x1D
            SM_CXSIZE = 30, // 0x1E
            SM_CYSIZE = 31, // 0x1F
            //SM_CXSIZEFRAME = 32, // 0x20
            SM_CXFRAME = 32, // 0x20
            //SM_CYSIZEFRAME = 33, // 0x21
            SM_CYFRAME = 33, // 0x21
            SM_CXMINTRACK = 34, // 0x22
            SM_CYMINTRACK = 35, // 0x23
            SM_CXDOUBLECLK = 36, // 0x24
            SM_CYDOUBLECLK = 37, // 0x25
            SM_CXICONSPACING = 38, // 0x26
            SM_CYICONSPACING = 39, // 0x27
            SM_MENUDROPALIGNMENT = 40, // 0x28
            SM_PENWINDOWS = 41, // 0x29
            SM_DBCSENABLED = 42, // 0x2A
            SM_CMOUSEBUTTONS = 43, // 0x2B
            SM_SECURE = 44, // 0x2C
            SM_CXEDGE = 45, // 0x2D
            SM_CYEDGE = 46, // 0x2E
            SM_CXMINSPACING = 47, // 0x2F
            SM_CYMINSPACING = 48, // 0x30
            SM_CXSMICON = 49, // 0x31
            SM_CYSMICON = 50, // 0x32
            SM_CYSMCAPTION = 51, // 0x33
            SM_CXSMSIZE = 52, // 0x34
            SM_CYSMSIZE = 53, // 0x35
            SM_CXMENUSIZE = 54, // 0x36
            SM_CYMENUSIZE = 55, // 0x37
            SM_ARRANGE = 56, // 0x38
            SM_CXMINIMIZED = 57, // 0x39
            SM_CYMINIMIZED = 58, // 0x3A
            SM_CXMAXTRACK = 59, // 0x3B
            SM_CYMAXTRACK = 60, // 0x3C
            SM_CXMAXIMIZED = 61, // 0x3D
            SM_CYMAXIMIZED = 62, // 0x3E
            SM_NETWORK = 63, // 0x3F
            SM_CLEANBOOT = 67, // 0x43
            SM_CXDRAG = 68, // 0x44
            SM_CYDRAG = 69, // 0x45
            SM_SHOWSOUNDS = 70, // 0x46
            SM_CXMENUCHECK = 71, // 0x47
            SM_CYMENUCHECK = 72, // 0x48
            SM_SLOWMACHINE = 73, // 0x49
            SM_MIDEASTENABLED = 74, // 0x4A
            SM_MOUSEWHEELPRESENT = 75, // 0x4B
            SM_XVIRTUALSCREEN = 76, // 0x4C
            SM_YVIRTUALSCREEN = 77, // 0x4D
            SM_CXVIRTUALSCREEN = 78, // 0x4E
            SM_CYVIRTUALSCREEN = 79, // 0x4F
            SM_CMONITORS = 80, // 0x50
            SM_SAMEDISPLAYFORMAT = 81, // 0x51
            SM_IMMENABLED = 82, // 0x52
            SM_CXFOCUSBORDER = 83, // 0x53
            SM_CYFOCUSBORDER = 84, // 0x54
            SM_TABLETPC = 86, // 0x56
            SM_MEDIACENTER = 87, // 0x57
            SM_STARTER = 88, // 0x58
            SM_SERVERR2 = 89, // 0x59
            SM_MOUSEHORIZONTALWHEELPRESENT = 91, // 0x5B
            SM_CXPADDEDBORDER = 92, // 0x5C
            SM_DIGITIZER = 94, // 0x5E
            SM_MAXIMUMTOUCHES = 95, // 0x5F

            SM_REMOTESESSION = 0x1000, // 0x1000
            SM_SHUTTINGDOWN = 0x2000, // 0x2000
            SM_REMOTECONTROL = 0x2001, // 0x2001
        }
#endregion System Metrics Declarations
    }
}
