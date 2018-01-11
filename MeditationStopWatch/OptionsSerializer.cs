using System;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MeditationStopWatch
{
	public class OptionsSerializer
	{
		private OptionsSerializer() { }

		public static void Save(string filename, object options)
		{
			Byte[] buffer = new Byte[80];
			MemoryStream ms;
			BinaryFormatter bf = new BinaryFormatter();

			System.Xml.XmlTextWriter xmlwriter =
			   new XmlTextWriter(filename, System.Text.Encoding.UTF8);

			xmlwriter.Formatting = Formatting.Indented;
			xmlwriter.WriteStartDocument();

			xmlwriter.WriteComment("Option File. Do not edit! zbarjevsky@gmail.com");
			xmlwriter.WriteStartElement(options.ToString());

			PropertyInfo[] props = options.GetType().GetProperties(
			   BindingFlags.Public |
			   BindingFlags.Instance |
			   BindingFlags.SetField);

			foreach ( PropertyInfo prop in props )
			{
				xmlwriter.WriteStartElement(prop.Name);

				object da = prop.GetValue(options, null);

				if ( da != null )
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
						}while ( count == buffer.Length );
					}//end try
					catch ( System.Runtime.Serialization.SerializationException )
					{
						System.Diagnostics.Trace.WriteLine("SERIALIZATION FAILED: {0}", prop.Name);
					}//end catch

				}//end if
				else xmlwriter.WriteAttributeString("Value", "null");

				xmlwriter.WriteEndElement();
			}//end foreach
			xmlwriter.WriteEndElement();
			xmlwriter.WriteEndDocument();
			xmlwriter.Flush();
			xmlwriter.Close();
		}//end Save

		public static void Load(string filename, object options)
		{
			Byte[] buffer = new Byte[80];
			MemoryStream ms;
			BinaryFormatter bf = new BinaryFormatter();

			System.Xml.XmlTextReader reader = new XmlTextReader(filename);

			while ( reader.Read() )
			{
				switch ( reader.NodeType )
				{
					case XmlNodeType.Element:

						if ( reader.HasAttributes )
						{
							string name = reader.Name;
							string val = reader.GetAttribute("Value");

							ms = new MemoryStream();

							int count = 0;
							do
							{
								count = reader.ReadBase64(buffer, 0, buffer.Length);
								ms.Write(buffer, 0, count);
							}while ( count == buffer.Length );

							ms.Position = 0;

							if ( val != "null" )
							{
								try
								{
									object da = bf.Deserialize(ms);

									System.Diagnostics.Trace.Write("Loading: '" + name + "'...");
									options.GetType().GetProperty(name).SetValue(options, da, null);
									System.Diagnostics.Trace.WriteLine("OK");
								}//end try
								catch ( System.Runtime.Serialization.SerializationException err )
								{
									System.Diagnostics.Trace.WriteLine("Failed: " + err.Message);
								}//end catch
							}//end if
						}//end if
						break;
				}//end switch
			}//end while
			reader.Close();
		}//end Load
	}//end class Options
}//end namespace DUMeterMZ
