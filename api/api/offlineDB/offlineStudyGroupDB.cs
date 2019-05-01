using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api.offlineDB
{
    public class offlineStudyGroupDB : IStudyGroupDB
    {

        private IStudyCourseDB studyCourseDB = new offlineStudyCourseDB();

        string filepath = Path.Combine(Environment.CurrentDirectory, "offlineDB", "Files", "studygroup.csv");
        public StudyGroup[] getAll()
        {
            List<StudyGroup> list = new List<StudyGroup>();

            using (StreamReader sr = new StreamReader(filepath))
            {
                string line;

                while((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    list.Add(new StudyGroup
                    {
                        ID = Convert.ToInt32(args[0]),
                        Active = Convert.ToBoolean(args[1]),
                        ShortName = args[2],
                        LongName = args[3],
                        StudyCourse = studyCourseDB.getCourseById(Convert.ToInt32(args[4]))
                    });
                }
            }
            return list.ToArray();
        }

        public StudyGroup getByID(int id)
        {
            return getAll().SingleOrDefault(x => x.ID == id);
        }
    }
}
