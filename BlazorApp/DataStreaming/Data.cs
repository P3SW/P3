using System;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    //Class containing the CPU and memory data read from the HEALTH_REPORT. Class contains properties to store this data and constructors.
    public class Data
    {
        public string ReportType { get; set; }
        public int NumericValue { get; set; }
        public DateTime LogTime { get; set; }

        public Data(string reportType, int numericValue, DateTime logTime)
        {
            ReportType = reportType;
            NumericValue = numericValue;
            LogTime = logTime;
        }

        public Data(SqlDataReader reader)
        {
            ReportType = (string) reader[0];
            NumericValue = (int) reader[1];
            LogTime = (DateTime) reader[2];
        }
        
    }
}