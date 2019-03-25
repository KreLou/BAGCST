using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using api.Interfaces;
using api.Models;
using System.Reflection;
using api.Controllers;

namespace api.offlineDB
{
    public class OfflineMealDB:IMealDB
    {

        //TODO @Louis what will be doing when Place is null 
        private IPlaceDB placeDB = new OfflinePlaceDB();
        private string meal_filename = Environment.CurrentDirectory + "\\offlineDB\\Files\\meals.csv";

  //      private static string path_offlineDBFiles = Environment.CurrentDirectory + "\\offlineDB\\Files\\";
   //     private static string filename_place = path_offlineDBFiles + "places.csv";

        /// <summary>
        /// Creates the string output for Meal
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        private string  ConvertFromMealitemToString(MealItem meal)
        {
            return 
                meal.MealID + ";"
                + meal.Place.PlaceID + ";"
                + meal.MealName + ";" 
                + meal.Description ;
        }
        /// <summary>
        /// Creates the meal output form the String input 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private MealItem ConvertfromStringToMealItem(string line)
        {
            
            string[] args = line.Split(";");
            MealItem ítem = new MealItem
            {
                MealID = Convert.ToInt32(args[0]),
                Place = placeDB.getPlaceItem(Convert.ToInt32(args[1])),
                MealName = args[2],
                Description = args[3]
            };
            return ítem;
        }


        /// <summary>
        /// Create new Meal
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public MealItem saveNewMeal(MealItem item)
        {
            // get all items 
            MealItem[] meals = getMeals();
            int max = 0;
            foreach (MealItem exMeal in meals)
            {
                if (exMeal.MealID >= max)
                {
                    max = exMeal.MealID;
                }
            }
            // the new id is the max Id from file + 1
            max++;
            item.MealID = max;

            // save item 
            File.AppendAllLines(meal_filename, new String[] { ConvertFromMealitemToString(item) });

            // return item 
            return item;
        }

        /// <summary>
        /// edits the Meal based on the given meal except for the ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>Meal</returns>
        public MealItem editMeal(int id, MealItem item)
        {
            // get the tempfile 
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(meal_filename))
            {
                string line;
                // end if the line not exist 
                while ((line = reader.ReadLine()) != null)
                {
                    // if the id in this line is the id from mealItem 
                    if (Convert.ToInt32(line.Split(";")[0]) == id)
                    {
                        item.MealID = id;
                        // then make the change in this item 
                        line = id+";"
                                 + item.MealName + ";"
                                 + item.Place.PlaceID + ";"
                                 + item.Description;
                        // saving the line in the mealsfile 
                        writer.WriteLine(line);
                    }
                    else
                    {   // no change 
                        writer.WriteLine(line);
                    }

                }
            }
            // delete the old item
            File.Delete(meal_filename);
            // save the new item 
            File.Move(tempFile, meal_filename);
            // return this item 
            return getMealItem(id);
        }

        /// <summary>
        /// deletes the meal based on the given ID
        /// </summary>
        /// <param name="id"></param>
        public void deleteMeal(int id)
        {
            // get the tempfile
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(meal_filename))
            {
                string line;
                // end if the item not exist 
                while ((line = reader.ReadLine()) != null)
                {   // if the id in the line is not mealId 
                    if (Convert.ToInt32(line.Split(";")[0]) != id)
                    {
                        // no change 
                        writer.WriteLine(line);
                    }
                }
            }
            // else delete the item 
            File.Delete(meal_filename);
            File.Move(tempFile, meal_filename);
        }

        /// <summary>
        /// Search for Meal in file, return Meal or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MealItem getMealItem(int id)
        {
            MealItem meal = null;
    
            using (StreamReader sr = new StreamReader(meal_filename))
            {
                string line;
                //end if end of file or meal is found
                while ((line = sr.ReadLine()) != null && meal == null)
                {   // if the mealId is same the id in this line 
                    int meal_ID = (int)Convert.ToInt64(line.Split(";")[0]);
                    if (meal_ID == id)
                      
                    {
                        meal = ConvertfromStringToMealItem(line);
                    }
                }
            }

            return meal;
        }

        /// <summary>
        /// Search for all Meals in file 
        /// </summary>
        /// <returns></returns>
        public MealItem[] getMeals()
        {
            // List for all meals 
            List<MealItem> list = new List<MealItem>();
            string line;
            using (StreamReader sr = new StreamReader(this.meal_filename))
            {
                // end if the line not exist 
                while ((line = sr.ReadLine()) != null)
                {
                    // the meal item 
                    MealItem meal = ConvertfromStringToMealItem(line);
                    // add this item to the list 
                    list.Add(meal);
                };
            }
            return list.ToArray();
        }

        public int selectMealIDFromOtherInformation(MealItem meal)
        {
            MealItem[] mealItems = getMeals();

            mealItems = mealItems
                .Where(item => item.Description.ToLower() == meal.Description.ToLower())
                .Where(item => item.MealName.ToLower() == meal.MealName.ToLower())
                .ToArray();
            if (mealItems.Length == 1)
            {
                return mealItems[0].MealID;
            }
            return 0;
        }

        public MealItem[] getMealItemsByPlaceID(int id)
        {
            MealItem[] items = getMeals();
            items = items
                .Where(item => item.Place.PlaceID == id)
                .OrderBy(item => item.MealName)
                .ToArray();
            return items;
        }
    }
}
