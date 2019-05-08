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
        private IPlaceDB placeDB;

        public PlaceController(IPlaceDB placeDB)
        {
            this.placeDB = placeDB;
        }

        /// <summary>
        /// returns the Place for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Place|NotFound</returns>
        [HttpGet("{id}")]
        public ActionResult<PlaceItem> getPlace(int id)
        {
           // get the item from database
            PlaceItem item = placeDB.getPlaceItem(id);
            if (item != null)
            {
                return Ok(item);
            }
            // else retun message
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
        {   // get the all items from the database
            PlaceItem[] items = placeDB.getPlaces();
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
            if (placeDB.getPlaceItem(id) == null)
            {
                return NotFound(($"No Place found for id: {id}"));
            }

            //Check if item not null
            if (place == null)
            {
                return BadRequest("Place not found");
            }
            PlaceItem[] placeItems = placeDB.getPlaces();
            // for every place in Places List 
            foreach (PlaceItem placeItem in placeItems)
            {
                // if the PlaceName is exist 
                if (placeItem.PlaceName == place.PlaceName)
                {
                    // then wrong 
                    return BadRequest($"Place is exist : {placeItem.PlaceName}");
                }

            }

            // else 
            //update existing item
            PlaceItem item_out = placeDB.editPlace( id,place);
   
             //return new item
             return Ok(item_out);
                

        }

        /// <summary>
        /// creates a PlaceItem based on the given ContactItem. If the given PlaceItem is null, it returns BadRequest.
        /// </summary>
        /// <param name="place"></param>
        /// <returns>PlaceItem|BadRequest</returns>
        [HttpPost]
        public ActionResult<PlaceItem> createPlace([FromBody] PlaceItem place)
        {
            // check if the item  null 
            if (place == null)
            {// then return ein messege 
                return BadRequest("Place not found");
            }
            
            if (placeDB.getPlaceItemByName(place.PlaceName) != null)
            {
                return BadRequest($"Place {place.PlaceName} already exist");
            }

            //else 
            // if the item not null then save new item 
            PlaceItem item_out = placeDB.saveNewPlace(place);

                return Created("", item_out);
               
        }


    }
}
