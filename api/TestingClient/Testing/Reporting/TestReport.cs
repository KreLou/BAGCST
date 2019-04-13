using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;

namespace TestingClient.Testing.Reporting
{
    class TestReport
    {
        public string TestName { get; set; }
        public string TestUser { get; set; }
        public DateTime TestRunAt { get; set; }

        public long ResponseTimeAverage { get; set; }
        public long ResponseTimeMax { get; set; }
        public long ResponseTimeMin { get; set; }
        
        public long[] ContentLengths { get { return _contentLength.ToArray(); } }

        public HttpStatusCodeReporter StatusCodeReporter { get; set; }


        private List<long> _contentLength;
        

        public TestReport()
        {
            _contentLength = new List<long>();
        }

        public void addContentLength(long length)
        {
            _contentLength.Add(length);
        }

        public override string ToString()
        {
            return $"TestReport for {TestName}\n" +
                $"by {TestUser}\n" +
                $"on {TestRunAt.ToString("yyyy-MM-dd HH:mm:ss")}\n" +
                $"\n" +
                $"Response-Time:\n" +
                $"- min: {ResponseTimeMin}\n" +
                $"- avr: {ResponseTimeAverage}\n" +
                $"- max: {ResponseTimeMax}\n" +
                $"\n" +
                $"StatusCodes:\n" +
                $"{StatusCodeReporter.ToString()}\n" +
                $"\n" +
                $"ContentLengths:\n" +
                $"- min: {ContentLengths.Min()}\n" +
                $"- avr: {ContentLengths.Average()}\n" +
                $"- max: {ContentLengths.Max()}\n" +
                $"\n" +
                $"End Report";
        }
    }
}
