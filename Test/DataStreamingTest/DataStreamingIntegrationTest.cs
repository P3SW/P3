using System;
using System.Collections.Generic;
using System.Linq;
using BlazorApp.Data;
using DataStreamingSimulation;
using Microsoft.Data.SqlClient;
using Xunit;
using Xunit.Abstractions;

namespace P3ConversionDashboard.Tests
{
    [Collection("Sequential")]
    public class DataStreamingIntegrationTest
    {
        private DatabaseStreamer testDatabaseStreamer;

        private Dictionary<string, int> numberOfRows = new Dictionary<string, int>()
        {
            {"LOGGING", 2213}, {"MANAGERS", 118}, {"LOGGING_CONTEXT", 124}, 
            {"MANAGER_TRACKING", 32}, {"HEALTH_REPORT", 282}, {"ENGINE_PROPERTIES", 957}, 
            {"AFSTEMNING", 159}
        };

        private readonly ITestOutputHelper _testOutputHelper;

        public DataStreamingIntegrationTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            testDatabaseStreamer = new DatabaseStreamer("../../../DataStreamingTest/setupDataStreaming.txt", 
                "2021-10-28 15:07:10.347", "2021-10-28 16:58:52.720");
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
            
            TestStream(table);
        }

        
        
        [Theory]
        [InlineData("MANAGERS")]
        [InlineData("LOGGING_CONTEXT")]
        public void TestStreamWithoutTimeStamp(string table)
        {
            testDatabaseStreamer.StreamTableOneTime(table);
            
            TestStream(table);
        }

        public void TestStream(string table)
        {
            int rows = QueryTestData(testDatabaseStreamer._queryString, "../../../DataStreamingTest/setupQueryTestData.txt");
            
            System.Threading.Thread.Sleep(1000);

            Assert.Equal(numberOfRows[table], rows);
        }
        
        private int QueryTestData(string queryString, string setupFile)
        {
            string connectionString = ConfigReader.ReadSetupFile(setupFile);
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            int rows = 0;

            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows++;
                    }
                    reader.Close();
                }
            }

            return rows;
        }
    }
}