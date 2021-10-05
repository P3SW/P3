using System;
using Microsoft.Data.SqlClient;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-91DJ17V\\SQLEXPRESS01;Database=ANS_CUSTOM_CI; User ID=aes; Password=aes;Trusted_Connection=False";
            try
            {
                string queryString = "SELECT * FROM dbo.AFSTEMNING";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    Console.WriteLine("State: {0}", connection.State);
                    Console.WriteLine("ConnectionString: {0}",
                        connection.ConnectionString);
                    
                    SqlCommand command = new SqlCommand(
                        queryString, connection);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}, {1}",
                                reader[0], reader[1]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
