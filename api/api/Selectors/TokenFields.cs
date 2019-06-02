using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Selectors
{
    public static class TokenFields
    {
        public static string UserID { get { return "userid"; } }
        public static string Username { get { return "username"; } }
        public static string Firstname { get { return "firstname"; } }
        public static string Lastname { get { return "lastname"; } }
        public static string DeviceID { get { return "device_id"; } }
        public static string SessionID { get { return "session_id";  } }
    }
}
