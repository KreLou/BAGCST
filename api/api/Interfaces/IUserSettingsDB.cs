using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IUserSettingsDB
    {

        UserSettingsItem getUserSettings(long userID);

        void setUserSettings(long userID, UserSettingsItem settings);


    }
}
