using Microsoft.AspNetCore.Mvc;
using BAGCST.api.FoodMenu.Database;
using BAGCST.api.FoodMenu.Models;

namespace BAGCST.api.FoodMenu.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase 
    {

        private IMealDB mealDB;

        private IPlaceDB placeDB;
        
        public MealController(IMealDB mealDB, IPlaceDB placeDB)
        {
            this.mealDB = mealDB;
            this.placeDB = placeDB;
        }

        /// <summary>
        /// returns the MealItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MealItem|NotFound</returns>
        [HttpGet("{id}")]
        [NonAction]
        public ActionResult<MealItem> getMealItem(int id)
        {   // get the meal Item from the datebase
            MealItem item = mealDB.getMealItem(id);
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
        [HttpGet("{placeid}")]
        public ActionResult<MealItem[]> getAllMeals(int placeID)
        {
            // get all the Meal item 
            MealItem[] items = mealDB.getMealItemsByPlaceID(placeID);

            if (items.Length == 0)
            {
                return NotFound($"No MealItems found for PlaceID: {placeID}");
            }
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
        [NonAction]
        public ActionResult<MealItem> editMeal(int id, [FromBody]MealItem meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Check if id is valid
            if (mealDB.getMealItem(id) == null)
            {
                return NotFound(($"No MealItem found for id: {id}"));
            }

            //update existing item
            MealItem item_out = mealDB.saveNewMeal(meal);

            //return new item
            return Ok(item_out);
        }

        /// <summary>
        /// deletes the MealItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [NonAction]
        public ActionResult deleteMeal(int id)
        {
                // check if the given Id is not null
            if (mealDB.getMealItem(id) == null)
            {
                return NotFound(($"No MealItem found for id: {id}"));
            }
            // if not null then delete this item 
            mealDB.deleteMeal(id);
            return Ok();
        }

        /// <summary>
        /// creates a MealItem based on the given MealItem. If the given MealItem is null, it returns BadRequest.
        /// </summary>
        /// <param name="meal"></param>
        /// <returns>MealItem|BadRequest</returns>
        [HttpPost]
        [NonAction]
        public ActionResult<MealItem> createMeal(MealItem meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // check if this meal exist 
            if (meal == null)
            {
                return BadRequest("MealItem not found");
            }
            PlaceItem foundedPlace = null;
            foundedPlace = placeDB.getPlaceItemByName(meal.Place.PlaceName);

            if (foundedPlace == null && meal.Place.PlaceID != 0)
            {
                foundedPlace = placeDB.getPlaceItem(meal.Place.PlaceID);
            }

            if (foundedPlace == null)
            {
                return NotFound("No PlaceItem found for Name or ID: " + meal.Place.PlaceName + ", " + meal.Place.PlaceID);
            }
            meal.Place = foundedPlace;
            // if not then created new mealItem 
            MealItem mealNew = mealDB.saveNewMeal(meal);
            return Created("", mealNew);
        }
    }
}
