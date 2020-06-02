using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CashFlowGame101.DataModel
{
    public class GameItemBase
    {
        public int Row { get; set; }
        
        public int Col { get; set; }

        public Point PositionInCell { get; set; }

        public string Name { get; set; } = "N/A";

        public GameItemBase(int row, int col, string name)
        {

        }
    }
}
