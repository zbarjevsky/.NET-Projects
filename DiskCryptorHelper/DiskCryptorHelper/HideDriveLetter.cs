using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskCryptorHelper
{
    public class HideDriveLetter
    {
        //https://www.howtogeek.com/howto/windows-vista/hide-drives-from-your-computer-in-windows-vista/
        public static void Hide(char driveLetter, bool hide = true)
        {
            const string explorer = @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer";
            const string noDrives = "NoDrives";

            //subkey nodrive in HKEY_CURRENT_USER
            RegistryKey rKeyExplorer = Registry.CurrentUser.CreateSubKey(explorer);
            int currValue = (int)rKeyExplorer.GetValue(noDrives, 0);

            currValue = UpdateHideDriveValue(currValue, driveLetter, hide);

            //create DWORD 'NoDrives' with value 4. for hiding drive C
            rKeyExplorer.SetValue(noDrives, currValue, RegistryValueKind.DWord);
            rKeyExplorer.Close();
        }

        public static bool IsDriveHidden(char driveLetter)
        {
            const string explorer = @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer";
            const string noDrives = "NoDrives";

            //subkey nodrive in HKEY_CURRENT_USER
            RegistryKey rKeyExplorer = Registry.CurrentUser.CreateSubKey(explorer);
            int currValue = (int)rKeyExplorer.GetValue(noDrives, 0);
            int mask = GetNoDriveMask(driveLetter);

            //create DWORD 'NoDrives' with value 4. for hiding drive C
            rKeyExplorer.Close();

            return (currValue & mask) != 0;
        }

        //This value is a 32 bit number, and the bits are arranged in reverse order with a value of 1 hiding that drive.
        //For example, if we wanted to hide drives A: and F: we would arrange it like this:
        //Z Y X W V U T S R Q P O N M L K J I H G F E D C B A
        //0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 0 0 0 0 1
        public static int UpdateHideDriveValue(int currValue, char driveLetter, bool hide)
        {
            int mask = GetNoDriveMask(driveLetter);
            if (hide)
            {
                currValue |= mask;
            }
            else
            {
                currValue &= ~mask;
            }
            return currValue;
        }

        public static int GetNoDriveMask(char driveLetter)
        {
            //const string driveLetters = "ZYXWVUTSRQPONMLKJIHGFEDCBA";
            int bitPlace = (driveLetter - 'A');
            int mask = 1 << bitPlace;
            return mask;
        }

        //Suppressing AutoRun Programmatically
        //https://msdn.microsoft.com/en-us/library/windows/desktop/cc144204%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
    }
}
