using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Right
    {
        /// <summary>
        /// Primary key of the Rights
        /// </summary>
        public int RightID { get; set; }

        /// <summary>
        /// Path of the app
        /// </summary>
        //[Required]
        public string Path { get; set; }
    }
}
