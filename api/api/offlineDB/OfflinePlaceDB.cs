using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

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
            return place.PalceID + ";" + place.Name ;
        }

        public PlaceItem editPlace(PlaceItem item)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Search for all active users in file 
        /// </summary>
        /// <returns></returns>
        public PlaceItem[] GetPlaces()
        {
            List<PlaceItem> list = new List<PlaceItem>();

            using (StreamReader sr = new StreamReader(this.place_filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    PlaceItem place = new PlaceItem()
                    {
                        PalceID = (int)Convert.ToInt64(args[0]),
            
                        Name = args[1]
              
                    };

     
                }
            }
            return list.ToArray();
        }


        /// <summary>
        /// Search for place in file, return user or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlaceItem GetPlace(int id)
        {
            PlaceItem place = null;

            using (StreamReader sr = new StreamReader(place_filename))
            {
                string line;
                //end if end of file or place is found
                while ((line = sr.ReadLine()) != null && place == null)
                {
                    int place_ID = (int)Convert.ToInt64(line.Split(";")[0]);
                    if (place_ID == id)
                    {
                        string[] args = line.Split(";");
                        place = new PlaceItem()
                        {
                            PalceID = place_ID,
                    
                            Name = args[2]
                      
                        };
                    }
                }
            }

            return place;
        }
        /// <summary>
        /// Create new place
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public PlaceItem saveNewPlace(PlaceItem item)
        {
            PlaceItem[] existinguser = GetPlaces();
            int max = 1;
            foreach (PlaceItem exPlace in existinguser)
            {
                max = exPlace.PalceID > max ? exPlace.PalceID + 1 : max;
            }

            item.PalceID = max;

            File.AppendAllLines(place_filename, new String[] { this.writeLine(item) });
            return item;
        }

        public void deletePlace(int id)
        {
            throw new NotImplementedException();
        }
    }
}
