using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;
using System.IO;

namespace api.Databases
{
    public class offlineDB_contacts : IRightsDB
    {
        private string csvFile = Environment.CurrentDirectory + "\\offlineDB\\Files\\rights.csv";

        /// <summary>
        /// returns a Right based on the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Right|null</returns>
        public Right getRight(int id)
        {
            Right[] rights = getAllRights();
            foreach (Right right in rights)
            {
                if (right.RightID == id)
                {
                    return right;
                }
            }
            return null;
        }

        /// <summary>
        /// returns array of Rights
        /// </summary>
        /// <returns>Right[]</returns>
        public Right[] getAllRights()
        {
            List<Right> list = new List<Right>();
            using (StreamReader reader = new StreamReader(csvFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    Right right = new Right()
                    {
                        RightID = Convert.ToInt32(args[0]),
                        Path = args[1],
                    };

                    list.Add(right);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// creates a Right based on the given Right
        /// </summary>
        /// <param name="right"></param>
        /// <returns>Right</returns>
        public Right createRight(Right right)
        {
            //1. Generate ID
            Right[] rights = getAllRights();
            int id = 0;
            foreach (Right right_ in rights)
            {
                if (right_.RightID >= id)
                {
                    id = right_.RightID;
                }
            }
            id++;
            right.RightID = id;

            //2. Save Right
            File.AppendAllLines(csvFile, new string[] { right.RightID + ";" + right.Path });

            //3. Return Right
            return getRight(id);
        }

        /// <summary>
        /// edits the Right based on the given Right except for the ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Right"></param>
        /// <returns>Right</returns>
        public Right editRight(int id, Right right)
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
                        writer.WriteLine(id + ";" + right.Path);
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            File.Delete(csvFile);
            File.Move(tempFile, csvFile);
            return getRight(id);
        }

        /// <summary>
        /// deletes the Right based on the given ID
        /// </summary>
        /// <param name="id"></param>
        public void deleteRight(int id)
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
    }
}
