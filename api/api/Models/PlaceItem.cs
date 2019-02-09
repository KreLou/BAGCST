using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class PlaceItem
    {

        /// <summary>
        /// unique ID/PK of place 
        /// </summary>
        public int PlaceID { get; set; }

        /// <summary>
        /// String name 
        /// the name of the place
        /// </summary>
        public String Name { get; set; }
    }
}
