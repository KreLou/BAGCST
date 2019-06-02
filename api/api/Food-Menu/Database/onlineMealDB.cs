using System;
using BAGCST.api.FoodMenu.Database;
using BAGCST.api.FoodMenu.Models;
using System.Data.SqlClient;
using api.database;
using System.Collections.Generic;

namespace BAGCST.api.FoodMenu.Database
{
    public class onlineMealDB : IMealDB
    {
        SqlConnection sqlConnection = null;
        public void deleteMeal(int id)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();

            try
            {
                using (sqlConnection)
                {
                    string SQL = "DELETE FROM [meal] WHERE [mealid] ='" + id.ToString() + "';";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlConnection = null;
                    return;
                }

            }
            catch (System.Exception)
            {

            }
        }

        public MealItem editMeal(int id, MealItem meal)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "UPDATE [meal] SET [foodplaceid]='" + meal.Place.ToString() + "',[mealname]='" + meal.MealName + "'," +
                        "[description]='" + meal.Description + "'" +
                        " WHERE [mealid] ='" + id.ToString() + "';";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlConnection = null;
                    return meal;
                }
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public MealItem getMealItem(int MealID)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "";
                    MealItem SQLItem = new MealItem();
                    SQLItem.Place = new PlaceItem();

                    SQL = "SELECT [mealid],[mealname],[description],[foodplace].[foodplaceid], [foodplace].[name] " +
                        " FROM [meal] INNER JOIN [foodplace] on [meal].[foodplaceid] = [foodplace].[foodplaceid] " +
                        " WHERE [mealid]='" + MealID.ToString() + "';";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        SQLItem.MealID = Convert.ToInt32(myReader["mealid"]);
                        SQLItem.MealName = myReader["mealname"].ToString();
                        SQLItem.Description = myReader["description"].ToString();
                        SQLItem.Place.PlaceID = Convert.ToInt32(myReader["foodplaceid"].ToString());
                        SQLItem.Place.PlaceName = myReader["name"].ToString();
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

        public MealItem[] getMealItemsByPlaceID(int id)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    MealItem SQLItem = new MealItem();
                    SQLItem.Place = new PlaceItem();
                    List<MealItem> MealItemList = new List<MealItem>();

                    string SQL = "SELECT [mealid],[mealname],[description],[foodplace].[foodplaceid], [foodplace].[name] " +
                        " FROM [meal] INNER JOIN [foodplace] on [meal].[foodplaceid] = [foodplace].[foodplaceid] " +
                        " WHERE [meal].[foodplaceid]='" + id.ToString() + "';";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        SQLItem.MealID = Convert.ToInt32(myReader["mealid"]);
                        SQLItem.MealName = myReader["mealname"].ToString();
                        SQLItem.Description = myReader["description"].ToString();
                        SQLItem.Place.PlaceID = Convert.ToInt32(myReader["foodplaceid"].ToString());
                        SQLItem.Place.PlaceName = myReader["name"].ToString();

                        MealItemList.Add(SQLItem);
                        SQLItem = new MealItem();
                        SQLItem.Place = new PlaceItem();
                    }

                        sqlConnection.Close();
                        sqlConnection = null;
                        return MealItemList.ToArray();
                }
            }
            catch (System.Exception)
            {
                return null;
            }

        }

        public MealItem[] getMeals()
        {
                sqlConnection = null;
                sqlConnection = TimeTableDatabase.getConnection();
                try
                {
                    using (sqlConnection)
                    {
                        MealItem SQLItem = new MealItem();
                        SQLItem.Place = new PlaceItem();
                        List<MealItem> MealItemList = new List<MealItem>();

                        string SQL = "SELECT [mealid],[mealname],[description],[foodplace].[foodplaceid], [foodplace].[name] " +
                            " FROM [meal] INNER JOIN [foodplace] on [meal].[foodplaceid] = [foodplace].[foodplaceid] " +
                            ";";

                        sqlConnection.Open();
                        SqlDataReader myReader = null;
                        SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                        myReader = myCommand.ExecuteReader();

                        while (myReader.Read())
                        {
                            SQLItem.MealID = Convert.ToInt32(myReader["mealid"]);
                            SQLItem.MealName = myReader["mealname"].ToString();
                            SQLItem.Description = myReader["description"].ToString();
                            SQLItem.Place.PlaceID = Convert.ToInt32(myReader["foodplaceid"].ToString());
                            SQLItem.Place.PlaceName = myReader["name"].ToString();

                            MealItemList.Add(SQLItem);
                            SQLItem = new MealItem();
                            SQLItem.Place = new PlaceItem();
                        }

                        sqlConnection.Close();
                        sqlConnection = null;
                        return MealItemList.ToArray();
                    }
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public MealItem saveNewMeal(MealItem meal)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "INSERT INTO [meal] ([foodplaceid],[mealname],[description]) " +
                        "VALUES('" + meal.Place.PlaceID.ToString() + "','" + meal.MealName + "','" + meal.Description+ "');" +
                        "SELECT SCOPE_IDENTITY();";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    int LastID = Convert.ToInt32(myCommand.ExecuteScalar());
                    sqlConnection.Close();
                    sqlConnection = null;
                    return getMealItem(LastID);
                }
            }
            catch (System.Exception ex)
            {

                return null;
            }
        }

        public int selectMealIDFromOtherInformation(MealItem meal)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "";
                    MealItem SQLItem = new MealItem();
                    SQLItem.Place = new PlaceItem();

                    SQL = "SELECT [mealid],[mealname],[description],[foodplace].[foodplaceid], [foodplace].[name] " +
                        " FROM [meal] INNER JOIN [foodplace] on [meal].[foodplaceid] = [foodplace].[foodplaceid] " +
                        " WHERE [mealname]='" + meal.MealName + "' AND [meal].[foodplaceid] ='"+meal.Place.PlaceID.ToString()+"';";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        SQLItem.MealID = Convert.ToInt32(myReader["mealid"]);
                        SQLItem.MealName = myReader["mealname"].ToString();
                        SQLItem.Description = myReader["description"].ToString();
                        SQLItem.Place.PlaceID = Convert.ToInt32(myReader["foodplaceid"].ToString());
                        SQLItem.Place.PlaceName = myReader["name"].ToString();
                        sqlConnection.Close();
                        sqlConnection = null;
                        return SQLItem.MealID;
                    }
                    else
                    {
                        sqlConnection.Close();
                        sqlConnection = null;
                        return 0;
                    }
                }

            }
            catch (System.Exception)
            {
                return 0;
            }
        }
    }
}
