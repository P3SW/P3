using System;
using System.Globalization;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace DataStreamingSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            const int increaseInSec = 5;
            string[] tablesToStream = {"AFSTEMNING", "AUDIT_LOGERROR", "ENGINE_PROPERTIES", "HEALTH_REPORT", "MANAGER_TRACKING"};
            
            //DatabaseConnect sqlConnection = new DatabaseConnect();
            try
            {
                
                CultureInfo provider = CultureInfo.InvariantCulture;
                
                DateTime startTime = DateTime.Parse("2021-10-05 15:06:20.823");
                //DateTime startTime = DateTime.ParseExact("2021-10-05 15:06:20.823", "yyyy-MM-dd HH:mm:ss.fff", provider);
                DateTime nextTime = startTime.AddSeconds(increaseInSec);
                
                while (true)
                {
                    DatabaseConnect sqlConnection = new DatabaseConnect();

                    for (int i = 0; i < tablesToStream.Length; i++)
                    {
                        Console.WriteLine("startTime " + startTime);
                        string queryString = makeQueryString(tablesToStream[i], startTime, nextTime);
                        sqlConnection.SqlConnect(queryString,sqlConnection.ReadSetupFile(), false);
                    }
                    
                    System.Threading.Thread.Sleep(1 * 1000);
                    
                    startTime = nextTime;
                    nextTime = startTime.AddSeconds(increaseInSec);
                }
                
                // Int32 maxTableCount = sqlConnection.GetMaxRowsInDB(tablesToStream, sqlConnection.ReadSetupFile());
                // for (int i = 0; maxTableCount > i; i++)
                // {
                //     string queryString = makeQueryString(startTime, nextTime);
                //     
                //     sqlConnection.SqlConnect(queryString,sqlConnection.ReadSetupFile(), true);
                //     
                //     System.Threading.Thread.Sleep(increaseInSec * 1000);
                //     
                //     startTime = nextTime;
                //     nextTime = startTime.AddSeconds(increaseInSec);
                // }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        static string makeQueryString(string table, DateTime startTime, DateTime nextTime)
        {
            string queryString = null;
            
            switch (table)
            {
                case "AFSTEMNING": // 2021-10-05 15:08:05.277
                    queryString = $@"SELECT * FROM AFSTEMNING
                                     WHERE START_TIME BETWEEN '{startTime}' and '{nextTime}';";
                    break;
                case "AUDIT_LOGERROR": // 2021-10-05 15:41:08.663
                    queryString = $@"SELECT * FROM AUDIT_LOGERROR
                                     WHERE CREATED BETWEEN '{startTime}' and '{nextTime}';"; 
                    break;
                case "ENGINE_PROPERTIES": // 2021-10-05 15:06:20.823
                    queryString = $@"SELECT * FROM ENGINE_PROPERTIES
                                     WHERE TIMESTAMP BETWEEN '{startTime}' and '{nextTime}';";
                    break;
                case "HEALTH_REPORT": // NULL
                    queryString = $@"SELECT * FROM HEALTH_REPORT
                                     WHERE LOG_TIME BETWEEN '{startTime}' and '{nextTime}';";
                    break;
                case "MANAGER_TRACKING": // 2021-10-05 00:00:00.000
                    queryString = $@"SELECT * FROM MANAGER_TRACKING
                                     WHERE STARTTIME BETWEEN '{startTime}' and '{nextTime}';";
                    break;
                default:
                    break;
            }
            
            return queryString;
            
            // 2021-10-05 15:08:05.277
            string afstemning = $@"SELECT * FROM AFSTEMNING
                                    WHERE START_TIME BETWEEN '{startTime}' and '{nextTime}';";
            // 2021-10-05 15:41:08.663
            string auditLogError = $@"SELECT * FROM AUDIT_LOGERROR
                                     WHERE CREATED BETWEEN '{startTime}' and '{nextTime}';";
            // 2021-10-05 15:23:17.960
            string engineProperties = $@"SELECT * FROM ENGINE_PROPERTIES
                                         WHERE TIMESTAMP BETWEEN '{startTime}' and '{nextTime}';";
            // NULL
            string healthReport = $@"SELECT * FROM HEALTH_REPORT
                                     WHERE LOG_TIME BETWEEN '{startTime}' and '{nextTime}';";
            // 2021-10-05 00:00:00.000
            string managerTracking = $@"SELECT * FROM MANAGER_TRACKING
                                     WHERE STARTTIME BETWEEN '{startTime}' and '{nextTime}';";

            // string test01 = "SELECT TOP 1 * FROM AFSTEMNING UNION ALL ";
            // string test02 = "SELECT TOP 1 * FROM AUDIT_LOGERROR  ";
            // string test03 = "SELECT TOP 1 * FROM ENGINE_PROPERTIES  ";
            // string test04 = "SELECT TOP 1 * FROM HEALTH_REPORT  ";
            // string test05 = "SELECT TOP 1 * FROM MANAGER_TRACKING;";
            //
            // string test06 =
            //     "SELECT TOP 1 * FROM AFSTEMNING, AUDIT_LOGERROR, ENGINE_PROPERTIES, HEALTH_REPORT, MANAGER_TRACKING";

            return afstemning; //test01 + test02 + test03 + test04 + test05;
            return afstemning + auditLogError + engineProperties + healthReport + managerTracking;
        }
        
    }
}