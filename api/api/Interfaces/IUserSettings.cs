using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IUserSettingsDB
    {
        PostGroupUserPushNotificationSetting[] getSubscribedPostGroupsSettings(long userID);

        void setSubscribedPostGroupIDs(long userID, PostGroupUserPushNotificationSetting[] postGroupIDs);


    }
}
