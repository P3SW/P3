using System;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class ErrorData
    {
        public DateTime Created { get; private set; }
        public string LogMessage { get; private set; }
        public string LogLevel { get; private set; }
        public long ExecutionID { get; private set; }
        public long ContextID { get; private set; }
        
        public ErrorData(DateTime created, string logMessage, string logLevel, long executionId, long contextId)
        {
            Created = created;
            LogMessage = logMessage;
            LogLevel = logLevel;
            ExecutionID = executionId;
            ContextID = contextId;
        }
        
        public ErrorData(SqlDataReader reader)
        {
            Created = (DateTime) reader[0];
            LogMessage = (string) reader[1];
            LogLevel = (string) reader[2];
            ExecutionID = (long) reader[3];
            ContextID = (long) reader[4];
        }
    }
}