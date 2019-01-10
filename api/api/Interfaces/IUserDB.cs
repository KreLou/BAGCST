using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IUserDB
    {
        /// <summary>
        /// Create a new News-Item in Database, return the new Item
        /// </summary>
        /// <param name="item"></param>
        UserItem saveNewUser(UserItem item);

        /// <summary>
        /// Delete a News-Item by ID
        /// </summary>
        /// <param name="id"></param>
        void inactivateUser(int id);

        /// <summary>
        /// Update existing User-Item by ID
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        UserItem editUser(UserItem item);

        /// <summary>
        /// Return all User-Items
        /// </summary>
        /// <param name="amount">How many items should found</param>
        /// <param name="startID">What is the startid, desc</param>
        /// <param name="groups">What groups should loaded</param>
        /// <returns></returns>
        UserItem[] getUser();

        /// <summary>
        /// Search for User with specific ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return correct User-Item or null if user not found</returns>
        UserItem getUserByID(int id);
    }

}
