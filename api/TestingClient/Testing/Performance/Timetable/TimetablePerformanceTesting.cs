using System;
using System.Collections.Generic;
using System.Text;
using TestingClient.Testing.Performance.Abstract;

namespace TestingClient.Testing.Performance.Timetable
{
    class TimetablePerformanceTesting : PerformanceTesting
    {
        public TimetablePerformanceTesting() : base("http://localhost:55510/api/timetable")
        {

        }
    }
}
