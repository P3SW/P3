using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ExecuteSQLScript
{
    public static class SQLScriptExecuter
    {
        //Executes a SQL script with the given path on the database
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
        
        public static void CreateDB(string path)
        {
            FileInfo fi = new FileInfo(path);
            Console.WriteLine(fi.FullName);
            
            try
            {
                string script = File.ReadAllText(fi.FullName);

                IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$",
                    RegexOptions.Multiline | RegexOptions.IgnoreCase);


                using (SqlConnection connection = new SqlConnection("Server=localhost,1433;Trust Server Certificate = true;User Id=sa;Password=Password123;Trusted_Connection=false;"))
                {
                    connection.Open();
                    foreach (string commandString in commandStrings)
                    {
                        if (commandString.Trim() != "")
                        {
                            using (SqlCommand command = new SqlCommand(commandString, connection))
                            {
                                try
                                {
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    throw;
                                }
                            }
                        }
                    }

                    connection.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}