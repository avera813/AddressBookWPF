using System.IO;
using System.Text;
using System.Xml;

namespace AddressBookWPF
{
    class ReaderWriter
    {
        private ReaderWriter() { }

        public static XmlDocument GetXmlDocument(string fileName)
        {
            XmlDocument xmlDoc;
            try
            {
                using (FileStream file = CheckFile.GetReadFileStream(@fileName))
                {
                    XmlTextReader reader = new XmlTextReader(file);
                    xmlDoc = new XmlDocument();

                    if (reader.MoveToContent() == XmlNodeType.Element && reader.Name == "Addresses")
                    {
                        xmlDoc.LoadXml(reader.ReadOuterXml());
                    }

                    reader.Close();
                    return xmlDoc;
                }
            }
            catch (XmlException ex)
            {
                throw new XmlException("Invalid Xml - Line: " + ex.LineNumber + ", Position: " + ex.LinePosition);
            }
        }

        public static void CreateXmlDocument(string fileName)
        {
            FileStream file = CheckFile.GetWriteFileStream(@fileName);
            XmlTextWriter writer = new XmlTextWriter(file, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Addresses");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        public static void WriteXmlToDocument(string fileName, XmlDocument xmlDoc)
        {
            using (FileStream file = CheckFile.GetWriteFileStream(@fileName))
            {
                XmlTextWriter writer = new XmlTextWriter(file, Encoding.UTF8);
                xmlDoc.WriteTo(writer);
                writer.Close();
            }
        }
    }
}
