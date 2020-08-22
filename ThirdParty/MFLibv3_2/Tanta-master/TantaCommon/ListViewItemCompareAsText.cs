using System;
using System.Collections;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// +------------------------------------------------------------------------------------------------------------------------------+
/// ¦                                                   TERMS OF USE: MIT License                                                  ¦
/// +------------------------------------------------------------------------------------------------------------------------------¦
/// ¦Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation    ¦
/// ¦files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy,    ¦
/// ¦modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software¦
/// ¦is furnished to do so, subject to the following conditions:                                                                   ¦
/// ¦                                                                                                                              ¦
/// ¦The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.¦
/// ¦                                                                                                                              ¦
/// ¦THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE          ¦
/// ¦WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR         ¦
/// ¦COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,   ¦
/// ¦ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                         ¦
/// +------------------------------------------------------------------------------------------------------------------------------+

namespace TantaCommon
{
    /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
    /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
    /// <summary>
    /// A class to act as a sorter for a list view item. Just sorts by text
    /// 
    /// Adapted from: https://msdn.microsoft.com/en-us/library/ms996467.aspx
    /// This is a pretty simple example. There are more sophisticated ones 
    /// around.
    /// 
    /// </summary>
    /// <history>
    ///    01 Nov 18  Cynic - Started
    /// </history>
    public class ListViewItemCompareAsText : IComparer
    {
        private int _col1 = 0, _col2 = 0;
        private bool _bAscending = true;

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Constructor
        /// </summary>
        /// <history>
        ///    01 Nov 18  Cynic - Started
        /// </history>
        public ListViewItemCompareAsText()
        {
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="column">the column to sort on</param>
        /// <history>
        ///    01 Nov 18  Cynic - Started
        /// </history>
        public void SetSortingColumn(int column, int secondaryColumn = -1)
        {
            if (_col1 == column)
                _bAscending = !_bAscending;
            else
                _bAscending = true;

            _col1 = column;
            _col2 = secondaryColumn;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// The comparitor
        /// </summary>
        /// <param name="column">the column to sort on</param>
        /// <history>
        ///    01 Nov 18  Cynic - Started
        /// </history>
        public int Compare(object x, object y)
        {
            if (_bAscending)
                return CompareImpl(x, y);
            return CompareImpl(y, x);
        }

        private int CompareImpl(object x, object y)
        {
            int returnVal = -1;
            ListViewItem itmX = x as ListViewItem;
            ListViewItem itmY = y as ListViewItem;

            string strX = itmX.SubItems[_col1].Text;
            string strY = itmY.SubItems[_col1].Text;
            returnVal = String.Compare(strX, strY);

            if (returnVal == 0 && _col2 >= 0 && _col2 != _col1) //equals
            {
                strX = itmX.SubItems[_col2].Text;
                strY = itmY.SubItems[_col2].Text;
                returnVal = String.Compare(strX, strY);
            }

            return returnVal;
        }
    }
}
