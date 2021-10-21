using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace DataStreamingSimulation
{
    public class DatabaseRead
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

        protected void TransferData(SqlDataReader reader)
        {
            DatabaseConnect sqlConnection = new DatabaseConnect();
            
            using (SqlConnection connection = new SqlConnection(sqlConnection.ReadSetupFile(true)))
            {
                connection.Open();
                PrintConnection(connection);

                string queryString = "Bla";
                
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    //command.Parameters.Add()
                }
                
            }
            
            
            Console.WriteLine(reader[0]);
            Console.WriteLine("Sent to DBO");
        }
        
    }
}