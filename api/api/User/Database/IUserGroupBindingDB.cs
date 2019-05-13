using System.Collections.Generic;
using BAGCST.api.User.Models;

namespace BAGCST.api.User.Database
{
    public interface IUserGroupBindingDB
    {
        UserGroupBindingItem addUserGroupBinding(int UserID, int GroupID);

        void deleteUserGroupBinding(int UserID, int GroupID);

        List<int> getUsersOfGroup(int[] GroupID);

        List<int> getGroupsOfUser(int[] UserID);
    }
}