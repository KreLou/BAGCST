using api.Models;
using BAGCST.api.StudySystem.Models;
using System;

namespace BAGCST.api.User.Models
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
        public StudyCourse StudyCourse { get; set; }

        /// <summary>
        /// group of profil
        /// specific name of group in the course
        /// e.g. "WI16-1"
        /// </summary>
        public StudyGroup StudyGroup { get; set; }

        /// <summary>
        /// ID of PostGroups, where user can post news
        /// </summary>
        public PostGroupItem[] PostGroups { get; set; }
        
        /// <summary>
        /// ID for PostGroups, which user subscribs
        /// </summary>
        public PostGroupUserPushNotificationSetting[] SubscribedPostGroups { get; set; }

        /// <summary>
        /// ID and Name of UserType, where user are
        /// e.g. Student or Dorzent
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// DSGVO accepted
        /// </summary>
        public bool DSGVO { get; set; }

        /// <summary>
        /// DSGVO Date accepted/non accepted
        /// </summary>
        public DateTime DSGVODate{ get; set; }
    }
}