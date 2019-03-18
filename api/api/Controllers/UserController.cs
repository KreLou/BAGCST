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
        
        private IUserDB database = getDatabase();

        /// <summary>
        /// Returns the current database
        /// TODO Implementing a switch which depends on the environment
        /// </summary>
        /// <returns></returns>
        private static IUserDB getDatabase()
        {
            return new offlineUserDB();
        }

        [HttpGet]
        public UserItem[] getAllUserItems()
        {
            return database.getUserItems();
        }

    }
}