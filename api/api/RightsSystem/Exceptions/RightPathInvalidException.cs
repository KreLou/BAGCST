﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAGCST.api.RightsSystem.Exception
{
    public class RightPathInvalidException: System.Exception
    {
        public string RightPath { get; private set; }
        public RightPathInvalidException(string rightPath) : base()
        {
            this.RightPath = rightPath;
        }
    }
}
