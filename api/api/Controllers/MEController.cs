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
    public class MEController : ControllerBase
    {
        private IUserDB userDB;
        private IUserSettingsDB userSettingsDB;
        private IPostGroupDB postGroupDB;
        
        public MEController()
        {
            this.postGroupDB = new offlinePostGroupDB();
            this.userDB = new offlineUserDB();
            this.userSettingsDB = new offlineUserSettings();
        }

        /// <summary>
        /// Returns the Settings for the current User
        /// </summary>
        /// <returns></returns>
        [HttpGet("settings")] 
        public IActionResult getSettings()
        {
            long userID = 1; //TODO Get From Token

            return Ok(userSettingsDB.getUserSettings(userID));
        }
        
        /// <summary>
        /// Replace the Settings for the current user
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        [HttpPost("settings")]
        public IActionResult postSettings([FromBody] UserSettingsItem settings)
        {
            long userID = 1; //TODO Get from Token
            this.userSettingsDB.setUserSettings(userID, settings);

            settings = userSettingsDB.getUserSettings(userID);
            return Ok(settings);
        }

        /// <summary>
        /// Get the Information about the current user
        /// </summary>
        /// <returns></returns>
        [HttpGet("info")]
        public IActionResult getInfo()
        {
            long userID = 1; //TODO get From Token

            UserItem userItem = userDB.getUserItem(userID);

            if (userItem == null) return NotFound("No UserItem found for your ID: " + userID);

            return Ok(userItem);
        }

        /// <summary>
        /// Returns the PostGroups, where the User is author
        /// </summary>
        /// <returns></returns>
        [HttpGet("postgroups")]
        public IActionResult getPostGroups()
        {
            long userID = 1;

            PostGroupItem[] postGroups = postGroupDB.getPostGroupsWhereUserIsAuthor(userID);

            return Ok(postGroups);
        }
    }
}