using api.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Selectors;

namespace api.offlineDB
{
    public class offlineUserSettings : IUserSettings
    {
        private string file_subscribedPostGroups = Environment.CurrentDirectory + "\\offlineDB\\Files\\postgroupuser.csv";

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
                    int foundPostGroup = Convert.ToInt32(args[1]);
                    PushNotificationType notificationType = PushNotificationType.Always;

                    if (foundedUser == userID)
                    {
                        PostGroupUserPushNotificationSetting setting = new PostGroupUserPushNotificationSetting
                        {
                            PostGroupID = foundPostGroup,
                            Type = notificationType
                        };
                        subscribedIDs.Add(setting);
                    }
                }
            }
            return subscribedIDs.Distinct().ToArray();
        }

        public void setSubscribedPostGroupIDs(long userID, PostGroupUserPushNotificationSetting[] postGroupIDs)
        {
            string[] lines = new string[postGroupIDs.Length];
            for (int i = 0; i < postGroupIDs.Length; i++)
            {
                lines[i] = userID + ";" + postGroupIDs[i].PostGroupID + ";" + postGroupIDs[i].Type;
            }
            File.AppendAllLines(file_subscribedPostGroups, lines);
        }
    }
}
