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

        //TODO what will be doing when Place is null 
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
                + meal.MealName + ";" 
                + meal.Place.PlaceID + ";" 
                + meal.Description ;
        }
        private MealItem ConvertfromStringToMealItem(string line)
        {
            
            string[] args = line.Split(";");
            MealItem ítem = new MealItem
            {
                MealID = Convert.ToInt32(args[0]),
                MealName = args[1],
        
                Place = placeDB.getPlaceItem(Convert.ToInt32(args[2])),
                Description = args[3]
            };
            return ítem;
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
                {
                    int meal_ID = (int)Convert.ToInt64(line.Split(";")[0]);
                    if (meal_ID == id)
                      
                    {
                        string[] args = line.Split(";");
                        meal = new MealItem()
                        {   
                            
                            MealID = meal_ID,
                            MealName = args[1],
                            Place = placeDB.getPlaceItem(Convert.ToInt32(args[2])),
                            Description = args[3],
                        };
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
            List<MealItem> list = new List<MealItem>();
            string line;
            using (StreamReader sr = new StreamReader(this.meal_filename))
            {
                
                while ((line = sr.ReadLine()) != null)
                {
                    MealItem meal = ConvertfromStringToMealItem(line);
                    list.Add(meal);
                };
            }
            return list.ToArray();
        }


        /// <summary>
        /// Create new Meal
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public MealItem saveNewMeal(MealItem item)
        {
            MealItem[] meals = getMeals();
            int max = 0;
            foreach (MealItem exMeal in meals)
            {
               if(exMeal.MealID >= max)
                {
                    max = exMeal.MealID;
                }
            }
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
        public MealItem editMeal(int id,MealItem item)
        {
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(meal_filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                  
                    if (Convert.ToInt32(line.Split(";")[0])== id)
                    {



                        line = ConvertFromMealitemToString(item);
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            File.Delete(meal_filename);
            File.Move(tempFile, meal_filename);
            return getMealItem(id);
        }

        /// <summary>
        /// deletes the meal based on the given ID
        /// </summary>
        /// <param name="id"></param>

        public void deleteMeal(int id)
        {
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(meal_filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (Convert.ToInt32(line.Split(";")[0]) != id)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            File.Delete(meal_filename);
            File.Move(tempFile, meal_filename);
        }
    }
}
