using BAGCST.api.Timetable.Models;
using System;

namespace BAGCST.api.Timetable.Database
{
    public interface ITimetableDB
    {
        /// <summary>
        /// Returns the Lecture from specific semester
        /// Starttime should be the beginning of the current semester
        /// </summary>
        /// <param name="studyGroup"></param>
        /// <param name="semester"></param>
        /// <returns></returns>
        LectureItem[] getSemesterLectures(string studyGroup, SemesterItem currentSemster);

        /// <summary>
        /// Get the Lectures for the Lecturer
        /// </summary>
        /// <param name="lecturer"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        LectureItem[] getLecturesByLecturer(string lecturer, DateTime startTime, DateTime endDate);
    }
}
