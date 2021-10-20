using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
                //string queryString = "SELECT REPORT_TYPE, REPORT_KEY, REPORT_NUMERIC_VALUE, REPORT_VALUE_TYPE, REPORT_VALUE_HUMAN FROM dbo.HEALTH_REPORT WHERE MONITOR_NO = 8/*REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY' OR REPORT_TYPE = 'NETWORK'*/";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //StreamPrinter printer = new StreamPrinter(connectionString);
                    PrintConnection(connection);

                    DBStreamer streamer = new DBStreamer(connection, connectionString);
                    streamer.StartListening();
                    // while (true)
                    // {
                    //     Thread.Sleep(1);
                    // }
                    Console.ReadLine();

                    //SqlCommand command = new SqlCommand(queryString, connection);

                    // using(SqlDataReader reader = command.ExecuteReader())
                    // {
                    //     while (reader.Read())
                    //     {
                    //         PrintReader(reader);
                    //     }
                    // }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
        
        private static void SqlDependencyOnChange(object sender, SqlNotificationEventArgs eventArgs)
        {
            if (eventArgs.Info == SqlNotificationInfo.Invalid)
            {

                Console.WriteLine("The query is not valid.");
                return;
            }

            Console.WriteLine("Change detected!");
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