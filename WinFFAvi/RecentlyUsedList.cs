using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WinFFAvi
{
  public class RecentlyUsedList
  {
    const string registryPath = "Software\\WinFFAvi\\FolderList";  // Registry path to keep persistent data

    public static int LoadFromRegistry(List<string> list)
    {
      int iSelectedIdx = 0;
      if (registryPath != null)
      {
        RegistryKey regKey = Registry.CurrentUser.OpenSubKey(registryPath);
        if (regKey != null)
        {
          int numEntries = (int)regKey.GetValue("Count", 0);
          iSelectedIdx = (int)regKey.GetValue("SelectedIndex", 0);

          for (int i = 0; i < numEntries; i++)
          {
            string filename = (string)regKey.GetValue("File" + i.ToString());
            if (filename != null)
              list.Add(filename);
          }

          regKey.Close();
        }
      }
      return iSelectedIdx;
    }

    public static void SaveToRegistry(List<string> list, int iSelectedIdx)
    {
      if (registryPath != null)
      {
        RegistryKey regKey = Registry.CurrentUser.CreateSubKey(registryPath);
        if (regKey != null)
        {
          regKey.SetValue("Count", list.Count);
          regKey.SetValue("SelectedIndex", iSelectedIdx);

          for (int i = 0; i < list.Count; i++)
          {
            regKey.SetValue("File" + i.ToString(), list[i]);
          }

          regKey.Close();
        }
      }
    }

    public static int LoadFromRegistry(ComboBox cmb)
    {
      //load list of last directories
      cmb.Items.Clear();
      cmb.Items.Add("."); //use same folder
      List<string> list = new List<string>(4);
      int iSelectedIdx = RecentlyUsedList.LoadFromRegistry(list);
      foreach (string s in list)
      {
        cmb.Items.Add(s);
      }
      cmb.SelectedIndex = iSelectedIdx;
      return cmb.Items.Count;
    }

    public static void SaveToRegistry(ComboBox cmb)
    {
      List<string> list = new List<string>(10);
      foreach (string s in cmb.Items)
      {
        if (s != ".")
          list.Add(s);
      }
      RecentlyUsedList.SaveToRegistry(list, cmb.SelectedIndex);
    }

  }//end class RecentlyUsedList
}//end namespace WinFFAvi
