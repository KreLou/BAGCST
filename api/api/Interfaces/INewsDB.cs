using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface INewsDB
    {
        /// <summary>
        /// Create a new News-Item in Database, return the new Item
        /// </summary>
        /// <param name="item"></param>
        NewsItem saveNewPost(NewsItem item);

        /// <summary>
        /// Delete a News-Item by ID
        /// </summary>
        /// <param name="id"></param>
        void deletePost(int id);

        /// <summary>
        /// Update existing News-Item by ID
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        NewsItem editPost(NewsItem item);

        /// <summary>
        /// Return all News-Items starting by startID and returns amount, filtered by groups
        /// </summary>
        /// <param name="amount">How many items should found</param>
        /// <param name="startID">What is the startid, desc</param>
        /// <param name="groups">What groups should loaded</param>
        /// <returns></returns>
        NewsItem[] getPosts(int amount, int startID, int[] groups);
    }

}
