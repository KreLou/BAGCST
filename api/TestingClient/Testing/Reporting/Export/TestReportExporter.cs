using System;
using System.Collections.Generic;
using System.Text;

namespace TestingClient.Testing.Reporting.Export
{
    class TestReportExporter : FileExporter
    {
        public TestReportExporter(DirectoryExporter directory) : base(directory, "test-report.txt")
        {
        }

        public void exportTestReport(TestReport report)
        {
            base.WriteLine(report.ToString());
        }
    }
}
