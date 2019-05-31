using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BAGCST.api.RightsSystem;

namespace BAGCST.api.RightsSystem.Models
{
    public class RightItem
    {
        /// <summary>
        /// Primary key of the Rights
        /// </summary>
        public int RightID { get; set; }

        /// <summary>
        /// Path of the app
        /// </summary>
        [Required]
        public string Path { get; set; }
    }
}
