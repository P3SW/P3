using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = new ConfigReader().ReadSetupFile(); 
            try
            {
                //Creates DB connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    PrintConnection(connection);
                    
                    //Ensures SqlDependency is stopped before starting it with the correct connection.
                    SqlDependency.Stop(connectionString);
                    SqlDependency.Start(connectionString);
                    
                    //Creates TableStreamers to watch for changes.
                    //It would be quite funny to have TableStreamer implement an interface and make a list of all the objects.

                    List<IStreamer> streamers = new List<IStreamer>();
                    streamers.Add(new TableStreamer<HealthData>(connection, 
                        "SELECT REPORT_TYPE, REPORT_KEY, REPORT_NUMERIC_VALUE, REPORT_VALUE_TYPE, REPORT_VALUE_HUMAN, LOG_TIME FROM dbo.HEALTH_REPORT WHERE MONITOR_NO = 8 ORDER BY LOG_TIME"));
                    streamers.Add(new TableStreamer<LogData>(connection, 
                        "SELECT CREATED, LOG_MESSAGE, LOG_LEVEL, EXECUTION_ID, CONTEXT_ID FROM dbo.logging ORDER BY CREATED"));

                    foreach (IStreamer streamer in streamers)
                    {
                        streamer.StartListening();
                    }

                    //Awaits a newline in the console to make the program run forever.
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        private static void PrintConnection(SqlConnection connection)
        {
            Console.WriteLine("State: {0}", connection.State);
            Console.WriteLine("ConnectionString: {0}",
                connection.ConnectionString);
        }
    }
}