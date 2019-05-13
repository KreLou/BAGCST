using BAGCST.api.StudySystem.Database;
using BAGCST.api.StudySystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyGroupController : ControllerBase
    {
        private readonly IStudyGroupDB studyGroupDB;

        public StudyGroupController(IStudyGroupDB studyGroupDB)
        {
            this.studyGroupDB = studyGroupDB;
        }

        [HttpGet]
        public StudyGroup[] getAll()
        {
            return studyGroupDB.getAll();
        }
    }
}