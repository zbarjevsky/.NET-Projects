using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyClickSound
{
    public class KeyPath
    {
        public string Key { get; set; }
        public string Path { get; set; }
    }

    internal class Settings
    {
        public List<KeyPath> Keys { get; set; }

    }
}
