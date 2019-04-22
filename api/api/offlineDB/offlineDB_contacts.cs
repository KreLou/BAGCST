using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;
using System.IO;

namespace api.Databases
{
    public class offlineContactsDB : IContactsDB
    {
        private string csvFile = Environment.CurrentDirectory + "\\offlineDB\\Files\\contacts.csv";



        private static ContactItem convertToContactItem(string line)
        {
            string[] args = line.Split(";");
            ContactItem item = new ContactItem()
            {
                ContactID = Convert.ToInt32(args[0]),
                FirstName = args[1],
                LastName = args[2],
                TelNumber = args[3],
                Email = args[4],
                Room = args[5],
                Responsibility = args[6],
                Course = args[7],
                Type = args[8],
                Title = args[9]
            };
            return item;
        }

        private static string convertToString(ContactItem item)
        {
            return item.ContactID + ";" + item.FirstName + ";" + item.LastName + ";" + item.TelNumber + ";" + item.Email + ";" + item.Room + ";" + item.Responsibility + ";" + item.Course + ";" + item.Type + ";" + item.Title;
        }

        /// <summary>
        /// Returns a ContactItem based on the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ContactItem|null</returns>
        public ContactItem getContactItem(int id)
        {
            ContactItem[] contacts = getAllContactItems();
            foreach(ContactItem item in contacts)
            {
                if(item.ContactID == id)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns a ContactItem based on the given E-Mail-Address
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ContactItem|null</returns>
        public ContactItem getContactItem(string email)
        {
            ContactItem[] contacts = getAllContactItems();
            foreach (ContactItem item in contacts)
            {
                if (item.Email == email)
                {
                    return item;
                }
            }
            return null;
            
        }

        /// <summary>
        /// Returns an array of ContactItems
        /// </summary>
        /// <returns>ContactItem[]</returns>
        public ContactItem[] getAllContactItems()
        {
            List<ContactItem> list = new List<ContactItem>();
            using (StreamReader reader = new StreamReader(csvFile))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    ContactItem item = convertToContactItem(line);

                    list.Add(item);
                }
            }
            return list.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToArray();
        }


        /// <summary>
        /// Creates a ContactItem based on the given ContactItem
        /// </summary>
        /// <param name="item"></param>
        /// <returns>ContactItem</returns>
        public ContactItem createContactItem(ContactItem item)
        {
            //1. Generate ID
            ContactItem[] items = getAllContactItems();
            int id = 0;
            foreach (ContactItem item_ in items)
            {
                if (item_.ContactID >= id)
                {
                    id = item_.ContactID;
                }
            }
            id++;
            item.ContactID = id;

            //2. Save Item
            File.AppendAllLines(csvFile, new string[] { convertToString(item) });

            //3. Return Item
            return getContactItem(id);
        }

        /// <summary>
        /// Edits a ContactItem based on the given ContactItem except for the ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>ContactItem</returns>
        public ContactItem editContactItem(int id, ContactItem item)
        {
            item.ContactID = id;
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(csvFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    ContactItem currentItem = convertToContactItem(line);
                    if (currentItem.ContactID == id)
                    {
                        writer.WriteLine(convertToString(item));
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            File.Delete(csvFile);
            File.Move(tempFile, csvFile);
            return getContactItem(id);
        }

        /// <summary>
        /// Deletes a ContactItem based on the given ID
        /// </summary>
        /// <param name="id"></param>
        public void deleteContactItem(int id)
        {
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(csvFile))
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
            File.Delete(csvFile);
            File.Move(tempFile, csvFile);
        }
    }
}
