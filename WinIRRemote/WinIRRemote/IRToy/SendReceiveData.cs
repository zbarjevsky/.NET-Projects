using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinIRRemote.IRToy
{
    using lirc_t = System.Int32;

    public class SendReceiveData
    {
        public bool waitTillDataIsReady(int maxUSecs) { return true; }
        public bool getData(out lirc_t data) { data = 0;  return true; }
       public bool dataReady() { return true; }
    }
}
