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
                string SQL = "INSERT INTO ";
                //item.ContactID;


      //          SELECT TOP(1000) [contactid]
      //,[contacttypid]
      //,[lastname]
      //,[firstname]
      //,[titel]
      //,[mail]
      //,[phonenumber]
      //,[roomnumber]
      //,[coursetypid]
      //  FROM[Stundenplan].[dbo].[contact]
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
            using (sqlConnection)
            {
                try
                {
                    ContactItem SQLItem = new ContactItem();
                    string SQL = "SELECT [contactid],[contacttypid],[lastname],[firstname],[titel],[mail],[phonenumber],[roomnumber],[coursetypid] FROM [contact] WHERE [contactid]='" + id + "';";
                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        SQLItem.ContactID = id;
                        SQLItem.FirstName = myReader["firstname"].ToString();
                        SQLItem.LastName = myReader["lastname"].ToString();
                        SQLItem.Title = myReader["titel"].ToString();
                        SQLItem.TelNumber = myReader["phonenumber"].ToString();
                        SQLItem.Email = myReader["mail"].ToString();
                        SQLItem.Room = myReader["roomnumber"].ToString();
                        SQLItem.Responsibility = myReader["coursetypid"].ToString();
                        SQLItem.Course = myReader["coursetypid"].ToString();
                        SQLItem.Type = myReader["contacttypid"].ToString();
                        sqlConnection.Close();
                        return SQLItem;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return null;
                    }

                }
                catch (Exception)
                {

                    return null;
                }
            }
        }

        public ContactItem getContactItem(string email)
        {
            using (sqlConnection)
            {
                try
                {
                    ContactItem SQLItem = new ContactItem();
                    string SQL = "SELECT [contactid],[contacttypid],[lastname],[firstname],[titel],[mail],[phonenumber],[roomnumber],[coursetypid] FROM [contact] WHERE [[mail]]='" + email + "';";
                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        SQLItem.ContactID = Convert.ToInt32(myReader["contactid"]);
                        SQLItem.FirstName = myReader["firstname"].ToString();
                        SQLItem.LastName = myReader["lastname"].ToString();
                        SQLItem.Title = myReader["titel"].ToString();
                        SQLItem.TelNumber = myReader["phonenumber"].ToString();
                        SQLItem.Email = myReader["mail"].ToString();
                        SQLItem.Room = myReader["roomnumber"].ToString();
                        SQLItem.Responsibility = myReader["coursetypid"].ToString();
                        SQLItem.Course = myReader["coursetypid"].ToString();
                        SQLItem.Type = myReader["contacttypid"].ToString();
                        sqlConnection.Close();
                        return SQLItem;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return null;
                    }

                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
    }
}
