using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public enum enmPostGroupItem
    {
        PostGroupID = 0,
        Name = 1,
        IsActive = 2,
        CreationDate = 3,
        EditDate = 4
    }

    public class PostGroupItem
    {
        #region DEBUG Funktion -- am Ende entfernen
        public string[] getStringArray()
        {
            List<string> ret = new List<string>();
            ret.Add(this.PostGroupID.ToString());
            ret.Add(this.Name.ToString());
            ret.Add(this.IsActive.ToString());
            ret.Add(this.CreationDate.ToString());
            ret.Add(this.EditDate.ToString());
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
        /// unique ID/PK of Group
        /// FK:
        /// - Posts (NewsFeed)
        /// </summary>
        public int PostGroupID { get; set; }
        
        /// <summary>
        /// Name of posting group (colloquial)
        /// max size: 35 char
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// active state of post group 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Timestamp of creation
        /// </summary>0
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Timestamp of edit
        /// </summary>
        public DateTime EditDate { get; set; }   
    }
}
