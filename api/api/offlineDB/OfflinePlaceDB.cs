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
            return place.PlaceID + ";" + place.Name ;
        }

        /// <summary>
        /// edits the Place based on the given ContactItem except for the ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>Place</returns>
        public PlaceItem editPlace(int id ,PlaceItem item)
        {
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(place_filename))
            {
                string line;
                while ((line= reader.ReadLine()) != null)
                {
                    if(Convert.ToInt32(line.Split(";")[0]   )== id)
                    {
                        writer.WriteLine(id+ ";" + item.Name);
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            File.Delete(place_filename);
            File.Move(tempFile, place_filename);
            return GetPlace(id);

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
                        PlaceID = (int)Convert.ToInt64(args[0]),
            
                        Name = args[1]
              
                    };
                    list.Add(place);

     
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
                            PlaceID = place_ID,
                    
                            Name = args[1]
                      
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
            PlaceItem[] existingplace = GetPlaces();
            int max = 0;
            foreach (PlaceItem exPlace in existingplace)
            {
                max = exPlace.PlaceID > max ? exPlace.PlaceID  : max;
            }

            item.PlaceID = max+1;

            File.AppendAllLines(place_filename, new String[] { this.writeLine(item) });
            return item;
        }

        /// <summary>
        /// deletes the Place based on the given ID
        /// </summary>
        /// <param name="id"></param>
        public void deletePlace(int id)
        {
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(place_filename))
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
            File.Delete(place_filename);
            File.Move(tempFile, place_filename);

        }
    }
}
