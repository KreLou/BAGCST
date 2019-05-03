using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.offlineDB;
using api.database;

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
            //return new offlinePostGroupDB();
            return new onlinePostGroupDB();
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

        [HttpGet("{id}")]
        public IActionResult getPostGroupItem(int id)
        {
            PostGroupItem item = database.getPostGroupItem(id);
            if (item == null) return NotFound($"No PostGroupItem found for ID {id}");
            return Ok(item);
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
            //TODO Should the Name be unique?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            item.CreationDate = DateTime.Now;
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
            //TODO What should happen, when the amount of posts > 0?
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


        [HttpPost("{postGroupID}/author/{userID}")]
        public IActionResult postAuthorOfPostGroup(int postGroupID, long userID)
        {
            if (database.getPostGroupItem(postGroupID) == null)
            {
                return NotFound($"No PostGroupItem found for ID: {postGroupID}");
            }
            database.addUserToPostGroupAuthors(postGroupID, userID);

            if (database.checkIfUserIsPostGroupAuthor(postGroupID, userID))
            {
                return Ok();
            }
            return BadRequest($"Could not add the User with the ID: {userID} to the PostGroup by ID: {postGroupID}");
            
        }

        [HttpDelete("{postGroupID}/author/{userID}")]
        public IActionResult deleteUserFromPostGroupAuthors(int postGroupID, long userID)
        {
            if (database.getPostGroupItem(postGroupID) == null)
            {
                return NotFound($"No PostGroupItem found for ID: {postGroupID}");
            }

            database.deleteUserFromPostGroupAuthors(postGroupID, userID);

            if (!database.checkIfUserIsPostGroupAuthor(postGroupID, userID))
            {
                return Ok();
            }
            return BadRequest($"Could not delete User {userID} from PostGroupItem {postGroupID}");
         }

        [HttpGet("mygroups")]
        public IActionResult getMyPostGroups()
        {
            long authorID = 1; //TODO Get userID from Token

            PostGroupItem[] myGroups = database.getPostGroupsWhereUserIsAuthor(authorID);

            if (myGroups.Length == 0)
            {
                return NotFound("No PostGroups found");
            }

            return Ok(myGroups);
        }

    }
}