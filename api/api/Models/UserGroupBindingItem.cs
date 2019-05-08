using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class UserGroupBindingItem
    {
        /// <summary>
        /// valid UserID of UserItem
        /// </summary>
        [Required]
        public int UserID { get; set; } 

        /// <summary>
        /// valid UserID of UserItem
        /// </summary>
        [Required]        
        public int GroupID { get; set; }
    }
}