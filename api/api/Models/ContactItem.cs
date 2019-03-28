using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ContactItem
    {
        /// <summary>
        /// Primary key of a contact
        /// </summary>
        public int ContactID { get; set; }

        /// <summary>
        /// First name of the person
        /// </summary>
        [Required]
        public string FirstName{ get; set; }

        /// <summary>
        /// Last name with title of the person
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Title for ex. Prof Dr.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Telephone number of the person
        /// </summary>
        public string TelNumber { get; set; }

        /// <summary>
        /// E-Mail-Address of the person
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Room description (name, number or both)
        /// </summary>
        public string Room { get; set; }
           
        /// <summary>
        /// Responsibilities of the person (course manager, office,...)
        /// </summary>
        public string Responsibility { get; set; }

        /// <summary>
        /// The course (e.g. WI)
        /// </summary>
        public string Course { get; set; }

        /// <summary>
        /// The persons type (lecturer, temporary speaker, student)
        /// </summary>
        public string Type { get; set; }
    }
}
