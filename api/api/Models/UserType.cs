using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class UserType
    {
        /// <summary>
        /// unique ID/PK of UserType
        /// </summary>
        [Required]
        public int ID { get; set; }

        /// <summary>
        /// Name of UserType e.g. Student, Dozent
        /// </summary>
        public string Name { get; set; }
    }
}
