using System;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public class Data
    {
        public string ReportType { get; set; }
        public long NumericValue { get; set; }
        public DateTime LogTime { get; set; }

        public Data(string reportType, long numericValue, DateTime logTime)
        {
            ReportType = reportType;
            NumericValue = numericValue;
            LogTime = logTime;
        }

        public Data(SqlDataReader reader)
        {
            ReportType = (string) reader[0];
            NumericValue = (long) reader[1];
            LogTime = (DateTime) reader[2];
        }
        
    }
}