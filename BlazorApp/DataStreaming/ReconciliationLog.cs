using System;
using System.Collections.Generic;
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
                  
            }
            
        }

        public string GetChangesQueryString()
        {
            throw new System.NotImplementedException();
        }
    }
}