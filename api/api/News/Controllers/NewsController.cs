using api.Models;
using api.offlineDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using api.database;
using BAGCST.api.News.Database;
using BAGCST.api.User.Database;
using BAGCST.api.News.Models;
using BAGCST.api.User.Models;
using Microsoft.AspNetCore.Authorization;
using api.Services;

namespace BAGCST.api.News.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {


        private INewsDB newsDB;
        private IPostGroupDB postGroupDB;
        private IUserSettingsDB userSettingsDB;
        private readonly TokenDecoderService tokenDecoder;

        public NewsController(INewsDB newsDB, IPostGroupDB postGroupDB, IUserSettingsDB userrSettingsDB, TokenDecoderService tokenDecoder)
        {
            this.newsDB = newsDB;
            this.postGroupDB = postGroupDB;
            this.userSettingsDB = userrSettingsDB;
            this.tokenDecoder = tokenDecoder;
        }
        

        /// <summary>
        /// Get all News-Post by filtering
        /// </summary>
        /// <param name="start" default="0">Start index, search is desc.</param>
        /// <param name="amount" default="10">How many News should load...</param>
        /// <param name="groups">Which group-id should loaded</param>
        /// <returns></returns>
        [HttpGet]
        public NewsItem[] getAllNews([FromQuery] Int64 start = int.MaxValue, [FromQuery] int amount = 10)
        {
            var userinfo = tokenDecoder.GetTokenInfo(User);
            long userID = userinfo.UserID;//1; //TODO Get User-ID by Token

            int[] groupsID = new int[0];

            PostGroupUserPushNotificationSetting[] settings = userSettingsDB.getSubscribedPostGroupsSettings(userID);

            if (settings.Length > 0) groupsID = settings.Where(x => x.PostGroupActive).Select(x => x.PostGroupID).ToArray();

            //TODO Groups settings should be stored in the database
            return newsDB.getPosts(amount, start, groupsID);
        }
        
        /// <summary>
        /// Posts a new news-item
        /// ID and Date will set by api
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("{postGroupID}")]
        public IActionResult postNewsItem(NewsItem item, int postGroupID)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userinfo = tokenDecoder.GetTokenInfo(User);
            long authorID = userinfo.UserID;//1; //TODO Register the author id by the auth-token
            item.Date = DateTime.Now;
            item.PostGroup = new PostGroupItem { PostGroupID = postGroupID };
            item.AuthorID = authorID;

            if (!postGroupDB.checkIfUserIsPostGroupAuthor(postGroupID, authorID))
            {
                return BadRequest($"User {authorID} is not allowed to post for PostGroup {postGroupID}");
            }
            try
            {
                item = newsDB.saveNewPost(item);
                return Created("",  item); 

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteNewsItem(int id)
        {
            try
            {
                newsDB.deletePost(id);
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
        public IActionResult updateNewsItem(int id, [FromBody] NewsItem item)
        {
            //TODO check if age of news less then 10 minutes
                if (item == null)
            {
                return BadRequest("No news-item found in body");
            }
            item.ID = id;
            item.Date = DateTime.Now;
            try
            {
                newsDB.editPost(item);
                return Ok(item);
            } 
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
