using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.offlineDB;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostGroupController : ControllerBase
    {

        private IPostGroupDB database = getDatabase();

        /// <summary>
        /// Returns the current database
        /// TODO Implementing a switch which depends on the environment
        /// </summary>
        /// <returns></returns>
        private static IPostGroupDB getDatabase()
        {
            return new offlinePostGroupDB();
        }

        /// <summary>
        /// Get all PostGroup-Post by filtering
        /// </summary>
        /// <param name="start" default="0">Start index, search is desc.</param>
        /// <param name="amount" default="10">How many PostGroup should load...</param>
        /// <param name="groups">Which group-id should loaded</param>
        /// <returns></returns>
        [HttpGet]
        public PostGroupItem[] getAllPostGroup([FromQuery] int start = 0, [FromQuery] int amount = 10,[FromQuery] int[] groups = null)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Posts a new PostGroup-item
        /// ID and Date will set by api
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult postPostGroupItem(PostGroupItem item)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult deletePostGroupItem(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update existing Item, override the datetime to now.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult updatePostGroupItem(int id, [FromBody] PostGroupItem item)
        {
            throw new NotImplementedException();
        }
    }
}