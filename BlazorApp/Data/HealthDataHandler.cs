using System;
using System.Collections.Generic;
using BlazorApp.DataStreaming.Events;
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
        public List<int> MemoryPercent { get; private set; }
        public static DateTime LastRowTimeStamp { get; private set; }
        
        public long MaxMemory = 21473734656; /* Approx 20gb */

        public HealthDataHandler(DateTime managerStartTime)
        {
            Cpu = new List<HealthData>();
            Memory = new List<HealthData>();
            MemoryPercent = new List<int>();
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
                else
                {
                    NewMemory.Add(new HealthData(reader));
                    
                    //Used for calculating available memory in bytes to used memory in percent of total memory
                    MemoryPercent.Add(Convert.ToInt32(((MaxMemory/1000000) - ((MaxMemory/1000000) -  ((long) (reader["REPORT_NUMERIC_VALUE"]) / 1000000)) / (MaxMemory/1000000)) * 100));

                }

                LastRowTimeStamp = (DateTime)reader[2];
            }
            PrintCPUAndMemory(NewCpu, NewMemory);
            Cpu.AddRange(NewCpu);
            Memory.AddRange(NewMemory);
        }

        //Returns a query string with the latest timestamp to ensure only new data is queried.
        public string GetNewestDataQueryString(string type)
        {
            return string.Format($"SELECT REPORT_TYPE, REPORT_NUMERIC_VALUE, LOG_TIME FROM dbo.HEALTH_REPORT " +
                                 "WHERE (REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY')" +
                                 $"AND LOG_TIME > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                 "ORDER BY LOG_TIME");
        }

        public void PrintCPUAndMemory(List<HealthData> cpu, List<HealthData> memory)
        {
            foreach (var data in cpu)
            {
                Console.WriteLine(data.LogTime + " " + data.ReportType + " " + data.NumericValue);
            }
            
            foreach (var data in memory)
            {
                Console.WriteLine(data.LogTime + " " + data.ReportType + " " + data.NumericValue);
            }
        }
    }
}