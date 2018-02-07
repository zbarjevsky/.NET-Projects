using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfAnalogClock.Tools
{
    public static class XmlObjectSerializer<T> where T : class
    {
        public static T Load(string path, bool validate = false, Type[] knownTypes = null)
        {
            T serializedObject = null;
            XmlSerializer serializer = new XmlSerializer(typeof(T), knownTypes);
            using (StreamReader streamReader = new StreamReader(path))
            {
                serializedObject = (T)serializer.Deserialize(streamReader);
            }
            return serializedObject;
        }

        public static void Save(string path, T serializedObject)
        {
            try
            {
                using (StreamWriter streamReader = new StreamWriter(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(streamReader, serializedObject);
                }

                byte[] buffer;
                using (MemoryStream stream = new MemoryStream())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stream, serializedObject);
                    buffer = stream.ToArray();
                }

                File.WriteAllBytes(path, buffer);
            }
            catch (Exception e)
            {
                Debug.Fail(e.ToString());
            }
        }
    }
}
