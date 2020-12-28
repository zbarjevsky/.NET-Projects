using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BarometerBT.Utils
{
    public class XmlHelper
    {
        public const int BUFFER_SIZE = 1024 * 1024;

        public static T Open<T>(string fileName)
        {
            using (StreamReader streamReader = new StreamReader(fileName, Encoding.UTF8, true, BUFFER_SIZE))
            {
                using (TextReader txt = new StringReader(streamReader.ReadToEnd()))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(txt);
                }
            }
        }

        public static void Save<T>(string fileName, T o)
        {
            using (StreamWriter streamReader = new StreamWriter(fileName, false, Encoding.UTF8, BUFFER_SIZE))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(streamReader, o);
            }
        }
    }
}
