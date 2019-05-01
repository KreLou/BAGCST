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

        [HttpGet("{userid}")]
        public IActionResult getLectureFeed(int userid)
        {
            LectureItem[] lectures = getLectures(userid);

            return Ok(lectures);
        }

        [HttpGet("{userid}/export")]
        public IActionResult getLectureExport(int userid)
        {
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

            //TODO: Check for iOS/Android/Windows acceptance of .ics/.iCal
            //Question for Wized: Is '.ics' enough on iOS? Please test.
            return File(bytes, "text/calendar", "bagcst_export.ics");
        }

        private LectureItem[] getLectures(int userid)
        {
            LectureItem[] lectures = null;
            //TODO: Get userinfo by userid (e.g. student/studygroup, lecturer)
            string studygroup = "WI16-1";
            bool isStudent = true;

            if (isStudent)
            {
                SemesterItem currentSemester = semesterDatabase.getCurrentSemesterByStudyGroup(studygroup);
                DateTime startDate = currentSemester == null ? getFirstOfMonth() : currentSemester.Start;
                lectures = timetableDatabase.getSemesterLectures(studygroup, startDate);
            }
            else //is Lecturer
            {
                string dozID = "Prof. Penzel";
                DateTime startDate = getFirstOfMonth();
                lectures = timetableDatabase.getLecturesByLecturer(dozID, startDate);
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