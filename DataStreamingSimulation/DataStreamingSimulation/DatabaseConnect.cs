using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace DataStreamingSimulation
{
    public class DatabaseConnect : DatabaseReader
    {
        
        public void SqlConnect(string queryString, string connectionString, bool print = false)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                PrintConnection(connection);
                
                // SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM AFSTEMNING",connection);
                //
                // Int32 count = (Int32)cmd.ExecuteScalar();
                // Console.WriteLine("--------------------" + count);
                
                SqlCommand command = new SqlCommand(queryString, connection);
                
                if (print) SqlReader(command, PrintReader);
                else Console.WriteLine("FALSE");
                
                // using(SqlDataReader reader = command.ExecuteReader())
                // {
                //     while (reader.Read())
                //     {
                //         PrintReader(reader);
                //     }
                // }
            }
        }

        // public void SqlReader(SqlCommand command, Action<SqlDataReader> readerHandler)
        // {
        //     using(SqlDataReader reader = command.ExecuteReader())
        //     {
        //         while (reader.Read())
        //         {
        //             readerHandler(reader);
        //             //PrintReader(reader);
        //         }
        //     }
        // }

        public Int32 GetMaxRowsInDB(string[] tables, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                List<int> tableRowNum = new List<int>();

                foreach (var table in tables)
                {
                    SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", connection);
                    Int32 count = (Int32)cmd.ExecuteScalar();
                    tableRowNum.Add(count);
                }
                
                connection.Close();
                return tableRowNum.Max();
            }
        }
        
        
        
        // private static void PrintConnection(SqlConnection connection)
        // {
        //     Console.WriteLine("State: {0}", connection.State);
        //     Console.WriteLine("ConnectionString: {0}",
        //         connection.ConnectionString);
        // }
        //
        // /* Print matching queryString columns */
        // private static void PrintReader(SqlDataReader reader)
        // {
        //     string printString = String.Empty;
        //
        //     for (int i = 0; reader.FieldCount > i ; i++)
        //     {
        //         if (reader.IsDBNull(i))
        //         {
        //             printString += "NULL,";
        //         } else
        //             printString += $"{reader[i]},";
        //     }
        //     
        //     Console.WriteLine(printString);
        // }

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