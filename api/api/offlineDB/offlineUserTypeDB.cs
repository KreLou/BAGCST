using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api.offlineDB
{
    public class offlineUserTypeDB: IUserTypeDB
    {
        string filepath = Path.Combine(Environment.CurrentDirectory, "offlineDB", "Files", "usertypes.csv");

        public UserType[] getAll()
        {
            List<UserType> list = new List<UserType>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    list.Add(new UserType
                    {
                        ID = Convert.ToInt32(args[0]),
                        Name = args[1]
                    });
                }
            }
            return list.ToArray();
        }

        public bool UserTypeExistsById(int id)
        {
            return getAll().SingleOrDefault(x => x.ID == id) != null;
        }

        public UserType getByID(int id)
        {
            return getAll().SingleOrDefault(x => x.ID == id);
        }
    }
}
