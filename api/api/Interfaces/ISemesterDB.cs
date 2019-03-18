using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ISemesterDB
    {
        SemesterItem[] getSemesterItem(string studyGroup);
        SemesterItem getCurrentSemesterByStudyGroup(string studyGroup);
    }
}
