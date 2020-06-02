using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowGame101.DataModel
{
    public class RatRaceItem : GameItemBase
    {
        public RatRaceItem(int row, int col, string name) : base(row, col, name)
        {

        }
    }

    public class RatRacePayDay : RatRaceItem
    {
        public RatRacePayDay(int row, int col, string name) : base(row, col, name)
        {

        }
    }

    public class RatRaceCharity : RatRaceItem
    {
        public RatRaceCharity(int row, int col, string name) : base(row, col, name)
        {

        }
    }

    public class RatRaceDeal : RatRaceItem
    {
        public RatRaceDeal(int row, int col, string name) : base(row, col, name)
        {

        }
    }

    public class RatRaceChild : RatRaceItem
    {
        public RatRaceChild(int row, int col, string name) : base(row, col, name)
        {

        }
    }

    public class RatRaceOpportunity : RatRaceItem
    {
        public RatRaceOpportunity(int row, int col, string name) : base(row, col, name)
        {

        }
    }

    public class RatRaceLiability : RatRaceItem
    {
        public RatRaceLiability(int row, int col, string name) : base(row, col, name)
        {

        }
    }
}
