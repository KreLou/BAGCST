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
        /// search for MenuItem by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>MenuItem|null</returns>
        MenuItem GetMenuItem(int id);


        /// <summary>
        /// creates a MenuItem
        /// </summary>
        /// <param name="item">MenuItem</param>
        /// <returns>full MenuItem</returns>
        MenuItem saveNewMenu(MenuItem item);

        /// <summary>
        /// edit MenuItem
        /// </summary>
        /// <param name="item"></param>
        /// <returns>edited MenuItem|null</returns>
        MenuItem editMenu(MenuItem item);

        /// <summary>
        /// delete MenuItem
        /// </summary>
        /// <param name="id">ID</param>
        void deleteMenu(int id);

        /// <summary>
        /// gett all Menu 
        /// </summary>
        /// <param name="MenuID">ID</param>
        /// <param name="Date">Date</param>
        /// <param name="Meal">Meal</param>
        /// <param name="Price">Price</param>
        /// <returns>MenuItem|null</returns>
        MenuItem[] GetMenus(int MenuID, DateTime Date, MealItem Meal, decimal Price);

        /// <summary>
        /// gett all Menu 
        /// </summary>
        /// <param name="MenuID">ID</param>
        /// <param name="Price">Price</param>
        /// <returns>MenuItem|null</returns>
        MenuItem[] GetMenus(int MenuID, DateTime Date);

        /// <summary>
        /// gett all Menu 
        /// </summary>
        /// <param name="Date">Date</param>
        MenuItem[] GetMenus( DateTime Date);

        /// <summary>
        /// gett all Menu 
        /// </summary>
        /// <param name="Date">Date</param>
        /// <param name="Meal">Meal</param>
        /// <param name="Price">Price</param>
        /// <returns>MenuItem|null</returns>
        MenuItem[] GetMenus( DateTime Date, MealItem Meal, decimal Price);

    }
}
