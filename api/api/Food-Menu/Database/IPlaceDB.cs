using BAGCST.api.FoodMenu.Models;

namespace BAGCST.api.FoodMenu.Database
{
    public interface IPlaceDB
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

        /// <summary>
        /// Search for Placeitem by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        PlaceItem getPlaceItemByName(string name);
    }
}
