using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace AirSupply
{
    public partial class AirSupplyForm : Form
    {
        RoomConfigForm TheConfig;
        TemperatureConfigForm TheTempConfig;
        SortableBindingList<RoomStatus> AllUnits = new SortableBindingList<RoomStatus>();
        int IndexOfUnitToUpdate = 0;        
        Timer theUpdateTimer = new Timer();
       
        public AirSupplyForm() {
            InitializeComponent();

            if (this.enableLoggingToolStripMenuItem.Checked) {
                C50Communication.ShouldWeLog = true;
            }

            this.TheConfig = new RoomConfigForm();
            this.TheConfig.FormClosing += new FormClosingEventHandler(this.TheConfig_FormClosing);
            this.TheTempConfig = new TemperatureConfigForm();

            foreach (Room aRoom in this.TheConfig.AllRooms) {
                RoomStatus aStatus = new RoomStatus(aRoom);
                this.AllUnits.Add(aStatus);
            }
           
            this.lastUpdateLbl.Text = "Last Updated: All Units at " + DateTime.Now.ToShortTimeString();

            //bind the UI to the List of Units
            this.mainDataGrid.AutoGenerateColumns = false;
            this.mainDataGrid.DataSource = this.AllUnits;
            this.mainDataGrid.AutoSize = true;
            this.mainDataGrid.AllowUserToOrderColumns = false;

            //add all the columns 
            this.mainDataGrid.Columns.Add(this.CreateTextBoxColumn("UnitID", "Group"));
            this.mainDataGrid.Columns.Add(this.CreateTextBoxColumn("RoomName", "Room Name"));
            this.mainDataGrid.Columns.Add(this.CreateTextBoxColumn("RoomNumber", "Number"));
            this.mainDataGrid.Columns.Add(this.CreateTextBoxColumn("RoomFloor", "Floor"));

            this.mainDataGrid.Columns.Add(this.CreateTextBoxColumn("Status", "Status"));
            this.mainDataGrid.Columns.Add(this.CreateComboBoxColumn(new Enums.Drive(), "Drive", "Drive"));
            this.mainDataGrid.Columns.Add(this.CreateComboBoxColumn(new Enums.Mode(), "Mode", "Mode"));
           
            //handle temperature by itself as C# doesn't like number only enums 
            //the range, 62 to 82 is restricted by the unit specifications...not me
            DataGridViewComboBoxColumn temperatureCol = new DataGridViewComboBoxColumn();
            List<string> tempOptions = Enumerable.Range(54, 29).Select(n => n.ToString()).ToList();
            tempOptions.Insert(0, "0");
            temperatureCol.DataSource = tempOptions;
            temperatureCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            temperatureCol.DataPropertyName = "SetTemp";
            temperatureCol.Name = "Set Temp";
            this.mainDataGrid.Columns.Add(temperatureCol);

            this.mainDataGrid.Columns.Add(this.CreateTextBoxColumn("InletTemp", "Inlet Temp"));
            this.mainDataGrid.Columns.Add(this.CreateComboBoxColumn(new Enums.AirDirection(), "AirDirection", "Air Direction"));
            this.mainDataGrid.Columns.Add(this.CreateComboBoxColumn(new Enums.FanSpeed(), "FanSpeed", "Fan Speed"));
            this.mainDataGrid.Columns.Add(this.CreateComboBoxColumn(new Enums.FilterSign(), "FilterSign", "Filter Sign"));
            this.mainDataGrid.Columns.Add(this.CreateTextBoxColumn("ErrorSign", "Error Sign"));

            //update a unit every 15 seconds
            this.theUpdateTimer.Tick += new EventHandler(this.UnitUpdateProcessor);
            this.theUpdateTimer.Interval = 1000;
            this.theUpdateTimer.Start();           
        }

        DataGridViewComboBoxColumn CreateComboBoxColumn(Enum theEnum, string heading, string dataName) {
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(theEnum.GetType());
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            combo.DataPropertyName = heading;
            combo.Name = dataName;
            return combo;
        }

        DataGridViewTextBoxColumn CreateTextBoxColumn(string heading, string dataName) {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.ReadOnly = true;
            column.DataPropertyName = heading;
            column.Name = dataName;
            return column;
        }

        //Update Room names/values in the UI if they were changed in the config 
        public void UpdateAllRooms() {
            foreach (RoomStatus aStatus in this.AllUnits) {
                Room updatedRoom;
                this.TheConfig.OrderedRoom.TryGetValue(aStatus.UnitID, out updatedRoom);
                if (updatedRoom != null) {
                    aStatus.Room = updatedRoom;
                }
            }
        }

        private void UnitUpdateProcessor(Object myObject, EventArgs myEventArgs) {
            this.theUpdateTimer.Stop();
            //update the next item
            if (this.IndexOfUnitToUpdate > this.AllUnits.Count - 1) {
                this.IndexOfUnitToUpdate = 0;
            }

            if(this.AllUnits[this.IndexOfUnitToUpdate] != null){                
                string xmlUpdate = C50Communication.GetAllStatus(this.AllUnits[this.IndexOfUnitToUpdate].UnitID);
                this.AllUnits[this.IndexOfUnitToUpdate].ProcessXMLUpdate(xmlUpdate);
                this.IndexOfUnitToUpdate++;
            }
            this.lastUpdateLbl.Text = "Last Update: Unit " + this.IndexOfUnitToUpdate.ToString() + " at " + DateTime.Now.ToShortTimeString();
            this.theUpdateTimer.Enabled = true;          
        }
      
        private void roomToUnitsAssignmentsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (this.TheConfig == null || this.TheConfig.IsDisposed) {
                this.TheConfig = new RoomConfigForm();
                this.TheConfig.FormClosing += new FormClosingEventHandler(this.TheConfig_FormClosing);
            }
            
            this.TheConfig.Show();
        }

        public void TheConfig_FormClosing(Object sender, FormClosingEventArgs e) {
            this.UpdateAllRooms();
        }

        private void sendRawXMLToolStripMenuItem_Click(object sender, EventArgs e) {
            SendRawXMLForm sendRaw = new SendRawXMLForm();
            sendRaw.Show();
        }

        private void viewLogsToolStripMenuItem_Click(object sender, EventArgs e) {
            //Just go to the web UI from AWS, it is free to read the table from there... create an IAM Username/password and give it to users :-) ... 
            ViewAWSLogs vl = new ViewAWSLogs();
            vl.Show();
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e) {
            DocForm doc = new DocForm();
            doc.Show();
        }

        private void enableLoggingToolStripMenuItem_Click(object sender, EventArgs e) {
            if (this.enableLoggingToolStripMenuItem.Checked) {
                C50Communication.ShouldWeLog = true;
            }
            else {
                C50Communication.ShouldWeLog = false;
            }
        }

        private void temperatureSettingsToolStripMenuItem_Click(object sender, EventArgs e) {

            if (this.TheTempConfig == null || this.TheTempConfig.IsDisposed) {
                this.TheTempConfig = new TemperatureConfigForm();               
            }

            this.TheTempConfig.Show();
        }
    }
}
