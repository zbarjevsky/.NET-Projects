using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.Physics
{
    public interface IDataPoint
    {
        bool IsValid { get; }
        DateTime Date { get; }
        double GetValue<T>(IUnitBase<T> measurementType) where T : struct, IConvertible;
    }
}
