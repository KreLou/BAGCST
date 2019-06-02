using BAGCST.api.Timetable.Models;

namespace BAGCST.api.Timetable.Database
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
