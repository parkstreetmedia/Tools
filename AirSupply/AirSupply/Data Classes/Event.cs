using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSupply
{
    [DynamoDBTable("Events")]
    public class Event 
    {
        [DynamoDBHashKey] //Partition key
        public int ID {
            get; set;
        }
        [DynamoDBRangeKey] //Sort key
        public string Date {
            get; set;
        }
        [DynamoDBProperty]
        public DateTime TimeEventStart {
            get; set;
        }
        [DynamoDBProperty]
        public DateTime TimeEventEnd {
            get; set;
        }
        [DynamoDBProperty]
        public DateTime TimeBookingStart {
            get; set;
        }
        [DynamoDBProperty]
        public DateTime TimeBookingEnd {
            get; set;
        }
        [DynamoDBProperty]
        public string EventName {
            get; set;
        }
        [DynamoDBProperty]
        public DateTime DateAdded {
            get; set;
        }
        [DynamoDBProperty]
        public DateTime DateChanged {
            get; set;
        }
        [DynamoDBProperty]
        public string RoomName {
            get; set;
        }
        [DynamoDBProperty]
        public string CancelReason {
            get; set;
        }
        [DynamoDBProperty]
        public string EventDescription {
            get; set;
        }
        [DynamoDBProperty]
        public bool DisplayOnWeb {
            get; set;
        }
        [DynamoDBProperty]
        public int RoomID {
            get; set;
        }
        [DynamoDBProperty]
        public string StatusDescription {
            get; set;
        }
        [DynamoDBProperty]
        public string Room {
            get; set;
        }
        [DynamoDBProperty]
        public string RoomDescription {
            get; set;
        }
        [DynamoDBProperty]
        public string RoomNumber {
            get; set;
        }
        [DynamoDBProperty]
        public string BuildingID {
            get; set;
        }

        [DynamoDBProperty]
        public string GroupingID {
            get; set;
        }
        [DynamoDBProperty]
        public string FloorID {
            get; set;
        }
        [DynamoDBProperty]
        public string RoomType {
            get; set;
        }
        [DynamoDBProperty]
        public string HVACZone {
            get; set;
        }
        
        public Event() { }

    }
}
