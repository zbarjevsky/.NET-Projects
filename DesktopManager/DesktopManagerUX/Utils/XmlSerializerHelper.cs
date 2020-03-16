using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DesktopManagerUX.Utils
{
    class XmlSerializerHelper
    {
    }

    public class XmlHelper
    {
        public static T Open<T>(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                return (T)serializer.Deserialize(streamReader);
            }
        }

        public static void Save<T>(string fileName, T o)
        {
            using (StreamWriter streamReader = new StreamWriter(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(streamReader, o);
            }
        }
    }

    public class SerializerHelper
    {
        public static T Open<T>(string fileName) where T : class, new()
        {
            try
            {
                return XmlHelper.Open<T>(fileName);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
                return new T();
            }
        }

        public static void Save<T>(string fileName, T o)
        {
            try
            {
                XmlHelper.Save(fileName, o);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
            }
        }
    }


}
