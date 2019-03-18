using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ITimetableDB
    {
        /// <summary>
        /// Returns the Lecture from specific semester
        /// Starttime should be the beginning of the current semester
        /// </summary>
        /// <param name="studyGroup"></param>
        /// <param name="semester"></param>
        /// <returns></returns>
        LectureItem[] getSemesterLectures(string studyGroup, DateTime startTime);

        /// <summary>
        /// Get the Lectures for the Lecturer
        /// </summary>
        /// <param name="lecturer"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        LectureItem[] getLecturesByLecturer(string lecturer, DateTime startTime);
    }
}
