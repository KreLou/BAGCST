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
    public class PlaceController : ControllerBase
    {
        private IPlaceDB database = getDatabase();

        /// <summary>
        /// Returns the current database
 
        /// </summary>
        /// <returns></returns>
        private static IPlaceDB getDatabase()
        {
            return new OfflinePlaceDB();
        }

        /// <summary>
        /// returns the Place for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Place|NotFound</returns>
        [HttpGet("{id}")]
        public ActionResult<PlaceItem> getPlace(int id)
        {
           
            PlaceItem item = database.GetPlace(id);
            if (item != null)
            {
                return Ok(item);
            }
            else
            {
                return NotFound($" No Place found for id :{ id}");
            }
         
        }

        /// <summary>
        /// returns an array of Places
        /// </summary>
        /// <returns>PlaceItem[]</returns>
        [HttpGet]
        public ActionResult<PlaceItem[]> getAllPlaces()
        {
            PlaceItem[] items = database.GetPlaces();
            return Ok(items);
        }


        /// <summary>
        /// returns the edited Place for the given ID and PlaceName . If ID is not found, it returns NotFound. 
        /// If the Place is not found, it returns BadRequest.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="place"></param>
        /// <returns>PlaceItem|NotFound|BadRequest</returns>
        [HttpPut("{id}")]
        public ActionResult<PlaceItem> editPlaceItem(int id, [FromBody]PlaceItem place)
        {
           
            //Check if id is valid
            if (database.GetPlace(id) == null)
            {
                return NotFound(($"No Place found for id: {id}"));
            }

            //Check if item not null
            if (place == null)
            {
                return BadRequest("Place not found");
            }

            //update existing item
            PlaceItem item_out = database.editPlace( id,place);

            //return new item
            return Ok(item_out);
        }
        /// <summary>
        /// deletes the PlaceItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult deletePlaceItem(int id)
        {
  
            if (database.GetPlace(id) == null)
            {
                return NotFound(($"No Place found for id: {id}"));
            }
            database.deletePlace(id);
            return Ok();
        }
        /// <summary>
        /// creates a PlaceItem based on the given ContactItem. If the given PlaceItem is null, it returns BadRequest.
        /// </summary>
        /// <param name="place"></param>
        /// <returns>PlaceItem|BadRequest</returns>
        [HttpPost]
        public ActionResult<PlaceItem> createPlace([FromBody] PlaceItem place)
        {
            
            if (place == null)
            {
                return BadRequest("Place not found");
            }

            PlaceItem item_out = database.saveNewPlace(place);
            return Created("", item_out);
        }


    }
}
