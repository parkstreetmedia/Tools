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
    [DynamoDBTable("Rooms")]
    public class Room : IComparable<Room>, IComparable
    {
        [DynamoDBIgnore]
        private string startHour;
        [DynamoDBIgnore]
        private string endHour;

        [DynamoDBHashKey]
        public int UnitID { get; set; } //RoomID/Group... whatever you'd like to call it 
        public string Address { get; set; } //The M-Net address that is on the physical unit 
        public int EMSRoomID { get; set; } //the ID of the room this unit is in 
        public string Name { get; set; }
        public string Number { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public bool IsOffice { get; set; }
        public string StartHour { get { if (string.IsNullOrEmpty(this.startHour)) { this.startHour = "08:00"; } return startHour; } set => startHour = value; }
        public string EndHour { get { if (string.IsNullOrEmpty(this.endHour)) { this.endHour = "20:00"; } return endHour; } set => endHour = value; }
        public bool SundayOff { get; set; }
        public bool MondayOff { get; set; }
        public bool TuesdayOff { get; set; }
        public bool WednesdayOff { get; set; }
        public bool ThursdayOff { get; set; }
        public bool FridayOff { get; set; }
        public bool SaturdayOff { get; set; }

        [DynamoDBIgnore]
        public string DaysOffHeader { get => "Days Off: "; }

        public Room() { }

        public int CompareTo(Room other) {
            return this.UnitID.CompareTo(other.UnitID);
        }

        public int CompareTo(object obj) {
            return this.CompareTo(obj);
        }

    }
}
