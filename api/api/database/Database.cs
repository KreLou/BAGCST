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
        private static string connectionString = "Data Source =192.168.99.123; Initial Catalog = Stundenplan; User ID = sa; Password=Viper001!";
        private static List<SqlConnection> connections = new List<SqlConnection>();
        public static void setConnectionString(string str)
        {
            connectionString = str;
        }

        public static SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }
        //private void connectionOpen()
        //{
        //    try
        //    {
        //        if (_conn.State == System.Data.ConnectionState.Open)
        //        {
        //            // Database is allrady open
        //        }
        //        else
        //        {
        //            //Databse open
        //            _conn.Open();
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        //Fail to open Database
        //    }
        //}
        //private void connectionClose()
        //{
        //    try
        //    {

        //        if (_conn.State == System.Data.ConnectionState.Closed)
        //        {
        //            // Database is allrady Closed
        //        }
        //        else
        //        {
        //            //Databse closed
        //            _conn.Close();
        //        }
        //        _conn.Close();
        //    }
        //    catch(Exception ex)
        //    {
        //       //Fail to Close Database
        //    }
        //}
    }
}
