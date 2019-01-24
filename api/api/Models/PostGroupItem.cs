using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class PostGroupItem
    {   
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
