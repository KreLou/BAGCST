using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Group
    {
        /// <summary>
        /// the ID of the group
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// the groups name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// the groups rights
        /// </summary>
        //[Required]
        public Right[] Rights { get; set; }
    }
}
