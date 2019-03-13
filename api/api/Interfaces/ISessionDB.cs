using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ISessionDB
    {
        SessionItem findExistingSession(long userID, long deviceID);

        SessionItem createNewSession(SessionItem item);

        SessionItem[] getAllSessions();

        SessionItem[] getAllActiveSessions();
    }
}
