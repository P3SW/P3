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
            const int INCREASE_IN_SEC = 5;
            const bool CONSOLE_PRINT = false;
            const string STREAM_START_DATE = "2021-10-05 15:06:20.823";
            string[] tablesToStream = {"AFSTEMNING", "AUDIT_LOGERROR", "ENGINE_PROPERTIES", "HEALTH_REPORT", "MANAGER_TRACKING"};
            
            try
            {
                DateTime startTime = DateTime.Parse(STREAM_START_DATE);
                DateTime nextTime = startTime.AddSeconds(INCREASE_IN_SEC);
                while (true)
                {
                    DatabaseConnect sqlConnection = new DatabaseConnect();
                    string setupFile = sqlConnection.ReadSetupFile();

                    for (int i = 0; i < tablesToStream.Length; i++)
                    {
                        string queryString = new QuerySetup().makeQueryString(tablesToStream[i], startTime, nextTime);
                        sqlConnection.SqlConnect(queryString,setupFile, CONSOLE_PRINT);
                    }
                    
                    startTime = nextTime;
                    nextTime = startTime.AddSeconds(INCREASE_IN_SEC);
                    System.Threading.Thread.Sleep(INCREASE_IN_SEC * 1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}