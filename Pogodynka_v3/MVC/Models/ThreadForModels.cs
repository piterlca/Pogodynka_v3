using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pogodynka_v3
{
    public class ThreadForModels
    {
        Model ThreadOwner;
         int measurePeriodInMiliseconds;
        public ThreadForModels(Model ModelThatOwnsTheThread, int measurePeriod)
        {
            this.measurePeriodInMiliseconds = measurePeriod;
            this.ThreadOwner = ModelThatOwnsTheThread;
        }

        public void startMeasuring()
        {
            System.Threading.Thread thread = new System.Threading.Thread(threadAction);
            thread.IsBackground = true;
            thread.Start();
        }

        private void threadAction()
        {
            while (true)
            {
                ThreadOwner.ModelDataGetCycle();
                Thread.Sleep(measurePeriodInMiliseconds);
            }
        }
    }
}
