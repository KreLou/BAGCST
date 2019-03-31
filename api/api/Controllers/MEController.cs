using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.offlineDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MEController : ControllerBase
    {
        private IUserDB userDB;
        private IUserSettingsDB userSettingsDB;
        
        public MEController()
        {
            this.userDB = new offlineUserDB();
            this.userSettingsDB = new offlineUserSettings();
        }

        [HttpGet("settings")] 
        public IActionResult getSettings()
        {
            long userID = 1; //TODO Get From Token

            return Ok();
        }
    }
}