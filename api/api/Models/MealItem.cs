using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class MealItem
    {        /// <summary>
             /// unique ID/PK of Meal 
             /// </summary>
        public int MealID { get; set; }
        //TODO muss change to PlaceItem 
        /// <summary>
        /// ID/ForeignKey of Meal
        /// </summary>
        public PlaceItem Place { get; set; }
        /// <summary>
        /// String MealName 
        /// the name of the meal 
        /// </summary>
        [Required]
        public String MealName { get; set; }

        /// <summary>
        /// String description
        /// necessaries the meal 
        /// </summary>
        [Required]
        public String Description { get; set; }

    }
}
