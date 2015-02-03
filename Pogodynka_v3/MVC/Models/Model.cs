using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pogodynka_v3
{
    public abstract class Model
    {
        protected string model_ID;
        protected int measurePeriodInMiliseconds;
        protected ModelData parameters;
        public SubscribersManager SubsManager;
        protected static List<Model> models = new List<Model>();
        protected ThreadForModels measuringThread;

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

        public Model()
        {
            SubsManager = new SubscribersManager();
            measuringThread = new ThreadForModels(this, measurePeriodInMiliseconds);
        }

        public void requestLatestData(View viewToNotify)
        {
            List<View> viewsToNotify = new List<View>();
            viewsToNotify.Add(viewToNotify);
            NotifySubscribers(viewsToNotify);
        }

        public void ModelDataGetCycle()
        {
            measure();
            NotifySubscribers();
        }
        protected abstract void measure();

        public void NotifySubscribers()
        {
            if (parameters == null) return;
            foreach (View view in SubsManager.getAllSubscribers())
            {
                view.updateView(parameters);
            }
        }
        protected void NotifySubscribers(List<View> subscribers)
        {
            if (parameters == null) return;
            foreach (View view in subscribers)
            {
                view.updateView(parameters);
            }
        }












    }
}
