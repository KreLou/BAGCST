using api.database;
using BAGCST.api.Timetable.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BAGCST.api.Timetable.Database
{
    public class onlineTimetableDB : ITimetableDB
    {
        SqlConnection sqlConnection = null;


        private LectureItem[] getTimeTable(string value, DateTime startTime, DateTime endTime, int typ)
        {
            sqlConnection = null;
            sqlConnection = TimeTableDatabase.getConnectionTimeTable();

            try
            {
                using (sqlConnection)
                {
                    //DateTime endTime = new DateTime();
                    endTime = Convert.ToDateTime(endTime);//DateTime.Now;
                    DateTime StartHourLessons = new DateTime();
                    DateTime EndHourLessons = new DateTime();
                    DateTime LessonsDay = new DateTime();
                    LectureItem SQLItem = new LectureItem();

                    List<LectureItem> list = new List<LectureItem>();

                    //SELECT[Unterricht].[Tag], [Unterricht].Stunde,[Unterricht].OrtsID,[Unterricht].DozentID,[Unterricht].NutzungsID,[Unterricht].Bemerkung,[Stunde].Anfang,[Stunde].Ende,[Zuhoerer].SGID FROM[Unterricht]
                    //LEFT JOIN[Stunde] ON[Unterricht].Stunde = [Stunde].Stunde
                    //LEFT JOIN[Zuhoerer] ON[Unterricht].Tag = [Zuhoerer].Tag AND[Unterricht].Stunde = [Zuhoerer].Stunde AND[Unterricht].OrtsID = [Zuhoerer].OrtsID
                    //WHERE[Zuhoerer].Tag BETWEEN '2017-01-01 00:00:00' AND '2019-01-01 00:00:00' AND[SGID]='WI16-1' ORDER BY[Zuhoerer].SGID, [Unterricht].Tag, [Stunde].Stunde
                    string SQLWhere = "";

                    if (typ == 1)
                    {
                        SQLWhere = " AND[DozentID] = '" + value + "' ";
                    }
                    else
                    {
                        SQLWhere = " AND[SGID]= '" + value + "' ";
                    }
                    string SQL = "SELECT[Unterricht].[Tag], [Unterricht].Stunde,[Unterricht].OrtsID,[Unterricht].DozentID,[Unterricht].NutzungsID,[Unterricht].Bemerkung,[Stunde].Anfang,[Stunde].Ende,[Zuhoerer].SGID FROM[Unterricht] " +
                                  "LEFT JOIN[Stunde] ON[Unterricht].Stunde = [Stunde].Stunde " +
                                  "LEFT JOIN[Zuhoerer] ON[Unterricht].Tag = [Zuhoerer].Tag AND[Unterricht].Stunde = [Zuhoerer].Stunde AND[Unterricht].OrtsID = [Zuhoerer].OrtsID " +
                                  "WHERE[Zuhoerer].Tag BETWEEN '" + startTime + "' AND '" + endTime + "' " +
                                   SQLWhere + " ORDER BY[Zuhoerer].SGID, [Unterricht].Tag, [Stunde].Stunde";

                    sqlConnection.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(SQL, sqlConnection);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        if (myReader["Anfang"].ToString() != "")
                        {
                            StartHourLessons = Convert.ToDateTime(myReader["Anfang"]);
                        }
                        else
                        {
                            StartHourLessons = Convert.ToDateTime("01.01.2000 00:00");
                        }

                        if (myReader["Ende"].ToString() != "")
                        {
                            EndHourLessons = Convert.ToDateTime(myReader["Ende"]);
                        }
                        else
                        {
                            EndHourLessons = Convert.ToDateTime("01.01.2000 00:00");
                        }

                        LessonsDay = Convert.ToDateTime(myReader["Tag"]);
                        
                        SQLItem.Start = Convert.ToDateTime(LessonsDay.Date + StartHourLessons.TimeOfDay);
                        SQLItem.End = Convert.ToDateTime(LessonsDay.Date + EndHourLessons.TimeOfDay); ;
                        SQLItem.Lecturer = myReader["DozentID"].ToString();
                        SQLItem.Place = myReader["OrtsID"].ToString();
                        SQLItem.StudyGroup = myReader["SGID"].ToString();
                        SQLItem.Title = myReader["NutzungsID"].ToString();
                        //Bemerkungsfeld feld noch

                        list.Add(SQLItem);
                        SQLItem = new LectureItem();

                    }
                    sqlConnection.Close();
                    sqlConnection = null;
                    return list.ToArray();
                }

            }
            catch (System.Exception)
            {

                return null;
            }

        }

        public LectureItem[] getLecturesByLecturer(string lecturer, DateTime startTime, DateTime endTime)
        {
            //TODO ABU Add End-Date
            return getTimeTable(lecturer, startTime, endTime, 1);

        }

        public LectureItem[] getSemesterLectures(string studyGroup, SemesterItem semester)
        {
            //TODO ABU Add End-Date
            return getTimeTable(studyGroup, semester.Start,semester.End,0);
        }
    }
}
