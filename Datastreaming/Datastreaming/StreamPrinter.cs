using System;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class StreamPrinter
    {
        private string _queryString;
        private SqlConnection _connection;
        public StreamPrinter(SqlConnection connection, string queryString)
        {
            _connection = connection;
            _queryString = queryString;
        }
        public void PrintChanges()
        {
            //string queryString = "SELECT REPORT_TYPE, REPORT_KEY, REPORT_NUMERIC_VALUE, REPORT_VALUE_TYPE, REPORT_VALUE_HUMAN FROM HEALTH_REPORT WHERE MONITOR_NO = 8/*REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY' OR REPORT_TYPE = 'NETWORK'*/";
            SqlCommand command = new SqlCommand(_queryString, _connection);
            PrintReader(command);
        }
        public void PrintReader(SqlCommand command)
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    foreach (var value in reader){}
                    {
                        Console.Write("{0}, ", reader);
                    }
                }
            }
            
            //Console.WriteLine("{0},{1}", reader[0], reader[1]);
        }
    }
}