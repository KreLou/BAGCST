using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IMenuDB
    {
        MenuItem saveNewMenu(MenuItem item);
        MenuItem editMenu(MenuItem item);
        MenuItem deleteMenu(MenuItem item);
        MenuItem[] GetMenus(int MenuID, DateTime Date, MealItem Meal, decimal Price);
    }
}
