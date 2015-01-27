using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Reflection;

namespace Pogodynka_v3
{
    public class RssReporter : Model
    {
        private string pathToSource;
        private TemperatureRetriever tempRetriever;
        private static string ConfigurationPath = "AvailableCitiesConf.xml";

        public static void InitModels()
        {
            List<City> CitiesAvailable = RssChannelConfiguration.LoadConfiguration(ConfigurationPath);
            foreach (City city in CitiesAvailable)
            {
                models.Add(new RssReporter(city.cityName, city.path));
            }
        }

        public override void measure()
        {
            List<Temperature> results = tempRetriever.getTemperatureFromRss();
            if (results != null)
                parameters = new ModelData(model_ID, results);
        }

        private RssReporter(string name, string path) : base()
        {
            this.model_ID = name;
            pathToSource = path;
            measurePeriodInMiliseconds = 6000;
            tempRetriever = new TemperatureRetriever(pathToSource);
            measuringThread.startMeasuring();
        }
    }
}
