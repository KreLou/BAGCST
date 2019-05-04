using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface ISemesterDB
    {
        /// <summary>
        /// Returns all SemsterItems for selected StudyGroup
        /// </summary>
        /// <param name="studyGroup"></param>
        /// <returns></returns>
        SemesterItem[] getSemesterItem(string studyGroup);

        /// <summary>
        /// Returns the current SemesterItem for the selected StudyGroup
        /// </summary>
        /// <param name="studyGroup"></param>
        /// <returns></returns>
        SemesterItem getCurrentSemesterByStudyGroup(string studyGroup);
    }
}
