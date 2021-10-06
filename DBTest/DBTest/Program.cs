using System;
using Microsoft.Data.SqlClient;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=ANS_CUSTOM_CI; User ID=aes; Password=Password123;Trusted_Connection=False";
            string queryString = "SELECT * FROM dbo.AFSTEMNING";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    Console.WriteLine("State: {0}", connection.State);
                    Console.WriteLine("ConnectionString: {0}", connection.ConnectionString);
                    
                    SqlCommand command = new SqlCommand(queryString, connection);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}, {1}, {2}", reader[1], reader[3], reader[6]);
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
