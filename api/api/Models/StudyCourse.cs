using System.ComponentModel.DataAnnotations;

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
