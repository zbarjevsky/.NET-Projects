using System;
using System.Collections.Generic;
using System.Text;

namespace SPUD
{
  public class Module
  {
    public static int Start_row = 3;        //starting row number for cam list in spreadsheet
    public static bool RefreshInProgress;
    public static string launchdir;         //program start directory
    public static bool ShowEdit;            //True to automatically show edits to cam records
    public static int totalcams;
    public static int totalvalidcams;
    public static int maxcol;
  }
}
