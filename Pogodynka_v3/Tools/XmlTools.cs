using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
namespace Pogodynka_v3.tools
{
    abstract class XmlTools
    {
        public static XmlNodeList getXmlNodes(Stream stream)
        {
            XmlTextReader reader = new XmlTextReader(stream);
            reader.XmlResolver = null;
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            XmlNodeList nodes = doc.GetElementsByTagName("item");
            return nodes;
        }

        public static XmlNodeList getXmlNodes(string path)
        {
            XmlTextReader reader = new XmlTextReader(path);
            reader.XmlResolver = null;
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            XmlNodeList nodes = doc.GetElementsByTagName("item");
            return nodes;
        }
    }
}
