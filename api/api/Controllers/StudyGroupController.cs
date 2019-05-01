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
    public class StudyGroupController : ControllerBase
    {
        private readonly IStudyGroupDB studyGroupDB = new offlineStudyGroupDB();

        [HttpGet]
        public StudyGroup[] getAll()
        {
            return studyGroupDB.getAll();
        }
    }
}