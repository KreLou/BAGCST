using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api.offlineDB
{
    public class offlineStudyCourseDB : IStudyCourseDB
    {
        string filepath = Path.Combine(Environment.CurrentDirectory, "offlineDB", "Files", "studycourse.csv");


        public StudyCourse[] getAllCourses()
        {
            List<StudyCourse> list = new List<StudyCourse>();

            using (StreamReader sr = new StreamReader(filepath))
            {
                string line;

                while((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    list.Add(new StudyCourse
                    {
                        ID = Convert.ToInt32(args[0]),
                        ShortText = args[1],
                        LongText = args[2]
                    });
                }
            }
            return list.ToArray();
        }

        public StudyCourse getCourseById(int id)
        {
            return getAllCourses().SingleOrDefault(x => x.ID == id);
        }
    }
}
