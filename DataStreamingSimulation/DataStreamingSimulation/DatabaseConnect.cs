using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace DataStreamingSimulation
{
    public class DatabaseConnect : DatabaseRead
    {
        
        public void SqlConnect(string queryString, string connectionString, bool print = false)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //PrintConnection(connection);
                
                SqlCommand command = new SqlCommand(queryString, connection);

                if (print)
                {
                    SqlReader(command, PrintReader);
                    connection.Close(); //BUG?
                }
                else
                {
                    SqlReader(command, TransferData);
                    connection.Close(); //BUG?
                    // Console.WriteLine("SEND TO FAKE STATE DATABASE!");
                    // DatabaseTransfer transfer = new DatabaseTransfer();
                    // transfer.TransferToDatabase(queryString);
                }
                
            }
        }
        
        // Edit 'bin/Debug/net5.0/setup.txt' to change DBO */
        // Format: 'Server=localhost\\SQLEXPRESS01;Database=ANS_CUSTOM_MVP; User ID=sa; Password=Password123;Trusted_Connection=False'
        public string ReadSetupFile(bool transferData = false)
        {
            const string fileName = "setup.txt";
            string[] connectionString = new[] {"","" };
            
            using (StreamReader sr = new StreamReader(fileName))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    connectionString[i++] = sr.ReadLine();
                }
            }

            Console.WriteLine("Read: " + connectionString[0]);
            Console.WriteLine("Write: " + connectionString[1]);
            
            if (transferData) return connectionString[1];
            else return connectionString[0];
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