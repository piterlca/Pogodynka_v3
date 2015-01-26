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
        protected int measurePeriodInMiliseconds;
        protected ModelData parameters;
        protected List<View> subscribers; 
        protected static List<Model> models = new List<Model>();


        protected abstract void measure();

        public void addSubscriber(View view)
        {
            lock (Globals.CriticalSection)
            {
                subscribers.Add(view);
            }
        }

        public void delSubscriber(View view)
        {
            lock (Globals.CriticalSection)
            {
                subscribers.Remove(view);
            }
        }

        public static List<string> getAvailableModels()
        {
            List<string> modelNames = new List<string>();
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

        protected void NotifySubscribers()
        {
            lock (Globals.CriticalSection)
            {
                foreach (View view in subscribers)
                {
                    view.updateView(parameters);
                }
            }
        }

        protected void threadAction()
        {
            while (true)
            {
                measure();
                if (parameters != null)
                {
                    NotifySubscribers();
                }
                Thread.Sleep(measurePeriodInMiliseconds);
            }
        }
    }
}
