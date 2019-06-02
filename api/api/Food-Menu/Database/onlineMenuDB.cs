using System;
using BAGCST.api.FoodMenu.Database;
using BAGCST.api.FoodMenu.Models;
using System.Data.SqlClient;
using api.database;
using System.Collections.Generic;

namespace BAGCST.api.FoodMenu.Database
{
    public class onlineMenuDB : IMenuDB
    {
        SqlConnection sqlConnection = null;
        public void deleteMenu(int id)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();

            try
            {
                using (sqlConnection)
                {
                    string SQL = "DELETE FROM [menu] WHERE [foodplanid] ='" + id.ToString() + "';";
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
                sqlConnection.Close();
                sqlConnection = null;
                return;
            }
        }

        public MenuItem editMenu(int id, MenuItem item)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "UPDATE [menu] SET [mealid]='" + item.Meal.MealID + "',[date]='" + item.Date.ToString() + "'," +
                        " [price]='" + item.Price.ToString().Replace(",",".") + "'" +
                        " WHERE [foodplanid] ='" + id.ToString() + "';";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlConnection = null;
                    return item;
                }
            }
            catch (System.Exception)
            {
                sqlConnection.Close();
                sqlConnection = null;
                return null;
            }
        }

        public MenuItem[] getAllMenus()
        {
            return getAllMenusByFitlerWhere("");
        }

        public MenuItem[] getFilterdMenus(DateTime startDate, DateTime endDate, int[] placeIDs)
        {
            string SQLWhereIN = "";
            int j = 0;
            foreach (int i in placeIDs)
            {
                if (j == 0)
                { SQLWhereIN += i.ToString(); }
                    else
                { SQLWhereIN +=","+ i.ToString(); }
                j++;
            }

            if (j==0)
            {
                SQLWhereIN = "";
            }
            else
            {
                SQLWhereIN = " AND[meal].[foodplaceid] IN(" + SQLWhereIN + ")";
            }
            string SQLWhere = "WHERE [date] BETWEEN '" + startDate.Date.ToString() + "' AND '" + endDate.Date.ToString() + "' "  + SQLWhereIN ;
            return getAllMenusByFitlerWhere(SQLWhere);
        }

        public MenuItem getMenuItem(int id)
        {
            MenuItem[] item = getAllMenusByFitlerWhere(" WHERE  [foodplanid]='" + id.ToString() + "'");
            if (item != null)
                { return item[0]; }
            else
            {
                return null; 
            }
        }

        private MenuItem[] getAllMenusByFitlerWhere(string SQLWhere)
        {

            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    MenuItem SQLItem = new MenuItem();
                    SQLItem.Meal = new MealItem();
                    SQLItem.Meal.Place = new PlaceItem();
                    List<MenuItem> MenuItemList = new List<MenuItem>();

                    string SQL = "SELECT  [foodplanid],[menu].[mealid],[date],[price],[attachmentsid],[foodplace].[foodplaceid], [foodplace].[name], [meal].[mealname],[meal].[description]" +
                        " FROM [menu] " +
                        "INNER JOIN [meal] on [meal].[mealid] = [menu].[mealid]" +
                        "INNER JOIN [foodplace] on [meal].[foodplaceid] = [foodplace].[foodplaceid] " +
                         SQLWhere +"" +
                         " ORDER BY [date],[mealname] ASC;";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();
                    int j = 0;
                    while (myReader.Read())
                    {
                        SQLItem.Date = Convert.ToDateTime(myReader["date"]);
                        SQLItem.Price = Convert.ToDecimal(myReader["price"]);
                        SQLItem.MenuID = Convert.ToInt32(myReader["foodplanid"]);
                        SQLItem.Meal.MealID = Convert.ToInt32(myReader["mealid"]);
                        SQLItem.Meal.MealName = myReader["mealname"].ToString();
                        SQLItem.Meal.Description = myReader["description"].ToString();
                        SQLItem.Meal.Place.PlaceID = Convert.ToInt32(myReader["foodplaceid"].ToString());
                        SQLItem.Meal.Place.PlaceName = myReader["name"].ToString();

                        MenuItemList.Add(SQLItem);
                        SQLItem = new MenuItem();
                        SQLItem.Meal = new MealItem();
                        SQLItem.Meal.Place = new PlaceItem();
                        j++;
                    }

                    sqlConnection.Close();
                    sqlConnection = null;
                    if (j == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return MenuItemList.ToArray();
                    }
                    
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }

            public MenuItem saveNewMenu(MenuItem item)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "INSERT INTO [menu] ([mealid],[date],[price]) " +
                        "VALUES('" + item.Meal.MealID.ToString() + "','" + item.Date.ToString()+ "','" + item.Price + "');" +
                        "SELECT SCOPE_IDENTITY();";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    int LastID = Convert.ToInt32(myCommand.ExecuteScalar());
                    sqlConnection.Close();
                    sqlConnection = null;
                    return getMenuItem(LastID);
                }
            }
            catch (System.Exception ex)
            {

                return null;
            }
        }
    }
}
