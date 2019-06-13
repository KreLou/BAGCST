using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using BAGCST.api.RightsSystem.Models;

namespace BAGCST.api.RightsSystem.Database
{
    public interface IGroupsDB
    {
        /// <summary>
        /// search for Group by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Group|null</returns>
        GroupItem getGroup(int id);

        /// <summary>
        /// get all Groups
        /// </summary>
        /// <returns>Array of Rights with length>=0</returns>
        GroupItem[] getAllGroups();

        /// <summary>
        /// creates a Group
        /// </summary>
        /// <param name="item">Group</param>
        /// <returns>full Group</returns>
        GroupItem createGroup(GroupItem item);

        /// <summary>
        /// edit Group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>edited Group|null</returns>
        GroupItem editGroup(int id, GroupItem item);

        /// <summary>
        /// delete Group
        /// </summary>
        /// <param name="id">ID</param>
        void deleteGroup(int id);

        /// <summary>
        /// returns all Groups for the User, identified by the given UserID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GroupItem[] getGroupsByUser(long userID);

        /// <summary>
        /// sets the given GroupIDs for the User, identified by the given UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="groupIDs"></param>
        void setGroupsForUser(long userID, int[] groupIDs);
    }
}
