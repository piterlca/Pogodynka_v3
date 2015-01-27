using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogodynka_v3
{
    public class SubscribersManager
    {
        private List<View> subscribers;
        public SubscribersManager()
        {
            subscribers = new List<View>();
        }
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
        public List<View> getAllSubscribers()
        {
            return subscribers;
        }

    }
}
