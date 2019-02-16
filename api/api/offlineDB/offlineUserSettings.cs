using api.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api.offlineDB
{
    public class offlineUserSettings : IUserSettings
    {
        private string file_subscribedPostGroups = Environment.CurrentDirectory + "\\offlineDB\\Files\\postgroupuser.csv";

        public int[] getSubscribedPostGroupsIDs(long userID)
        {
            List<int> subscribedIDs = new List<int>();
            using (StreamReader sr = new StreamReader(file_subscribedPostGroups))
            {
                string currentLine = null;
                while((currentLine = sr.ReadLine()) != null)
                {
                    string[] args = currentLine.Split(";");
                    long foundedUser = Convert.ToInt64(args[0]);
                    int foundPostGroup = Convert.ToInt32(args[1]);

                    if (foundedUser == userID)
                    {
                        subscribedIDs.Add(foundPostGroup);
                    }
                }
            }
            return subscribedIDs.Distinct().ToArray();
        }

        public void setSubscribedPostGroupIDs(long userID, int[] postGroupIDs)
        {
            string[] lines = new string[postGroupIDs.Length];
            for (int i = 0; i < postGroupIDs.Length; i++)
            {
                lines[i] = userID + ";" + postGroupIDs[i];
            }
            File.AppendAllLines(file_subscribedPostGroups, lines);
        }
    }
}
