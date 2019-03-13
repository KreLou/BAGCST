using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class SessionItem
    {
        public string Token { get; set; }
        public long InternalID { get; set;}
        public long DeviceID { get; set; }
        public long UserID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        

    }
}
