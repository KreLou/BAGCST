using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace api.offlineDB
{
    public class offlineUserDB : IUserDB
    {

        private static string offlineDBPath = Environment.CurrentDirectory + "\\offlineDB";
        private string user_filename = offlineDBPath + "\\Files\\users.csv";

        private IStudyCourseDB studyCourseDB = new offlineStudyCourseDB();
        private IStudyGroupDB studyGroupDB = new offlineStudyGroupDB();

        /// <summary>
        /// Creates the string output for User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string convertUserToLine(UserItem user)
        {
            string course = "";

            if (user.StudyCourse != null) course = user.StudyCourse.ID.ToString();
            return user.UserID + ";" + user.Active + ";" + user.Username + ";" + user.Firstname + ";" + user.Lastname + ";" + user.Email + ";" + user.StudyCourse + ";" + user.StudyGroup; 
        }

        private UserItem getUserFromLine(string line)
        {
            string[] args = line.Split(";");
            UserItem user = new UserItem()
            {
                UserID = (long)Convert.ToInt64(args[0]),
                Active = Convert.ToBoolean(args[1]),
                Username = args[2],
                Firstname = args[3],
                Lastname = args[4],
                Email = args[5],
                StudyCourse = getStudyCourse(args[6]),
                StudyGroup = getStudyGroup(args[7])
            };
            return user;
        }

        private StudyGroup getStudyGroup(string arg)
        {
            if (arg == null || arg == string.Empty) return null;
            return studyGroupDB.getByID(Convert.ToInt32(arg));
        }

        private StudyCourse getStudyCourse(string arg)
        {
            if (arg == null || arg == string.Empty) return null;
            return studyCourseDB.getCourseById(Convert.ToInt32(arg));
        }

        public UserItem editUserItem(long id, UserItem item)
        {
            string pth_tmp = Path.GetTempFileName();
            string writeLine = convertUserToLine(item);

            using(StreamWriter sw = new StreamWriter(pth_tmp))
            using(StreamReader sr = new StreamReader(user_filename))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    UserItem user = getUserFromLine(line);
                     if (user.UserID == id)
                    {
                        sw.WriteLine(writeLine);
                    }else
                    {
                        sw.WriteLine(line);
                    }
                }

            }
            File.Delete(user_filename);
            File.Move(pth_tmp, user_filename);
            
            return getUserItem(id);
        }

        public int[] getSubscribedPostGroups(long id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Search for all active users in file 
        /// </summary>
        /// <returns></returns>
        public UserItem[] getUserItems()
        {
            List<UserItem> list = new List<UserItem>();

            using (StreamReader sr = new StreamReader(this.user_filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    UserItem user = getUserFromLine(line);

                    if (user.Active) list.Add(user);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// Search for User in file, return user or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserItem getUserItem(long id)
        {
            UserItem user = null;

            using (StreamReader sr = new StreamReader(user_filename))
            {
                string line;
                //end if end of file or user is found
                while((line = sr.ReadLine()) != null && user == null)
                {
                    UserItem foundUser = getUserFromLine(line);
                    if (foundUser.UserID == id)
                    {
                        user = foundUser;
                    }
                }
            }

            return user;
        }


        /// <summary>
        /// Create new User
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public UserItem saveNewUserItem(UserItem item)
        {
            UserItem existinguser = this.getUserItem(item.UserID);
            long max = getMaxUsedUserId() + 1;
            

            item.UserID = max;

            File.AppendAllLines(user_filename, new String[] { this.convertUserToLine(item) });
            return item;
        }

        private long getMaxUsedUserId()
        {
            long max = 0;
            UserItem[] users = getUserItems();
            foreach(UserItem user in users)
            {
                if (user.UserID > max) max = user.UserID;
            }
            return max;
        }

        public void deleteUserItem(long id)
        {
            string user_temp_filename = Path.GetTempFileName();

            // deletes Users from DB
            using (StreamReader sr = new StreamReader(user_filename))
            using (StreamWriter sw = new StreamWriter(user_temp_filename))
            {
                string[] lines = sr.ReadToEnd().Split(Environment.NewLine.ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
                List<string> output_lines = lines.OfType<string>().ToList();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    //divide read line into several params
                    string[] lineparams = line.Split(';');
                    if (Convert.ToInt32(lineparams[0]) != id)
                    {
                        output_lines.Add(line);
                    }
                }  
                output_lines.Where(wi => Convert.ToInt32(wi.Split(';')[0]) != id).ToList()
                    .ForEach(e => sw.WriteLine(e));
            }
        }

        public void addToPostGroup(long UserID, int PostGroupID)
        {
            bool Exists = false;
            string pth_postgroupuser = offlineDBPath + "\\files\\BindPOstGroupUser.csv";
            //check, if connection already exists, to avoid duplicates
            using(StreamReader sr = new StreamReader(pth_postgroupuser))
            {
                while (!sr.EndOfStream)
                {
                    string[] lineparams = sr.ReadLine().Split(';');
                    if (Convert.ToInt32(lineparams[0]) == PostGroupID 
                    && Convert.ToInt32(lineparams[1]) == UserID)
                    {
                        Exists = true;
                    }
                }
            }

            // if non existing in this combination add combination
            if (!Exists)
            {
                using(StreamWriter sw = new StreamWriter(offlineDBPath + "\\files\\BindPOstGroupUser.csv", true))
                {
                    sw.WriteLine($"{PostGroupID};{UserID}");
                }
            }

            throw new NotImplementedException();
        }

        public void deleteFromPostGroup(long UserID, int PostGroupID)
        {
            string pth_temp = Path.GetTempFileName();

            using (StreamWriter sw = new StreamWriter(pth_temp))
            using(StreamReader sr = new StreamReader(user_filename))
            {
                while (!sr.EndOfStream)
                {
                    string[] lineparams = sr.ReadLine().Split(';');
                    if (!(Convert.ToInt32(lineparams[0]) == PostGroupID 
                        && Convert.ToInt32(lineparams[1]) == UserID))
                    {
                        sw.WriteLine(lineparams.Aggregate((phrase, word) => $"{phrase};{word}"));
                    }
                }
            }
        }

        public UserItem getUserByName(string username)
        {
            UserItem[] allUsers = getUserItems();

            UserItem[] possibleItems = allUsers
                .Where(x => x.Active == true)
                .Where(x => x.Username.ToLower() == username.ToLower())
                .ToArray();

            if (possibleItems.Length == 1)
            {
                return possibleItems[0];
            }
            if (possibleItems.Length == 0)
            {
                return null;
            }
            throw new System.Exception("Username no unique");
        }
    }
}