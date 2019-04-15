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
        private IUserSettingsDB settingsDatabase = getSettingsDatabase();
        private IPostGroupDB postGroupDatabase = getPostGroupDatabase();

        private static IPostGroupDB getPostGroupDatabase()
        {
            return new offlinePostGroupDB();
        }

        private static IUserSettingsDB getSettingsDatabase()
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
            UserItem user = userDatabase.getUserItem(id);
            if (user == null)
            {
                return NotFound("No UserItem found for ID: " + id);
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

    }
}