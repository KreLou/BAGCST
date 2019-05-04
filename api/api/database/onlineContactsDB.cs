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
           // sqlConnection = TimeTableDatabase.getConnection();
        }


        /// <summary>
        /// Creates a ContactItem based on the given ContactItem
        /// </summary>
        /// <param name="item"></param>
        /// <returns>ContactItem</returns>
        public ContactItem createContactItem(ContactItem item)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    //In der SQL Anweisung muss noch Contacttypid und CourseTypID und Verantwortlicher geändert werden.
                    string SQL = "INSERT INTO [contact] ([contacttypid],[lastname],[firstname],[titel],[mail], [phonenumber],[roomnumber],[coursetypid]) " +
                        "VALUES('1','" + item.LastName + "','" + item.FirstName + "','" + item.Title + "','" + item.Email + "','" + item.TelNumber + "','" + item.Room + "',NULL);" +
                        "SELECT SCOPE_IDENTITY();";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    int LastID = Convert.ToInt32(myCommand.ExecuteScalar());
                    sqlConnection.Close();
                    sqlConnection = null;
                    return getContactItem(LastID);
                }
            }
            catch (System.Exception)
            {

               return null;
           }
        }

        /// <summary>
        /// Deletes a ContactItem based on the given ID
        /// </summary>
        /// <param name="id"></param>
        public void deleteContactItem(int id)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "DELETE FROM [contact] WHERE [contactid] ='" + id.ToString() + "';";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlConnection = null;
                    return;
                }
            }
            catch (System.Exception)
            {

                return;
            }
        }

        /// <summary>
        /// Edits a ContactItem based on the given ContactItem except for the ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>ContactItem</returns>
        public ContactItem editContactItem(int id, ContactItem item)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    //In der SQL Anweisung muss noch Contacttypid und CourseTypID  und Verantwortlicher geändert werden.
                    string SQL = "UPDATE [contact] SET [lastname]='"+item.LastName+"',[firstname]='"+item.FirstName+"',[titel]='"+item.Title+"',[mail]='"+item.Email+"', [phonenumber]='"+item.TelNumber+"',[roomnumber]='"+item.Room+"' WHERE [contactid] ='"+id.ToString()+"';";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlConnection = null;
                    return getContactItem(id);
                }
            }
            catch (System.Exception)
            {
                
                return null;
            }

        }
        /// <summary>
        /// Returns an array of ContactItems
        /// </summary>
        /// <returns>ContactItem[]</returns>
        public ContactItem[] getAllContactItems()
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
            {

                    ContactItem SQLItem = new ContactItem();
                    List<ContactItem> ContactItemList = new List<ContactItem>();
                    string SQL = "SELECT [contactid],[lastname],[firstname],[titel],[mail],[phonenumber],[roomnumber], " +
                        "[contact].contacttypid,[contacttyp].typname AS contacttyp_typname ,[contact].coursetypid,[coursetyp].[typname] AS coursetyp_typname" +
                        " FROM[contact]" +
                        " LEFT JOIN[coursetyp]" +
                        " ON[coursetyp].[coursetypid] =[contact].[coursetypid]" +
                        " LEFT JOIN[contacttyp]" +
                        " ON[contacttyp].[contacttypid] = [contact].contacttypid;";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        SQLItem.ContactID = Convert.ToInt32(myReader["contactid"]);
                        SQLItem.FirstName = myReader["firstname"].ToString();
                        SQLItem.LastName = myReader["lastname"].ToString();
                        SQLItem.Title = myReader["titel"].ToString();
                        SQLItem.TelNumber = myReader["phonenumber"].ToString();
                        SQLItem.Email = myReader["mail"].ToString();
                        SQLItem.Room = myReader["roomnumber"].ToString();
                        SQLItem.Responsibility = myReader["coursetypid"].ToString();
                        SQLItem.Course = myReader["coursetyp_typname"].ToString();
                        SQLItem.Type = myReader["contacttyp_typname"].ToString();
                        ContactItemList.Add(SQLItem);
                        SQLItem = new ContactItem();
                    }
                    sqlConnection.Close();
                    sqlConnection = null;
                    return ContactItemList.ToArray();

                }

            }
            catch (System.Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// Returns a ContactItem based on the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ContactItem|null</returns>
        public ContactItem getContactItem(int id)
        {
            return GetContactItemByValue(id.ToString(), 1);
        }

        /// <summary>
        /// Returns a ContactItem based on the given E-Mail-Address
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ContactItem|null</returns>
        public ContactItem getContactItem(string email)
        {
            return GetContactItemByValue(email,2);
        }

        // Returns a ContactItem based on the given ID (Typ=1) or E-Mail-Address(typ=2) 
        private ContactItem GetContactItemByValue(string value, int typ)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnection();
            try
            {
                using (sqlConnection)
                {
                    string SQL = "";
                    ContactItem SQLItem = new ContactItem();

                    SQL = "SELECT [contactid],[lastname],[firstname],[titel],[mail],[phonenumber],[roomnumber], " +
                        "[contact].contacttypid,[contacttyp].typname AS contacttyp_typname ,[contact].coursetypid,[coursetyp].[typname] AS coursetyp_typname" +
                        " FROM[contact]" +
                        " LEFT JOIN[coursetyp]" +
                        " ON[coursetyp].[coursetypid] =[contact].[coursetypid]" +
                        " LEFT JOIN[contacttyp]" +
                        " ON[contacttyp].[contacttypid] = [contact].contacttypid ";
                    if (typ==1)
                    {
                        //Innerjoin fehlt
                        SQL += " WHERE [contactid]='" + value + "';";
                    }
                    if (typ == 2)
                    {
                        //Innerjoin fehlt
                        SQL += " WHERE [mail]='" + value + "';";
                    }

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
                        SQLItem.Course = myReader["contacttyp_typname"].ToString();  
                        SQLItem.Type = myReader["coursetyp_typname"].ToString();
                        sqlConnection.Close();
                        sqlConnection = null;
                        return SQLItem;
                    }
                    else
                    {
                        sqlConnection.Close();
                        sqlConnection = null;
                        return null;
                    }
                }

            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
