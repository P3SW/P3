using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ManagerTrackingFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> managerNames = new List<string>();

            SqlConnection conn = new SqlConnection("Server=localhost,1433;Database=ANS_CUSTOM_2;Trust Server Certificate = true; User Id=sa;Password=Password123;Trusted_Connection=False");
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT [MGR] FROM [dbo].[MANAGER_TRACKING]", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine(reader.RecordsAffected);
                        while (reader.Read())
                        {
                            managerNames.Add((string) reader[0]);
                        }
                    }
                }

                foreach (string manager in managerNames)
                {
                    Console.WriteLine($"Fixing {manager}");
                    
                    string updateString;
                    if (String.Compare(manager, 48, "AnsConversionUserManager", 0, 24) == 0)
                    {
                        Console.WriteLine("UserManager");
                        updateString = $"UPDATE [dbo].[MANAGER_TRACKING] SET STARTTIME = (SELECT TOP(1) TIMESTAMP FROM [dbo].[ENGINE_PROPERTIES] WHERE [MANAGER] = '{manager}' AND [KEY] = 'START_TIME'), ENDTIME = (SELECT TIMESTAMP FROM [dbo].[ENGINE_PROPERTIES] WHERE [MANAGER] = '{manager}' AND [KEY] = 'WRITE [TOTAL]') WHERE [MGR] = '{manager}'";
                    }
                    else if (String.Compare(manager, 38, "ANSInitManager", 0, 14) == 0)
                    {
                        Console.WriteLine("InitManager");
                        updateString = $"UPDATE [dbo].[MANAGER_TRACKING] SET STARTTIME = (SELECT TOP(1) TIMESTAMP FROM [dbo].[ENGINE_PROPERTIES] WHERE [MANAGER] = '{manager}' AND [KEY] = 'START_TIME'), ENDTIME = (SELECT TIMESTAMP FROM [dbo].[ENGINE_PROPERTIES] WHERE [MANAGER] = '{manager}' AND [KEY] = 'WRITE [TOTAL]') WHERE [MGR] = '{manager}'";
                    }
                    else
                    {
                        updateString = $"UPDATE [dbo].[MANAGER_TRACKING] SET STARTTIME = (SELECT TOP(1) TIMESTAMP FROM [dbo].[ENGINE_PROPERTIES] WHERE [MANAGER] = '{manager}' AND [KEY] = 'START_TIME'), ENDTIME = (SELECT TOP(1) TIMESTAMP FROM [dbo].[ENGINE_PROPERTIES] WHERE [MANAGER] = '{manager}' AND [KEY] = 'runtimeOverall') WHERE [MGR] = '{manager}'";

                    }
                    using (SqlCommand command = new SqlCommand(updateString, conn))
                    {
                        int result = command.ExecuteNonQuery();
                        Console.WriteLine($"{manager} fixed with {result} rows changed!");
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