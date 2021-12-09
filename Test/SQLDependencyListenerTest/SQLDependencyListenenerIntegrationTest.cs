using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Data;
using DataStreamingSimulation;
using ExecuteSQLScript;
using Microsoft.Data.SqlClient;
using SQLDatabaseRead;
using Xunit;

namespace P3ConversionDashboard.Tests.SQLDependencyListenerTest
{
    [Collection("Sequential")]
    public class SQLDependencyListenenerIntegrationTest
    {
        //test health data
        private List<HealthData> cpu = new List<HealthData>()
        {
            new HealthData("CPU", 5, DateTime.Parse("2021-10-28 15:07:24.683")),
            new HealthData("CPU", 9, DateTime.Parse("2021-10-28 15:07:56.987")),
            new HealthData("CPU", 6, DateTime.Parse("2021-10-28 15:08:28.723")),
            new HealthData("CPU", 12, DateTime.Parse("2021-10-28 15:09:02.230")), 
            new HealthData("CPU", 14, DateTime.Parse("2021-10-28 15:09:34.173"))
        };

        //test health data
        private List<HealthData> memory = new List<HealthData>()
        {
            new HealthData("MEMORY", 6601781248, DateTime.Parse("2021-10-28 15:07:24.700")),
            new HealthData("MEMORY", 6552645632, DateTime.Parse("2021-10-28 15:07:56.987")),
            new HealthData("MEMORY", 6561001472, DateTime.Parse("2021-10-28 15:08:28.723")),
            new HealthData("MEMORY", 6566375424, DateTime.Parse("2021-10-28 15:09:02.230")),
            new HealthData("MEMORY", 6563631104, DateTime.Parse("2021-10-28 15:09:34.187"))
        };

        //tests the SQLDependencyListener.cs to check if it works as it should
        [Fact]
        public async void SQLDependencyTest()
        {
            //drops database if it exists
            await Task.Run(() => SQLScriptExecuter.CreateDB("../../../SQLDependencyListenerTest/DROP_ANS_DB_P3_TEST.sql"));
            
            //creates a new database
            await Task.Run(() => SQLScriptExecuter.CreateDB("../../../SQLDependencyListenerTest/NEW_CREATE_ANS_DB_P3_TEST.sql"));
            
            HealthDataHandler testHealthHandler = new HealthDataHandler(DateTime.Parse("2021-10-28 15:07:23.277"));
            
            SQLDependencyListener.Connection = new SqlConnection(ConfigReader.ReadSetupFile("../../../SQLDependencyListenerTest/SQLDependencyListenerSetup.txt"));
            SQLDependencyListener.Connection.Open();
            
            DatabaseStreamer testDatabaseStreamer = new DatabaseStreamer("../../../SQLDependencyListenerTest/DataStreamingSetup.txt", 
                "2021-10-28 15:07:23.277", "2021-10-28 15:10:06.043");
            testDatabaseStreamer.Stream(100);
            
            SQLDependencyListener testListener = new SQLDependencyListener(
                "SELECT REPORT_TYPE, REPORT_NUMERIC_VALUE, LOG_TIME FROM dbo.HEALTH_REPORT WHERE REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY' ORDER BY LOG_TIME",
                "SELECT REPORT_TYPE, REPORT_NUMERIC_VALUE, LOG_TIME FROM dbo.HEALTH_REPORT WHERE (REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY') " + 
                                    "AND LOG_TIME > '2021-10-28 15:07:23.277' ORDER BY LOG_TIME", 
                    testHealthHandler);
            
            testListener.StartListening();

            TimeSpan ts = DateTime.Parse("2021-10-28 15:10:06.043") - DateTime.Parse("2021-10-28 15:07:23.277");
            
            System.Threading.Thread.Sleep((int) ts.TotalMilliseconds / 10 + 1000);

            testListener.StopListening();
            
            //assert
            CheckSQLDependencyListener(testHealthHandler);
        }
        
        //checks that the data queried by the SQLDependencyListener is the correct data
        public void CheckSQLDependencyListener(HealthDataHandler healthDataHandler)
        {
            Assert.NotEmpty(healthDataHandler.Cpu);
            Assert.NotEmpty(healthDataHandler.Memory);

            for (int i = 0; i < 5; i++)
            {
                Assert.Equal(cpu[i].ReportType, healthDataHandler.Cpu[i].ReportType);
                Assert.Equal(cpu[i].NumericValue, healthDataHandler.Cpu[i].NumericValue);
                Assert.Equal(cpu[i].LogTime, healthDataHandler.Cpu[i].LogTime);
                
                Assert.Equal(memory[i].ReportType, healthDataHandler.Memory[i].ReportType);
                Assert.Equal(memory[i].NumericValue, healthDataHandler.Memory[i].NumericValue);
                Assert.Equal(memory[i].LogTime, healthDataHandler.Memory[i].LogTime);
            }
        }
        
        
        
    }
}