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
        /// Get all existing PostGroups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PostGroupItem[] getAllPostGroupItems()
        {
            //TODO Groups settings should be stored in the database
            return database.getPostGroupItems();
        }
        
        /// <summary>
        /// Posts a new news-item
        /// ID and Date will set by api
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult postPostGroupItem(PostGroupItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            item.EditDate = DateTime.Now;
            try
            {
                item = database.saveNewPostGroupItem(item);
                return Created("",  item); 

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deletePostGroupItem(int id)
        {
            try
            {
                database.deletePostGroupItem(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            if (item == null)
            {
                return BadRequest($"No {nameof(PostGroupItem)} found in body");
            }
            item.PostGroupID = id;
            item.EditDate = DateTime.Now;
            try
            {
                database.editPostGroupItem(id, item);
                return Ok(item);
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}