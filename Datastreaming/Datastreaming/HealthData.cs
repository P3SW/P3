using System;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class HealthData
    {
        public int ExecutionID { get; private set; }
        public string ReportType { get; private set; }
        public string ReportKey { get; private set; }
        public string ReportStringValue { get; private set; }
        public long ReportNumericValue { get; private set; }
        public string ReportValueType { get; private set; }
        public string ReportValueHuman { get; private set; }
        public DateTime LogTime { get; private set; }

        public static DateTime LastRowTimeStamp { get; private set; }

        public HealthData(string reportType, string reportKey, long reportNumericValue, DateTime logTime)
        {
            ReportType = reportType;
            ReportKey = reportKey;
            ReportNumericValue = reportNumericValue;
            LogTime = logTime;
            LastRowTimeStamp = logTime;
        }

        public HealthData(SqlDataReader r)
        {
            
        }

        public HealthData()
        {

        }

        public static string GetChangesQueryString()
        {
            return string.Format("SELECT REPORT_TYPE, REPORT_KEY, REPORT_NUMERIC_VALUE, REPORT_VALUE_TYPE, REPORT_VALUE_HUMAN, " +
                                 "LOG_TIME FROM dbo.HEALTH_REPORT WHERE MONITOR_NO = 8 " +
                                 $"AND LOG_TIME > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}'");
        }
    }
}