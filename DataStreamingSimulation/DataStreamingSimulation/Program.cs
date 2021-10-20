using System;
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
            
            DatabaseConnect sqlConnection = new DatabaseConnect();
            
            try
            {
                DateTime startTime = DateTime.Parse("2021-10-05 15:08:05.277");
                DateTime nextTime = startTime.AddSeconds(increaseInSec);
                
                Int32 maxTableCount = sqlConnection.GetMaxRowsInDB(tablesToStream, sqlConnection.ReadSetupFile());
                
                for (int i = 0; maxTableCount > i; i++)
                {
                    string queryString = makeQueryString(startTime, nextTime);
                    
                    sqlConnection.SqlConnect(queryString,sqlConnection.ReadSetupFile(), true);
                    
                    System.Threading.Thread.Sleep(increaseInSec * 1000);
                    
                    startTime = nextTime;
                    nextTime = startTime.AddSeconds(increaseInSec);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        static string makeQueryString(DateTime startTime, DateTime nextTime)
        {
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
            
            return afstemning + auditLogError + engineProperties + healthReport + managerTracking;
        }
        
    }
}