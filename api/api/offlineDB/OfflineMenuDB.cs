using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace api.offlineDB
{
    public class OfflineMenuDB:IMenuDB
    {

        private string menu_filename = Environment.CurrentDirectory + "\\offlineDB\\Files\\menus.csv";

        /// <summary>
        /// Creates the string output for menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        private string writeLine(MenuItem menu)
        {
            return menu.MenuID + ";" + menu.Meal + ";" + menu.Price + ";" + menu.Date ;
        }

        public MenuItem editMenu(MenuItem item)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Search for Menu in file, return menu or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuItem GetMenuItem(int id)
        {
            MenuItem menu = null;

            using (StreamReader sr = new StreamReader(menu_filename))
            {
                string line;
                //end if end of file or menu is found
                while ((line = sr.ReadLine()) != null && menu == null)
                {
                    int menu_id = (int)Convert.ToInt64(line.Split(";")[0]);
                    if (menu_id == id)
                    {
                        string[] args = line.Split(";");
                        menu = new MenuItem()
                        {
                            MenuID = menu_id,
                            Meal = args[1],
                            Price = (decimal)Convert.ToInt64( args[2]),
                            Date = DateTime.Parse(args[3])

                        };
                    }
                }
            }

            return menu;
        }

        /// <summary>
        /// Create new menu
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public MenuItem saveNewMenu(MenuItem item)
        {
            MenuItem[] existinguser = GetMenus(item.Date);
            int max = 1;
            foreach (MenuItem exMenu in existinguser)
            {
                max = exMenu.MenuID > max ? exMenu.MenuID + 1 : max;
            }

            item.MenuID = max;

            File.AppendAllLines(menu_filename, new String[] { this.writeLine(item) });
            return item;
        }

        public void deleteMenu(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Search for all active Menu in file 
        /// </summary>
        /// <returns></returns>
        public MenuItem[] GetMenus(DateTime date)
        {
            List<MenuItem> list = new List<MenuItem>();

            using (StreamReader sr = new StreamReader(this.menu_filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    MenuItem menu = new MenuItem()
                    {
                        MenuID = (int) Convert.ToInt64(args[0]),
                        Meal = args[1],
                        Price = (decimal)Convert.ToInt64(args[2]),
                        Date = date
                    };


                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// Search for all active Menu in file 
        /// </summary>
        /// <returns></returns>
        public MenuItem[] GetMenus(int id, DateTime date, MealItem meal, decimal price)
        {
            List<MenuItem> list = new List<MenuItem>();

            using (StreamReader sr = new StreamReader(this.menu_filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    MenuItem menu = new MenuItem()
                    {
                        MenuID = id,
                        Meal = meal,
                        Price = price,
                        Date = date
                    };


                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// Search for all active Menu in file 
        /// </summary>
        /// <returns></returns>
        public MenuItem[] GetMenus(DateTime date, MealItem meal, decimal price)
        {
            List<MenuItem> list = new List<MenuItem>();

            using (StreamReader sr = new StreamReader(this.menu_filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    MenuItem menu = new MenuItem()
                    {
                        MenuID = (int)Convert.ToInt64(args[0]),
                        Meal =meal,
                        Price = price,
                        Date = date
                    };


                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// Search for all active Menu in file 
        /// </summary>
        /// <returns></returns>
        public MenuItem[] GetMenus(int id, DateTime date)
        {
            List<MenuItem> list = new List<MenuItem>();

            using (StreamReader sr = new StreamReader(this.menu_filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    MenuItem menu = new MenuItem()
                    {
                        MenuID = id,
                        Meal = args[1],
                        Price = (decimal)Convert.ToInt64(args[2]),
                        Date = date
                    };


                }
            }
            return list.ToArray();
        }
    }
}
