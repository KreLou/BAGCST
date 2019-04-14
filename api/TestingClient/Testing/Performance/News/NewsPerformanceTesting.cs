using System;
using System.Collections.Generic;
using System.Text;
using TestingClient.Testing.Performance.Abstract;

namespace TestingClient.Testing.Performance.News
{
    class NewsPerformanceTesting : PerformanceTesting
    {
        public NewsPerformanceTesting():base("http://localhost:55510/api/news?amount=100")
        {

        }
    }
}
