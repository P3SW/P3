using System;
using System.Collections.Generic;
using BlazorApp.DataStreaming;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class ReconciliationLog : IData
    {
        public List<ReconciliationData> ReconciliationList { get; private set; }
        private List<ReconciliationData> newReconciliationData;
        public static DateTime LastRowTimeStamp { get; private set; }

        public ReconciliationLog()
        {
            ReconciliationList = new List<ReconciliationData>();
        }
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

        public string GetChangesQueryString()
        {
            return string.Format($"SELECT [AFSTEMTDATO],[DESCRIPTION],[MANAGER],[AFSTEMRESULTAT]" +
                                 $"FROM [dbo].[AFSTEMNING] WHERE AFSTEMTDATO > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}' " +
                                 $"ORDER BY AFSTEMTDATO");
        }
    }
}