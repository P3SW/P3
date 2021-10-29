using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class HealthData : IData
    {
        public List<Data> CPU { get; private set; }
        public List<Data> Memory { get; private set; }

        private List<Data> newCPU;
        private List<Data> newMemory;

        public static DateTime LastRowTimeStamp { get; private set; }

        public HealthData()
        {
            CPU = new List<Data>();
            Memory = new List<Data>();
        }

        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            newCPU = new List<Data>();
            newMemory = new List<Data>();

            while (reader.Read())
            {
                string reportType = (string) reader[0];
                if (reportType.Equals("CPU"))
                {
                    newCPU.Add(new Data(reader));
                }
                else
                {
                    newMemory.Add(new Data(reader));
                }
                LastRowTimeStamp = (DateTime) reader[2];
            }
            CPU.AddRange(newCPU);
            Memory.AddRange(newMemory);
        }
        
        //Returns a query string with the latest timestamp to ensure only new data is queried.
        public string GetChangesQueryString()
        {
            return string.Format($"SELECT REPORT_TYPE, REPORT_NUMERIC_VALUE, LOG_TIME FROM dbo.HEALTH_REPORT " +
                                 $"WHERE MONITOR_NO = 8 AND REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY'" +
                                 $"AND LOG_TIME > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                 "ORDER BY LOG_TIME");
        }
    }
}