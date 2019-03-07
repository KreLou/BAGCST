using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Models;
using api.offlineDB;

namespace api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase 
    {
        private IMealDB database = getDatabase();
        private IPlaceDB placeDataBase = getPlaceDatabase();

        /// <summary>
        /// Returns the current database
        /// </summary>
        /// <returns></returns>
        private static IMealDB getDatabase()
        {
            return new OfflineMealDB();
        }
        /// <summary>
        /// Returns the current database
        /// </summary>
        /// <returns></returns>
        private static IPlaceDB getPlaceDatabase()
        {
            return new OfflinePlaceDB();
        }
        /// <summary>
        /// returns the MealItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ContactItem|NotFound</returns>
        [HttpGet("{id}")]
        public ActionResult<MealItem> getMealItem(int id)
        {
            MealItem item = database.getMealItem(id);
            if (item == null)
            {
                return NotFound($"No MealItem found for id: {id}");
            }
            else
            {
                return Ok(item);
            }
        }

        /// <summary>
        /// returns an array of MealItem
        /// </summary>
        /// <returns>MealItem[]</returns>
        [HttpGet]
        public ActionResult<MealItem[]> getAllMeals()
        {
            MealItem[] items = database.getMeals();
            return Ok(items);
        }

        /// <summary>
        /// returns the edited MealItem for the given ID and MealItem. If ID is not found, it returns NotFound. 
        /// If the MealItem is not found, it returns BadRequest.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="meal"></param>
        /// <returns>MealItem|NotFound|BadRequest</returns>
        [HttpPut("{id}")]
        public ActionResult<MealItem> editMeal(int id, [FromBody]MealItem meal)
        {
            //Check if id is valid
            if (database.getMealItem(id) == null)
            {
                return NotFound(($"No MealItem found for id: {id}"));
            }

            //Check if item not null
            if (meal == null)
            {
                return BadRequest("MealItem not found");
            }

            //update existing item
            MealItem item_out = database.editMeal(id, meal);

            //return new item
            return Ok(item_out);
        }

        /// <summary>
        /// deletes the MealItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult deleteMeal(int id)
        {
       
            if (database.getMealItem(id) == null)
            {
                return NotFound(($"No MealItem found for id: {id}"));
            }
            database.deleteMeal(id);
            return Ok();
        }

        /// <summary>
        /// creates a MealItem based on the given MealItem. If the given MealItem is null, it returns BadRequest.
        /// </summary>
        /// <param name="meal"></param>
        /// <returns>MealItem|BadRequest</returns>
        [HttpPost]
        public ActionResult<MealItem> createMeal(MealItem meal)
        {
         
            if (meal == null)
            {
                return BadRequest("MealItem not found");
            }

            MealItem mealNew = database.saveNewMeal(meal);
            return Created("", mealNew);
        }
    }
}
