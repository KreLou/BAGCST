

using BAGCST.api.User.Models;

namespace BAGCST.api.User.Database
{
    public interface IUserSettingsDB
    {
        PostGroupUserPushNotificationSetting[] getSubscribedPostGroupsSettings(long userID);

        void setSubscribedPostGroupIDs(long userID, PostGroupUserPushNotificationSetting[] postGroupIDs);


    }
}
