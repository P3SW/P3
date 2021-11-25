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
            const int INCREASE_IN_SEC = 1;
            const bool CONSOLE_PRINT = false;
            const string STREAM_START_DATE = "2021-10-28 15:07:10.347"; 
            string[] tablesToStream = {"AFSTEMNING", "LOGGING", "ENGINE_PROPERTIES", "MANAGER_TRACKING", "HEALTH_REPORT"};
            
            try
            {
                DateTime startTime = DateTime.Parse(STREAM_START_DATE);
                DateTime nextTime = startTime.AddSeconds(INCREASE_IN_SEC);
                DatabaseConnect sqlConnection = new DatabaseConnect();
                string setupFile = sqlConnection.ReadSetupFile();
                
                string queryString = new QuerySetup().MakeOneTimeQueryString("MANAGERS");
                sqlConnection.SqlConnect(queryString,setupFile, CONSOLE_PRINT);
                
                queryString = new QuerySetup().MakeOneTimeQueryString("LOGGING_CONTEXT");
                sqlConnection.SqlConnect(queryString,setupFile, CONSOLE_PRINT);
                
                while (true)
                {
                    for (int i = 0; i < tablesToStream.Length; i++)
                    {
                        queryString = new QuerySetup().MakeQueryString(tablesToStream[i], startTime, nextTime);
                        sqlConnection.SqlConnect(queryString,setupFile, CONSOLE_PRINT);
                    }
                    
                    startTime = nextTime;
                    nextTime = startTime.AddSeconds(INCREASE_IN_SEC);
                    System.Threading.Thread.Sleep(INCREASE_IN_SEC * 10); // How frequently it inserts into ANS_DB_P3
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}