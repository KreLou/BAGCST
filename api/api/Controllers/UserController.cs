using System;
using api.Models;
using api.Interfaces;
using api.offlineDB;
using api.database;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private IUserDB userDB;
        private IUserSettingsDB userSettingsDB;
        private IPostGroupDB postGroupDB;

        public UserController(IUserDB userDB, IUserSettingsDB userSettingsDB, IPostGroupDB postGroupDB)
        {
            this.userDB = userDB;
            this.userSettingsDB = userSettingsDB;
            this.postGroupDB = postGroupDB;
        }

        [HttpGet]
        public UserItem[] getAllUserItems()
        {
            return userDB.getUserItems();
        }

        [HttpGet("{id}")]
        public ActionResult getUserByID(long id)
        {
            UserItem user = getFullUserItem(id);
            if (user == null)
            {
                return NotFound("No UserItem found for ID: " + id);
            }
            return Ok(user);
        }

        [HttpGet("me")]
        public ActionResult getMyUserInformation()
        {
            long userID = 1; //TODO Change to Token

            UserItem user = getFullUserItem(userID);
            if (user == null)
            {
                return NotFound("No UserItem found for ID:" + userID);
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public ActionResult updateUser(long id, [FromBody] UserItem user)
        {
            //TODO Check if user is allowed to update this item
            
            if (userDB.getUserItem(id) == null)
            {
                return NotFound("No UserItem found for ID: " + id);
            }
            user.UserID = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user = userDB.editUserItem(id, user);
            return Ok(user);
        }

        private UserItem getFullUserItem(long userID)
        {
            UserItem user = userDB.getUserItem(userID);
            if (user != null)
            {
                user.SubscribedPostGroups = userSettingsDB.getSubscribedPostGroupsSettings(userID);
                user.PostGroups = postGroupDB.getPostGroupsWhereUserIsAuthor(userID);
            }
            return user;
        }

    }
}