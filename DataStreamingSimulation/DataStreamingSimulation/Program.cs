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
            string[] tablesToStream = {"AFSTEMNING", "AUDIT_LOGERROR", "ENGINE_PROPERTIES", "HEALTH_REPORT", "MANAGER_TRACKING"};
            
            // Console.WriteLine(tablesToStream[0]);
            
            const int increaseInSec = 5;
            DatabaseConnect sqlConnection = new DatabaseConnect();
            
            
            
            try
            {
                //DateTime startTime = DateTime.Parse("2021-09-24 01:29:29.887");
                //DateTime startTime = DateTime.Parse("2021-05-15 02:01:08.180");
                DateTime startTime = DateTime.Parse("2021-05-18 14:28:14.060");
                DateTime nextTime = startTime.AddSeconds(increaseInSec);

                Int32 maxTableCount = sqlConnection.GetMaxRowsInDB(tablesToStream, sqlConnection.ReadSetupFile());

                Console.WriteLine("*****************" + maxTableCount);
                
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
        
        public static string makeQueryString(DateTime startTime, DateTime nextTime)
        {
            string afstemning = $@"SELECT * FROM AFSTEMNING
                                    WHERE START_TIME BETWEEN '{startTime}' and '{nextTime}';";
            
            string healthReport = $@"SELECT * FROM HEALTH_REPORT
                                     WHERE LOG_TIME BETWEEN '{startTime}' and '{nextTime}';";

            string test = "ANS_CUSTOM_MVP.Rows.Count";
            
            return healthReport;
        }
        
    }
}