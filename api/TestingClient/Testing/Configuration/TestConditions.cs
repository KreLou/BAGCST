using System;
using System.Collections.Generic;
using System.Text;
using TestingClient.Testing.Configuration.WaitingMethod;

namespace TestingClient.Testing.Configuration
{
    class TestConditions
    {
        /// <summary>
        /// Amount of Iterations
        /// </summary>
        public int Iterations { get; set; }
        

        public IWaitingMethod WaitingMethod { get; set; }
    }
}
