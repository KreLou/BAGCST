using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using api.Interfaces;
using api.Models;

namespace api.offlineDB
{
    public class offlinePostGroupDB : IPostGroupDB
    {
        private static string path_offlineDBFiles = Environment.CurrentDirectory + "\\offlineDB\\Files\\";
        private static string filename_postgroup = path_offlineDBFiles + "postgroups.csv";
        private static string filename_postgroupuser = path_offlineDBFiles + "postgroupuser.csv";

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

                while(!sr.EndOfStream)
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

                while(!sr_pg.EndOfStream)
                {
                    lineparams = sr_pg.ReadLine().Split(";");
                    int _id = Convert.ToInt32(lineparams[0]);

                    lineparams = _id == id ? this.getStringArray(item) : lineparams;
                    
                    sw_pg.WriteLine(lineparams.Aggregate((phrase, word) => $"{phrase};{word}"));
                }
            }

            File.Delete(filename_postgroup);
            File.Move(tempfile_postgroup, filename_postgroup);
            
            File.Delete(filename_postgroupuser);
            File.Move(tempfile_postgroupuser, filename_postgroupuser);
            
            return item;
        }
        public PostGroupItem getPostGroupItem(int id)
        {
            throw new NotImplementedException();
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
                    string[] arr = currentline.Split(";");
                    foreach (string sMID in arr[2].Split(";"))
                    {
                        PostGroupItem item = new PostGroupItem() 
                        {
                            PostGroupID = Convert.ToInt32(arr[0]),
                            Name = arr[1],
                            //IDs prüfen, MemberID wird nicht hier verküpft mit Struct
                            //MemberID = Convert.ToInt32(sMID),
                            IsActive = Convert.ToBoolean(arr[2]),
                            CreationDate = Convert.ToDateTime(arr[3])
                        };
                    }
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
            PostGroupItem RetItem = new PostGroupItem();

            try
            { 
                if (File.Exists(filename_postgroup))
                {
                    int NewID = 0;

                    // read last PostGroup
                    // get last given ID from DB to calc a new ID
                    using (StreamReader sr = new StreamReader(filename_postgroup))
                    {
                        string currentLine;
                        while ((currentLine = sr.ReadLine()) != null)
                        {
                            try 
                            {
                                NewID = Convert.ToInt32(currentLine.Split(";")[0]);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception($"ReadLastPostGroupID-Fehler: {ex.Message}");
                            }
                        }
                    }

                    using (StreamWriter sw = new StreamWriter(filename_postgroup))
                    {
                        RetItem = new PostGroupItem()
                        {
                            PostGroupID     = (NewID + 1),
                            Name            = item.Name,
                            IsActive        = item.IsActive,
                            CreationDate    = item.CreationDate,
                            EditDate        = item.EditDate
                        };
                        sw.WriteLine(RetItem);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodInfo.GetCurrentMethod()}-Fehler: {ex.Message}");
            }

            return RetItem;
 
        }
    }
}