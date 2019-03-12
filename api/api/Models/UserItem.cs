namespace api.Models
{
    public class UserItem
    {
        /// <summary>
        /// PK of User
        /// </summary>
        /// <value></value>
        public long UserID { get; set; }

        /// <summary>
        /// User is active?
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// username to identify by human
        /// common: Matrikelnummer
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// firstname of profil
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// lastname of profil
        /// </summary>
        public string Lastname { get; set; }
        
        /// <summary>
        /// email of profil
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// course of student
        /// topic he's learning
        /// e.g. "WI16"
        /// </summary>
        public string StudyCourse { get; set; }

        /// <summary>
        /// group of profil
        /// specific name of group in the course
        /// e.g. "WI16-1"
        /// </summary>
        public string StudyGroup { get; set; }

        /// <summary>
        /// ID of PostGroups, where user can post news
        /// </summary>
        public int[] PostGroups { get; set; }
        
        /// <summary>
        /// ID for PostGroups, which user subscribs
        /// </summary>
        public int[] SubscribedPostGroups { get; set; }
    }
}