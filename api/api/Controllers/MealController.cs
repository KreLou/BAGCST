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

        private IMealDB mealDatabase = getMealDatabase();

        private IPlaceDB placeDataBase = getPlaceDatabase();

        /// <summary>
        /// Returns the current Meal database
        /// </summary>
        /// <returns></returns>
        private static IMealDB getMealDatabase()
        {
            return new OfflineMealDB();
        }
        /// <summary>
        /// Returns the Place database   
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
        /// <returns>MealItem|NotFound</returns>
        [HttpGet("{id}")]
        public ActionResult<MealItem> getMealItem(int id)
        {   // get the meal Item from the datebase
            MealItem item = mealDatabase.getMealItem(id);
            // if this item not found 
            if (item == null)
            {
                // retern message Notfound 
                return NotFound($"No MealItem found for id: {id}");
            }
            // else return this item 
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
            // get all the Meal item 
            MealItem[] items = mealDatabase.getMeals();
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
            if (mealDatabase.getMealItem(id) == null)
            {
                return NotFound(($"No MealItem found for id: {id}"));
            }

            //Check if item  null
            if (meal == null)
            {
                return BadRequest("MealItem not found");
            }
            // if the Meal have null value 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //update existing item
            MealItem item_out = mealDatabase.editMeal(id, meal);

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
                // check if the given Id is not null
            if (mealDatabase.getMealItem(id) == null)
            {
                return NotFound(($"No MealItem found for id: {id}"));
            }
            // if not null then delete this item 
            mealDatabase.deleteMeal(id);
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
            // check if this meal exist 
            if (meal == null)
            {
                return BadRequest("MealItem not found");
            }
            // if the Meal have null value 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // if not then created new mealItem 
            MealItem mealNew = mealDatabase.saveNewMeal(meal);
            return Created("", mealNew);
        }
    }
}
