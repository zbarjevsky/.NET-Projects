using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace SPUD
{
  class spud
  {
    //Import MioMap spud file
    public static List<SpeedCam> Import_MioMap(string sFileName)
    {
      FileStream fs = File.OpenRead(sFileName);
      BinaryReader br = new BinaryReader(fs);

      List<SpeedCam> listSpeedCam = new List<SpeedCam>();
      try
      {
        while (fs.Position<fs.Length-10)
        {
          SpeedCam sp = SpeedCam.Read(br);
          listSpeedCam.Add(sp);
        }
      }//end try
      catch (EndOfStreamException err)
      {
        System.Diagnostics.Debug.WriteLine("Import_MioMap Error: " + err.Message);
      }//end catch

      return listSpeedCam;
    }

    public static void Save_MioMap(BindingList<SpeedCam> list, string sFileName, bool bNotSaveDeleted)
    {
      using (BinaryWriter w = new BinaryWriter(File.Open(sFileName, FileMode.Create)))
      {
        foreach (SpeedCam sp in list)
        {
          if (bNotSaveDeleted && sp.sFlag == SpeedCam.RecordTypes.Deleted)
            continue;

          SpeedCam.Write(w, sp);
        }//end foreach
      }
    }

    internal static List<SpeedCam> Import_CSV(string sFileName)
    {
        List<SpeedCam> listSpeedCam = new List<SpeedCam>();
        string[] svLines = File.ReadAllLines(sFileName);
        foreach (var sLine in svLines)
        {
            if (sLine.StartsWith("X"))
                continue;
            
            SpeedCam sp = SpeedCam.ReadCSV(sLine);
            listSpeedCam.Add(sp);
        }
        return listSpeedCam;
    }
  }
}
