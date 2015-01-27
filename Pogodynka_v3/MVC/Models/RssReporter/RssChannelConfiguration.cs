using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Pogodynka_v3
{
    abstract class RssChannelConfiguration
    {
        public static List<City> LoadConfiguration(string pathToSource)
        {
            List<City> Cities = new List<City>();
            XmlNodeList nodes = tools.XmlTools.getXmlNodes(pathToSource);
            foreach (XmlNode node in nodes)
            {
                Cities.Add(new City(node["City"].InnerText, node["PathToReport"].InnerText));
            }
            return Cities;
        }
    }
}
