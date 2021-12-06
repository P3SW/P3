using System;
using System.IO;
using Microsoft.Data.SqlClient;

namespace ExecuteSQLScript
{
    public static class SQLScriptExecuter
    {
        public static void Execute(string path)
        {
            FileInfo fi = new FileInfo(path);

            SqlConnection tmpConn;
            string script = File.ReadAllText(fi.FullName);
            tmpConn = new SqlConnection();
            tmpConn.ConnectionString = "Server=localhost,1433;Trust Server Certificate = true;User Id=sa;Password=Password123;Trusted_Connection=false;";

            SqlCommand myCommand = new SqlCommand(script, tmpConn);
            try
            {
                tmpConn.Open();
                int result = myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}