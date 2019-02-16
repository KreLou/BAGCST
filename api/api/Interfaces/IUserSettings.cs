using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IUserSettings
    {
        int[] getSubscribedPostGroupsIDs(long userID);

        void setSubscribedPostGroupIDs(long userID, int[] postGroupIDs);
    }
}
