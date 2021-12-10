using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataStreamingSimulation
{
    public class TransferData : IHandleData
    {
        public void ApplyData(SqlDataReader reader)
        {
            DatabaseConnect sqlConnection = new DatabaseConnect();
            
            using (SqlConnection connection = new SqlConnection(sqlConnection.ReadSetupFile(DatabaseStreamer.FilePath,true)))
            {
                DataTable schemaTable = reader.GetSchemaTable();
                DataRow row = schemaTable.Rows[0];
                string paramString = String.Empty;
                int lastValue = reader.FieldCount - 1;
                
                for (int i = 0; reader.FieldCount > i ; i++) 
                {
                    paramString += (i == lastValue ? @$"@param{i}" : $"@param{i}, ");
                }

                string queryString = $"INSERT INTO {row["BaseTableName"]} VALUES (" + paramString + ");";
                
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
