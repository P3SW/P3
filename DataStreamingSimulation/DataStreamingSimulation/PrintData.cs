using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataStreamingSimulation
{
    public class PrintData : IHandleData
    {
        public void ApplyData(SqlDataReader reader)
        {
            DataTable schemaTable = reader.GetSchemaTable();
            DataRow row = schemaTable.Rows[0];
            string printString = String.Empty;
            
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
    }
}