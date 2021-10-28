using System;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class StreamPrinter
    {
        private SqlConnection _connection;
        public StreamPrinter(SqlConnection connection)
        {
            _connection = connection;
        }
        public void PrintChanges(string queryString)
        {
            using (SqlCommand command = new SqlCommand(queryString, _connection))
            {
                PrintReader(command);
            }
        }
        public void PrintReader(SqlCommand command)
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.VisibleFieldCount; i++)
                    {
                        Console.Write("{0} ", reader[i]);
                    }
                    Console.Write("\n");
                }
            }
        }
    }
}