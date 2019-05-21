using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class LoginDataItem
    {
        [Required]
        public string DeviceName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
