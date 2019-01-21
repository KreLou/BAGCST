using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;
using System.IO;

namespace api.Databases
{
    public class offlineDB_contacts
    {
        private string csvFile = Environment.CurrentDirectory + "\\offlineDB\\Files\\contacts.csv";

        public ContactItem item_packed = new ContactItem();
        List<string[]> itemList = new List<string[]>();

        public void addToItemList(ContactItem item_packed)
        {
            string[] item_splitted = {item_packed.ContactID.ToString() + ";", item_packed.Firstname + ";", item_packed.Lastname+ ";",
                                 item_packed.Room.ToString() + ";", item_packed.TelNumber.ToString() + ";", item_packed.Type + ";",
                                 item_packed.Responsibility + ";", item_packed.Course};

            
            itemList.Add(item_splitted);
        }

        private bool filesIsUsed(string file)
        {
            try
            {
                File.Open(file, FileMode.Open);
            }
            catch
            {
                return true;
            }
            return false;
        }

        public void updateCsv()
        {
            using (StreamWriter writer = new StreamWriter(csvFile))
            {
                if (!filesIsUsed(csvFile))
                {
                    foreach (string[] item in itemList)
                    {
                        writer.WriteLine(item);
                    }
                }
                else
                {
                    throw new FileLoadException("The file can't be updated because it's used");
                }
            }
        }
        
    }
}
