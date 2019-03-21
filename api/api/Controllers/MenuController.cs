using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.offlineDB;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IMenuDB menuDatabase = getMenuDatabase();

        private IMealDB mealDatabase = getMealDataBase();


        /// <summary>
        /// Returns the current Menu database
        /// </summary>
        /// <returns></returns>
        private static IMenuDB getMenuDatabase()
        {
            return new OfflineMenuDB();
        }

        /// <summary>
        /// Returns the meal database
        /// </summary>
        /// <returns></returns>
        private static IMealDB getMealDataBase()
        {
            return new OfflineMealDB();
        }


        /// <summary>
        /// returns the MenuItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MenuItem|NotFound</returns>
        [HttpGet("{id}")]
        public ActionResult<MenuItem> getItem(int id)
        {
            // get the MenuItem from the database
            MenuItem item = menuDatabase.getMenuItem(id);

            // check if the item exist 
            if (item == null)
            {
                // if no then message
                return NotFound($"No MenuItem found for id: {id}");
            }
            // else 
            else
            { // return this item 
                return Ok(item);
            }
        }


        /// <summary>
        /// returns an array of MenuItems for the given date 
        /// </summary>
        /// <returns>MenuItem[]</returns>
        [HttpGet("date")]
        public ActionResult<MenuItem[]> getAllMenuItem(DateTime date)
        {
            // get all menuItem
            MenuItem[] items = menuDatabase.getMenusbyDate(date);
            return Ok(items);
        }


        /// <summary>
        /// returns an array of MenuItems
        /// </summary>
        /// <returns>MenuItem[]</returns>
        [HttpGet]
        public ActionResult<MenuItem[]> getAllMenuItem()
        {
            MenuItem[] items = menuDatabase.getMenus();
            return Ok(items);
        }


        /// <summary>
        /// returns the edited MenuItem for the given ID and MenuItem. If ID is not found, it returns NotFound. 
        /// If the MenuItem is not found, it returns BadRequest.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menu"></param>
        /// <returns>MenuItem|NotFound|BadRequest</returns>
        [HttpPut("{id}")]
        public ActionResult<MenuItem> editMenuItem(int id, [FromBody]MenuItem menu)
        {
            //Check if id is valid
            if (menuDatabase.getMenuItem(id) == null)
            {
                return NotFound(($"No MenuItem found for id: {id}"));
            }

            //Check if item not null
            if (menu == null)
            {
                return BadRequest("MenuItem not found");
            }
     
            //update existing item
            MenuItem menuNew = menuDatabase.editMenu( id,menu);

            //return new item
            return Ok(menuNew);
        }


        /// <summary>
        /// deletes the menuItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult deleteMenuItem(int id)
        {
            // check if the item not null
            if (menuDatabase.getMenuItem(id) == null)
            {   // if null 
                return NotFound(($"No MenuItem found for id: {id}"));
            }
            // else delete this item 
            menuDatabase.deleteMenu(id);
            return Ok();
        }


        /// <summary>
        /// creates a MenuItem based on the given MenuItem. If the given MenuItem is null, it returns BadRequest.
        /// </summary>
        /// <param name="menu"></param>
        /// <returns>MenuItem|BadRequest</returns>
        [HttpPost]
        public ActionResult<MenuItem> createMenuItem(MenuItem menu)
        {
            // check if the item not null
            if (menu == null)
            {
                // if null then message 
                return BadRequest("MenuItem not found");
            }
            // else creat new item 
            MenuItem menuNew = menuDatabase.saveNewMenu(menu);
            return Created("", menuNew);
        }
    }
}