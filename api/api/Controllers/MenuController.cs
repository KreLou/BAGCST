using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.offlineDB;
using api.Models;
using api.Exception;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IMenuDB menuDatabase = getMenuDatabase();

        private IMealDB mealDatabase = getMealDataBase();

        private IPlaceDB placeDatabase = getPlaceDatabase();

        private static IPlaceDB getPlaceDatabase()
        {
            return new OfflinePlaceDB();
        }


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
        public ActionResult<MenuItem> getMenuItem(int id)
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



        /// <summary>0
        /// returns an array of MenuItems
        /// </summary>
        /// <returns>MenuItem[]</returns>
        [HttpGet]
        public ActionResult<MenuItem[]> getAllMenuItem([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int[] placeIDs)
        {
            if (startDate == DateTime.MinValue) startDate = DateTime.Today;
            if (endDate == DateTime.MinValue) endDate = startDate.AddDays(7);
            if (placeIDs.Length == 0) placeIDs = placeDatabase.getPlaces().Select(x => x.PlaceID).ToArray();
            MenuItem[] items = menuDatabase.getFilterdMenus(startDate, endDate, placeIDs);
            items = items.OrderBy(x => x.Date).ThenBy(x => x.Meal.Place.PlaceName).ThenBy(x => x.Meal.MealName).ToArray();
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
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                menu.Meal.Place = handlePlaceInput(menu.Meal.Place);
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }

            menu.Meal = handleMealInput(menu.Meal);

            //update existing item
            MenuItem menuNew = menuDatabase.editMenu(id, menu);

            //return new item
            return Ok(menuNew);
        }

        /// <summary>
        /// Check if this MealItem already exist
        /// add FoundedID if exist
        /// else create new
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        private MealItem handleMealInput(MealItem meal)
        {
            int foundedMealID = mealDatabase.selectMealIDFromOtherInformation(meal);
            if (foundedMealID == 0)
            {
                meal = mealDatabase.saveNewMeal(meal);
            }else
            {
                meal.MealID = foundedMealID;
            }
            return meal;
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
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                menu.Meal.Place = handlePlaceInput(menu.Meal.Place);
            }catch(NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }


            menu.Meal = handleMealInput(menu.Meal);

            MenuItem[] alreadySavedMenuItems = menuDatabase.getFilterdMenus(menu.Date, menu.Date, new[] { menu.Meal.Place.PlaceID });
            MenuItem[] filteredSavedMenuItems = alreadySavedMenuItems.Where(item => item.Meal.MealID == menu.Meal.MealID).ToArray();
            if (filteredSavedMenuItems.Length > 0)
            {
                return BadRequest("This MealItem is already planed in the Menu");
            }

            MenuItem menuNew = menuDatabase.saveNewMenu(menu);
            return Created("", menuNew);
        }

        /// <summary>
        /// Search for the PlaceItem
        /// Search for ID and check with the given Name
        /// Or Search for Place by given Name
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private PlaceItem handlePlaceInput(PlaceItem item)
        {
            PlaceItem foundedItem = null;
            if (item.PlaceID != 0)
            {
                foundedItem = placeDatabase.getPlaceItem(item.PlaceID);
                if (foundedItem.PlaceName.ToLower() == item.PlaceName.ToLower())   //Both Items are the same
                {
                    return foundedItem;
                }
            }
            foundedItem = placeDatabase.getPlaceItemByName(item.PlaceName);
            if (foundedItem == null)
            {
                throw new NotFoundException($"No PlaceItem found for Name: '{item.PlaceName}'");
            }
            return foundedItem;
        }
    }
}