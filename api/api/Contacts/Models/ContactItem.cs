using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using api.Models;
using BAGCST.api.User.Models;
using BAGCST.api.StudySystem.Models;

namespace BAGCST.api.Contacts.Models
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
        public string FirstName { get; set; }
 
        /// <summary>
        /// Lastname with title of the person
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
        public StudyCourse Course { get; set; }

        /// <summary>
        /// The persons type (lecturer, temporary speaker, student)
        /// </summary>
        public UserType Type { get; set; }
    }
}
