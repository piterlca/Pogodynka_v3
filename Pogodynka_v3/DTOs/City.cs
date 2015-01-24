using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogodynka_v3
{
    public class City
    {
        public string cityName;
        public string path;

        public City(string name, string path)
        {
            this.cityName = name;
            this.path = path;
        }
    }
}
