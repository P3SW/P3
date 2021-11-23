using System;
using System.Collections.Generic;
using BlazorApp.DataStreaming.Events;
using Microsoft.Data.SqlClient;
using SQLDatabaseRead;

namespace BlazorApp.Data
{
    //Class containing lists of reconciliation data. Implements IData which ensures the class contains necessary methods.
    public class LogDataHandler : EventBase, IDataHandler
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

        //Returns a query string with the latest timestamp to ensure only new data is queried.
        public string GetNewestDataQueryString(string type)
        {
            switch (type)
            {
                case "ERROR" :
                    return string.Format($"SELECT DISTINCT [CREATED], [LOG_MESSAGE], [LOG_LEVEL],  " +
                                         $"[dbo].[LOGGING_CONTEXT].[CONTEXT] " +
                                         $"FROM [dbo].[LOGGING] " +
                                         $"INNER JOIN [dbo].[LOGGING_CONTEXT] " +
                                         $"ON (LOGGING.CONTEXT_ID = LOGGING_CONTEXT.CONTEXT_ID) " +
                                         $"WHERE CREATED > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                         $"ORDER BY CREATED");
                case "RECONCILIATION" :
                    return string.Format($"SELECT [AFSTEMTDATO],[DESCRIPTION],[AFSTEMRESULTAT],[MANAGER]" +
                                         $"FROM [dbo].[AFSTEMNING] WHERE AFSTEMTDATO > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}' " +
                                         $"ORDER BY AFSTEMTDATO");
                default:
                    throw new Exception("Type not okay");
            }
            
        }

        public void PrintLogData(List<LogData> LogDataList)
        {
            foreach (var recon in LogDataList)
            {
                Console.WriteLine(recon.Timestamp + " " + recon.Grade + " " + recon.ManagerName + " " + recon.Description);
            }
        }
    }
}
