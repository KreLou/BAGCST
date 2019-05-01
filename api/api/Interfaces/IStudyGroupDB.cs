using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IStudyGroupDB
    {
        StudyGroup[] getAll();

        StudyGroup getByID(int id);
    }
}
