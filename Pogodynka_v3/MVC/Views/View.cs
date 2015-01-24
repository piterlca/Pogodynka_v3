using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogodynka_v3
{
    public abstract class View
    {
        public abstract bool isModelBeingViewed(string modelID);


        public abstract void delDataFromView(string modelID);
        public abstract void updateView(ModelData Parameters);

    }
}
