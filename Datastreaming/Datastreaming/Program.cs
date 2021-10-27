using System;
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
                //string queryString = "SELECT REPORT_TYPE, REPORT_KEY, REPORT_NUMERIC_VALUE, REPORT_VALUE_TYPE, REPORT_VALUE_HUMAN FROM dbo.HEALTH_REPORT WHERE REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY' OR REPORT_TYPE = 'NETWORK'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    PrintConnection(connection);
                    SqlDependency.Stop(connectionString);
                    SqlDependency.Start(connectionString);
                    
                    TableStreamer<HealthData> streamer = new TableStreamer<HealthData>(connection, connectionString, 
                        "SELECT REPORT_TYPE, REPORT_KEY, REPORT_NUMERIC_VALUE, REPORT_VALUE_TYPE, REPORT_VALUE_HUMAN, LOG_TIME FROM dbo.HEALTH_REPORT WHERE MONITOR_NO = 8");
                    streamer.StartListening();
                    TableStreamer<LogData> streamer2 = new TableStreamer<LogData>(connection, connectionString,
                        "SELECT CREATED, LOG_MESSAGE, LOG_LEVEL, EXECUTION_ID, CONTEXT_ID FROM dbo.logging");
                    streamer2.StartListening();
                    
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

        private static void PrintReader(SqlDataReader reader)
        {
            Console.WriteLine("{0}, {1}", reader[0], reader[1]);
        }
    }
}