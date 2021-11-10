using System;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    //Class responsible for storing data from the logging table. Contains a constructor to create object from reader.
    public class ErrorData
    {
        public DateTime Timestamp { get; private set; }
        public string LogMessage { get; private set; }
        public string LogLevel { get; private set; }
        public string ManagerName { get; private set; }
        public ErrorData(DateTime timestamp, string logMessage, string logLevel, string managerName)
        {
            Timestamp = timestamp;
            LogMessage = logMessage;
            LogLevel = logLevel;
            ManagerName = managerName;
        }
        
        public ErrorData(SqlDataReader reader)
        {
            Timestamp = (DateTime) reader["CREATED"];
            LogMessage = (string) reader["LOG_MESSAGE"]; 
            LogLevel = (string) reader["LOG_LEVEL"];
            ManagerName = (string) reader["CONTEXT"];
        }
    }
}