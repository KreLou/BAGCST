using api.database;
using BAGCST.api.Timetable.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace BAGCST.api.Timetable.Database
{
    public class onlineSemesterDB : ISemesterDB
    {
        SqlConnection sqlConnection = null;
        public SemesterItem getCurrentSemesterByStudyGroup(string studyGroup)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnectionTimeTable();
            SemesterItem Item = new SemesterItem();
            try
            {
                using (sqlConnection)
                {

                    string SQL = "SELECT Min( Tag ) AS Tag_start, Max( Tag ) AS Tag_ende,[Semester]  FROM [SG_anwesend] " +
                                 "WHERE SGID = '"+ studyGroup + "' AND[Semester] IN(SELECT[Semester]  FROM[SG_anwesend] " +
                                 "WHERE SGID = '" + studyGroup + "' AND Tag BETWEEN  CONVERT(date, DATEADD(WEEK, -2, GETDATE())) AND CONVERT(date, DATEADD(WEEK, 2, GETDATE())) Group BY Semester) Group BY Semester;";
                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        Item.Semester = Convert.ToInt32(myReader["Semester"]);
                        Item.Start = Convert.ToDateTime(myReader["Tag_start"]);
                        Item.End = Convert.ToDateTime(myReader["Tag_ende"]);
                        Item.StudyGroup = studyGroup;
                    }
                    sqlConnection.Close();
                    sqlConnection = null;
                    return Item ;
                }

            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public SemesterItem[] getSemesterItem(string studyGroup)
        {

            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnectionTimeTable();
            SemesterItem Item = new SemesterItem();
            List<SemesterItem> ListItem = new List<SemesterItem>(); 
            try
            {
                using (sqlConnection)
                {

                    string SQL = "SELECT Min(Tag ) AS Tag_start, Max( Tag ) AS Tag_ende,[Semester]  FROM[SG_anwesend] WHERE SGID = 'WI16-1'  Group BY Semester; ";
                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        Item.Semester = Convert.ToInt32(myReader["Semester"]);
                        Item.Start = Convert.ToDateTime(myReader["Tag_start"]);
                        Item.End = Convert.ToDateTime(myReader["Tag_ende"]);
                        Item.StudyGroup = studyGroup;
                        ListItem.Add(Item);
                        Item = new SemesterItem();
                    }
                    sqlConnection.Close();
                    sqlConnection = null;
                    return ListItem.ToArray();
                }

            }
            catch (System.Exception)
            {

                return null;
            }
        }
    }
}
