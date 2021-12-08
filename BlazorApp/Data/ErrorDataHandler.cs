using System;
using System.Collections.Generic;
using SQLDatabaseRead;

namespace BlazorApp.Data
{
    public class ErrorDataHandler : LogDataHandler, IDataHandler
    {
        private int _executionId;
        
        public ErrorDataHandler(DateTime managerStartTime, Action<List<LogData>> triggerUpdate, int executionId) 
            : base(managerStartTime, triggerUpdate)
        {
            _executionId = executionId;
        }
        
        //Returns a query string with the latest timestamp to ensure only new data is queried.
        public string GetNewestDataQueryString()
        {
            return string.Format("SELECT DISTINCT [CREATED], [LOG_MESSAGE], [LOG_LEVEL],  " +
                                 "[dbo].[LOGGING_CONTEXT].[CONTEXT] " +
                                 "FROM [dbo].[LOGGING] " +
                                 "INNER JOIN [dbo].[LOGGING_CONTEXT] " +
                                 "ON (LOGGING.CONTEXT_ID = LOGGING_CONTEXT.CONTEXT_ID AND LOGGING.EXECUTION_ID = LOGGING_CONTEXT.EXECUTION_ID) " + 
                                 $"WHERE CREATED > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                 $"AND [LOGGING_CONTEXT].[EXECUTION_ID] = '{_executionId}'"+
                                 "ORDER BY CREATED");
        }
    }
}