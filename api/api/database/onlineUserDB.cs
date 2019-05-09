using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace api.database
{
    public class onlineUserDB : IUserDB
    {
        SqlConnection sqlConnection = null;

        public void addToPostGroup(long UserID, int PostGroupID)
        {
            throw new NotImplementedException();
        }

        public void deleteFromPostGroup(long UserID, int PostGroupID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete User by ID
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void deleteUserItem(long id)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "DELETE FROM [user] WHERE [userid] =" +id.ToString() + ";";
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

        public UserItem editUserItem(long id, UserItem item)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {

                    //DSGVO fehlt im Model
                    // Sudentennummer fehlt im Model "[studentnumber]='" + item.s + "'," 
                    //"[groupid]='" + item. + "'," +
                    //"[usertypid]='" + item.Name + "'" +
                    string SQL = "UPDATE [user] SET " +
                        "[mail]='" + item.Email + "'," +
                        "[lastname]='" + item.Lastname + "'," +
                        "[firstname]='" + item.Firstname + "'," +
                        "[studentnumber]='" + item.Username + "'"+
                        " WHERE [userid] ='" + id.ToString() + "';";
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

        public int[] getSubscribedPostGroups(long UserID)
        {
            throw new NotImplementedException();
        }

        public UserItem getUserByName(string username)
        {
            return this.getUserItems().SingleOrDefault(x => x.Username == username);
        }

        public UserItem getUserItem(long id)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {

                    UserItem SQLItem = new UserItem();
                    string SQL = "SELECT [userid],[mail],[lastname],[firstname],[dsgvo],[studentnumber],[groupid],[usertypid] " +
                        " FROM [user] WHERE [userid]=" + id.ToString() + ";";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        SQLItem.Email = myReader["mail"].ToString();
                        SQLItem.Firstname = myReader["firstname"].ToString();
                        SQLItem.Lastname = myReader["lastname"].ToString();
                        SQLItem.Username = myReader["studentnumber"].ToString();
                        SQLItem.UserID = Convert.ToInt32(myReader["userid"]);
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

        public UserItem[] getUserItems()
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {

                    UserItem SQLItem = new UserItem();
                    List<UserItem> list = new List<UserItem>();
                    string SQL = "SELECT [userid],[mail],[lastname],[firstname],[dsgvo],[studentnumber],[groupid],[usertypid] " +
                        " FROM [user];";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        SQLItem.Email = myReader["mail"].ToString();
                        SQLItem.Firstname = myReader["firstname"].ToString();
                        SQLItem.Lastname = myReader["lastname"].ToString();
                        SQLItem.Username = myReader["studentnumber"].ToString();
                        SQLItem.UserID = Convert.ToInt32(myReader["userid"]);
                        list.Add(SQLItem);
                        //if (SQLItem.Active) list.Add(SQLItem);                        
                        SQLItem = new UserItem();
                    }
                        sqlConnection.Close();
                        sqlConnection = null;
                        return list.ToArray();
                    
                }

            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public UserItem saveNewUserItem(UserItem item)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    //In der SQL Anweisung muss noch Contacttypid und CourseTypID und Verantwortlicher geändert werden.
                    string SQL = "INSERT INTO [contact] " +
                        "([mail],[lastname],[firstname],[dsgvo],[studentnumber], [groupid],[usertypid]) " +
                        "VALUES('" + item.Email + "','" + item.Lastname + "','" + item.Firstname + "','1','" + item.Username + "','1',' 1');" +
                        "SELECT SCOPE_IDENTITY();";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    int LastID = Convert.ToInt32(myCommand.ExecuteScalar());
                    item.UserID = LastID;
                    sqlConnection.Close();
                    sqlConnection = null;
                    return (item);
                }
            }
            catch (System.Exception)
            {

                return null;
            }
        }
    }
}
