using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SPUD
{
  internal class SpeedCamComparer : System.Collections.IComparer
  {
    public static int m_sortColumn = 0;
    public static SortOrder m_sortOrder = SortOrder.Descending;

    public SpeedCamComparer(int iSortColumn)
    {
      if (m_sortColumn == iSortColumn)
      {
        m_sortOrder = (SortOrder)(((int)m_sortOrder + 1) % 3);
      }
      else
      {
        m_sortOrder = SortOrder.Ascending;
      }
      m_sortColumn = iSortColumn;
    }

    public int Compare(object x, object y)
    {
      SpeedCam c1 = x as SpeedCam;
      SpeedCam c2 = y as SpeedCam;

      int res = 0;
      int col = (m_sortOrder == SortOrder.None) ? 0 : m_sortColumn;

      res = c1.GetColumnValue(col).CompareTo(c2.GetColumnValue(col));

      //if equals - enforce order of comparing
      if ( res == 0 )
        res = c1.Index.CompareTo(c2.Index);

      if (res == 0)
        res = c1.Flag.CompareTo(c2.Flag);

      if (res == 0)
        res = c1.Longtitude.CompareTo(c2.Longtitude);

      
      if (m_sortOrder == SortOrder.Descending)
        return -res;

      return res;
    }
  }
}