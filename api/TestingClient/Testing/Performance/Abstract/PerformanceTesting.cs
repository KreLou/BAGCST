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
using TestingClient.Testing.Configuration.WaitingMethod;
using TestingClient.Testing.Reporting;

namespace TestingClient.Testing.Performance.Abstract
{
    class PerformanceTesting
    {
        private readonly string RequestURL;

        public RequestResponseInformation[] ResponseInformation { get; private set; }

        public TestConditions TestConditions { get; set; }

        public PerformanceTesting(string url)
        {
            this.RequestURL = url;

            //Set Default.
            this.TestConditions = new TestConditions { Iterations = 10, WaitingMethod = new StaticWaiting { WaitingDelay= 300 } };
        }

        public async Task PerfomTest()
        {
            Console.WriteLine("Use WaitingMethod: " + this.TestConditions.WaitingMethod.GetType().Name);
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

        }

        public TestReport generateTestReport()
        {
            TestReportGenerator generator = new TestReportGenerator(this.GetType().Name,  this.ResponseInformation);

            return generator.getTestReport();
        }

        private void handleWaiting()
        {
            this.TestConditions.WaitingMethod.Wait();
        }
    }
}
