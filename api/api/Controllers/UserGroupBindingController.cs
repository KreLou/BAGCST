using System;
using api.Models;
using api.Interfaces;
using api.offlineDB;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserGroupBindingController : ControllerBase
    {
        private IUserGroupBindingDB userGroupBindingDB;
        private IUserDB userDB;
        private IGroupsDB groupsDB;

        public UserGroupBindingController(IUserGroupBindingDB userGroupBindingDB, IUserDB userDB, IGroupsDB groupsDB)
        {
            this.userGroupBindingDB = userGroupBindingDB;
            this.userDB = userDB;
            this.groupsDB = groupsDB;
        }

        [HttpGet]
        public IActionResult test() {
            return Ok("Test");
        }

        [HttpPost]
        public IActionResult addUserGroupBinding([FromQuery] int UserID,[FromQuery] int GroupID)
        {
            IActionResult ret = null;
            //check User
            if ( userDB.getUserItem(UserID) == null) ret = BadRequest($"No UserItem with id {UserID} found");
            //check Group
            if ( groupsDB.getGroup(GroupID) == null) ret = BadRequest($"No GroupItem with id {GroupID} found");
            //create binding
            if (ret == null) ret = Ok(userGroupBindingDB.addUserGroupBinding(UserID, GroupID));
            return ret;
        }

        [HttpDelete]
        public IActionResult deleteUserGroupBinding([FromQuery] int UserID,[FromQuery] int GroupID)
        {
            userGroupBindingDB.deleteUserGroupBinding(UserID, GroupID);
            return Ok();
        }

        [HttpGet("users/{GroupID}")]
        public UserItem[] getUsersOfGroup([FromQuery] int[] GroupIDs)
        {
            List<UserItem> ret = new List<UserItem>();            
            userGroupBindingDB.getUsersOfGroup(GroupIDs).ForEach(GID => ret.Add(userDB.getUserItem(GID)));
            return ret.ToArray();
        }

        [HttpGet("groups/{UserID}")]
        public Group[] getGroupsofUser([FromQuery] int[] UserIDs)
        {
            List<Group> ret = new List<Group>();
            userGroupBindingDB.getGroupsOfUser(UserIDs).ForEach(UID => ret.Add(groupsDB.getGroup(UID)));
            return ret.ToArray();
        }
    }
}