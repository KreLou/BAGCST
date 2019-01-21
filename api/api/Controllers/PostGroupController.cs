<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> c71de8ea970c1e17e86f769f13ea45f6ec7724f5
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
<<<<<<< HEAD
=======

>>>>>>> c71de8ea970c1e17e86f769f13ea45f6ec7724f5
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
<<<<<<< HEAD
        /// Get all existing PostGroups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PostGroupItem[] getAllPostGroups()
        {
            //TODO Groups settings should be stored in the database
            return database.getPostGroups();
        }
        
        /// <summary>
        /// Posts a new news-item
=======
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
>>>>>>> c71de8ea970c1e17e86f769f13ea45f6ec7724f5
        /// ID and Date will set by api
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult postPostGroupItem(PostGroupItem item)
        {
<<<<<<< HEAD
            item.EditDate = DateTime.Now;
            try
            {
                item = database.saveNewPostGroup(item);
                return Created("",  item); 

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
=======
            throw new NotImplementedException();
>>>>>>> c71de8ea970c1e17e86f769f13ea45f6ec7724f5
        }

        [HttpDelete("{id}")]
        public IActionResult deletePostGroupItem(int id)
        {
<<<<<<< HEAD
            try
            {
                database.deletePostGroupItem(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
=======
            throw new NotImplementedException();
>>>>>>> c71de8ea970c1e17e86f769f13ea45f6ec7724f5
        }

        /// <summary>
        /// Update existing Item, override the datetime to now.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
<<<<<<< HEAD
        public IActionResult updateNewsItem(int id, [FromBody] PostGroupItem item)
        {
            if (item == null)
            {
                return BadRequest("No news-item found in body");
            }
            item.PostGroupID = id;
            item.EditDate = DateTime.Now;
            try
            {
                database.editPostGroup(item);
                return Ok(item);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
=======
        public IActionResult updatePostGroupItem(int id, [FromBody] PostGroupItem item)
        {
            throw new NotImplementedException();
>>>>>>> c71de8ea970c1e17e86f769f13ea45f6ec7724f5
        }
    }
}