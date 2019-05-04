using System;
using api.Models;
using api.Controllers;
using System.Collections.Generic;

namespace api.Controllers
{
    interface IUserGroupBindingDB
    {
        UserGroupBindingItem addUserGroupBinding(int UserID, int GroupID);
        void deleteUserGroupBinding(int UserID, int GroupID);

        List<int> getUsersOfGroup(int GroupID);

        List<int> getGroupsOfUser(int UserID);
    }
}