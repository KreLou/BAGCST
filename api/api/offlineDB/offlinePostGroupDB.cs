using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using api.Interfaces;
using api.Models;

namespace api.offlineDB
{
    public class offlinePostGroupDB : IPostGroupDB
    {
        private string filename = Environment.CurrentDirectory + "\\offlineDB\\Files\\postgroups.csv";
        
        /// <summary>
        /// deletes PostGroup by PostGroupID
        /// </summary>
        /// <param name="PostGroupID"></param>
        public void deletePostGroupItem(int PostGroupID)
        {
            string tempfile = Path.GetTempFileName();

            using (StreamReader sr = new StreamReader(filename))
            using (StreamWriter sw = new StreamWriter(tempfile))
            {
                string[] lineparams;
                string line = string.Empty;

                while(!sr.EndOfStream)
                {
                    lineparams = sr.ReadLine().Split(";");
                    int id = Convert.ToInt32(lineparams[0]);
                    if (id != PostGroupID)
                    {
                        sw.WriteLine(lineparams.Aggregate((phrase, word) => $"{phrase};{word}"));                        
                    }
                }
            }

            File.Delete(filename);
            File.Move(tempfile, filename);
        }

        public PostGroupItem editPostGroup(PostGroupItem item)
        {
            string tempfile = Path.GetTempFileName();

            using (StreamReader sr = new StreamReader(filename))
            using (StreamWriter sw = new StreamWriter(tempfile))
            {
                string[] lineparams;
                string line = string.Empty;

                while(!sr.EndOfStream)
                {
                    lineparams = sr.ReadLine().Split(";");
                    int id = Convert.ToInt32(lineparams[0]);
                    if (id == item.PostGroupID)
                    {
                        
                        // override the line with the new parameters
                        // linq to combine strings
                        // foreach (int MID in item.MemberID)
                        // {
                        //     line = new string[]
                        //     {
                        //             item.PostGroupID.ToString(),
                        //             item.Name,
                        //             MID.ToString(),
                        //             item.IsActive.ToString(),
                        //             item.CreationDate.ToString()
                        //     }.Aggregate((linepart, word) => $"{linepart};{word}");

                        //     //Saves the line in the temp.File
                        //     sw.WriteLine(line);
                        // }
                    }
                }
            }

            File.Delete(filename);
            File.Move(tempfile, filename);
            return item;
        }

        /// <summary>
        /// return all groups
        /// </summary>
        /// <returns></returns>
        public PostGroupItem[] getPostGroups()
        {
            List<PostGroupItem> lstPGI = new List<PostGroupItem>();

            using (StreamReader sr = new StreamReader(filename))
            {
                string currentline = string.Empty;
                while ((currentline = sr.ReadLine()) != null)
                {
                    string[] arr = currentline.Split(";");
                    foreach (string sMID in arr[2].Split(";"))
                    {
                        PostGroupItem item = new PostGroupItem() 
                        {
                            PostGroupID = Convert.ToInt32(arr[0]),
                            Name = arr[1],
                            MemberID = Convert.ToInt32(sMID),
                            IsActive = Convert.ToBoolean(arr[3]),
                            CreationDate = Convert.ToDateTime(arr[4])
                        };
                    }
                }
            }

            return lstPGI.ToArray();
        }

        /// <summary>
        /// modify active state of a group
        /// </summary>
        /// <param name="id">concrete ID of group</param>
        /// <param name="isActive">state of active</param>
        public void updateActiveStatePostGroup(int id, bool isActive)
        {
            string tempname = Path.GetTempFileName();
            string[] lineparams = null;

            using (StreamReader sr = new StreamReader(filename))
            using (StreamWriter sw = new StreamWriter(tempname))
            {
                while (!sr.EndOfStream)
                {
                    lineparams = sr.ReadLine().Split(";");
                    int _id = Convert.ToInt32(lineparams[0]);
                    if (_id == id)
                    {
                        lineparams[3] = isActive.ToString();
                    }
                    sw.WriteLine(lineparams.Aggregate((linepart, param) => $"{linepart};{param}"));
                }
            }
            
            File.Delete(filename);
            File.Move(tempname, filename);
        }

        public PostGroupItem saveNewPostGroup(PostGroupItem item)
        {
            throw new System.NotImplementedException();
        }
    }
}