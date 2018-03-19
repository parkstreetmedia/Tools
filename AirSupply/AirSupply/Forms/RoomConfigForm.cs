using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AirSupply
{
    public partial class RoomConfigForm : Form
    {
        public SortableBindingList<Room> AllRooms = new SortableBindingList<Room>();
        public Dictionary<int, Room> OrderedRoom = new Dictionary<int, Room>();
      
        public RoomConfigForm() {
            InitializeComponent();

            this.FormClosing += this.ConfigureForm_FormClosing;

            //Load XML
            this.LoadRoomConfig();

            //Load UI
            this.configRoomsGridView.AutoGenerateColumns = false;
            this.configRoomsGridView.DataSource = this.AllRooms;
            this.configRoomsGridView.AutoSize = true;
            this.configRoomsGridView.AllowUserToOrderColumns = false;

            //add all the columns 
            this.configRoomsGridView.Columns.Add(this.CreateTextBoxColumn("Address", "M-Net", false));
            this.configRoomsGridView.Columns.Add(this.CreateTextBoxColumn("UnitID", "Group", true));
            this.configRoomsGridView.Columns.Add(this.CreateTextBoxColumn("Name", "Name", false));
            this.configRoomsGridView.Columns.Add(this.CreateTextBoxColumn("Number", "Number", false));
            this.configRoomsGridView.Columns.Add(this.CreateTextBoxColumn("Floor", "Floor", false));
            this.configRoomsGridView.Columns.Add(this.CreateTextBoxColumn("Building", "Building", false));

            this.configRoomsGridView.Columns.Add(this.CreateTextBoxColumn("EMSRoomID", "EMS Room ID", false));

            this.configRoomsGridView.Columns.Add(this.CreateCheckBoxColumn("IsOffice", "Is this an Office?"));
            this.configRoomsGridView.Columns.Add(this.CreateTimeComboBoxColumn("startHour", "Office Start Hour"));
            this.configRoomsGridView.Columns.Add(this.CreateTimeComboBoxColumn("endHour", "Office Start Hour"));
            this.configRoomsGridView.Columns.Add(this.CreateTextBoxColumn("DaysOffHeader", "", true));
            this.configRoomsGridView.Columns.Add(this.CreateCheckBoxColumn("sundayOff", "Sun"));
            this.configRoomsGridView.Columns.Add(this.CreateCheckBoxColumn("mondayOff", "M"));
            this.configRoomsGridView.Columns.Add(this.CreateCheckBoxColumn("tuesdayOff", "T"));
            this.configRoomsGridView.Columns.Add(this.CreateCheckBoxColumn("wednesdayOff", "W"));
            this.configRoomsGridView.Columns.Add(this.CreateCheckBoxColumn("thursdayOff", "Th"));
            this.configRoomsGridView.Columns.Add(this.CreateCheckBoxColumn("fridayOff", "F"));
            this.configRoomsGridView.Columns.Add(this.CreateCheckBoxColumn("saturdayOff", "S"));
        }

        DataGridViewTextBoxColumn CreateTextBoxColumn(string heading, string dataName, bool isRO) {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.ReadOnly = isRO;
            column.DataPropertyName = heading;
            column.Name = dataName;
            return column;
        }

        DataGridViewComboBoxColumn CreateComboBoxColumn(Enum theEnum, string heading, string dataName) {
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(theEnum.GetType());
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            combo.DataPropertyName = heading;
            combo.Name = dataName;
            return combo;
        }

        DataGridViewComboBoxColumn CreateTimeComboBoxColumn(string heading, string dataName) {
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i)).ToString("HH:mm")).ToList();
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            combo.DataPropertyName = heading;
            combo.Name = dataName;
            return combo;
        }

        DataGridViewCheckBoxColumn CreateCheckBoxColumn(string heading, string dataName) {
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = heading;
            column.Name = dataName;
            return column;
        }

        public void LoadRoomConfig() {
            List<Room> allRooms = AWSCommunication.GetAllRooms();
            foreach (Room aRoom in allRooms.OrderBy(x => x.Floor)) {
                this.AllRooms.Add(aRoom);
               // this.OrderedRoom.Add(aRoom.UnitID, aRoom);
            }           
        }

        private void ConfigureForm_FormClosing(Object sender, FormClosingEventArgs e) {
            try {
                //write to AWS 
                AWSCommunication.BatchWriteRoomsToAWS(this.AllRooms.ToList());
            }
            catch(Exception ex) {
                MessageBox.Show("Could not save your changes! They are being lost...sorry!\nError: " + ex.Message);
            }

            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
