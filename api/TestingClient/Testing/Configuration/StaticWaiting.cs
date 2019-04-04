using System;
using System.Collections.Generic;
using System.Text;

namespace TestingClient.Testing.Configuration
{
    class StaticWaiting: IWaitingMethod
    {
        public int WaitingDelay { get; set; }
    }
}
