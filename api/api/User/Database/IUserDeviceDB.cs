using BAGCST.api.User.Models;

namespace BAGCST.api.User.Database
{
    public interface IUserDeviceDB
    {
        UserDeviceItem createNewUserDevice(UserDeviceItem item);
        UserDeviceItem getDeviceByNameAndUser(long userID, string deviceName);

        UserDeviceItem[] getAllUserDeviceItems();
    }
}
