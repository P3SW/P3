using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class TableStreamer
    {
        private SqlDependency _dependency;

        private string _connectionString;
        private string _queryString;
        private SqlConnection _connection;
        private StreamPrinter _printer;
        private List<HealthData> dataList;
        public TableStreamer(SqlConnection connection, string connectionString, string queryString)
        {
            _connection = connection;
            _connectionString = connectionString;
            _queryString = queryString;
            _printer = new StreamPrinter(_connection, _queryString);
            
            dataList = new List<HealthData>();
            
            
            using (SqlCommand command = new SqlCommand(_queryString, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new HealthData((string) reader[0], (string) reader[1], (long) reader[2], (DateTime) reader[5]));
                        for (int i = 0; i < reader.VisibleFieldCount; i++)
                        {
                            Console.Write("{0} ", reader[i]);
                        }
                        Console.Write("\n");
                    }
                }
            }
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
                    
                    CloseReader(command);
                    
                    //_printer.PrintReader(command);
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
                _printer.PrintChanges(HealthData.GetChangesQueryString());
            }
            StartListening();
        }

        private static void CloseReader(SqlCommand command)
        {
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }
    }
}