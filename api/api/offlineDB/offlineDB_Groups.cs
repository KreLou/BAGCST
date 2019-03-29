using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;
using System.IO;
using api.Databases;

namespace api.offlineDB
{
    public class offlineDB_Groups : IGroupsDB
    {
        private string csvFile = Environment.CurrentDirectory + "\\offlineDB\\Files\\groups.csv";
        List<Right> rightsTemp;

        /// <summary>
        /// returns a Group based on the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Group|null</returns>
        public Group getGroup(int id)
        {
            Group[] groups = getAllGroups();
            foreach (Group group in groups)
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
        public Group[] getAllGroups()
        {
            List<Group> list = new List<Group>();
            using (StreamReader reader = new StreamReader(csvFile))
            {
                    rightsTemp = new List<Right>();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] args = line.Split(";");
                        Group group = new Group()
                        {
                            ID = Convert.ToInt32(args[0]),
                            Name = args[1],
                        };

                        //filling Rights is more complex bc it's an array
                        
                        List<string> rightIDs = args[2].Split(",").ToList();
                        offlineDB_Rights dbRights = new offlineDB_Rights();
                        foreach (string rightID in rightIDs)
                        {
                            rightsTemp.Add(dbRights.getRight(Convert.ToInt32(rightID)));
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
        public Group createGroup(Group group)
        {
            //1. Generate ID
            Group[] groups = getAllGroups();
            int id = 0;
            foreach (Group group_ in groups)
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
        public Group editGroup(int id, Group group)
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

        private string groupUser_csv = Environment.CurrentDirectory + "\\offlineDB\\Files\\groupUser.csv";
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

        public string rightsToString(Right[] rights)
        {
            string string_out = new string("");
            foreach(Right right in rights)
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
