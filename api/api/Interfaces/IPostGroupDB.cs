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
        PostGroupItem saveNewPostGroupItem(PostGroupItem item);

        /// <summary>
        /// Update existing News-Item by ID
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        PostGroupItem editPostGroupItem(int id, PostGroupItem item);

        /// <summary>
        /// deletes POstGroup by given ID
        /// </summary>
        void deletePostGroupItem(int id);

        /// <summary>
        /// Return all PostGroupItems
        /// </summary>
        /// <returns></returns>
        PostGroupItem[] getPostGroupItems();

        /// <summary>
        /// returns a single postgroup
        /// </summary>
        /// <returns></returns>
        PostGroupItem getPostGroupItem(int id);
    }
}
