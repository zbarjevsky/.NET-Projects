using System;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace MeditationStopWatch
{
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

    public class OptionsSerializer
    {
        private OptionsSerializer() { }

        public static void Save(string filename, object options)
        {
            try
            {
                Byte[] buffer = new Byte[80];
                MemoryStream ms;
                BinaryFormatter bf = new BinaryFormatter();

                System.Xml.XmlTextWriter xmlwriter =
                   new XmlTextWriter(filename, System.Text.Encoding.UTF8);

                xmlwriter.Formatting = Formatting.Indented;
                xmlwriter.WriteStartDocument();

                xmlwriter.WriteComment("Options File. Do not edit! zbarjevsky@gmail.com");
                xmlwriter.WriteStartElement(options.ToString());

                PropertyInfo[] props = options.GetType().GetProperties(
                   BindingFlags.Public |
                   BindingFlags.Instance |
                   BindingFlags.SetField);

                foreach (PropertyInfo prop in props)
                {
                    xmlwriter.WriteStartElement(prop.Name);

                    object da = prop.GetValue(options, null);

                    if (da != null)
                    {
                        xmlwriter.WriteAttributeString("Value", da.ToString());

                        ms = new MemoryStream();
                        try
                        {
                            bf.Serialize(ms, da);
                            ms.Position = 0;
                            int count = 0;
                            do
                            {
                                count = ms.Read(buffer, 0, buffer.Length);
                                xmlwriter.WriteBase64(buffer, 0, count);
                            } while (count == buffer.Length);
                        }//end try
                        catch (System.Runtime.Serialization.SerializationException err)
                        {
                            System.Diagnostics.Trace.WriteLine(string.Format("SERIALIZATION FAILED: {0}\n {1}", 
                                prop.Name, err));
                        }//end catch

                    }//end if
                    else xmlwriter.WriteAttributeString("Value", "null");

                    xmlwriter.WriteEndElement();
                }//end foreach
                xmlwriter.WriteEndElement();
                xmlwriter.WriteEndDocument();
                xmlwriter.Flush();
                xmlwriter.Close();

            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
            }
        }//end Save

        public static void Load<T>(string filename, T options) where T : new()
        {
            try
            {
                Byte[] buffer = new Byte[80];
                MemoryStream ms;
                BinaryFormatter bf = new BinaryFormatter();

                System.Xml.XmlTextReader reader = new XmlTextReader(filename);

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            if (reader.HasAttributes)
                            {
                                string name = reader.Name;
                                string val = reader.GetAttribute("Value");

                                ms = new MemoryStream();

                                int count = 0;
                                do
                                {
                                    count = reader.ReadBase64(buffer, 0, buffer.Length);
                                    ms.Write(buffer, 0, count);
                                } while (count == buffer.Length);

                                ms.Position = 0;

                                if (val != "null")
                                {
                                    try
                                    {
                                        object da = bf.Deserialize(ms);

                                        System.Diagnostics.Trace.Write("Loading: '" + name + "'...");
                                        options.GetType().GetProperty(name).SetValue(options, da, null);
                                        System.Diagnostics.Trace.WriteLine("OK");
                                    }//end try
                                    catch (System.Runtime.Serialization.SerializationException err)
                                    {
                                        System.Diagnostics.Trace.WriteLine("Failed: " + err.Message);
                                    }//end catch
                                }//end if
                            }//end if
                            break;
                    }//end switch
                }//end while
                reader.Close();
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
            }
        }//end Load
    }//end class OptionsSerializer
}//end namespace DUMeterMZ
