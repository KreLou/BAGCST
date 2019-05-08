using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class SemesterItem
    {
        public string StudyGroup { get; set; }
        public int Semester { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
