using System;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public class ManagerTrackingData : IData
    {
        private string _manager;
        private string _status;
        private int _runtime;
        private int _rowsRead;
        private int _rowsWritten;
        private DateTime _startTime;
        private DateTime _endTime;

        public ManagerTrackingData()
        {
            
        }

        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public string GetChangesQueryString()
        {
            throw new NotImplementedException();
        }
    }
}