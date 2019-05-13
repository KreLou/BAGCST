using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAGCST.api.User.Models
{
    public class UserDeviceItem
    {
        public long DeviceID { get; set; }
        public long UserID { get; set; }
        public DateTime CreateTime { get; set; }
        public string DeviceName { get; set; }
    }
}
