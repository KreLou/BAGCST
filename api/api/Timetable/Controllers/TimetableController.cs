using System.Text;
using BAGCST.api.User.Database;
using BAGCST.api.Timetable.Database;
using BAGCST.api.Timetable.Models;
using BAGCST.api.Timetable.Services;
using Microsoft.AspNetCore.Mvc;

namespace BAGCST.api.Timetable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private IUserDB userDB;
        private LectureService lectureService;
        
        public TimetableController(IUserDB userDB, ITimetableDB timetableDB, ISemesterDB semesterDB)
        {
            this.userDB = userDB;
            lectureService = new LectureService(userDB, timetableDB, semesterDB);
        }

        [HttpGet]
        public IActionResult getLectureFeed([FromQuery] string studentNumber, [FromQuery] string hash)
        {
            //ToDo: implement exceptions for wrong/not existing studentNumber
            long userID = userDB.getUserByName(studentNumber).UserID;
            LectureItem[] lectures = lectureService.getLectures(userID);

            return Ok(lectures);
        }

        [HttpPost("{userID}")]
        public IActionResult getLectureView(long userID)
        {
            LectureItem[] lectures = lectureService.getLectures(userID);

            return Ok(lectures);
        }

        [HttpPost("export/{userID}")]
        public IActionResult getLectureExport(long userID)
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

            foreach (LectureItem lecture in lectureService.getLectures(userID))
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
    }
}