using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ClipboardManager.DesktopUtil
{
    internal class Storage
    {
        public void SaveIconPositions(IEnumerable<NamedDesktopPoint> iconPositions, IDictionary<string, string> registryValues)
        {
            var xDoc = new XDocument(
                new XElement("Desktop",
                    new XElement("Icons",
                        iconPositions.Select(p => new XElement("Icon",
                            new XAttribute("x", p.X),
                            new XAttribute("y", p.Y),
                            new XText(p.Name)))),
                    new XElement("Registry",
                        registryValues.Select(p => new XElement("Value",
                            new XElement("Name", new XCData(p.Key)),
                            new XElement("Data", new XCData(p.Value)))))));

                string fileName = GetNewFileName();
                if (File.Exists(fileName))
                { File.Delete(fileName); }

                using (var stream = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    using (var writer = XmlWriter.Create(stream))
                    {
                        xDoc.WriteTo(writer);
                    }
                    stream.Flush();
                    stream.Close();
                }
        }

        public IEnumerable<NamedDesktopPoint> GetIconPositions()
        {
                string fileName = GetLatestEistingFileName();
                if (File.Exists(fileName) == false)
                { return new NamedDesktopPoint[0]; }

                using (var stream = new FileStream(fileName, FileMode.Open))
                {
                    using (var reader = XmlReader.Create(stream))
                    {
                        var xDoc = XDocument.Load(reader);

                        return xDoc.Root.Element("Icons").Elements("Icon")
                            .Select(el => new NamedDesktopPoint(el.Value, int.Parse(el.Attribute("x").Value), int.Parse(el.Attribute("y").Value)))
                            .ToArray();
                    }
                }
        }

        public IDictionary<string, string> GetRegistryValues()
        {
            using (var storage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                if (storage.FileExists("Desktop") == false)
                { return new Dictionary<string, string>(); }

                using (var stream = storage.OpenFile("Desktop", FileMode.Open))
                {
                    using (var reader = XmlReader.Create(stream))
                    {
                        var xDoc = XDocument.Load(reader);

                        return xDoc.Root.Element("Registry").Elements("Value")
                            .ToDictionary(el => el.Element("Name").Value, el => el.Element("Data").Value);
                    }
                }
            }
        }

        private string GetNewFileName()
        {
            string userDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            userDataFolder = Path.Combine(userDataFolder, "ClipboardMZ");
            Directory.CreateDirectory(userDataFolder);
            string fileName = string.Format("DesktopIcons_{0}.xml", DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss"));
            fileName = Path.Combine(userDataFolder, fileName);
            return fileName;
        }

        private string[] GetExistingFiles()
        {
            string userDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            userDataFolder = Path.Combine(userDataFolder, "ClipboardMZ");
            string[] fileNames = Directory.GetFiles(userDataFolder, "DesktopIcons_*.xml");
            fileNames = fileNames.OrderBy(s => s).ToArray();
            return fileNames;
        }

        private string GetLatestEistingFileName()
        {
            string[] fileNames = GetExistingFiles();
            return fileNames.Last();
        }
    }
}
