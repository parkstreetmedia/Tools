using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSupply
{
    [DynamoDBTable("LastRun")]
    public class LastRun 
    {
        [DynamoDBHashKey] //Partition key
        public DateTime ID {
            get; set;
        }
        [DynamoDBRangeKey]
        public string ShortDate {
            get; set;
        }

        public DateTime LastRunTime {
            get; set;
        }

        public string Status {
            get; set;
        }

        public int NumberOfUpdates {
            get; set;
        }

        public string Description {
            get; set;
        }

        public LastRun(DateTime id, DateTime lastRunDateTime, string shortDate, string status, string description, int numberOfUpdates) {
            this.ID = id;
            this.LastRunTime = lastRunDateTime;
            this.ShortDate = shortDate;
            this.Status = status;
            this.Description = description;
            this.NumberOfUpdates = numberOfUpdates;
        }

        public LastRun() { }

    }
}
