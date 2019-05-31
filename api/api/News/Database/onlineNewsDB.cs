using api.database;
using api.Models;
using BAGCST.api.News.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BAGCST.api.News.Database
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
            catch (System.Exception)
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
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "UPDATE [news] SET " +
                        "[newsgroupid]='" + item.PostGroup.PostGroupID.ToString() + "'," +
                        "[headline]='" + item.Title + "',[text]='" + item.Message + "'," +
                        "[updatetime]= CURRENT_TIMESTAMP,[push]=0," +
                        "[attachmentsid]=NULL " +
                        " WHERE [newsid] ='" + item.ID.ToString() + "';";

                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlConnection = null;
                    return item;
                }
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// Return all News-Items starting by startID and returns amount, filtered by groups
        /// </summary>
        /// <param name="amount">How many items should found</param>
        /// <param name="startID">What is the startid, desc</param>
        /// <param name="groups">What groups should loaded</param>
        /// <returns></returns>
        public NewsItem[] getPosts(int amount, Int64 startID, int[] groups)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string groupid="";
                    int j = 0;
                    foreach (int i in groups)
                    {

                        if (j > 0)
                        {
                            groupid = groupid + "," + i.ToString();
                        }
                        else
                        {
                            groupid = i.ToString();
                        }
                        j++;                       
                    }

                    NewsItem SQLItem = new NewsItem();
                    SQLItem.PostGroup = new PostGroupItem();
                List<NewsItem> NewsItemList = new List<NewsItem>();
                    string SQL = "Select TOP " + amount +  " news.newsid, news.newsgroupid, news.headline, news.text, news.createtime, news.updatetime, news.push, news.attachmentsid, newsgroup.newsgroupname " +
                        "from news " +
                        "LEFT JOIN newsgroup on news.newsgroupid = newsgroup.newsgroupid " +
                        "WHERE news.newsid >= " + startID + " AND news.newsgroupid IN(" + groupid + ")"+
                        " ORDER BY newsid DESC";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        SQLItem.ID = Convert.ToInt32(myReader["newsid"]);
                        SQLItem.Message = myReader["text"].ToString();
                        SQLItem.Title= myReader["headline"].ToString();
                        SQLItem.Date = Convert.ToDateTime(myReader["createtime"]);
                        //SQLItem.AuthorID 
                        SQLItem.PostGroup.PostGroupID = Convert.ToInt32(myReader["newsgroupid"]);
                        SQLItem.PostGroup.Name= myReader["newsgroupname"].ToString();
                        NewsItemList.Add(SQLItem);
                        SQLItem = new NewsItem();
                        SQLItem.PostGroup = new PostGroupItem();
                    }
                    sqlConnection.Close();
                    sqlConnection = null;
                    return NewsItemList.ToArray();

                }

            }
            catch (System.Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// Saves new item and assing new id, return the new item 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public NewsItem saveNewPost(NewsItem item)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "INSERT INTO [news] ([newsgroupid],[headline],[text],[createtime],[updatetime],[push],[attachmentsid])  " +
                        "VALUES ("+item.PostGroup.PostGroupID.ToString()+ ",'"+item.Title+ "','"+item.Message+ "',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,0,NULL);" +
                        "SELECT SCOPE_IDENTITY()";

                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    int LastID = Convert.ToInt32(myCommand.ExecuteScalar());
                    sqlConnection.Close();
                    sqlConnection = null;
                    item.ID = LastID;
                    return item;
                }
            }
            catch (System.Exception)
            {

                return null;
            }
        }
    }
}
