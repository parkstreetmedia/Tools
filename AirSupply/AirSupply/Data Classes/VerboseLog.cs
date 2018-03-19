using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSupply
{
    [DynamoDBTable("VerboseLog")]
    public class VerboseLog
    {     
        [DynamoDBHashKey]
        public DateTime DateTimeOfAction { get; set; }

        [DynamoDBRangeKey] 
        public int UnitID { get; set; }

        public int EMSUnitID { get; set; }
        public bool IsOccupied { get; set; }
        public string NewSetTemp { get; set; }
        public string Reason { get; set; }
        public string EventName { get; set; }
        public DateTime EventStartHour { get; set; }
        public DateTime EventEndHour { get; set; }

        public VerboseLog() { }

        public VerboseLog(DateTime now, int unit, int emsId, bool isOccupado, string newTemp, string reason, string eventName, DateTime eventStarthour, DateTime eventEndHour) {
            this.DateTimeOfAction = now;
            this.UnitID = unit;
            this.EMSUnitID = emsId;
            this.IsOccupied = isOccupado;
            this.NewSetTemp = newTemp;
            this.Reason = reason;
            this.EventName = eventName;
            this.EventStartHour = eventStarthour;
            this.EventEndHour = eventEndHour;
        }

        public VerboseLog(DateTime now, int unit, int emsId, bool isOccupado, string newTemp, string reason) {
            this.DateTimeOfAction = now;
            this.UnitID = unit;
            this.EMSUnitID = emsId;
            this.IsOccupied = isOccupado;
            this.NewSetTemp = newTemp;
            this.Reason = reason;
        }
    }
}
