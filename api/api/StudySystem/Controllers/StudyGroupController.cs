using BAGCST.api.StudySystem.Database;
using BAGCST.api.StudySystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BAGCST.api.StudySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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