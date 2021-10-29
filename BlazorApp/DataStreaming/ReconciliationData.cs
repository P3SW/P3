using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class ReconciliationData
    {
        public DateTime Timestamp { get; private set; }
        public string Description { get; private set; }
        public string Manager { get; private set; }
        public string Result { get; private set; }

        public ReconciliationData(DateTime timestamp, string description, string manager, string result)
        {
            Timestamp = timestamp;
            Description = description;
            Manager = manager;
            Result = result;
        }

        public ReconciliationData(SqlDataReader reader)
        {
            Timestamp = (DateTime) reader[0];
            Description = (string) reader[1];
            Manager = (string) reader[2];
            Result = (string) reader[3];
        }
    }
}