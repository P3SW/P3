using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class DBStreamer
    {
        private SqlDependency _dependency;

        public string ConnectionString { get; private set; }
        private string _queryString;
        public SqlConnection Connection { get; private set; }

        public DBStreamer(SqlConnection connection, string connectionString)
        {
            Connection = connection;
            ConnectionString = connectionString;
            //_queryString = "SELECT name, age FROM dbo.people";
            _queryString = "SELECT REPORT_TYPE, REPORT_KEY, REPORT_NUMERIC_VALUE, REPORT_VALUE_TYPE, REPORT_VALUE_HUMAN, LOG_TIME FROM dbo.HEALTH_REPORT WHERE MONITOR_NO = 8 /*AND LOG_TIME >= '2021-10-20 09:30:10.770'*/";
            SqlDependency.Stop(ConnectionString);
            SqlDependency.Start(ConnectionString);
        }

        public void StartListening()
        {   
            try
            {
                using (SqlCommand command = new SqlCommand(_queryString, Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = _queryString;
                
                    _dependency = new SqlDependency(command);
                    _dependency.OnChange += SqlDependencyChange;
                
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PrintReader(reader);
                        }
                    }
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
                PrintChanges();
            }
            StartListening();
        }
        public void PrintChanges()
        {
            SqlCommand command = new SqlCommand(_queryString, Connection);

            using(SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    PrintReader(reader);
                }
            }
        }
        public void PrintReader(SqlDataReader reader)
        {
            for (int i = 0; i < reader.VisibleFieldCount; i++)
            {
                Console.Write("{0} ", reader[i]);
            }
            Console.Write("\n");
            //Console.WriteLine("{0},{1}", reader[0], reader[1]);
        }
    }
}