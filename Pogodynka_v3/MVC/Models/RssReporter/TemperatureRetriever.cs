using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace Pogodynka_v3
{
    public class TemperatureRetriever
    {
        private string path;

        public TemperatureRetriever(string path)
        {
            this.path = path;
        }
        public List<Temperature> getTemperatureFromRss()
        {
            WebResponse response = Tools.WebTools.RequestConnection(path);
            if (response == null)
            {
                return null;
            }
            Stream stream = response.GetResponseStream();
            XmlNodeList nodes = tools.XmlTools.getXmlNodes(stream);
            List<Temperature> temperaturesInCity = new List<Temperature>();

            foreach (XmlNode node in nodes)
            {
                temperaturesInCity.Add(new Temperature(int.Parse(this.readTemperatureFromString(node["description"].InnerText)),
                node["title"].InnerText));
            }
            return temperaturesInCity;
        }

        private string readTemperatureFromString(string text)
        {
            States state = States.WHITE;

            string temperatura = null;

            foreach (char sign in text)
            {
                switch (state) //***przejscia***//
                {
                    case States.WHITE:
                        if (sign <= '9' && sign >= '0')
                            state = States.TEMPERATURE;
                        else if (sign == '&')
                            state = States.MARK;
                        else if (sign == '-')
                            state = States.MINUS;
                        break;

                    case States.MINUS:
                        if (sign <= '9' && sign >= '0' || sign == '-')
                        {
                            temperatura += '-';
                            state = States.TEMPERATURE;
                        }
                        else
                        {
                            state = States.WHITE;
                        }
                        break;

                    case States.TEMPERATURE:
                        if (sign <= '9' && sign >= '0') state = States.TEMPERATURE;
                        else if (sign == '&')
                            state = States.MARK;
                        else if (sign == '-')
                            state = States.MINUS;
                        else
                            state = States.WHITE;
                        break;

                    case States.MARK:
                        if (sign <= '9' && sign >= '0')
                        {
                            temperatura = null;
                            state = States.TEMPERATURE;
                        }
                        if (sign == 'd')
                            state = States.Deg;
                        else if (sign == '-')
                            state = States.MINUS;
                        else
                            state = States.WHITE;
                        break;

                    case States.Deg:
                        if (sign <= '9' && sign >= '0')
                        {
                            temperatura = null;
                            state = States.TEMPERATURE;
                        }
                        if (sign == 'e')
                            state = States.dEg;
                        else if (sign == '-')
                            state = States.MINUS;
                        else
                            state = States.WHITE;
                        break;

                    case States.dEg:
                        if (sign <= '9' && sign >= '0')
                        {
                            temperatura = null;
                            state = States.TEMPERATURE;
                        }
                        if (sign == 'g')
                            state = States.STORE;
                        else if (sign == '-')
                            state = States.MINUS;
                        else
                            state = States.WHITE;
                        break;

                }
                switch (state) //***akcje dla stanow***//
                {
                    case States.TEMPERATURE:
                        temperatura += sign;
                        break;

                    case States.STORE:
                        return temperatura;
                }
            }
            return null;
        }
        private enum States { TEMPERATURE, WHITE, MARK, Deg, dEg, STORE, MINUS };

    }
}
