using api.database;
using api.Models;
using BAGCST.api.User.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BAGCST.api.User.Controllers
{
    public class onlineUserSettings : IUserSettingsDB
    {
        SqlConnection sqlConnection = null;
        public PostGroupUserPushNotificationSetting[] getSubscribedPostGroupsSettings(long userID)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {

                    PostGroupUserPushNotificationSetting SQLItem = new PostGroupUserPushNotificationSetting();

                    List<PostGroupUserPushNotificationSetting> list = new List<PostGroupUserPushNotificationSetting>();

                    string SQL = "SELECT[newsgroupid],[active] FROM subscribe WHERE userid='"+userID.ToString()+"';";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        SQLItem.PostGroupID = Convert.ToInt32(myReader["newsgroupid"].ToString());
                        SQLItem.PostGroupActive = Convert.ToBoolean(myReader["active"].ToString());
                        list.Add(SQLItem);                       
                        SQLItem = new PostGroupUserPushNotificationSetting();
                    }
                    sqlConnection.Close();
                    sqlConnection = null;
                    return list.ToArray();
                }

            }
            catch (System.Exception)
            {
                sqlConnection.Close();
                sqlConnection = null;
                return null;
            }
        }

        public void setSubscribedPostGroupIDs(long userID, PostGroupUserPushNotificationSetting[] postGroupIDs)
        {
            throw new NotImplementedException();
        }
    }
}
