using System;
using System.Collections.Generic;
using BlazorApp.Data.Events;
using Microsoft.Data.SqlClient;
using SQLDatabaseRead;

namespace BlazorApp.Data
{
    public class HealthDataHandler : EventBase, IDataHandler
    {
        public List<HealthData> Cpu { get; private set; }
        public List<HealthData> Memory { get; private set; }
        public List<HealthData> NewCpu { get; private set; }
        public List<HealthData> NewMemory { get; private set; }
        public static DateTime LastRowTimeStamp { get; private set; }
        

        public HealthDataHandler(DateTime managerStartTime)
        {
            Cpu = new List<HealthData>();
            Memory = new List<HealthData>();
            LastRowTimeStamp = managerStartTime;
        }

        //Inserts data from the reader into temporary lists and adds these to the full list of data.
        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            NewCpu = new List<HealthData>();
            NewMemory = new List<HealthData>();

            while (reader.Read())
            {
                string reportType = (string)reader[0];
                if (reportType.Equals("CPU"))
                {
                    NewCpu.Add(new HealthData(reader));
                }
                else if (reportType.Equals("MEMORY"))
                {
                    NewMemory.Add(new HealthData(reader));
                }

                LastRowTimeStamp = (DateTime)reader[2];
            }
            Cpu.AddRange(NewCpu);
            Memory.AddRange(NewMemory);
            
            HealthTriggerUpdate(Cpu, Memory);
        }

        //Returns a query string with the latest timestamp to ensure only new data is queried.
        public string GetNewestDataQueryString()
        {
            return string.Format($"SELECT REPORT_TYPE, REPORT_NUMERIC_VALUE, LOG_TIME FROM dbo.HEALTH_REPORT " +
                                 "WHERE (REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY')" +
                                 $"AND LOG_TIME > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                 "ORDER BY LOG_TIME");
        }
        
        //Method used to create new healthdata objects from values.
        public void AddHealthData(string reportType, long numericValue, DateTime logtime)
        {
            if (reportType == "CPU")
            {
                Cpu.Add(new HealthData(reportType, numericValue, logtime));
            }
            else
            {
                Memory.Add(new HealthData(reportType, numericValue, logtime));
            }
        }
    }
}