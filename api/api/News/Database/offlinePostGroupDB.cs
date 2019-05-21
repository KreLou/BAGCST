using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using api.Models;
using BAGCST.api.News.Models;

namespace BAGCST.api.News.Database
{
    public class offlinePostGroupDB : IPostGroupDB
    {
        private static string path_offlineDBFiles = Path.Combine(Environment.CurrentDirectory, "offlineDB","Files");
        private static string filename_postgroup = Path.Combine(path_offlineDBFiles,"postgroups.csv");
        private static string filename_postgroupauthor = Path.Combine(path_offlineDBFiles, "postgroupauthors.csv");

        #region DEBUG Funktion -- am Ende entfernen
        public string[] getStringArray(PostGroupItem item)
        {
            List<string> ret = new List<string>();
            ret.Add(item.PostGroupID.ToString());
            ret.Add(item.Name.ToString());
            ret.Add(item.IsActive.ToString());
            ret.Add(item.CreationDate.ToString());
            ret.Add(item.EditDate.ToString());
            return ret.ToArray();
        }
        public PostGroupItem getItemFromStringArray(string[] inputarray)
        {
            return new PostGroupItem()
            {
                PostGroupID = Convert.ToInt32(inputarray[0]),
                Name = inputarray[1],
                IsActive = Convert.ToBoolean(inputarray[2]),
                EditDate = Convert.ToDateTime(inputarray[3]),
                CreationDate = Convert.ToDateTime(inputarray[4])
            };
        }
        #endregion


        /// <summary>
        /// deletes PostGroup by PostGroupID
        /// </summary>
        /// <param name="PostGroupID"></param>
        public void deletePostGroupItem(int id)
        {
            //TODO delete all references in reference table
            string tempfile = Path.GetTempFileName();

            using (StreamReader sr = new StreamReader(filename_postgroup))
            using (StreamWriter sw = new StreamWriter(tempfile))
            {
                string[] lineparams;
                string line = string.Empty;

                while (!sr.EndOfStream)
                {
                    lineparams = sr.ReadLine().Split(";");
                    if (Convert.ToInt32(lineparams[0]) != id)
                    {
                        sw.WriteLine(lineparams.Aggregate((phrase, word) => $"{phrase};{word}"));
                    }
                }
            }

            File.Delete(filename_postgroup);
            File.Move(tempfile, filename_postgroup);
        }

        public PostGroupItem editPostGroupItem(int id, PostGroupItem item)
        {
            string tempfile_postgroup = Path.GetTempFileName();
            string tempfile_postgroupuser = Path.GetTempFileName();

            using (StreamReader sr_pg = new StreamReader(filename_postgroup))
            using (StreamWriter sw_pg = new StreamWriter(tempfile_postgroup))
            {
                string[] lineparams;
                string line = string.Empty;

                while (!sr_pg.EndOfStream)
                {
                    lineparams = sr_pg.ReadLine().Split(";");
                    int _id = Convert.ToInt32(lineparams[0]);

                    lineparams = _id == id ? this.getStringArray(item) : lineparams;

                    sw_pg.WriteLine(lineparams.Aggregate((phrase, word) => $"{phrase};{word}"));
                }
            }

            File.Delete(filename_postgroup);
            File.Move(tempfile_postgroup, filename_postgroup);

            //File.Delete(filename_postgroupuser);
            //File.Move(tempfile_postgroupuser, filename_postgroupuser);

            return item;
        }
        public PostGroupItem getPostGroupItem(int id)
        {
            PostGroupItem item = null;

            using (StreamReader sr = new StreamReader(filename_postgroup))
            {
                string currentLine = null;
                while((currentLine = sr.ReadLine()) != null)
                {
                    PostGroupItem foundedItem = getPostGroupItemFromStringLine(currentLine);
                    if (foundedItem.PostGroupID == id) item = foundedItem;
                }
            }
            return item;
        }

        /// <summary>
        /// return all groups
        /// </summary>
        /// <returns></returns>
        public PostGroupItem[] getPostGroupItems()
        {
            List<PostGroupItem> lstPGI = new List<PostGroupItem>();

            using (StreamReader sr = new StreamReader(filename_postgroup))
            {
                string currentline = string.Empty;
                while ((currentline = sr.ReadLine()) != null)
                {
                    lstPGI.Add(getPostGroupItemFromStringLine(currentline));
                }
            }

            return lstPGI.ToArray();
        }

        /// <summary>
        /// save complete new Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public PostGroupItem saveNewPostGroupItem(PostGroupItem item)
        {
            try
            {
                if (File.Exists(filename_postgroup))
                {
                    item.PostGroupID = getMaxUsedPostGroupID() + 1;

                    File.AppendAllLines(filename_postgroup, new string[] { getStringFromPostGroupItem(item) });

                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"{MethodInfo.GetCurrentMethod()}-Fehler: {ex.Message}");
            }

            return item;

        }

        private string getStringFromPostGroupItem(PostGroupItem item)
        {
            return item.PostGroupID + ";"
                + item.Name + ";"
                + item.IsActive + ";"
                + item.CreationDate + ";"
                + item.EditDate;
        }

        private PostGroupItem getPostGroupItemFromStringLine(string line)
        {
            string[] args = line.Split(";");
            PostGroupItem item = new PostGroupItem
            {
                PostGroupID = Convert.ToInt32(args[0]),
                Name = args[1],
                IsActive = Convert.ToBoolean(args[2]),
                CreationDate = Convert.ToDateTime(args[3]),
                EditDate = Convert.ToDateTime(args[4])
            };
            return item;
        }


        private int getMaxUsedPostGroupID()
        {
            int max = 0;
            PostGroupItem[] postGroupItems = getPostGroupItems();
            foreach(PostGroupItem postGroup in postGroupItems)
            {
                if (postGroup.PostGroupID > max) max = postGroup.PostGroupID;
            }

            return max;
        }

        public void addUserToPostGroupAuthors(int postGroupID, long userID)
        {
            string[] lines = new string[] { postGroupID + ";" + userID };
            File.AppendAllLines(filename_postgroupauthor, lines);
        }

        public void deleteUserFromPostGroupAuthors(int postGroupID, long userID)
        {
            string tempfile = Path.GetTempFileName();

            using (StreamReader sr = new StreamReader(filename_postgroupauthor))
            using (StreamWriter sw = new StreamWriter(tempfile))
            {
                string currentReadLine;
                while ((currentReadLine = sr.ReadLine()) != null)
                {
                    string[] args = currentReadLine.Split(";");
                    int currentPostGroupID = Convert.ToInt32(args[0]);
                    long currentUserID = Convert.ToInt64(args[1]);

                    //IF the IDs not fit, the line will written to the tempfile
                    if (!((currentPostGroupID == postGroupID) 
                        && (currentUserID == userID)))
                    {
                        sw.WriteLine(currentReadLine);
                    }
                }
            }
            File.Delete(filename_postgroupauthor);
            File.Move(tempfile, filename_postgroupauthor);
        }

        public bool checkIfUserIsPostGroupAuthor(int postGroupID, long userID)
        {
            using (StreamReader sr = new StreamReader(filename_postgroupauthor))
            {
                string currentLine;

                while((currentLine = sr.ReadLine()) != null)
                {
                    string[] args = currentLine.Split(";");
                    int currentPostGroupID = Convert.ToInt32(args[0]);
                    long currentUserID = Convert.ToInt64(args[1]);

                    if ((currentPostGroupID == postGroupID)
                        && (currentUserID == userID))
                    {
                        //Return true if both IDs fit and end the while
                        return true;
                    }

                }
            }
            return false; //Return false as fallback, if not found
        }

        public PostGroupItem[] getPostGroupsWhereUserIsAuthor(long userID)
        {
            List<PostGroupItem> usersGroups = new List<PostGroupItem>();

            using (StreamReader sr = new StreamReader(filename_postgroupauthor))
            {
                string currentLine;

                while ((currentLine = sr.ReadLine()) != null)
                {
                    string[] args = currentLine.Split(";");
                    int currentPostGroupID = Convert.ToInt32(args[0]);
                    long currentUserID = Convert.ToInt64(args[1]);
                    
                    if (currentUserID == userID)
                    {
                        // Move this postgroup to the list
                        usersGroups.Add(getPostGroupItem(currentPostGroupID));
                    }

                }
            }

                return usersGroups.ToArray();
        }
    }
}
