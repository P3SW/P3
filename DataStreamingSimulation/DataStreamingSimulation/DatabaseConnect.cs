using System;
using System.IO;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;

namespace DataStreamingSimulation
{
    public class DatabaseConnect
    {

        public void SqlConnect(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                PrintConnection(connection);

                SqlCommand command = new SqlCommand(queryString, connection);

                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PrintReader(reader);
                    }
                }
            }
        }
        
        private static void PrintConnection(SqlConnection connection)
        {
            Console.WriteLine("State: {0}", connection.State);
            Console.WriteLine("ConnectionString: {0}",
                connection.ConnectionString);
        }
        
        /* Print matching queryString columns */
        private static void PrintReader(SqlDataReader reader)
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

        // "Server=localhost\\SQLEXPRESS01;Database=ANS_CUSTOM_MVP; User ID=sa; Password=Password123;Trusted_Connection=False";
        public string ReadSetupFile()
        {
            const string fileName = "setup.txt";
            string connectionString;
            using (StreamReader sr = new StreamReader(fileName))
            {
                connectionString = sr.ReadLine();
            }

            Console.WriteLine(connectionString);
            return connectionString;
        }
    }
}