using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using api.Interfaces;
using api.Models;
using System.Reflection;

namespace api.offlineDB
{
    public class OfflineMealDB:IMealDB
    {

        private string meal_filename = Environment.CurrentDirectory + "\\offlineDB\\Files\\meals.csv";

        private static string path_offlineDBFiles = Environment.CurrentDirectory + "\\offlineDB\\Files\\";
        private static string filename_place = path_offlineDBFiles + "places.csv";

        /// <summary>
        /// Creates the string output for Meal
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        private string writeLine(MealItem meal)
        {
            return meal.MealID + ";" + meal.MealName + ";" + meal.Place + ";" + meal.description ;
        }

        public MealItem editMeal(MealItem item)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Search for all aMeals in file 
        /// </summary>
        /// <returns></returns>
        public MealItem[] GetMeals()
        {
            List<MealItem> list = new List<MealItem>();

            using (StreamReader sr = new StreamReader(this.meal_filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    MealItem meal = new MealItem()
                    {
                        MealID = (int)Convert.ToInt64(args[0]),
                        MealName = args[1],
                        //Place = filename_place.,
                        description = args[3],
                 
                    };

                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// Search for Meal in file, return Meal or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MealItem GetMeal(int id)
        {
            MealItem meal = null;

            using (StreamReader sr = new StreamReader(meal_filename))
            {
                string line;
                //end if end of file or meal is found
                while ((line = sr.ReadLine()) != null && meal == null)
                {
                    int meal_id = (int)Convert.ToInt64(line.Split(";")[0]);
                    if (meal_id == id)
                    {
                        string[] args = line.Split(";");
                        meal = new MealItem()
                        {
                            MealID = meal_id,
                            MealName = args[1],
                            Place = meal.Place,
                            description = args[3],
                        };
                    }
                }
            }

            return meal;
        }


        /// <summary>
        /// Create new Meal
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public MealItem saveNewMeal(MealItem item)
        {
            MealItem[] existinguser = GetMeals();
            int max = 1;
            foreach (MealItem exMeal in existinguser)
            {
                max = exMeal.MealID > max ? exMeal.MealID + 1 : max;
            }

            item.MealID = max;

            File.AppendAllLines(meal_filename, new String[] { this.writeLine(item) });
            return item;
        }

        public void deleteMeal(int id)
        {
            throw new NotImplementedException();
        }
    }
}
