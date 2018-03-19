using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AirSupply
{  

   public static class C50Communication
    {
        const string GETALLSTATUS = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Packet><Command>getRequest</Command><DatabaseManager><Mnet Group=\"PSCUNIT\" Drive=\"*\" Mode=\"*\" SetTemp=\"*\" InletTemp=\"*\" Ventilation=\"*\" AirDirection=\"*\" FanSpeed=\"*\" FilterSign=\"*\" ErrorSign=\"*\"/></DatabaseManager></Packet>";
        const string SETANYPROPERTY = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Packet><Command>setRequest</Command><DatabaseManager><Mnet Group=\"PSCUNIT\" PSCTOSET /></DatabaseManager></Packet>";
        const string SETTEMPANDMODE = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Packet><Command>setRequest</Command><DatabaseManager><Mnet Group=\"PSCUNIT\" SetTemp=\"THENEWTEMP\" Mode=\"THENEWMODE\" /></DatabaseManager></Packet>";
        
        public static bool ShouldWeLog = false;
        public static string LogLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\AirSupplyDebuggingLog.txt";

       public static string SendRequestAndWaitReply(string request) {
            string response = "";
            try {
                if (XMLConfigData.FakeC50Responses) {

                    string a = "<?xml version =\"1.0\" encoding=\"UTF-8\"?><Packet><Command>getResponse</Command><DatabaseManager><Mnet Group=\"AAA\" Drive=\"ON\" Mode=\"HEAT\" SetTemp=\"15.5\" InletTemp=\"20.8\" Ventilation=\"OFF\" AirDirection=\"MID1\" FanSpeed=\"UNKNOWN\" FilterSign=\"ON\" ErrorSign=\"OFF\" />   </DatabaseManager></Packet>";
                    int num = request.IndexOf("Group=");
                    request = request.Substring(num + 7, 3);
                    request = request.Replace("\"", "");
                    request = request.Trim();
                    a = a.Replace("AAA", request);
                    if (ShouldWeLog) {
                        WriteLogLine("Sending Request: " + request);
                        WriteLogLine("Received Response: " + a);
                    }
                    return a;
                }

                if (C50Communication.ShouldWeLog) {
                    WriteLogLine("Sending Request: " + request);
                }

                System.Net.ServicePointManager.Expect100Continue = false;

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(XMLConfigData.C50ControlURL);

                byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(request);
                req.Method = "POST";
                req.ContentType = "text/xml";
                req.ContentLength = requestBytes.Length;
                Stream requestStream = req.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                response = sr.ReadToEnd();

                if (ShouldWeLog) {
                    WriteLogLine("Received Response: " + response);
                }

                sr.Close();
                res.Close();
            }
            catch (Exception ex) {
                if (ShouldWeLog) {
                    WriteLogLine("Exception Caught: " + ex.Message);
                }
                return "";
            }

            return response;
        }

        public static bool SendRequest(string request) {
            try {
                if (XMLConfigData.FakeC50Responses == false) {
                    System.Net.ServicePointManager.Expect100Continue = false;

                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(XMLConfigData.C50ControlURL);

                    byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(request);
                    req.Method = "POST";
                    req.ContentType = "text/xml";
                    req.ContentLength = requestBytes.Length;
                    Stream requestStream = req.GetRequestStream();
                    requestStream.Write(requestBytes, 0, requestBytes.Length);
                    requestStream.Close();
                }

                if (ShouldWeLog) {
                    WriteLogLine("Sending Command: " + request);
                }
            }
            catch (Exception ex) {
                if (ShouldWeLog) {
                    WriteLogLine("Exception Caught: " + ex.Message);
                }
                return false;
            }
            return true;
        }

        public static void WriteLogLine(string line) {
            StreamWriter TheLog = new StreamWriter(LogLocation, true);
            TheLog.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + line);
            TheLog.Flush();
            TheLog.Close();
            TheLog.Dispose();
        }

        public static string GetAllStatus(int unitID) {
            string command = GETALLSTATUS.Replace("PSCUNIT", unitID.ToString());
            return C50Communication.SendRequestAndWaitReply(command);
        }       

        public static bool SetTemp(Enums.Mode theMode, string newTemp, int theUnitID) {
            string command = SETTEMPANDMODE.Replace("THENEWTEMP", newTemp);
            command = command.Replace("THENEWMODE", theMode.ToString());
            command = command.Replace("PSCUNIT", theUnitID.ToString());
            return SendRequest(command);
        }


        public static string SetAnyProperty(string prop, string value, int theUnitID) {
            if ((value.Contains("UNKNOWN")) || (value == "0")) {
                //we shouldn't try and set this value...
                return "";
            }

            string command = SETANYPROPERTY.Replace("PSCUNIT", theUnitID.ToString());
            string toSet = prop + "=\"" + value + "\"";
            command = command.Replace("PSCTOSET", toSet);
            return C50Communication.SendRequestAndWaitReply(command);
        }
    }
}
