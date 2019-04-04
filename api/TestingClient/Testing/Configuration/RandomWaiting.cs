using System;
using System.Collections.Generic;
using System.Text;

namespace TestingClient.Testing.Configuration
{
    class RandomWaiting: IWaitingMethod
    {
        public int MinDelay { get; set; }
        public int MaxDelay { get; set; }
    }
}
