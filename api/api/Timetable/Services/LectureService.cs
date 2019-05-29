using System;
using api.Exception;
using BAGCST.api.User.Database;
using BAGCST.api.User.Models;
using BAGCST.api.Timetable.Database;
using BAGCST.api.Timetable.Models;

namespace BAGCST.api.Timetable.Services
{
    public class LectureService
    {
        private IUserDB userDB;
        private ITimetableDB timetableDB;
        private ISemesterDB semesterDB;

        public LectureService(IUserDB userDB, ITimetableDB timetableDB, ISemesterDB semesterDB)
        {
            this.userDB = userDB;
            this.timetableDB = timetableDB;
            this.semesterDB = semesterDB;
        }

        public LectureItem[] getLectures(long userID)
        {
            LectureItem[] lectures = new LectureItem[0];
            UserItem userItem = userDB.getUserItem(userID);

            if (userItem != null)
            {
                if (userItem.UserType.Name == "Student")
                {
                    string studyGroup = userItem.StudyGroup.ShortName;
                    SemesterItem currentSemester = semesterDB.getCurrentSemesterByStudyGroup(studyGroup);

                    if (currentSemester == null)
                    {
                        //Create pseudo-semester
                        currentSemester = new SemesterItem
                        {
                            Start = getFirstOfMonth(),
                            End = getFirstOfMonth().AddMonths(3),
                            StudyGroup = studyGroup
                        };
                    }
                    lectures = timetableDB.getSemesterLectures(studyGroup, currentSemester);
                }
                else if (userItem.UserType.Name == "Dozent")
                {
                    //ToDo: get dozID
                    string dozID = "Prof. Penzel";
                    DateTime startDate = getFirstOfMonth();
                    DateTime endDate = startDate.AddMonths(3);
                    lectures = timetableDB.getLecturesByLecturer(dozID, startDate, endDate);
                }
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
