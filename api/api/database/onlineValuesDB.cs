using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace api.database
{
    public class onlineValuesDB
    {
        public string getByID(int id)
        {
            using (SqlConnection sql = TimeTableDatabase.getConnection())
            {
                sql.Open();

                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT  *  FROM Tabelle_1 Where id=" + id + ";", sql);
                myReader = myCommand.ExecuteReader();
                String Value = "";

                if (myReader.Read())
                {
                    Value = myReader["test"].ToString();
                }
                sql.Close();
                return Value;
            }
        }

        public void setByID(int id, string value)
        {
            using (SqlConnection sql = TimeTableDatabase.getConnection())
            {
                sql.Open();

                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("Update Tabelle_1 SET test='" + value + "' Where id='" + id + "';", sql);
                myReader = myCommand.ExecuteReader();
                myReader.Close();
                sql.Close();

                //sql = "INSERT INTO `bestellliste` (`id`, `bestellnummer`, `bestelldatum`,`datum`, `mandantenid`, `mandantenartid`, `gesperrt`, `status`) VALUES (NULL, '" + Kdnr + "',  CURRENT_DATE(), '" + Datumstart + "', '" + Kdnr + "', '" + SchulID + "', '0', '0');";
                //cmd = new MySqlCommand(sql, _conn);
                //rdr = cmd.ExecuteReader();
                //LastID = Convert.ToInt32(cmd.LastInsertedId);
                //rdr.Close();
                //Bestellnummer = Kdnr.ToString() + "-" + LastID.ToString();

                //sql = "UPDATE `bestellliste` SET `bestellnummer` = '" + Bestellnummer + "' WHERE `bestellliste`.`id` = '" + LastID + "'; ";
                //cmd = new MySqlCommand(sql, _conn);
                //rdr = cmd.ExecuteReader();
                //rdr.Close();
            }
        }

        public void newID(string value)
        {
            using (SqlConnection sql = TimeTableDatabase.getConnection())
            {
                sql.Open();

                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("INSERT INTO Tabelle_1 (`id`,`test`) VALUES (NULL,'"+value+"');", sql);
                myReader = myCommand.ExecuteReader();
                myReader.Close();

                sql.Close();
            }
        }

        public void deleteByID(int id)
        {
            using (SqlConnection sql = TimeTableDatabase.getConnection())
            {
                sql.Open();

                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("DELETE FROM Tabelle_1 SET Where id='" + id + "';", sql);
                myReader = myCommand.ExecuteReader();
                myReader.Close();
                sql.Close();
            }
        }

    }
}