using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAGCST.api.RightsSystem.Exception
{
    public class RightItemNotFoundException: System.Exception
    {
        public int RightID { get; private set; }
        public RightItemNotFoundException(int id): base()
        {
            this.RightID = id;
        }
    }
}
