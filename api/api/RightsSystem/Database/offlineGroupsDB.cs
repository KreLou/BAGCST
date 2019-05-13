using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using System.IO;
using BAGCST.api.RightsSystem.Models;

namespace BAGCST.api.RightsSystem.Database
{
    public class offlineGroupsDB : IGroupsDB
    {
        private string csvFile = Path.Combine(Environment.CurrentDirectory,"offlineDB","Files","groups.csv");
        List<RightItem> rightsTemp;

        /// <summary>
        /// returns a Group based on the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Group|null</returns>
        public GroupItem getGroup(int id)
        {
            GroupItem[] groups = getAllGroups();
            foreach (GroupItem group in groups)
            {
                if (group.ID == id)
                {
                    return group;
                }
            }
            return null;
        }

        /// <summary>
        /// returns array of Groups
        /// </summary>
        /// <returns>Group[]</returns>
        public GroupItem[] getAllGroups()
        {
            List<GroupItem> list = new List<GroupItem>();
            using (StreamReader reader = new StreamReader(csvFile))
            {
                 rightsTemp = new List<RightItem>();
                 string line;
                 while ((line = reader.ReadLine()) != null)
                 {
                     string[] args = line.Split(";");
                     GroupItem group = new GroupItem()
                     {
                         ID = Convert.ToInt32(args[0]),
                         Name = args[1],
                     };

                     //filling Rights is more complex bc it's an array
                     
                     List<string> rightIDs = args[2].Split(",").ToList();
                     offlineRightsDB dbRights = new offlineRightsDB();
                     foreach (string rightID in rightIDs)
                     {
                            rightsTemp.Add(dbRights.getRight(Convert.ToInt32(rightID)));
                     }
                     
                     foreach(RightItem right in rightsTemp.ToArray())
                     {
                        if(right == null)
                        {
                            rightsTemp.Remove(right);
                        }
                     }
                     group.Rights = rightsTemp.ToArray();
                     rightsTemp.Clear();
                     list.Add(group);
                 }
            }
            return list.ToArray();
        }

        /// <summary>
        /// creates a Group based on the given Group
        /// </summary>
        /// <param name="group"></param>
        /// <returns>Group</returns>
        public GroupItem createGroup(GroupItem group)
        {
            //1. Generate ID
            GroupItem[] groups = getAllGroups();
            int id = 0;

            foreach (GroupItem group_ in groups)
            {
                if (group_.ID >= id)
                {
                    id = group_.ID;
                }
            }
            id++;
            group.ID = id;

            //2. Save Group
            File.AppendAllLines(csvFile, new string[] { group.ID + ";" + group.Name + ";" + rightsToString(group.Rights) });

            //3. Return Group
            return getGroup(id);
        }

        /// <summary>
        /// edits the Group based on the given Group except for the ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Group"></param>
        /// <returns>Group</returns>
        public GroupItem editGroup(int id, GroupItem group)
        {
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(csvFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (Convert.ToInt32(line.Split(";")[0]) == id)
                    {
                        writer.WriteLine(id + ";" + group.Name + ";" + rightsToString(group.Rights));
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            File.Delete(csvFile);
            File.Move(tempFile, csvFile);
            return getGroup(id);
        }

        /// <summary>
        /// deletes the Group based on the given ID
        /// </summary>
        /// <param name="id"></param>
        public void deleteGroup(int id)
        {
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(csvFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (Convert.ToInt32(line.Split(";")[0]) != id)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            File.Delete(csvFile);
            File.Move(tempFile, csvFile);
        }

        //TODO UserID from Token
        //long userID = 1;

        private string groupUser_csv = Path.Combine(Environment.CurrentDirectory,"offlineDB","Files","groupUser.csv");
        public int[] getGroupsByUser(long userID)
        {
            List<int> associatedGroups = new List<int>();

            using (StreamReader sr = new StreamReader(groupUser_csv))
            {
                string currentLine = null;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    string[] args = currentLine.Split(";");
                    long foundUser = Convert.ToInt64(args[0]);
                    int foundGroup = Convert.ToInt32(args[1]);

                    if (foundUser == userID)
                    {
                        associatedGroups.Add(foundGroup);
                    }
                }
            }
            return associatedGroups.Distinct().ToArray();
        }

        public void setGroupsForUser(long userID, int[] groupIDs)
        {
            string[] lines = new string[groupIDs.Length];
            for (int i = 0; i > groupIDs.Length; i++)
            {
                lines[i] = userID + ";" + groupIDs[i];
            }
            File.AppendAllLines(groupUser_csv, lines);
        }

        public string rightsToString(RightItem[] rights)
        {
            string string_out = new string("");
            foreach(RightItem right in rights)
            {
                string_out += right.RightID.ToString();

                if(right != rights.Last())
                {
                    string_out += ",";
                }
            }

            return string_out;
        }
    }
}
