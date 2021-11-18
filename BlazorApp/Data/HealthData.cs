using System;
using Microsoft.Data.SqlClient;

namespace BlazorApp.Data
{
    //Class containing the CPU and memory data read from the HEALTH_REPORT. Class contains properties to store this data and constructors.
    public class HealthData
    {
        public string ReportType { get; set; }
        public long NumericValue { get; set; }
        public DateTime LogTime { get; set; }

        public HealthData(SqlDataReader reader)
        {
            ReportType = (string) reader[0];
            NumericValue = (long) reader[1];
            LogTime = (DateTime) reader[2];
        }
    }
}