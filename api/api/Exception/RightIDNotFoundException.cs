using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Exception
{
    public class RightIDNotFoundException: System.Exception
    {
        public string RightPath { get; private set; }
        public RightIDNotFoundException(string rightPath): base () {
            this.RightPath = rightPath;
        }
    }
}
