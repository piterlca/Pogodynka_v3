using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pogodynka_v3
{
    abstract class Model
    {
        protected string model_ID;
        protected ModelData parameters;
        public abstract void measure();

        protected List<View> subscribers;
        public void addSubscriber(View view)
        {
            subscribers.Add(view);
        }
        public void delSubscriber(View view)
        {
            subscribers.Remove(view);
        }
        protected void NotifySubscribers()
        {
            foreach (View view in subscribers)
            {
                view.updateView(parameters);
            }
        }

        protected static List<Model> models = new List<Model>();
        public static List<string> getAvailableModels()
        {
            List<string>  modelNames = new List<string>();
            foreach (Model model in models)
            {
                modelNames.Add(model.model_ID);
            }
            return modelNames;

        }
        public static Model identifyModel(string name)
        {
            foreach (Model model in models)
            {
                if (model.model_ID == name)
                {
                    return model;
                }
            }

            return null;
        }


    }
}
