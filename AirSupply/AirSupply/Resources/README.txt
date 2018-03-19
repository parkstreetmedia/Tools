README - 
This is two different executables: 

AirSupply.exe - a user interactive UI that polls from, parses, and sends data to C-50a controller. There is a bit of AWS DynamoDB backing to provide storage for details relating to units/rooms, user defaults, and temperature history. 

EMStoAWS.exe - a windows service that pulls data from an EMS SQL server (not through any EMS api, just through a SQL connection). The reason for this dirty direct pull is to avoid touching the EMS server in any way that may change something. With a Read-Only SQL account, your EMS server is safe. So with recent events pulled from EMS it just creates a DynamoDB object and pushes it into the cloud. There you are free to use the data everywhere and safely. I use AWS API to populate a JSON object with a Lamda Query of the Events Dynamo table... do as you wish. 

SETTING UP ACCOUNTS - 
Open and edit Accounts.xml 
For the C-50a Controller: Edit the IP to yours, you should be able to leave the rest of the URL as is but change as needed 

For AWS: create DynamoDB tables, that isn't handled in the code. It expects the following tables and keys:

Table name	Events
Primary partition key	ID (Number)
Primary sort key	Date (String)

Table name	Rooms
Primary partition key	UnitID (Number)
Primary sort key	EMSRoomID (Number)

Table name	RoomStatus
Primary partition key	DateTimeUpdated (String)
Primary sort key	UnitID (String)

Table name	TemperatureDefaults
Primary partition key	TempDefaultID (String)
Primary sort key	(None - we only ever store 1 object here... it is a hack) 

Table name	LastRun
Primary partition key	ID (String)
Primary sort key	ShortDate (String)

Table name	VerboseLog
Primary partition key	DateTimeOfAction (String)
Primary sort key	UnitID (Number)

To enable your users to view the AWS tables for the log viewer - rather than fetching the data and displaying it, which costs money, I just direct the user to the AWS Console. 
So create a RO user on Dynamo and put the AWS Account number, IAM Username, and IAM Password into the Accounts.xml config to present them handily to the user when they 
click on View Logs. 

For SQL: create a RO user on your EMS Sql Server and grant it permission to run Selects and Connect to the server. Then update the connection string as needed.  
 
INSTALLING THE SERVICE -  

From an administrative console run:
installutil /i EMSToAWS.exe

If installutil is not in your path, it will be around:
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\
Where the version isn't particularly relevant if the installer ran successfully as you have a version of .NET that works. 

Once the service is installed, if you have no entries in the AWS LastRun table all events from the past year will be loaded to AWS (if you have so many events this takes longer than 15 minutes... it could get interesting, but that would be millions of events or a really slow connection). Otherwise, the service will only update the events that have been added/changed/removed since the last run (every 15 minutes). 