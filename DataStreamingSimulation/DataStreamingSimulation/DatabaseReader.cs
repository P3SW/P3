using System;
using Microsoft.Data.SqlClient;

namespace DataStreamingSimulation
{
    public class DatabaseReader
    {
        
        public void SqlReader(SqlCommand command, Action<SqlDataReader> readerHandler)
        {
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    readerHandler(reader);
                }
            }
        }
        
        public void PrintConnection(SqlConnection connection)
        {
            Console.WriteLine("State: {0}", connection.State);
            Console.WriteLine("ConnectionString: {0}",
                connection.ConnectionString);
        }
        
        /* Print matching queryString columns */
        public void PrintReader(SqlDataReader reader)
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