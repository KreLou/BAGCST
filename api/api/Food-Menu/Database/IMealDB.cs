using BAGCST.api.FoodMenu.Models;

namespace BAGCST.api.FoodMenu.Database
{
    public interface IMealDB
    {

        /// <summary>
        /// search for MealItem by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>MealItem|null</returns>
        MealItem getMealItem(int MealID);

        /// <summary>
        /// creates a MealItem
        /// </summary>
        /// <param name="meal">MealItem</param>
        /// <returns>full MealItem</returns>
        MealItem saveNewMeal(MealItem meal);

        /// <summary>
        /// edit MealItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="meal"></param>
        /// <returns>edited MealItem|null</returns>
        MealItem editMeal(int id ,MealItem meal);

        /// <summary>
        /// delete MealItem
        /// </summary>
        /// <param name="id">ID</param>
        void deleteMeal(int id);


        /// <summary>
        /// get all MealItem
        /// </summary>
        /// <returns>Array of MealItem with length>=0</returns>
        MealItem[] getMeals();

        /// <summary>
        /// Select the MealID, if this Item already exist
        /// </summary>
        /// <param name="meal">Meal, but without id</param>
        /// <returns>Id or 0</returns>
        int selectMealIDFromOtherInformation(MealItem meal);

        /// <summary>
        /// Filter all Meals by PlaceID
        /// Order by Meal.MealName asc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MealItem[] getMealItemsByPlaceID(int id);



    }
}
