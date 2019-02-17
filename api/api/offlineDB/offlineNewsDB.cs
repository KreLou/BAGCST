using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace api.offlineDB
{
    public class offlineNewsDB : INewsDB
    {

        private IPostGroupDB postGroupDatabase = new offlinePostGroupDB();

        private string filename = Environment.CurrentDirectory + "\\offlineDB\\Files\\news.csv";

        /// <summary>
        /// Delete a News-item from db by id
        /// </summary>
        /// <param name="id"></param>
        public void deletePost(int id)
        {
            string tempfile = Path.GetTempFileName();

            using (StreamReader sr = new StreamReader(filename))
            using (StreamWriter sw = new StreamWriter(tempfile))
            {
                string line;
                
                while((line = sr.ReadLine()) != null)
                {
                    int _id = Convert.ToInt32(line.Split(";")[0]);
                    if (_id != id) sw.WriteLine(line);
                }
            }

            File.Delete(filename);
            File.Move(tempfile, filename);
        }

        /// <summary>
        /// Saves new item and assing new id, return the new item 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public NewsItem saveNewPost(NewsItem item)
        {
            if (File.Exists(filename))
            {
                //Need new id
                item.ID = getMaxUsedNewsID() + 1;
                item.PostGroup = postGroupDatabase.getPostGroupItem(item.PostGroup.PostGroupID);

                File.AppendAllLines(filename, new String[] { ConvertFromNewsItemToString(item) });
                return item;
            }
            throw new FileNotFoundException("File " + filename + " not found");
        }

        /// <summary>
        /// Update existing News-Item by replacing 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public NewsItem editPost(NewsItem item)
        {
            string tempfile = Path.GetTempFileName();

            using (StreamReader sr = new StreamReader(filename))
            using (StreamWriter sw = new StreamWriter(tempfile))
            {
                string line;

                while((line = sr.ReadLine()) != null)
                {
                    int id = Convert.ToInt32(line.Split(";")[0]);
                    if (id == item.ID)
                    {
                        //override the line with the new parameters
                        line = ConvertFromNewsItemToString(item);
                    }
                    //Saves the line in the temp.File
                    sw.WriteLine(line);
                }
            }

            File.Delete(filename);
            File.Move(tempfile, filename);
            return item;
        }

        /// <summary>
        /// Return all News-Items starting by startID and returns amount, filtered by groups
        /// </summary>
        /// <param name="amount">How many items should found</param>
        /// <param name="startID">What is the startid, desc</param>
        /// <param name="groups">What groups should loaded</param>
        /// <returns></returns>
        public NewsItem[] getPosts(int amount, int startID, int[] groups)
        {

            List<NewsItem> list = new List<NewsItem>();

            using (StreamReader sr = new StreamReader(filename))
            {
                List<string> lines = new List<string>();

                //while (!sr.EndOfStream) lines.Add(sr.ReadLine());
                //list.Add(lines.ForEach(line => line.Split(";").ToList().ForEach(itm => new NewsItem(GroupID =  ))

                string currentline;
                while ((currentline = sr.ReadLine()) != null)
                {
                    NewsItem item = ConvertFromStringToNewsItem(currentline);

                    //TODO What is with PostGroup == null?
                    if ((item.ID <= startID || startID == 0)
                        && (item.PostGroup != null) //If no postgroup found (because pg is deleted)
                        && (groups.Length > 0 && groups.Contains(item.PostGroup.PostGroupID))
                        && (item.PostGroup.IsActive)) //Only Articles from active post-groups
                    {
                        list.Add(item);
                    };
                }
            }

            list = list.OrderBy(item => item.ID).ToList();

            while (list.Count > amount)
            {
                list.RemoveAt(0);
            }

            list.Reverse();
            return list.ToArray();
        }

        private string ConvertFromNewsItemToString(NewsItem item)
        {
            return item.ID + ";"
                + item.PostGroup.PostGroupID + ";"
                + item.AuthorID + ";"
                + item.Date + ";"
                + item.Title + ";"
                + item.Message;
        }

        private NewsItem ConvertFromStringToNewsItem(string line)
        {
            string[] args = line.Split(";");
            NewsItem item = new NewsItem
            {
                ID = Convert.ToInt32(args[0]),
                PostGroup = postGroupDatabase.getPostGroupItem(Convert.ToInt32(args[1])),
                AuthorID = Convert.ToInt64(args[2]),
                Date = Convert.ToDateTime(args[3]),
                Title = args[4],
                Message = args[5]
            };
            return item;
        }

        private int getMaxUsedNewsID()
        {
            int max = 0;
            using (StreamReader sr = new StreamReader(filename))
            {
                string currentLine = null;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    NewsItem item = ConvertFromStringToNewsItem(currentLine);
                    if (item.ID > max) max = item.ID;
                }
            }
            return max;
        }
    }
}
