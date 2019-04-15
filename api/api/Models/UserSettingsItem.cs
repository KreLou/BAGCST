using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class UserSettingsItem
    {
        /// <summary>
        /// Subscribed PostGroups
        /// </summary>
        public PostGroupItem[] SubscribedPostGroups { get; set; }
    }
}
