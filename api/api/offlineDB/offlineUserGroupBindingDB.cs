using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using api.Models;
using api.Interfaces;
using api.Controllers;

namespace api.offlineDB
{
    public class offlineUserGroupBindingDB : IUserGroupBindingDB
    {
        string filename_offlineUGBDB = Path.Combine(Environment.CurrentDirectory, "offlineDB", "Files", "usergroupbinding.csv");
        List<UserGroupBindingItem> _lstugb = null;
        List<UserGroupBindingItem> lstUGB {
            get {
                if (_lstugb == null)
                {
                    string[] lines = File.ReadAllLines(filename_offlineUGBDB);
                    _lstugb = new List<UserGroupBindingItem>();
                    lines.ToList().ForEach(x => {
                        string[] elem = x.Split(";");
                        UserGroupBindingItem _ugbTmp = new UserGroupBindingItem()
                        {
                            UserID = Convert.ToInt32(elem[0]),
                            GroupID = Convert.ToInt32(elem[1])
                        };
                        lstUGB.Add(_ugbTmp); 
                    });
                }
                return _lstugb;
            }
            set {
                _lstugb = value;
            }
        }

        public UserGroupBindingItem addUserGroupBinding(int UserID, int GroupID)
        {
            UserGroupBindingItem _ugbItem = null;
            _ugbItem = lstUGB.SingleOrDefault(itm => itm.UserID == UserID && itm.GroupID == GroupID);

            //no item found - create new item
            if (_ugbItem == null) 
            {
                _ugbItem = new UserGroupBindingItem()
                {
                    UserID = UserID,
                    GroupID = GroupID
                };
                lstUGB.Add(_ugbItem);
            }
            return _ugbItem;
        }

        public void deleteUserGroupBinding(int UserID, int GroupID)
        {
            string tempFile = Path.GetTempFileName();
            lstUGB.RemoveAll(x => x.UserID == UserID && x.GroupID == GroupID);

            using (StreamWriter writer = new StreamWriter(tempFile))
            {
                lstUGB.ForEach(x => writer.WriteLine($"{UserID};{GroupID}"));
            }
            File.Delete(filename_offlineUGBDB);
            File.Move(tempFile, filename_offlineUGBDB);
        }

        public List<int> getGroupsOfUser(int[] UserIDs)
        {
            
            return lstUGB.Where(x => UserIDs.Contains(x.UserID)).Select(x => x.GroupID).ToList();
        }

        public List<int> getUsersOfGroup(int[] GroupIDs)
        {
            return lstUGB.Where(x => GroupIDs.Contains(x.GroupID)).Select(x => x.UserID).ToList();
        }
    }
}