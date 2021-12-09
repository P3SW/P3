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
        private List<LogData> NewLogDataList { get; set; }
        protected DateTime LastRowTimeStamp { get; private set; }
        private Action<List<LogData>> TriggerUpdate { get; set; }

        protected LogDataHandler(DateTime managerStartTime, Action<List<LogData>> triggerUpdate)
        {
            LogDataList = new List<LogData>();
            LastRowTimeStamp = managerStartTime;
            TriggerUpdate = triggerUpdate;
        }
        
        //Inserts data from the reader into temporary list and adds these to the full list of data.
        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            NewLogDataList = new List<LogData>();
            while (reader.Read())
            {
                NewLogDataList.Add(new LogData(reader));
            }

            LastRowTimeStamp = NewLogDataList[NewLogDataList.Count - 1].Timestamp;
            LogDataList.AddRange(NewLogDataList);
            
            TriggerUpdate(NewLogDataList);
        }
        
        public void AddLogData(DateTime timestamp, string description, string managerName, string grade)
        {
            LogDataList.Add(new LogData(timestamp, description, managerName, grade));
        }
    }
}
