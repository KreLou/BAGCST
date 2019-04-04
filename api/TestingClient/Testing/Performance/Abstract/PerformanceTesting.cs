using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestingClient.Testing.Configuration;
using TestingClient.Testing.Handlers;

namespace TestingClient.Testing.Performance.Abstract
{
    class PerformanceTesting
    {
        private readonly string RequestURL;

        public RequestResponseInformation[] ResponseInformation { get; private set; }

        public TestConditions TestConditions { get; set; }

        public AuditTestResults TestResult { get; set; }

        public PerformanceTesting(string url)
        {
            this.RequestURL = url;

            this.TestConditions = new TestConditions { Iterations = 10, WaitingMethod = new StaticWaiting { WaitingDelay= 300 } };
        }

        public async Task PerfomTest()
        {
            Console.WriteLine("Start Test");

            List<RequestResponseInformation> listResponseInformation = new List<RequestResponseInformation>();

            for (int round = 1; round <= TestConditions.Iterations; round++)
            {
                Console.SetCursorPosition(0, Console.CursorTop -1);
                Console.WriteLine($"[{this.GetType().Name}] Run Test {round} of {TestConditions.Iterations}");

                HttpRequest request = new HttpRequest(RequestURL);
                await request.Run();


                listResponseInformation.Add(request.ResponseInformation);

                handleWaiting();
            }

            ResponseInformation = listResponseInformation.ToArray();


            handleAudit();
        }

        private void handleAudit()
        {
            AuditResponseGenerator generator = new AuditResponseGenerator(this.ResponseInformation);

            this.TestResult = generator.TestResult;
        }

        private void handleWaiting()
        {
            int delay = 0;
            if (this.TestConditions.WaitingMethod is RandomWaiting)
            {
                RandomWaiting config = (RandomWaiting)this.TestConditions.WaitingMethod;
                Random r = new Random();

                delay = r.Next(config.MinDelay, config.MaxDelay);
            }else
            {
                delay = ((StaticWaiting)this.TestConditions.WaitingMethod).WaitingDelay;
            }
            Thread.Sleep(delay);
        }
    }
}
