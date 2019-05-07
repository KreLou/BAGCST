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
        private static string connectionString = "Data Source =192.168.99.129; Initial Catalog = Stundenplan; User ID = sa; Password=Viper001!";
        private static List<SqlConnection> connections = new List<SqlConnection>();
        public static void setConnectionString(string str)
        {
            connectionString = str;
        }
        public static SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
