using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;

using api.offlineDB;
namespace api.offlineDB
{
    public class OfflinePlaceDB : IPlaceDB
    {
        private string place_filename = Environment.CurrentDirectory + "\\offlineDB\\Files\\places.csv";

        /// <summary>
        /// Creates the string output for Place
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        private string writeLine(PlaceItem place)
        {
            return place.PlaceID + ";" + 
                   place.PlaceName ;
        }

        /// <summary>
        /// edits the Place based on the given PlaceItem except for the ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>Place</returns>
        public PlaceItem editPlace(int id ,PlaceItem item)
        {   // get the tempfile
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(place_filename))
            {   
                string line;
                // if the line exist
                while ((line= reader.ReadLine()) != null)
                {   // if the id in the file = item Id 
                    if(Convert.ToInt32(line.Split(";")[0] )== id)
                    {   // save the line in the file 
                        writer.WriteLine(id+ ";" + item.PlaceName);
                    }
                    else
                    {   // else no change 
                        writer.WriteLine(line);
                    }
                }
            }
            // delete the old item 
            File.Delete(place_filename);
            // save the new item 
            File.Move(tempFile, place_filename);
            // return this item 
            return getPlaceItem(id);

        }

        /// <summary>
        /// Search for all Places in file 
        /// </summary>
        /// <returns></returns>
        public PlaceItem[] getPlaces()
        {
            // list for alle Placeitems 
            List<PlaceItem> list = new List<PlaceItem>();

            using (StreamReader sr = new StreamReader(this.place_filename))
            {
                string line;
                // if the line exist
                while ((line = sr.ReadLine()) != null)
                {   
                    // place 
                    string[] args = line.Split(";");
                    PlaceItem place = new PlaceItem()
                    {
                        PlaceID = (int)Convert.ToInt64(args[0]),
                        PlaceName = args[1]
                    };
                    // add this place to the list 
                    list.Add(place);

     
                }
            }
            // return this list as array 
            return list.ToArray();
        }


        /// <summary>
        /// Search for place in file, return Place or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlaceItem getPlaceItem(int id)
        {
            PlaceItem place = null;

            using (StreamReader sr = new StreamReader(place_filename))
            {
                string line;
                //end if end of file or place is found
                while ((line = sr.ReadLine()) != null && place == null)
                {   // if the given id = the place Id 
                    int place_ID = (int)Convert.ToInt64(line.Split(";")[0]);
                    if (place_ID == id)
                    {
                        string[] args = line.Split(";");
                        place = new PlaceItem()
                        {
                            PlaceID = place_ID,
                            PlaceName = args[1]
                      
                        };
                    }
                }
            }
            // this place
            return place;
        }
        /// <summary>
        /// Create new place
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public PlaceItem saveNewPlace(PlaceItem item)
        {
            // get all Places 
            PlaceItem[] places = getPlaces();
            // the max id = 0
            int max = 0;
            // for every place in Places List 
            foreach (PlaceItem place in places)
            {
                // change the max to the maxId from Place
                max = place.PlaceID > max ? place.PlaceID  : max;
                if(item.PlaceName == place.PlaceName)
                {
                    return null;
                }
             
            }
            // the new Id is max+1 
            item.PlaceID = max+1;
            // save the place item in the file 
            File.AppendAllLines(place_filename, new String[] { this.writeLine(item) });
            return item;
        }
    }
}
