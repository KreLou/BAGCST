using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class UserType
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
