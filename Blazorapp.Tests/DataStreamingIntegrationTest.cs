using System;
using System.Collections.Generic;
using BlazorApp.Data;
using DataStreamingSimulation;
using Microsoft.Data.SqlClient;
using Xunit;
using Xunit.Abstractions;

namespace P3ConversionDashboard.Tests
{
    public class DataStreamingIntegrationTest
    {
        private DatabaseStreamer testDatabaseStreamer;

        private readonly ITestOutputHelper _testOutputHelper;

        public DataStreamingIntegrationTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            testDatabaseStreamer = new DatabaseStreamer("../../../integrationTestDataStreamingSetup.txt");
        }

        [Theory] 
        [InlineData("AFSTEMNING", "2021-10-28 15:07:56.987" , "2021-10-28 15:28:02.323")]
        [InlineData("LOGGING", "2021-10-28 15:07:56.987" , "2021-10-28 15:28:02.323")]
        [InlineData("MANAGER_TRACKING", "2021-10-28 15:07:56.987" , "2021-10-28 15:28:02.323")]
        [InlineData("HEALTH_REPORT", "2021-10-28 15:07:56.987" , "2021-10-28 15:28:02.323")]
        [InlineData("ENGINE_PROPERTIES", "2021-10-28 15:07:56.987" , "2021-10-28 15:28:02.323")]
        public void TestStreamWithoutTimeStamp(string table, string startTime, string nextTime)
        {
            testDatabaseStreamer.StreamTable(table,Convert.ToDateTime(startTime),Convert.ToDateTime(nextTime));
            
            TestStream();
        }

        
        
        [Theory]
        [InlineData("MANAGERS")]
        [InlineData("LOGGING_CONTEXT")]
        public void TestStreamWithoutTimeStamp(string table)
        {
            testDatabaseStreamer.StreamTableOneTime(table);
            
            TestStream();
        }

        public void TestStream()
        {
           
            List<List<string>> dataFromTestDatabase = new();
            List<List<string>> dataFromNetcompanyDatabase = new();

            QueryTestData(testDatabaseStreamer._queryString, dataFromNetcompanyDatabase);
            QueryTestData(testDatabaseStreamer._queryString, dataFromTestDatabase);

            Assert.Equal(dataFromNetcompanyDatabase.Count, dataFromTestDatabase.Count);
            
            for (int i = 0; i < dataFromNetcompanyDatabase.Count; i++)
            {
                Assert.Equal(dataFromNetcompanyDatabase[i].Count, dataFromTestDatabase[i].Count);
                
                for (int j = 0; j < dataFromNetcompanyDatabase[i].Count; j++)
                    Assert.Equal(dataFromNetcompanyDatabase[i][j], dataFromTestDatabase[i][j]);
                
            } 
        }
        
        private void QueryTestData(string queryString, List<List<string>> storage)
        {
            string connectionString = ConfigReader.ReadSetupFile("../../../blazorTestSetup.txt");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        List<string> testData = new();
                        for (int i = 0; i < reader.FieldCount; i++)
                            testData.Add(Convert.ToString(reader[i]));
                        
                        storage.Add(testData);
                    }
                    reader.Close();
                }
            }
        }
    }
}