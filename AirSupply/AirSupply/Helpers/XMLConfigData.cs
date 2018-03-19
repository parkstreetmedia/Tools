using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AirSupply
{
    public class XMLConfigData
    {
        public static string AWSKey;
        public static string AWSSecret;
        public static string C50ControlURL;
        public static string EMSSQLConnectionString;
        public static string AWSDynamoCredsIAM;
        public static string AWSDynamoCredsUsername;
        public static string AWSDynamoCredsPassword;
        public static bool FakeC50Responses;
        public static bool VerboseLoggingToAWS;

        static XMLConfigData() {
            //Load keys
            XDocument xmlKeys = XDocument.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\Accounts.xml");
            foreach (var aKey in xmlKeys.Descendants("AWSKeys")) {
                foreach (var aKeyAttribute in aKey.Elements("Key")) {
                    foreach (var keyItem in aKeyAttribute.Attributes()) {
                        if (keyItem.Name == "TheKey") {
                            AWSKey = keyItem.Value;
                        }

                        if (keyItem.Name == "TheSecret") {
                            AWSSecret = keyItem.Value;
                        }
                    }
                }
            }

            foreach (var aKey in xmlKeys.Descendants("C50")) {
                foreach (var aKeyAttribute in aKey.Elements("Server")) {
                    foreach (var keyItem in aKeyAttribute.Attributes()) {
                        if (keyItem.Name == "EndPoint") {
                            C50ControlURL = keyItem.Value;
                        }
                    }
                }
            }

            foreach (var aKey in xmlKeys.Descendants("SQLAccounts")) {
                foreach (var aKeyAttribute in aKey.Elements("SQL")) {
                    foreach (var keyItem in aKeyAttribute.Attributes()) {
                        if (keyItem.Name == "ConnectionString") {
                            EMSSQLConnectionString = keyItem.Value;
                        }
                    }
                }
            }

            foreach (var aKey in xmlKeys.Descendants("AWSLogs")) {
                foreach (var aKeyAttribute in aKey.Elements("Account")) {
                    foreach (var keyItem in aKeyAttribute.Attributes()) {
                        if (keyItem.Name == "AccountNumber") {
                            AWSDynamoCredsIAM = keyItem.Value;
                        }

                        if (keyItem.Name == "Username") {
                            AWSDynamoCredsUsername = keyItem.Value;
                        }

                        if (keyItem.Name == "Password") {
                            AWSDynamoCredsPassword = keyItem.Value;
                        }
                    }
                }
            }

            foreach (var aKey in xmlKeys.Descendants("Testing")) {
                foreach (var aKeyAttribute in aKey.Elements("Debugging")) {
                    foreach (var keyItem in aKeyAttribute.Attributes()) {
                        if (keyItem.Name == "FakeC50Responses") {
                            FakeC50Responses = Boolean.Parse(keyItem.Value);
                        }

                        if (keyItem.Name == "VerboseLoggingToAWS") {
                            VerboseLoggingToAWS = Boolean.Parse(keyItem.Value);
                        }                        
                    }
                }
            }
        }
    }
}

