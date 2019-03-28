using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class ContactItem
    {
        /// <summary>
        /// Primary key of contacts
        /// Primary key of a contact
        /// </summary>
        public int ContactID { get; set; }

        /// <summary>
        /// Firstname of the person
        /// </summary>
        [Required]
        public string Firstname { get; set; }

        /// <summary>
        /// Lastname with title of the person
        /// </summary>
        [Required]
        public string Lastname { get; set; }

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
        /// Room description, possibly with letters in front of, that's why it's string
        /// </summary>
        public string Room { get; set; }
           
        /// <summary>
        /// Responsibilities of the person (course manager, office,...)
        /// </summary>
        public string Responsibility { get; set; }

        /// <summary>
        /// The course (f.a. WI)
        /// </summary>
        public string Course { get; set; }

        /// <summary>
        /// The persons type (lecturer, temporary speaker, student)
        /// </summary>
        public string Type { get; set; }
    }
}
