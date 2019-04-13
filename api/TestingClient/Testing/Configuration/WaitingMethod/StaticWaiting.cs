using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestingClient.Testing.Configuration.WaitingMethod
{
    class StaticWaiting: IWaitingMethod
    {
        public int WaitingDelay { get; set; }

        public void Wait()
        {
            Thread.Sleep(this.WaitingDelay);
        }
    }
}
