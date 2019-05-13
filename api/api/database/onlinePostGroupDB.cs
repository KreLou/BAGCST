using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace api.database
{
    public class onlinePostGroupDB : IPostGroupDB
    {
        SqlConnection sqlConnection = null;


        /// <summary>
        /// save complete new AuthorsToPostGroup
        /// </summary>
        /// <returns></returns>
        public void addUserToPostGroupAuthors(int postGroupID, long userID)
        {
            try
            {
                using (sqlConnection)
                {
                    if (checkIfUserIsPostGroupAuthor(postGroupID,userID)==false)
                    {
                        sqlConnection = null;
                        sqlConnection = TimeTableDatabase.getConnection();

                        string SQL = "INSERT INTO [postgroupauthor] ([userid],[newsgroupid])  " +
                            "VALUES (" + userID.ToString() + "," + postGroupID.ToString() + ");";
                        sqlConnection.Open();
                        SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                        myCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                        sqlConnection = null;

                    }

                }
            }
            catch (System.Exception)
            {
                sqlConnection.Close();
                sqlConnection = null;
            }
}

        public bool checkIfUserIsPostGroupAuthor(int postGroupID, long userID)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {

                    string SQL = "SELECT [postgroupauthorid] " +
                        " FROM [postgroupauthor] WHERE [newsgroupid]=" + postGroupID.ToString() + " AND [userid] = "+userID.ToString()+";";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        sqlConnection.Close();
                        sqlConnection = null;
                        return true;
                    }
                    else
                    {
                        sqlConnection.Close();
                        sqlConnection = null;
                        return false;
                    }
                }

            }
            catch (System.Exception)
            {
                sqlConnection.Close();
                sqlConnection = null;
                return false;
            }
        }

        /// <summary>
        /// deletes PostGroup by PostGroupID
        /// </summary>
        /// <param name="PostGroupID"></param>
        public void deletePostGroupItem(int id)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "DELETE FROM [newsgroup] WHERE [newsgroupid] ='" + id.ToString() + "';";
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
                sqlConnection.Close();
                sqlConnection = null;
                return;
            }
        }

        public void deleteUserFromPostGroupAuthors(int postGroupID, long userID)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "DELETE FROM [postgroupauthor] WHERE [newsgroupid] =" + postGroupID.ToString() + " AND [userid] = "+userID.ToString()+";";
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
                sqlConnection.Close();
                sqlConnection = null;
                return;
            }
        }

        /// <summary>
        /// Update existing Postgroup-Item by ID and replacing 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public PostGroupItem editPostGroupItem(int id, PostGroupItem item)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "UPDATE [newsgroup] SET " +
                        "[newsgroupname]='" + item.Name+ "'" +
                        " WHERE [newsgroupid] ='" + item.PostGroupID.ToString() + "';";
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
        /// return groupitem by id
        /// </summary>
        /// <returns></returns>
        public PostGroupItem getPostGroupItem(int id)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {

                    PostGroupItem SQLItem = new PostGroupItem();
                    string SQL = "SELECT [newsgroupid],[newsgroupname] " +
                        " FROM[newsgroup] WHERE [newsgroupid]="+id.ToString()+";";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        SQLItem.Name = myReader["newsgroupname"].ToString();
                        SQLItem.PostGroupID = Convert.ToInt32(myReader["newsgroupid"]);
                        sqlConnection.Close();
                        sqlConnection = null;
                        return SQLItem;
                    }
                    else
                    {
                        sqlConnection.Close();
                        sqlConnection = null;
                        return null;
                    }
                }

            }
            catch (System.Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// return all groups in a array
        /// </summary>
        /// <returns></returns>
        public PostGroupItem[] getPostGroupItems()
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {

                    PostGroupItem SQLItem = new PostGroupItem();
                    List<PostGroupItem> lstPGI = new List<PostGroupItem>();
                    string SQL = "SELECT [newsgroupid],[newsgroupname] " +
                        " FROM[newsgroup];";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        SQLItem.Name= myReader["newsgroupname"].ToString();
                        SQLItem.PostGroupID= Convert.ToInt32(myReader["newsgroupid"]);
                        lstPGI.Add(SQLItem);
                        SQLItem = new PostGroupItem();
                    }
                    sqlConnection.Close();
                    sqlConnection = null;
                    return lstPGI.ToArray();
                }

            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public PostGroupItem[] getPostGroupsWhereUserIsAuthor(long userID)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {

                    PostGroupItem SQLItem = new PostGroupItem();
                    List<PostGroupItem> lstPGI = new List<PostGroupItem>();
                    string SQL = "SELECT [newsgroupid],[newsgroupname] " +
                        " FROM[newsgroup]" +
                        "WHERE [userid] = '" + userID.ToString() + "';";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        SQLItem.Name = myReader["newsgroupname"].ToString();
                        SQLItem.PostGroupID = Convert.ToInt32(myReader["newsgroupid"]);
                        lstPGI.Add(SQLItem);
                        SQLItem = new PostGroupItem();
                    }
                    sqlConnection.Close();
                    sqlConnection = null;
                    return lstPGI.ToArray();
                }

            }
            catch (System.Exception)
            {
                sqlConnection.Close();
                sqlConnection = null;
                return null;
            }
        }

        /// <summary>
        /// save complete new Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public PostGroupItem saveNewPostGroupItem(PostGroupItem item)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "INSERT INTO [newsgroup] ([newsgroupname])  " +
                        "VALUES ('" + item.Name.ToString() + "');" +
                        "SELECT SCOPE_IDENTITY()";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    int LastID = Convert.ToInt32(myCommand.ExecuteScalar());
                    sqlConnection.Close();
                    sqlConnection = null;
                    item.PostGroupID = LastID;
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
