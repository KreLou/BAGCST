using System;
using System.Collections.Generic;
using System.Text;
using TestingClient.Testing.Performance.Abstract;

namespace TestingClient.Testing.Performance.Postgroup
{
    class PostgroupPerformanceTesting : PerformanceTesting
    {
        public PostgroupPerformanceTesting():base("http://localhost:55510/api/postgroup")
        {

        }
    }
}
