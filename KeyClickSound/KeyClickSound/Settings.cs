using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KeyClickSound
{
    [Serializable]
    public class KeyPath
    {
        [XmlAttribute]
        public string Key { get; set; }
        public string Path { get; set; }
    }

    [Serializable]
    public class Settings
    {
        public List<KeyPath> Keys { get; set; } = new List<KeyPath>();

    }
}
