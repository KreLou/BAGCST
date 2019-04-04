using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;

namespace TestingClient.Testing.Handlers
{
    class AuditResponseGenerator
    {
        private RequestResponseInformation[] ResponseInformation;

        public AuditTestResults TestResult { get; private set; }

        public AuditResponseGenerator(RequestResponseInformation[] responseInformation)
        {
            ResponseInformation = responseInformation;

            HttpStatusCode[] statusCodes = ResponseInformation.Select(x => x.StatusCode).ToArray();

            this.TestResult = new AuditTestResults
            {
                RunningTestTitle = this.GetType().Name
            };


            handleDurationTime();
        }


        private void handleDurationTime()
        {
            long averageMillisecods = (long)ResponseInformation.Average(x => x.ElapsedMiliseconds);
            long min = this.ResponseInformation.Min(x => x.ElapsedMiliseconds);
            long max = this.ResponseInformation.Max(x => x.ElapsedMiliseconds);

            TestResult.AverageTimeMilliseconds = averageMillisecods;
            TestResult.MinTimeMilliseconds = min;
            TestResult.MaxTimeMilliseconds = max;

        }
    }
}
