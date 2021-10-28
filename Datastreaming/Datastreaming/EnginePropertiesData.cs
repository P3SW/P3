using System;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class EnginePropertiesData : IData
    {
        public string Manager { get; private set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }
        public int RunNo { get; set; }
        public static DateTime LastRowTimeStamp { get; private set; }

        public EnginePropertiesData(string manager, string key, string value, DateTime timestamp, int runNo)
        {
            Manager = manager;
            Key = key;
            Value = value;
            Timestamp = timestamp;
            RunNo = runNo;
            LastRowTimeStamp = timestamp;
        }

        public EnginePropertiesData()
        {
            
        }

        public void ConstructFromSqlReader(SqlDataReader reader)
        {
            Manager = (string) reader[0];
            Key = (string) reader[1];
            Value = (string) reader[2];
            Timestamp = (DateTime) reader[3];
            RunNo = (int) reader[4];
            LastRowTimeStamp = (DateTime) reader[3];
        }

        public string GetChangesQueryString()
        {
            return string.Format("SELECT MANAGER, KEY, VALUE, TIMESTAMP, RUN_NO FROM dbo.logging " +
                                 $"WHERE TIMESTAMP > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                 "ORDER BY TIMESTAMP");
        }
    }
}