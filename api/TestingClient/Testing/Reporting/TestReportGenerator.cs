using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace TestingClient.Testing.Reporting
{
    class TestReportGenerator
    {
        private RequestResponseInformation[] ResponseInformation;

        public TestReport TestReport { get; private set; }

        public TestReportGenerator(string testname, RequestResponseInformation[] responseInformation)
        {
            ResponseInformation = responseInformation;
            string user = Environment.UserName;

            TestReport = new TestReport
            {
                TestName = testname,
                TestRunAt = DateTime.Now,
                TestUser = user
            };



            handleStatusCodes();
            handleDurationTime();
            handleContent();
        }

        private void handleContent()
        {
            foreach (RequestResponseInformation info in ResponseInformation)
            {
                var body = info.HttpContent.ReadAsStringAsync().Result;
                TestReport.addContentLength(body.Length);
            }
        }

        private void handleStatusCodes()
        {
            TestReport.StatusCodeReporter = new HttpStatusCodeReporter(ResponseInformation.Select(x => x.StatusCode).ToArray());
        }

        private void handleDurationTime()
        {
            long averageMillisecods = (long)ResponseInformation.Average(x => x.ElapsedMiliseconds);
            long min = this.ResponseInformation.Min(x => x.ElapsedMiliseconds);
            long max = this.ResponseInformation.Max(x => x.ElapsedMiliseconds);

            TestReport.ResponseTimeAverage = averageMillisecods;
            TestReport.ResponseTimeMin = min;
            TestReport.ResponseTimeMax = max;

        }

        internal TestReport getTestReport()
        {
            return TestReport;
        }
    }
}
