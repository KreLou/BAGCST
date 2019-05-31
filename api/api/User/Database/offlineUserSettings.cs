using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using api.Selectors;
using BAGCST.api.User.Database;
using BAGCST.api.User.Models;

namespace api.offlineDB
{
    public class offlineUserSettings : IUserSettingsDB
    {
        private string file_subscribedPostGroups = Environment.CurrentDirectory + "\\offlineDB\\Files\\postgroupuser.csv";

        private PostGroupUserPushNotificationSetting convertLineTOItem(string line)
        {
            string[] args = line.Split(";");
            long foundedUser = Convert.ToInt64(args[0]);
            int foundPostGroup = Convert.ToInt32(args[1]);
            bool active = Convert.ToBoolean(args[2]);

            return new PostGroupUserPushNotificationSetting
            {
                PostGroupActive = active,
                PostGroupID = foundPostGroup,
                Type = PushNotificationType.Always
            };
        }

        private string convertItemToLine(PostGroupUserPushNotificationSetting item)
        {
            return $"{item.PostGroupID};{item.PostGroupActive};{item.Type}";
        }

        public PostGroupUserPushNotificationSetting[] getSubscribedPostGroupsSettings(long userID)
        {
            List<PostGroupUserPushNotificationSetting> subscribedIDs = new List<PostGroupUserPushNotificationSetting>();
            using (StreamReader sr = new StreamReader(file_subscribedPostGroups))
            {
                string currentLine = null;
                while((currentLine = sr.ReadLine()) != null)
                {
                    string[] args = currentLine.Split(";");
                    long foundedUser = Convert.ToInt64(args[0]);

                    if (foundedUser == userID)
                    {
                        PostGroupUserPushNotificationSetting setting = convertLineTOItem(currentLine);

                        if (subscribedIDs.SingleOrDefault(x => x.PostGroupID == setting.PostGroupID) == null)
                        {
                            subscribedIDs.Add(setting);
                        }
                    }
                }
            }
            return subscribedIDs.Distinct().ToArray();
        }

        public void setSubscribedPostGroupIDs(long userID, PostGroupUserPushNotificationSetting[] postGroupIDs)
        {
            string path_temp = Path.GetTempFileName();

            using (StreamReader sr = new StreamReader(file_subscribedPostGroups))
            using (StreamWriter sw = new StreamWriter(path_temp))
            {
                string line;
                //Copy all other users
                while((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    long userIDFromLine = Convert.ToInt64(args[0]);

                    if (userID != userIDFromLine)
                    {
                        sw.WriteLine(line);
                    }

                }

                foreach(PostGroupUserPushNotificationSetting setting in postGroupIDs)
                {
                    line = userID + ";" + convertItemToLine(setting);
                    sw.WriteLine(line);
                }
            }
            File.Delete(file_subscribedPostGroups);
            File.Move(path_temp, file_subscribedPostGroups);
        }
    }
}
