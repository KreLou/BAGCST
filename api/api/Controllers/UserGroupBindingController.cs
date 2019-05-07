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
        private IUserGroupBindingDB database = getDatabase();
        private IUserDB databaseUser = getUserDatabase();
        private IGroupsDB databaseGroup = getGroupDatabase();

        private static IUserGroupBindingDB getDatabase()
        {
            return new offlineUserGroupBindingDB();
        }
        private static IUserDB getUserDatabase()
        {
            return new offlineUserDB();
        }
        private static IGroupsDB getGroupDatabase()
        {
            return new offlineDB_Groups();
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
            if ( databaseUser.getUserItem(UserID) == null) ret = BadRequest($"No UserItem with id {UserID} found");
            //check Group
            if ( databaseGroup.getGroup(GroupID) == null) ret = BadRequest($"No GroupItem with id {GroupID} found");
            //create binding
            if (ret == null) ret = Ok(database.addUserGroupBinding(UserID, GroupID));
            return ret;
        }

        [HttpDelete]
        public IActionResult deleteUserGroupBinding([FromQuery] int UserID,[FromQuery] int GroupID)
        {
            database.deleteUserGroupBinding(UserID, GroupID);
            return Ok();
        }

        [HttpGet("users/{GroupID}")]
        public UserItem[] getUsersOfGroup([FromQuery] int[] GroupIDs)
        {
            List<UserItem> ret = new List<UserItem>();            
            database.getUsersOfGroup(GroupIDs).ForEach(GID => ret.Add(databaseUser.getUserItem(GID)));
            return ret.ToArray();
        }

        [HttpGet("groups/{UserID}")]
        public Group[] getGroupsofUser([FromQuery] int[] UserIDs)
        {
            List<Group> ret = new List<Group>();
            database.getGroupsOfUser(UserIDs).ForEach(UID => ret.Add(databaseGroup.getGroup(UID)));
            return ret.ToArray();
        }
    }
}