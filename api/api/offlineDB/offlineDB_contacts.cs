using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;
using System.IO;

namespace api.Databases
{
    public class offlineDB_contacts : IContactsDB
    {
        private string csvFile = Environment.CurrentDirectory + "\\offlineDB\\Files\\contacts.csv";

        //public ContactItem item_packed = new ContactItem();
        //List<string[]> itemList = new List<string[]>();

        //public void addToItemList(ContactItem item_packed)
        //{
        //    string[] item_splitted = {item_packed.ContactID.ToString() + ";", item_packed.Firstname + ";", item_packed.Lastname+ ";",
        //                         item_packed.Room.ToString() + ";", item_packed.TelNumber.ToString() + ";", item_packed.Type + ";",
        //                         item_packed.Responsibility + ";", item_packed.Course};

            
        //    itemList.Add(item_splitted);
        //}

        //private bool filesIsUsed(string file)
        //{
        //    try
        //    {
        //        File.Open(file, FileMode.Open);
        //    }
        //    catch
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public void updateCsv()
        //{
        //    using (StreamWriter writer = new StreamWriter(csvFile))
        //    {
        //        if (!filesIsUsed(csvFile))
        //        {
        //            foreach (string[] item in itemList)
        //            {
        //                writer.WriteLine(item);
        //            }
        //        }
        //        else
        //        {
        //            throw new FileLoadException("The file can"t be updated because it"s used");
        //        }
        //    }
        //}

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

        public ContactItem[] getAllContactItems()
        {
            List<ContactItem> list = new List<ContactItem>();
            using (StreamReader reader = new StreamReader(csvFile))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] args = line.Split(";");
                    ContactItem item = new ContactItem() {
                        ContactID = Convert.ToInt32(args[0]),
                        Firstname = args[1],
                        Lastname = args[2],
                        TelNumber = args[3],
                        Email = args[4],
                        Room = args[5],
                        Responsibility = args[6],
                        Course = args[7],
                        Type = args[8]
                    };
                    
                    list.Add(item);
                }
            }
                return list.ToArray();
        }

        public ContactItem createContactItem(ContactItem item)
        {
            //1. Generate ID
            ContactItem[] items = getAllContactItems();
            int id = 0;
            foreach(ContactItem item_ in items)
            {
                if(item_.ContactID >= id)
                {
                    id = item_.ContactID;
                }
            }
            id++;
            item.ContactID = id;

            //2. Save Item
            File.AppendAllLines(csvFile, new string[] { item.ContactID + ";" + item.Firstname + ";" + item.Lastname + ";" + item.TelNumber + ";" + item.Email + ";" + item.Room + ";" + item.Responsibility + ";" + item.Course + ";" + item.Type });

            //3. Return Item
            return getContactItem(id);
        }

        public ContactItem editContactItem(int id, ContactItem item)
        {
            string tempFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(tempFile))
            using (StreamReader reader = new StreamReader(csvFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (Convert.ToInt32(line.Split(";")[0]) == id)
                    {
                        writer.WriteLine(item.ContactID + ";" + item.Firstname + ";" + item.Lastname + ";" + item.TelNumber + ";" + item.Email + ";" + item.Room + ";" + item.Responsibility + ";" + item.Course + ";" + item.Type);
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
