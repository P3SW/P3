using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class DbStreamer
    {
        private SqlDependency _dependency;

        private string _connectionString;
        private string _queryString;
        private SqlConnection _connection;
        private StreamPrinter _printer;
        public DbStreamer(SqlConnection connection, string connectionString)
        {
            _connection = connection;
            _connectionString = connectionString;
            //_queryString = "SELECT name, age FROM dbo.people";
            _queryString = "SELECT REPORT_TYPE, REPORT_KEY, REPORT_NUMERIC_VALUE, REPORT_VALUE_TYPE, REPORT_VALUE_HUMAN, LOG_TIME FROM dbo.HEALTH_REPORT WHERE MONITOR_NO = 8";
            _printer = new StreamPrinter(_connection, _queryString);
            SqlDependency.Stop(_connectionString);
            SqlDependency.Start(_connectionString);
        }

        public void StartListening()
        {   
            try
            {
                using (SqlCommand command = new SqlCommand(_queryString, _connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = _queryString;
                
                    _dependency = new SqlDependency(command);
                    _dependency.OnChange += SqlDependencyChange;
                    
                    _printer.PrintReader(command);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void SqlDependencyChange(object sender, SqlNotificationEventArgs eventArgs)
        {
            if (eventArgs.Info == SqlNotificationInfo.Invalid)
            {
                Console.WriteLine("Info: {0}, Source: {1}, Type: {2}", eventArgs.Info, eventArgs.Source, eventArgs.Type);
            }
            else
            {
                //Console.WriteLine("Info: {0}, Source: {1}, Type: {2}", eventArgs.Info, eventArgs.Source, eventArgs.Type);
                _printer.PrintChanges();
            }
            StartListening();
        }
    }
}