using api.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAGCST.api.User.Models
{
    public class PostGroupUserPushNotificationSetting
    {
        public int PostGroupID { get; set; }
        public bool PostGroupActive { get; set; }
        public PushNotificationType Type { get; set; } = PushNotificationType.Always;
    }
}
