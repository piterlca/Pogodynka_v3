using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;

namespace Pogodynka_v3
{
    class ReadFromRss : Model
    {
        
        private string pathToSource;

        public static void InitModels()
        {
            List<City> CitiesAvailable = LoadConfiguration("AvailableCitiesConf.xml");
            foreach (City city in CitiesAvailable)
            {
                models.Add(new ReadFromRss(city.cityName, city.path));
            }
        }

        protected override void measure()
        {
            //object LastestData;
            List<Temperature> results = getTemperatureFromRss();
            if (results != null)
                parameters = new ModelData(model_ID, results);

        }

        private ReadFromRss(string name, string path)
        {
            this.model_ID = name;
            pathToSource = path;
            subscribers = new List<View>();
            measurePeriodInMiliseconds = 50;
            Thread thread = new Thread(threadAction);
            thread.IsBackground = true;
            thread.Start();
        }

        private static List<City> LoadConfiguration(string ConfigurationPath)
        {
            List<City> Cities = new List<City>();
            XmlNodeList nodes = tools.XmlTools.getXmlNodes(ConfigurationPath);
            foreach (XmlNode node in nodes)
            {
                Cities.Add(new City(node["City"].InnerText, node["PathToReport"].InnerText));
            }
            return Cities;
        }

        private List<Temperature> getTemperatureFromRss()
        {
            WebResponse response = Tools.WebTools.RequestConnection(pathToSource);
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
