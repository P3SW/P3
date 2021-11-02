using System;
using System.Collections.Generic;
using Datastreaming;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public class HealthData : IData
    {
        public List<Data> CPU { get; private set; }
        public List<Data> Memory { get; private set; }

        private List<Data> _newCpu;
        private List<Data> _newMemory;

        public static DateTime LastRowTimeStamp { get; private set; }

        public HealthData()
        {
            CPU = new List<Data>();
            Memory = new List<Data>();
        }

        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            _newCpu = new List<Data>();
            _newMemory = new List<Data>();

            while (reader.Read())
            {
                string reportType = (string) reader[0];
                if (reportType.Equals("CPU"))
                {
                    _newCpu.Add(new Data(reader));
                }
                else
                {
                    _newMemory.Add(new Data(reader));
                }
                LastRowTimeStamp = (DateTime) reader[2];
            }
            CPU.AddRange(_newCpu);
            Memory.AddRange(_newMemory);
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