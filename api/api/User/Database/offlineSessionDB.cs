using api.Interfaces;
using api.Models;
using BAGCST.api.User.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BAGCST.api.User.Databasae
{
    public class offlineSessionDB : ISessionDB
    {
        private string filePath = Path.Combine(Environment.CurrentDirectory, "offlineDB", "Files", "sessions.csv");
        private string convertToLine(SessionItem item)
        {
            return $"{item.InternalID};" +
                $"{item.DeviceID};" +
                $"{item.UserID};" +
                $"{item.StartTime};" +
                $"{item.ExpirationTime};" +
                $"{item.isActivied};" +
                $"{item.ActivationCode};" +
                $"{item.ShortHashCode};" +
                $"{item.Token}";
        }
        private SessionItem convertToItem(string line)
        {
            string[] args = line.Split(';');
            return new SessionItem
            {
                InternalID = (long)Convert.ToInt64(args[0]),
                DeviceID = (long)Convert.ToInt64(args[1]),
                UserID = (long)Convert.ToInt64(args[2]),
                StartTime = Convert.ToDateTime(args[3]),
                ExpirationTime = Convert.ToDateTime(args[4]),
                isActivied = Convert.ToBoolean(args[5]),
                ActivationCode = args[6],
                ShortHashCode = args[7],
                Token = args[8]
            };
        }
        public SessionItem createNewSession(SessionItem item)
        {
            item.InternalID = getNextFreeSessionID();
            string writeLine = convertToLine(item);

            File.AppendAllLines(filePath, new string[] { writeLine });
            return item;
        }

        public SessionItem findExistingSession(long userID, long deviceID)
        {
            DateTime now = DateTime.Now;
            SessionItem[] sessions = getAllSessions();
            SessionItem[] possibleItems = sessions
                .Where(x => x.DeviceID == deviceID && x.UserID == userID)
                .Where(x => x.StartTime <= now)
                .Where(x => x.ExpirationTime >= now).ToArray();
            if (possibleItems.Length == 1)
            {
                return possibleItems[0];
            }
            else if (possibleItems.Length == 0)
            {
                return null;
            }
            throw new System.Exception("No Unique Session found");
        }

        public SessionItem[] getAllSessions()
        {
            List<SessionItem> sessions = new List<SessionItem>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    SessionItem item = convertToItem(line);
                    sessions.Add(item);
                }
            }

            return sessions.ToArray();
        }

        public SessionItem[] getAllActiveSessions()
        {
            DateTime now = DateTime.Now;
            return getAllSessions()
                .Where(x => x.ExpirationTime <= now)
                .Where(x => x.StartTime >= now)
                .ToArray();
        }

        private long getNextFreeSessionID()
        {
            if (getAllSessions().Length >= 2)
            {
                SessionItem itemWithHighestID = getAllSessions().Aggregate((x1, x2) => x1.InternalID > x2.InternalID ? x1 : x2);
                return itemWithHighestID.InternalID + 1;
            } else if (getAllSessions().Length == 1)
            {
                return getAllSessions()[0].InternalID + 1;
            }
            return 1;
        }

        public SessionItem getSessionItemByActivationCode(string code)
        {
            SessionItem item = this.getAllSessions().Where(x => x.ActivationCode == code).Single();
            return item;
        }

        public SessionItem updateSessionItem(long sessionID, SessionItem item)
        {
            string tempfile = Path.GetTempFileName();

            using (StreamReader sr = new StreamReader(filePath))
            using (StreamWriter sw = new StreamWriter(tempfile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    SessionItem foundItem = convertToItem(line);
                    if (foundItem.InternalID == sessionID)
                    {
                        sw.WriteLine(convertToLine(item));
                    }else
                    {
                        sw.WriteLine(line);
                    }
                } 
            }
            File.Delete(filePath);
            File.Move(tempfile, filePath);
            return item;
        }

        public SessionItem getSessionByInternalID(long sessionID)
        {
            return getAllSessions().SingleOrDefault(x => x.InternalID == sessionID);
        }
    }
}
