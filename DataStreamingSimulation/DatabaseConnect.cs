using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace DataStreamingSimulation
{
    public class DatabaseConnect
    {
        private readonly DatabaseRead _databaseRead = new DatabaseRead();
        
        public void SqlConnect(string queryString, string connectionString, bool print = false)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    if (print)
                    {
                        _databaseRead.SqlReader(command, new PrintData().ApplyData);
                    }
                    else
                    {
                        _databaseRead.SqlReader(command, new TransferData().ApplyData);
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                connection.Close();
            }
        }
        
        /// <remarks>
        ///   <para> Edit 'bin/Debug/net5.0/setup.txt' to change DBO. </para>
        ///   <para> Format example: 'Server=localhost\\SQLEXPRESS01;Database=ANS_CUSTOM_MVP;
        ///     User ID=sa; Password=Password123;Trusted_Connection=False' </para>
        /// </remarks>
        public string ReadSetupFile(string fileName, bool transferData = false)
        {
            string[] connectionString = new[] {"","" };
            
            using (StreamReader sr = new StreamReader(fileName))
            {
                int i = 0;
                while (!sr.EndOfStream) connectionString[i++] = sr.ReadLine();
            }
          
            if (transferData) return connectionString[1];
            return connectionString[0];
        }
        
        public void PrintConnection(SqlConnection connection)
        {
            Console.WriteLine("State: {0}", connection.State);
            Console.WriteLine("ConnectionString: {0}", connection.ConnectionString);
        }
    }
}