using System;
using System.Collections.Generic;
using BlazorApp.Data;
using SQLDatabaseRead;

namespace BlazorApp.DataStreaming.Events
{
    public class ReconciliationDataHandler : LogDataHandler, IDataHandler
    {
        public ReconciliationDataHandler(DateTime managerStartTime, Action<List<LogData>> triggerUpdate) 
            : base(managerStartTime, triggerUpdate) {}
        
        //Returns a query string to ensure only data for the current manager is queried.
        public string GetNewestDataQueryString()
        {
            return string.Format("SELECT [AFSTEMTDATO],[DESCRIPTION],[AFSTEMRESULTAT],[MANAGER]" +
                                 $"FROM [dbo].[AFSTEMNING] " +
                                 $"WHERE [MANAGER] = '{ConversionDataAssigner.CurrentManager.Name}'" +
                                 "ORDER BY AFSTEMTDATO");
        }
    }
}