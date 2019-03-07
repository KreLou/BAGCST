using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;

namespace api.Models
{   /// <summary>
    /// Menu model show all elments for the objekt menu in the database
    /// </summary>

    public class MenuItem
    {
        /// <summary>
        /// unique ID/PK of Menu 
        /// </summary>
        public int MenuID { get; set; }

        /// <summary>
        /// DateTime Date
        /// the date whe the menu exist
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        ///  ID/ForeignKey of Meal
        /// </summary>
       
        public MealItem Meal { get; set; }
        /// <summary>
        /// the price of menu 
        /// </summary>
        public decimal Price { get; set; }
    }
}
