using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public class HealthData : IData
    {
        public List<Data> Cpu { get; private set; } 
        public List<Data> Memory { get; private set; }
        public List<Data> NewCpu { get; private set; }
        public List<Data> NewMemory { get; private set; }
        public static DateTime LastRowTimeStamp { get; private set; }

        public HealthData()
        {
            Cpu = new List<Data>();
            Memory = new List<Data>();  
        }
        
        

        //Inserts data from the reader into temporary lists and adds these to the full list of data.
        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            NewCpu = new List<Data>();
            NewMemory = new List<Data>();

            while (reader.Read())
            {
                string reportType = (string) reader[0];
                if (reportType.Equals("CPU"))
                {
                    NewCpu.Add(new Data(reader));
                }
                else
                {
                    NewMemory.Add(new Data(reader));
                }
                LastRowTimeStamp = (DateTime) reader[2];
            }
            Cpu.AddRange(NewCpu);
            Memory.AddRange(NewMemory);
        }
        
        //Returns a query string with the latest timestamp to ensure only new data is queried.
        public string GetChangesQueryString()
        {
            return string.Format($"SELECT REPORT_TYPE, REPORT_NUMERIC_VALUE, LOG_TIME FROM dbo.HEALTH_REPORT " +
                                 $"WHERE REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY'" +
                                 $"AND LOG_TIME > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                 "ORDER BY LOG_TIME");
        }
    }
}