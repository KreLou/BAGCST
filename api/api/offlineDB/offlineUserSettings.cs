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
    public class offlineUserSettings : IUserSettingsDB
    {
        private string file_subscribedPostGroups = Path.Combine(Environment.CurrentDirectory,"offlineDB","Files","postgroupuser.csv");

        private readonly offlineUserDB userDB;
        private readonly offlinePostGroupDB postGroupDB;

        public offlineUserSettings()
        {
            this.userDB = new offlineUserDB();
            this.postGroupDB = new offlinePostGroupDB();
        }
        
        public UserSettingsItem getUserSettings(long userID)
        {
            List<PostGroupItem> subscribedIDs = new List<PostGroupItem>();
            using (StreamReader sr = new StreamReader(file_subscribedPostGroups))
            {
                string currentLine = null;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    string[] args = currentLine.Split(";");
                    long foundedUser = Convert.ToInt64(args[0]);
                    int foundPostGroup = Convert.ToInt32(args[1]);

                    if (foundedUser == userID)
                    {
                        subscribedIDs.Add(postGroupDB.getPostGroupItem(foundPostGroup));
                    }
                }
            }

            UserSettingsItem settings = new UserSettingsItem
            {
                SubscribedPostGroups = subscribedIDs.ToArray()
            };
            return settings;
        }

        public void setUserSettings(long userID, UserSettingsItem settings)
        {
            string[] lines = new string[settings.SubscribedPostGroups.Length];
            for (int i = 0; i < settings.SubscribedPostGroups.Length; i++)
            {
                lines[i] = userID + ";" + settings.SubscribedPostGroups[i].PostGroupID;
            }
            File.AppendAllLines(file_subscribedPostGroups, lines);
        }
    }
}
