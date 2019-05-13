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
                    string courseID = "";
                    if (item.Course.ID == 0)
                    { courseID = "NULL"; }
                    else
                    { courseID = "'" + item.Course.ID.ToString() + "'"; }

                    string SQL = "INSERT INTO [contact] ([relationtypid],[lastname],[firstname],[titel],[mail], [phonenumber],[roomnumber],[coursetypid],[responsibility]) " +
                        "VALUES('"+item.Type.ID.ToString()+"','" + item.LastName + "','" + item.FirstName + "','" + item.Title + "','" + item.Email + "','" + item.TelNumber + "','" + item.Room + "',"+courseID+",'"+item.Responsibility+"');" +
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
                    string courseID = "";
                    if (item.Course.ID == 0)
                    { courseID = "NULL"; }
                    else
                    { courseID = "'" + item.Course.ID.ToString() + "'"; }
                        
                    //In der SQL Anweisung muss noch Contacttypid und CourseTypID  und Verantwortlicher geändert werden.
                    string SQL = "UPDATE [contact] SET [lastname]='"+item.LastName+"',[firstname]='"+item.FirstName+"'," +
                        "[titel]='"+item.Title+"',[mail]='"+item.Email+"', [phonenumber]='"+item.TelNumber+"'," +
                        "[roomnumber]='"+item.Room+ "', [coursetypid]=" + courseID + " , [relationtypid]='" + item.Type.ID.ToString()+ "', [responsibility] ='"+item.Responsibility+"' " +
                        " WHERE [contactid] ='"+id.ToString()+"';";
                    sqlConnection.Open();
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlConnection = null;
                    return item;
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
                    SQLItem.Course = new StudyCourse();
                    SQLItem.Type = new UserType();
                    List<ContactItem> ContactItemList = new List<ContactItem>();
                    string SQL = "SELECT [contactid],[lastname],[firstname],[titel],[mail],[phonenumber],[roomnumber], " +
                        "[contact].relationtypid,[relationtyp].relationtypname,[contact].coursetypid,[coursetyp].[shortname],[coursetyp].[longname], [responsibility] " +
                        " FROM[contact]" +
                        " LEFT JOIN[coursetyp]" +
                        " ON[coursetyp].[coursetypid] =[contact].[coursetypid]" +
                        " LEFT JOIN[relationtyp]" +
                        " ON[relationtyp].[relationtypid] = [contact].relationtypid;";

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

                        SQLItem.Responsibility = myReader["responsibility"].ToString();


                        if (myReader["coursetypid"].ToString() != "")
                        {
                            SQLItem.Course.ID = Convert.ToInt32(myReader["coursetypid"]);
                            SQLItem.Course.LongText = myReader["longname"].ToString();
                            SQLItem.Course.ShortText = myReader["shortname"].ToString();

                        }


                        SQLItem.Type.ID = Convert.ToInt32(myReader["relationtypid"]);
                        SQLItem.Type.Name = myReader["relationtypname"].ToString();


                        ContactItemList.Add(SQLItem);

                        SQLItem = new ContactItem();
                        SQLItem.Type = new UserType();
                        SQLItem.Course = new StudyCourse();
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
                    SQLItem.Course = new StudyCourse();
                    SQLItem.Type = new UserType();

                    SQL = "SELECT [contactid],[lastname],[firstname],[titel],[mail],[phonenumber],[roomnumber], " +
                        "[contact].relationtypid,[relationtyp].relationtypname,[contact].coursetypid,[coursetyp].[shortname],[coursetyp].[longname], [responsibility] " +
                        " FROM[contact]" +
                        " LEFT JOIN[coursetyp]" +
                        " ON[coursetyp].[coursetypid] =[contact].[coursetypid]" +
                        " LEFT JOIN[relationtyp]" +
                        " ON[relationtyp].[relationtypid] = [contact].relationtypid ";
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

                        SQLItem.Responsibility = myReader["responsibility"].ToString();


                        if (myReader["coursetypid"].ToString() != "")
                        {
                            SQLItem.Course.ID = Convert.ToInt32(myReader["coursetypid"]);
                            SQLItem.Course.LongText = myReader["longname"].ToString();
                            SQLItem.Course.ShortText = myReader["shortname"].ToString();

                        }


                        SQLItem.Type.ID = Convert.ToInt32(myReader["relationtypid"]);
                        SQLItem.Type.Name = myReader["relationtypname"].ToString();
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
