using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IUserDeviceDB
    {
        UserDeviceItem createNewUserDevice(UserDeviceItem item);
        UserDeviceItem getDeviceByNameAndUser(long userID, string deviceName);

        UserDeviceItem[] getAllUserDeviceItems();
    }
}
