using System;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    //Class responsible for storing data from the logging table. Contains a constructor to create object from reader.
    public class ErrorData
    {
        public DateTime Created { get; private set; }
        public string LogMessage { get; private set; }
        public string LogLevel { get; private set; }
        public string ManagerName { get; private set; }
        public ErrorData(DateTime created, string logMessage, string logLevel, string managerName)
        {
            Created = created;
            LogMessage = logMessage;
            LogLevel = logLevel;
            ManagerName = managerName;
        }
        
        public ErrorData(SqlDataReader reader)
        {
            while (reader.Read())
            {
                Created = (DateTime) reader["CREATED"];
                LogMessage = (string) reader["LOG_MESSAGE"];
                //Console.WriteLine(LogMessage);
                LogLevel = (string) reader["LOG_LEVEL"];
                ManagerName = (string) reader["CONTEXT"];
                //Console.WriteLine("ManagerName");
                //Console.WriteLine(ManagerName);
            }

            Console.WriteLine("End of read");
            
        }
    }
}