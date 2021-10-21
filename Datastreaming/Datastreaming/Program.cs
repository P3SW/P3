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
                string queryString = "SELECT REPORT_TYPE, REPORT_KEY, REPORT_NUMERIC_VALUE, REPORT_VALUE_TYPE, REPORT_VALUE_HUMAN FROM dbo.HEALTH_REPORT WHERE REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY' OR REPORT_TYPE = 'NETWORK'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    PrintConnection(connection);

                    DbStreamer streamer = new DbStreamer(connection, connectionString);
                    streamer.StartListening();

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