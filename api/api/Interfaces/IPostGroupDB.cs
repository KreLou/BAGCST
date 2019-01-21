using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IPostGroupDB
    {
        /// <summary>
        /// Create a new News-Item in Database, return the new Item
        /// </summary>
        /// <param name="item"></param>
        PostGroupItem saveNewPostGroup(PostGroupItem item);

        /// <summary>
        /// Delete a News-Item by ID
        /// </summary>
        /// <param name="id"></param>
        void updateActiveStatePostGroup(int id, bool isActive);

        /// <summary>
        /// Update existing News-Item by ID
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        PostGroupItem editPostGroup(PostGroupItem item);

        /// <summary>
        /// deletes POstGroup by given ID
        /// </summary>
        void deletePostGroupItem(int PostGroupID);

        /// <summary>
        /// Return all News-Items starting by startID and returns amount, filtered by groups
        /// </summary>
        /// <param name="amount">How many items should found</param>
        /// <param name="startID">What is the startid, desc</param>
        /// <param name="groups">What groups should loaded</param>
        /// <returns></returns>
        PostGroupItem[] getPostGroups();
    }
}
