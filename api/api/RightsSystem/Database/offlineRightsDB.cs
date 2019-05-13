using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using System.IO;
using BAGCST.api.RightsSystem.Models;

namespace BAGCST.api.RightsSystem.Database
{
    public class offlineRightsDB : IRightsDB
    {
        private string csvFile = Path.Combine(Environment.CurrentDirectory,"offlineDB","Files","rights.csv");

        /// <summary>
        /// returns a Right based on the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Right|null</returns>
        public RightItem getRight(int id)
        {
            RightItem[] rights = getAllRights();
            foreach (RightItem right in rights)
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
        public RightItem[] getAllRights()
        {
            List<RightItem> list = new List<RightItem>();
            using (StreamReader reader = new StreamReader(csvFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    RightItem right = new RightItem()
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
        public RightItem createRight(RightItem right)
        {
            //1. Generate ID
            RightItem[] rights = getAllRights();
            int id = 0;
            foreach (RightItem right_ in rights)
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
        public RightItem editRight(int id, RightItem right)
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

        public RightItem getRightbyPath(string path)
        {
            RightItem[] allRights = getAllRights();

            RightItem right = allRights.Where(item => item.Path.ToLower() == path.ToLower()).Single();

            return right;
        }
    }
}
