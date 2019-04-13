using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestingClient.Testing.Configuration.WaitingMethod
{
    class RandomWaiting: IWaitingMethod
    {
        public int MinDelay { get; set; }
        public int MaxDelay { get; set; }

        public void Wait()
        {
            Random r = new Random();
            int sleepTime = r.Next(this.MinDelay, this.MaxDelay);

            Thread.Sleep(sleepTime);

        }
    }
}
