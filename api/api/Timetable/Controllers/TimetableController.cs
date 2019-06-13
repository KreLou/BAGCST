using System.Text;
using api.Services;
using api.Selectors;
//using api.Models;
//using api.offlineDB;
using BAGCST.api.User.Database;
using BAGCST.api.User.Models;
using BAGCST.api.Timetable.Models;
using BAGCST.api.Timetable.Services;
using Microsoft.AspNetCore.Mvc;

namespace BAGCST.api.Timetable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly IUserDB userDB;
        private readonly LectureService lectureService;
        private readonly TokenDecoderService tokenDecoderService;
        
        public TimetableController(IUserDB userDB, LectureService lectureService, TokenDecoderService tokenDecoderService)
        {
            this.userDB = userDB;
            this.lectureService = lectureService;
            this.tokenDecoderService = tokenDecoderService;
        }

        [HttpGet]
        public IActionResult getLectureFeed([FromQuery] string studentNumber = "", [FromQuery] string hash = "")
        {
            long userID = 0;
            LectureItem[] lectures = new LectureItem[0];

            UserItem userItem = userDB.getUserByName(studentNumber);
            if (userItem != null)
            {
                userID = userItem.UserID;
                lectures = lectureService.getLectures(userID);
            }

            return Ok(lectures);
        }

        [HttpGet("view")]
        public IActionResult getLectureView()
        {
            long userID = getUserID(User);
            LectureItem[] lectures = lectureService.getLectures(userID);

            return Ok(lectures);
        }

        [HttpGet("export")]
        public IActionResult getLectureExport()
        {
            long userID = getUserID(User);
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

        private long getUserID(System.Security.Claims.ClaimsPrincipal User)
        {
            long userID = long.MinValue;

            try
            {
                TokenInformation tokenInformation = tokenDecoderService.GetTokenInfo(User);
                userID = tokenInformation.UserID;
            }
            catch
            {

            }

            return userID;
        }
    }
}