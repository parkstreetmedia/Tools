using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AirSupply
{
    [DynamoDBTable("RoomStatus")]
    public class RoomStatus : INotifyPropertyChanged
    {     
        [DynamoDBHashKey]
        public DateTime DateTimeUpdated { get; set; }

        [DynamoDBRangeKey] 
        public int UnitID { get; set; }

        private string address;
        public string Address { get { return this.Room.Address; } set { this.address = value; } }

        private int emsUnitID;
        public int EMSUnitID { get { return this.Room.EMSRoomID; } set { this.emsUnitID = value; } }

        private string modeText;
        public string ModeText { get { return this.mode.ToString(); } set { this.modeText = value; } }

        [DynamoDBIgnore]
        Enums.Mode mode;
        
        [DynamoDBIgnore]
        public Enums.Mode Mode {
            get => mode; set {
                if (value == Enums.Mode.UNKNOWN) { return; }
                mode = value;
                this.SendCommand("Mode", value.ToString());
            }
        }

        private string driveText;
        public string DriveText { get { return this.drive.ToString(); } set { this.driveText = value; } }

        [DynamoDBIgnore]
        Enums.Drive drive;       
        [DynamoDBIgnore]
        public Enums.Drive Drive {
            get => drive;
            set {
                if (value == Enums.Drive.UNKNOWN) { return; }
                drive = value;
                this.SendCommand("Drive", value.ToString());
            }
        }

        private string airDirectionText;
        public string AirDirectionText { get { return this.airDirection.ToString(); } set { this.airDirectionText = value; } }

        [DynamoDBIgnore]
        Enums.AirDirection airDirection;
        [DynamoDBIgnore]
        public Enums.AirDirection AirDirection {
            get => airDirection; set {
                if (value == Enums.AirDirection.UNKNOWN) { return; }
                airDirection = value;
                this.SendCommand("AirDirection", value.ToString());
            }
        }

        private string fanSpeedText;
        public string FanSpeedText { get { return this.fanSpeed.ToString(); } set { this.fanSpeedText = value; } }

        [DynamoDBIgnore]
        Enums.FanSpeed fanSpeed;
        [DynamoDBIgnore]
        public Enums.FanSpeed FanSpeed {
            get => fanSpeed; set {
                if (value == Enums.FanSpeed.UNKNOWN) { return; }
                fanSpeed = value;
                this.SendCommand("FanSpeed", value.ToString());
            }
        }

        private string filterSignText;
        public string FilterSignText { get { return this.filterSign.ToString(); } set { this.filterSignText = value; } }

        [DynamoDBIgnore]
        Enums.FilterSign filterSign;
        [DynamoDBIgnore]
        public Enums.FilterSign FilterSign {
            get => filterSign; set {
                if (value == Enums.FilterSign.UNKNOWN) { return; }
                if (value == Enums.FilterSign.ON) { return; }
                filterSign = value;
                this.SendCommand("FilterSign", "RESET");
            }
        }

        [DynamoDBIgnore]
        double setTemp;
        
        public string SetTemp {
            get {
                double inF = setTemp;
                inF = Math.Round(inF);
                return inF.ToString();
            }
            set {
                if (value == "0") { return; }
                string newTemp = TemperatureDefaults.ToCWithCorrectLimits(this.mode, value);
                this.SendCommand("SetTemp", newTemp);
            }
        }

        public double InletTemp { get; set; }
        public double OutsideHighTemp { get; set; }
        public double OutsideLowTemp { get; set; }

        public string ErrorSign { get; set; }

        [DynamoDBIgnore]
        public Room Room { get; set; }

        //we don't do anything with these... but AWS Persistence doesn't work with just a getter :-/
        private string roomName;
        public string RoomName { get => this.Room.Name; set => this.roomName = value; }
        private string roomNumber;
        public string RoomNumber { get => this.Room.Number; set => this.roomNumber = value; }
        private string roomFloor;
        public string RoomFloor { get => this.Room.Floor; set => this.roomFloor = value; }
        private string roomBuilding;
        public string RoomBuilding { get => this.Room.Building; set => this.roomBuilding = value; }

        [DynamoDBIgnore]
        string status;

        public string Status {
            get {
                this.status = "Not Responding";

                if (this.mode == Enums.Mode.HEAT) {
                    if (this.InletTemp > this.setTemp) {
                        this.status = "Irrational";
                    }
                    else {
                        this.status = "Good";
                    }
                }
                if (this.mode == Enums.Mode.COOL) {
                    if (this.InletTemp < this.setTemp) {
                        this.status = "Irrational";
                    }
                    else {
                        this.status = "Good";
                    }
                }

                if (this.mode == Enums.Mode.AUTOHEAT) {
                    if (this.InletTemp > this.setTemp && this.drive != Enums.Drive.OFF) {
                        this.status = "Irrational";
                    }
                    else {
                        this.status = "Good";
                    }
                }

                if (this.mode == Enums.Mode.AUTOCOOL) {
                    if (this.InletTemp < this.setTemp && this.drive != Enums.Drive.OFF) {
                        this.status = "Irrational";
                    }
                    else {
                        this.status = "Good";
                    }
                }

                //Auto means it is doing the good auto thing and is off, if the auto set and drive on it should report autoheat or autocool
                if (this.mode == Enums.Mode.AUTO) {
                    if (this.drive != Enums.Drive.OFF) {
                        this.status = "Irrational";
                    }
                    else {
                        this.status = "Good";
                    }
                }


                return status;
            }
            set => status = value;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public RoomStatus() { }

        public RoomStatus(Room aRoom) {
            if (this.Room == null) {
                this.Room = aRoom;
                this.UnitID = this.Room.UnitID;

                //set defaults
                this.setTemp = 0;
                this.mode = Enums.Mode.UNKNOWN;
                this.airDirection = Enums.AirDirection.UNKNOWN;
                this.drive = Enums.Drive.UNKNOWN;
                this.fanSpeed = Enums.FanSpeed.UNKNOWN;
                this.filterSign = Enums.FilterSign.UNKNOWN;
                this.status = "Unknown";

                string xmlUpdate = C50Communication.GetAllStatus(this.UnitID);
                this.ProcessXMLUpdate(xmlUpdate);
            }
            else {
                this.Room = aRoom;
                this.UnitID = this.Room.UnitID;
            }           
        }

        public RoomStatus(DateTime now, Room aRoom, TemperatureDefaults temp) {
            if (this.Room == null) {
                this.Room = aRoom;
                this.UnitID = this.Room.UnitID;
                string xmlUpdate = C50Communication.GetAllStatus(this.UnitID);
                this.ProcessXMLUpdate(xmlUpdate);
            }
            else {
                this.Room = aRoom;
                this.UnitID = this.Room.UnitID;
            }
        
            this.DateTimeUpdated = now;
            this.OutsideHighTemp = temp.ForecastMax;
            this.OutsideLowTemp = temp.ForecastMin;
        }

        public bool ProcessXMLUpdate(string xmlFragment) {
            if (string.IsNullOrEmpty(xmlFragment)) {
                return false;
            }
            if (new[] { "(500)", "Server Error", "Internal Server" }.Any(x => xmlFragment.Contains(x))) {
                return false;
            }
            XDocument units = XDocument.Parse(xmlFragment);
            foreach (var aUnit in units.Descendants("DatabaseManager")) {
                foreach (var unitAttribute in aUnit.Elements("Mnet")) {

                    foreach (var unitItem in unitAttribute.Attributes()) {
                        if (unitItem.Name == "Group") {
                            if(this.UnitID != Int32.Parse(unitItem.Value)) {
                                //clearly there is a problem.
                                return false;
                            }
                        }

                        if (unitItem.Name == "Ventilation") {
                            continue;
                            //we don't have that option...
                        }

                        if (unitItem.Name == "ErrorSign") {
                            this.ErrorSign = unitItem.Value;
                        }

                        if (unitItem.Name == "SetTemp") {
                            Double theTemp;
                            bool hasTemp = Double.TryParse(unitItem.Value, out theTemp);
                            if (hasTemp) {
                                this.setTemp = TemperatureDefaults.CtoF(theTemp);
                            }
                            else {
                                this.setTemp = 0;
                            }

                        }

                        if (unitItem.Name == "InletTemp") {
                            Double toRound = TemperatureDefaults.CtoF(Double.Parse(unitItem.Value));
                            this.InletTemp = Math.Round(toRound, 2);
                        }

                        if (unitItem.Name == "Drive") {
                            if (!Enum.TryParse(unitItem.Value, out this.drive)) {
                                this.drive = Enums.Drive.UNKNOWN;
                            }
                        }

                        if (unitItem.Name == "Mode") {
                            if (!Enum.TryParse(unitItem.Value, out this.mode)) {
                                this.mode = Enums.Mode.UNKNOWN;
                            }
                        }

                        if (unitItem.Name == "AirDirection") {
                            if (!Enum.TryParse(unitItem.Value, out this.airDirection)) {
                                this.airDirection = Enums.AirDirection.UNKNOWN;
                            }
                        }

                        if (unitItem.Name == "FanSpeed") {
                            if (!Enum.TryParse(unitItem.Value, out this.fanSpeed)) {
                                this.fanSpeed = Enums.FanSpeed.UNKNOWN;
                            }
                        }

                        if (unitItem.Name == "FilterSign") {
                            if (!Enum.TryParse(unitItem.Value, out this.filterSign)) {
                                this.filterSign = Enums.FilterSign.UNKNOWN;
                            }
                        }
                    }

                }
            }
            this.NotifyPropertyChanged(this.UnitID.ToString());
            return true;
        }

        private void SendCommand(string prop, string val) {
            string result = C50Communication.SetAnyProperty(prop, val, this.UnitID);
            this.ProcessXMLUpdate(result);
        }

        private void NotifyPropertyChanged(string p) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
    }
}
