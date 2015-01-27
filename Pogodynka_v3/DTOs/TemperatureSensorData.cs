using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogodynka_v3
{
    public class TemperatureSensorData : ModelData
    {
        public TemperatureSensorData(string ID, List<ModelValues> values)
        {
            this.senderID = ID;
            this.modelValues = new List<ModelValues>(values);
        }
    }
}
