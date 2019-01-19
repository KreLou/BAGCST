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
        /// <param name="place"></param>
        /// <returns></returns>


        PlaceItem editPlace(PlaceItem place);
        /// <summary>
        /// Delete a Place by ID
        /// </summary>
        /// <param name="id"></param>
        void deletePlace(int id);

        /// <summary>
        /// Return all Places 
        /// </summary>
        /// <returns></returns>
        PlaceItem[] GetPlaces();

        /// <summary>
        /// Return a Place 
        /// </summary>
        /// <returns></returns>
        PlaceItem GetPlace(int id );
    }
}
