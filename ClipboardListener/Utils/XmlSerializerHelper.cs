using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ClipboardManager.Utils
{
    /// <summary>
    /// A generic class used to serialize objects.
    /// </summary>
    [ComVisible(false)]
    public static class GenericSerializer
    {
        /// <summary>
        /// Serializes the given object.
        /// </summary>
        /// <typeparam name="T">The type of the object to be serialized.</typeparam>
        /// <param name="obj">The object to be serialized.</param>
        /// <returns>String representation of the serialized object.</returns>
        public static string Serialize<T>(T obj)
        {
            XmlSerializer xs = null;
            StringWriter sw = null;
            try
            {
                xs = new XmlSerializer(typeof(T));
                sw = new StringWriter();
                xs.Serialize(sw, obj);
                sw.Flush();
                return sw.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
        public static string Serialize<T>(T obj, Type[] extraTypes)
        {
            XmlSerializer xs = null;
            StringWriter sw = null;
            try
            {
                xs = new XmlSerializer(typeof(T), extraTypes);
                sw = new StringWriter();
                xs.Serialize(sw, obj);
                sw.Flush();
                return sw.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
        /// <summary>
        /// Serializes the given object.
        /// </summary>
        /// <typeparam name="T">The type of the object to be serialized.</typeparam>
        /// <param name="obj">The object to be serialized.</param>
        /// <param name="writer">The writer to be used for output in the serialization.</param>
        public static void Serialize<T>(T obj, XmlWriter writer)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            xs.Serialize(writer, obj);
        }
        /// <summary>
        /// Serializes the given object.
        /// </summary>
        /// <typeparam name="T">The type of the object to be serialized.</typeparam>
        /// <param name="obj">The object to be serialized.</param>
        /// <param name="writer">The writer to be used for output in the serialization.</param>
        /// <param name="extraTypes"><c>Type</c> array
        ///       of additional object types to serialize.</param>
        public static void Serialize<T>(T obj, XmlWriter writer, Type[] extraTypes)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T), extraTypes);
            xs.Serialize(writer, obj);
        }
        /// <summary>
        /// Deserializes the given object.
        /// </summary>
        /// <typeparam name="T">The type of the object to be deserialized.</typeparam>
        /// <param name="reader">The reader used to retrieve the serialized object.</param>
        /// <returns>The deserialized object of type T.</returns>
        public static T Deserialize<T>(XmlReader reader)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            return (T)xs.Deserialize(reader);
        }
        /// <summary>
        /// Deserializes the given object.
        /// </summary>
        /// <typeparam name="T">The type of the object to be deserialized.</typeparam>
        /// <param name="reader">The reader used to retrieve the serialized object.</param>
        /// <param name="extraTypes"><c>Type</c> array
        ///           of additional object types to deserialize.</param>
        /// <returns>The deserialized object of type T.</returns>
        public static T Deserialize<T>(XmlReader reader, Type[] extraTypes)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T), extraTypes);
            return (T)xs.Deserialize(reader);
        }
        /// <summary>
        /// Deserializes the given object.
        /// </summary>
        /// <typeparam name="T">The type of the object to be deserialized.</typeparam>
        /// <param name="XML">The XML file containing the serialized object.</param>
        /// <returns>The deserialized object of type T.</returns>
        public static T Deserialize<T>(string XML)
        {
            if (XML == null || XML == string.Empty)
                return default(T);
            XmlSerializer xs = null;
            StringReader sr = null;
            try
            {
                xs = new XmlSerializer(typeof(T));
                sr = new StringReader(XML);
                return (T)xs.Deserialize(sr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
        public static T Deserialize<T>(string XML, Type[] extraTypes)
        {
            if (XML == null || XML == string.Empty)
                return default(T);
            XmlSerializer xs = null;
            StringReader sr = null;
            try
            {
                xs = new XmlSerializer(typeof(T), extraTypes);
                sr = new StringReader(XML);
                return (T)xs.Deserialize(sr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
        public static void SaveAs<T>(this T Obj, string FileName,
                           Encoding encoding, Type[] extraTypes)
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(FileName));
            if (!di.Exists)
                di.Create();
            XmlDocument document = new XmlDocument();
            XmlWriterSettings wSettings = new XmlWriterSettings();
            wSettings.Indent = true;
            wSettings.Encoding = encoding;
            wSettings.CloseOutput = true;
            wSettings.CheckCharacters = false;
            using (XmlWriter writer = XmlWriter.Create(FileName, wSettings))
            {
                if (extraTypes != null)
                    Serialize<T>(Obj, writer, extraTypes);
                else
                    Serialize<T>(Obj, writer);
                writer.Flush();
                document.Save(writer);
            }
        }
        public static void SaveAs<T>(this T Obj, string FileName, Type[] extraTypes)
        {
            SaveAs<T>(Obj, FileName, Encoding.UTF8, extraTypes);
        }
        public static void SaveAs<T>(this T Obj, string FileName, Encoding encoding)
        {
            SaveAs<T>(Obj, FileName, encoding, null);
        }
        public static void SaveAs<T>(this T Obj, string FileName)
        {
            SaveAs<T>(Obj, FileName, Encoding.UTF8);
        }
        public static T Open<T>(string FileName, Type[] extraTypes)
        {
            T obj = default(T);
            if (File.Exists(FileName))
            {
                XmlReaderSettings rSettings = new XmlReaderSettings();
                rSettings.CloseInput = true;
                rSettings.CheckCharacters = false;
                using (XmlReader reader = XmlReader.Create(FileName, rSettings))
                {
                    reader.ReadOuterXml();
                    if (extraTypes != null)
                        obj = Deserialize<T>(reader, extraTypes);
                    else
                        obj = Deserialize<T>(reader);
                }
            }
            return obj;
        }
        public static T Open<T>(this T obj, string FileName)
        {
            return Open<T>(FileName, null);
        }
    }
}
