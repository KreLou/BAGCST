using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api.offlineDB
{
    public class offlineTimetableDB : ITimetableDB
    {
        private string filepath = Path.Combine(Environment.CurrentDirectory, "offlineDB", "Files", "timetable.csv");
        private string convertLectureItemToString(LectureItem item)
        {
            return $"{item.StudyGroup};" +
                $"{item.Semester};" +
                $"{item.Title};" +
                $"{item.Lecturer};" +
                $"{item.Place};" +
                $"{item.Start};" +
                $"{item.End}";
        }
        private LectureItem convertStringToLectureItem(string line)
        {
            string[] args = line.Split(';');
            return new LectureItem
            {
                StudyGroup = args[0],
                Semester = Convert.ToInt32(args[1]),
                Title = args[2],
                Lecturer = args[3],
                Place = args[4],
                Start = Convert.ToDateTime(args[5]),
                End = Convert.ToDateTime(args[6])
            };
        }
        public LectureItem[] getLecturesByLecturer(string lecturer, DateTime startTime, DateTime endDate)
        {
            List<LectureItem> lectures = new List<LectureItem>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                string currentLine;

                while((currentLine = sr.ReadLine()) != null)
                {
                    LectureItem item = convertStringToLectureItem(currentLine);

                    if (item.Start >= startTime && item.Lecturer == lecturer)
                    {
                        lectures.Add(item);
                    }
                }
            }
            lectures = sortLectures(lectures);
            lectures = lectures.Where(x => x.Start <= endDate).ToList();
            return lectures.ToArray();
        }

        public LectureItem[] getSemesterLectures(string studyGroup, SemesterItem semesterItem)
        {
            List<LectureItem> lectures = new List<LectureItem>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                string line;
                while((line = sr.ReadLine())!= null)
                {
                    LectureItem item = convertStringToLectureItem(line);
                    if (item.Start >= semesterItem.Start && item.StudyGroup == studyGroup && item.Start <= semesterItem.End)
                    {
                        lectures.Add(item);
                    }
                }
            }
            lectures = sortLectures(lectures);
            return lectures.ToArray();
        }



        private List<LectureItem> sortLectures(List<LectureItem> lectures)
        {
            return lectures.OrderBy(x => x.Start).ToList();
        }
    }
}
