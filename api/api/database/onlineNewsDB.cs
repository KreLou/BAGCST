using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace api.database
{
    
    public class onlineNewsDB : INewsDB
    {
        private SqlConnection sqlConnection = null;

        /// <summary>
        /// Delete a News-item from db by id
        /// </summary>
        /// <param name="id"></param>
        public void deletePost(int id)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "DELETE FROM [news] WHERE [newsid] ='" + id.ToString() + "';";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlConnection = null;
                    return;
                }
            }
            catch (Exception)
            {

                return;
            }
        }

        /// <summary>
        /// Update existing News-Item by replacing 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public NewsItem editPost(NewsItem item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all News-Items starting by startID and returns amount, filtered by groups
        /// </summary>
        /// <param name="amount">How many items should found</param>
        /// <param name="startID">What is the startid, desc</param>
        /// <param name="groups">What groups should loaded</param>
        /// <returns></returns>
        public NewsItem[] getPosts(int amount, int startID, int[] groups)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves new item and assing new id, return the new item 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public NewsItem saveNewPost(NewsItem item)
        {
            throw new NotImplementedException();
        }
    }
}
