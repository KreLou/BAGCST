using BAGCST.api.StudySystem.Database;
using BAGCST.api.StudySystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BAGCST.api.StudySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyCourseController : ControllerBase
    {
        private readonly IStudyCourseDB studyCourseDB;
        
        public StudyCourseController(IStudyCourseDB studyCourseDB)
        {
            this.studyCourseDB = studyCourseDB;
        }

        [HttpGet]
        public StudyCourse[] getAll()
        {
            return studyCourseDB.getAllCourses();
        }
    }
}