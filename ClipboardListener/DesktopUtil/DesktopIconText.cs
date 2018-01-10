//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;

namespace ClipboardManager.DesktopUtil
{
    class DesktopIconText
    {
        //public string GetIconText(IntPtr desktopProcessHandle, int itemIndex)
        //{
        //    IntPtr lviItemMem = Win32.VirtualAllocEx(desktopProcessHandle, IntPtr.Zero, 4096, Win32.AllocationType.Reserve | Win32.AllocationType.Commit, Win32.MemoryProtection.ReadWrite);
        //    IntPtr lviItemText = Win32.VirtualAllocEx(desktopProcessHandle, IntPtr.Zero, 4096, Win32.AllocationType.Reserve | Win32.AllocationType.Commit, Win32.MemoryProtection.ReadWrite);

        //    try
        //    {
        //        uint numberOfBytes = 0;
        //        // Declare and populate the LVITEM structure.  Some of this code came from here:
        //        // http://stackoverflow.com/questions/4857602/get-listview-items-from-other-windows
        //        LVITEM lvi = new LVITEM();
        //        lvi.mask = Win32.LVIF_TEXT;
        //        lvi.cchTextMax = 512;
        //        lvi.iItem = itemIndex;      // the zero-based index of the ListView item
        //        lvi.iSubItem = 0;           // the one-based index of the subitem, or 0 if this

        //        //  Memory for the actual item text.
        //        lvi.pszText = lviItemText;

        //        // Send the LVM_GETITEM message to fill the LVITEM structure
        //        IntPtr ptrLvi = Marshal.AllocHGlobal(Marshal.SizeOf(lvi));
        //        Marshal.StructureToPtr(lvi, ptrLvi, false);

        //        // write the lvi structure into the desktops process memory
        //        Win32.WriteProcessMemory(desktopProcessHandle, lviItemMem,
        //                                 ptrLvi,
        //                                 Marshal.SizeOf(lvi),
        //                                 ref numberOfBytes);

        //        Marshal.FreeHGlobal(ptrLvi);

        //        // get the item at itemIndex
        //        Win32.SendMessage(mDesktopHandle, Win32.LVM_GETITEM, IntPtr.Zero, lviItemMem);

        //        // read the item text out of the desktop process memory -> itemTextBytes
        //        byte[] itemTextBytes = new byte[4096];
        //        Win32.ReadProcessMemory(desktopProcessHandle, lviItemText,
        //                                Marshal.UnsafeAddrOfPinnedArrayElement(itemTextBytes, 0),
        //                                4096,
        //                                ref numberOfBytes);

        //        // bytes to string
        //        string itemText = Encoding.Unicode.GetString(itemTextBytes).TrimEnd(new[] { '\0' });
        //        return itemText;
        //    }
        //    finally
        //    {
        //        if (lviItemMem != IntPtr.Zero)
        //            Win32.VirtualFreeEx(desktopProcessHandle, lviItemMem, 0, Win32.FreeType.Release);
        //        if (lviItemText != IntPtr.Zero)
        //            Win32.VirtualFreeEx(desktopProcessHandle, lviItemText, 0, Win32.FreeType.Release);
        //    }
        //}
    }
}
