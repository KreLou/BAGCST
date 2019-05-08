using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.offlineDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyCourseController : ControllerBase
    {
        private readonly IStudyCourseDB studyCourseDB = new offlineStudyCourseDB();

        [HttpGet]
        public StudyCourse[] getAll()
        {
            return studyCourseDB.getAllCourses();
        }
    }
}