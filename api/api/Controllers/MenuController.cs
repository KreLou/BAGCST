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
        private IMenuDB database = getDatabase();


        /// <summary>
        /// Returns the current database

        /// </summary>
        /// <returns></returns>
        private static IMenuDB getDatabase()
        {
            return new OfflineMenuDB();
        }

        /// <summary>
        /// returns the MenuItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MenuItem|NotFound</returns>
        [HttpGet("{id}")]
        public ActionResult<MenuItem> GetItem(int id)
        {
            MenuItem item = database.GetMenuItem(id);

            if (item == null)
            {
                return NotFound($"No MenuItem found for id: {id}");
            }
            else
            {
                return Ok(item);
            }
        }
        /// <summary>
        /// returns an array of ContactItems
        /// </summary>
        /// <returns>MenuItem[]</returns>
        [HttpGet]
        public ActionResult<MenuItem[]> getAllMenuItem(DateTime date)
        {
            MenuItem[] items = database.GetMenus(  date);
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
            if (database.GetMenuItem(id) == null)
            {
                return NotFound(($"No MenuItem found for id: {id}"));
            }

            //Check if item not null
            if (menu == null)
            {
                return BadRequest("MenuItem not found");
            }

            //update existing item
            MenuItem menuNew = database.editMenu( menu);

            //return new item
            return Ok(menuNew);
        }

        /// <summary>
        /// deletes the menuNew for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult deleteMenuItem(int id)
        {
            //TODO check for permission
            if (database.GetMenuItem(id) == null)
            {
                return NotFound(($"No menuNew found for id: {id}"));
            }
            database.deleteMenu(id);
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

            if (menu == null)
            {
                return BadRequest("MenuItem not found");
            }

            MenuItem menuNew = database.saveNewMenu(menu);
            return Created("", menuNew);
        }
    }
}