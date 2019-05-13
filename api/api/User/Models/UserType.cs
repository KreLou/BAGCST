using System.ComponentModel.DataAnnotations;

namespace BAGCST.api.User.Models
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
