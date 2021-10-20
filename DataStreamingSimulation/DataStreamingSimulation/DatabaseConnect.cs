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
                
                SqlCommand command = new SqlCommand(queryString, connection);
                
                if (print) SqlReader(command, PrintReader);
                else Console.WriteLine("SEND TO FAKE STATE DATABASE!");
            }
        }
        
        /* Edit 'bin/Debug/net5.0/setup.txt' to change DBO */
        /* Format: 'Server=localhost\\SQLEXPRESS01;Database=ANS_CUSTOM_MVP; User ID=sa; Password=Password123;Trusted_Connection=False' */
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
    }
}