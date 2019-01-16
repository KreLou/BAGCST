using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IMealDB
    {
        Meal saveNewMeal(Meal meal);
        Meal editMeal(Meal meal);
        void deleteMeal(Meal meal);
        Meal[] GetMeals(int MealID, PlaceItem Place, String MealName, String description);





    }
}
