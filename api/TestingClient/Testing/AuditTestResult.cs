using System;
using System.Collections.Generic;
using System.Text;

namespace TestingClient.Testing
{
    class AuditTestResults
    {
        public string RunningTestTitle { get; set; }
        public long AverageTimeMilliseconds { get; set; }
        public long MinTimeMilliseconds { get; set; }
        public long MaxTimeMilliseconds { get; set; }

        public override string ToString()
        {
            return $"Test-Result: for {RunningTestTitle}\n" +
                    $"AverageTime: {AverageTimeMilliseconds} ms\n" +
                    $"Max. Time:   {MaxTimeMilliseconds} ms\n" +
                    $"Min. Time:   {MinTimeMilliseconds} ms";
        }
    }
}
