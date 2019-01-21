using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class NewsItem
    {
        /// <summary>
        /// unique ID/PK of Post 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Short header
        /// max size: 35 char
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Body message of post
        /// plaintext
        /// </summary>
        public string Message { get; set; }

        //TODO Musst change to PostGroupItem because we need the name of the postgroup
        /// <summary>
        /// ID/ForeignKey of posting group
        /// </summary>
        public int PostGroupID { get; set; }

        /// <summary>
        /// Timestamp of posting
        /// </summary>
        public DateTime Date { get; set; }

        /*
         * Possible Extension: automatic posting at specific time
         */
    }
}
