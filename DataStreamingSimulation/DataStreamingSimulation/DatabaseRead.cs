using System;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;

namespace DataStreamingSimulation
{
    public class DatabaseRead
    {
        
        protected void SqlReader(SqlCommand command, Action<SqlDataReader> readerHandler)
        {
            using(SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
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
        
        // Print streaming data
        protected void PrintReader(SqlDataReader reader)
        {
            string printString = String.Empty;
            DataTable schemaTable = reader.GetSchemaTable();
            DataRow row = schemaTable.Rows[0];
            
            Console.WriteLine("---- FROM TABLE: {0} ---- ", row["BaseTableName"]);
            
            for (int i = 0; reader.FieldCount > i ; i++)
            {
                if (reader.IsDBNull(i))
                {
                    printString += $"{reader.GetName(i)}: NULL\n";
                }
                else if (reader[i].GetType() == typeof(Byte[]))
                {
                    string byteString = reader.GetString(0).ToString();
                    printString += $"{reader.GetName(i)}: {byteString}\n";
                }
                else
                {
                    printString += $"{reader.GetName(i)}: {reader[i]}\n";
                }
            }
            
            Console.WriteLine(printString);
        }

        protected void TransferData(SqlDataReader reader)
        {
            DatabaseConnect sqlConnection = new DatabaseConnect();
            
            using (SqlConnection connection = new SqlConnection(sqlConnection.ReadSetupFile(true)))
            {
                //connection.Open();
                
                string printString = String.Empty;
                DataTable schemaTable = reader.GetSchemaTable();
                DataRow row = schemaTable.Rows[0];

                string queryString = String.Empty;
                string valuesQueryTail = String.Empty;
                
                string openingQuery = $"INSERT INTO {row["BaseTableName"]} ";
                string valuesQueryHead = "VALUES (";
                string closingQuery = ");";


                Regex rgx = new Regex(@"(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2}).(\d{3})");
                
                int lastValue = reader.FieldCount - 1;
                
                for (int i = 0; reader.FieldCount > i ; i++)
                {
                    valuesQueryTail += (i == lastValue ? @$"@param{i}" : $"@param{i}, ");
                }

                queryString = openingQuery + valuesQueryHead + valuesQueryTail + closingQuery;

                Console.WriteLine("---- queryString: ---- ");
                Console.WriteLine(queryString);
                
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    for (int i = 0; reader.FieldCount > i; i++)
                    {
                        if (reader[i] is DBNull)
                        {
                            command.Parameters.Add($"@param{i}",SqlDbType.VarBinary, -1).Value = reader[i];
                        }
                        else
                        {
                            command.Parameters.AddWithValue($"@param{i}", reader[i]);
                        }
                    }
                    
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}