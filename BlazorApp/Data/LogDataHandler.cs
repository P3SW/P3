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
        
        //Inserts data from the reader into a list.
        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            LogDataList = new List<LogData>();
            while (reader.Read())
            {
                LogDataList.Add(new LogData(reader));
            }

            TriggerUpdate(LogDataList);
        }
        
        //Inserts data into a list using passed values
        public void AddLogData(DateTime timestamp, string description, string managerName, string grade)
        {
            LogDataList.Add(new LogData(timestamp, description, managerName, grade));
        }
    }
}
