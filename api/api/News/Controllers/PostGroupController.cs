using System;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using BAGCST.api.News.Database;

namespace BAGCST.api.News.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostGroupController : ControllerBase
    {
        private IPostGroupDB postGroupDB;

        public PostGroupController(IPostGroupDB postGroupDB)
        {
            this.postGroupDB = postGroupDB;
        }

        /// <summary>
        /// Get all existing PostGroups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PostGroupItem[] getAllPostGroupItems()
        {
            //TODO Groups settings should be stored in the database
            return postGroupDB.getPostGroupItems();
        }

        [HttpGet("{id}")]
        public IActionResult getPostGroupItem(int id)
        {
            PostGroupItem item = postGroupDB.getPostGroupItem(id);
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
                item = postGroupDB.saveNewPostGroupItem(item);
                return Created("",  item); 

            }
            catch (System.Exception e)
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
                postGroupDB.deletePostGroupItem(id);
                return Ok();
            }
            catch(System.Exception ex)
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
                postGroupDB.editPostGroupItem(id, item);
                return Ok(item);
            } 
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("{postGroupID}/author/{userID}")]
        public IActionResult postAuthorOfPostGroup(int postGroupID, long userID)
        {
            if (postGroupDB.getPostGroupItem(postGroupID) == null)
            {
                return NotFound($"No PostGroupItem found for ID: {postGroupID}");
            }
            postGroupDB.addUserToPostGroupAuthors(postGroupID, userID);

            if (postGroupDB.checkIfUserIsPostGroupAuthor(postGroupID, userID))
            {
                return Ok();
            }
            return BadRequest($"Could not add the User with the ID: {userID} to the PostGroup by ID: {postGroupID}");
            
        }

        [HttpDelete("{postGroupID}/author/{userID}")]
        public IActionResult deleteUserFromPostGroupAuthors(int postGroupID, long userID)
        {
            if (postGroupDB.getPostGroupItem(postGroupID) == null)
            {
                return NotFound($"No PostGroupItem found for ID: {postGroupID}");
            }

            postGroupDB.deleteUserFromPostGroupAuthors(postGroupID, userID);

            if (!postGroupDB.checkIfUserIsPostGroupAuthor(postGroupID, userID))
            {
                return Ok();
            }
            return BadRequest($"Could not delete User {userID} from PostGroupItem {postGroupID}");
         }

        [HttpGet("mygroups")]
        public IActionResult getMyPostGroups()
        {
            long authorID = 1; //TODO Get userID from Token

            PostGroupItem[] myGroups = postGroupDB.getPostGroupsWhereUserIsAuthor(authorID);

            if (myGroups.Length == 0)
            {
                return NotFound("No PostGroups found");
            }

            return Ok(myGroups);
        }

    }
}