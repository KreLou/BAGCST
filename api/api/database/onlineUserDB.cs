using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        /// <summary>
        /// Edit UserItem by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public UserItem editUserItem(long id, UserItem item)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {

                    //DSGVO fehlt im Model
                    //"[usertypid]='" + item.??? + "'" +
                    string SQL = "UPDATE [user] SET " +
                        "[mail]='" + item.Email + "'," +
                        "[lastname]='" + item.Lastname + "'," +
                        "[firstname]='" + item.Firstname + "'," +
                        "[studentnumber]='" + item.Username + "',"+
                        "[groupid]='" + item.StudyGroup.ID.ToString() + "'," +
                        "[usertypid]='" + item.UserType.ID.ToString() + "'," +
                        "[dsgvo]='" + item.DSGVO.ToString() + "'" +
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
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {

                    UserItem SQLItem = new UserItem();
                    SQLItem.UserType = new UserType();
                    SQLItem.StudyGroup = new StudyGroup();

                    string SQL = "SELECT[userid],[mail],[lastname],[firstname],[dsgvo],[studentnumber], [usertyp].usertypid, [usertyp].[usertypname],[group].[groupname], [group].[groupid],[group].[groupnameshort],[group].[active] " +
                        " FROM [user] WHERE [studentnumber]='" + username + "'" +
                        " INNER JOIN[usertyp] on[user].usertypid = usertyp.usertypid " +
                        " INNER JOIN[group] on[user].groupid = [group].[groupid] " +
                        ";";

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
                        SQLItem.DSGVO = Convert.ToBoolean(myReader["dsgvo"]);

                        SQLItem.UserType.ID = Convert.ToInt32(myReader["usertypid"]);
                        SQLItem.UserType.Name = myReader["usertypname"].ToString();

                        SQLItem.StudyGroup.ID = Convert.ToInt32(myReader["groupid"]);
                        SQLItem.StudyGroup.LongName = myReader["groupname"].ToString();
                        SQLItem.StudyGroup.ShortName = myReader["groupnameshort"].ToString();
                        SQLItem.StudyGroup.Active = Convert.ToBoolean(myReader["groupid"]);
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

        public UserItem getUserItem(long id)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {

                    UserItem SQLItem = new UserItem();
                    SQLItem.UserType = new UserType();
                    SQLItem.StudyGroup = new StudyGroup();

                    string SQL = "SELECT[userid],[mail],[lastname],[firstname],[dsgvo],[studentnumber], [usertyp].usertypid, [usertyp].[usertypname],[group].[groupname], [group].[groupid],[group].[groupnameshort],[group].[active] " +
                        " FROM [user] " +
                        " INNER JOIN[usertyp] on[user].usertypid = usertyp.usertypid " +
                        " INNER JOIN[group] on[user].groupid = [group].[groupid] "+
                        " WHERE [userid]=" + id.ToString() + ";";

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
                        SQLItem.DSGVO = Convert.ToBoolean(myReader["dsgvo"]);

                        SQLItem.UserType.ID = Convert.ToInt32(myReader["usertypid"]);
                        SQLItem.UserType.Name = myReader["usertypname"].ToString();

                        SQLItem.StudyGroup.ID = Convert.ToInt32(myReader["groupid"]);
                        SQLItem.StudyGroup.LongName = myReader["groupname"].ToString();
                        SQLItem.StudyGroup.ShortName = myReader["groupnameshort"].ToString();
                        SQLItem.StudyGroup.Active = Convert.ToBoolean(myReader["groupid"]);

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
                    SQLItem.UserType = new UserType();
                    SQLItem.StudyGroup = new StudyGroup();
                    List<UserItem> list = new List<UserItem>();
                    string SQL = "SELECT[userid],[mail],[lastname],[firstname],[dsgvo],[studentnumber], [usertyp].usertypid, [usertyp].[usertypname],[group].[groupname], [group].[groupid],[group].[groupnameshort],[group].[active] " +
                        " FROM [user] " +
                        " INNER JOIN[usertyp] on[user].usertypid = usertyp.usertypid " +
                        " INNER JOIN[group] on[user].groupid = [group].[groupid];";

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
                        SQLItem.DSGVO = Convert.ToBoolean(myReader["dsgvo"]);

                        SQLItem.UserType.ID = Convert.ToInt32(myReader["usertypid"]);
                        SQLItem.UserType.Name= myReader["usertypname"].ToString();

                        SQLItem.StudyGroup.ID = Convert.ToInt32(myReader["groupid"]);
                        SQLItem.StudyGroup.LongName = myReader["groupname"].ToString();
                        SQLItem.StudyGroup.ShortName = myReader["groupnameshort"].ToString();
                        SQLItem.StudyGroup.Active = Convert.ToBoolean(myReader["groupid"]);

                        list.Add(SQLItem);
                        //if (SQLItem.Active) list.Add(SQLItem);                        
                        SQLItem = new UserItem();
                        SQLItem.UserType = new UserType();
                        SQLItem.StudyGroup = new StudyGroup();
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
                        "VALUES('" + item.Email + "','" + item.Lastname + "','" + item.Firstname + "','"+ item.DSGVO.ToString() +"','" + item.Username + "','"+item.StudyGroup.ID.ToString()+"','"+item.UserType.ID.ToString()+"');" +
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
