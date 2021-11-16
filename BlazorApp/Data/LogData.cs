using System;
using Microsoft.Data.SqlClient;

namespace BlazorApp.Data
{
    //Class contains log data from the database. Has constructor with SqlDataReader.
    public class LogData
    {
        public DateTime Timestamp { get; private set; }
        public string Description { get; private set; }
        public string ManagerName { get; private set; }
        public string Grade { get; private set; }

        public LogData(DateTime timestamp, string description, string managerName, string grade)
        {
            Timestamp = timestamp;
            Description = description;
            ManagerName = managerName;
            Grade = grade;
        }

        public LogData(SqlDataReader reader)
        {
            Timestamp = (DateTime) reader[0];
            Description = (string) reader[1];
            ManagerName = (string) reader[2];
            Grade = (string) reader[3];
        }
    }
}