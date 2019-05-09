using System;
using System.Text;
using api.Interfaces;
using api.Models;
using api.offlineDB;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private ITimetableDB timetableDB;
        private ISemesterDB semesterDB;
        

        public TimetableController(ITimetableDB timetableDB, ISemesterDB semesterDB)
        {
            this.timetableDB = timetableDB;
            this.semesterDB = semesterDB;
        }

        [HttpGet]
        public IActionResult getLectureFeed()
        {
            //TODO: Get userid from Token (or better: Get isStudent + studygroup/dozId)
            //Alt: Forward the whole token to 'getLectures(token)', check for isStudent + studygroup/dozId at central place
            int userid = 1;
            LectureItem[] lectures = getLectures(userid);

            return Ok(lectures);
        }

        [HttpGet("export")]
        public IActionResult getLectureExport()
        {
            //TODO: Get userid from Token (or better: Get isStudent + studygroup/dozId)
            int userid = 1;
            string calDateFormat = "yyyyMMddTHHmm00Z";
            var calendarString = new StringBuilder();

            calendarString.AppendLine("BEGIN:VCALENDAR");
            calendarString.AppendLine("VERSION:2.0");
            calendarString.AppendLine("PRODID:BAGCST - Campus App");
            calendarString.AppendLine("METHOD:PUBLISH");
            calendarString.AppendLine("BEGIN:VTIMEZONE");
            calendarString.AppendLine("TZID:CET");
            calendarString.AppendLine("BEGIN:DAYLIGHT");
            calendarString.AppendLine("TZOFFSETFROM:+0100");
            calendarString.AppendLine("TZOFFSETTO:+0200");
            calendarString.AppendLine("TZNAME:Central European Summer Time");
            calendarString.AppendLine("DTSTART:20160327T020000");
            calendarString.AppendLine("RRULE:FREQ=YEARLY;BYDAY=-1SU;BYMONTH=3");
            calendarString.AppendLine("END:DAYLIGHT");
            calendarString.AppendLine("BEGIN:STANDARD");
            calendarString.AppendLine("TZOFFSETFROM:+0200");
            calendarString.AppendLine("TZOFFSETTO:+0100");
            calendarString.AppendLine("TZNAME:Central European Time");
            calendarString.AppendLine("DTSTART:20161030T030000");
            calendarString.AppendLine("RRULE:FREQ=YEARLY;BYDAY=-1SU;BYMONTH=10");
            calendarString.AppendLine("END:STANDARD");
            calendarString.AppendLine("END:VTIMEZONE");

            foreach (LectureItem lecture in getLectures(userid))
            {
                calendarString.AppendLine("BEGIN:VEVENT");
                calendarString.AppendLine("LOCATION:" + lecture.Place);
                calendarString.AppendLine("SUMMARY:" + lecture.Title);
                calendarString.AppendLine("DESCRIPTION:" + lecture.Lecturer);
                calendarString.AppendLine("CLASS:PUBLIC");
                calendarString.AppendLine("DTSTART:" + lecture.Start.ToUniversalTime().ToString(calDateFormat));
                calendarString.AppendLine("DTEND:" + lecture.End.ToUniversalTime().ToString(calDateFormat));
                calendarString.AppendLine("END:VEVENT");
            }

            calendarString.AppendLine("END:VCALENDAR");

            var bytes = Encoding.UTF8.GetBytes(calendarString.ToString());

            return File(bytes, "text/calendar", "bagcst_export.ics");
        }

        private LectureItem[] getLectures(int userid)
        {
            LectureItem[] lectures = null;
            //TODO: Get userinfo by userid (e.g. student/studygroup, lecturer)
            //TODO Ad Switch and Adapter for the UserItem
            string studygroup = "WI16-1";
            bool isStudent = true;

            if (isStudent)
            {
                SemesterItem currentSemester = semesterDB.getCurrentSemesterByStudyGroup(studygroup);
                if (currentSemester == null)
                {
                    //Create pseudo-semester
                    currentSemester = new SemesterItem
                    {
                        Start = getFirstOfMonth(),
                        End = getFirstOfMonth().AddMonths(3),
                        StudyGroup = studygroup
                    };
                }
                lectures = timetableDB.getSemesterLectures(studygroup, currentSemester);
            }
            else
            {
                string dozID = "Prof. Penzel";
                DateTime startDate = getFirstOfMonth();
                DateTime endDate = startDate.AddMonths(3);
                lectures = timetableDB.getLecturesByLecturer(dozID, startDate, endDate);
            }

            return lectures;
        }

        private DateTime getFirstOfMonth()
        {
            DateTime today = DateTime.Today;

            return new DateTime(today.Year, today.Month, 1);
        }
    }
}