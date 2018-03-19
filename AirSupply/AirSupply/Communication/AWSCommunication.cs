using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AirSupply
{
    public static class AWSCommunication
    {
        private static DynamoDBContext GetDynamoDbContext() {
            BasicAWSCredentials creds = new BasicAWSCredentials(XMLConfigData.AWSKey, XMLConfigData.AWSSecret);
            AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
            clientConfig.RegionEndpoint = RegionEndpoint.USEast1;
            AmazonDynamoDBClient c = new AmazonDynamoDBClient(creds, clientConfig);
            return new DynamoDBContext(c);
        }

        public static bool SaveLastRun(LastRun aRun, bool storeAsSuccess) {
            bool didWork = false;
            try {
                var context = GetDynamoDbContext();

                context.Save<LastRun>(aRun);
                if (storeAsSuccess) {
                    //Now save it as the most recent run, to avoid searching we save it as ID 1809-02-27 12:00 as well. 
                    DateTime theFirst = DateTime.Parse("1809-02-27 8:00 PM");
                    aRun.ID = theFirst;
                    aRun.ShortDate = theFirst.ToShortDateString();
                    context.Save<LastRun>(aRun);
                }
                didWork = true;
            }
            catch (Exception) {
                didWork = false;
            }
            return didWork;
        }

        public static bool SaveTemperatureDefaults(TemperatureDefaults tempDefaults) {
            bool didWork = false;
            try {
                var context = GetDynamoDbContext();

                context.Save<TemperatureDefaults>(tempDefaults);
                didWork = true;
            }
            catch (Exception) {
                didWork = false;
            }
            return didWork;
        }

        public static int BatchWriteEventsToAWS(List<Event> recentEvents) {
            int countItems = 0;
            var context = GetDynamoDbContext();

            DynamoDBOperationConfig c = new DynamoDBOperationConfig();
            c.OverrideTableName = "Events";
            var nb = context.CreateBatchWrite<Event>(c);
            foreach (Event anEventToWrite in recentEvents) {                
                    nb.AddPutItem(anEventToWrite);
                    countItems++;              
            }

            nb.Execute();
            return countItems;
        }


        public static int BatchWriteRoomStatusToAWS(List<RoomStatus> allStatus) {
            int countItems = 0;
            var context = GetDynamoDbContext();

            DynamoDBOperationConfig c = new DynamoDBOperationConfig();
            c.OverrideTableName = "RoomStatus";
            var nb = context.CreateBatchWrite<RoomStatus>(c);
            foreach (RoomStatus aRoomToWrite in allStatus) {
                nb.AddPutItem(aRoomToWrite);
                countItems++;
            }

            nb.Execute();
            return countItems;
        }      

        public static int BatchWriteRoomsToAWS(List<Room> allRooms) {
            int countItems = 0;
            var context = GetDynamoDbContext();

            DynamoDBOperationConfig c = new DynamoDBOperationConfig();
            c.OverrideTableName = "Rooms";
            var nb = context.CreateBatchWrite<Room>(c);
            foreach (Room aRoomToWrite in allRooms) {
                nb.AddPutItem(aRoomToWrite);
                countItems++;
            }

            nb.Execute();
            return countItems;
        }

        public static int BatchWriteVerboseLogToAWS(List<VerboseLog> verboseLogOfActions) {
            int countItems = 0;
            var context = GetDynamoDbContext();

            DynamoDBOperationConfig c = new DynamoDBOperationConfig();
            c.OverrideTableName = "VerboseLog";
            var nb = context.CreateBatchWrite<VerboseLog>(c);
            foreach (VerboseLog aLog in verboseLogOfActions) {
                nb.AddPutItem(aLog);
                countItems++;
            }

            nb.Execute();
            return countItems;
        }

        public static LastRun GetMostRecentLastRun() {
            //The most recent run is stored at hashkey 1809-02-27 12:00 for simplicity 
            var context = GetDynamoDbContext();
            DateTime theFirst = DateTime.Parse("1809-02-27 8:00 PM");
            return context.Load<LastRun>(theFirst, theFirst.ToShortDateString());
        }

        public static TemperatureDefaults GetTemperatureDefaults() {
            var context = GetDynamoDbContext();
            return context.Load<TemperatureDefaults>("1");          
        }

        public static List<Room> GetAllRooms() {
            var context = GetDynamoDbContext();
            DynamoDBOperationConfig c = new DynamoDBOperationConfig();
            c.OverrideTableName = "Rooms";
            List<ScanCondition> conditions = new List<ScanCondition>();
            conditions.Add(new ScanCondition("UnitID", ScanOperator.IsNotNull));
            var allRooms = context.Scan<Room>(conditions, c);
            if (allRooms != null) {
                return allRooms.ToList<Room>();
            }
            return null;
        }

    }
}

