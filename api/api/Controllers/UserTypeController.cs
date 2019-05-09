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
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeDB userTypeDB;

        public UserTypeController(IUserTypeDB userTypeDB)
        {
            this.userTypeDB = userTypeDB;
        }

        [HttpGet]
        public UserType[] getAll()
        {
            return userTypeDB.getAll();
        }
    }
}