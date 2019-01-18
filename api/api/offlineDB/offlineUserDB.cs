using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace api.offlineDB
{
    public class offlineUserDB : IUserDB
    {

        private string user_filename = Environment.CurrentDirectory + "\\offlineDB\\Files\\users.csv";

        /// <summary>
        /// Creates the string output for User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string writeLine(UserItem user)
        {
            return user.MemberID + ";" + user.Active + ";" + user.Username + ";" + user.Firstname + ";" + user.Lastname + ";" + user.Email + ";" + user.StudyCourse + ";" + user.StudyGroup; 
        }

        public UserItem editUser(UserItem item)
        {
            throw new System.NotImplementedException();
        }

        public int[] getSubscribedPostGroupsByUserID(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Search for all active users in file 
        /// </summary>
        /// <returns></returns>
        public UserItem[] getUser()
        {
            List<UserItem> list = new List<UserItem>();

            using (StreamReader sr = new StreamReader(this.user_filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    UserItem user = new UserItem()
                    {
                        MemberID = (long)Convert.ToInt64(args[0]),
                        Active = Convert.ToBoolean(args[1]),
                        Username = args[2],
                        Firstname = args[3],
                        Lastname = args[4],
                        Email = args[5],
                        StudyCourse = args[6],
                        StudyGroup = args[7]
                    };

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
        public UserItem getUserByID(int id)
        {
            UserItem user = null;

            using (StreamReader sr = new StreamReader(user_filename))
            {
                string line;
                //end if end of file or user is found
                while((line = sr.ReadLine()) != null && user == null)
                {
                    long user_id = (long)Convert.ToInt64(line.Split(";")[0]);
                    if (user_id == id)
                    {
                        string[] args = line.Split(";");
                        user = new UserItem()
                        {
                            MemberID = user_id,
                            Active = Convert.ToBoolean(args[1]),
                            Username = args[2],
                            Firstname = args[3],
                            Lastname = args[4],
                            Email = args[5],
                            StudyCourse = args[6],
                            StudyGroup = args[7]
                        };
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
        public UserItem saveNewUser(UserItem item)
        {
            UserItem[] existinguser = getUser();
            long max = 1;
            foreach (UserItem exUser in existinguser)
            {
                max = exUser.MemberID > max ? exUser.MemberID +1  : max;
            }

            item.MemberID = max;

            File.AppendAllLines(user_filename, new String[] { this.writeLine(item) });
            return item;
        }

        public void deleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}