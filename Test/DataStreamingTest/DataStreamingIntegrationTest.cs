using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Data;
using DataStreamingSimulation;
using ExecuteSQLScript;
using Microsoft.Data.SqlClient;
using Xunit;
using Xunit.Abstractions;

namespace P3ConversionDashboard.Tests.DataStreamingTest
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

        private List<List<string>> streamsWithTimestamp = new List<List<string>>()
        {
            new List<string>() {"AFSTEMNING", "2021-10-28 15:07:56.987", "2021-10-28 15:28:02.323"},
            new List<string>() {"LOGGING", "2021-10-28 15:07:56.987", "2021-10-28 15:28:02.323"},
            new List<string>() {"MANAGER_TRACKING", "2021-10-28 15:07:56.987", "2021-10-28 15:28:02.323"},
            new List<string>() {"HEALTH_REPORT", "2021-10-28 15:07:56.987", "2021-10-28 15:28:02.323"},
            new List<string>() {"ENGINE_PROPERTIES", "2021-10-28 15:07:56.987", "2021-10-28 15:28:02.323"}
        };

        private List<string> streamsWithoutTimestamp = new List<string>() { "MANAGERS", "LOGGING_CONTEXT" };

        private readonly ITestOutputHelper _testOutputHelper;

        public DataStreamingIntegrationTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async void DatabaseStreamTest()
        {
            await Task.Run(() => SQLScriptExecuter.CreateDB("../../../DataStreamingTest/DROP_ANS_DB_P3_TEST.sql"));
            await Task.Run(() => SQLScriptExecuter.CreateDB("../../../DataStreamingTest/NEW_CREATE_ANS_DB_P3_TEST.sql"));
            
            testDatabaseStreamer = new DatabaseStreamer("../../../DataStreamingTest/DataStreamingSetup.txt", 
                "2021-10-28 15:07:10.347", "2021-10-28 16:58:52.720");

            foreach (string stream in streamsWithoutTimestamp)
            {
                TestStreamWithoutTimeStamp(stream);
            }

            foreach (List<string> stream in streamsWithTimestamp)
            {
                TestStreamWithTimeStamp(stream[0], stream[1], stream[2]);
            }
        }
        
        public void TestStreamWithTimeStamp(string table, string startTime, string nextTime)
        {
            testDatabaseStreamer.StreamTable(table,Convert.ToDateTime(startTime),Convert.ToDateTime(nextTime));
            
            TestStream(table);
        }
        
        public void TestStreamWithoutTimeStamp(string table)
        {
            testDatabaseStreamer.StreamTableOneTime(table);
            
            TestStream(table);
        }

        public void TestStream(string table)
        {
            int rows = QueryTestData(testDatabaseStreamer._queryString, "../../../DataStreamingTest/QueryStreamedDataSetup.txt");
            
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