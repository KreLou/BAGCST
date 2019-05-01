using System;
using api.Models;
using api.Interfaces;
using api.offlineDB;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private IUserDB userDatabase = getUserDatabase();
        private IUserSettings settingsDatabase = getSettingsDatabase();
        private IPostGroupDB postGroupDatabase = getPostGroupDatabase();

        private static IPostGroupDB getPostGroupDatabase()
        {
            return new offlinePostGroupDB();
        }

        private static IUserSettings getSettingsDatabase()
        {
            return new offlineUserSettings();
        }

        /// <summary>
        /// Returns the current database
        /// TODO Implementing a switch which depends on the environment
        /// </summary>
        /// <returns></returns>
        private static IUserDB getUserDatabase()
        {
            return new offlineUserDB();
        }

        [HttpGet]
        public UserItem[] getAllUserItems()
        {
            return userDatabase.getUserItems();
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
            
            if (userDatabase.getUserItem(id) == null)
            {
                return NotFound("No UserItem found for ID: " + id);
            }
            user.UserID = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user = userDatabase.editUserItem(id, user);
            return Ok(user);
        }

        private UserItem getFullUserItem(long userID)
        {
            UserItem user = userDatabase.getUserItem(userID);
            if (user != null)
            {
                user.SubscribedPostGroups = settingsDatabase.getSubscribedPostGroupsSettings(userID);
                user.PostGroups = postGroupDatabase.getPostGroupsWhereUserIsAuthor(userID);
            }
            return user;
        }

    }
}