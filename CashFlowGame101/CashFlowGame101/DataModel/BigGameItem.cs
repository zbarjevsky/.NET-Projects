using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowGame101.DataModel
{
    public class BigGameItem : GameItemBase
    {
        public BigGameItem(int row, int col, string name) : base(row, col, name)
        {

        }
    }

    public class BigGameCashFlowDay : BigGameItem
    {
        public BigGameCashFlowDay(int row, int col, string name) : base(row, col, name)
        {

        }
    }

    public class BigGameDream : BigGameItem
    {
        public BigGameDream(int row, int col, string name) : base(row, col, name)
        {

        }
    }

    public class BigGameAsset : BigGameItem
    {
        public BigGameAsset(int row, int col, string name) : base(row, col, name)
        {

        }
    }
}
