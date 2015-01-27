using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogodynka_v3
{
    public class ModelValue
    {
        public int temperatureValue;
        public string time;

        public ModelValue(int value, string time)
        {
            this.temperatureValue = value;
            this.time = time;
        }
    }
}
