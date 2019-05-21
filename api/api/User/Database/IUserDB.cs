using BAGCST.api.User.Models;

namespace BAGCST.api.User.Database
{
    public interface IUserDB
    {
        /// <summary>
        /// Create a new News-Item in Database, return the new Item
        /// </summary>
        /// <param name="item"></param>
        UserItem saveNewUserItem(UserItem item);

        /// <summary>
        /// Update existing User-Item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        UserItem editUserItem(long id, UserItem item);

        /// <summary>
        /// Delete a News-Item by ID
        /// </summary>
        /// <param name="id"></param>
        void deleteUserItem(long id);
      
      
        UserItem getUserByName(string username);

        /// <summary>
        /// Return all User-Items
        /// </summary>
        /// <returns>Array of UserItems</returns>
        UserItem[] getUserItems();

        /// <summary>
        /// Search for User with specific ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return correct User-Item or null if user not found</returns>
        UserItem getUserItem(long id);

        /// <summary>
        /// Search for SubscribedPostGroups by the UserID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Array of PostGroupIDs</returns>
        int[] getSubscribedPostGroups(long UserID);

        /// <summary>
        /// add user to postgroup to make them possible to post
        /// </summary>
        /// <param name="id"></param>
        void addToPostGroup(long UserID, int PostGroupID);

        /// <summary>
        /// delete User from PostGroup
        /// </summary>
        /// <param name="id"></param>
        void deleteFromPostGroup(long UserID, int PostGroupID);
    }

}
