using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IMealDB
    {

        /// <summary>
        /// search for MealItem by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>MealItem|null</returns>
        MealItem GetMeal(int MealID);

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
        MealItem[] GetMeals();





    }
}
