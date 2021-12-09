using System;
using System.Collections.Generic;
using BlazorApp.Data.Events;
using Microsoft.Data.SqlClient;

namespace BlazorApp.Data
{
    //Class containing lists of reconciliation data. Implements IData which ensures the class contains necessary methods.
    public abstract class LogDataHandler : EventBase
    {
        public List<LogData> LogDataList { get; private set; }
        public List<LogData> newLogDataList { get; private set; }
        public DateTime LastRowTimeStamp { get; private set; }
        private Action<List<LogData>> TriggerUpdate { get; set; }
        
        public LogDataHandler(DateTime managerStartTime, Action<List<LogData>> triggerUpdate)
        {
            LogDataList = new List<LogData>();
            LastRowTimeStamp = managerStartTime;
            TriggerUpdate = triggerUpdate;
        }
        
        //Inserts data from the reader into temporary list and adds these to the full list of data.
        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            newLogDataList = new List<LogData>();
            while (reader.Read())
            {
                newLogDataList.Add(new LogData(reader));
            }

            LastRowTimeStamp = newLogDataList[newLogDataList.Count - 1].Timestamp;
            LogDataList.AddRange(newLogDataList);
            
            TriggerUpdate(newLogDataList);
        }
    }
}
