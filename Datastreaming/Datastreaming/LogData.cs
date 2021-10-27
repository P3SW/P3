using System;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class LogData : IData
    {
        public DateTime Created { get; private set; }
        public string LogMessage { get; private set; }
        public string LogLevel { get; private set; }
        public long ExecutionID { get; private set; }
        public long ContextID { get; private set; }
        
        public static DateTime LastRowTimeStamp { get; private set; }

        public LogData(DateTime created, string logMessage, string logLevel, long executionId, long contextId)
        {
            Created = created;
            LogMessage = logMessage;
            LogLevel = logLevel;
            ExecutionID = executionId;
            ContextID = contextId;
            LastRowTimeStamp = created;
        }

        public LogData()
        {
        }
        
        public void ConstructFromSqlReader(SqlDataReader reader)
        {
            Created = (DateTime) reader[0];
            LogMessage = (string) reader[1];
            LogLevel = (string) reader[2];
            ExecutionID = (long) reader[3];
            ContextID = (long) reader[4];
            LastRowTimeStamp = (DateTime) reader[0];
        }        
        
        public string GetChangesQueryString()
        {
            return string.Format("SELECT CREATED, LOG_MESSAGE, LOG_LEVEL, EXECUTION_ID, CONTEXT_ID FROM dbo.logging " +
                                 $"WHERE CREATED > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}'");
        }
    }
}