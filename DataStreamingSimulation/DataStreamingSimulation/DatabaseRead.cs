using System;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using System.Globalization;
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

                int lastValue = reader.FieldCount - 1;

                Regex rgx = new Regex(@"(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2}).(\d{3})");
                
                //TODO - System.Byte[]
                
                for (int i = 0; reader.FieldCount > i ; i++)
                {
                    Console.WriteLine(reader[i].GetType());
                    
                    if (reader.IsDBNull(i))
                    {
                        valuesQueryTail += (i == lastValue ? "NULL" : "NULL, ");
                    } 
                    else if (reader[i].GetType() == typeof(DateTime))
                    {
                        valuesQueryTail += (i == lastValue ? $"(CONVERT(datetime,'{reader[i]}'))" : $"(CONVERT(datetime,'{reader[i]}')), ");
                    }
                    else if (rgx.IsMatch($"({reader[i]})"))
                    {
                        valuesQueryTail += (i == lastValue ? $"('{reader[i]}')" : $"('{reader[i]}'), ");
                    }
                    else
                    { 
                        valuesQueryTail += (i == lastValue ? $"('{reader[i]}')" : $"('{reader[i]}'), ");
                    }
                    
                }
                
                queryString = openingQuery + valuesQueryHead + valuesQueryTail + closingQuery;

                Console.WriteLine("---- queryString: ---- ");
                Console.WriteLine(queryString);
                
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();

                    int numOfRowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(numOfRowsAffected);
                }
            }
        }
        
    }
}