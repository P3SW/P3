using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Data;
using BlazorApp.Pages;
using Bunit;
using ExecuteSQLScript;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using Xunit.Abstractions;

namespace P3ConversionDashboard.Tests
{
    public class BlazorBackendFrontendIntegrationTest : TestContext
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public BlazorBackendFrontendIntegrationTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }


        [Fact]
        public async void TestBlazorBackendFrontend()
        {
            await Task.Run(() => SQLScriptExecuter.CreateDB("../../../BlazorBackendTest/DROP_ANS_DB_P3_TEST.sql"));
            await Task.Run(() => SQLScriptExecuter.CreateDB("../../../BlazorBackendTest/NEW_CREATE_ANS_DB_P3_TEST.sql"));
            await Task.Run(() => AssignTestData());
            
            JSInterop.Mode = JSRuntimeMode.Loose; // because of radzen
            
            var errorComponent = RenderComponent<Errors>();
            var reconComponent = RenderComponent<Reconciliations>();


            string renderedErrorMarkup = errorComponent.Markup;
            string renderedReconMarkup = reconComponent.Markup;

            foreach (ManagerStatusHandler manager in ConversionDataAssigner.FinishedManagers)
            {
                foreach (LogData error in manager.ErrorHandler.LogDataList)
                {
                    Assert.Contains(error.Description, renderedErrorMarkup);
                    Assert.Contains(error.Timestamp.ToString(), renderedErrorMarkup);
                    Assert.Contains(error.Grade, renderedErrorMarkup);
                    Assert.Contains(error.ManagerName, renderedErrorMarkup);
                }

                foreach (LogData reconciliation in manager.ReconciliationHandler.LogDataList)
                {
                    Assert.Contains(reconciliation.Description, renderedReconMarkup);
                    Assert.Contains(reconciliation.Timestamp.ToString(), renderedReconMarkup);
                    Assert.Contains(reconciliation.Grade, renderedReconMarkup);
                    Assert.Contains(reconciliation.ManagerName, renderedReconMarkup);
                }
            }
            //erases the list for the next test
            ConversionDataAssigner.FinishedManagers = new List<ManagerStatusHandler>();
        }
        
        
        
        

        public void AssignTestData()
        {
            ConversionDataAssigner.FinishedManagers = new List<ManagerStatusHandler>()
            {
                new ManagerStatusHandler("TestManager1", 1, DateTime.Now, 2),
                new ManagerStatusHandler("TestManager2", 2, DateTime.Now, 2),
                new ManagerStatusHandler("TestManager3", 3, DateTime.Now, 2),
                new ManagerStatusHandler("TestManager4", 4, DateTime.Now, 2)
            };

            foreach (ManagerStatusHandler manager in ConversionDataAssigner.FinishedManagers)
            {
                manager.ErrorHandler.AddLogData(DateTime.Now, GenerateRandomString(15),GenerateRandomString(8),"INFO");

                manager.ErrorHandler.AddLogData(DateTime.Now, GenerateRandomString(15), GenerateRandomString(8),
                    "WARN");
                manager.ErrorHandler.AddLogData(DateTime.Now, GenerateRandomString(15), GenerateRandomString(8),
                    "ERROR");
                manager.ErrorHandler.AddLogData(DateTime.Now, GenerateRandomString(15), GenerateRandomString(8),
                    "FATAL");

                manager.ReconciliationHandler.AddLogData(DateTime.Now, GenerateRandomString(15),
                    GenerateRandomString(8), "OK");
                manager.ReconciliationHandler.AddLogData(DateTime.Now, GenerateRandomString(15),
                    GenerateRandomString(8), "DISABLED");
                manager.ReconciliationHandler.AddLogData(DateTime.Now, GenerateRandomString(15),
                    GenerateRandomString(8), "MISMATCH");
                manager.ReconciliationHandler.AddLogData(DateTime.Now, GenerateRandomString(15),
                    GenerateRandomString(8), "FAIL MISMATCH");
                
                manager.Health.AddHealthData("CPU", 5, DateTime.Now);
                manager.Health.AddHealthData("CPU", 5, DateTime.Now);
                
                manager.Health.AddHealthData("MEMORY", 5, DateTime.Now);
                manager.Health.AddHealthData("MEMORY", 5, DateTime.Now);
            }
        }
        
        public  string GenerateRandomString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
 
            var random       = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }
        
    }
}