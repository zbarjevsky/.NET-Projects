using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFiConnect.MkZ
{
    public interface ILog
    {
        void Log(string format, params object[] args);
    }
}
