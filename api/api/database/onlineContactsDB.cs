using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace api.database
{
    public class onlineContactsDB : IContactsDB
    {
        SqlConnection sqlConnection = null;
        public onlineContactsDB()
        {
            sqlConnection = TimeTableDatabase.getConnection();
            
        }

        public ContactItem createContactItem(ContactItem item)
        {
            using (sqlConnection)
            {

            }
                throw new NotImplementedException();
        }

        public void deleteContactItem(int id)
        {
            throw new NotImplementedException();
        }

        public ContactItem editContactItem(int id, ContactItem item)
        {
            throw new NotImplementedException();
        }

        public ContactItem[] getAllContactItems()
        {
            throw new NotImplementedException();
        }

        public ContactItem getContactItem(int id)
        {
            throw new NotImplementedException();
        }

        public ContactItem getContactItem(string email)
        {
            throw new NotImplementedException();
        }
    }
}
