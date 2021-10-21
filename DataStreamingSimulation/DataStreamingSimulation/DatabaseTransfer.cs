using System;
using Microsoft.Data.SqlClient;

namespace DataStreamingSimulation
{
    public class DatabaseTransfer : DatabaseConnect
    {
        
        public void TransferToDatabase(string queryString)
        {
            DatabaseConnect sqlConnection = new DatabaseConnect();
            sqlConnection.SqlConnect(queryString, sqlConnection.ReadSetupFile(true));
            Console.WriteLine("SENDING TO DB..");
        }
    }
}