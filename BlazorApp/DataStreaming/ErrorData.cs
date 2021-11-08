using System;
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
            Created = (DateTime) reader[0];
            LogMessage = (string) reader[1];
            LogLevel = (string) reader[2];
            ManagerName = (string) reader[3];
        }
    }
}