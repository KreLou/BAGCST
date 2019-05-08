using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class StudyCourse
    {
        [Required]
        public int ID { get; set; } 

        [StringLength(2)]
        public string ShortText { get; set; }
    
        public string LongText { get; set; }
    }
}
