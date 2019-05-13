using api.Interfaces;
using api.Models;
using BAGCST.api.Timetable.Controllers;
using BAGCST.api.Timetable.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BAGCST.api.Timetable.Database
{
    public class offlineSemesterDB : ISemesterDB
    {
        private string filepath = Path.Combine(Environment.CurrentDirectory, "offlineDB", "Files", "semester.csv");
        private string convertToString(SemesterItem item)
        {
            return $"{item.StudyGroup};" +
                $"{item.Semester};" +
                $"{item.Start};" +
                $"{item.End}";
        }
        private SemesterItem convertToSemester(string line)
        {
            string[] args = line.Split(';');
            return new SemesterItem
            {
                StudyGroup = args[0],
                Semester = Convert.ToInt32(args[1]),
                Start = Convert.ToDateTime(args[2]),
                End = Convert.ToDateTime(args[3])
            };
        }
        public SemesterItem getCurrentSemesterByStudyGroup(string studyGroup)
        {
            DateTime today = DateTime.Today;
            SemesterItem[] items = getSemesterItem(studyGroup);
            items = items.Where(x => x.Start >= today).ToArray();
            items = items.Where(x => x.End <= today).ToArray();
            if (items.Length > 0)
            {
                return items[0];
            }
            return null;
        }

        public SemesterItem[] getSemesterItem(string studyGroup)
        {
            List<SemesterItem> list = new List<SemesterItem>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    SemesterItem item = convertToSemester(line);
                    if (item.StudyGroup == studyGroup)
                    {
                        list.Add(item);
                    }
                }
            }
            return list.OrderBy(x => x.Start).ToArray();
        }
    }
}
