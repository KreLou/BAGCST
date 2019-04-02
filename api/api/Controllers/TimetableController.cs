using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.offlineDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private ITimetableDB timetableDatabase = getTimetableDatabase();
        private ISemesterDB semesterDatabase = getSemesterDatabase();

        private static ISemesterDB getSemesterDatabase()
        {
            return new offlineSemesterDB();
        }

        private static ITimetableDB getTimetableDatabase()
        {
            return new offlineTimetableDB();
        }

        [HttpGet]
        public IActionResult GetLectures()
        {
            LectureItem[] lectures = null;
            string studygroup = "WI16-1"; //TODO from token
            bool isStudent = true;

            if (isStudent)
            {
                SemesterItem currentSemester = semesterDatabase.getCurrentSemesterByStudyGroup(studygroup);

                DateTime startDate = currentSemester == null ? getFirstOfMonth() : currentSemester.Start;

                lectures = timetableDatabase.getSemesterLectures(studygroup, startDate);
            } else //is Lecturer
            {
                string dozID = "Prof. Penzel";
                DateTime startDate = getFirstOfMonth();

                lectures = timetableDatabase.getLecturesByLecturer(dozID, startDate);

            }
            return Ok(lectures);
        }

        private DateTime getFirstOfMonth()
        {
            DateTime today = DateTime.Today;
            return new DateTime(today.Year, today.Month, 1);
        }
    }
}