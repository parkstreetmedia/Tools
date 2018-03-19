using RRHDMIControl.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRHDMIControl {
    public partial class HDMISwitchRemote : Form {
        SerialPort theSerialConn;
        string SerialPort = "COM1";
        string SerialCommandResponse = "";

        public HDMISwitchRemote() {
            InitializeComponent();
            //while we spin up
            this.SetAllButtonsEnabled(false);
            //from user settings file
            this.ReadSettings();

            //set global hotkeys
            KeyboardHook hook = new KeyboardHook();
            hook.KeyPressed +=
                new EventHandler<KeyPressedEventArgs>(hook_globalSelect1);
            // register the control + alt + F12 combination as hot key.
            hook.RegisterHotKey(RRHDMIControl.ModifierKeys.Control | RRHDMIControl.ModifierKeys.Alt, Keys.Q);

            KeyboardHook hook2 = new KeyboardHook();
            hook2.KeyPressed +=
                new EventHandler<KeyPressedEventArgs>(hook_globalSelect2);
            // register the control + alt + F12 combination as hot key.
            hook2.RegisterHotKey(RRHDMIControl.ModifierKeys.Control | RRHDMIControl.ModifierKeys.Alt, Keys.W);

            KeyboardHook hook3 = new KeyboardHook();
            hook3.KeyPressed +=
                new EventHandler<KeyPressedEventArgs>(hook_globalSelect3);
            // register the control + alt + F12 combination as hot key.
            hook3.RegisterHotKey(RRHDMIControl.ModifierKeys.Control | RRHDMIControl.ModifierKeys.Alt, Keys.E);

            KeyboardHook hook4 = new KeyboardHook();
            hook4.KeyPressed +=
                new EventHandler<KeyPressedEventArgs>(hook_globalSelect4);
            // register the control + alt + F12 combination as hot key.
            hook4.RegisterHotKey(RRHDMIControl.ModifierKeys.Control | RRHDMIControl.ModifierKeys.Alt, Keys.R);


            this.theSerialConn = new SerialPort();
            this.theSerialConn.BaudRate = 9600;
            this.theSerialConn.Parity = Parity.None;
            this.theSerialConn.PortName = this.SerialPort;
            this.theSerialConn.StopBits = StopBits.One;
            this.theSerialConn.DataBits = 8;
            this.theSerialConn.Handshake = Handshake.None;
            this.theSerialConn.DataReceived += TheSerialConn_DataReceived;


            try {
                this.theSerialConn.Open();
                this.theSerialConn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Cannot open the serial port\nPlease fix the problem and restart me\nException follows:\n" + ex.Message, "Error");
            }

            this.GetSelectedInput();
        }

        private void TheSerialConn_DataReceived(object sender, SerialDataReceivedEventArgs e) {     
            var sp = (SerialPort)sender;
            string availableData = sp.ReadExisting();
           // MessageBox.Show(availableData);    
            
            if (availableData.Trim().Contains("AVx1"))
            {
                this.ParseStatus(availableData);
            }
            if (this.txtBoxStatus.Text.Length > 2000){
                this.txtBoxStatus.Text = "";
            } 
            this.txtBoxStatus.Text = availableData + "\n\r" + this.txtBoxStatus.Text;

        }

        void hook_globalSelect1(object sender, KeyPressedEventArgs e)
        {
            this.changeInput1Checkbox_CheckedChanged(null, null);
        }

        void hook_globalSelect2(object sender, KeyPressedEventArgs e)
        {
            this.changeInput2Checkbox_CheckedChanged(null, null);
        }

        void hook_globalSelect3(object sender, KeyPressedEventArgs e)
        {
            this.changeInput3Checkbox_CheckedChanged(null, null);
        }

        void hook_globalSelect4(object sender, KeyPressedEventArgs e)
        {
            this.changeInput4Checkbox_CheckedChanged(null, null);
        }

        private string SendCommand(string commandString) {
            //only one command may be sent at a time... but we don't want to just let the app not respond...
            this.SetAllButtonsEnabled(false);
            try {
                if (!this.theSerialConn.IsOpen) {
                    this.theSerialConn.Open();
                }
                //byte[] theEnd = new byte[] { 13, 10 };
                this.theSerialConn.NewLine = "\r";
                this.theSerialConn.WriteLine(commandString);
                

                int timeCount = 0;
                //wait 10 seconds for a response
                while ((this.SerialCommandResponse == string.Empty) && (timeCount < 200)) {
                    System.Threading.Thread.Sleep(50);
                    Application.DoEvents();
                    timeCount++;
                }
            } catch (Exception ex) {
                MessageBox.Show("There was an unexpected error with the serial connection or something:\n" + ex.Message, "Error");
                this.theSerialConn.Close();
            }

            this.SetAllButtonsEnabled(true);
            return this.SerialCommandResponse;            
        }

        private void ReadSettings() {
            try {
                // read setting
                this.SerialPort = (string)Settings.Default["SerialPort"];

                if (((string)Settings.Default["Input1Label"]) != string.Empty) {
                    this.changeInput1Checkbox.Text = Settings.Default["Input1Label"].ToString();
                }

                if (((string)Settings.Default["Input2Label"]) != string.Empty) {
                    this.changeInput2Checkbox.Text = Settings.Default["Input2Label"].ToString();
                }

                if (((string)Settings.Default["Input3Label"]) != string.Empty) {
                    this.changeInput3Checkbox.Text = Settings.Default["Input3Label"].ToString();
                }

                if (((string)Settings.Default["Input4Label"]) != string.Empty) {
                    this.changeInput4Checkbox.Text = Settings.Default["Input4Label"].ToString();
                }
            } catch (Exception) {
                //there must not be any settings... ah well
            }
        }

        private void connectionSetupToolStripMenuItem_Click(object sender, EventArgs e) {
            ConnectionSettings form = new ConnectionSettings();
            form.ShowDialog();
            this.ReadSettings();
        }

        private void inputLabelsToolStripMenuItem_Click(object sender, EventArgs e) {
            InputLabels form = new InputLabels();
            form.ShowDialog();
            this.ReadSettings();
        }


        private void SetAllButtonsEnabled(bool status) {
            this.changeInput1Checkbox.Enabled = status;
            this.changeInput2Checkbox.Enabled = status;
            this.changeInput3Checkbox.Enabled = status;
            this.changeInput4Checkbox.Enabled = status;          
        }

        private void Form_Resize(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void HDMISwitchRemote_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                return;
            }                         
          e.Cancel = true;               
          this.Hide();
        }

        private void notifyIcon_Click(object sender, MouseEventArgs e) {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void DetachInputButtonEvents()
        {
            this.changeInput1Checkbox.CheckedChanged -= changeInput1Checkbox_CheckedChanged;
            this.changeInput2Checkbox.CheckedChanged -= changeInput2Checkbox_CheckedChanged;
            this.changeInput3Checkbox.CheckedChanged -= changeInput3Checkbox_CheckedChanged;
            this.changeInput4Checkbox.CheckedChanged -= changeInput4Checkbox_CheckedChanged;
        }

        private void AttachInputButtonEvents()
        {
            this.changeInput1Checkbox.CheckedChanged += changeInput1Checkbox_CheckedChanged;
            this.changeInput2Checkbox.CheckedChanged += changeInput2Checkbox_CheckedChanged;
            this.changeInput3Checkbox.CheckedChanged += changeInput3Checkbox_CheckedChanged;
            this.changeInput4Checkbox.CheckedChanged += changeInput4Checkbox_CheckedChanged;
        }

        private void changeInput1Checkbox_CheckedChanged(object sender, EventArgs e) {
            this.changeInput("1");
            this.DetachInputButtonEvents();
            this.changeInput1Checkbox.Checked = true;
            this.changeInput2Checkbox.Checked = false;
            this.changeInput3Checkbox.Checked = false;
            this.changeInput4Checkbox.Checked = false;
            this.AttachInputButtonEvents();
            this.changeInput1Checkbox.BackColor = Color.LightGreen;
            this.changeInput2Checkbox.BackColor = default(Color);
            this.changeInput3Checkbox.BackColor = default(Color);
            this.changeInput4Checkbox.BackColor = default(Color);
        }

        private void changeInput2Checkbox_CheckedChanged(object sender, EventArgs e) {
            this.changeInput("2");
            this.DetachInputButtonEvents();
            this.changeInput1Checkbox.Checked = false;
            this.changeInput2Checkbox.Checked = true;
            this.changeInput3Checkbox.Checked = false;
            this.changeInput4Checkbox.Checked = false;
            this.AttachInputButtonEvents();
            this.changeInput1Checkbox.BackColor = default(Color);
            this.changeInput2Checkbox.BackColor = Color.LightGreen;
            this.changeInput3Checkbox.BackColor = default(Color);
            this.changeInput4Checkbox.BackColor = default(Color);                
        }

        private void changeInput3Checkbox_CheckedChanged(object sender, EventArgs e) {
            this.changeInput("3");
            this.DetachInputButtonEvents();
            this.changeInput1Checkbox.Checked = false;
            this.changeInput2Checkbox.Checked = false;
            this.changeInput3Checkbox.Checked = true;
            this.changeInput4Checkbox.Checked = false;
            this.AttachInputButtonEvents();
            this.changeInput1Checkbox.BackColor = default(Color);
            this.changeInput2Checkbox.BackColor = default(Color);
            this.changeInput3Checkbox.BackColor = Color.LightGreen;
            this.changeInput4Checkbox.BackColor = default(Color);         
        }

        private void changeInput4Checkbox_CheckedChanged(object sender, EventArgs e) {
            this.changeInput("4");
            this.DetachInputButtonEvents();
            this.changeInput1Checkbox.Checked = false;
            this.changeInput2Checkbox.Checked = false;
            this.changeInput3Checkbox.Checked = false;
            this.changeInput4Checkbox.Checked = true;
            this.AttachInputButtonEvents();
            this.changeInput1Checkbox.BackColor = default(Color);
            this.changeInput2Checkbox.BackColor = default(Color);
            this.changeInput3Checkbox.BackColor = default(Color);
            this.changeInput4Checkbox.BackColor = Color.LightGreen;
          
        }
              
        private string GetSelectedInput() {
            return this.SendCommand("Status");           
        }

        private void ParseStatus(string selectedInput)
        {
            //1xva2x
           // MessageBox.Show(selectedInput);
            if ((selectedInput.Length < 6) || (!selectedInput.ToLower().Contains("avx1")))
            {
                return;
            }
            if (selectedInput.ToLower().IndexOf("avx1") <= 0)
            {
                return;
            }
            selectedInput = selectedInput.Substring(0,selectedInput.ToLower().IndexOf("avx1"));
            selectedInput = selectedInput.ToLower();
            selectedInput = selectedInput.Trim();
            selectedInput = selectedInput.Replace("\n", "");
            selectedInput = selectedInput.Replace("\r", "");
            selectedInput = selectedInput.Replace("\n\r", "");
            selectedInput = selectedInput.Replace("\r\n", "");
            selectedInput = selectedInput.Replace("\n\n", "");
            selectedInput = selectedInput.Replace("avx1 ", "");
            selectedInput = selectedInput.Replace("av1", "");
            selectedInput = selectedInput.Replace("x", "");
            selectedInput = selectedInput.Trim();
           // MessageBox.Show(selectedInput);

            if (selectedInput != "")
            {
                this.DetachInputButtonEvents();

                this.changeInput1Checkbox.Checked = false;
                this.changeInput2Checkbox.Checked = false;
                this.changeInput3Checkbox.Checked = false;
                this.changeInput4Checkbox.Checked = false;

                switch (selectedInput)
                {
                    case "1":
                        this.changeInput1Checkbox.Checked = true;
                        this.Text = this.changeInput1Checkbox.Text;
                        break;
                    case "2":
                        this.changeInput2Checkbox.Checked = true;
                        this.Text = this.changeInput2Checkbox.Text;
                        break;
                    case "3":
                        this.changeInput3Checkbox.Checked = true;
                        this.Text = this.changeInput3Checkbox.Text;
                        break;
                    case "4":
                        this.changeInput4Checkbox.Checked = true;
                        this.Text = this.changeInput4Checkbox.Text;
                        break;
                }

                this.AttachInputButtonEvents();
            }
            else
            {
                this.Text = "No Input selected!";
                this.DetachInputButtonEvents();

                this.changeInput1Checkbox.Checked = false;
                this.changeInput2Checkbox.Checked = false;
                this.changeInput3Checkbox.Checked = false;
                this.changeInput4Checkbox.Checked = false;
                this.changeInput1Checkbox.BackColor = default(Color);
                this.changeInput2Checkbox.BackColor = default(Color);
                this.changeInput3Checkbox.BackColor = default(Color);
                this.changeInput4Checkbox.BackColor = default(Color);

                this.AttachInputButtonEvents();
            }
        }


        private string changeInput(string inputNumber) {           
            string command = "x" + inputNumber + "AVx1";
            return this.SendCommand(command);
        }

        private void ProjectorRemote_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            //close the serial port...
            if (this.theSerialConn.IsOpen) {
                this.theSerialConn.Close();
            }
            this.theSerialConn.Dispose();
        }


    }
}
