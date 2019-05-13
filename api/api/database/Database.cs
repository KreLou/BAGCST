using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace api.database
{
    public static class TimeTableDatabase
    {
        //private static string connectionString = "Data Source =192.168.99.123; Initial Catalog = schulessen; User ID = sa; Password=Viper001!";
<<<<<<< HEAD:api/api/database/TimeTableDatabase.cs
        private static string connectionString = "Data Source =192.168.123.112; Initial Catalog = Stundenplan; User ID = TestLouis; Password=Louis";
        private static string connectionString_timetable = "Data Source =192.168.123.112; Initial Catalog = Stundenplan-Original; User ID = TestLouis; Password=Louis";
=======
        private static string connectionString = "Data Source =192.168.99.129; Initial Catalog = Stundenplan; User ID = sa; Password=Viper001!";
        private static string connectionString_timetable = "Data Source =192.168.99.129; Initial Catalog = Stundenplan-Original; User ID = sa; Password=Viper001!";
>>>>>>> parent of 1424380... Rename Database > TimeTableDatabase:api/api/database/Database.cs
        private static List<SqlConnection> connections = new List<SqlConnection>();
        public static void setConnectionString(string str)
        {
            connectionString = str;
        }
        public static SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static SqlConnection getConnectionTimeTable()
        {
            return new SqlConnection(connectionString_timetable);
        }
    }
}
