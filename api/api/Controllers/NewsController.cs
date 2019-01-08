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
    public class NewsController : ControllerBase
    {

        private INewsDB database = getDatabase();

        /// <summary>
        /// Returns the current database
        /// TODO Implementing a switch which depends on the environment
        /// </summary>
        /// <returns></returns>
        private static INewsDB getDatabase()
        {
            return new offlineNewsDB();
        }

        /// <summary>
        /// Get all News-Post by filtering
        /// </summary>
        /// <param name="start" default="0">Start index, search is desc.</param>
        /// <param name="amount" default="10">How many News should load...</param>
        /// <param name="groups">Which group-id should loaded</param>
        /// <returns></returns>
        [HttpGet]
        public NewsItem[] getAllNews([FromQuery] int start = 0, [FromQuery] int amount = 10,[FromQuery] int[] groups = null)
        {
            //TODO Groups settings should be stored in the database
            return database.getPosts(amount, start, groups);
        }
        
        /// <summary>
        /// Posts a new news-item
        /// ID and Date will set by api
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult postNewsItem(NewsItem item)
        {
            item.Date = DateTime.Now;
            try
            {
                item = database.saveNewPost(item);
                return Created("",  item); 

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteNewsItem(int id)
        {
            try
            {
                database.deletePost(id);
                return Ok();
            }catch(Exception ex)
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
        public IActionResult updateNewsItem(int id, [FromBody] NewsItem item)
        {
            if (item == null)
            {
                return BadRequest("No news-item found in body");
            }
            item.ID = id;
            item.Date = DateTime.Now;
            try
            {
                database.editPost(item);
                return Ok(item);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}