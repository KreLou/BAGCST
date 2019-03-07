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
        /// gett all Menu 
        /// </summary>
        /// <param name="Date">Date</param>
        MenuItem[] getMenusbyDate( DateTime Date);

        /// <summary>
        /// gett all Menu 
        /// </summary>
        /// <param name="Date">Date</param>
        MenuItem[] getMenus();

    }
}
