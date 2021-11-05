using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public class ReconciliationLog : IData
    {
        public List<ReconciliationData> ReconciliationList { get; private set; }
        public List<ReconciliationData> newReconciliationData { get; private set; }
        public static DateTime LastRowTimeStamp { get; private set; }

        public ReconciliationLog()
        {
            ReconciliationList = new List<ReconciliationData>();
        }
        
        //Inserts data from the reader into temporary list and adds these to the full list of data.
        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            newReconciliationData = new List<ReconciliationData>();
            while (reader.Read())
            {
                newReconciliationData.Add(new ReconciliationData(reader));
                LastRowTimeStamp = (DateTime) reader[0];
            }
            ReconciliationList.AddRange(newReconciliationData);
        }

        //Returns a query string with the latest timestamp to ensure only new data is queried.
        public string GetChangesQueryString()
        {
            return string.Format($"SELECT [AFSTEMTDATO],[DESCRIPTION],[MANAGER],[AFSTEMRESULTAT]" +
                                 $"FROM [dbo].[AFSTEMNING] WHERE AFSTEMTDATO > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}' " +
                                 $"ORDER BY AFSTEMTDATO");
        }
    }
}