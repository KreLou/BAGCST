using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api.offlineDB
{
    public class offlineUserDeviceDB : IUserDeviceDB
    {
        private string filepath = Path.Combine(Environment.CurrentDirectory, "offlineDB", "Files", "userDevice.csv");

        private string convertToString(UserDeviceItem item)
        {
            return $"{item.DeviceID};{item.DeviceName};{item.UserID};{item.CreateTime}";
        }
        private UserDeviceItem convertToItem(string line)
        {
            string[] args = line.Split(';');
            UserDeviceItem item = new UserDeviceItem
            {
                DeviceID = (long) Convert.ToDouble(args[0]),
                DeviceName = args[1],
                UserID = (long) Convert.ToDouble(args[2]),
                CreateTime = Convert.ToDateTime(args[3])
            };
            return item;
        }
        
        public UserDeviceItem createNewUserDevice(UserDeviceItem item)
        {
            item.DeviceID = getNextFreeNumber();
            string writeLine = convertToString(item);
            File.AppendAllLines(filepath, new string[] { writeLine });

            return item;
        }

        private long getNextFreeNumber()
        {
            long maxUsed = 0;
            using (StreamReader sr = new StreamReader(filepath))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    UserDeviceItem item = convertToItem(line);
                    if (item.DeviceID >= maxUsed) maxUsed = item.DeviceID;
                }
            }
            return (maxUsed+1);
        }

        public UserDeviceItem getDeviceByNameAndUser(long userID, string deviceName)
        {
            UserDeviceItem[] possibleItems = getAllUserDeviceItems().Where(x => x.UserID == userID && x.DeviceName.ToLower() == deviceName.ToLower()).ToArray();

            if (possibleItems.Length == 0)
            {
                return null;
            }
            else if (possibleItems.Length == 1)
            {
                return possibleItems[0];
            }
            throw new Exception("Not unique Item found");
        }

        public UserDeviceItem[] getAllUserDeviceItems()
        {
            List<UserDeviceItem> items = new List<UserDeviceItem>();

            using (StreamReader sr = new StreamReader(filepath))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    var item = convertToItem(line);
                    items.Add(item);
                }
            }
            return items.ToArray();
        }
    }
}
