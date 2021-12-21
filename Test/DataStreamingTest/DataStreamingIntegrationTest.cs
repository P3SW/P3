using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Data;
using DataStreamingSimulation;
using ExecuteSQLScript;
using Microsoft.Data.SqlClient;
using Xunit;

namespace P3ConversionDashboard.Tests.DataStreamingTest
{
    [Collection("Sequential")]
    public class DataStreamingIntegrationTest
    {
        private DatabaseStreamer testDatabaseStreamer;

        //the number of rows for each streamed table
        private Dictionary<string, int> numberOfRows = new Dictionary<string, int>()
        {
            {"LOGGING", 2213}, {"MANAGERS", 118}, {"LOGGING_CONTEXT", 124}, 
            {"MANAGER_TRACKING", 32}, {"HEALTH_REPORT", 282}, {"ENGINE_PROPERTIES", 957}, 
            {"AFSTEMNING", 159}
        };

        //the tables that are streamed in the test which have timestamps
        private List<List<string>> streamsWithTimestamp = new List<List<string>>()
        {
            new List<string>() {"AFSTEMNING", "2021-10-28 15:07:56.987", "2021-10-28 15:28:02.323"},
            new List<string>() {"LOGGING", "2021-10-28 15:07:56.987", "2021-10-28 15:28:02.323"},
            new List<string>() {"MANAGER_TRACKING", "2021-10-28 15:07:56.987", "2021-10-28 15:28:02.323"},
            new List<string>() {"HEALTH_REPORT", "2021-10-28 15:07:56.987", "2021-10-28 15:28:02.323"},
            new List<string>() {"ENGINE_PROPERTIES", "2021-10-28 15:07:56.987", "2021-10-28 15:28:02.323"}
        };

        //the tables that are streamed in the test which dont have timestamps
        private List<string> streamsWithoutTimestamp = new List<string>() { "MANAGERS", "LOGGING_CONTEXT" };

        //tests if the database streamer, streams the correct amount of data
        [Fact]
        public async void DatabaseStreamTest()
        {
            //drops database if it exitsts
            await Task.Run(() => SQLScriptExecuter.ExecuteMultipleLines("../../../DataStreamingTest/DROP_ANS_DB_P3_TEST.sql"));
            
            //creates new database
            await Task.Run(() => SQLScriptExecuter.ExecuteMultipleLines("../../../DataStreamingTest/NEW_CREATE_ANS_DB_P3_TEST.sql"));
            
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

        //runs the stream
        public void TestStream(string table)
        {
            int rows = QueryTestData(testDatabaseStreamer.QueryString, "../../../DataStreamingTest/QueryStreamedDataSetup.txt");
            
            System.Threading.Thread.Sleep(1000);

            //checks if it has streamed the correct amount of rows
            Assert.Equal(numberOfRows[table], rows);
        }
        
        //queries the data streamed
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