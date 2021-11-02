using System;
using BlazorApp.DataStreaming;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class EnginePropertiesData : IData
    {
        public string Manager { get; private set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }
        public static DateTime LastRowTimeStamp { get; private set; }

        public EnginePropertiesData(string manager, string key, string value, DateTime timestamp)
        {
            Manager = manager;
            Key = key;
            Value = value;
            Timestamp = timestamp;
            LastRowTimeStamp = timestamp;
        }

        public EnginePropertiesData(SqlDataReader reader)
        {
            Manager = (string) reader[0];
            Key = (string) reader[1];
            Value = (string) reader[2];
            Timestamp = (DateTime) reader[3];
            LastRowTimeStamp = (DateTime) reader[3];
        }

        
        //TODO: Move this shit to a new class to follow the pattern of all the other classes.
        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            
        }

        public string GetChangesQueryString()
        {
            return string.Format("SELECT MANAGER, KEY, VALUE, TIMESTAMP FROM dbo.logging " +
                                 $"WHERE TIMESTAMP > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                 "ORDER BY TIMESTAMP");
        }
    }
}