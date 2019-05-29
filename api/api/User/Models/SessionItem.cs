using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BAGCST.api.User.Models
{
    public class SessionItem
    {
        public string Token { get; set; }
        public long InternalID { get; set;}
        public long DeviceID { get; set; }
        public long UserID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool isActivied { get; set; }
        public string ActivationCode { get; set; }
        public string ShortHashCode { get; set; }

        public void setActivationCode()
        {
            string code = $"BA{DeviceID}Glauchau{UserID}WI{StartTime}{ExpirationTime}16";
            ActivationCode = getMD5(code);
        }
        public void setShortHashCode()
        {
            string code = $"{DeviceID}{UserID}BA-Glacuhau APP";
            ShortHashCode = getMD5(code);
        }

        private string getMD5(string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textHash = Encoding.Default.GetBytes(value);
            byte[] result = md5.ComputeHash(textHash);

            return BitConverter.ToString(result).Replace("-", "");

        }

    }
}
