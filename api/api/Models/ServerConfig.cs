using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ServerConfig
    {
        public string SQLConnectionString { get; set; }
        public string APIURl { get; set; }
        public string SMTP_Host { get; set; }
        public int SMTP_Port { get; set; }
        public bool SMTP_UseCurrentUser { get; set; }
        public string SMTP_User { get; set; }
        public string SMTP_Password { get; set; }
        public string SMTP_SendAs { get; set; }
    }
}
