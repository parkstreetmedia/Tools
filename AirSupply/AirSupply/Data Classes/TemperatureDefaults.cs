using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace AirSupply
{
    [DynamoDBTable("TemperatureDefaults")]
    public class TemperatureDefaults
    {
        [DynamoDBIgnore]
        const string WEATHERAPIURL = "https://forecast.weather.gov/MapClick.php?lat=REPLACELAT&lon=REPLACELONG&FcstType=dwml";
        [DynamoDBIgnore]
        private string tempDefaultID;

        [DynamoDBHashKey]
        public string TempDefaultID { get { return "1"; } set { this.tempDefaultID = value; } }

        public double ForecastMax { get; set; }
        public double ForecastMin { get; set; }
        public double ForecastAvg { get; set; }
        public string LocationLat { get; set; }
        public string LocationLong { get; set; }
        public string ForecastTempToUse { get; set; }
        public string HeatOccupiedRoomsTo { get; set; }
        public string HeatUnoccupiedRoomsTo { get; set; }
        public string CoolOccupiedRoomsTo { get; set; }
        public string CoolUnoccupiedRoomsTo { get; set; }
        public int TempToSwitchToHeat { get; set; }
        public int TempToSwitchToCool { get; set; }

        public TemperatureDefaults() { }

        public TemperatureDefaults(bool getForcast) {
            if (getForcast) {
                this.GetForecast();
            }
        }

        public Enums.Mode WhatModeToSet() {
           if ((!string.IsNullOrEmpty(this.ForecastTempToUse)) && (this.ForecastMin > 0) && (this.ForecastMax > 0)) {
                double outsideTemp = this.ForecastAvg;
                switch (this.ForecastTempToUse) {
                    case "Average of Daily High and Daily Low Forecast": outsideTemp = this.ForecastAvg; break;
                    case "Daily High Forecast": outsideTemp = this.ForecastMax; break;
                    case "Daily Low Forecast": outsideTemp = this.ForecastMin; break;
                }

                if (outsideTemp < this.TempToSwitchToHeat) {
                    return Enums.Mode.AUTOHEAT;
                }
                if (outsideTemp > this.TempToSwitchToCool) {
                    return Enums.Mode.AUTOCOOL;
                }               
            }
            //don't know what to do... just look like you are doing something
            return Enums.Mode.FAN;
        }

        public bool GetForecast() {
            try {

                double max = 0.0;
                double min = 0.0;

                string getURL = WEATHERAPIURL.Replace("REPLACELAT", this.LocationLat);
                getURL = getURL.Replace("REPLACELONG", this.LocationLong);
                getURL = getURL.Replace("REPLACEDATE", DateTime.Now.ToString("YYYY-MM-DD"));
                using (WebClient client = new WebClient()) {
                    client.Headers.Add("User-Agent: Chrome");
                    string xmlContent = client.DownloadString(getURL);
                    //the first daily max and min are the ones we want, so we'll do it dirty 
                    int locationOfMin = xmlContent.IndexOf("Daily Minimum Temperature</name>");
                    if (locationOfMin > 0) {
                        string xmlMin = xmlContent.Substring(locationOfMin);
                        locationOfMin = xmlMin.IndexOf("<value>") + 7;
                        if (locationOfMin > 0) {
                            xmlMin = xmlMin.Substring(locationOfMin);
                            string onlyMin = xmlMin.Substring(0, xmlMin.IndexOf("<"));
                            if (Double.TryParse(onlyMin, out min)) {
                                this.ForecastMin = min;
                            }
                        }
                    }

                    int locationOfMax = xmlContent.IndexOf("Daily Maximum Temperature</name>");
                    if (locationOfMax > 0) {
                        string xmlMax = xmlContent.Substring(locationOfMax);
                        locationOfMax = xmlMax.IndexOf("<value>") + 7;
                        if (locationOfMax > 0) {
                            xmlMax = xmlMax.Substring(locationOfMax);
                            string onlyMax = xmlMax.Substring(0, xmlMax.IndexOf("<"));
                            if (Double.TryParse(onlyMax, out max)) {
                                this.ForecastMax = max;
                            }
                        }
                    }

                    if (max > 0 && min > 0) {
                        this.ForecastAvg = ((max + min) / 2);
                    }
                }
                return true;
            }
            catch (Exception) { }
            return false;
        }

        public string SelectCorrectTempInF(Enums.Mode theMode, bool isOccupied) {
            string temp = "60";
            if (theMode == Enums.Mode.AUTOCOOL) {
                if (isOccupied) {
                    temp = this.CoolOccupiedRoomsTo;
                }
                else {
                    temp = this.CoolUnoccupiedRoomsTo;
                }
            }
            else {
                if (isOccupied) {
                    temp = this.HeatOccupiedRoomsTo;
                }
                else {
                    temp = this.HeatUnoccupiedRoomsTo;
                }
            }

            return ToCWithCorrectLimits(theMode, temp);
        }

        public static string ToCWithCorrectLimits(Enums.Mode theMode, string temp) {
            Double tempD;

            if (Double.TryParse(temp, out tempD)) {
                if (tempD == 0) {
                    return "19"; //default on error
                }

                //convert to C
                tempD = FtoC(tempD);
                tempD = RoundWithCorrectLimit(tempD);

                if (theMode == Enums.Mode.COOL || theMode == Enums.Mode.DRY) {
                    if (tempD < 19) {
                        return "19";
                    }
                    if (tempD > 30) {
                        return "30";
                    }
                }

                if (theMode == Enums.Mode.HEAT || theMode == Enums.Mode.FAN) {
                    if (tempD < 17) {
                        return "17";
                    }
                    if (tempD > 28) {
                        return "28";
                    }
                }

                if (theMode == Enums.Mode.AUTO || theMode == Enums.Mode.AUTOHEAT || theMode == Enums.Mode.AUTOCOOL) {
                    if (tempD < 19) {
                        return "19";
                    }
                    if (tempD > 28) {
                        return "28";
                    }
                }

                return tempD.ToString();
            }
            return "19"; //default
        }

        public static double RoundWithCorrectLimit(double c) {
            //So we can only set in whole or half numbers, the system doesn't support anything else.
            c = c * 2;
            c = Math.Round(c, MidpointRounding.AwayFromZero);
            c = c / 2;
            return c;
        }

        public static double CtoF(double c) {
            return ((9.0 / 5.0) * c) + 32;
        }

        public static double FtoC(double f) {
            return ((5.0 / 9.0) * (f - 32));
        }

    }
}
