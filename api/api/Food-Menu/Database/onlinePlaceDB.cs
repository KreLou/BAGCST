using System;
using BAGCST.api.FoodMenu.Database;
using BAGCST.api.FoodMenu.Models;
using System.Data.SqlClient;
using api.database;
using System.Collections.Generic;

namespace BAGCST.api.FoodMenu.Database
{
    public class onlinePlaceDB : IPlaceDB
    {
        SqlConnection sqlConnection = null;
        public PlaceItem editPlace(int id, PlaceItem place)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "UPDATE [foodplace] SET [name]='" + place.PlaceName + "'" +
                        " WHERE [foodplaceid] ='" + id.ToString() + "';";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlConnection = null;
                    return place;
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public PlaceItem getPlaceItem(int id)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    PlaceItem SQLItem = new PlaceItem();

                    string SQL = "SELECT [foodplaceid],[name] FROM [foodplace] "+
                        " WHERE [foodplaceid]='" + id.ToString() + "';";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        SQLItem.PlaceID = Convert.ToInt32(myReader["foodplaceid"]);
                        SQLItem.PlaceName = myReader["name"].ToString();
                        sqlConnection.Close();
                        sqlConnection = null;
                        return SQLItem;
                    }
                    else
                    {
                        sqlConnection.Close();
                        sqlConnection = null;
                        return null;
                    }
                }

            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public PlaceItem getPlaceItemByName(string name)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    PlaceItem SQLItem = new PlaceItem();

                    string SQL = "SELECT [foodplaceid],[name] FROM [foodplace] " +
                        " WHERE [name]='" + name + "';";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        SQLItem.PlaceID = Convert.ToInt32(myReader["foodplaceid"]);
                        SQLItem.PlaceName = myReader["name"].ToString();
                        sqlConnection.Close();
                        sqlConnection = null;
                        return SQLItem;
                    }
                    else
                    {
                        sqlConnection.Close();
                        sqlConnection = null;
                        return null;
                    }
                }

            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public PlaceItem[] getPlaces()
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    PlaceItem SQLItem = new PlaceItem();
                    List<PlaceItem> ListPlaceItem = new List<PlaceItem>();
                    string SQL = "SELECT [foodplaceid],[name] FROM [foodplace] " +
                        " ;";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        SQLItem.PlaceID = Convert.ToInt32(myReader["foodplaceid"]);
                        SQLItem.PlaceName = myReader["name"].ToString();
                        ListPlaceItem.Add(SQLItem);
                        SQLItem = new PlaceItem();
                    }

                        sqlConnection.Close();
                        sqlConnection = null;
                        return ListPlaceItem.ToArray();
                }

            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public PlaceItem saveNewPlace(PlaceItem place)
        {

            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "INSERT INTO [foodplace] ([name]) " +
                        "VALUES('" + place.PlaceName + "');" +
                        "SELECT SCOPE_IDENTITY();";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    int LastID = Convert.ToInt32(myCommand.ExecuteScalar());
                    sqlConnection.Close();
                    sqlConnection = null;
                    return getPlaceItem(LastID);
                }
            }
            catch (System.Exception ex)
            {

                return null;
            }
        }
    }
}
