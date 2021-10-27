using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;

namespace DataStreamingSimulation
{
    public class DatabaseRead
    {
        public void SqlReader(SqlCommand command, Action<SqlDataReader> readerHandler)
        {
            using(SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
            {
                while (reader.Read())
                {
                    readerHandler(reader);
                }
            }
        }
        
        public Int32 GetMaxRowsInDb(string[] tables, string connectionString)
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