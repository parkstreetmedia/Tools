using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.IO;
using AirSupply;

namespace EMSToAWS
{
    public partial class EMSToAWSService : ServiceBase {
        System.Timers.Timer timer = new System.Timers.Timer();
       
        LastRun TheLastRun;
        TemperatureDefaults TempDefaults;

        public EMSToAWSService() {
            InitializeComponent();
        }

        public EMSToAWSService(string[] args) {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            //add new events every 15 minutes 
            timer.Interval = 900000; 
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
            //and run on first start...
            this.DoAllTheThings();
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args) {
            this.DoAllTheThings();
        }

        public void DoAllTheThings() {
            try {

                //so times match, it doesn't matter if they are off by ~20 seconds... rather have keys match in AWS
                DateTime NowTime = DateTime.Now;
            
                //reset from last time 
                SortedList<Room, int> allRooms = new SortedList<Room, int>();
                List<VerboseLog> verboseLogOfActions = new List<VerboseLog>();  

                //Get TemperatureDefaults
                this.TempDefaults = AWSCommunication.GetTemperatureDefaults();
                this.TempDefaults.GetForecast();

                //Get All Rooms
                var allRoomsFromAWS = AWSCommunication.GetAllRooms();

                //Get All Room Temps 
                List<RoomStatus> allRoomStatus = new List<RoomStatus>();
                foreach (Room aRoom in allRoomsFromAWS) {
                    RoomStatus aStatus = new RoomStatus(NowTime, aRoom, this.TempDefaults);
                    allRoomStatus.Add(aStatus);
                    allRooms.Add(aRoom, aRoom.EMSRoomID);
                }

                //Store unit status 
                AWSCommunication.BatchWriteRoomStatusToAWS(allRoomStatus);

                //Get All EMS Events
                List<Event> events;
                if (this.TheLastRun == null) {
                    this.TheLastRun = AWSCommunication.GetMostRecentLastRun();
                }
                if (this.TheLastRun != null) {
                    events = EMSCommunication.GetRecentEvents(this.TheLastRun.LastRunTime);
                }
                else {                    
                    events = EMSCommunication.GetRecentEvents(DateTime.Now.AddDays(-7));
                }

                List<Room> alreadySet = new List<Room>();

                //Set room temps based on events
                foreach (Event anEvent in events) {
                    //assume no one is in a room
                    bool isOccupied = false;

                    //only if the room has an EMS identity
                    if (anEvent.RoomID > 0 && allRooms.ContainsValue(anEvent.RoomID)) {

                        //if an event is just too long... ignore it
                        if((anEvent.TimeEventEnd - anEvent.TimeEventStart).TotalDays > 2) {
                            continue;
                        }

                        //Event is over
                        if (anEvent.TimeEventEnd >= DateTime.Now) {
                            continue;
                        }

                        //Event is still going on
                        if (anEvent.TimeEventStart <= DateTime.Now && anEvent.TimeEventEnd >= DateTime.Now) {
                            isOccupied = true;
                        }

                        //Event is happening in the next 2 hours
                        if (anEvent.TimeEventStart >= DateTime.Now && anEvent.TimeEventStart <= DateTime.Now.AddHours(2)) {
                            isOccupied = true;
                        }

                        if (isOccupied) {
                            foreach (Room aUnit in allRooms.Keys) {
                                if (aUnit.EMSRoomID == anEvent.RoomID) {
                                    //Set temp
                                    string whatTemp = this.SetRoomTemp(aUnit, isOccupied);
                                    if (XMLConfigData.VerboseLoggingToAWS) {
                                        verboseLogOfActions.Add(new VerboseLog(NowTime, aUnit.UnitID, aUnit.EMSRoomID, isOccupied, whatTemp, "Event Going on", anEvent.EventName, anEvent.TimeEventStart, anEvent.TimeEventEnd));
                                    }
                                    //If we just set a room based on an event, don't set it again
                                    if (!alreadySet.Contains(aUnit)) {
                                        alreadySet.Add(aUnit);
                                    }
                                }
                            }        
                        }
                    }
                }

                //being lazy instead of walking the list backwards...
                foreach (Room toRemove in alreadySet) {
                    allRooms.Remove(toRemove);
                }

                //Set room temps based on office schedules
                foreach (Room aRoom in allRooms.Keys) {
                    bool isOccupied = false;

                    if (aRoom.IsOffice) {
                        //check hours 
                        string today = DateTime.Now.ToString("yyyy-MM-dd");
                        DateTime startTime = DateTime.Parse(today + " " + aRoom.StartHour);
                        DateTime endTime = DateTime.Parse(today + " " + aRoom.EndHour);

                        if (DateTime.Now > startTime && DateTime.Now < endTime) {
                            isOccupied = true;
                        }
                        switch (DateTime.Now.Day) {
                            case 0: if (aRoom.SundayOff) { isOccupied = false; } break;
                            case 1: if (aRoom.MondayOff) { isOccupied = false; } break;
                            case 2: if (aRoom.TuesdayOff) { isOccupied = false; } break;
                            case 3: if (aRoom.WednesdayOff) { isOccupied = false; } break;
                            case 4: if (aRoom.ThursdayOff) { isOccupied = false; } break;
                            case 5: if (aRoom.FridayOff) { isOccupied = false; } break;
                            case 6: if (aRoom.SaturdayOff) { isOccupied = false; } break;
                        }
                    }

                    //Set  temp
                    string whatTemp = this.SetRoomTemp(aRoom, isOccupied);
                    if (XMLConfigData.VerboseLoggingToAWS) {
                        verboseLogOfActions.Add(new VerboseLog(NowTime, aRoom.UnitID, aRoom.EMSRoomID, isOccupied, whatTemp, "Regular Hours, No Events"));
                    }
                    
                }
                
                //Add all the events for today to the online calendar 
                int numOfUpdates = AWSCommunication.BatchWriteEventsToAWS(events);

                //Log this run
                LastRun theLastRun = new LastRun(NowTime, NowTime, NowTime.ToShortDateString(), "Success", "", numOfUpdates);
                AWSCommunication.SaveLastRun(theLastRun, true);
                this.TheLastRun = theLastRun;
                if (XMLConfigData.VerboseLoggingToAWS) {
                    AWSCommunication.BatchWriteVerboseLogToAWS(verboseLogOfActions);
                }
            }
            catch (Exception ex) {
                try {
                    LastRun theLastRun = new LastRun(DateTime.Now, DateTime.Now, DateTime.Now.ToShortDateString(), "Fail", "Exception!: " + ex.Message, 0);
                    AWSCommunication.SaveLastRun(theLastRun, false);
                }
                catch (Exception e) {
                    //darn...
                    string output = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\EMSToAWSServiceERROR.txt";
                    StreamWriter ops = new StreamWriter(output, true);
                    ops.WriteLine("EMS To AWS Error");
                    ops.WriteLine("There was an error writing the error to the error log!");
                    ops.WriteLine("The Network is probably down...or something network/AWS access related. Check that or the AWS Credentials");
                    ops.WriteLine("Error Follows");
                    ops.WriteLine(e.Message);
                    ops.WriteLine(e.InnerException);
                    ops.Flush();
                    ops.Close();
                    ops.Dispose();
                }
            }
        }      

        private string SetRoomTemp(Room aUnit, bool isOccupied) {
            Enums.Mode theMode = this.TempDefaults.WhatModeToSet();
            string newTemp  = this.TempDefaults.SelectCorrectTempInF(theMode, isOccupied);
            if (XMLConfigData.FakeC50Responses) {
                return newTemp;
            }
            else {
                // TODO TESTING AHHHHHH!!!!!
                //C50Communication.SetTemp(theMode, newTemp, aUnit.UnitID);
            }
            return newTemp;
        }      

        protected override void OnStop() { }
    }
}
