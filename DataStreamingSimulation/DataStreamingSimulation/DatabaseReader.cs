using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace DataStreamingSimulation
{
    public class DatabaseReader
    {
        
        protected void SqlReader(SqlCommand command, Action<SqlDataReader> readerHandler)
        {
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    readerHandler(reader);
                }
            }
        }
        
        protected void PrintConnection(SqlConnection connection)
        {
            Console.WriteLine("State: {0}", connection.State);
            Console.WriteLine("ConnectionString: {0}", connection.ConnectionString);
        }
        
        /* Print matching queryString columns */
        protected void PrintReader(SqlDataReader reader)
        {
            string printString = String.Empty;

            for (int i = 0; reader.FieldCount > i ; i++)
            {
                if (reader.IsDBNull(i))
                {
                    printString += "NULL,";
                } else
                    printString += $"{reader[i]},";
            }
            
            Console.WriteLine(printString);
        }
    }
}