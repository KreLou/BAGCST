using api.Models;
using BAGCST.api.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAGCST.api.User.Database
{
    public interface ISessionDB
    {
        SessionItem findExistingSession(long userID, long deviceID);

        SessionItem createNewSession(SessionItem item);

        SessionItem[] getAllSessions();

        SessionItem[] getAllActiveSessions();

        SessionItem getSessionItemByActivationCode(string code);

        SessionItem updateSessionItem(long sessionID, SessionItem item);

        SessionItem getSessionByInternalID(long sessionID);
    }
}
