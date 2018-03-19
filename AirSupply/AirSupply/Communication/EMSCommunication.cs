using AirSupply;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace AirSupply
{
    public static class EMSCommunication
    {
        const string SQLEVENTSBETWEENDATES = "SELECT tblBooking.ID, tblBooking.TimeBookingStart, tblBooking.TimeBookingEnd, tblBooking.TimeEventStart, tblBooking.TimeEventEnd, tblBooking.EventName, tblBooking.DateAdded, tblBooking.DateChanged, tblBooking.CancelReason, tblEventType.Description AS EventDescription, tblEventType.DisplayOnWeb, tblStatus.Description as StatusDescription, tblRoom.Room, tblRoom.Description AS RoomDescription, tblRoom.BuildingID, tblRoom.GroupingID, tblRoom.FloorID, tblRoom.RoomType, tblRoom.HVACZone FROM tblBooking LEFT JOIN tblEventType ON tblBooking.EventTypeID = tblEventType.ID LEFT JOIN tblStatus ON tblBooking.StatusID = tblStatus.ID  LEFT JOIN tblRoom ON tblBooking.RoomID = tblRoom.ID  Where tblBooking.TimeEventStart > 'FROMWHEN' AND tblBooking.TimeEventStart< 'TILLWHEN' AND tblBooking.CancelDate IS NULL;";

        public static DataSet QueryEMS(string sqlQuery) {
            SqlConnection sqlConn = new SqlConnection(XMLConfigData.EMSSQLConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sqlQuery;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConn;
            sqlConn.Open();
            DataSet theData = new DataSet();

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd)) {
                adapter.Fill(theData);
            }
            
            sqlConn.Close();
            return theData;
        }

        public static List<Event> GetRecentEvents(DateTime startingFrom) {
            string startDate = startingFrom.ToShortDateString() + " " + startingFrom.ToShortTimeString();
            string sql = SQLEVENTSBETWEENDATES.Replace("FROMWHEN", startDate);
            sql = sql.Replace("TILLWHEN", DateTime.Now.AddDays(8).ToShortDateString() + " " + startingFrom.ToShortTimeString());
            DataSet results = QueryEMS(sql);
            return SQLToEvents(results);
        }

        public static List<Event> SQLToEvents(DataSet results) {
            List<Event> allEvents = new List<Event>();
            foreach (DataRow aRow in results.Tables[0].Rows) {
                Event anEvent = new Event();

                anEvent.ID = Int32.Parse(aRow["ID"].ToString());
                anEvent.Date = DateTime.Parse(aRow["TimeBookingStart"].ToString()).ToString("yyyy-MM-dd");

                anEvent.EventName = aRow["EventName"].ToString();
                anEvent.TimeBookingStart = DateTime.Parse(aRow["TimeBookingStart"].ToString());
                anEvent.TimeBookingEnd = DateTime.Parse(aRow["TimeBookingEnd"].ToString());
                anEvent.TimeEventStart = DateTime.Parse(aRow["DateAdded"].ToString());
                anEvent.TimeEventEnd = DateTime.Parse(aRow["DateChanged"].ToString());
                anEvent.DateAdded = DateTime.Parse(aRow["TimeEventStart"].ToString());
                anEvent.DateChanged = DateTime.Parse(aRow["TimeEventEnd"].ToString());
                anEvent.CancelReason = aRow["CancelReason"].ToString();
                anEvent.EventDescription = aRow["EventDescription"].ToString();
                anEvent.DisplayOnWeb = Boolean.Parse(aRow["DisplayOnWeb"].ToString());
                anEvent.StatusDescription = aRow["StatusDescription"].ToString();
                anEvent.Room = aRow["Room"].ToString();
                anEvent.RoomDescription = aRow["RoomDescription"].ToString();
                anEvent.BuildingID = aRow["BuildingID"].ToString();
                anEvent.GroupingID = aRow["GroupingID"].ToString();
                anEvent.FloorID = aRow["FloorID"].ToString();
                anEvent.RoomType = aRow["RoomType"].ToString();
                anEvent.RoomType = aRow["HVACZone"].ToString();

                allEvents.Add(anEvent);
            }

            return allEvents;
        }
    }
}