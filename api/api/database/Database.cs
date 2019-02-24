using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace api.database
{
    public class Database
    {
        private SqlConnection _conn;
        public Database(string connectionString)
        {
            try
            {
                _conn = new SqlConnection(connectionString);
                Console.Write("Verbindun offen");
            }
            catch (Exception ex)
            {
                //Fail to Connect the Database
            }
        }
        private void connectionOpen()
        {
            try
            {
                if (_conn.State == System.Data.ConnectionState.Open)
                {
                    // Database is allrady open
                }
                else
                {
                    //Databse open
                    _conn.Open();
                }
                
            }
            catch (Exception ex)
            {
                //Fail to open Database
            }
        }
        private void connectionClose()
        {
            try
            {

                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    // Database is allrady Closed
                }
                else
                {
                    //Databse closed
                    _conn.Close();
                }
                _conn.Close();
            }
            catch(Exception ex)
            {
               //Fail to Close Database
            }
        }
    }
}
