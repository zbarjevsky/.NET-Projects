using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MkZ.Tools
{
    public class XmlHelper
    {
        public const int BUFFER_SIZE = 128 * 1024;

        public static T Open<T>(string fileName) where T : class, new()
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8, true, BUFFER_SIZE))
                {
                    using (TextReader txt = new StringReader(reader.ReadToEnd()))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        return (T)serializer.Deserialize(txt);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error opening: {fileName}, Error: {ex.Message}");
                return null;
            }        
        }

        public static void Save<T>(string fileName, T o) where T : class, new()
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8, BUFFER_SIZE))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, o);
            }
        }
    }
}
