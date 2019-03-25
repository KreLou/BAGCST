using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IMenuDB
    {

        /// <summary>
        /// creates a MenuItem
        /// </summary>
        /// <param name="item">MenuItem</param>
        /// <returns>full MenuItem</returns>
        MenuItem saveNewMenu(MenuItem item);

        /// <summary>
        /// edit MenuItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>edited MenuItem|null</returns>
        MenuItem editMenu(int id, MenuItem item);

        /// <summary>
        /// delete MenuItem
        /// </summary>
        /// <param name="id">ID</param>
        void deleteMenu(int id);

        /// <summary>
        /// search for MenuItem by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>MenuItem|null</returns>
        MenuItem getMenuItem(int id);

        /// <summary>
        /// get all Menu 
        /// </summary>
        MenuItem[] getAllMenus();

        /// <summary>
        /// Select all Menus from start to end, only in this placed
        /// Order: 
        /// 1. Menu.Date asc, 
        /// 2. Menu.Meal.Place.PlaceName asc, 
        /// 3. Menu.Meal.MealName asc
        /// </summary>
        /// <param name="startDate">First day</param>
        /// <param name="endDate">last day</param>
        /// <param name="placeIDs">Only this PlaceIDs</param>
        /// <returns></returns>
        MenuItem[] getFilterdMenus(DateTime startDate, DateTime endDate, int[] placeIDs);

    }
}
