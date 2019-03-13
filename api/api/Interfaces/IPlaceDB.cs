using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IPlaceDB
    {
        /// <summary>
        /// Create a new News-Item in Database, return the new Item
        /// </summary>
        /// <param name="place"></param>
        PlaceItem saveNewPlace(PlaceItem place);


        /// <summary>
        /// Update existing News-Item by ID
        /// </summary>
       /// <param name="id"></param>
        /// <param name="place"></param>
        /// <returns></returns>
        PlaceItem editPlace(int id ,PlaceItem place);
  

        /// <summary>
        /// Return all Places 
        /// </summary>
        /// <returns></returns>
        PlaceItem[] getPlaces();

        /// <summary>
        /// Return a Place 
        /// </summary>
        /// <returns></returns>
        PlaceItem getPlaceItem(int id );
    }
}
