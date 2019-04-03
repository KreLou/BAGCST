using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Exception
{
    public class DuplicateRightsItemFoundException: System.Exception
    {
        public int RightID { get; private set; }
        public DuplicateRightsItemFoundException(int rightID) : base()
        {
            this.RightID = rightID;
        }
    }
}
