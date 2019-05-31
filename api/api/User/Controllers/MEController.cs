using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Services;
using BAGCST.api.News.Database;
using BAGCST.api.RightsSystem.Database;
using BAGCST.api.User.Database;
using BAGCST.api.User.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MEController : ControllerBase
    {
        private readonly IUserDB userDB;
        private readonly IGroupsDB groupsDB;
        private readonly TokenDecoderService tokenDecoder;
        private readonly IUserSettingsDB userSettingsDB;
        private readonly IPostGroupDB postGroupDB;
        public MEController(IUserDB userDB, IGroupsDB groupsDB, TokenDecoderService tokenDecoder, IUserSettingsDB userSettingsDB, IPostGroupDB postGroupDB)
        {
            this.userDB = userDB;
            this.groupsDB = groupsDB;
            this.tokenDecoder = tokenDecoder;
            this.userSettingsDB = userSettingsDB;
            this.postGroupDB = postGroupDB;
        }

        [HttpGet("rights")]
        public IActionResult getUserRights()
        {
            var info = this.tokenDecoder.GetTokenInfo(User);

            var rights = this.groupsDB.getGroupsByUser(info.UserID);

            return Ok(rights);
        }

        [HttpGet("postGroups")]
        public IActionResult getMysubscribedPostGroups()
        {
            long userID = 1; //TODO get from token

            var groups = userSettingsDB.getSubscribedPostGroupsSettings(userID);

            groups = groups.Where(x => x.PostGroupActive).OrderBy(x => x.PostGroupID).ToArray();

            return Ok(groups);
        }

        [HttpPost("postGroups")]
        public IActionResult setMysubscribedPostGroups([FromBody] PostGroupUserPushNotificationSetting[] settings)
        {
            long userID = 1; //TODO get from token
            userSettingsDB.setSubscribedPostGroupIDs(userID, settings);

            return Ok();
        }

        [HttpGet("author")]
        public IActionResult getPostGroupsWhereIAmTheAuthor()
        {
            long userID = 1; //TODO get from token

            var groups = postGroupDB.getPostGroupsWhereUserIsAuthor(userID);

            return Ok(groups);
        }
    }
}