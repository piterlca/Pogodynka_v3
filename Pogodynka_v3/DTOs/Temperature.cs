using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogodynka_v3
{
    public class Temperature
    {
        public int temperatureValue;
        public string time;

        public Temperature(int temperature, string time)
        {
            this.temperatureValue = temperature;
            this.time = time;
        }
    }
}
