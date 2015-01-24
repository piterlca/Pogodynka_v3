using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogodynka_v3
{
    public class ModelData
    {
        public string senderID;
        public List<Temperature> temperatureRecords;

        public ModelData(string ID, List<Temperature> values)
        {
            this.senderID = ID;
            this.temperatureRecords = new List<Temperature>(values);
        }
    }
}
