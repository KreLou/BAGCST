﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAGCST.api.Timetable.Models
{
    public class LectureItem
    {
        public string StudyGroup { get; set; }
        public int Semester { get; set; }
        public string Place { get; set; }
        public string Title { get; set; }
        public string Lecturer { get; set; }
        /// <summary>
        /// Other textfield
        /// </summary>
        public string Comment { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
